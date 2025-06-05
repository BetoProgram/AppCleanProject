using System.Net;

namespace AppCleanProject.Application.Commons.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode Codigo { get; set; }
        public object Errores { get; set; }
        public CustomException(HttpStatusCode codigo, object? errores = null)
        {
            Codigo = codigo;
            Errores = errores!;
        }
    }
}
