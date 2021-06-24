using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Post_Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Post_Service.Logic
{
	public class PostHandler
	{
		private readonly IServiceProvider serviceProvider;
		public PostHandler(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public async void DeleteAllPostsByUser(string guid)
		{
			using (var scope = serviceProvider.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				try
				{
					var result = await context.Post.Where(x => x.userId.ToString() == guid).ToListAsync();
					if (result.Count > 0)
					{
						context.RemoveRange(result);
						await context.SaveChangesAsync();
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}
	}
}
