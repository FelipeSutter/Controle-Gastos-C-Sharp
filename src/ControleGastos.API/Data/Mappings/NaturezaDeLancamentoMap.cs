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
    public class NaturezaDeLancamentoMap : IEntityTypeConfiguration<NaturezaDeLancamento>
    {
        public void Configure(EntityTypeBuilder<NaturezaDeLancamento> builder)
        {
            // Cria a tabela NaturezaDeLancamento e diz que a chave dele é o Id
            builder.ToTable("natureza_de_lancamento")
            .HasKey(p_=> p_.Id);

            // Muitas naturezas para um usuário
            builder.HasOne(p => p.Usuario) // Um usuario
            .WithMany() // Possui muitas
            .HasForeignKey(fk => fk.IdUsuario); // Naturezas

            // Cria a tabela de email como varchar
            builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            builder.Property(p => p.Observacao)
            .HasColumnType("VARCHAR");

            // Cria a tabela de data como timestamp
            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");

        }
    }
}