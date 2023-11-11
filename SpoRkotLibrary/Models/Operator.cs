using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class Operator
{
    public int OperatorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
