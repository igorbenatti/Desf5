using System.Collections.Generic;
using System.Threading.Tasks;
using Desf5.Application.Dtos;
using Desf5.Domain.Entities;

namespace Desf5.Application.Interfaces;

public interface IProdutoService
{
    Task<Produto> Adicionar(CadastrarRequest request);

    Task<Produto> Alterar(int id, AlterarRequest request);

    Task<bool> Remover(int id);

    Task<List<Produto>> GetAll();

    Task<Produto> GetById(int id);

    Task<List<Produto>> GetByName(string nome);
}
