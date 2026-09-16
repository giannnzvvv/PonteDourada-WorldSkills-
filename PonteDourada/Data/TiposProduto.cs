using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class TiposProduto
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
