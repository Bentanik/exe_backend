using System.Text.Json.Serialization;
using exe_backend.Domain.ValueObjects;

namespace exe_backend.Domain.Models;

public class Transaction : DomainEntity<Guid>
{
    public bool PaymentStatus { get; private set; }

    [JsonIgnore]
    public Guid UserId { get; private set; }
    public User? User { get; set; }
}