using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRateCalculator.Models;
using CurrencyRateCalculator;
using MvvmNano;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CurrencyRateCalculator
{
    public class Currency_CalcullatorViewModel : BaseViewModel
    {

        /// <summary>
        /// Geters and setters for the variables used in this ViewModel,  when changed, the binding staff can be noticed
        /// </summary>


        RateInfo rateInfos;
        public RateInfo RateInfos
        {
            get { return rateInfos; }
            set { rateInfos = value; NotifyPropertyChanged(); }
        }

        string inputMoney = String.Empty;
        public string InputMoney
        {
            get { return inputMoney; }
            set { inputMoney = value;  NotifyPropertyChanged(); }
        }

        string outputMoney = String.Empty;
        public string  OutputMoney
        {
            get { return outputMoney; }
            set { outputMoney = value; NotifyPropertyChanged(); }
        }

        bool offline = true;
        public bool OffLine
        {
            get { return offline; }
            set { offline = value; NotifyPropertyChanged(); }
        }



        public ICommand SwitchModeCommand { get; private set; }

        public ICommand TextChangedCommand { get; private set; }


        /// <summary>
        /// Bind the commands to the Tasks
        /// </summary>
        public Currency_CalcullatorViewModel()
        {
            SwitchModeCommand = new MvvmNanoCommand<Xamarin.Forms.ToggledEventArgs>(SwitchMode);
            TextChangedCommand = new MvvmNanoCommand (TextChanged);

        }

        public override void Initialize()
        {
            base.Initialize();
            IsBusy = true;

            // 
            SetSource();
        }

        public decimal convertToDecimal(String str)
        {
            // Decimal separator is ",".
            CultureInfo culture = new CultureInfo("tr-TR");
            // Decimal sepereator is ".".
            CultureInfo culture1 = new CultureInfo("en-US");
            // You can remove letters here by adding regex expression.
            str = Regex.Replace(str, @"\s+|[a-z|A-Z]", "");
            decimal result = 0;
            var success = decimal.TryParse(str, NumberStyles.AllowThousands |
                NumberStyles.AllowDecimalPoint, culture, out result);
            // No need NumberStyles.AllowThousands or
            // NumberStyles.AllowDecimalPoint but I used:
            decimal result1 = 0;
            var success1 = decimal.TryParse(str, NumberStyles.AllowThousands |
                NumberStyles.AllowDecimalPoint, culture1, out result1);
            if (success && success1)
            {
                if (result > result1)
                    return result1;
                else
                    return result;
            }
            if (success && !success1)
                return result;
            if (!success && success1)
                return result1;
            return 0;
        }

        public void TextChanged()
        {
           
            decimal rate = convertToDecimal(RateInfos.rates.USD);

            decimal inputmoney = convertToDecimal(InputMoney);
            decimal outputmoney = inputmoney * rate;

            OutputMoney = outputmoney.ToString();
        }


        public void SwitchMode(Xamarin.Forms.ToggledEventArgs args)
        {
            /*
             if (args.Value != false)
             {
                 IsBusy = true;

                 IsBusy = false;
             }
            */
            OffLine = !OffLine;
            SetSource();
        }

        /// <summary>
        /// Set the data for the page
        /// </summary>
        async void SetSource()
        {
            if (OffLine == false)
            {
                var rateInfos = await App.TodoManager.TryGetRateInfos();
                await Task.Delay(1000);
                RateInfos = rateInfos;
            }

            else
            {
                var rateInfos = await App.TodoManager.ITryGetRateInfosFromLocaDatabase();
                await Task.Delay(1000);
                RateInfos = rateInfos;
            }
            IsBusy = false;

        }

        /// <summary>
        /// String format convertor
        /// </summary>


        void SwitchMode()
        {
            //if (args.Value != false)
            {
                IsBusy = true;

    
                IsBusy = false;
            }
        }


        /// <summary>
        /// Get all the schedules and shifts from the jobs owned by this candidate
        /// </summary>





    }
}
