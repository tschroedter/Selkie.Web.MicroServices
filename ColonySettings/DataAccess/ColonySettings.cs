using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Selkie.Web.MicroServices.ColonySettings.Interfaces.DataAccess;

namespace Selkie.Web.MicroServices.ColonySettings.DataAccess
{
    public sealed class ColonySettings : IColonySettings
    {
        public ColonySettings()
        {
            CostMatrixInternalData = string.Empty;
            CostPerFeatureInternalData = string.Empty;
            IsFixedStartNode = false;
        }

        internal const char NumberSeperator = ',';
        internal const char ArraySeperator = ':';

        [NotNull]
        [Required]
        public string CostMatrixInternalData { get; set; }

        [NotNull]
        [Required]
        public string CostPerFeatureInternalData { get; set; }

        [NotMapped]
        public int[][] CostMatrix
        {
            get
            {
                if ( string.IsNullOrEmpty(CostMatrixInternalData) )
                {
                    return new int[0][];
                }

                string[] lines = CostMatrixInternalData.Split(ArraySeperator);

                var arrays = new int[lines.Length][];

                for ( var i = 0 ; i < lines.Length ; i++ )
                {
                    int[] array = Array.ConvertAll(lines [ i ].Split(NumberSeperator),
                                                   int.Parse);

                    arrays [ i ] = array;
                }

                return arrays;
            }
            set
            {
                int[][] data = value;

                var builder = new StringBuilder();
                var isFirstLine = true;

                foreach ( int[] ints in data )
                {
                    if ( isFirstLine )
                    {
                        isFirstLine = false;
                    }
                    else
                    {
                        builder.Append(ArraySeperator);
                    }

                    string text = string.Join(",",
                                              ints.Select(p => p.ToString()).ToArray());

                    builder.Append(text);
                }

                CostMatrixInternalData = builder.ToString();
            }
        }

        [NotMapped]
        public int[] CostPerFeature
        {
            get
            {
                if ( string.IsNullOrEmpty(CostPerFeatureInternalData) )
                {
                    return new int[0];
                }

                return Array.ConvertAll(CostPerFeatureInternalData.Split(NumberSeperator),
                                        int.Parse);
            }
            set
            {
                int[] data = value;
                CostPerFeatureInternalData = string.Join(",",
                                                         data.Select(p => p.ToString()).ToArray());
            }
        }

        [Required]
        public int FixedStartNode { get; set; }

        [Required]
        public bool IsFixedStartNode { get; set; }

        [Required]
        public Guid ColonyId { get; set; }

        [Key]
        public Guid ColonySettingsId { get; set; }

        [NotMapped]
        public Guid Id
        {
            get
            {
                return ColonySettingsId;
            }
            set
            {
                ColonySettingsId = value;
            }
        }
    }
}