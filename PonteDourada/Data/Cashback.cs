using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Cashback
{
    public int Id { get; set; }

    public int? SolicitacaoId { get; set; }

    public double? Valor { get; set; }

    public virtual Solicitacao? Solicitacao { get; set; }
}
