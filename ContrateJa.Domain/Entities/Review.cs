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
      
      if(reviewerId == reviewedId)
        throw new ArgumentException("Reviewer cannot rate himself.", nameof(reviewerId));

      if (jobId <= 0)
        throw new ArgumentException("Job Id cannot be lower than 1.", nameof(jobId));

      rating = ValidateRating(rating);

      comment = NormalizeAndValidateComment(comment);

      return new Review(reviewerId, reviewedId, jobId, rating, comment);
    }

    public void EditReview(int rating, string comment)
    {
      rating = ValidateRating(rating);
      comment = NormalizeAndValidateComment(comment);

      if (Rating == rating && Comment == comment)
        return;
      
      Rating =  rating;
      Comment = comment;
      Touch();
    }

    public void ChangeRating(int rating)
    {
      rating = ValidateRating(rating);

      if (Rating == rating) return;
      
      Rating = rating;
      Touch();
    }

    public void EditComment(string comment)
    {
      comment = NormalizeAndValidateComment(comment);

      if (Comment == comment)
        return;

      Comment = comment;
      Touch();
    }

    private static int ValidateRating(int rating)
    {
      if (rating < 1 || rating > 5)
        throw new ArgumentException("Rating cannot be lower than 1 or greater than 5.", nameof(rating));

      return rating;
    }
    
    private static string NormalizeAndValidateComment(string comment)
    {
      if (string.IsNullOrWhiteSpace(comment))
        throw new ArgumentException("Comment cannot be null or empty.", nameof(comment));

      comment = comment.Trim();

      if (comment.Length > 1000)
        throw new ArgumentException("Comment is too long.", nameof(comment));

      return comment;
    }

    private void Touch()
      => UpdatedAt = DateTime.UtcNow;
  }
}