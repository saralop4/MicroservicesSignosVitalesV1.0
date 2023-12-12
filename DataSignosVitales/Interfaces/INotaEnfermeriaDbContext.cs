
using DataSignosVitales.DTOs;
using DataSignosVitales.Entities.NotaEnfermeriaModels;

using Microsoft.EntityFrameworkCore;

namespace DataSignosVitales.Interfaces
{
    public interface INotaEnfermeriaDbContext
    {

        public DbSet<NotasEnfermerium> NotasEnfermeria { get; set; }

        public DbSet<SisMae> SisMaes { get; set; }

        public DbSet<SisMedi> SisMedis { get; set; }

        public  DbSet<SisCama> SisCamas { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
