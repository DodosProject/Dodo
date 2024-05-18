using Microsoft.EntityFrameworkCore;
using DoDo.Models;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Data.Common;

namespace DoDo.Data
{
    public class DoDoContext : DbContext
    {

        public DoDoContext(DbContextOptions<DoDoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .Property(usr => usr.UserId)
                .HasColumnName("Id");
            modelBuilder.Entity<DoTask>()
                .Property(bk => bk.DoTaskId)
                .HasColumnName("Id");
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Name = "Admin", Email = "admin@admin.com", Password = "admin", RegistrationDate = new DateTime(2024,04,03,17,0,0)},
                new User { UserId = 2, Name = "Ignacio", Email = "ignaciocasaus1cns@gmail.com", Password = "patata", RegistrationDate = new DateTime(2024,04,04,19,0,0)},
                new User { UserId = 3, Name = "Alex", Email = "emaildealex@gmail.com", Password = "pimiento", RegistrationDate = new DateTime(2024,04,05,18,30,0)},
                new User { UserId = 4, Name = "Pepe", Email = "pepe@pepe.com", Password = "pepe", RegistrationDate = new DateTime(2024,04,15,19,30,0)}
            );
            modelBuilder.Entity<DoTask>().HasData(
                new DoTask {DoTaskId = 10001, Title = "Fregar", Description = "Tengo que fregar el suelo", CreationDate = new DateTime(2024,05,15,19,30,0), Completed = false, Priority = 1, UserId = 1},                
                new DoTask {DoTaskId = 10002, Title = "Comprar", Description = "Hacer la compra de comida del mes", CreationDate = new DateTime(2024,05,16,19,30,0), Completed = false, Priority = 1, UserId = 1},
                new DoTask {DoTaskId = 10003, Title = "Banco", Description = "Ir al banco a ingresar dinero", CreationDate = new DateTime(2024,04,15,19,30,0), Completed = true, Priority = 2, UserId = 3},
                new DoTask {DoTaskId = 10004, Title = "Leer", Description = "Leer 3 capítulos de La comunidad del anillo", CreationDate = new DateTime(2024,04,24,16,30,0), Completed = false, Priority = 1, UserId = 2},
                new DoTask {DoTaskId = 10005, Title = "Colada", Description = "Hacer la colada de la ropa sucia", CreationDate = new DateTime(2024,04,28,19,30,0), Completed = true, Priority = 3, UserId = 4},                
                new DoTask {DoTaskId = 10006, Title = "Crear App", Description = "Crear una App en Vue contenerizada", CreationDate = new DateTime(2024,05,15,19,30,0), Completed = true, Priority = 4, UserId = 3},
                new DoTask {DoTaskId = 10007, Title = "Clonar repo", Description = "Clonar el repositorio que me han mandado", CreationDate = new DateTime(2024,04,11,19,30,0), Completed = true, Priority = 2, UserId = 3},
                new DoTask {DoTaskId = 10008, Title = "Desplegar", Description = "Desplegar la App en AWS", CreationDate = new DateTime(2024,05,09,19,30,0), Completed = false, Priority = 1, UserId = 2}
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }


        public DbSet<User> Users { get; set; }
        public DbSet<DoTask> DoTasks { get; set; }
       
    }
}