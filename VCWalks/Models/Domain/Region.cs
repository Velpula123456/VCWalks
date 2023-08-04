namespace VCWalks.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }

        //Navigation

        public virtual ICollection<Walk> Walks { get; set; }
    }
}
