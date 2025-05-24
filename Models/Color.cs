using System;
using System.Collections.Generic;

namespace JDshop.Models;

public partial class Color
{
    public int Id { get; set; }

    public string? Color1 { get; set; }

    public virtual ICollection<ProductSizeColor> ProductSizeColors { get; set; } = new List<ProductSizeColor>();
}
