using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AppDomainModel.Interfaces;
using AppDomainModel.Model;
using AppDomainModel.Models.NarudzbeSkupno;
using Azure;
using Microsoft.Data.SqlClient;

namespace DataAccess.Services
{
    public class NarudzbaService : INarudzbaService
    {
        private readonly IGenericService<Narudzba> _narudzbaService;
        private readonly IGenericService<NacinIsporuke> _nacinIsporukeService;
        private readonly IGenericService<Proizvod> _proizvodService;
        private readonly IKosaricaService _kosaricaService;
        private readonly IGenericService<NarucenProizvodArtikl> _narucenProizvodArtiklService;
        private readonly IGenericRepository<NaruceniArtikl> _naruceniArtiklService;
        private readonly IGenericService<Adresa> _adresa;

        public NarudzbaService(IGenericService<Narudzba> narudzbaService, IGenericService<NacinIsporuke> nacinIsporukeService, IGenericService<Proizvod> proizvodService, IKosaricaService kosaricaService, IGenericService<NarucenProizvodArtikl> narucenProizvodArtiklService, IGenericRepository<NaruceniArtikl> naruceniArtiklService, IGenericService<Adresa> adresa)
        {
            _narudzbaService = narudzbaService;
            _nacinIsporukeService = nacinIsporukeService;
            _proizvodService = proizvodService;
            _kosaricaService = kosaricaService;
            _narucenProizvodArtiklService = narucenProizvodArtiklService;
            _naruceniArtiklService = naruceniArtiklService;
            _adresa = adresa;
        }

        public async Task<Narudzba> CreateNarudzbuAsync(string kupacEmail, int nacinIsporukeId, string kosaricaId, Adresa adresaDostave)
        {
            // Dohvat kosarice
            var kosarica = await _kosaricaService.GetKosaricaKupacAsync(kosaricaId);
            if (kosarica == null)
                throw new Exception("Košarica nije pronađena");
           
           // Dohvat artikala iz kosarice
            var artikliNarudzbe = new List<NaruceniArtikl>();
            foreach (var artikl in kosarica.Artikli)
            {
                string proizvodQuery = @"
                SELECT 
                    p.Id,
                    p.Naziv,
                    p.Opis,
                    p.Cijena,
                    p.SlikaUrl,
                    p.VrstaProizvodaID,
                    vp.Naziv AS VrstaProizvodaNaziv,
                    p.RobnaMarkaID,
                    rm.Naziv AS RobnaMarkaNaziv
                FROM 
                    Proizvod p
                LEFT JOIN 
                    VrstaProizvoda vp ON p.VrstaProizvodaID = vp.ID
                LEFT JOIN 
                    RobnaMarka rm ON p.RobnaMarkaID = rm.ID
                WHERE 
                    p.Id = @ID;";

                var naruceniProizvod = await _proizvodService.UcitajPoIdAsync(proizvodQuery, dr => new Proizvod
                {
                    Id = (int)dr["Id"],
                    Naziv = (string)dr["Naziv"],
                    Opis = (string)dr["Opis"],
                    Cijena = (decimal)dr["Cijena"],
                    SlikaUrl = (string)dr["SlikaUrl"],
                    VrstaProizvodaID = (int)dr["VrstaProizvodaID"],
                    VrstaProizvoda = new VrstaProizvoda
                    {
                        Id = (int)dr["VrstaProizvodaID"],
                        Naziv = (string)dr["VrstaProizvodaNaziv"]
                    },
                    RobnaMarkaID = (int)dr["RobnaMarkaID"],
                    RobnaMarka = new RobnaMarka
                    {
                        Id = (int)dr["RobnaMarkaID"],
                        Naziv = (string)dr["RobnaMarkaNaziv"]
                    }
                }, new SqlParameter("@ID", artikl.Id));

                #region NarucenProizvodArtikl
                    var naruceniProizvodArtikl = new NarucenProizvodArtikl(naruceniProizvod.Id, naruceniProizvod.Naziv, naruceniProizvod.SlikaUrl);

                    string narucenProizvodArtiklQuery = @"
                    INSERT INTO dbo.NarucenProizvodArtikl (ProizvodArtiklId, ProizvodNaziv, SlikaUrl)
                    VALUES (@ProizvodArtiklId, @ProizvodNaziv, @SlikaUrl);";

                    var parametriNarucenProizvodArtikl = new[]
                    {
                        new SqlParameter("@ProizvodArtiklId", naruceniProizvodArtikl.ProizvodArtiklId),
                        new SqlParameter("@ProizvodNaziv", naruceniProizvodArtikl.ProizvodNaziv),
                        new SqlParameter("@SlikaUrl", naruceniProizvodArtikl.SlikaUrl)
                    };

                    int narucenProizvodArtiklId = await _narucenProizvodArtiklService.DodajAsync(narucenProizvodArtiklQuery, parametriNarucenProizvodArtikl);

                    naruceniProizvodArtikl.Id = narucenProizvodArtiklId;
                #endregion

               #region NaruceniArtikl
                    var naruceniArtikl = new NaruceniArtikl(naruceniProizvodArtikl, naruceniProizvod.Cijena, artikl.Kolicina);

                    string naruceniArtiklQuery = @"
                    INSERT INTO [dbo].[NaruceniArtikl]
                        ([ArtiklNaruceniId], [Cijena], [Kolicina])
                    VALUES (@ArtiklNaruceniId, @Cijena, @Kolicina);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);"; 

                    var parametriNaruceniArtikl = new[]
                    {
                        new SqlParameter("@ArtiklNaruceniId", naruceniArtikl.ArtiklNaruceni.Id),
                        new SqlParameter("@Cijena", naruceniArtikl.Cijena),
                        new SqlParameter("@Kolicina", naruceniArtikl.Kolicina)
                    };

                    int naruceniArtiklId = await _naruceniArtiklService.DodajAsync(naruceniArtiklQuery, parametriNaruceniArtikl);

                    naruceniArtikl.Id = naruceniArtiklId;

                    var na = new NaruceniArtikl();
                    na.Id = naruceniArtiklId;
                    na.ArtiklNaruceni = naruceniProizvodArtikl;
                    na.Cijena = naruceniProizvod.Cijena;
                    na.Kolicina = artikl.Kolicina;
                #endregion


                artikliNarudzbe.Add(na);
            }

