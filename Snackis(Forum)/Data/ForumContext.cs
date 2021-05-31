using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Snackis_Forum_.Data
{
    public class ForumContext : DbContext
    {
        public ForumContext (DbContextOptions<ForumContext> options)
            : base(options)
        {
        }

        public DbSet<Models.SiteTitle> SiteTitle { get; set; }
        public DbSet<Models.Post> Posts { get; set; }
        public DbSet<Models.PrivateMessage> PrivateMessages { get; set; }
        public DbSet<Models.Subject> Subjects { get; set; }
        public DbSet<Models.SubjectThread> SubjectThreads { get; set; }
    }
}
