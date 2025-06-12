using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Desf5.Domain.Interfaces;
using Desf5.Infra.Data.Context;
using Desf5.Domain.Entities;
using System.Linq;
using Desf5.Domain.Enums;

namespace Desf5.Infra.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly ILogger<ProdutoRepository> _logger;
    private readonly Desf5DbContext _desf5DbContext;

    public ProdutoRepository(ILogger<ProdutoRepository> logger, Desf5DbContext desf5DbContext)
    {
        _logger = logger;
        _desf5DbContext = desf5DbContext;
    }
    public async Task<Produto> GerenciarProduto(Acao acao, Produto produto)
    {
        try
        {
            if (acao == Acao.Adicionar)
                _desf5DbContext.Produto.Add(produto);
            else if (acao == Acao.Alterar)
            {
                _desf5DbContext.ChangeTracker.Clear();
                _desf5DbContext.Produto.Update(produto);
            }
            else
                _desf5DbContext.Produto.Remove(produto);

            await _desf5DbContext.SaveChangesAsync();

            return produto;
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "Erro no método GerenciarProduto");
            throw;
        }
    }

    public async Task<List<Produto>> GetAll()
    {
        try
        {
            return await _desf5DbContext.Produto.ToListAsync();
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
            return await _desf5DbContext.Produto.FirstOrDefaultAsync(x => x.Id.Equals(id));
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
            return await _desf5DbContext.Produto.Where(x => x.Nome.ToLower().Contains(nome.ToLower())).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no método GetByName");
            throw;
        }
    }
}
