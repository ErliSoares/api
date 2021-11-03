using FluentValidation.Results;

namespace Domain.Core
{
    /// <summary>
    ///     Representa um erro de validação
    /// </summary>
    public class ValidationApiFailure
    {
        public ValidationApiFailure()
        {
        }

        public ValidationApiFailure(ValidationFailure error)
        {
            ErrorCode = error.ErrorCode;
            ErrorMessage = error.ErrorMessage;
            PropertyName = error.PropertyName;
        }

        public ValidationApiFailure(string errorCode, string errorMessage, string propertyName)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        /// <summary>
        ///     Código único do tipo de erro
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        ///     Mensagens de erro a ser exibida para o usuário final
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     Nome da propriedade que retornou esse erro
        /// </summary>
        public string PropertyName { get; set; }
    }
}
