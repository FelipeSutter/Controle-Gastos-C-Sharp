using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.API.Data.Mappings;
using ControleGastos.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.API.Data
{

    // Cria um construtor de ApplicationContext contendo o DbContextOptions dentro dele.
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options) {
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<NaturezaDeLancamento> Naturezas { get; set; }

        // Isso cria o modelo definido dentro do UsuarioMap
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new NaturezaDeLancamentoMap());
        }

    }
}