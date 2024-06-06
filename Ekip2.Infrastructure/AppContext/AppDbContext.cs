

namespace Ekip2.Infrastructure.AppContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public const string DevConnectionString = "AppConnectionDev";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<LeaveType> LeaveTypes { get; set; }
        public virtual DbSet<Advance> Advances { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageLog> PackageLogs { get; set; }
        public  virtual DbSet<Expense> Expenses { get; set; }
        public  virtual DbSet<Salary> Salaries { get; set; }
        public  virtual DbSet<SalaryLog> SalaryLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            SetBaseProperties();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetBaseProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Veritabanı değişiklik izleyicisinden BaseEntity türevli tüm varlıkların durumunu kontrol eder ve ilgili işlemleri gerçekleştirir.
        /// </summary>
        private void SetBaseProperties()
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            var userId = /*_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ??*/ "UserBulunamadı";


            foreach (var entry in entries)
            {
                SetIfAdded(entry, userId);
                SetIfModified(entry, userId);
                SetIfDeleted(entry, userId);
            }
        }
        /// <summary>
        /// Veritabanı girişinin durumu silinmişse ilgili işlemleri gerçekleştirir.
        /// </summary>
        /// <param name="entry">
        /// Güncellenecek girişin temsil ettiği <see cref="EntityEntry{TEntity}"/> nesnesi.
        /// </param>
        /// <param name="userId">
        /// İşlem yapan kullanıcının benzersiz kimlik bilgisi.
        /// </param>
        private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State != EntityState.Deleted)
            {
                return;
            }
            if (entry.Entity is not AuditableEntity entity)
            {
                return;
            }
            entry.State = EntityState.Modified;
            entry.Entity.Status = Domain.Enums.Status.Deleted;
            entity.DeletedDate = DateTime.Now;
            entity.DeletedBy = userId;
        }
        /// <summary>
        /// Veritabanı girişinin durumu değiştirilmişse ilgili işlemleri gerçekleştirir.
        /// </summary>
        /// <param name="entry">
        /// Güncellenecek girişin temsil ettiği <see cref="EntityEntry{TEntity}"/> nesnesi.
        /// </param>
        /// <param name="userId">
        /// İşlem yapan kullanıcının benzersiz kimlik bilgisi.
        /// </param>
        private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Modified)
            {
                entry.Entity.Status = Domain.Enums.Status.Updated;
                entry.Entity.UpdatedBy = userId;
                entry.Entity.UpdatedDate = DateTime.Now;
            }
        }
        /// <summary>
        /// Veritabanına yeni bir varlık eklenirken ilgili işlemleri gerçekleştirir.
        /// </summary>
        /// <param name="entry">
        /// Eklenen girişin temsil ettiği <see cref="EntityEntry{TEntity}"/> nesnesi.
        /// </param>
        /// <param name="userId">
        /// İşlem yapan kullanıcının benzersiz kimlik bilgisi.
        /// </param>
        private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Status = Domain.Enums.Status.Created;
                entry.Entity.CreatedBy = userId;
                entry.Entity.CreatedDate = DateTime.Now;
            }
        }
    }
}
