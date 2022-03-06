using CurrencyRateCalculator;
using MvvmNano.Forms;




namespace CurrencyRateCalculator
{
    public partial class Currency_CalcullatorPage : MvvmNanoContentPage<Currency_CalcullatorViewModel>
    {
        public Currency_CalcullatorPage()
        {
            //NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();
            BindingContext = new Currency_CalcullatorViewModel();
            (BindingContext as Currency_CalcullatorViewModel).Initialize();
        }
    }
}