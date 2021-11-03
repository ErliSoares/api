using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domain.Core;
using FluentValidation;

namespace Domain.Entities
{
    /// <summary>
    ///     Classe base para todas as entidades
    /// </summary>
    public abstract class BaseEntity
    {
        private static IValidator _inserUpdateValitor;
        private static IValidator _deleteValitor;

        /// <summary>
        ///     Identificador único do registro
        /// </summary>
        [Key]
        public int Id { get; set; }

        private IValidator DeleteValitor
        {
            get
            {
                if (_deleteValitor == null)
                {
                    _deleteValitor = ConfigureDeleteValitor();
                }
                return _deleteValitor;
            }
        }

        private IValidator InserUpdateValitor
        {
            get
            {
                if (_inserUpdateValitor == null)
                {
                    _inserUpdateValitor = ConfigureInserUpdateValitor();
                }
                return _inserUpdateValitor;
            }
        }

        protected abstract IValidator ConfigureInserUpdateValitor();

        protected abstract IValidator ConfigureDeleteValitor();

        /// <summary>
        ///     Valida a entidade atual para inserir ou atualizar
        /// </summary>
        /// <returns>Retorna resultado da validação</returns>
        public async Task<ValidationApiResult> ValidateToInsertUpdate()
        {
            return new(await InserUpdateValitor.ValidateAsync(new ValidationContext<BaseEntity>(this)));
        }

        /// <summary>
        ///     Valida a entidade atual para excluir o registro
        /// </summary>
        /// <returns>Retorna resultado da validação</returns>
        public async Task<ValidationApiResult> ValidateToDelete()
        {
            return new(await DeleteValitor.ValidateAsync(new ValidationContext<BaseEntity>(this)));
        }
    }
}
