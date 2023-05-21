using System.Drawing;
using ImageProcessor.Imaging.Formats;

namespace Infrastructure.FileManagement.FileSettings;

public class ImageFileSettings : BaseFileSettings
{
    public ImageFileSettings(Size size) : base(new JpegFormat() { Quality = 100 }, size)
    {
        
    }
}