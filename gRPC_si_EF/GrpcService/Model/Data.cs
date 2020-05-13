using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GrpcService
{
    public class Data {
    }

    [DataContract]
    public class Post
    {
        [DataMember]
        public Guid PostId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Domain { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public List<Comment> Comments { get; set; }

        public Post()
        {
            this.PostId = Guid.NewGuid();
        }

        public Post(string description, string domain, Nullable<DateTime> date = null)
        {
            this.PostId = Guid.NewGuid();
            this.Description = description;
            this.Domain = domain;
            this.Date = date ?? DateTime.Now;

        }

        public override string ToString()
        {
            return $"Post {this.PostId}: -description: {this.Description} -domain: {this.Domain}\n";
        }
    }

    [DataContract]
    public class Comment
    {
        [DataMember]
        public Guid CommentId { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public Guid PostId { get; set; }

        [DataMember]
        public Post Post { get; set; }

        public Comment()
        {
            this.CommentId = Guid.NewGuid();
        }

        public Comment(string text, Guid postId)
        {
            this.CommentId = Guid.NewGuid();
            this.Text = text;
            this.PostId = postId;
        }

        public override string ToString()
        {
            return $"Comment {this.CommentId}: -text: {this.Text} -postId: {this.PostId}\n";
        }
    }
}
