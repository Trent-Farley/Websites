namespace Proj.Models
{
    public class Commit
    {
        public string Sha { get; set; }
        public string AuthorName { get; set; }
        public string ShaUrl { get; set; }
        public string CommitMessage { get; set; }
        public string Timestamp { get; set; }
    }
}