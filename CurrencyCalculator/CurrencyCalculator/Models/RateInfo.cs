﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
namespace CurrencyRateCalculator.Models
{
    public class RateInfo : ObservableObject
    {
        bool _success = false;
        public bool success
        {
            get { return _success; }
            set { SetProperty(ref _success, value); }
        }

        string _timestamp = string.Empty;
        public string timestamp
        {
            get { return _timestamp; }
            set { SetProperty(ref _timestamp, value); }
        }


        string _base_currency = string.Empty;
        public string Base
        {
            get { return _base_currency; }
            set { SetProperty(ref _base_currency, value); }
        }

        string _trade_date = string.Empty;
        public string date
        {
            get { return _trade_date; }
            set { SetProperty(ref _trade_date, value); }
        }


        rates _rates = new rates();
        public rates rates
        {
            get { return _rates; }
            set { SetProperty(ref _rates, value); }
        }

    }
}
