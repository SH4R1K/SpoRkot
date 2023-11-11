using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class Report
{
    public int ReportId { get; set; }

    public int OperatorId { get; set; }

    public int ReportInfoId { get; set; }

    public virtual HttpQuality? HttpQuality { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual ReportInfo ReportInfo { get; set; } = null!;

    public virtual SmsQuality? SmsQuality { get; set; }

    public virtual Stat? Stat { get; set; }

    public virtual VoiceQuality? VoiceQuality { get; set; }
}
