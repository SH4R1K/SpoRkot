using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class Stat
{
    public int ReportId { get; set; }

    public short CountTestConnection { get; set; }

    public int POLQA { get; set; }

    public short NegativeMOSSamplesCount { get; set; }

    public short CountSMS { get; set; }

    public short CountLoadFile { get; set; }

    public short CountWebBrowsing { get; set; }

    public virtual Report Report { get; set; } = null!;
}
