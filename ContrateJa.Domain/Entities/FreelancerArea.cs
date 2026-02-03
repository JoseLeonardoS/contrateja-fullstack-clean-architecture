using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities
{
  public sealed class FreelancerArea
  {
    public long Id { get; private set; }
    public long FreelancerId { get; private set; }
    public Area Area { get; private set; }
    
    private FreelancerArea() { }

    private FreelancerArea(long freelancerId, Area area)
    {
      FreelancerId = freelancerId;
      Area = area;
    }

    public static FreelancerArea Create(long freelancerId, Area area)
    {
      if(freelancerId <= 0)
        throw new ArgumentOutOfRangeException(nameof(freelancerId));
      
      if(area == null)
        throw new ArgumentNullException(nameof(area));
      
      return new FreelancerArea(freelancerId, area);
    }
  }
}