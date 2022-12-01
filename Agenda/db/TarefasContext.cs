using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Agenda.db;

public partial class TarefasContext : DbContext
{
    public TarefasContext()
    {
    }

    public TarefasContext(DbContextOptions<TarefasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tarefa> Tarefa { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=admin;database=tarefas", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Tarefa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tarefa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Concluida).HasColumnName("concluida");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
