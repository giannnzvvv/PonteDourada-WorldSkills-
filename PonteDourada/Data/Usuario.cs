using System;
using System.Collections.Generic;

namespace PonteDourada.Data;

public partial class Usuario
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string SenhaHash { get; set; } = null!;

    public virtual Pessoa IdNavigation { get; set; } = null!;
}
