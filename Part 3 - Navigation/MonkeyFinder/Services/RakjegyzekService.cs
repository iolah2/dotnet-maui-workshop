using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace MonkeyFinder.Services
{
    public class RakjegyzekService
    {
        //string ipPath = @"10.0.2.2:7231";
        string ipPath = "https://192.168.1.117:44312";//@"127.0.0.1:7231";//@"192.168.1.117:7231";
        //string ipPath2 = ippa// @"127.0.0.1:5077";
        //        string ipPath = @"192.168.1.117:8080";
        //string ipPath = @"localhost:8080";
        //string ipPath = @"192.168.1.103:8080";
        readonly HttpClient _httpClient;
        public RakjegyzekService()
        {
            HttpClientHelper httpClientHelper = new HttpClientHelper();
            HttpClientHandler handler = httpClientHelper.GetInsecureHandler();
            _httpClient = new HttpClient(handler);
            //this.httpClient = new HttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _httpClient.BaseAddress = new Uri($@"{ipPath}");
        }
        List<Rakjegyzek> rakjegyzekList;

        public async Task<List<Rakjegyzek>> GetRakjegyzeks()
        {

        //https://github.com/wickedw/MAUI.SSLTest/blob/master/ssltest/MainPage.xaml.cs
           

            //"https://localhost:7161");
            //var x = await _httpClient.GetAsync("https://localhost:7161/WeatherForecast");
            /*if (rakjegyzekList?.Count > 0)
                return rakjegyzekList;*/

            // Online
            //https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/rest?view=net-maui-7.0
            //string json = JsonSerializer.Serialize<Rakjegyzek>(item, _serializerOptions);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            string uri =string.Format($@"{ipPath}/rakjegyzek/get", String.Empty);
            

            var response = await _httpClient.GetAsync($@"{ipPath}/rakjegyzek/get");//zek/Get");
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
        }//https://mail.google.com/mail/u/0/#inbox

        public async Task<bool> PutItemToFelrakAsync(int id)
        {
            //if (rakjegyzekList?.Count > 0)
            //  return rakjegyzekList;

            // Online
            //https://localhost:7231/Rakjegyzek/Update?id=22455
            //kivéve: 22452
            var response = await _httpClient.GetAsync($@"{ipPath}/Rakjegyzek/Update?id=" + id.ToString());
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

        internal async Task<bool> PutItemToFelrakAsync(string aktRakjegy)
        {
            var response = await _httpClient.GetAsync($@"{ipPath}/Rakjegy/{aktRakjegy}");
            /*//"https://www.montemagno.com/monkeys.json");//https://localhost:7231/Rakjegyzek");*/
            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(true);//rakjegyzekList = await response.Content.ReadFromJsonAsync<List<Rakjegyzek>>();
            }
            return await Task.FromResult(false);
        }
    }
}

