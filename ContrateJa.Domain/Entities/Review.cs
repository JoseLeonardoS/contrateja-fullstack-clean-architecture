namespace ContrateJa.Domain.Entities
{
  public sealed class Review
  {
    public long Id { get; private set; }
    public long ReviewerId { get; private set; }
    public long ReviewedId { get; private set; }
    public long JobId { get; private set; }
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public DateTime SubmittedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Review() { }

    private Review(long reviewerId, long reviewedId, long jobId, int rating, string comment)
    {
      ReviewerId = reviewerId;
      ReviewedId = reviewedId;
      JobId = jobId;
      Rating = rating;
      Comment = comment;
      SubmittedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public static Review Create(long reviewerId, long reviewedId, long jobId, int rating, string comment)
    {
      if (reviewerId <= 0)
        throw new ArgumentException("Reviewer Id cannot be lower than 1.", nameof(reviewerId));

      if (reviewedId <= 0)
        throw new ArgumentException("Reviewed Id cannot be lower than 1.", nameof(reviewedId));

      if (jobId <= 0)
        throw new ArgumentException("Job Id cannot be lower than 1.", nameof(jobId));

      if (rating < 1 || rating > 5)
        throw new ArgumentException("Rating cannot be lower than 1 or greater than 5.", nameof(rating));

      if (string.IsNullOrWhiteSpace(comment))
        throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));

      comment = comment.Trim();

      if (comment.Length > 1000)
        throw new ArgumentException("Commnet is too long.", nameof(comment));

      return new Review(reviewerId, reviewedId, jobId, rating, comment);
    }

    public void EditReview(int rating, string commnet)
    {
      // TODO: lógica para editar review
    }

    public void ChangeRating()
    {
      // TODO: lógica para editar rating
    }

    public void EditComment(string newComment)
    {
      if (string.IsNullOrEmpty(newComment))
        throw new ArgumentException("Commnet cannot be null or emtpy.", nameof(newComment));

      newComment = newComment.Trim();

      if (newComment.Length > 1000)
        throw new ArgumentException("Commnet is too long.", nameof(newComment));

      if (Comment == newComment)
        return;

      Comment = newComment;
      Touch();
    }

    private void Touch()
      => UpdatedAt = DateTime.UtcNow;
  }
}