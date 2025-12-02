namespace LiteraVerseApi.DTOs;

public class UserProfileResponse
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
    public int StoriesCount { get; set; }
    public int PublishedStoriesCount { get; set; }
    public int TotalViews { get; set; }
    public int FavoritesCount { get; set; }
}