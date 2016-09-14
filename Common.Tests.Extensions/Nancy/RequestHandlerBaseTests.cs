using System;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Nancy;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.Common.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public abstract class RequestHandlerBaseTests <TResponse, TFinder, THandler>
        where TResponse : IResponse
        where TFinder : IInformationFinder <TResponse>
        where THandler : IRequestHandler <TResponse>
    {
        private readonly Guid DoesNotMatter = Guid.Parse("00000000-0000-0000-0000-000000000001");

        [Theory]
        [AutoNSubstituteData]
        public void Save_ReturnsStatusOK_WhenCalled()
        {
            // Arrange
            TResponse toBeCreated = CreateResponse();
            TResponse created = CreateResponse();
            TFinder finder = CreateFinder();
            finder.Save(toBeCreated).Returns(created);
            THandler sut = CreateSut(finder);

            // Act
            Response actual = sut.Save(toBeCreated);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK,
                            actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void DeleteById_ReturnsResponse_WhenCalled()
        {
            // Arrange
            TResponse response = CreateResponse();
            TFinder finder = CreateFinder();
            finder.Delete(response.Id).Returns(response);
            THandler sut = CreateSut(finder);

            // Act
            Response actual = sut.DeleteById(response.Id);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK,
                            actual.StatusCode);
        }

        [Theory]
        [AutoNSubstituteData]
        public void FindById_ReturnsResponse_ForKnownId()
        {
            // Arrange
            TResponse response = CreateResponse();
            TFinder finder = CreateFinder();
            finder.FindById(response.Id).Returns(response);
            THandler sut = CreateSut(finder);

            // Act
            Response actual = sut.FindById(response.Id);

            // Assert
            Assert.AreEqual(HttpStatusCode.OK,
                            actual.StatusCode);
        }

        private IQueryable <TResponse> CreateList(CallInfo arg)
        {
            TResponse one = CreateResponse();
            TResponse two = CreateResponse();

            var list = new Collection <TResponse>
                       {
                           one,
                           two
                       };

            return list.AsQueryable();
        }

        protected abstract TFinder CreateFinder();

        protected abstract THandler CreateSut(TFinder finder);

        protected abstract TResponse CreateResponse();

        [Test]
        public void DeleteById_ReturnsResponse_ForAddFailed()
        {
            // Arrange
            TFinder finder = CreateFinder();
            finder.Delete(DoesNotMatter).Returns(null);
            THandler sut = CreateSut(finder);

            // Act
            Response actual = sut.DeleteById(DoesNotMatter);

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound,
                            actual.StatusCode);
        }

        [Test]
        public void List_ReturnsResponse_WhenCalled()
        {
            // Arrange
            TFinder finder = CreateFinder();
            finder.List().Returns(CreateList);
            THandler sut = CreateSut(finder);

            // Act
            Response actual = sut.List();

            // Assert
            Assert.AreEqual(HttpStatusCode.OK,
                            actual.StatusCode);
        }
    }
}