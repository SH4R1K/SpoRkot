using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class SmsQuality
{
    public int ReportId { get; set; }

    public decimal ShareUndelivered { get; set; }

    public decimal AverageDeliveryTime { get; set; }

    public virtual Report Report { get; set; } = null!;
}
