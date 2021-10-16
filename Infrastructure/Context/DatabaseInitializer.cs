using System.Linq;
using Microsoft.EntityFrameworkCore;
using UStart.Domain.Entities;

namespace UStart.Infrastructure.Context
{
    public static class DatabaseInitializer
    {
        public static void Initialize(UStartContext dbContext)
        {
            
            if (dbContext.Database.GetPendingMigrations().Count() == 0)
            {
                if (!dbContext.Usuarios.Any())
                {
                    Usuario usuario = new Usuario("suporte@email.com", "123456");
                    dbContext.Usuarios.Add(usuario);

                    if (dbContext.ChangeTracker.HasChanges())
                       dbContext.SaveChanges();
                }
            }
        }
    }
}