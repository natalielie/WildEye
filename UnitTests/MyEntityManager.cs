using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnitTests.DB;

namespace UnitTests
{
    public class MyEntityManager : IMyEntityManager
    {
        private readonly IDbContextGenerator _contextGenerator;
        public MyEntityManager(IDbContextGenerator contextGenerator)
        {
            _contextGenerator = contextGenerator;
        }

        public EcoClean.Models.Enterprise.Enterprise GetMyEntity()
        {
            var configuration = _contextGenerator.GenerateMyDbContext().Entities.FirstOrDefault();
            return configuration;
        }

        public bool SaveMyEntity(EcoClean.Models.Enterprise.Enterprise myEntity)
        {
            using (var context = _contextGenerator.GenerateMyDbContext())
            {
                var entityToUpdate = context.Entities.FirstOrDefault();
                if (entityToUpdate == null)
                {
                    entityToUpdate = new EcoClean.Models.Enterprise.Enterprise();
                    context.Entities.Add(entityToUpdate);
                }
                PropertiesHelper.CopyProperties(myEntity, entityToUpdate);
                context.SaveChanges();
            }
            return true;
        }

        public bool DeleteMyEntity(EcoClean.Models.Enterprise.Enterprise myEntity)
        {
            using (var context = _contextGenerator.GenerateMyDbContext())
            {
                var entityToDelete = context.Entities.FirstOrDefault();
                if (entityToDelete == null)
                {
                    return true;
                }
                context.Entities.Remove(entityToDelete);
                PropertiesHelper.CopyProperties(myEntity, entityToDelete);
                context.SaveChanges();
            }
            return true;
        }
    }
}
