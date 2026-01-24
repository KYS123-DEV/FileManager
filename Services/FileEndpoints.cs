using FileManager.DTO;

namespace FileManager.Services
{
  public class FileEndpoints : ICommonEndPoints
  {
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
      //00. 공통 진입점 등록
      var group = app.MapGroup("/api/files");

      //10. Get 파일 목록 조회
      group.MapGet("/", async (FileService fileService) =>
      {
        var files = await fileService.GetFileListAsync<FileDTO>();
        return Results.Ok(files);
      });

      //20. Post 파일 업로드
      group.MapPost("/upload", async (HttpRequest request, FileService fileService) =>
      {
        var files = request.Form.Files;
        if (files.Count == 0) return Results.BadRequest("파일이 없습니다.");

        var isSuccess = await fileService.UploadFileAsync(files);

        return isSuccess ? Results.Ok(new { message = "파일 저장 완료"}) : Results.Problem("파일 저장 중 오류 발생");
      });


    }
  }
}
