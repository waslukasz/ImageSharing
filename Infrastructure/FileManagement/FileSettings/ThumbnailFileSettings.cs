using System.Drawing;
using ImageProcessor.Imaging.Formats;

namespace Infrastructure.FileManagement.FileSettings;

public class ThumbnailFileSettings : BaseFileSettings
{
    public ThumbnailFileSettings() : base(new JpegFormat() { Quality = 70 },new Size(200, 200))
    {
        
    }
}