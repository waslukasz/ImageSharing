using System.Drawing;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Processors;

namespace Infrastructure.FileManagement;

public class ThumbnailSettings
{
    private ISupportedImageFormat _format;

    private Size _size;

    public ThumbnailSettings(ISupportedImageFormat format, Size size)
    {
        _format = format;
        _size = size;
    }

    public ThumbnailSettings()
    {
        _format = new JpegFormat() { Quality = 70 };
        _size = new Size(200, 200);
    }

    public Size GetSize() => _size;

    public ISupportedImageFormat GetFormat() => _format;
}