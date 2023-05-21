using System.Drawing;
using ImageProcessor.Imaging.Formats;

namespace Infrastructure.FileManagement.FileSettings;

public abstract class BaseFileSettings
{
    protected ISupportedImageFormat Format;

    protected Size Size;

    public BaseFileSettings(ISupportedImageFormat format, Size size)
    {
        Format = format;
        Size = size;
    }

    public Size GetSize() => Size;

    public ISupportedImageFormat GetFormat() => Format;
}
