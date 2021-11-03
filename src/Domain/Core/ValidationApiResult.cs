using System.Collections.Generic;
using FluentValidation.Results;

namespace Domain.Core
{
    /// <summary>
    ///     Representa o resultado da validação dos dados
    /// </summary>
    public class ValidationApiResult
    {
        public ValidationApiResult()
        {
            Errors = new List<ValidationApiFailure>();
        }

        public ValidationApiResult(ValidationResult validationResult)
        {
            Errors = new List<ValidationApiFailure>();

            foreach (var error in validationResult.Errors) Errors.Add(new ValidationApiFailure(error));
        }

        public ValidationApiResult(List<ValidationApiFailure> failures)
        {
            Errors = failures;
        }

        /// <summary>
        ///     Lista de erros
        /// </summary>
        public List<ValidationApiFailure> Errors { get; }

        /// <summary>
        ///     Define se tem errro de validação
        /// </summary>
        public bool HasError => Errors.Count > 0;

        /// <summary>
        ///     Adiciona um novo erro de validação
        /// </summary>
        /// <param name="errorCode">Código único do tipo de erro</param>
        /// <param name="errorMessage">Mensagem a ser exibida para o usuário</param>
        /// <param name="propertyName">Nome da propriedade a que o erro inserido representa</param>
        public void AddFaliure(string errorCode, string errorMessage, string propertyName)
        {
            Errors.Add(new ValidationApiFailure(errorCode, errorMessage, propertyName));
        }
    }
}
