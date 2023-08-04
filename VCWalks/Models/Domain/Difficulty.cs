namespace VCWalks.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation

        public virtual ICollection<Walk> Walks { get; set; }

    }
}
