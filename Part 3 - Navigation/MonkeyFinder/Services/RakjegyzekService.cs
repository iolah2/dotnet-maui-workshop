using System.Net.Http.Json;

namespace MonkeyFinder.Services
{
    public class RakjegyzekService
    {
        readonly HttpClient httpClient;
        public RakjegyzekService()
        {
            this.httpClient = new HttpClient();
        }
        List<Rakjegyzek> rakjegyzekList;

        public async Task<List<Rakjegyzek>> GetRakjegyzeks()
        {
            /*if (rakjegyzekList?.Count > 0)
                return rakjegyzekList;*/

            // Online
            var response = await httpClient.GetAsync("https://localhost:7231/Rakjegyzek/Get");
                /*//"https://www.montemagno.com/monkeys.json");//https://localhost:7231/Rakjegyzek");*/
            if (response.IsSuccessStatusCode)
            {
                rakjegyzekList = await response.Content.ReadFromJsonAsync<List<Rakjegyzek>>();
            }

            // Offline
            /*using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);*/

            return rakjegyzekList;
        }

        public async Task<bool> PutItemToFelrakAsync(int id)
        {
            //if (rakjegyzekList?.Count > 0)
            //  return rakjegyzekList;

            // Online
            //https://localhost:7231/Rakjegyzek/Update?id=22455
            var response = await httpClient.GetAsync("https://localhost:7231/Rakjegyzek/Update?id="+id.ToString());
            /*//"https://www.montemagno.com/monkeys.json");//https://localhost:7231/Rakjegyzek");*/
            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);//rakjegyzekList = await response.Content.ReadFromJsonAsync<List<Rakjegyzek>>();
            }

            // Offline
            /*using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            monkeyList = JsonSerializer.Deserialize<List<Monkey>>(contents);*/

            //return rakjegyzekList;

            //using SqlConnection con = new SqlConnection(connectionString);
            //SqlCommand command = new SqlCommand($"Update Rakjegyzek Set Felrakva = 1 Where [RakjegyzekAZ] = {id}", con);
            //await con.OpenAsync();
            //var count = command.ExecuteNonQuery();
            return await Task.FromResult(false);
        }
    }
}
