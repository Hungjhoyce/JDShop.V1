using System;
using System.Collections.Generic;

namespace JDshop.Models;

public partial class AccountType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
