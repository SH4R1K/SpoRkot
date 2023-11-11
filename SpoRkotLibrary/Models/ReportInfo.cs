using System;
using System.Collections.Generic;

namespace SpoRkotLibrary.Models;

public partial class ReportInfo
{
    public int ReportInfoId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Location { get; set; } = null!;

    public string FederalDistrict { get; set; } = null!;

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}
