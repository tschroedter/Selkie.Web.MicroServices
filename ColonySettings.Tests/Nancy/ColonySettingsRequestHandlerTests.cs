using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.ColonySettings.Nancy;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;

namespace Selkie.Web.MicroServices.ColonySettings.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonySettingsRequestHandlerTests
        : RequestHandlerBaseTests <IColonySettingsForResponse, IColonySettingsInformationFinder, IColonyRequestHandler>
    {
        protected override IColonySettingsInformationFinder CreateFinder()
        {
            return Substitute.For <IColonySettingsInformationFinder>();
        }

        protected override IColonyRequestHandler CreateSut(IColonySettingsInformationFinder finder)
        {
            return new ColonyRequestHandler(finder);
        }

        protected override IColonySettingsForResponse CreateResponse()
        {
            return new ColonySettingsForResponse
                   {
                       ColonySettingsId = Guid.NewGuid()
                   };
        }
    }
}