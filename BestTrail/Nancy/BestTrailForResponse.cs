using System.Collections.Generic;
using Selkie.Web.MicroServices.BestTrail.Interfaces.Nancy;

namespace Selkie.Web.MicroServices.BestTrail.Nancy
{
    public sealed class BestTrailForResponse : IBestTrailForResponse
    {
        public BestTrailForResponse()
        {
            Trail = new int[0];
        }

        public double Alpha { get; set; }
        public int BestTrailId { get; set; }
        public double Beta { get; set; }
        public int ColonyId { get; set; }
        public double Gamma { get; set; }
        public int Iteration { get; set; }
        public double Length { get; set; }
        public IEnumerable <int> Trail { get; set; }
        public string Type { get; set; }

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