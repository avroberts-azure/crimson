using Crimson.Model;

namespace Crimson.Core.Import
{
    /// <summary>
    /// Open and read a file of property prices.
    /// The implementation to set the file source as local or http
    /// is made in the DI container.
    /// </summary>
    public interface IPricesReader
    {
        Task<Dictionary<string, PriceSet>> GetPricesAsync(string startsWith);

        Task<Dictionary<string, PriceSet>> GetPricesAsync();
    }
}