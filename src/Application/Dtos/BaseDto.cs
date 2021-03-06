using Domain.Entities;

namespace Application.Dtos
{
    /// <summary>
    ///     Base para todas as classes de transferĂȘncia de dados entre fontes externas e o sistema
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        ///     Identificador Ășnico do registro
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Converte o DTO atual para uma entidade
        /// </summary>
        /// <returns>Entidade</returns>
        public abstract BaseEntity ToEntity();
    }
}
