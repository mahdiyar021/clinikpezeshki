using clinikpezeshki.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace clinikpezeshki.Contexts
{
    public class MainContext:DbContext,IUnitOfWork
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(AppSetting.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }


        public int SaveChange()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : DomainEntity
        {
            return base.Set<TEntity>();
        }

        public DbSet<Patient>? Patients { get; set; }
        public DbSet<Medicine>? Medicines { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Disbursement>? Disbursements { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<HowPay>? HowPays { get; set; }
        public DbSet<Prescription>? Prescriptions { get; set; }
        public DbSet<MedicinePrescription>? MedicinePrescriptions { get; set; }
        public DbSet<Treatment>? Treatments { get; set; }
        public DbSet<Expertise>? Expertise { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<OnlineReservation>? OnlineReservations { get; set; }


    }
}
