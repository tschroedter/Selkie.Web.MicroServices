using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;
using Selkie.Web.MicroServices.RacetrackSettings.Nancy;

namespace Selkie.Web.MicroServices.RacetrackSettings.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class RacetrackSettingsRequestHandlerTests
        : RequestHandlerBaseTests
              <IRacetrackSettingsForResponse, IRacetrackSettingsInformationFinder, IRacetrackSettingsRequestHandler>
    {
        private static int NextIdForResponse;

        protected override IRacetrackSettingsInformationFinder CreateFinder()
        {
            return Substitute.For <IRacetrackSettingsInformationFinder>();
        }

        protected override IRacetrackSettingsRequestHandler CreateSut(IRacetrackSettingsInformationFinder finder)
        {
            return new RacetrackSettingsRequestHandler(finder);
        }

        protected override IRacetrackSettingsForResponse CreateResponse()
        {
            return new RacetrackSettingsForResponse
                   {
                       ColonyId = NextIdForResponse++
                   };
        }
    }
}