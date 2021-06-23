namespace glitchserver.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int BugId { get; set; }
        public int CreatorId { get; set; }
    }
}