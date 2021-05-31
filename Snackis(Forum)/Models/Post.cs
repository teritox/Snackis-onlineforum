using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime PostDate { get; set; }
        public bool Reported { get; set; }
        public int ThreadId { get; set; }
        public int AnswerToId { get; set; }
    }
}
