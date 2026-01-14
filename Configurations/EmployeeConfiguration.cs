using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simulation_16.Models;

namespace Simulation_16.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Experience).IsRequired();
            builder.ToTable(opt =>
            {
                opt.HasCheckConstraint("CK_Experience", "Experience BETWEEN 0 AND 50 ");

            });builder.Property(p => p.ImageUrl).IsRequired().HasMaxLength(512);


            builder.HasOne(p => p.Branch).WithMany(p => p.Employees).HasForeignKey(p => p.BranchId).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
