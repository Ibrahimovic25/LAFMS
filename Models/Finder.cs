using System;
using System.Collections.Generic;

namespace LAFMS.Models;

public partial class Finder
{
    public string FinderId { get; set; } = null!;

    public string FinderName { get; set; } = null!;

    public string FinderContact { get; set; } = null!;

    public virtual ICollection<FoundRecord> FoundRecords { get; set; } = new List<FoundRecord>();
}
