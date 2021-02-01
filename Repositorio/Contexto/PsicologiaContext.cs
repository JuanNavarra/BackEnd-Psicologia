namespace Repositorio
{
    using Microsoft.EntityFrameworkCore;
    using Modelos;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public partial class PsicologiaContext : DbContext
    {
        #region Constructor
        public PsicologiaContext(DbContextOptions<PsicologiaContext> options) : base(options)
        {
        }
        #endregion

        #region Entidades
        public virtual DbSet<BlogKey> BlogKey { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<KeyWords> KeyWords { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Comentarios> Comentarios { get; set; }
        public virtual DbSet<FaqDetalle> FaqDetalle { get; set; }
        public virtual DbSet<Faqs> Faqs { get; set; }
        #endregion

        #region Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogKey>(entity =>
            {
                entity.HasKey(e => e.Idblogkey);

                entity.ToTable("BlogKey", "rec");

                entity.Property(e => e.Idblogkey).HasColumnName("idblogkey");

                entity.Property(e => e.Idblog).HasColumnName("idblog");

                entity.Property(e => e.Idkey).HasColumnName("idkey");

                entity.HasOne(d => d.IdblogNavigation)
                    .WithMany(p => p.BlogKey)
                    .HasForeignKey(d => d.Idblog)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.IdkeyNavigation)
                    .WithMany(p => p.BlogKey)
                    .HasForeignKey(d => d.Idkey)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.HasKey(e => e.Idblog);

                entity.ToTable("Blogs", "rec");

                entity.Property(e => e.Idblog).HasColumnName("idblog");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechaactualizacion)
                    .HasColumnName("fechaactualizacion")
                    .HasColumnType("date");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("fechacreacion")
                    .HasColumnType("date");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Idcreador).HasColumnName("idcreador");

                entity.Property(e => e.Idimagen).HasColumnName("idimagen");

                entity.Property(e => e.Subtitulo)
                    .IsRequired()
                    .HasColumnName("subtitulo");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasColumnName("slug");

                entity.Property(e => e.Cita)
                    .IsRequired(false)
                    .HasColumnName("cita");

                entity.Property(e => e.Autorcita)
                    .IsRequired(false)
                    .HasColumnName("autorcita");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.Idcategoria);

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.Idcategoria);

                entity.HasOne(d => d.IdcreadorNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.Idcreador);

                entity.HasOne(d => d.IdimagenNavigation)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.Idimagen);
            });

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.Idcategoria);

                entity.ToTable("Categorias", "rec");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechaactualizacion)
                    .HasColumnName("fechaactualizacion")
                    .HasColumnType("date");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("fechacreacion")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Imagenes>(entity =>
            {
                entity.HasKey(e => e.Idimagen);

                entity.ToTable("Imagenes", "rec");

                entity.Property(e => e.Idimagen).HasColumnName("idimagen");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("fechacreacion")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre).HasColumnName("nombre");

                entity.Property(e => e.Ruta)
                    .IsRequired()
                    .HasColumnName("ruta");
            });

            modelBuilder.Entity<KeyWords>(entity =>
            {
                entity.HasKey(e => e.Idkey);

                entity.ToTable("KeyWords", "rec");

                entity.Property(e => e.Idkey).HasColumnName("idkey");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechaactualizacion)
                    .HasColumnName("fechaactualizacion")
                    .HasColumnType("date");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("fechacreacion")
                    .HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Idusuario);

                entity.ToTable("Usuarios", "rec");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasColumnName("apellido");

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email");

                entity.Property(e => e.Idimagen).HasColumnName("idimagen");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre");

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass");

                entity.HasOne(d => d.IdimagenNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Idimagen);
            });

            modelBuilder.Entity<Comentarios>(entity =>
            {
                entity.HasKey(e => e.Idcomentario);

                entity.ToTable("Comentarios", "rec");

                entity.Property(e => e.Idcomentario).HasColumnName("idcomentario");

                entity.Property(e => e.Comentario)
                    .IsRequired()
                    .HasColumnName("comentario");

                entity.Property(e => e.Creador)
                    .IsRequired()
                    .HasColumnName("creador");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Fechacreaciion)
                    .HasColumnName("fechacreaciion")
                    .HasColumnType("date");

                entity.Property(e => e.Idblog).HasColumnName("idblog");
            });

            modelBuilder.Entity<FaqDetalle>(entity =>
            {
                entity.HasKey(e => e.Idfaqdetalle);

                entity.ToTable("FaqDetalle", "prin");

                entity.Property(e => e.Idfaqdetalle).HasColumnName("idfaqdetalle");

                entity.Property(e => e.Contenido)
                    .IsRequired()
                    .HasColumnName("contenido");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechaactualizacion)
                    .HasColumnName("fechaactualizacion")
                    .HasColumnType("date");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("fechacreacion")
                    .HasColumnType("date");

                entity.Property(e => e.Idfaq).HasColumnName("idfaq");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo");

                entity.HasOne(d => d.IdfaqNavigation)
                    .WithMany(p => p.FaqDetalle)
                    .HasForeignKey(d => d.Idfaq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_faq_faqid");
            });

            modelBuilder.Entity<Faqs>(entity =>
            {
                entity.HasKey(e => e.Idfaq);

                entity.ToTable("Faqs", "prin");

                entity.Property(e => e.Idfaq).HasColumnName("idfaq");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fechaactualizacion)
                    .HasColumnName("fechaactualizacion")
                    .HasColumnType("date");

                entity.Property(e => e.Fechacreacion)
                    .HasColumnName("fechacreacion")
                    .HasColumnType("date");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        #endregion

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
