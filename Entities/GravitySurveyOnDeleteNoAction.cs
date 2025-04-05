using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AGSS.Entities;

public partial class GravitySurveyOnDeleteNoAction : DbContext
{
    public GravitySurveyOnDeleteNoAction()
    {
    }

    public GravitySurveyOnDeleteNoAction(DbContextOptions<GravitySurveyOnDeleteNoAction> options)
        : base(options)
    {
    }

    public virtual DbSet<Analyst> Analysts { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<AreaCoordinate> AreaCoordinates { get; set; }

    public virtual DbSet<Channel1> Channel1s { get; set; }

    public virtual DbSet<Channel2> Channel2s { get; set; }

    public virtual DbSet<Channel3> Channel3s { get; set; }

    public virtual DbSet<ChiefEnginner> ChiefEnginners { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<LeadSpecialist> LeadSpecialists { get; set; }

    public virtual DbSet<Metadata> Metadata { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileCoordinate> ProfileCoordinates { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Spectrometer> Spectrometers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=AGSS;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analyst>(entity =>
        {
            entity.HasKey(e => e.AnalystId).HasName("PK__Analyst__DEC7CE295D87D1B5");

            entity.ToTable("Analyst");

            entity.Property(e => e.AnalystId).HasColumnName("AnalystID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("PK__Area__70B820282852E5B2");

            entity.ToTable("Area");

            entity.Property(e => e.AreaId).HasColumnName("AreaID");
            entity.Property(e => e.Area1).HasColumnName("Area");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

            entity.HasOne(d => d.Project).WithMany(p => p.Areas)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Area__ProjectID__5CD6CB2B");
        });

        modelBuilder.Entity<AreaCoordinate>(entity =>
        {
            entity.HasKey(e => e.AreaCoordinatesId).HasName("PK__AreaCoor__1D1B075675F3A04E");

            entity.Property(e => e.AreaCoordinatesId).HasColumnName("AreaCoordinatesID");
            entity.Property(e => e.AreaId).HasColumnName("AreaID");

            entity.HasOne(d => d.Area).WithMany(p => p.AreaCoordinates)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK__AreaCoord__AreaI__68487DD7");
        });

        modelBuilder.Entity<Channel1>(entity =>
        {
            entity.HasKey(e => e.Channel1Id).HasName("PK__Channel1__13345075653994A2");

            entity.ToTable("Channel1");

            entity.Property(e => e.Channel1Id).HasColumnName("Channel1ID");
            entity.Property(e => e.ProfileCoordinatesId).HasColumnName("ProfileCoordinatesID");

            entity.HasOne(d => d.ProfileCoordinates).WithMany(p => p.Channel1s)
                .HasForeignKey(d => d.ProfileCoordinatesId)
                .HasConstraintName("FK__Channel1__Profil__05D8E0BE");
        });

        modelBuilder.Entity<Channel2>(entity =>
        {
            entity.HasKey(e => e.Channel2Id).HasName("PK__Channel2__066E031990B200E4");

            entity.ToTable("Channel2");

            entity.Property(e => e.Channel2Id).HasColumnName("Channel2ID");
            entity.Property(e => e.ProfileCoordinatesId).HasColumnName("ProfileCoordinatesID");

            entity.HasOne(d => d.ProfileCoordinates).WithMany(p => p.Channel2s)
                .HasForeignKey(d => d.ProfileCoordinatesId)
                .HasConstraintName("FK__Channel2__Profil__08B54D69");
        });

        modelBuilder.Entity<Channel3>(entity =>
        {
            entity.HasKey(e => e.Channel3Id).HasName("PK__Channel3__78B23170CC2A743C");

            entity.ToTable("Channel3");

            entity.Property(e => e.Channel3Id).HasColumnName("Channel3ID");
            entity.Property(e => e.ProfileCoordinatesId).HasColumnName("ProfileCoordinatesID");

            entity.HasOne(d => d.ProfileCoordinates).WithMany(p => p.Channel3s)
                .HasForeignKey(d => d.ProfileCoordinatesId)
                .HasConstraintName("FK__Channel3__Profil__0B91BA14");
        });

        modelBuilder.Entity<ChiefEnginner>(entity =>
        {
            entity.HasKey(e => e.ChiefEnginnerId).HasName("PK__ChiefEng__7A91E8EBA7F0535A");

            entity.ToTable("ChiefEnginner");

            entity.Property(e => e.ChiefEnginnerId).HasColumnName("ChiefEnginnerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B887F9E9DC");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContactPerson).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(15);
            entity.Property(e => e.OrganizationName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flight__8A9E148E1A33D57A");

            entity.ToTable("Flight");

            entity.Property(e => e.FlightId).HasColumnName("FlightID");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Operator).WithMany(p => p.Flights)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("FK__Flight__Operator__59FA5E80");

            entity.HasOne(d => d.Project).WithMany(p => p.Flights)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK__Flight__ProjectI__59063A47");
        });

        modelBuilder.Entity<LeadSpecialist>(entity =>
        {
            entity.HasKey(e => e.LeadSpecialistId).HasName("PK__LeadSpec__73B79B73DDCC81BC");

            entity.ToTable("LeadSpecialist");

            entity.Property(e => e.LeadSpecialistId).HasColumnName("LeadSpecialistID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Metadata>(entity =>
        {
            entity.HasKey(e => e.MetadataId).HasName("PK__Metadata__66106FF91ACF3FF2");

            entity.Property(e => e.MetadataId).HasColumnName("MetadataID");
            entity.Property(e => e.SpectrometerId).HasColumnName("SpectrometerID");

            entity.HasOne(d => d.Spectrometer).WithMany(p => p.Metadata)
                .HasForeignKey(d => d.SpectrometerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Metadata__Spectr__656C112C");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.HasKey(e => e.OperatorId).HasName("PK__Operator__7BB12F8E684FFD0E");

            entity.ToTable("Operator");

            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Login).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.ProfileId).HasName("PK__Profile__290C8884A050E6F1");

            entity.ToTable("Profile");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");
            entity.Property(e => e.AreaId).HasColumnName("AreaID");

            entity.HasOne(d => d.Area).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("FK__Profile__AreaID__5FB337D6");
        });

        modelBuilder.Entity<ProfileCoordinate>(entity =>
        {
            entity.HasKey(e => e.ProfileCoordinatesId).HasName("PK__ProfileC__C2F4776F354514D5");

            entity.Property(e => e.ProfileCoordinatesId).HasColumnName("ProfileCoordinatesID");
            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.Profile).WithMany(p => p.ProfileCoordinates)
                .HasForeignKey(d => d.ProfileId)
                .HasConstraintName("FK__ProfileCo__Profi__02FC7413");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABED0B326ABA8");

            entity.ToTable("Project");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.AnalystId).HasColumnName("AnalystID");
            entity.Property(e => e.ChiefEnginnerId).HasColumnName("ChiefEnginnerID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.LeadSpecialistId).HasColumnName("LeadSpecialistID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);

            entity.HasOne(d => d.Analyst).WithMany(p => p.Projects)
                .HasForeignKey(d => d.AnalystId)
                .HasConstraintName("FK__Project__Analyst__5441852A");

            entity.HasOne(d => d.ChiefEnginner).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ChiefEnginnerId)
                .HasConstraintName("FK__Project__ChiefEn__52593CB8");

            entity.HasOne(d => d.Customer).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Project__Custome__5165187F");

            entity.HasOne(d => d.LeadSpecialist).WithMany(p => p.Projects)
                .HasForeignKey(d => d.LeadSpecialistId)
                .HasConstraintName("FK__Project__LeadSpe__534D60F1");
        });

        modelBuilder.Entity<Spectrometer>(entity =>
        {
            entity.HasKey(e => e.SpectrometerId).HasName("PK__Spectrom__7993EA23D4AEE010");

            entity.ToTable("Spectrometer");

            entity.Property(e => e.SpectrometerId).HasColumnName("SpectrometerID");
            entity.Property(e => e.FlightId).HasColumnName("FlightID");

            entity.HasOne(d => d.Flight).WithMany(p => p.Spectrometers)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__Spectrome__Fligh__628FA481");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
