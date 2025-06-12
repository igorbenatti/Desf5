using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desf5.Application.Dtos;

public class AlterarRequest
{
    [Display(Name = "Nome do produto")]
    [StringLength(100, ErrorMessage = "Nome do produto deve conter no máximo 100 caracteres")]
    public string nome { get; set; }

    [Display(Name = "Descrição do produto")]
    [StringLength(200, ErrorMessage = "Descrição do produto deve conter no máximo 200 caracteres")]
    public string descricao { get; set; }

    [Display(Name = "Preço do produto")]
    [Column(TypeName = "decimal(10,2)")]
    [DisplayFormat(DataFormatString = "{0:c2}")]
    [DataType(DataType.Currency)]
    public decimal? preco { get; set; }

    [Display(Name = "Quantidade em estoque do produto")]
    public int? quantidadeEmEstoque { get; set; }
}
