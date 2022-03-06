using CurrencyRateCalculator.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CurrencyRateCalculator.Services
{

    /// <summary>
    /// The main controlor of the whole project
    /// </summary>
    public class TodoItemManager
	{
		IRestService restService;

        public TodoItemManager (IRestService service)
		{
			restService = service;
		}

        /// <summary>
        /// Online mode, get data from api
        /// </summary>

        public Task<RateInfo> TryGetRateInfos()
        {
            return restService.ITryGetRateInfos();
        }
        /// <summary>
        /// Offline mode, get data from local database
        /// </summary>

        public Task<RateInfo> ITryGetRateInfosFromLocaDatabase()
        {
            return restService.ITryGetRateInfosFromLocaDatabase();
        }
        



    }
}
