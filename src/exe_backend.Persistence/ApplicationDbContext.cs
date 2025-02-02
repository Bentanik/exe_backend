namespace exe_backend.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<User> Users { get; set; }
}