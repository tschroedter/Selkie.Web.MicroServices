using System;
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
            : base("/besttrails")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:Guid}" ] =
                parameters => handler.FindById(( Guid ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <BestTrailForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <BestTrailForResponse>());

            Delete [ "/{id:Guid}" ] =
                parameters => handler.DeleteById(( Guid ) parameters.id);
        }
    }
}