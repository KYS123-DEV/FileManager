using CommonUtilsDe;
using FileManager.DTO;

namespace FileManager.Services
{
  /// <summary>
  /// Endpoint 등록: 파일 관련
  /// </summary>
  public class FileEndPoints : ICommonEndPoints
  {
    public void MapEndPoints(IEndpointRouteBuilder app)
    {
      //00. 공통 진입점 등록
      var group = app.MapGroup("/api/files");

      //10. Get 파일 목록 조회
      group.MapGet("/", async (FileService fileService) =>
      {
        try
        {
          var files = await fileService.GetFileListAsync<FileDTO>();
          return Results.Ok(files);
        }
        catch (Exception)
        {
          string message = MsgHelper.ShowError("파일 목록 조회 실패");
          return Results.Problem(detail: message, statusCode: 500);
        }
      });

      //20. Post 파일 업로드
      group.MapPost("/upload", async (HttpRequest request, FileService fileService) =>
      {
        try
        {
          var files = request.Form.Files;
          if (files.Count == 0) return Results.BadRequest("파일이 없습니다.");

          var isSuccess = await fileService.UploadFileAsync(files);
          string message = MsgHelper.ShowInfo("파일 업로드 완료");
          return isSuccess ? Results.Ok(message) : Results.Problem("파일 저장 중 오류 발생");
        }
        catch (Exception)
        {
          string message = MsgHelper.ShowError("파일 업로드 실패");
          return Results.Problem(detail: message, statusCode: 500);
        }
      });

      //30. Get 파일 다운로드
      group.MapGet("/download/{fileNo}", async (string fileNo, FileService fileService) =>
      {
        try
        {
          var (fileData, fileNm) = await fileService.DownloadFileByNoAsync(fileNo);

          if (fileData == null) return Results.NotFound("파일을 찾을 수 없습니다.");

          return Results.File(
            fileContents: fileData,
            contentType: "application/octet-stream",
            fileDownloadName: fileNm
            );
        }
        catch (Exception)
        {
          string message = MsgHelper.ShowError("파일 다운로드 실패");
          return Results.Problem(detail: message, statusCode: 500);
        }
      });
    }
  }
}
