using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Posting_Service.Models
{
    public class Post
    {
        public virtual string id { get; set; }
        public virtual string userId { get; set; }
        public virtual string postContent { get; set; }
        public virtual string username { get; set; }
        public virtual long likes { get; set; }

    }
}
