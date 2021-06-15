using Microsoft.Extensions.Configuration;
using Snackis_Forum_.Models;
using Snackis_Forum_.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Snackis_Forum_.Gateway
{
    public class FulaOrdGateway : IFulaOrdGateway
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public FulaOrdGateway(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public async Task<List<FulaOrd>> GetBadWords()
        {
            var response = await _httpClient.GetAsync(_config["ConnectionStrings:FulaOrdAPI"]);
            string apiResponse = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Models.FulaOrd>>(apiResponse);
        }

        public async Task<Models.FulaOrd> PostBadWord(FulaOrd fultOrd)
        {
            var response = await _httpClient.PostAsJsonAsync(_config["ConnectionStrings:FulaOrdAPI"], fultOrd);
            FulaOrd returnValue = await response.Content.ReadFromJsonAsync<FulaOrd>();

            return returnValue;
        }

        public async Task<FulaOrd> DeleteBadWord(int deleteId)
        {
            var response = await _httpClient.DeleteAsync(_config["ConnectionStrings:FulaOrdAPI"] + "/" + deleteId);
            FulaOrd returnValue = await response.Content.ReadFromJsonAsync<FulaOrd>();

            return returnValue;
        }

        public async Task<string> FilterBadWords(string text)
        {
            List<FulaOrd> fulaOrdList = await GetBadWords();

            string replacement = "*********";

            foreach (var ord in fulaOrdList)
            {
                text = Regex.Replace(text, ord.Word, replacement, RegexOptions.IgnoreCase);
            }

            return text;

        }

    }
}