            // Dohvat nacina isporuke
            string nacinIsporukeQuery = @"
                SELECT 
                     ni.Id
                    ,ni.KratkiNaziv
                    ,ni.VrijemeDostave
                    ,ni.Opis
                    ,ni.Cijena
                FROM dbo.NacinIsporuke ni
                WHERE 
                ni.Id = @ID;";
            
            var nacinIsporukeObj = await _nacinIsporukeService.UcitajPoIdAsync(nacinIsporukeQuery, dr => new NacinIsporuke
                {
                    Id = (int)dr["Id"],
                    KratkiNaziv = (string)dr["KratkiNaziv"],
                    VrijemeDostave = (string)dr["KratkiNaziv"],
                    Opis = (string)dr["Opis"],
                    Cijena = (decimal)dr["Cijena"],
                }, new SqlParameter("@ID", nacinIsporukeId));

            var ukupnaCijena = artikliNarudzbe.Sum(x => x.Cijena * x.Kolicina); 

            string stringKolekcijaId = string.Join(",", artikliNarudzbe.Select(x => x.Id));

            var novaNarudzba = new Narudzba(artikliNarudzbe, kupacEmail, adresaDostave, nacinIsporukeObj, ukupnaCijena);

            string narudzbaQuery = @"
            INSERT INTO dbo.Narudzba (KupacEmail, DatumNarudzbe, AdresaDostaveId, NacinIsporukeId, NaruceniArtikliId, UkupnaCijena, Status)
            VALUES (@KupacEmail, @DatumNarudzbe, @AdresaDostaveId, @NacinIsporukeId, @NaruceniArtikliId, @UkupnaCijena, @Status);";

