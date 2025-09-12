using Microsoft.EntityFrameworkCore;

namespace SingkoFItnessWebApi.Models;

public partial class SingkoFitnessWebDbContext : DbContext
{
    public SingkoFitnessWebDbContext()
    {
    }

    public SingkoFitnessWebDbContext(DbContextOptions<SingkoFitnessWebDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aiquery> Aiqueries { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<NutritionLog> NutritionLogs { get; set; }

    public virtual DbSet<ProgressLog> ProgressLogs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Workout> Workouts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=Acidos\\SQLEXPRESS;Database=SingkoFitnessWebDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aiquery>(entity =>
        {
            entity.HasKey(e => e.QueryId).HasName("PK__AIQuerie__5967F7DB946013BA");

            entity.ToTable("AIQueries");

            entity.Property(e => e.Airesponse).HasColumnName("AIResponse");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.Aiqueries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__AIQueries__UserI__4CA06362");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__A074AD2F50CACF62");

            entity.Property(e => e.DistanceKm).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.ExerciseName).HasMaxLength(150);
            entity.Property(e => e.WeightUsed).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.Workout).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.WorkoutId)
                .HasConstraintName("FK__Exercises__Worko__4316F928");
        });

        modelBuilder.Entity<NutritionLog>(entity =>
        {
            entity.HasKey(e => e.NutritionId).HasName("PK__Nutritio__8A74A056B2D99A65");

            entity.Property(e => e.Carbs).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Fat).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.MealType).HasMaxLength(50);
            entity.Property(e => e.Protein).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.NutritionLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Nutrition__UserI__45F365D3");
        });

        modelBuilder.Entity<ProgressLog>(entity =>
        {
            entity.HasKey(e => e.ProgressId).HasName("PK__Progress__BAE29CA52D547A02");

            entity.Property(e => e.BodyFatPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.MuscleMass).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.ProgressLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ProgressL__UserI__48CFD27E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A623B5CBA");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B6160A6AD6675").IsUnique();

            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C5CF5C4D6");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053410675321").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasDefaultValue(1);
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3D5E1FD2");
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.HasKey(e => e.WorkoutId).HasName("PK__Workouts__E1C42A01BD0AAD65");

            entity.Property(e => e.CaloriesBurned).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.WorkoutName).HasMaxLength(150);

            entity.HasOne(d => d.User).WithMany(p => p.Workouts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Workouts__UserId__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}