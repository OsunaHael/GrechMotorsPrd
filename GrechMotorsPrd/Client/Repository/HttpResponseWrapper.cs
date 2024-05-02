using System.Net;

namespace GrechMotorsPrd.Client.Repository
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
        {
            Error = error;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Error { get; set; }
        public T? Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        public async Task<string> GetErrorMessage()
        {
            if(!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode;

            if(statusCode == HttpStatusCode.NotFound)
            {
                return "No se encontró el recurso solicitado";
            } 
            else if(statusCode == HttpStatusCode.BadRequest) 
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync();
            } 
            else if(statusCode == HttpStatusCode.Unauthorized)
            {
                return "No está autorizado para realizar esta acción";
            }
            else if(statusCode == HttpStatusCode.Forbidden)
            {
                return "No tiene permiso para realizar esta acción";
            }
            else
            {
                return "Ocurrió un error inesperado, por favor inténtelo de nuevo más tarde";
            }
        }
    }
}
