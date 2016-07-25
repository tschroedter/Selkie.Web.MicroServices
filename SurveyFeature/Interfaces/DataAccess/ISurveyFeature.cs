using Selkie.Web.MicroServices.Common.Interfaces;

namespace Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess
{
    public interface ISurveyFeature : IEntity
    {
        double AngleToXAxisAtEndPoint { get; set; }
        double AngleToXAxisAtStartPoint { get; set; }
        int ColonyId { get; set; }
        double EndPointX { get; set; }
        double EndPointY { get; set; }
        bool IsUnknown { get; set; }
        double Length { get; set; }
        string RunDirection { get; set; }
        double StartPointX { get; set; }
        double StartPointY { get; set; }
        int SurveyFeatureId { get; set; }
    }
}