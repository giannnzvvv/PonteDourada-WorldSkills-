using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Solicitacao
{
    public int Id { get; set; }

    public DateOnly Validade { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public int ClienteId { get; set; }

    public string Descricao { get; set; } = null!;

    public double? Cashback { get; set; }

    public virtual ICollection<Cashback> Cashbacks { get; set; } = new List<Cashback>();

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<ProdutoSolicitacao> ProdutoSolicitacaos { get; set; } = new List<ProdutoSolicitacao>();
}
