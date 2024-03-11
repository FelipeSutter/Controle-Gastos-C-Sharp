using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.API.Data.Mappings
{
    // Essa classe serve para criar o mapeamento da entidade no banco de dados
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            // Cria a tabela usuario e diz que a chave dele é o Id
            builder.ToTable("usuario")
            .HasKey(p_=> p_.Id);

            // Cria a tabela de email como varchar
            builder.Property(p => p.Email)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.Senha)
            .HasColumnType("VARCHAR")
            .IsRequired();

            // Cria a tabela de data como timestamp
            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");

        }
    }
}