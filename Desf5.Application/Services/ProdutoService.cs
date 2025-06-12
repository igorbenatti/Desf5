using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desf5.Application.Interfaces;
using Desf5.Domain.Interfaces;
using Desf5.Domain.Entities;

namespace Desf5.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly ILogger<ProdutoService> _logger;
    private readonly IProdutoRepository _repository;

    public ProdutoService(ILogger<ProdutoService> logger, IProdutoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<List<Produto>> GetAll()
    {
        try
        {
            List<Produto> listaProdutos = await _repository.GetAll();

            return listaProdutos != null && listaProdutos.Count > 0 ? listaProdutos : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método GetAll");
            throw;
        }
    }

    public async Task<Produto> GetById(int id)
    {
        try
        {
            Produto produto = await _repository.GetById(id);

            return produto != null ? produto : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método GetById");
            throw;
        }
    }

    public async Task<List<Produto>> GetByName(string nome)
    {
        try
        {
            List<Produto> listaProdutos = await _repository.GetByName(nome);

            return listaProdutos != null ? listaProdutos : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método GetByNome");
            throw;
        }
    }
}
