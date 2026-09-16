using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PonteDourada.Data;

public partial class Sessao2Context : DbContext
{
    public Sessao2Context()
    {
    }

    public Sessao2Context(DbContextOptions<Sessao2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cashback> Cashbacks { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Fornecedor> Fornecedors { get; set; }

    public virtual DbSet<Pessoa> Pessoas { get; set; }

    public virtual DbSet<Produto> Produtos { get; set; }

    public virtual DbSet<ProdutoSolicitacao> ProdutoSolicitacaos { get; set; }

    public virtual DbSet<Solicitacao> Solicitacaos { get; set; }

    public virtual DbSet<TiposProduto> TiposProdutos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=Sessao2;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cashback>(entity =>
        {
            entity.ToTable("Cashback");

            entity.HasOne(d => d.Solicitacao).WithMany(p => p.Cashbacks)
                .HasForeignKey(d => d.SolicitacaoId)
                .HasConstraintName("FK_Cashback_Solicitacao");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cliente__3214EC0705F4F54B");

            entity.ToTable("Cliente");

            entity.HasIndex(e => e.Cpf, "UQ__Cliente__C1F89731ECB2414E").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CPF");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cliente_Pessoa");

            entity.HasOne(d => d.Responsavel).WithMany(p => p.InverseResponsavel)
                .HasForeignKey(d => d.ResponsavelId)
                .HasConstraintName("FK__Cliente__Respons__403A8C7D");
        });

        modelBuilder.Entity<Fornecedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forneced__3214EC07E5BDDF11");

            entity.ToTable("Fornecedor");

            entity.HasIndex(e => e.Cnpj, "UQ__Forneced__AA57D6B40A4CB1F8").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cnpj)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNPJ");
            entity.Property(e => e.RazaoSocial)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Fornecedor)
                .HasForeignKey<Fornecedor>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Fornecedor_Pessoa");
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pessoa__3214EC0780D161FD");

            entity.ToTable("Pessoa");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Produto__3214EC07E32A28F2");

            entity.ToTable("Produto");

            entity.Property(e => e.DataHoraCadastro).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasColumnType("text");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Fornecedor).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.FornecedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Produto__Fornece__4D94879B");

            entity.HasOne(d => d.Tipo).WithMany(p => p.Produtos)
                .HasForeignKey(d => d.TipoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Produto_TiposProduto");
        });

        modelBuilder.Entity<ProdutoSolicitacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProdutoS__3214EC07FCD94F73");

            entity.ToTable("ProdutoSolicitacao");

            entity.HasOne(d => d.Produto).WithMany(p => p.ProdutoSolicitacaos)
                .HasForeignKey(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProdutoSo__Produ__5535A963");

            entity.HasOne(d => d.Solicitcao).WithMany(p => p.ProdutoSolicitacaos)
                .HasForeignKey(d => d.SolicitcaoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProdutoSo__Solic__5441852A");
        });

        modelBuilder.Entity<Solicitacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Solicita__3214EC07576DC8DA");

            entity.ToTable("Solicitacao");

            entity.Property(e => e.DataHoraCadastro).HasColumnType("datetime");
            entity.Property(e => e.Descricao).HasColumnType("text");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Solicitacaos)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Solicitac__Clien__5165187F");
        });

        modelBuilder.Entity<TiposProduto>(entity =>
        {
            entity.ToTable("TiposProduto");

            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC0722561F1D");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.Login, "UQ__Usuario__5E55825B41DEA89A").IsUnique();

            entity.HasIndex(e => e.Login, "UQ__Usuario__5E55825BEA2686C3").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SenhaHash)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Usuario)
                .HasForeignKey<Usuario>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuario_Pessoa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
