using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Cliente
{
    public int Id { get; set; }

    public DateOnly DataNascimento { get; set; }

    public string Cpf { get; set; } = null!;

    public int? ResponsavelId { get; set; }

    public virtual Pessoa IdNavigation { get; set; } = null!;

    public virtual ICollection<Cliente> InverseResponsavel { get; set; } = new List<Cliente>();

    public virtual Cliente? Responsavel { get; set; }

    public virtual ICollection<Solicitacao> Solicitacaos { get; set; } = new List<Solicitacao>();
}
