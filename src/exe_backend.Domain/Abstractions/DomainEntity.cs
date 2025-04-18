namespace exe_backend.Domain.Abstractions;

public class DomainEntity<TKey> : IDomainEntity<TKey>
{
    public TKey Id { get; set; } = default!;
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool? IsDeleted { get; set; }
    /// <summary>
    /// True if domain entity has an identity
    /// </summary>
    /// <returns></returns>
    public bool IsTransient()
    {
        return Id.Equals(default(TKey));
    }
}
