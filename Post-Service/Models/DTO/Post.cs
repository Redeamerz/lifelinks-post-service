using System;

namespace Posting_Service.Models
{
	public class Post
	{
		public Guid userId { get; set; }
		public string postContent { get; set; }
		public string username { get; set; }
		public long likes { get; set; }
	}
}