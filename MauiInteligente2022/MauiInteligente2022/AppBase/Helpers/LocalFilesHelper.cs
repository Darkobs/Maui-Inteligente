namespace MauiInteligente2022.AppBase.Helpers;

public class LocalFilesHelper
{
    static readonly string DEFAULT_PATH = FileSystem.AppDataDirectory;

    public async Task SaveFileAsync(string name, byte[] content)
    {
        var filePath = Path.Combine(DEFAULT_PATH, name);
        await File.WriteAllBytesAsync(filePath, content);
    }

    public async Task SaveFileAsync(string name, string content)
    {
        var filePath = Path.Combine(DEFAULT_PATH, name);
        await File.WriteAllTextAsync(filePath, content);
    }

    public async Task<byte[]> ReadFileAsync(string name)
    {
        var filePath = Path.Combine(DEFAULT_PATH, name);
        if(!File.Exists(filePath))
        {
            return null;
        }

        return await File.ReadAllBytesAsync(filePath);
    }

    public async Task<string> ReadTextFileAsync(string name)
    {
        var filePath = Path.Combine(DEFAULT_PATH, name);
        if (!File.Exists(filePath))
        {
            return null;
        }

        return await File.ReadAllTextAsync(filePath);
    }

    public void DeleteFile(string name)
    {
        var filePath = Path.Combine(DEFAULT_PATH, name);

        if(File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
