using ContrateJa.Domain.Entities;

namespace ContrateJa.Tests.Domain.Entities
{
    public sealed class ReviewTests
  {
    private static DateTime WaitUntilAfter(DateTime baseline, int timeoutMs = 200)
    {
      var start = DateTime.UtcNow;
      while (DateTime.UtcNow <= baseline)
      {
        if ((DateTime.UtcNow - start).TotalMilliseconds > timeoutMs)
          break;

        Thread.SpinWait(50);
      }
      return DateTime.UtcNow;
    }

    public static IEnumerable<object[]> InvalidIds()
    {
      yield return new object[] { 0L };
      yield return new object[] { -1L };
      yield return new object[] { -999L };
    }

    public static IEnumerable<object[]> InvalidRatings()
    {
      yield return new object[] { 0 };
      yield return new object[] { -1 };
      yield return new object[] { 6 };
      yield return new object[] { 999 };
    }

    public static IEnumerable<object[]> InvalidComments()
    {
      yield return new object[] { "" };
      yield return new object[] { " " };
      yield return new object[] { "   " };
      yield return new object[] { "\t" };
      yield return new object[] { "\n" };
      yield return new object[] { "\r\n" };
    }

    [Fact]
    public void Create_WhenValid_SetsFieldsAndTimestamps()
    {
      var review = Review.Create(1, 2, 10, 5, "Great job");

      Assert.Equal(1, review.ReviewerId);
      Assert.Equal(2, review.ReviewedId);
      Assert.Equal(10, review.JobId);
      Assert.Equal(5, review.Rating);
      Assert.Equal("Great job", review.Comment);
      Assert.NotEqual(default, review.SubmittedAt);
      Assert.NotEqual(default, review.UpdatedAt);
    }

    [Fact]
    public void Create_TrimsComment()
    {
      var review = Review.Create(1, 2, 10, 5, "  Great job  ");

      Assert.Equal("Great job", review.Comment);
    }

    [Theory]
    [MemberData(nameof(InvalidIds))]
    public void Create_WithInvalidReviewerId_Throws(long reviewerId)
    {
      var ex = Assert.Throws<ArgumentException>(() => Review.Create(reviewerId, 2, 10, 5, "Ok"));
      Assert.Equal("reviewerId", ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(InvalidIds))]
    public void Create_WithInvalidReviewedId_Throws(long reviewedId)
    {
      var ex = Assert.Throws<ArgumentException>(() => Review.Create(1, reviewedId, 10, 5, "Ok"));
      Assert.Equal("reviewedId", ex.ParamName);
    }

    [Fact]
    public void Create_WhenReviewerEqualsReviewed_Throws()
    {
      var ex = Assert.Throws<ArgumentException>(() => Review.Create(1, 1, 10, 5, "Ok"));
      Assert.Equal("reviewerId", ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(InvalidIds))]
    public void Create_WithInvalidJobId_Throws(long jobId)
    {
      var ex = Assert.Throws<ArgumentException>(() => Review.Create(1, 2, jobId, 5, "Ok"));
      Assert.Equal("jobId", ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(InvalidRatings))]
    public void Create_WithInvalidRating_Throws(int rating)
    {
      var ex = Assert.Throws<ArgumentException>(() => Review.Create(1, 2, 10, rating, "Ok"));
      Assert.Equal("rating", ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(InvalidComments))]
    public void Create_WithBlankComment_Throws(string comment)
    {
      var ex = Assert.Throws<ArgumentException>(() => Review.Create(1, 2, 10, 5, comment));
      Assert.Equal("comment", ex.ParamName);
    }

    [Fact]
    public void Create_WithTooLongComment_Throws()
    {
      var comment = new string('a', 1001);

      var ex = Assert.Throws<ArgumentException>(() => Review.Create(1, 2, 10, 5, comment));
      Assert.Equal("comment", ex.ParamName);
    }

    [Fact]
    public void EditReview_WhenValid_UpdatesRatingCommentAndUpdatedAt()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var before = review.UpdatedAt;

      WaitUntilAfter(before);

      review.EditReview(5, "  Great  ");

      Assert.Equal(5, review.Rating);
      Assert.Equal("Great", review.Comment);
      Assert.True(review.UpdatedAt > before);
    }

    [Fact]
    public void EditReview_WhenNoChanges_DoesNotUpdateUpdatedAt()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var before = review.UpdatedAt;

      review.EditReview(3, "Ok");

      Assert.Equal(before, review.UpdatedAt);
    }

    [Theory]
    [MemberData(nameof(InvalidRatings))]
    public void EditReview_WithInvalidRating_Throws(int rating)
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");

      var ex = Assert.Throws<ArgumentException>(() => review.EditReview(rating, "Ok"));
      Assert.Equal("rating", ex.ParamName);
    }

    [Theory]
    [MemberData(nameof(InvalidComments))]
    public void EditReview_WithBlankComment_Throws(string comment)
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");

      var ex = Assert.Throws<ArgumentException>(() => review.EditReview(3, comment));
      Assert.Equal("comment", ex.ParamName);
    }

    [Fact]
    public void EditReview_WithTooLongComment_Throws()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var comment = new string('a', 1001);

      var ex = Assert.Throws<ArgumentException>(() => review.EditReview(3, comment));
      Assert.Equal("comment", ex.ParamName);
    }

    [Fact]
    public void ChangeRating_WhenValid_UpdatesRatingAndUpdatedAt()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var before = review.UpdatedAt;

      WaitUntilAfter(before);

      review.ChangeRating(4);

      Assert.Equal(4, review.Rating);
      Assert.True(review.UpdatedAt > before);
    }

    [Fact]
    public void ChangeRating_WhenSameRating_DoesNotUpdateUpdatedAt()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var before = review.UpdatedAt;

      review.ChangeRating(3);

      Assert.Equal(before, review.UpdatedAt);
    }

    [Theory]
    [MemberData(nameof(InvalidRatings))]
    public void ChangeRating_WithInvalidRating_Throws(int rating)
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");

      var ex = Assert.Throws<ArgumentException>(() => review.ChangeRating(rating));
      Assert.Equal("rating", ex.ParamName);
    }

    [Fact]
    public void EditComment_WhenValid_UpdatesCommentAndUpdatedAt()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var before = review.UpdatedAt;

      WaitUntilAfter(before);

      review.EditComment("  Nice  ");

      Assert.Equal("Nice", review.Comment);
      Assert.True(review.UpdatedAt > before);
    }

    [Fact]
    public void EditComment_WhenSameComment_DoesNotUpdateUpdatedAt()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var before = review.UpdatedAt;

      review.EditComment("Ok");

      Assert.Equal(before, review.UpdatedAt);
    }

    [Theory]
    [MemberData(nameof(InvalidComments))]
    public void EditComment_WithBlankComment_Throws(string comment)
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");

      var ex = Assert.Throws<ArgumentException>(() => review.EditComment(comment));
      Assert.Equal("comment", ex.ParamName);
    }

    [Fact]
    public void EditComment_WithTooLongComment_Throws()
    {
      var review = Review.Create(1, 2, 10, 3, "Ok");
      var comment = new string('a', 1001);

      var ex = Assert.Throws<ArgumentException>(() => review.EditComment(comment));
      Assert.Equal("comment", ex.ParamName);
    }
  }
}