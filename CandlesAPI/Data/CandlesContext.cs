using CandlesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CandlesAPI.Data
{
    public class CandlesContext : DbContext
    {
        public CandlesContext() { }
        public CandlesContext(DbContextOptions<CandlesContext> options) : base(options)
        {     
        }

        public virtual DbSet<Candle> Candles { get; set; }
    }
}
