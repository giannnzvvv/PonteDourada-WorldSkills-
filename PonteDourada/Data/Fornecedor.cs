using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Fornecedor
{
    public int Id { get; set; }

    public string RazaoSocial { get; set; } = null!;

    public string Cnpj { get; set; } = null!;

    public virtual Pessoa IdNavigation { get; set; } = null!;

    public virtual ICollection<Produto> Produtos { get; set; } = new List<Produto>();
}
