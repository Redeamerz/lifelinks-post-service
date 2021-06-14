using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Posting_Service.Models;
using Posting_Service.SessionFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NHibernate.Linq;
namespace Posting_Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PostController : ControllerBase
	{
		FluentNHibernateHelper fluentNHibernateHelper;

		public PostController(IConfiguration configuration)
		{
			fluentNHibernateHelper = new FluentNHibernateHelper(configuration);
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			List<Post> postList;
			try
			{
				using (var session = fluentNHibernateHelper.OpenSession())
				{
					postList = await session.Query<Post>().ToListAsync();
				}
				return Ok(postList);
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpGet("GetUserPosts")]
		public async Task<IActionResult> Get(string uid)
		{
			List<Post> postList;
			try
			{
				using (var session = fluentNHibernateHelper.OpenSession())
				{
					postList = await session.Query<Post>().Where(x => x.userId == uid).ToListAsync();
				}
				return Ok(postList);
			}
			catch
			{
				return BadRequest();
			}
		}

		// POST: api/Account
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Post post)
		{
			post.id = Guid.NewGuid().ToString();
			post.likes = 0;
			try
			{
				using (var session = fluentNHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						await session.SaveOrUpdateAsync(post);
						transaction.Commit();
					}
				}
				return Created("lifelinks", post);
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateMessage([FromBody] Post post)
		{
			try
			{
				using (var session = fluentNHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						await session.SaveOrUpdateAsync(post);
						transaction.Commit();
					}
				}
				return NoContent();
			}
			catch
			{
				return BadRequest();
			}
		}

		[HttpPut("likes")]
		public async Task<IActionResult> UpdateLikes([FromBody] Post post)
		{
			try
			{
				using (var session = fluentNHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						var currentLikes = await session.GetAsync<Post>(post.id);
						currentLikes.likes++;
						await session.SaveOrUpdateAsync(currentLikes);
						transaction.Commit();
					}
				}
				return NoContent();
			}
			catch
			{
				return BadRequest();
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete]
		public async Task<IActionResult> Delete(Post post)
		{
			try
			{
				using (var session = fluentNHibernateHelper.OpenSession())
				{
					using (var transaction = session.BeginTransaction())
					{
						await session.DeleteAsync(post);
						transaction.Commit();
					}
				}
				return NoContent();
			}
			catch
			{
				return BadRequest();
			}

		}
	}
}
