using System.ComponentModel;

namespace Desf5.Domain.Enums;

public enum Acao
{
    [Description("Adicionar")]
    Adicionar = 1,

    [Description("Alterar")]
    Alterar = 2,

    [Description("Remover")]
    Remover = 3
}
