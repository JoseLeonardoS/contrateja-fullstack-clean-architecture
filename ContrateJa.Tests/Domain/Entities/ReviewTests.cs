using ContrateJa.Domain.Entities;

namespace ContrateJa.Tests.Domain.Entities;

public sealed class ReviewTests
{
    private static Review CreateReview(
        long reviewerId = 1,
        long reviewedId = 2,
        long jobId = 3,
        int rating = 5,
        string comment = "Ótimo profissional.")
    {
        return Review.Create(reviewerId, reviewedId, jobId, rating, comment);
    }

    [Fact]
    public void Create_SetsTimestamps()
    {
        var before = DateTime.UtcNow;
        var review = CreateReview();
        var after = DateTime.UtcNow;

        Assert.InRange(review.CreatedAt, before, after);
        Assert.InRange(review.UpdatedAt, before, after);
        Assert.InRange(review.SubmittedAt, before, after);
        Assert.True(review.UpdatedAt >= review.CreatedAt);
    }

    [Fact]
    public void Create_WithInvalidReviewerId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(reviewerId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(reviewerId: -1));
    }

    [Fact]
    public void Create_WithInvalidReviewedId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(reviewedId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(reviewedId: -1));
    }

    [Fact]
    public void Create_WhenReviewerEqualsReviewed_Throws()
    {
        var ex = Assert.Throws<ArgumentException>(() => CreateReview(reviewerId: 1, reviewedId: 1));
        Assert.Equal("reviewerId", ex.ParamName);
    }

    [Fact]
    public void Create_WithInvalidJobId_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(jobId: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(jobId: -1));
    }

    public static IEnumerable<object[]> InvalidRatings()
    {
        yield return new object[] { 0 };
        yield return new object[] { -1 };
        yield return new object[] { 6 };
        yield return new object[] { 100 };
    }

    [Theory]
    [MemberData(nameof(InvalidRatings))]
    public void Create_WithInvalidRating_Throws(int rating)
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => CreateReview(rating: rating));
        Assert.Equal("rating", ex.ParamName);
    }

    public static IEnumerable<object[]> ValidRatings()
    {
        yield return new object[] { 1 };
        yield return new object[] { 2 };
        yield return new object[] { 3 };
        yield return new object[] { 4 };
        yield return new object[] { 5 };
    }

    [Theory]
    [MemberData(nameof(ValidRatings))]
    public void Create_WithValidRating_DoesNotThrow(int rating)
    {
        var exception = Record.Exception(() => CreateReview(rating: rating));
        Assert.Null(exception);
    }

    public static IEnumerable<object[]> NullOrWhitespaceComments()
    {
        yield return new object[] { null! };
        yield return new object[] { "" };
        yield return new object[] { "   " };
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceComments))]
    public void Create_WithNullOrWhitespaceComment_Throws(string? comment)
    {
        Assert.Throws<ArgumentException>(() => CreateReview(comment: comment!));
    }

    [Fact]
    public void Create_WithCommentExceeding1000Chars_Throws()
    {
        Assert.Throws<ArgumentException>(() => CreateReview(comment: new string('a', 1001)));
    }

    [Fact]
    public void EditReview_WhenDifferent_UpdatesAndUpdatedAt()
    {
        var review = CreateReview();
        var oldUpdatedAt = review.UpdatedAt;

        review.EditReview(3, "Bom profissional.");

        Assert.Equal(3, review.Rating);
        Assert.Equal("Bom profissional.", review.Comment);
        Assert.True(review.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void EditReview_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var review = CreateReview();
        var oldUpdatedAt = review.UpdatedAt;

        review.EditReview(review.Rating, review.Comment);

        Assert.Equal(oldUpdatedAt, review.UpdatedAt);
    }

    [Theory]
    [MemberData(nameof(InvalidRatings))]
    public void EditReview_WithInvalidRating_Throws(int rating)
    {
        var review = CreateReview();
        Assert.Throws<ArgumentOutOfRangeException>(() => review.EditReview(rating, "Bom profissional."));
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceComments))]
    public void EditReview_WithNullOrWhitespaceComment_Throws(string? comment)
    {
        var review = CreateReview();
        Assert.Throws<ArgumentException>(() => review.EditReview(3, comment!));
    }

    [Theory]
    [MemberData(nameof(InvalidRatings))]
    public void ChangeRating_WithInvalidRating_Throws(int rating)
    {
        var review = CreateReview();
        Assert.Throws<ArgumentOutOfRangeException>(() => review.ChangeRating(rating));
    }

    [Fact]
    public void ChangeRating_WhenDifferent_UpdatesRatingAndUpdatedAt()
    {
        var review = CreateReview(rating: 5);
        var oldUpdatedAt = review.UpdatedAt;

        review.ChangeRating(3);

        Assert.Equal(3, review.Rating);
        Assert.True(review.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ChangeRating_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var review = CreateReview(rating: 5);
        var oldUpdatedAt = review.UpdatedAt;

        review.ChangeRating(5);

        Assert.Equal(oldUpdatedAt, review.UpdatedAt);
    }

    [Theory]
    [MemberData(nameof(NullOrWhitespaceComments))]
    public void EditComment_WithNullOrWhitespaceComment_Throws(string? comment)
    {
        var review = CreateReview();
        Assert.Throws<ArgumentException>(() => review.EditComment(comment!));
    }

    [Fact]
    public void EditComment_WithCommentExceeding1000Chars_Throws()
    {
        var review = CreateReview();
        Assert.Throws<ArgumentException>(() => review.EditComment(new string('a', 1001)));
    }

    [Fact]
    public void EditComment_WhenDifferent_UpdatesCommentAndUpdatedAt()
    {
        var review = CreateReview();
        var oldUpdatedAt = review.UpdatedAt;

        review.EditComment("Novo comentário.");

        Assert.Equal("Novo comentário.", review.Comment);
        Assert.True(review.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void EditComment_WhenSame_DoesNotUpdateUpdatedAt()
    {
        var review = CreateReview();
        var oldUpdatedAt = review.UpdatedAt;

        review.EditComment(review.Comment);

        Assert.Equal(oldUpdatedAt, review.UpdatedAt);
    }
}