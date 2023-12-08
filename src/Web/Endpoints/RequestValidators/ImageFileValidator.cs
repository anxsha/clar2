namespace neatbook.Web.Endpoints.RequestValidators;

public static class ImageFileValidator {
  /// <summary>
  /// Validates if the file has a supported image extension (gif, png, jpeg or jpg)
  /// and if its size does not exceed the limit passed in <paramref name="maxFileSize"/>
  /// </summary>
  /// <param name="file">uploaded form file</param>
  /// <param name="maxFileSize">maximum file size in bytes</param>
  /// <returns>true if the file has a supported extension and a proper size, else false</returns>
  public static bool IsValidImage(this IFormFile file, int maxFileSize) {
    // File is 0-length or too big
    if (file is not {Length: > 0} || file.Length > maxFileSize) {
      return false;
    }

    using var reader = new BinaryReader(file.OpenReadStream());
    var signatures = _imageFileSignatures.Values.SelectMany(x => x).ToList();
    var headerBytes = reader.ReadBytes(_imageFileSignatures.Max(m => m.Value.Max(n => n.Length)));
    // Check if the uploaded file is of supported image extensions by verifying signatures
    bool fileIsImage = signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));

    return fileIsImage;
  }

  private static readonly Dictionary<string, List<byte[]>> _imageFileSignatures = new() {
    {".gif", new List<byte[]> {new byte[] {0x47, 0x49, 0x46, 0x38}}},
    {".png", new List<byte[]> {new byte[] {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A}}}, {
      ".jpeg", new List<byte[]> {
        new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
        new byte[] {0xFF, 0xD8, 0xFF, 0xE2},
        new byte[] {0xFF, 0xD8, 0xFF, 0xE3},
        new byte[] {0xFF, 0xD8, 0xFF, 0xEE},
        new byte[] {0xFF, 0xD8, 0xFF, 0xDB},
      }
    }, {
      ".jpg", new List<byte[]> {
        new byte[] {0xFF, 0xD8, 0xFF, 0xE0},
        new byte[] {0xFF, 0xD8, 0xFF, 0xE1},
        new byte[] {0xFF, 0xD8, 0xFF, 0xE8},
        new byte[] {0xFF, 0xD8, 0xFF, 0xEE},
        new byte[] {0xFF, 0xD8, 0xFF, 0xDB},
      }
    },
  };
}
