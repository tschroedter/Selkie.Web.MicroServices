using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Selkie.Web.MicroServices.Colony.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.Colony.DataAccess
{
    public sealed class Colony : IColony
    {
        public Colony()
        {
            Description = string.Empty;
            Status = ColonyProgress.Status.Unknown;
        }

        [Key]
        public int ColonyId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public ColonyProgress.Status Status { get; set; }

        [NotMapped]
        public int Id
        {
            get
            {
                return ColonyId;
            }
            set
            {
                ColonyId = value;
            }
        }
    }
}