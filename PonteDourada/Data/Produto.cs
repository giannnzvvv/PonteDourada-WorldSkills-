using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Produto
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public double? Valor { get; set; }

    public int TipoId { get; set; }

    public int FornecedorId { get; set; }

    public string? Descricao { get; set; }

    public DateOnly Validade { get; set; }

    public DateTime DataHoraCadastro { get; set; }

    public int? Estoque { get; set; }

    public virtual Fornecedor Fornecedor { get; set; } = null!;

    public virtual ICollection<ProdutoSolicitacao> ProdutoSolicitacaos { get; set; } = new List<ProdutoSolicitacao>();

    public virtual TiposProduto Tipo { get; set; } = null!;
}
