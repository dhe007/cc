using CurrencyRateCalculator.Services;
using MvvmNano;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace CurrencyRateCalculator
{
    public partial class App : MvvmNanoApplicationEx
    {
        public static TodoItemManager TodoManager { get; set; }

        public App()
        {
            TodoManager = new TodoItemManager(new RestService());
            InitializeComponent();
            MainPage = new CurrencyRateCalculator.MainPage();
        }

        protected override void OnStart()
        {
            base.OnStart();
            MainPage = new NavigationPage(new Currency_CalcullatorPage());
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(new AppPresenter(this));
        }

    }
}
