using FileManager.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;

namespace FileManager.Services
{
  public class FileService
  {
    private readonly string _connStr;
    public FileService(IConfiguration config)
    {
      _connStr = config.GetConnectionString("DBConnectionInfo")!;
    }

    public async Task<List<FileDTO>> GetFileListAsync<T>()
    {
      try
      {
        var list = new List<FileDTO>();
        string query = "SELECT FileNo, FileKind, FileNm, FileSize, EntryDt FROM syfile01t";
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(query, conn);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
          list.Add(new FileDTO
          {
            FileNo = reader.GetString("FileNo"),
            FileKind = reader.GetString("FileKind"),
            FileNm = reader.GetString("FileNm"),
            FileSize = reader.GetInt32("FileSize"),
            EntryDt = reader.GetString("EntryDt")
          });
        }

        return list;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
