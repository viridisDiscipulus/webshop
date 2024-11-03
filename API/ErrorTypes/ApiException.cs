namespace API.ErrorTypes
{
    public class ApiException : ApiResponse
    {
        #region Properies
        
        public string Opis { get; set; }
        
        #endregion

        public ApiException(int status, string poruka = null, string opis = null) : base(status, poruka)
        {
            Opis = opis;

        }


    }
}