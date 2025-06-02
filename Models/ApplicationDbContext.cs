using Microsoft.EntityFrameworkCore;

namespace EmbalagemPedidos.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Caixa> Caixas { get; set; } = null!;
        public DbSet<RespostaEmbalagem> RespostasEmbalagem { get; set; } = null!;
        public DbSet<Dimensoes> Dimensoes { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Caixa>()
                .Ignore(c => c.Produtos);

            modelBuilder.Entity<Caixa>()
                .Property(c => c.ProdutosSerializados)
                .HasColumnName("Produtos");

            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Dimensoes)
                .WithOne()
                .HasForeignKey<Dimensoes>(d => d.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Caixa>()
                .HasOne<Dimensoes>()
                .WithMany()
                .HasForeignKey(c => c.DimensoesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Caixa>()
                .HasOne<RespostaEmbalagem>()
                .WithMany(r => r.Caixas)
                .HasForeignKey(c => c.RespostaEmbalagemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RespostaEmbalagem>()
                .HasOne(r => r.Pedido)
                .WithMany()
                .HasForeignKey(r => r.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}