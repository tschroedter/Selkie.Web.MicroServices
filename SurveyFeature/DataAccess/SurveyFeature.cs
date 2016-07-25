using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.SurveyFeature.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.SurveyFeature.DataAccess
{
    [ExcludeFromCodeCoverage]
    public sealed class SurveyFeature : ISurveyFeature
    {
        public SurveyFeature()
        {
            EndPointX = DefaultCoordinate;
            EndPointY = DefaultCoordinate;
            RunDirection = string.Empty;
            StartPointX = DefaultCoordinate;
            StartPointY = DefaultCoordinate;
        }

        internal const double DefaultCoordinate = 0.0;

        [Required]
        public double AngleToXAxisAtEndPoint { get; set; }

        [Required]
        public double AngleToXAxisAtStartPoint { get; set; }

        [Required]
        public int ColonyId { get; set; }

        [Required]
        public double EndPointX { get; set; }

        [Required]
        public double EndPointY { get; set; }

        [Required]
        public bool IsUnknown { get; set; }

        [Required]
        public double Length { get; set; }

        [Required]
        [NotNull]
        public string RunDirection { get; set; }

        [Required]
        public double StartPointX { get; set; }

        [Required]
        public double StartPointY { get; set; }

        [Key]
        public int SurveyFeatureId { get; set; }

        [NotMapped]
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