using Microsoft.EntityFrameworkCore;



public partial class HisDbContext: DbContext
{
    public HisDbContext(DbContextOptions<HisDbContext> options) : base(options)
    {
    }
    public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
    public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
    public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }

}
