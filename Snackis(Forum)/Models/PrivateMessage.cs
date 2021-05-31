using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }
        public string MessageTitle { get; set; }
        public string Message { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }
}
