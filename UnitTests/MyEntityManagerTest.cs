using EcoClean.Models.Enterprise;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using UnitTests.DB;

namespace UnitTests
{
    public class MyEntityManagerTest
    {
        IDbContextGenerator contextGenerator;
        List<EcoClean.Models.Enterprise.Enterprise> entities;

        [SetUp]
        public void Setup()
        {
            entities = new List<EcoClean.Models.Enterprise.Enterprise>();
            entities.Add(new EcoClean.Models.Enterprise.Enterprise
            {
                EnterpriseId = 1,
                Name = "WOG",
                Kind = "Gas Station",
                PhoneNumber = "+40097656789",
                Product = "Fuel",
                Address = "Riverside st, 33b",
                Rate = 0,
                ClientId = 1
            });
            var myDbMoq = new Mock<IMyDbContext>();
            myDbMoq.Setup(p => p.Entities).Returns(DbContextMock.GetQueryableMockDbSet<Enterprise>(entities));
            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);

            var moq = new Mock<IDbContextGenerator>();
            moq.Setup(p => p.GenerateMyDbContext()).Returns(myDbMoq.Object);

            contextGenerator = moq.Object;
        }

        [Test]
        public void GetEnterpriseShouldReturnConfig()
        {
            var manager = new MyEntityManager(contextGenerator);

            var entity = manager.GetMyEntity();

            Assert.NotNull(entity);
            Assert.AreEqual(1, entity.EnterpriseId);
            Assert.AreEqual("WOG", entity.Name);
        }

        [Test]
        public void SaveEnterpriseShouldReturnValueUpdated()
        {
            var manager = new MyEntityManager(contextGenerator);

            var entity = manager.GetMyEntity();
            entity.Name = "OKKO";
            manager.SaveMyEntity(entity);
            var entitySaved = manager.GetMyEntity();

            Assert.NotNull(entity);
            Assert.NotNull(entitySaved);
            Assert.AreEqual(entity.Name, entitySaved.Name);
            Assert.AreEqual("OKKO", entitySaved.Name);
        }

        [Test]
        public void DeleteEnterpriseShouldReturnNull()
        {
            var manager = new MyEntityManager(contextGenerator);

            var entity = manager.GetMyEntity();
            var entityDeleted = manager.DeleteMyEntity(entity);

            Assert.AreEqual(1, entity.EnterpriseId);
            Assert.IsTrue(entityDeleted);
        }
    }
}