using Microsoft.AspNetCore.Mvc;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snackis_Forum_.Controller
{
    //If called with an id this API will return the post with that id.

    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly IForumDataService _ds;
        public DiscussionController(IForumDataService ds)
        {
            _ds = ds;
        }

        //GET api/<DiscussionController>/5
        [HttpGet("{id}")]

        public async Task<IEnumerable<Post>> GetAsync(int id)
        {
            var posts = await _ds.GetForumPosts(id);

            if(posts == null)
            {
                return (IEnumerable<Post>)NotFound();
            }

            return posts;
        }
    }
}
