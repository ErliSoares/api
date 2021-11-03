using System;
using Domain.Entities;

namespace Application.Dtos
{
    /// <summary>
    ///     Representa um produto, utilizado para transferência de dados entre a api e o cliente
    /// </summary>
    public class ProdutoDto : BaseDto
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

        /// <inheritdoc />
        public override ProdutoEntity ToEntity()
        {
            return new()
            {
                DataCadastro = DataCadastro,
                Descricao = Descricao,
                Id = Id,
                Quantidade = Quantidade,
                Tipo = Tipo,
                ValorUnitario = ValorUnitario
            };
        }
    }
}
