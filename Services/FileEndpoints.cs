
using FileManager.DTO;

namespace FileManager.Services
{
  public class FileEndpoints : ICommonEndPoints
  {
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
      //진입점 등록
      var group = app.MapGroup("/api/files");

      //Get 파일 목록 조회
      group.MapGet("/", async (FileService fileService) =>
      {
        var files = await fileService.GetFileListAsync<FileDTO>();
        return Results.Ok(files);
      });
    }
  }
}
