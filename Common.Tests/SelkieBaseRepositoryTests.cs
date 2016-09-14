using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.Common.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class SelkieBaseRepositoryTests
    {
        [Theory]
        [AutoNSubstituteData]
        public void AddOrUpdate_CallsAddOrUpdate_ForEntityWithDefaultId([NotNull] ITest test,
                                                                        [NotNull, Frozen] ITestContext context,
                                                                        [NotNull] TestSelkieBaseRepository sut)
        {
            // Arrange
            test.Id.Returns(Guid.Empty);

            // Act
            sut.Save(test);

            // Assert
            context.Received().AddOrUpdate(test);
        }

        [Theory]
        [AutoNSubstituteData]
        public void AddOrUpdate_CallsAddOrUpdateForSlot_ForExistingEtity([NotNull] ITest test,
                                                                         [NotNull, Frozen] ITestContext context,
                                                                         [NotNull] TestSelkieBaseRepository sut)
        {
            // Arrange
            test.Id.Returns(Guid.NewGuid());

            // Act
            sut.Save(test);

            // Assert
            context.Received().AddOrUpdate(test);
        }

        [Test]
        public void All_ReturnsEntites_WhenCalled()
        {
            // Arrange
            var context = Substitute.For <ITestContext>();
            context.All().Returns(CreateEntities);
            TestSelkieBaseRepository sut = CreateSut(context);

            // Act
            IEnumerable <ITest> actual = sut.All;

            // Assert
            Assert.AreEqual(3,
                            actual.Count());
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsDEntity_ForKnownId([NotNull] ITest test)
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var context = Substitute.For <ITestContext>();
            context.Find(id).Returns(test);
            TestSelkieBaseRepository sut = CreateSut(context);

            // Act
            ITest actual = sut.FindById(id);

            // Assert
            Assert.AreEqual(test,
                            actual);
        }

        [Test]
        public void FindById_ReturnsNull_ForUnknownId()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var context = Substitute.For <ITestContext>();
            context.Find(id).Returns(( ITest ) null);
            TestSelkieBaseRepository sut = CreateSut(context);

            // Act
            ITest actual = sut.FindById(id);

            // Assert
            Assert.Null(actual);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Remove_CallsSave_WhenCalled([NotNull] ITest test,
                                                [NotNull, Frozen] ITestContext context,
                                                [NotNull] TestSelkieBaseRepository sut)
        {
            // Arrange
            // Act
            sut.Remove(test);

            // Assert
            context.Received().Remove(test);
        }

        [Theory]
        [AutoNSubstituteData]
        public void Save_CallsSaveChanges_WhenCalled([NotNull, Frozen] ITestContext context,
                                                     [NotNull] TestSelkieBaseRepository sut)
        {
            // Arrange
            // Act
            sut.Save();

            // Assert
            context.Received().SaveChanges();
        }

        private IQueryable <ITest> CreateEntities(CallInfo arg)
        {
            var one = Substitute.For <ITest>();
            one.Id.Returns(Guid.Parse("00000000-0000-0000-0000-000000000001"));

            var two = Substitute.For <ITest>();
            two.Id.Returns(Guid.Parse("00000000-0000-0000-0000-000000000002"));

            var three = Substitute.For <ITest>();
            three.Id.Returns(Guid.Parse("00000000-0000-0000-0000-000000000003"));

            var list = new[]
                       {
                           one,
                           two,
                           three
                       };

            return list.AsQueryable();
        }

        private TestSelkieBaseRepository CreateSut(ITestContext context)
        {
            return new TestSelkieBaseRepository(context);
        }

        public interface ITest : IEntity
        {
        }

        public interface ITestContext : IDbContext <ITest>
        {
            IQueryable <ITest> AllITests { get; }
        }

        public class TestSelkieBaseRepository
            : SelkieBaseRepository <ITest, ITestContext>
        {
            public TestSelkieBaseRepository([NotNull] ITestContext context)
                : base(context)
            {
            }
        }
    }
}