using ContrateJa.Application.Abstractions.Repositories;

namespace ContrateJa.Application.UseCases.Jobs.GetJobById;

public sealed class GetJobByIdHandler
{
    private readonly IJobRepository _jobRepository;
    
    public GetJobByIdHandler(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public async Task Handle(GetJobByIdQuery query, CancellationToken ct = default)
    {
        if(query is null)
            throw new ArgumentNullException(nameof(query));

        if (query.Id <= 0)
            throw new ArgumentOutOfRangeException(nameof(query.Id));

        var job = await _jobRepository.GetById(query.Id, ct);
        
        if(job is null)
            throw new InvalidOperationException("Job not found.");
    }
}