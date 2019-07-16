﻿using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Data.Entities.Models
{
    public partial class PetshopContext : DbContext
    {
        public PetshopContext()
        {
        }

        public PetshopContext(DbContextOptions<PetshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<Animal> Animal { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<PorteAnimal> PorteAnimal { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Promocao> Promocao { get; set; }
        public virtual DbSet<RacaAnimal> RacaAnimal { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }
        public virtual DbSet<ServicoLog> ServicoLog { get; set; }
        public virtual DbSet<ServicoProduto> ServicoProduto { get; set; }
        public virtual DbSet<ServicoUsuario> ServicoUsuario { get; set; }
        public virtual DbSet<TipoAnimal> TipoAnimal { get; set; }
        public virtual DbSet<TipoPagamento> TipoPagamento { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioEspecialidade> UsuarioEspecialidade { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<VendaAvaliacao> VendaAvaliacao { get; set; }
        public virtual DbSet<VendaProduto> VendaProduto { get; set; }
        public virtual DbSet<VendaServico> VendaServico { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // get the configuration from the app settings
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                // define the database to use
                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.Property(e => e.DiaMarcado).HasColumnType("date");

                entity.Property(e => e.Observacao)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.HasOne(d => d.Animal)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.AnimalId)
                    .HasConstraintName("FK__Agendamen__Anima__33D4B598");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Agendamen__Clien__32E0915F");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.ServicoId)
                    .HasConstraintName("FK__Agendamen__Servi__34C8D9D1");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__Agendamen__Usuar__35BCFE0A");
            });

            modelBuilder.Entity<Animal>(entity =>
            {
                entity.Property(e => e.Alergia)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Detalhes)
                    .HasMaxLength(600)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(120)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Animal__ClienteI__2C3393D0");

                entity.HasOne(d => d.PorteAnimal)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.PorteAnimalId)
                    .HasConstraintName("FK__Animal__PorteAni__2B3F6F97");

                entity.HasOne(d => d.RacaAnimal)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.RacaAnimalId)
                    .HasConstraintName("FK__Animal__RacaAnim__2A4B4B5E");

                entity.HasOne(d => d.TipoAnimal)
                    .WithMany(p => p.Animal)
                    .HasForeignKey(d => d.TipoAnimalId)
                    .HasConstraintName("FK__Animal__TipoAnim__29572725");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DataCadastro).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Endereco)
                    .HasMaxLength(230)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Rg)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PorteAnimal>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.Property(e => e.DataCadastro).HasColumnType("date");

                entity.Property(e => e.Especificacao).HasColumnType("text");

                entity.Property(e => e.Fabricante)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Preco).HasColumnType("decimal(15, 2)");
            });

            modelBuilder.Entity<Promocao>(entity =>
            {
                entity.Property(e => e.Percentual).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.DataInicio).HasColumnType("date");

                entity.Property(e => e.DataFim).HasColumnType("date");

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.Promocao)
                    .HasForeignKey(d => d.ProdutoId)
                    .HasConstraintName("FK__Promocao__Produt__2F10007B");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.Promocao)
                    .HasForeignKey(d => d.ServicoId)
                    .HasConstraintName("FK__Promocao__Servic__300424B4");
            });

            modelBuilder.Entity<RacaAnimal>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Preco).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.PrecoAntigo).HasColumnType("decimal(15, 2)");
            });

            modelBuilder.Entity<ServicoLog>(entity =>
            {
                entity.Property(e => e.DataAlteracao).HasColumnType("date");

                entity.Property(e => e.PrecoAntigo).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.PrecoNovo).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.ServicoLog)
                    .HasForeignKey(d => d.ServicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicoLo__Servi__1FCDBCEB");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ServicoLog)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicoLo__Usuar__20C1E124");
            });

            modelBuilder.Entity<ServicoProduto>(entity =>
            {
                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.ServicoProduto)
                    .HasForeignKey(d => d.ProdutoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicoPr__Produ__1BFD2C07");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.ServicoProduto)
                    .HasForeignKey(d => d.ServicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicoPr__Servi__1CF15040");
            });

            modelBuilder.Entity<ServicoUsuario>(entity =>
            {
                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.ServicoUsuario)
                    .HasForeignKey(d => d.ServicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicoUs__Servi__49C3F6B7");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.ServicoUsuario)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ServicoUs__Usuar__48CFD27E");
            });

            modelBuilder.Entity<TipoAnimal>(entity =>
            {
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoPagamento>(entity =>
            {
                entity.Property(e => e.Nome)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Crv)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DataCadastro).HasColumnType("date");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Rg)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.SenhaAcesso)
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UsuarioEspecialidade>(entity =>
            {
                entity.HasOne(d => d.TipoAnimal)
                    .WithMany(p => p.UsuarioEspecialidade)
                    .HasForeignKey(d => d.TipoAnimalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioEs__TipoA__145C0A3F");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuarioEspecialidade)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UsuarioEs__Usuar__15502E78");
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.Property(e => e.DataPagamento).HasColumnType("date");

                entity.Property(e => e.ValorPago).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorProdutos).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorProdutosDesconto).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorServico).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorServicoDesconto).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Agendamento)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.AgendamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Venda__Agendamen__3A81B327");

                entity.HasOne(d => d.TipoPagamento)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.TipoPagamentoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Venda__TipoPagam__3B75D760");
            });

            modelBuilder.Entity<VendaAvaliacao>(entity =>
            {
                entity.HasOne(d => d.Venda)
                    .WithMany(p => p.VendaAvaliacao)
                    .HasForeignKey(d => d.VendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendaAval__Venda__45F365D3");
            });

            modelBuilder.Entity<VendaProduto>(entity =>
            {
                entity.Property(e => e.Valor).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorComDesconto).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Produto)
                    .WithMany(p => p.VendaProduto)
                    .HasForeignKey(d => d.ProdutoId)
                    .HasConstraintName("FK__VendaProd__Produ__3F466844");

                entity.HasOne(d => d.Venda)
                    .WithMany(p => p.VendaProduto)
                    .HasForeignKey(d => d.VendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendaProd__Venda__3E52440B");
            });

            modelBuilder.Entity<VendaServico>(entity =>
            {
                entity.Property(e => e.Valor).HasColumnType("decimal(15, 2)");

                entity.Property(e => e.ValorComDesconto).HasColumnType("decimal(15, 2)");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.VendaServico)
                    .HasForeignKey(d => d.ServicoId)
                    .HasConstraintName("FK__VendaServ__Servi__4316F928");

                entity.HasOne(d => d.Venda)
                    .WithMany(p => p.VendaServico)
                    .HasForeignKey(d => d.VendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VendaServ__Venda__4222D4EF");
            });
        }
    }
}