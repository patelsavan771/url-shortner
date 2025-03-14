namespace UrlShortner.Helpers;

using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using UrlShortner.Helpers;

public class UniqueCodeGenerator
{
    public const int Length = 6;
    public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private static readonly Random random = new();
    private DapperHelper dapperHelper;

    public UniqueCodeGenerator(DapperHelper dapperHelper)
    {
        this.dapperHelper = dapperHelper; 
    }

    public async Task<string> GenerateAsync()
    {
        char[] codeArr = new char[Length];
        while (true)
        {
            //generate unique code
            for (int i = 0; i < Length; i++)
            {
                int randomIndex = random.Next(Alphabet.Length);
                codeArr[i] = Alphabet[randomIndex];
            }

            string code = new string(codeArr);
            if (await IsUniqueAsync(code))
            {
                return code;
            }
        }
    }

    private async Task<bool> IsUniqueAsync(string code)
    {
        string query = "SELECT id FROM url_master where code = @code LIMIT 1;";

        string? id = await dapperHelper.ExecuteScalarAsync<string>(query, new {code});
        if(id is null)
        {
            return true;
        }
        return false;
    }
}