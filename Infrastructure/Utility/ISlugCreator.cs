namespace Infrastructure.Utility;

public interface ISlugCreator
{
    public string CreateSlug(params string[] strings);
}