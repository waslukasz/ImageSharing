using Infrastructure.Dto;
using Microsoft.VisualBasic;

namespace Infrastructure.Utility;

public class UniqueFileNameAssigner : ISlugCreator
{
    public const char FileNameSeparator = '-';
    public string CreateSlug(params string[] strings)
    {

        string mergedFileName = String.Join(FileNameSeparator, strings);

        mergedFileName = ReplaceSpacesWithSeparator(mergedFileName);
        mergedFileName = ReplaceInvalidChars(mergedFileName);

        return mergedFileName;
    }
    
    private string ReplaceInvalidChars(string filename)
    {
        return string.Join(FileNameSeparator, filename.Split(Path.GetInvalidFileNameChars()));    
    }

    private string ReplaceSpacesWithSeparator(string filename)
    {
        return String.Join(FileNameSeparator,filename.Split(' '));
    }
}