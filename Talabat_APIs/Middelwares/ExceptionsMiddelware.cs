using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middelwares
{
	public class ExceptionsMiddelware
	{
		private readonly RequestDelegate Next;
		private readonly ILogger<ExceptionsMiddelware> Logger;
		private readonly IWebHostEnvironment Env;

		public ExceptionsMiddelware(RequestDelegate next,ILogger<ExceptionsMiddelware> logger,IWebHostEnvironment env)
        {
			this.Next = next;
			this.Logger = logger;
			this.Env = env;
		}
        public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await Next.Invoke(httpContext);
			}
			catch (Exception ex)
			{

				Logger.LogError(ex.Message);

				httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var response = Env.IsDevelopment() ?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
					:
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);

				var json = JsonSerializer.Serialize(response);
				httpContext.Response.WriteAsync(json);
			}

			
		}
	}
}
