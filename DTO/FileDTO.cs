using System.Text.Json.Serialization;

namespace FileManager.DTO
{
  /// <summary>
  /// 파일 전달 DTO
  /// </summary>
  public record class FileDTO
  {
    public long FileNo { get; set; }
    public string? FileKind { get; set; }
    public string? FileNm { get; set; }
    public byte[]? FileData { get; set; }
    public int FileSize { get; set; }
    public string? EntryId { get; set; }
    public string? EntryDt { get; set; }
    
    public override string ToString()
    {
      return $"FileNo:{FileNo}, FileKind:{FileKind}, FileNm:{FileNm}, FileSize:{FileSize}, EntryId:{EntryId}, EntryDt:{EntryDt}";
    }
  }
}
