using System;

namespace API.ErrorTypes
{
    public class ApiResponse
    {
        #region Properties
        public int Status { get; set; }
        public string Poruka { get; set; }
        #endregion

        public ApiResponse(int status, string poruka = null) 
        {
            Status = status;
            Poruka = poruka ?? GetPorukaZaStatus(status);
        }

        private string GetPorukaZaStatus(int status)
        {
            switch(status)
            {
                case 400:
                    return "Zahtjev nije dobro formiran";
                case 401:
                    return "Nemate dozvolu za ovu akciju";
                case 404:
                    return "Resurs nije pronađen";
                case 500:
                    return "Greška na serveru";
                default:
                    return null;
            }
        }

    }
}