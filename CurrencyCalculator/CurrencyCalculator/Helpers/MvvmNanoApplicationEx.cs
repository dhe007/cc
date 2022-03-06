using MvvmNano;
using MvvmNano.Forms;
using Xamarin.Forms;
using MvvmNano.Forms;
using MvvmNano.Ninject;

namespace CurrencyRateCalculator
{

    public class MvvmNanoApplicationEx : MvvmNanoApplication
    {

        protected override void OnStart()
        {
            base.OnStart();
            SetUpPresenter();
            SetUpMessenger();
        }

        protected override IMvvmNanoIoCAdapter GetIoCAdapter()
        {
            return new MvvmNanoNinjectAdapter();
        }


        /// <summary>
        /// Registers MvvmNanoFormsPresenter. If you're using your own
        /// custom presenter, override this method for registration (but
        /// don't call base.SetUpPresenter()!).
        /// </summary>
        protected virtual void SetUpPresenter()
        {
            MvvmNanoIoC.RegisterAsSingleton<IPresenter>(new MvvmNanoFormsPresenter(this));
        }

        /// <summary>
        /// Registers MvvmNanoFormsMessenger. If you're using your own
        /// custom messenger, override this method for registration (but
        /// don't call base.SetUpMessenger()!).
        /// </summary>
        protected virtual void SetUpMessenger()
        {
            MvvmNanoIoC.RegisterAsSingleton<IMessenger, MvvmNanoFormsMessenger>();
        }

        /// <summary>
        /// Sets up the main page for the given View Model type.
        /// </summary>
        protected void SetUpMainPage<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            MainPage = new MvvmNanoNavigationPage(GetPageFor<TViewModel>());
        }

        protected void SetUpMainPage<TViewModel, TNavigationParameter>(TNavigationParameter parameter) where TViewModel : MvvmNanoViewModel<TNavigationParameter>
        {
            MainPage = new NavigationPage(GetPageFor<TViewModel, TNavigationParameter>(parameter));
        }

        /// <summary>
        /// Creates a MvvmNanoContentPage for the given View Model type.
        /// </summary>
        public Page GetPageFor<TViewModel>() where TViewModel : MvvmNanoViewModel
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>();
            viewModel.Initialize();

            var page = MvvmNanoIoC
                .Resolve<IPresenter>()
                .CreateViewFor<TViewModel>() as Page;

            if (page == null)
                throw new MvvmNanoException("Could not create a view for View Model of type " + typeof(TViewModel) + ".");

            (page as IView).SetViewModel(viewModel);

            return page as Page;
        }

        public Page GetPageFor<TViewModel, TNavigationParameter>(TNavigationParameter navigationParameter) where TViewModel : IViewModel<TNavigationParameter>
        {
            var viewModel = MvvmNanoIoC.Resolve<TViewModel>() as IViewModel<TNavigationParameter>;
            viewModel.Initialize(navigationParameter);

            var page = MvvmNanoIoC
                .Resolve<IPresenter>()
                .CreateViewFor<TViewModel>() as Page;

            if (page == null)
                throw new MvvmNanoException("Could not create a MvvmNanoContentPage for View Model of type " + typeof(TViewModel) + ".");

            (page as IView).SetViewModel(viewModel);

            return page as Page;
        }
    }
}
