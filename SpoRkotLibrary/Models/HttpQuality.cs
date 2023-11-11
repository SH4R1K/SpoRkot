using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class HttpQuality
{
    public int ReportId { get; set; }

    public decimal SessionFailureRatio { get; set; }

    public decimal ULMeanUserDataRate { get; set; }

    public decimal DLMeanUserDataRate { get; set; }

    public decimal SessionTime { get; set; }

    public virtual Report Report { get; set; } = null!;
}
