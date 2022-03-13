using Elevador.Models;
using Microsoft.EntityFrameworkCore;

namespace Elevador.Context
{
    public class ElevatorContext : DbContext
    {

        public DbSet<ElevatorWork> ElevatorWork { get; set; } = null!;
        public DbSet<ElevatorFloor> ElevatorFloor { get; set; } = null!;

        public ElevatorContext(DbContextOptions<ElevatorContext> options)
        : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
