using MvvmNano;

namespace CurrencyRateCalculator
{
    public class BaseViewModel : MvvmNanoViewModel
    {
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    NotifyPropertyChanged(nameof(IsBusy));
                }
            }
        }




        protected void Close()
        {
            AppPresenter presenter = MvvmNanoIoC.Resolve<IPresenter>() as AppPresenter;

            presenter.CloseCurrentPage();

        }
    }

    public class BaseViewModel<TNavigationParameter> : MvvmNanoViewModel<TNavigationParameter>
    {
        bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    NotifyPropertyChanged(nameof(IsBusy));
                }
            }
        }

        protected void CloseAllPop()
        {
            AppPresenter presenter = MvvmNanoIoC.Resolve<IPresenter>() as AppPresenter;

            presenter.CloseAllPopupPage();
        }

        protected void Close()
        {
            AppPresenter presenter = MvvmNanoIoC.Resolve<IPresenter>() as AppPresenter;

            presenter.CloseCurrentPage();
        }



    }
}

