using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Pessoa
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Telefone { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Fornecedor? Fornecedor { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
