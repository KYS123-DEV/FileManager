using FileManager.DTO;
using Microsoft.Data.SqlClient;
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

    /// <summary>
    /// 파일 목록 불러오기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<List<FileDTO>> GetFileListAsync<T>()
    {
      try
      {
        var list = new List<FileDTO>();
        string query = "SELECT FileNo, FileKind, FileNm, FileSize, EntryDt FROM syfile01t ORDER BY EntryDt DESC";
        using var conn = new SqlConnection(_connStr);
        using var cmd = new SqlCommand(query, conn);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
          list.Add(new FileDTO
          {
            FileNo = reader.GetInt64("FileNo"),
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
        Debug.WriteLine($"{ex.StackTrace} [+] {ex.Message}");
        throw;
      }
    }

    /// <summary>
    /// 파일 업로드
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    public async Task<bool> UploadFileAsync(IFormFileCollection files)
    {
      try
      {
        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();
        using var cmd = new SqlCommand("dbo.usp_fileupdate_id", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        foreach (var file in files)
        {
          using var ms = new MemoryStream();
          await file.CopyToAsync(ms);
          byte[] fileBytes = ms.ToArray();

          cmd.Parameters.Clear();
          cmd.Parameters.AddWithValue("@p_transaction_mode", "i");
          cmd.Parameters.AddWithValue("@p_fileKind", "");
          cmd.Parameters.AddWithValue("@p_fileNm", file.FileName);
          cmd.Parameters.AddWithValue("@p_fileData", fileBytes);
          cmd.Parameters.AddWithValue("@p_fileSize", fileBytes.Length);
          cmd.Parameters.AddWithValue("@p_entryId", "");

          var result = await cmd.ExecuteScalarAsync();

          if (result == null || Convert.ToInt16(result) < 0)
            return false;
        }
        return true;
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"{ex.StackTrace} [+] {ex.Message}");
        throw;
      }
    }

    public async Task<(byte[] fileData, string fileNm)> DownloadFileByNoAsync(string fileNo)
    {
      try
      {
        using var conn = new SqlConnection(_connStr);
        await conn.OpenAsync();
        
        string query = "SELECT filedata, filenm FROM syfile01t WHERE fileno = @p_fileNo";
        using var cmd = new SqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@p_fileNo", Convert.ToInt64(fileNo));

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
          byte[] fileData = (byte[]) reader["filedata"];
          string fileNm = reader.GetString("filenm");
          return (fileData, fileNm);
        }

        return (null, string.Empty);
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"{ex.StackTrace} [+] {ex.Message}");
        throw;
      }
    }
  }
}
