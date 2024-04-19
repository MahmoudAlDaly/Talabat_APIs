namespace Talabat.APIs.Errors
{
	public class ApiResponse
	{
        public int StatusCode { get; set; }
		public string? Message { get; set; }


        public ApiResponse(int statuscode , string? message = null)
        {
            StatusCode = statuscode ;
            Message = message?? GetDefaultMessageForSatatusCode(statuscode);
        }

        private string? GetDefaultMessageForSatatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => " A Bad Request you have made",
                401 => " Authorized you are not",
                404 => "resource is not found",
                500 => "Errors are the path",
                _ => null,
            };
        }
    }
}
