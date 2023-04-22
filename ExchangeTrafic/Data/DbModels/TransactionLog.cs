using System;
using System.Collections.Generic;

namespace ExchangeTrafic.Data.DbModels;

public partial class TransactionLog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? RequestUrl { get; set; }

    public string? ResponseeLog { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? TransactionType { get; set; }
}
