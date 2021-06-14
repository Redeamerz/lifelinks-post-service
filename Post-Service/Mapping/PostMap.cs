using Posting_Service.Models;
using FluentNHibernate.Mapping;

namespace Posting_Service.Mapping
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Table("Post");
            Id(x => x.id).Column("Id");
            Map(x => x.userId).Column("UserId");
            Map(x => x.postContent).Column("PostContent");
            Map(x => x.username).Column("Username");
            Map(x => x.likes).Column("Likes");
        }
    }
}
