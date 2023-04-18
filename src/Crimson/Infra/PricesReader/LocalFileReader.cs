using Crimson.Shared;
using Crimson.Models;

namespace Crimson.Infra.PricesReader
{
    public class LocalFileReader : IPricesReader
    {
        private readonly Configuration _configuration;
        private readonly IPricesParser _parser;

        public LocalFileReader(Configuration config, IPricesParser parser)
        {
            _configuration = config;
            _parser = parser;
        }

        public IEnumerable<PriceRecord> GetPrices()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var fullPath = Path.Combine(path, _configuration.LocalFileLocation, _configuration.LocalFileName);
            List<PriceRecord> prices = new();

            using (FileStream fs = File.OpenRead(fullPath))
            {
                using (StreamReader reader = new(fs))
                {
                    while (reader.Peek() > 0)
                    {
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            var nextPrice = _parser.ConvertCsvLine(line);

                            if (nextPrice != null)
                            {
                                prices.Add(nextPrice);
                            }

                        }

                    }
                }
            }

            return prices;
        }
    }
}
