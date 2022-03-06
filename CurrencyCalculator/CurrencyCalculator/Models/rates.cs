using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyRateCalculator.Models
{
    public class rates : ObservableObject
    {
        string usd = string.Empty;
        public string USD
        {
            get { return usd; }
            set { SetProperty(ref usd, value); }
        }



    }

}
