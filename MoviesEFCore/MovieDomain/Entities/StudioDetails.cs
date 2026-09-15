using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class StudioDetails
    {
        public int Id { get; set; }

        [Required]
        public string LicenseNumber { get; set; }

        //studio vs studioDetails 
        public int StudioId { get; set; }//foreign key - ანუ ეს მნიშნელობებს მიიღებს მხოლოდ studio-ს აიდიდან
        public Studio Studio { get; set; }//navigation property


    }
}
