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
    [HttpGet("{id: int}")]
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
    [HttpGet("{nome}")]
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
}
