namespace EasyTime.Application.Contract.Dtos.Comments
{
    public record CommentDto
    {
        public long BusinessId { get; set; }
        public Guid UserId { get; set; }
        public long CommentId { get; set; }
        public string CommentText { get; set; } = string.Empty;
    }
}