            var parametri = new[]
            {
                new SqlParameter("@KupacEmail", novaNarudzba.KupacEmail),
                new SqlParameter("@DatumNarudzbe", novaNarudzba.DatumNarudzbe),
                new SqlParameter("@AdresaDostaveId", novaNarudzba.AdresaDostave.Id), 
                new SqlParameter("@NacinIsporukeId", novaNarudzba.NacinIsporuke.Id), 
                new SqlParameter("@NaruceniArtikliId", stringKolekcijaId),
                new SqlParameter("@UkupnaCijena", novaNarudzba.UkupnaCijena),
                new SqlParameter("@Status", novaNarudzba.Status)
            };

            int narudzbaId = await _narudzbaService.DodajAsync(narudzbaQuery, parametri);

            novaNarudzba.Id = narudzbaId;

            bool narudzbaUspjesnoSpremljena = narudzbaId > 0;

            if (!narudzbaUspjesnoSpremljena)
                return null;
            else {
                await _kosaricaService.DeleteKosaricaKupacAsync(kosaricaId);
                return novaNarudzba;
            }
        }

        public async Task<IReadOnlyList<NacinIsporuke>> GetNacineIsporukeAsync()
        {
            string query = @"
                SELECT  [Id]
                        ,[KratkiNaziv]
                        ,[VrijemeDostave]
                        ,[Opis]
                        ,[Cijena]
                    FROM [dbo].[NacinIsporuke]";

                var naruceniProizvod = await _nacinIsporukeService.UcitajSveAsync(query, dr => new NacinIsporuke
                {
                    Id = (int)dr["Id"],
                    KratkiNaziv = (string)dr["KratkiNaziv"],
                    VrijemeDostave = (string)dr["VrijemeDostave"],
                    Opis = (string)dr["Opis"],
                    Cijena = (decimal)dr["Cijena"]
                });

                return (IReadOnlyList<NacinIsporuke>)naruceniProizvod;

        }

