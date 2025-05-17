using System.Text.Json;

using CSharp.Book.Downloader.Lib.Config;
using CSharp.Book.Downloader.Lib.Interfaces;

namespace CSharp.Book.Downloader.Lib.Core;

public class ConfigLoader : IConfigLoader {
    private readonly JsonSerializerOptions jsonOptions = new() {
        PropertyNameCaseInsensitive = true,
    };

    public DownloadClientConfig? Config { get; private set; }

    public DownloadClientConfig LoadConfig(Stream stream) {
        StreamReader reader = new(stream);
        string configJson = reader.ReadToEnd();
        return ReadConfig(configJson);
    }

    public DownloadClientConfig LoadConfig(string filePath) {
        string configJson = File.ReadAllText(filePath);
        return ReadConfig(configJson);
    }

    private DownloadClientConfig ReadConfig(string json) {
        DownloadClientConfig? config =
            JsonSerializer.Deserialize<DownloadClientConfig>(json, jsonOptions)
            ?? throw new FormatException($"Unable to load the provided configuration");

        Config = config;
        return config;
    }
}
