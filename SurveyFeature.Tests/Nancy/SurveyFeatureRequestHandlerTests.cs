using System;
using System.Diagnostics.CodeAnalysis;
using NSubstitute;
using NUnit.Framework;
using Selkie.Web.MicroServices.Common.Tests.Extensions.Nancy;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;
using Selkie.Web.MicroServices.SurveyFeature.Nancy;

namespace Selkie.Web.MicroServices.SurveyFeature.Tests.Nancy
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class SurveyFeatureRequestHandlerTests
        : RequestHandlerBaseTests
              <ISurveyFeatureForResponse, ISurveyFeatureInformationFinder, ISurveyFeatureRequestHandler>
    {
        protected override ISurveyFeatureInformationFinder CreateFinder()
        {
            return Substitute.For <ISurveyFeatureInformationFinder>();
        }

        protected override ISurveyFeatureRequestHandler CreateSut(ISurveyFeatureInformationFinder finder)
        {
            return new SurveyFeatureRequestHandler(finder);
        }

        protected override ISurveyFeatureForResponse CreateResponse()
        {
            return new SurveyFeatureForResponse
                   {
                       ColonyId = Guid.NewGuid()
                   };
        }
    }
}