        public async Task<Narudzba> GetNarudzbaByIdAsync(int id, string email)
        {
            string query = string.Empty;

            #region Secure query
            //  query = @"
            //    SELECT  n.[Id]
            //             ,n.[KupacEmail]
            //             ,n.[DatumNarudzbe]
            //             ,a.[iD] AS AdresaDostaveId
			// 			,ISNULL(a.[Ime], '') AS Ime
            //             ,ISNULL(a.[Prezime], '') AS Prezime
            //             ,ISNULL(a.[Ulica], '') AS Ulica
            //             ,ISNULL(a.[Grad], '') AS Grad
            //             ,ISNULL(a.[PostanskiBroj], '') AS PostanskiBroj
            //             ,ISNULL(a.[Drzava], '') AS Drzava
            //             ,i.[iD] AS NacinIsporukeId
            //             ,i.[KratkiNaziv]
            //             ,i.[VrijemeDostave]
            //             ,i.[Opis]
            //             ,i.[Cijena]
            //             ,n.[NaruceniArtikliId]
            //             ,n.[UkupnaCijena]
            //             ,n.[Status]
            //         FROM [dbo].[Narudzba] n
            //         LEFT JOIN [dbo].[Adrese] a ON n.AdresaDostaveId = 0
            //         LEFT JOIN [dbo].[NacinIsporuke] i ON n.NacinIsporukeId = i.Id
            //         WHERE n.[Id] = @ID AND [KupacEmail] = @Email;";
            #endregion

             query = @"
               SELECT  n.[Id]
                        ,n.[KupacEmail]
                        ,n.[DatumNarudzbe]
                        ,a.[iD] AS AdresaDostaveId
						,ISNULL(a.[Ime], '') AS Ime
                        ,ISNULL(a.[Prezime], '') AS Prezime
                        ,ISNULL(a.[Ulica], '') AS Ulica
                        ,ISNULL(a.[Grad], '') AS Grad
                        ,ISNULL(a.[PostanskiBroj], '') AS PostanskiBroj
                        ,ISNULL(a.[Drzava], '') AS Drzava
                        ,i.[iD] AS NacinIsporukeId
                        ,i.[KratkiNaziv]
                        ,i.[VrijemeDostave]
                        ,i.[Opis]
                        ,i.[Cijena]
                        ,n.[NaruceniArtikliId]
                        ,n.[UkupnaCijena]
                        ,n.[Status]
                    FROM [dbo].[Narudzba] n
                    LEFT JOIN [dbo].[Adrese] a ON n.AdresaDostaveId = 0
                    LEFT JOIN [dbo].[NacinIsporuke] i ON n.NacinIsporukeId = i.Id
                    WHERE n.[Id] = @ID";

                var narudzba = await _narudzbaService.UcitajPoIdAsync(query, dr => new Narudzba
                {
                    Id = (int)dr["Id"],
                    KupacEmail = (string)dr["KupacEmail"],
                    DatumNarudzbe = (DateTimeOffset)dr["DatumNarudzbe"],
                    AdresaDostave = new Adresa {
                        Id = (int)dr["AdresaDostaveId"],
                        Ime = (string)dr["Ime"],
                        Prezime = (string)dr["Prezime"],
                        Ulica = (string)dr["Ulica"],
                        Grad = (string)dr["Grad"],
                        PostanskiBroj = (string)dr["PostanskiBroj"],
                        Drzava = (string)dr["Drzava"]
                    },
                    NacinIsporuke = new NacinIsporuke{
                        Id = (int)dr["NacinIsporukeId"],
                        KratkiNaziv = (string)dr["KratkiNaziv"],
                        VrijemeDostave = (string)dr["VrijemeDostave"],
                        Opis = (string)dr["Opis"],
                        Cijena = (decimal)dr["Cijena"]
                    },
                    NaruceniArtikliId = (string)dr["NaruceniArtikliId"],
                    UkupnaCijena = (decimal)dr["UkupnaCijena"],
                    Status = ParseEnumFromEnumMember<StatusNarudzbe>(dr["Status"].ToString())
                }, new SqlParameter("@ID", id)
                //  ,new SqlParameter("@Email", email)
                 );
               
               return narudzba;
        }
        public async Task<Narudzba> UcitajArtikleNarudzbe(Narudzba narudzba)
        {
             foreach (string naruceniArtiklId in narudzba.NaruceniArtikliId.Split(','))
                {
                    string naruceniArtiklQuery = @"
                        SELECT  na.Id AS NaruceniArtiklId,
                                npa.Id AS NarucenProizvodArtiklId,
                                npa.ProizvodArtiklId,
                                npa.ProizvodNaziv,
                                npa.SlikaUrl,
                                na.Cijena,
                                na.Kolicina
                            FROM NaruceniArtikl na
                            LEFT JOIN NarucenProizvodArtikl npa ON npa.Id = na.ArtiklNaruceniId
                            WHERE na.Id = @ID;
                            ";

                    int id;
                    Int32.TryParse(naruceniArtiklId, out id);

                    var naruceniArtikl = await _naruceniArtiklService.UcitajPoIdAsync(naruceniArtiklQuery, dr => new NaruceniArtikl
                    {
                        Id = (int)dr["NaruceniArtiklId"],
                        ArtiklNaruceni = new NarucenProizvodArtikl
                        {
                            Id = (int)dr["NarucenProizvodArtiklId"],
                            ProizvodArtiklId = (int)dr["ProizvodArtiklId"],
                            ProizvodNaziv = (string)dr["ProizvodNaziv"],
                            SlikaUrl = (string)dr["SlikaUrl"]
                        },
                        Cijena = (decimal)dr["Cijena"],
                        Kolicina = (int)dr["Kolicina"]
                    }, new SqlParameter("@ID",id));

                    if (narudzba.NaruceniArtikli == null)
                    {
                        narudzba.NaruceniArtikli = new List<NaruceniArtikl>();
                    }

                    narudzba.NaruceniArtikli.Add(naruceniArtikl);
                }

                return narudzba;
        }


