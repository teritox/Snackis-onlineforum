using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Models
{
    public class SiteTitle
    {
        public int Id { get; set; }

        [Display(Name = "Huvudtitel")]
        public string MainTitle { get; set; }
    }
}
