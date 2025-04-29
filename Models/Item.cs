using System;
using System.Collections.Generic;

namespace LAFMS.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemDescription { get; set; } = null!;

    public virtual ICollection<FoundRecord> FoundRecords { get; set; } = new List<FoundRecord>();
}
