using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ITSolution.Framework.Servers.Core.CustomUserAPI.Data
{
    [Table("COUNTRIES")]
    public class Country
    {
        [Column("COUNTRY_ID")]
        public int CountryId { get; set; }

        [Column("COUNTRY_ISO_CODE")]
        public string CountryIsoCode { get; set; }

        [Column("COUNTRY_NAME")]
        public string CountryName { get; set; }

        [Column("COUNTRY_SUBREGION")]
        public string CountrySubregion { get; set; }

        [Column("COUNTRY_SUBREGION_ID")]
        public int CountrySubregionId { get; set; }

        [Column("COUNTRY_REGION")]
        public string CountryRegion { get; set; }

        [Column("COUNTRY_REGION_ID")]
        public int CountryRegionId { get; set; }

        [Column("COUNTRY_TOTAL")]
        public string CountryTotal { get; set; }

        [Column("COUNTRY_TOTAL_ID")]
        public int CountryTotalId { get; set; }

        [Column("COUNTRY_NAME_HIST")]
        public string CountryNameHist { get; set; }
        public Country()
        {

        }
    }
}
