using FileManager.Services;
using Microsoft.Data.SqlClient;

namespace FileManager
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      // 컨테이너 등록
      builder.Services.AddScoped<FileService>();
      builder.Services.AddAuthorization();
      var app = builder.Build();

      // 미들웨어
      var options = new DefaultFilesOptions();
      options.DefaultFileNames.Clear();
      options.DefaultFileNames.Add("main.html");
      app.UseDefaultFiles(options);
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseAuthorization();

      // endpoint 등록
      RegisterAppEndPoints(app);

      //실행
      app.Run();
    }

    //엔드포인트 종합 등록
    static void RegisterAppEndPoints(IEndpointRouteBuilder commonEndPoints)
    {
      ICommonEndPoints fileApi = new FileEndpoints();
      fileApi.MapEndPoints(commonEndPoints);
    }
  }
}
