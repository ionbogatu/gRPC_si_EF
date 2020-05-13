using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService
{
    public class DbService
    {

        readonly Context Context;

        public DbService()
        {
            this.Context = new Context();

            if (!this.Context.Posts.Any()) this.InitDb();
        }

        /** API Query **/


        public List<Post> GetPosts()
        {
            return GetFilteredPosts(f => true);
        }

        public List<Comment> GetComments()
        {
            return GetFilteredComments(d => true);
        }


        public List<Post> GetFilteredPosts(Func<Post, bool> filter)
        {
            return Context.Posts.Where(filter).ToList();
        }
        public List<Comment> GetFilteredComments(Func<Comment, bool> filter)
        {
            return Context.Comments.Where(filter).ToList();
        }




        /** Seed and Setup **/

        private Tuple<List<Post>, List<Comment>> Seed()
        {
            List<Post> posts = new List<Post>
            {

                new Post("Description-1", "Domain-1"),
                new Post("Description-2", "Domain-2"),
            };

            List<Comment> comments = new List<Comment>
            {
                new Comment("text-1-1",posts[0].PostId),
                new Comment("text-1-2",posts[0].PostId),
                new Comment("text-2-1",posts[1].PostId),
            };



            return new Tuple<List<Post>, List<Comment>>(posts, comments);
        }

        public void InitDb()
        {
            ClearDb();
            Tuple<List<Post>, List<Comment>> seed = Seed();
            foreach (Post p in seed.Item1)
                Context.Posts.Add(p);
            foreach (Comment c in seed.Item2)
                Context.Comments.Add(c);

            Context.SaveChanges();

        }

        private void ClearDb()
        {
            Context.Comments.RemoveRange(Context.Comments);
            Context.Posts.RemoveRange(Context.Posts);
            Context.SaveChanges();
        }



    }
}
