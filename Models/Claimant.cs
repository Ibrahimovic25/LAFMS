using System;
using System.Collections.Generic;

namespace LAFMS.Models;

public partial class Claimant
{
    public string ClaimantId { get; set; } = null!;

    public string? ClaimantName { get; set; }

    public virtual ICollection<FoundRecord> FoundRecords { get; set; } = new List<FoundRecord>();
}
