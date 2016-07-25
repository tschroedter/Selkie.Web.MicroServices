using JetBrains.Annotations;
using Nancy;
using Nancy.ModelBinding;
using Selkie.Web.MicroServices.Colony.Interfaces.Nancy;

// ReSharper disable ImplicitlyCapturedClosure

namespace Selkie.Web.MicroServices.Colony.Nancy
{
    public class ColonyModule
        : NancyModule
    {
        public ColonyModule([NotNull] IColonyRequestHandler handler)
            : base("/colonies")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:int}" ] =
                parameters => handler.FindById(( int ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <ColonyForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <ColonyForResponse>());

            Delete [ "/{id:int}" ] =
                parameters => handler.DeleteById(( int ) parameters.id);
        }
    }
}