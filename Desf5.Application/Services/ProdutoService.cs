using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desf5.Application.Interfaces;
using Desf5.Domain.Interfaces;
using Desf5.Domain.Entities;
using Desf5.Application.Dtos;
using Desf5.Domain.Enums;

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

    public async Task<Produto> Adicionar(CadastrarRequest request)
    {
        try
        {
            Produto novoProduto = new()
            {
                Nome = request.nome,
                Descricao = request.descricao,
                Preco = request.preco,
                QuantidadeEmEstoque = request.quantidadeEmEstoque
            };
            return await _repository.GerenciarProduto(Acao.Adicionar, novoProduto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método Adicionar");
            throw;
        }
    }

    public async Task<Produto> Alterar(int id, AlterarRequest request)
    {
        try
        {
            Produto produto = await _repository.GetById(id);

            if (produto == null)
                throw new FormatException($"Produto [{id}] não encontrado");

            produto.Nome = !string.IsNullOrEmpty(request.nome) ? request.nome : produto.Nome;
            produto.Descricao = !string.IsNullOrEmpty(request.descricao) ? request.descricao : produto.Descricao;
            
            if (request.preco != null)
                produto.Preco = request.preco ?? 0;

            if (request.quantidadeEmEstoque != null)
                produto.QuantidadeEmEstoque = request.quantidadeEmEstoque ?? 0;

            return await _repository.GerenciarProduto(Acao.Alterar, produto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método Alterar");
            throw;
        }
    }

    public async Task<bool> Remover(int id)
    {
        try
        {
            Produto produto = await _repository.GetById(id);

            if (produto == null)
                throw new FormatException($"Produto [{id}] não encontrado");

            await _repository.GerenciarProduto(Acao.Remover, produto);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método Remover");
            throw;
        }
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
