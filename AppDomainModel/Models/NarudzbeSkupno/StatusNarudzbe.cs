using System.Runtime.Serialization;

namespace AppDomainModel.Models.NarudzbeSkupno
{
    public enum StatusNarudzbe
    {
        [EnumMember(Value = "Na čekanju")]
        NaCekanju,

        [EnumMember(Value = "Plaćanje izvršeno")]
        PlacanjeIzvrseno,

        [EnumMember(Value = "Plaćanje neuspješno")]
        PlacanjeNeuspjesno,
    }
}