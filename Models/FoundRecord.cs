using System;
using System.Collections.Generic;

namespace LAFMS.Models;

public partial class FoundRecord
{
    public int RecordId { get; set; }

    public int ItemId { get; set; }

    public string FinderId { get; set; } = null!;

    public string? ClaimantId { get; set; }

    public string FoundDate { get; set; } = null!;

    public string? ClaimDate { get; set; }

    public virtual Claimant? Claimant { get; set; }

    public virtual Finder Finder { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
