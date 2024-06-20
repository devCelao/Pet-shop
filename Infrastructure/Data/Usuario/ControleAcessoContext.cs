using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Usuario;
public class ControleAcessoContext : DbContext
{
    public ControleAcessoContext(DbContextOptions<ControleAcessoContext> options)
        : base(options)
    {
        
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Permissao> Permissoes { get; set; }
    public DbSet<PermissaoUsuario> PermissoesUsuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Ignore<ValidationResult>();

        modelBuilder.Entity<Usuario>()
            .ToTable("USUARIO");

        modelBuilder.Entity<Permissao>()
            .ToTable("PERMISSAO");

        modelBuilder.Entity<PermissaoUsuario>()
            .ToTable("PERMISSAO_USUARIO");


        // Configurar chave composta para PermissaoUsuario
        modelBuilder.Entity<PermissaoUsuario>()
            .HasKey(pu => pu.ID);

        // Configurar relacionamento muitos-para-muitos entre Usuario e Permissao
        modelBuilder.Entity<PermissaoUsuario>()
            .HasOne(pu => pu.Usuario)
            .WithMany(u => u.Permissoes)
            .HasForeignKey(pu => pu.UsuarioId);

        modelBuilder.Entity<PermissaoUsuario>()
            .HasOne(pu => pu.Permissao)
            .WithMany(p => p.Permissoes)
            .HasForeignKey(pu => pu.PermissaoId);
    }
}
