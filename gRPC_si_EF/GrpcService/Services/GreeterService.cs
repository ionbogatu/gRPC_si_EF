using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override Task<ResultString> GetFirstPostDescription(EmptyRequest request, ServerCallContext context)
        {
            DbService dbs = new DbService();

            return Task.FromResult(new ResultString
            {
                Message = dbs.GetPosts()[0].Description
            });
        }

        public override Task<ResultPosts> GetAllPosts(EmptyRequest request, ServerCallContext context)
        {
            DbService dbs = new DbService();

            var result = new ResultPosts();

            List<Post> posts = dbs.GetPosts();
            foreach(Post p in posts)
            {
                result.Message.Add(new PostModel{
                    PostId = p.PostId.ToString(),
                    Description = p.Description,
                    Domain = p.Domain
                });
            }

            return Task.FromResult(result);
        }
    }
}
