namespace ContrateJa.Domain.Entities
{
  public sealed class CompletedJob
  {
    public long Id { get; private set; }
    public long JobId { get; private set; }
    public long FreelancerId { get; private set; }
    public long ContractorId { get; private set; }
    public DateTime CompletedAt { get; private set; }
    
    private CompletedJob() { }

    private CompletedJob(long jobId, long freelancerId, long contractorId)
    {
      JobId = jobId;
      FreelancerId = freelancerId;
      ContractorId = contractorId;
      CompletedAt = DateTime.UtcNow;
    }

    public static CompletedJob Create(long jobId, long freelancerId, long contractorId)
    {
      if(jobId <= 0)
        throw new ArgumentOutOfRangeException(nameof(jobId), "Job Id cannot be lower than 1." );
      
      if(freelancerId <= 0)
        throw new ArgumentOutOfRangeException(nameof(freelancerId), "Freelancer Id cannot be lower than 1." );
      
      if(contractorId <= 0)
        throw new ArgumentOutOfRangeException(nameof(contractorId), "Contractor Id cannot be lower than 1." );
      
      if(freelancerId == contractorId)
        throw  new ArgumentException("Freelancer and contractor must be different.");
      
      return new CompletedJob(jobId, freelancerId, contractorId);
    }
  }
}
