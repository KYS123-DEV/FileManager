using CommonUtilsDev;
using FileManager.Services;
using Microsoft.Data.SqlClient;

namespace FileManager
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);

      builder.Services.AddAuthorization();

      //HTTP 컨텍스트 접근자 등록
      builder.Services.AddHttpContextAccessor();

      //세션 저장소(메모리) 및 세션 사용 설정
      builder.Services.AddDistributedMemoryCache();
      builder.Services.AddSession();

      builder.Services.AddScoped<FileService>();
      builder.Services.AddScoped<SessionManager>();

      var app = builder.Build();

      // 미들웨어
      var options = new DefaultFilesOptions();
      options.DefaultFileNames.Clear();
      options.DefaultFileNames.Add("main.html");
      //options.DefaultFileNames.Add("login.html");
      app.UseDefaultFiles(options);
      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseAuthorization();
      app.UseSession();

      // endpoint 등록
      RegisterAppEndPoints(app);

      //실행
      app.Run();
    }

    //엔드포인트 종합 등록
    static void RegisterAppEndPoints(IEndpointRouteBuilder commonEndPoints)
    {
      ICommonEndPoints fileApi = new FileEndPoints();
      fileApi.MapEndPoints(commonEndPoints);
    }
  }
}
