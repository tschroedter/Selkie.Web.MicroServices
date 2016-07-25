using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;
using Selkie.Web.MicroServices.Colony.Nancy;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;

namespace Selkie.Web.MicroServices.Colony.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ColonyRequestHandlerTests
        : RequestHandlerBaseTests <IColonyForResponse, IColonyInformationFinder, IColonyRequestHandler>
    {
        private static int NextIdForResponse;

        protected override IColonyInformationFinder CreateFinder()
        {
            return Substitute.For <IColonyInformationFinder>();
        }

        protected override IColonyRequestHandler CreateSut(IColonyInformationFinder finder)
        {
            return new ColonyRequestHandler(finder);
        }

        protected override IColonyForResponse CreateResponse()
        {
            return new ColonyForResponse
                   {
                       ColonyId = NextIdForResponse++
                   };
        }
    }
}