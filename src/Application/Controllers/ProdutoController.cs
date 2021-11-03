using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos;
using Domain.Core;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers
{
    /// <summary>
    ///     Realiza o CRUD para para a entidade de produto
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : BaseControllerApi
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoRepository _repository;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        ///     Retorna todos produtos
        /// </summary>
        /// <returns>Lista de todos os produtos</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDto>), 200)] //OK
        [ProducesResponseType(typeof(void), 400)] //Bad Request
        [ProducesResponseType(typeof(void), 401)] //Unauthorized
        [ProducesResponseType(typeof(ErrorInternal), 500)] //Internal Server Error
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repository.Select());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        ///     Retorna o produto, através do código de chave primária informado
        /// </summary>
        /// <para name="key">Código do produto</para>
        /// <returns>Registro do código informado</returns>
        [HttpGet("{key:int:min(0)}")]
        [ProducesResponseType(typeof(ProdutoDto), 200)] //OK
        [ProducesResponseType(typeof(void), 400)] //Bad Request
        [ProducesResponseType(typeof(void), 401)] //Unauthorized
        [ProducesResponseType(typeof(void), 404)] //Not Found
        [ProducesResponseType(typeof(ErrorInternal), 500)] //Internal Server Error
        public async Task<IActionResult> GetById(int key)
        {
            try
            {
                var result = await _repository.Select(key);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        ///     Insere um novo produto
        /// </summary>
        /// <para name="dto">Produto para ser inserido</para>
        /// <returns>Produto com o campo de chave primária criado pelo banco</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProdutoDto), 201)] //Created
        [ProducesResponseType(typeof(void), 400)] //Bad Request
        [ProducesResponseType(typeof(void), 401)] //Unauthorized
        [ProducesResponseType(typeof(ValidationApiResult), 422)] //Unprocessable Entity
        [ProducesResponseType(typeof(ErrorInternal), 500)] //Internal Server Error
        public async Task<IActionResult> Post([FromBody] ProdutoDto dto)
        {
            try
            {
                if (dto == null || dto.Id != 0) return BadRequest();

                var entity = dto.ToEntity();
                var resultValidation = await entity.ValidateToInsertUpdate();
                if (resultValidation != null && resultValidation.HasError) return UnprocessableEntity(resultValidation);
                entity = await _repository.Insert(entity);
                dto.Id = entity.Id;
                return Created(dto, dto.Id);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        ///     Atualiza um produto existente
        /// </summary>
        /// <para name="dto">Produto para ser atualizado</para>
        [HttpPut("{key:int:min(1)}")]
        [ProducesResponseType(typeof(ProdutoDto), 200)] //OK
        [ProducesResponseType(typeof(void), 400)] //Bad Request
        [ProducesResponseType(typeof(void), 401)] //Unauthorized
        [ProducesResponseType(typeof(void), 404)] //Not Found
        [ProducesResponseType(typeof(ValidationApiResult), 422)] //Unprocessable Entity
        [ProducesResponseType(typeof(ErrorInternal), 500)] //Internal Server Error
        public async Task<IActionResult> Put(int key, [FromBody] ProdutoDto dto)
        {
            try
            {
                if (dto == null || dto.Id == 0 || dto.Id != key) return BadRequest();

                var entity = dto.ToEntity();
                var resultValidation = await entity.ValidateToInsertUpdate();
                if (resultValidation != null && resultValidation.HasError) return UnprocessableEntity(resultValidation);
                await _repository.Update(entity);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        ///     Apaga um produto existente
        /// </summary>
        /// <para name="key">Chave primária do produto a ser apagado</para>
        [HttpDelete("{key:int:min(1)}")]
        [ProducesResponseType(typeof(void), 200)] //OK
        [ProducesResponseType(typeof(void), 401)] //Unauthorized
        [ProducesResponseType(typeof(void), 404)] //Not Found
        [ProducesResponseType(typeof(ValidationApiResult), 422)] //Unprocessable Entity
        [ProducesResponseType(typeof(ErrorInternal), 500)] //Internal Server Error
        public async Task<IActionResult> Delete(int key)
        {
            try
            {
                if (key == 0) return BadRequest();
                var entity = await _repository.Select(key);
                if (entity == null) return NotFound();

                var resultValidation = await entity.ValidateToDelete();
                if (resultValidation != null && resultValidation.HasError) return UnprocessableEntity(resultValidation);
                await _repository.Delete(key);
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
