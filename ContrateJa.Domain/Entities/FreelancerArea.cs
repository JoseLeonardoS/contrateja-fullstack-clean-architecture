using ContrateJa.Domain.Primitives;
using ContrateJa.Domain.ValueObjects;

namespace ContrateJa.Domain.Entities;

public sealed class FreelancerArea : Entity
{
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
        if (freelancerId <= 0)
            throw new ArgumentOutOfRangeException(nameof(freelancerId));

        if (area is null)
            throw new ArgumentNullException(nameof(area));

        return new FreelancerArea(freelancerId, area);
    }

    public void ChangeArea(Area newArea)
    {
        if (newArea is null)
            throw new ArgumentNullException(nameof(newArea));

        if (Area.Equals(newArea))
            return;

        Area = newArea;
        Touch();
    }
}