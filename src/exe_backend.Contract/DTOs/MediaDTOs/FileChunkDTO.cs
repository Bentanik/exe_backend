namespace exe_backend.Contract.DTOs.MediaDTOs;

public class FileChunkDTO
{
    public byte[] ChunkData { get; set; }  // The data of the current chunk (part of the file)
    public string FileName { get; set; }   // The name of the file (e.g., "image.jpg")
    public int ChunkIndex { get; set; }    // The index of the chunk (e.g., 1, 2, 3, ...)
    public int TotalChunks { get; set; }   // The total number of chunks for the file (the number of parts)
    public long FileSize { get; set; }     // The total size of the file (in bytes)
    public string FileChecksum { get; set; } = default!;  // A checksum for integrity verification (e.g., hash of the entire file)
    public string FileExtension { get; set; } = default!; // The file's extension (e.g., .jpg, .mp4, .txt)
    public string FileIdentifier { get; set; } = default!;  // A unique identifier for the file (helps differentiate files)
}
