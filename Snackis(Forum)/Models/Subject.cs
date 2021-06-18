using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Display(Name = "Kategori")]
        public int SitetitleId { get; set; }
        public string Name { get; set; }
    }
}
