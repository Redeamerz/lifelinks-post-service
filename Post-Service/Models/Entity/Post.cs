using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post_Service.Models.Entity
{
	public class Post
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; } = Guid.NewGuid();

		[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
		public DateTime DateTime { get; set; } = DateTime.Now;

		public Guid userId { get; set; }
		public string postContent { get; set; }
		public string username { get; set; }
		public long likes { get; set; } = 0;
	}
}