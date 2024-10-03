using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Encyclopedia_Of_Laws.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

#nullable disable

namespace Encyclopedia_Of_Laws.Data
{
    public partial class EncyclopediaOfLawsContext : IdentityDbContext<ApplicationUser>
    {
        public EncyclopediaOfLawsContext()
        {
        }

        public EncyclopediaOfLawsContext(DbContextOptions<EncyclopediaOfLawsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<ClientLetter> ClientLetters { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<UserIssue> UserIssues { get; set; }
        public virtual DbSet<LawyerInfo> LawyerInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");


            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.AssignedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("AssignedDate");

                entity.Property(e => e.LawyerId).HasColumnName("Lawyer_ID");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.Subject).HasColumnName("Subject");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("RequestDate");

                entity.Property(e => e.RequestStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Request_Status")
                    .HasDefaultValueSql("(N'Pending')");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Lawyer)
                    .WithMany(p => p.RequestLawyers)
                    .HasForeignKey(d => d.LawyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests_Lawyers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RequestUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requests_users");
            });

            modelBuilder.Entity<ClientLetter>(entity =>
            {
                entity.Property(e => e.LettertId).HasColumnName("LetterID");

                entity.Property(e => e.AssignedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("AssignedDate");

                entity.Property(e => e.LawyerId).HasColumnName("Lawyer_ID");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.Subject).HasColumnName("Subject");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Lawyer)
                    .WithMany(p => p.ClientLetterLawyers)
                    .HasForeignKey(d => d.LawyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientLetter_Lawyers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClientLetterUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientLetter_users");
            });

            modelBuilder.Entity<UserIssue>(entity =>
            {
                entity.HasKey(e => e.IssueId)
                    .HasName("PK__UserIssu__749E804CF181F235");

                entity.Property(e => e.IssueId).HasColumnName("issueID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("issue_Date");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("(N'pending')");

                entity.Property(e => e.Subject).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
