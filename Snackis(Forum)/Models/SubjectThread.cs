using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Models
{
    public class SubjectThread
    {
        public int Id { get; set; }
        public string TreadTitle { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LatestPost { get; set; }
        public string AuthorId { get; set; }
        public int SubjectId { get; set; }
    }
}
