using CurrencyRateCalculator.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CurrencyRateCalculator.Services
{

    /// <summary>
    /// Interface for the RestService Connection setup class
    /// </summary>
    public interface IRestService
	{   
        Task<RateInfo> ITryGetRateInfos();
        Task<RateInfo> ITryGetRateInfosFromLocaDatabase();


    }
}
