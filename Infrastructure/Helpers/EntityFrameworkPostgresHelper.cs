using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UStart.Infrastructure.Helpers
{
    public class EntityFrameworkPostgresHelper
    {
        public static void MapPropertiesLowerCase<T>(ModelBuilder modelBuilder) where T: Domain.Contracts.Entities.IEntity
        {            
            var properties =typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                modelBuilder.Entity(typeof(T).FullName).Property(prop.Name).HasColumnName(prop.Name.ToLower());
            }
        }
    }
}
