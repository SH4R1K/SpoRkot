using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class VoiceQuality
{
    public int ReportId { get; set; }

    public decimal NonAccessibilityRatio { get; set; }

    public decimal CutoffRatio { get; set; }

    public decimal MOSPOLQA { get; set; }

    public decimal NegativeMOSSamplesRatio { get; set; }

    public virtual Report Report { get; set; } = null!;
}
