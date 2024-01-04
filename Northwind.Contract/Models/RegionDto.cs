using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contract.Models
{
    public class RegionDto
    {
        [Required(ErrorMessage = "Region ID Is Required")]
        public int RegionId { get; set; }

        [Required(ErrorMessage = "Region Description Is Required")]
        public string? RegionDescription { get; set; }
    }
}