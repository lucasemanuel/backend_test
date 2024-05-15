using MongoDB.Bson;

namespace Domain.Entities;

public class BankAccount
{
    public ObjectId _id { get; set; }
    public string? accountNumber { get; set; }
    public decimal? balance { get; set; }
    public virtual Customer Customer { get; set; }
    public bool? isInactived { get; set; }
    public ObjectId Customer_id { get; set; }
    public virtual ICollection<BankTransaction> bankTransactions { get; }
}