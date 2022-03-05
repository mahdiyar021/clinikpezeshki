using Microsoft.Extensions.Configuration;
using System.IO;

public static class AppSettingConfiguration
{
    public static IConfigurationRoot GetRoot()
    {
        var Config = new ConfigurationBuilder();

        var path = GetAppSettingPath();
        
        Config.AddJsonFile(path);

        return Config.Build();
    }
    public static string GetAppSettingPath()

    =>Path.Combine(Directory.GetCurrentDirectory(),AppSettingConsts.AppSettingFileName);
    

}

