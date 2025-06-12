using System.Collections.Generic;
using System.Threading.Tasks;
using Desf5.Domain.Entities;

namespace Desf5.Domain.Interfaces;

public interface IProdutoRepository
{
    Task<List<Produto>> GetAll();

    Task<Produto> GetById(int id);

    Task<List<Produto>> GetByName(string nome);
}
