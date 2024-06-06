namespace Ekip2.Presentation.Extentions;

public static class FormFileExtention
{
    public static async Task<byte[]> StringToByteArrayAsync(this IFormFile formFile)
    {
        using MemoryStream memory = new MemoryStream();
        await formFile.CopyToAsync(memory);
        return memory.ToArray();
    }
}
