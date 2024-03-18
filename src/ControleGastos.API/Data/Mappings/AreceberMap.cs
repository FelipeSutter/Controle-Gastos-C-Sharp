using ControleGastos.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.API.Data.Mappings
{
    // Essa classe serve para criar o mapeamento da entidade no banco de dados
    public class AreceberMap : IEntityTypeConfiguration<Areceber>
    {
        public void Configure(EntityTypeBuilder<Areceber> builder)
        {
            // Cria a tabela Areceber e diz que a chave dele é o Id
            builder.ToTable("areceber")
            .HasKey(p_=> p_.Id);

            // Muitas naturezas para um usuário
            builder.HasOne(p => p.Usuario) // Um usuario
            .WithMany() // Possui muitas
            .HasForeignKey(fk => fk.IdUsuario); // Naturezas

            // Muitas titulos a pagar para um usuário
            builder.HasOne(p => p.Natureza) // Um usuario
            .WithMany() // Possui muitas
            .HasForeignKey(fk => fk.IdNatureza); // Naturezas

            // Cria a tabela de desricao como varchar
            builder.Property(p => p.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired();

            // Campo valor inicial/original
            builder.Property(p => p.ValorOriginal)
            .HasColumnType("double precision")
            .IsRequired();

            // Campo valor pago
            builder.Property(p => p.ValorRecebido)
            .HasColumnType("double precision")
            .IsRequired();

            builder.Property(p => p.Observacao)
            .HasColumnType("VARCHAR");

            // Cria a tabela de data como timestamp
            builder.Property(p => p.DataCadastro)
            .HasColumnType("timestamp")
            .IsRequired();

            // Cria a tabela de data como timestamp
            builder.Property(p => p.DataVencimento)
            .HasColumnType("timestamp")
            .IsRequired();

            // Cria a tabela de data como timestamp
            builder.Property(p => p.DataReferencia)
            .HasColumnType("timestamp");

            // Cria a tabela de data como timestamp
            builder.Property(p => p.DataRecebimento)
            .HasColumnType("timestamp");

            builder.Property(p => p.DataInativacao)
            .HasColumnType("timestamp");

        }
    }
}