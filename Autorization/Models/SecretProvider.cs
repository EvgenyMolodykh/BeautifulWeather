using Microsoft.Extensions.Configuration;

public class SecretProvider
{
    public string YandexApiKey { get; }

    public SecretProvider()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<SecretProvider>()
            .Build();

        YandexApiKey = config["YandexApiKey"];
    }
}