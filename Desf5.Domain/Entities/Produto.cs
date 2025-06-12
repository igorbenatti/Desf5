using System;
using System.ComponentModel.DataAnnotations;

namespace Desf5.Domain.Entities;

public class Produto
{
    [Key]
    public int Id { get; set; }

    public string Nome { get; set; }

    public string Descricao { get; set; }

    public decimal Preco { get; set; }

    public int QuantidadeEmEstoque { get; set; }
}
