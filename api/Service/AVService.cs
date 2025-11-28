using System.Globalization;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#nullable disable

namespace api.Service
{
    public class AVService : IAVService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;
        public AVService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try
            {
                var profile = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=OVERVIEW&symbol={symbol}&apikey={_config["AVKey"]}");
                var priceData = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol={symbol}&apikey={_config["AVKey"]}");
                var dividendData = await _httpClient.GetAsync($"https://www.alphavantage.co/query?function=DIVIDENDS&symbol={symbol}&apikey={_config["AVKey"]}");
                
                if (!profile.IsSuccessStatusCode || !priceData.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await profile.Content.ReadAsStringAsync();
                var priceContent = await priceData.Content.ReadAsStringAsync();
                var dividendContent = await dividendData.Content.ReadAsStringAsync();
                
                if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(priceContent))
                {
                    return null;
                }

                var tasks = JsonConvert.DeserializeObject<AVStock[]>(content);
                var stock = tasks?.FirstOrDefault();
                if (stock == null)
                {
                    return null;
                }

                // parse price from GLOBAL_QUOTE JSON
                if (!TryParsePrice(priceContent, out var price))
                {
                    return null;
                }

                // parse dividend amount from DIVIDENDS JSON
                var dividendAmount = TryParseDividend(dividendContent);

                // set the parsed dividend amount
                stock.DividendPerShare = (double)dividendAmount;

                return stock.ToStockFromAV(price);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private bool TryParsePrice(string priceContent, out decimal price)
        {
            price = 0;
            try
            {
                var j = JObject.Parse(priceContent);
                var priceToken = j.SelectToken("['Global Quote']['05. price']") ?? j.SelectToken("['Global_Quote']['05_price']");
                var priceStr = priceToken?.ToString();

                if (string.IsNullOrWhiteSpace(priceStr) ||
                    !decimal.TryParse(priceStr, NumberStyles.Any, CultureInfo.InvariantCulture, out price))
                {
                    return false;
                }
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private decimal TryParseDividend(string dividendContent)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dividendContent))
                {
                    return 0;
                }

                var dividendJ = JObject.Parse(dividendContent);
                var dividendToken = dividendJ.SelectToken("$.data[0].amount");
                var dividendStr = dividendToken?.ToString();

                if (!string.IsNullOrWhiteSpace(dividendStr) &&
                    decimal.TryParse(dividendStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var dividendAmount))
                {
                    return dividendAmount;
                }

                return 0;
            }
            catch (JsonException)
            {
                return 0;
            }
        }
    }
}