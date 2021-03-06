﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Selkie.Web.MicroServices.Common.Interfaces;
using Selkie.Web.MicroServices.Common.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public abstract class InformationFinderBaseTests <TResponse, TEntity, TRepository, TFinder>
        where TResponse : IResponse
        where TEntity : IEntity
        where TRepository : class, IRepository <TEntity>
        where TFinder : IInformationFinder <TResponse>
    {
        private readonly Guid DoesNotMatter = Guid.Parse("00000000-0000-0000-0000-000000000001");

        protected abstract TEntity CreateEntity();

        protected abstract TRepository CreateRepository();

        protected abstract TResponse CreateResponse();

        protected abstract TFinder CreateSut(TRepository repository);

        private IQueryable <TEntity> CreateList(CallInfo arg)
        {
            TEntity one = CreateEntity();
            TEntity two = CreateEntity();

            var list = new Collection <TEntity>
                       {
                           one,
                           two
                       };


            return list.AsQueryable();
        }

        [Test]
        public void Delete_CallsRemove_WhenCalled()
        {
            // Arrange
            TEntity entity = CreateEntity();
            TRepository repository = CreateRepository();
            repository.FindById(Guid.NewGuid()).ReturnsForAnyArgs(entity);
            TFinder sut = CreateSut(repository);

            // Act
            sut.Delete(DoesNotMatter);

            // Assert
            repository.Received().Remove(entity);
        }

        [Test]
        public void Delete_ReturnsDeletedItem_WhenCalled()
        {
            // Arrange
            TEntity entity = CreateEntity();
            TRepository repository = CreateRepository();
            repository.FindById(DoesNotMatter).ReturnsForAnyArgs(entity);
            TFinder sut = CreateSut(repository);

            // Act
            TResponse actual = sut.Delete(DoesNotMatter);

            // Assert
            Assert.AreEqual(entity.Id,
                            actual.Id);
        }

        [Test]
        public void Delete_ReturnsNull_ForCanNotAdd()
        {
            // Arrange
            TRepository repository = CreateRepository();
            repository.FindById(Guid.NewGuid()).ReturnsForAnyArgs(null);
            TFinder sut = CreateSut(repository);

            // Act
            TResponse actual = sut.Delete(DoesNotMatter);

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void FindById_ReturnsDay_ForExistingId()
        {
            // Arrange
            TEntity entity = CreateEntity();
            TRepository repository = CreateRepository();
            repository.FindById(entity.Id).Returns(entity);
            TFinder sut = CreateSut(repository);

            // Act
            TResponse actual = sut.FindById(entity.Id);

            // Assert
            Assert.AreEqual(entity.Id,
                            actual.Id);
        }

        [Test]
        public void FindById_ReturnsNull_ForNotExistingId()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            TRepository repository = CreateRepository();
            repository.FindById(id).Returns(null);
            TFinder sut = CreateSut(repository);

            // Act
            TResponse actual = sut.FindById(id);

            // Assert
            Assert.Null(actual);
        }

        [Test]
        public void List_ReturnsColonies_WhenCalled()
        {
            // Arrange
            TRepository repository = CreateRepository();
            repository.All.Returns(CreateList);
            TFinder sut = CreateSut(repository);

            // Act
            IEnumerable <TResponse> actual = sut.List();

            // Assert
            Assert.AreEqual(2,
                            actual.Count());
        }

        [Test]
        public void Save_CallsSave_WhenCalled()
        {
            // Arrange
            TResponse toBeUpdated = CreateResponse();
            TEntity entity = CreateEntity();
            TRepository repository = CreateRepository();
            TFinder sut = CreateSut(repository);

            // Act
            sut.Save(toBeUpdated);

            // Assert
            repository.Received().Save(Arg.Is <TEntity>(x => x.Id == toBeUpdated.Id));
        }

        [Test]
        public void Save_ReturnsUpdatedColony_ForExisting()
        {
            // Arrange
            TResponse toBeUpdated = CreateResponse();
            TEntity entity = CreateEntity();
            TRepository repository = CreateRepository();
            TFinder sut = CreateSut(repository);

            // Act
            TResponse actual = sut.Save(toBeUpdated);

            // Assert
            Assert.AreEqual(toBeUpdated.Id,
                            actual.Id);
        }
    }
}