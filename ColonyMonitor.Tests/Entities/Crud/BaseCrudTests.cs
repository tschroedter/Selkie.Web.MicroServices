using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud;
using Selkie.Web.MicroServices.ColonyMonitor.Entities.Crud.Rest;
using Selkie.Web.MicroServices.ColonyMonitor.Interfaces;

namespace Selkie.MicroServices.ColonyMonitor.Tests.Entities.Crud
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public sealed class BaseCrudTests
    {
        public class TestCrud : BaseCrud <TestDto>
        {
            public TestCrud(
                [NotNull] IBaseUrlReader reader,
                [NotNull] ISelkieRestClientFactory factory)
                : base(reader,
                       factory,
                       "/tests/")
            {
            }
        }

        public class TestDto
        {
            public string Text = string.Empty;
        }

        [Test]
        [AutoNSubstituteData]
        public void BaseUrl_ReturnsUrl_WhenCalled(
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            reader.BaseUrl.Returns("baseUrl");

            // Act
            var sut = new TestCrud(reader,
                                   factory);

            // Assert
            Assert.AreEqual("baseUrl",
                            sut.BaseUrl);
        }

        [Test]
        [AutoNSubstituteData]
        public void CreateOrUpdate_CallCreateOrUpdate_WhenCalled(
            Guid guid,
            TestDto testDtoToCreateOrUpdate,
            TestDto testDto,
            [NotNull] ISelkieRestClient client,
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            client.CreateOrUpdate(testDtoToCreateOrUpdate).Returns(testDto);
            factory.Create(string.Empty,
                           string.Empty).ReturnsForAnyArgs(client);

            var sut = new TestCrud(reader,
                                   factory);

            // Act
            TestDto actual = sut.CreateOrUpdate(testDtoToCreateOrUpdate);

            // Assert
            client.Received().CreateOrUpdate(testDtoToCreateOrUpdate);
        }

        [Test]
        [AutoNSubstituteData]
        public void CreateOrUpdate_ReturnsDto_WhenCalled(
            TestDto testDtoToCreateOrUpdate,
            TestDto testDto,
            [NotNull] ISelkieRestClient client,
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            client.CreateOrUpdate(testDtoToCreateOrUpdate).Returns(testDto);
            factory.Create(string.Empty,
                           string.Empty).ReturnsForAnyArgs(client);

            var sut = new TestCrud(reader,
                                   factory);

            // Act
            TestDto actual = sut.CreateOrUpdate(testDtoToCreateOrUpdate);

            // Assert
            Assert.AreEqual(testDto,
                            actual);
        }

        [Test]
        [AutoNSubstituteData]
        public void MicroServiceName_ReturnsName_WhenCalled(
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            // Act
            var sut = new TestCrud(reader,
                                   factory);

            // Assert
            Assert.AreEqual("/tests/",
                            sut.MicroServiceName);
        }

        [Test]
        [AutoNSubstituteData]
        public void Read_CallRead_WhenCalled(
            Guid guid,
            TestDto testDto,
            [NotNull] ISelkieRestClient client,
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            client.Read <TestDto>(guid).Returns(testDto);
            factory.Create(string.Empty,
                           string.Empty).ReturnsForAnyArgs(client);

            var sut = new TestCrud(reader,
                                   factory);

            // Act
            TestDto actual = sut.Read(guid);

            // Assert
            client.Received().Read <TestDto>(guid);
        }

        [Test]
        [AutoNSubstituteData]
        public void Read_ReturnsDto_WhenCalled(
            Guid guid,
            TestDto testDto,
            [NotNull] ISelkieRestClient client,
            [NotNull] IBaseUrlReader reader,
            [NotNull] ISelkieRestClientFactory factory)
        {
            // Arrange
            client.Read <TestDto>(guid).Returns(testDto);
            factory.Create(string.Empty,
                           string.Empty).ReturnsForAnyArgs(client);

            var sut = new TestCrud(reader,
                                   factory);

            // Act
            TestDto actual = sut.Read(guid);

            // Assert
            Assert.AreEqual(testDto,
                            actual);
        }
    }
}