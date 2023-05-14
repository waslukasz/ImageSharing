using Infrastructure.Dto;
using Microsoft.VisualBasic;

namespace Infrastructure.Utility;

public class UniqueFileNameAssigner
{
    public static char FileNameSeparator = '-';
    public string RenameFile(FileDto file)
    {
        string title = file.Title;
        string name = file.Name;
        string mergedFileName = title + FileNameSeparator + name;

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