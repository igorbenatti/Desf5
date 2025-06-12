using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Desf5.Application.Dtos;
using Desf5.Application.Interfaces;
using System.Collections.Generic;
using Desf5.Domain.Entities;

namespace Desf5.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProdutoController : ControllerBase
{
    private readonly ILogger<ProdutoController> _logger;
    private readonly IProdutoService _service;

    public ProdutoController(ILogger<ProdutoController> logger, IProdutoService service)
    {
        _logger = logger;
        _service = service;
    }

    /// <summary>Adicionar produto</summary>
    /// <remarks>
    /// Exemplo:
    ///
    ///     POST
    ///     {
    ///        "nome": "string",
    ///        "descricao": "string",
    ///        "preco": 0,
    ///        "quantidadeEmEstoque": 0
    ///     }
    ///
    /// </remarks>
    /// <param name="request"></param>
    /// <response code="201">Created</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpPost()]
    public async Task<ActionResult> Adicionar([FromBody] CadastrarRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: Adicionar -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            return StatusCode((int)HttpStatusCode.Created, await _service.Adicionar(request));
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: Adicionar -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }

    /// <summary>Alterar produto</summary>
    /// <remarks>
    /// Exemplo:
    ///
    ///     PUT
    ///     {
    ///        "nome": "string",
    ///        "descricao": "string",
    ///        "preco": 0,
    ///        "quantidadeEmEstoque": 0
    ///     }
    ///
    /// </remarks>
    /// <param name="id">Identificador do produto</param>
    /// <param name="request"></param>
    /// <response code="200">OK</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpPut("{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] AlterarRequest request)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: Alterar -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            return StatusCode((int)HttpStatusCode.OK, await _service.Alterar(id, request));
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: Alterar -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }

    /// <summary>Remover produto</summary>
    /// <param name="id">Identificador do produto</param>
    /// <response code="200">OK</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpDelete()]
    public async Task<ActionResult> Remover(int id)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: Remover -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            return StatusCode((int)HttpStatusCode.OK, await _service.Remover(id));
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: Remover -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }

    /// <summary>Consultar todos produtos</summary>
    /// <response code="200">OK</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(List<Produto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpGet()]
    public async Task<ActionResult> GetAll()
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: GetAll -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _service.GetAll();

            return response != null && response.Count > 0
                ? StatusCode((int)HttpStatusCode.OK, response)
                : StatusCode((int)HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: GetAll -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }

    /// <summary>Consultar produto por ID</summary>
    /// <param name="id">Identificador do produto</param>
    /// <response code="200">OK</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: GetById -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _service.GetById(id);

            return response != null
                ? StatusCode((int)HttpStatusCode.OK, response)
                : StatusCode((int)HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: GetById -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }

    /// <summary>Consultar produto por nome</summary>
    /// <param name="nome">Nome do produto</param>
    /// <response code="200">OK</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(List<Produto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpGet("nome/{nome}")]
    public async Task<ActionResult> GetByName(string nome)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: GetByName -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _service.GetByName(nome);

            return response != null && response.Count > 0
                ? StatusCode((int)HttpStatusCode.OK, response)
                : StatusCode((int)HttpStatusCode.NoContent);
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: GetByName -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }

    /// <summary>Consultar quantidade de produtos cadastrados</summary>
    /// <response code="200">OK</response>
    /// <response code="400">BadRequest</response>
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [HttpGet("contador")]
    public async Task<ActionResult> Count()
    {
        if (!ModelState.IsValid)
        {
            _logger.LogError("ProdutoController :: Count -> ErrorMessage: Modelo inválido");
            return BadRequest(ModelState);
        }

        try
        {
            var response = await _service.GetAll();

            return StatusCode((int)HttpStatusCode.OK, response != null ? response.Count : 0);
        }
        catch (Exception ex)
        {
            _logger.LogError("ProdutoController :: Count -> ExMessage: {mensagem}", ex.Message);
            return StatusCode((int)HttpStatusCode.BadRequest, new Response { status = (int)HttpStatusCode.BadRequest, isvalid = false, message = ex.Message });
        }
    }
}
