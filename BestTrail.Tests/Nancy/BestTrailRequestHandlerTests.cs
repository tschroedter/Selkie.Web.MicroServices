using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;
using Selkie.Web.MicroServices.BestTrail.Nancy;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;

namespace Selkie.Web.MicroServices.BestTrail.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class BestTrailRequestHandlerTests
        : RequestHandlerBaseTests <IBestTrailForResponse, IBestTrailInformationFinder, IBestTrailRequestHandler>
    {
        protected override IBestTrailInformationFinder CreateFinder()
        {
            return Substitute.For <IBestTrailInformationFinder>();
        }

        protected override IBestTrailRequestHandler CreateSut(IBestTrailInformationFinder finder)
        {
            return new BestTrailRequestHandler(finder);
        }

        protected override IBestTrailForResponse CreateResponse()
        {
            return new BestTrailForResponse
                   {
                       ColonyId = Guid.NewGuid()
                   };
        }
    }
}