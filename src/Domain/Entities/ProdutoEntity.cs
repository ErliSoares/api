using System;
using FluentValidation;

namespace Domain.Entities
{
    /// <summary>
    ///     Representa uma entidade de produto para
    /// </summary>
    public class ProdutoEntity : BaseEntity
    {
        /// <summary>
        ///     Descrição do produto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        ///     Valor unitário
        /// </summary>
        public double ValorUnitario { get; set; }

        /// <summary>
        ///     Data de cadastro
        /// </summary>
        public DateTime DataCadastro { get; set; }

        /// <summary>
        ///     Quantidade
        /// </summary>
        public double Quantidade { get; set; }

        /// <summary>
        ///     Tipo do produto, podendo ser alimento, limpeza ou outros
        /// </summary>
        public ProdutoTipo Tipo { get; set; }

        protected override IValidator ConfigureInserUpdateValitor()
        {
            var validator = new InlineValidator<ProdutoEntity>();
            validator.RuleFor(p => p.ValorUnitario).GreaterThan(0);
            return validator;
        }


        protected override IValidator ConfigureDeleteValitor()
        {
            var validator = new InlineValidator<ProdutoEntity>();
            validator.RuleFor(p => p.Quantidade).Equal(0);
            return validator;
        }
    }
}
