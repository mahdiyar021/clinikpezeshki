public class AppSetting
{
    public static string JwtSecretKey => AppSettingConfiguration.GetRoot()
                                                                .GetSection("Jwt")
                                                                .GetSection("SecretKey")
                                                                .Value;
    public static string JwtIssuer => AppSettingConfiguration.GetRoot()
                                                             .GetSection("Jwt")
                                                             .GetSection("Issuer")
                                                             .Value;
    public static string JwtAudience => AppSettingConfiguration.GetRoot()
                                                               .GetSection("Jwt")
                                                               .GetSection("Audience")
                                                               .Value;
    public static string ConnectionString => AppSettingConfiguration.GetRoot().GetConnectionString("main");

}

