using System;

namespace Selkie.Web.MicroServices.ColonyMonitor.Dtos
{
    public class SurveyFeatureDto
    {
        public SurveyFeatureDto()
        {
            RunDirection = string.Empty;
        }

        public double AngleToXAxisAtEndPoint { get; set; }
        public double AngleToXAxisAtStartPoint { get; set; }
        public Guid ColonyId { get; set; }
        public double EndPointY { get; set; }
        public double EndPointX { get; set; }
        public bool IsUnknown { get; set; }
        public double Length { get; set; }
        public string RunDirection { get; set; }
        public double StartPointY { get; set; }
        public double StartPointX { get; set; }
        public Guid SurveyFeatureId { get; set; }
    }
}