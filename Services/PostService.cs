using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Data;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
        //private readonly List<Post> _post;
        //public PostService()
        //{
        //    _post = new List<Post>();
        //    for (var i = 0; i < 5; i++)
        //    {
        //        _post.Add(new Post { Id = Guid.NewGuid(),Name=i.ToString() });
        //    }
        //}

        private readonly Data.DbContext _dataContext;

        public PostService(Data.DbContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _dataContext.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            await _dataContext.Posts.AddAsync(post);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;

        }
        public async Task<List<Post>> GetPostsAsync()
        {
            return await _dataContext.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _dataContext.Posts.Update(postToUpdate);
            var updated = await _dataContext.SaveChangesAsync();
            return updated > 0;
            
                   
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post =await GetPostByIdAsync(postId);
            if (post == null)
                return false;
            _dataContext.Posts.Remove(post);
            var deleted = await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}
