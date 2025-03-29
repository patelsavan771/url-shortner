using UrlShortner.Helpers;
using UrlShortner.Models;

namespace url_shortener.Helpers;

public class UrlShortenerHelper
{
    private DapperHelper dapperHelper;
    private IConfiguration configuration;

    public UrlShortenerHelper(DapperHelper dapperHelper, IConfiguration configuration)
    {
        this.dapperHelper = dapperHelper;
        this.configuration = configuration;
    }

    public async Task<string> GenerateShortenUrl(string url)
    {
        UniqueCodeGenerator generator = new UniqueCodeGenerator(dapperHelper);
        string code = await generator.GenerateAsync();
        string? server = configuration.GetValue<string>("USER_FACING_SERVER");
        if (string.IsNullOrEmpty(server))
        {
            throw new NullReferenceException("Null server url");
        }
        string shortenUrl = $"{server}/{code}";

        // save code to db
        await SaveToDb(new ShortenUrl
        {
            code = code,
            url = url,
            createdOn = DateTime.Now
        });

        return shortenUrl;
    }

    internal async Task<string> GetFullUrlAsync(string code)
    {

        string query = "SELECT url FROM url_master WHERE code = @code";
        string? result = await dapperHelper.ExecuteScalarAsync<string>(query, new { code });

        if (result != null)
        {
            return result.ToString();
        }

        return "";
    }

    private async Task SaveToDb(ShortenUrl shortenUrl)
    {
        string query = "INSERT INTO url_master (code, url, createdOn) VALUES (@code, @url, @createdOn);";
        await dapperHelper.ExecuteNonQueryAsync(query, shortenUrl);
    }
}