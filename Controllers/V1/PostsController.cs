using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts.V1;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers.V1
{
    public class PostsController :Controller
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        //[HttpGet("api/v1/posts")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _postService.GetPostsAsync());
        }


        [HttpGet(ApiRoutes.Posts.Get)]
        //[HttpGet("api/v1/posts")]
        public async Task<IActionResult> Get([FromRoute]Guid postId)
        {
            var post = await _postService.GetPostByIdAsync(postId);
            if (post==null)
            {
                return NotFound();
            }
            return Ok(post);
        }


        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            var posts = new Post();
            if (post.Name != null)
            {
                posts.Name = post.Name;

            }
            await _postService.CreatePostAsync(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}",post.Id.ToString());
            return Created(locationUrl,post);
        }

        [HttpPut(ApiRoutes.Posts.Update)]
        //[HttpGet("api/v1/posts")]
        public async Task<IActionResult> Update([FromRoute] Guid postId,[FromBody] string request)
        {
            var post = new Post 
            { 
                Id = postId,
                Name= request.ToString()
            };

            var updated =await _postService.UpdatePostAsync(post);

            if (!updated)
                return NotFound();

            return Ok(post);
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid postId)
        {
            var delete = await _postService.DeletePostAsync(postId);
            if (!delete)
                return NoContent();

            return NotFound();

        }
    }
}
