using System;

namespace transactions_api.Dto
{
    public class CommentReadDto
    {
        public int CommentId { get; set; }
        public DateTime DateAdded { get; set; }
        public int TransactionId { get; set; }
        public string Text { get; set; }
    }
}