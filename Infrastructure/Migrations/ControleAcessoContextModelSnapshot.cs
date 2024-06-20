﻿// <auto-generated />
using System;
using Infrastructure.Data.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ControleAcessoContext))]
    partial class ControleAcessoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Infrastructure.Data.Usuario.Permissao", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("COD_PERMISSAO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TXT_DESCRICAO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("PERMISSAO", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Data.Usuario.PermissaoUsuario", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PermissaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("PermissaoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("PERMISSAO_USUARIO", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Data.Usuario.Usuario", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("COD_USUARIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DT_ALTERACAO")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DT_REGISTRO")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IND_ATIVO")
                        .HasColumnType("bit");

                    b.Property<bool>("IND_EMAIL_CONFIRMADO")
                        .HasColumnType("bit");

                    b.Property<string>("NOM_USUARIO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TXT_EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TXT_SENHA")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("USUARIO", (string)null);
                });

            modelBuilder.Entity("Infrastructure.Data.Usuario.PermissaoUsuario", b =>
                {
                    b.HasOne("Infrastructure.Data.Usuario.Permissao", "Permissao")
                        .WithMany("Permissoes")
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Infrastructure.Data.Usuario.Usuario", "Usuario")
                        .WithMany("Permissoes")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permissao");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Infrastructure.Data.Usuario.Permissao", b =>
                {
                    b.Navigation("Permissoes");
                });

            modelBuilder.Entity("Infrastructure.Data.Usuario.Usuario", b =>
                {
                    b.Navigation("Permissoes");
                });
#pragma warning restore 612, 618
        }
    }
}