        public async Task<List<Narudzba>> GetNarudzbeKupcaAsync(string kupacEmail)
        {
            string query = @"
               SELECT  n.[Id]
                        ,n.[KupacEmail]
                        ,n.[DatumNarudzbe]
                        ,ISNULL(a.[iD], 1) AS AdresaDostaveId
						,ISNULL(a.[Ime], '') AS Ime
                        ,ISNULL(a.[Prezime], '') AS Prezime
                        ,ISNULL(a.[Ulica], '') AS Ulica
                        ,ISNULL(a.[Grad], '') AS Grad
                        ,ISNULL(a.[PostanskiBroj], '') AS PostanskiBroj
                        ,ISNULL(a.[Drzava], '') AS Drzava
                        ,i.[iD] AS NacinIsporukeId
                        ,i.[KratkiNaziv]
                        ,i.[VrijemeDostave]
                        ,i.[Opis]
                        ,i.[Cijena]
                        ,ISNULL(n.[NaruceniArtikliId], 0) AS NaruceniArtikliId
                        ,n.[UkupnaCijena]
                        ,n.[Status]
                    FROM [dbo].[Narudzba] n
                    LEFT JOIN [dbo].[Adrese] a ON n.AdresaDostaveId = 0
                    LEFT JOIN [dbo].[NacinIsporuke] i ON n.NacinIsporukeId = i.Id";

                query += $" WHERE [KupacEmail] = '{kupacEmail.Replace("'", "''")}';";  

                var narudzba = await _narudzbaService.UcitajSveAsync(query, dr => new Narudzba
                {
                    Id = (int)dr["Id"],
                    KupacEmail = (string)dr["KupacEmail"],
                    DatumNarudzbe = (DateTimeOffset)dr["DatumNarudzbe"],
                    AdresaDostave = new Adresa {
                        Id = (int)dr["AdresaDostaveId"],
                        Ime = (string)dr["Ime"],
                        Prezime = (string)dr["Prezime"],
                        Ulica = (string)dr["Ulica"],
                        Grad = (string)dr["Grad"],
                        PostanskiBroj = (string)dr["PostanskiBroj"],
                        Drzava = (string)dr["Drzava"]
                    },
                    NacinIsporuke = new NacinIsporuke{
                        Id = (int)dr["NacinIsporukeId"],
                        KratkiNaziv = (string)dr["KratkiNaziv"],
                        VrijemeDostave = (string)dr["VrijemeDostave"],
                        Opis = (string)dr["Opis"],
                        Cijena = (decimal)dr["Cijena"]
                    },
                    NaruceniArtikliId = (string)dr["NaruceniArtikliId"],
                    UkupnaCijena = (decimal)dr["UkupnaCijena"],
                    Status = ParseEnumFromEnumMember<StatusNarudzbe>(dr["Status"].ToString())
                });

              return (List<Narudzba>)narudzba;;
        }
        public async Task<List<Narudzba>> UcitajArtikleNarudzbi(List<Narudzba> narudzbe)
        {
            foreach (var narudzba in narudzbe)
            {
                foreach (string naruceniArtiklId in narudzba.NaruceniArtikliId.Split(','))
                {
                    string naruceniArtiklQuery = @"
                        SELECT  na.Id AS NaruceniArtiklId,
                                npa.Id AS NarucenProizvodArtiklId,
                                npa.ProizvodArtiklId,
                                npa.ProizvodNaziv,
                                npa.SlikaUrl,
                                na.Cijena,
                                na.Kolicina
                            FROM NaruceniArtikl na
                            LEFT JOIN NarucenProizvodArtikl npa ON npa.Id = na.ArtiklNaruceniId
                            WHERE na.Id = @ID;
                            ";

                    int id;
                    Int32.TryParse(naruceniArtiklId, out id);

                    var naruceniArtikl = await _naruceniArtiklService.UcitajPoIdAsync(naruceniArtiklQuery, dr => new NaruceniArtikl
                    {
                        Id = (int)dr["NaruceniArtiklId"],
                        ArtiklNaruceni = new NarucenProizvodArtikl
                        {
                            Id = (int)dr["NarucenProizvodArtiklId"],
                            ProizvodArtiklId = (int)dr["ProizvodArtiklId"],
                            ProizvodNaziv = (string)dr["ProizvodNaziv"],
                            SlikaUrl = (string)dr["SlikaUrl"]
                        },
                        Cijena = (decimal)dr["Cijena"],
                        Kolicina = (int)dr["Kolicina"]
                    }, new SqlParameter("@ID",id));

                    if (narudzba.NaruceniArtikli == null)
                    {
                        narudzba.NaruceniArtikli = new List<NaruceniArtikl>();
                    }

                    narudzba.NaruceniArtikli.Add(naruceniArtikl);
                }

            }

            return narudzbe;
        }

