using System;
using Nancy;

namespace Selkie.Web.MicroServices.Common.Interfaces.Nancy
{
    public interface IRequestHandler <in T>
    {
        Response DeleteById(Guid id);
        Response FindById(Guid id);
        Response List();
        Response Save(T response);
    }
}