namespace ContrateJa.Domain.Entities
{
  public class Review
  {
    public int Id { get; set; }
    public int ReviewerId { get; set; }
    public int ReviewedId { get; set; }
    public int JobId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime SubmitedAt { get; set; }

    public Review(int reviewerId, int reviewedId, int jobId, int rating, string comment)
    {
      ReviewerId = reviewerId;
      ReviewedId = reviewedId;
      JobId = jobId;
      Rating = rating;
      Comment = comment;
    }
  }
}