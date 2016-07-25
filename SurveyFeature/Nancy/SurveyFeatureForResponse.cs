using Selkie.Web.MicroServices.SurveyFeature.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.SurveyFeature.Nancy
{
    public sealed class SurveyFeatureForResponse : ISurveyFeatureForResponse
    {
        public SurveyFeatureForResponse()
        {
            EndPointX = DefaultCoordinate;
            EndPointY = DefaultCoordinate;
            RunDirection = string.Empty;
            StartPointX = DefaultCoordinate;
            StartPointY = DefaultCoordinate;
        }

        internal const double DefaultCoordinate = 0.0;

        public double AngleToXAxisAtEndPoint { get; set; }

        public double AngleToXAxisAtStartPoint { get; set; }

        public int ColonyId { get; set; }

        public double EndPointX { get; set; }

        public double EndPointY { get; set; }

        public bool IsUnknown { get; set; }

        public double Length { get; set; }

        public string RunDirection { get; set; }

        public double StartPointX { get; set; }

        public double StartPointY { get; set; }

        public int SurveyFeatureId { get; set; }

        public int Id
        {
            get
            {
                return SurveyFeatureId;
            }
            set
            {
                SurveyFeatureId = value;
            }
        }
    }
}