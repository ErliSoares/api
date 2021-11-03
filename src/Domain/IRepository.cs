using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain
{
    /// <summary>
    ///     Reponsável por armazenar os dados do objeto <typeparamref name="T" />
    /// </summary>
    /// <typeparam name="T">Entidade para armazenar os dados</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        ///     Insere uma entidade no banco de dados
        /// </summary>
        /// <param name="entity">Entidade para ser salva</param>
        /// <returns>Retorna a entidade salva com a chave primária preenchido</returns>
        Task<T> Insert(T entity);

        /// <summary>
        ///     Atualiza uma entidade no banco de dados
        /// </summary>
        /// <param name="entity">Entidade para ser autualizada</param>
        /// <returns>Entidade atualizada</returns>
        Task<T> Update(T entity);

        /// <summary>
        ///     Exclui o registro no banco de dados
        /// </summary>
        /// <param name="id">Código do registro a ser excluído</param>
        /// <returns>
        ///     Retorna <see langword="true" /> se foi encontrado o registro
        ///     com a chave e excluído com sucesso, ou <see langword="false" />
        ///     caso contrario
        /// </returns>
        Task<bool> Delete(int id);

        /// <summary>
        ///     Busca o registro pela chave primária
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> Select(int id);

        /// <summary>
        ///     Busca todos os registros salvos
        /// </summary>
        /// <returns>Lista de registros salvos</returns>
        Task<IEnumerable<T>> Select();

        /// <summary>
        ///     Verifica se o registro está salvo no banco de dados
        /// </summary>
        /// <param name="id">Chave primário do registro</param>
        /// <returns>
        ///     <see langword="true" /> caso exista registro no banco de dados com a chave informada e
        ///     <see langword="false" /> caso contrário
        /// </returns>
        Task<bool> Exist(int id);
    }
}
