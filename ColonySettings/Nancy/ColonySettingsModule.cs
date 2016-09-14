using System;
using JetBrains.Annotations;
using Nancy;
using Nancy.ModelBinding;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.Nancy;

// ReSharper disable ImplicitlyCapturedClosure

namespace Selkie.Web.MicroServices.ColonySettings.Nancy
{
    public class ColonySettingsModule
        : NancyModule
    {
        public ColonySettingsModule([NotNull] IColonyRequestHandler handler)
            : base("/colonysettings")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:Guid}" ] =
                parameters => handler.FindById(( Guid ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <ColonySettingsForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <ColonySettingsForResponse>());

            Delete [ "/{id:Guid}" ] =
                parameters => handler.DeleteById(( Guid ) parameters.id);
        }
    }
}