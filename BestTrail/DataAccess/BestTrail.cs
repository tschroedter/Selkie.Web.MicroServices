using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.BestTrail.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.BestTrail.DataAccess
{
    public sealed class BestTrail : IBestTrail
    {
        public BestTrail()
        {
            TrailInternalData = string.Empty;
            Trail = new int[0];
            Type = "Unknown";
        }

        internal const char NumberSeperator = ',';
        internal const char ArraySeperator = ':';

        [NotNull]
        [Required]
        public string TrailInternalData { get; set; }

        [Required]
        public double Alpha { get; set; }

        [Key]
        public int BestTrailId { get; set; }

        [Required]
        public double Beta { get; set; }

        [Required]
        public int ColonyId { get; set; }

        [Required]
        public double Gamma { get; set; }

        [Required]
        public int Iteration { get; set; }

        [Required]
        public double Length { get; set; }

        [NotNull]
        [NotMapped]
        public IEnumerable <int> Trail
        {
            get
            {
                if ( string.IsNullOrEmpty(TrailInternalData) )
                {
                    return new int[0];
                }

                return Array.ConvertAll(TrailInternalData.Split(NumberSeperator),
                                        int.Parse);
            }
            set
            {
                TrailInternalData = string.Join(",",
                                                value.Select(p => p.ToString()).ToArray());
            }
        }

        [NotNull]
        [UsedImplicitly]
        public string Type { get; set; }

        [NotMapped]
        public int Id
        {
            get
            {
                return BestTrailId;
            }
            set
            {
                BestTrailId = value;
            }
        }
    }
}