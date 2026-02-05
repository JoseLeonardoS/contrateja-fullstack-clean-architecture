using ContrateJa.Domain.Entities;
using ContrateJa.Application.Abstractions.Repositories;
namespace ContrateJa.Application.UseCases.CompletedJobs.CompleteJob
{
    public sealed class CompleteJobHandler
    {
        private readonly ICompletedJobRepository _completedJobRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompleteJobHandler(
            ICompletedJobRepository completedJobRepository,
            IUnitOfWork unitOfWork)
        {
            _completedJobRepository = completedJobRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(CompletedJobCommand command, CancellationToken ct = default)
        {
            if (await _completedJobRepository.ExistsByJobId(command.JobId, ct))
                throw new InvalidOperationException($"Job already completed");

            var completedJob = CompletedJob.Create(
                command.JobId,
                command.FreelancerId,
                command.ContractorId);

            await _completedJobRepository.Add(completedJob, ct);
            await _unitOfWork.SaveChanges(ct);
        }
    }
}