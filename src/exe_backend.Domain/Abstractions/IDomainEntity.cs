namespace exe_backend.Domain.Abstractions;

public interface IDomainEntity<T> : IDomainEntity
{
    public T Id { get; set; }
}

public interface IDomainEntity
{
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}