        public async Task<List<Narudzba>> GetNarudzbeKupcaZaAdministratoraAsync(){
             string query = @"
               SELECT  n.[Id]
                        ,n.[KupacEmail]
                        ,n.[DatumNarudzbe]
                        ,ISNULL(a.[iD], 1) AS AdresaDostaveId
						,ISNULL(a.[Ime], '') AS Ime
                        ,ISNULL(a.[Prezime], '') AS Prezime
                        ,ISNULL(a.[Ulica], '') AS Ulica
                        ,ISNULL(a.[Grad], '') AS Grad
                        ,ISNULL(a.[PostanskiBroj], '') AS PostanskiBroj
                        ,ISNULL(a.[Drzava], '') AS Drzava
                        ,i.[iD] AS NacinIsporukeId
                        ,i.[KratkiNaziv]
                        ,i.[VrijemeDostave]
                        ,i.[Opis]
                        ,i.[Cijena]
                        ,ISNULL(n.[NaruceniArtikliId], 0) AS NaruceniArtikliId
                        ,n.[UkupnaCijena]
                        ,n.[Status]
                    FROM [dbo].[Narudzba] n
                    LEFT JOIN [dbo].[Adrese] a ON n.AdresaDostaveId = 0
                    LEFT JOIN [dbo].[NacinIsporuke] i ON n.NacinIsporukeId = i.Id";

                var narudzba = await _narudzbaService.UcitajSveAsync(query, dr => new Narudzba
                {
                    Id = (int)dr["Id"],
                    KupacEmail = (string)dr["KupacEmail"],
                    DatumNarudzbe = (DateTimeOffset)dr["DatumNarudzbe"],
                    AdresaDostave = new Adresa {
                        Id = (int)dr["AdresaDostaveId"],
                        Ime = (string)dr["Ime"],
                        Prezime = (string)dr["Prezime"],
                        Ulica = (string)dr["Ulica"],
                        Grad = (string)dr["Grad"],
                        PostanskiBroj = (string)dr["PostanskiBroj"],
                        Drzava = (string)dr["Drzava"]
                    },
                    NacinIsporuke = new NacinIsporuke{
                        Id = (int)dr["NacinIsporukeId"],
                        KratkiNaziv = (string)dr["KratkiNaziv"],
                        VrijemeDostave = (string)dr["VrijemeDostave"],
                        Opis = (string)dr["Opis"],
                        Cijena = (decimal)dr["Cijena"]
                    },
                    NaruceniArtikliId = (string)dr["NaruceniArtikliId"],
                    UkupnaCijena = (decimal)dr["UkupnaCijena"],
                    Status = ParseEnumFromEnumMember<StatusNarudzbe>(dr["Status"].ToString())
                });

              return (List<Narudzba>)narudzba;;
        }
        public static TEnum ParseEnumFromEnumMember<TEnum>(string value) where TEnum : Enum
            {
                foreach (var field in typeof(TEnum).GetFields())
                {
                    var attribute = Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) as EnumMemberAttribute;
                    if (attribute != null && attribute.Value == value)
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }

                return (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase: true);
            }
    }


}