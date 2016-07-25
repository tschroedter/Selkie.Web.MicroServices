using JetBrains.Annotations;
using Nancy;
using Nancy.ModelBinding;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;

// ReSharper disable ImplicitlyCapturedClosure

namespace Selkie.Web.MicroServices.BestTrail.Nancy
{
    public class BestTrailModule
        : NancyModule
    {
        public BestTrailModule([NotNull] IBestTrailRequestHandler handler)
            : base("/besttrail")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <BestTrailForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <BestTrailForResponse>());

            Delete [ "/{id:int}" ] =
                parameters => handler.DeleteById(( int ) parameters.id);
        }
    }
}