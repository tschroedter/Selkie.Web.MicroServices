using System;
using JetBrains.Annotations;
using Nancy;
using Nancy.ModelBinding;
using Selkie.Web.MicroServices.RacetrackSettings.Interfaces.Nancy;

// ReSharper disable ImplicitlyCapturedClosure

namespace Selkie.Web.MicroServices.RacetrackSettings.Nancy
{
    public class RacetrackSettingsModule
        : NancyModule
    {
        public RacetrackSettingsModule([NotNull] IRacetrackSettingsRequestHandler handler)
            : base("/racetracksettings")
        {
            Get [ "/" ] =
                parameters => handler.List();

            Get [ "/{id:Guid}" ] =
                parameters => handler.FindById(( Guid ) parameters.id);

            Post [ "/" ] =
                parameters => handler.Save(this.Bind <RacetrackSettingsForResponse>());

            Put [ "/" ] =
                parameters => handler.Save(this.Bind <RacetrackSettingsForResponse>());

            Delete [ "/{id:Guid}" ] =
                parameters => handler.DeleteById(( Guid ) parameters.id);
        }
    }
}