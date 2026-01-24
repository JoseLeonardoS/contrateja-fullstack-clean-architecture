using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities
{
  public class FreelancerArea
  {
    public int FreelancerId { get; set; }
    public Area Area { get; set; }

    public FreelancerArea(int freelancerId, Area area)
    {
      FreelancerId = freelancerId;
      Area = area;
    }
  }
}