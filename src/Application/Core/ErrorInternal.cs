using System;

namespace Application.Core
{
    /// <summary>
    ///     Representa erro interno do servidor, status 500.
    /// </summary>
    public class ErrorInternal
    {
        /// <summary>
        ///     Cria uma instância da classe <see cref="ErrorInternal" />.
        /// </summary>
        /// <param name="exception">Exceção criada no código.</param>
        public ErrorInternal(Exception exception)
        {
            Exception = exception;
            Message = exception.Message;
        }

        /// <summary>
        ///     Mensagem com informação sobre o erro
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Detalhes do erro
        /// </summary>
        public object Exception { get; set; }
    }
}
