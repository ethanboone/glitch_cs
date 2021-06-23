namespace glitchserver.Models
{
    public class Bug
    {
        public int Id { get; set; }
        public bool Closed { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string CloseDate { get; set; }
        public int CreatorId { get; set; }
    }
}