using Nancy;

namespace Selkie.Web.MicroServices.Common.Interfaces.Nancy
{
    public interface IRequestHandler <in T>
    {
        Response DeleteById(int id);
        Response FindById(int id);
        Response List();
        Response Save(T response);
    }
}