using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projectoef.Models;

namespace projectoef;

public class TareasContext: DbContext
{
    public DbSet<Categoria> Categorias {get; set;}
    public DbSet<Tarea> Tareas {get; set;}
    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria{CategoriaId = Guid.Parse("c617c17e-fe06-4009-bc12-6c10cd654239"), Nombre = "Actividades pendientes", Peso = 20});
        categoriasInit.Add(new Categoria{CategoriaId = Guid.Parse("c617c17e-fe06-4009-bc12-6c10cd654202"), Nombre = "Actividades personales", Peso = 50});

        modelBuilder.Entity<Categoria>(categoria =>{
            categoria.ToTable("Categoria");
            categoria.HasKey(p => p.CategoriaId);

            categoria.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(150);
            categoria.Property(p => p.Descripcion)
                    .IsRequired(false);

            categoria.Property(p => p.Peso);

            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();
        
        tareasInit.Add(new Tarea{TareaId = Guid.Parse("c617c17e-fe06-4009-bc12-6c10cd654240"), CategoriaId = Guid.Parse("c617c17e-fe06-4009-bc12-6c10cd654239"), Titulo = "Pago de servicios publicos", PrioridadTarea = Prioridad.Media, FechaCreacion = DateTime.Now});

        tareasInit.Add(new Tarea{TareaId = Guid.Parse("c617c17e-fe06-4009-bc12-6c10cd654241"), CategoriaId = Guid.Parse("c617c17e-fe06-4009-bc12-6c10cd654202"), Titulo = "terminar de ver pelicula en netflix", PrioridadTarea = Prioridad.Baja, FechaCreacion = DateTime.Now});


        modelBuilder.Entity<Tarea>(tarea =>{
            tarea.ToTable("Tarea");
            tarea.HasKey(p => p.TareaId);            
            
            tarea.HasOne(p => p.Categoria)
                .WithMany(p => p.Tareas)
                .HasForeignKey(p => p.CategoriaId);

            tarea.Property(p => p.Titulo)
                .IsRequired()
                .HasMaxLength(200);
                
            tarea.Property(p => p.Descripcion)
                .IsRequired(false);

            tarea.Property(p => p.PrioridadTarea);
            tarea.Property(p => p.FechaCreacion);
            tarea.Ignore(p => p.Resumen);

            tarea.HasData(tareasInit);
        });
    }
    
}