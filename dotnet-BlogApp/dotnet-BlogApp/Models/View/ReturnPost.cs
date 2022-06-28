namespace dotnet_BlogApp.Models.View
{
    public class ReturnPost
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public bool IsPrivate { get; set; } = true;

        public string AuthorAlias { get; set; } = "Anonymous";
    }
}
