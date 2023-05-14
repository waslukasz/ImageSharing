using LiteX.Storage.FileSystem;

namespace WebAPI.Configuration;

public static class LiteXStorageConfiguration
{
    public static void ConfigureLiteX(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddLiteXFileSystemStorageService(fssc =>
        {
            fssc.Directory = "Uploads";
        });
    }
}