using Microsoft.EntityFrameworkCore;
using StudentManagementMVC.Models;
namespace StudentManagementMVC.Data;
public class ApplicationDbContext : DbContext {
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
 public DbSet<Student> Students => Set<Student>();
 protected override void OnModelCreating(ModelBuilder modelBuilder){
  modelBuilder.Entity<Student>(e=>{ e.ToTable("Students"); e.HasKey(s=>s.Id); e.Property(s=>s.StudentCode).IsRequired().HasMaxLength(20); e.Property(s=>s.FullName).IsRequired().HasMaxLength(100); e.Property(s=>s.Email).IsRequired().HasMaxLength(150); e.Property(s=>s.Phone).HasMaxLength(20); e.Property(s=>s.Major).IsRequired().HasMaxLength(100); e.Property(s=>s.Gender).IsRequired().HasMaxLength(10); e.Property(s=>s.GPA).HasPrecision(4,2); e.HasIndex(s=>s.StudentCode).IsUnique(); });
 }
}