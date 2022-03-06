using CurrencyRateCalculator;
using MvvmNano.Forms;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace CurrencyRateCalculator
{
    public class AppPresenter : MvvmNanoFormsPresenter
    {
        public AppPresenter(MvvmNanoApplicationEx app) : base(app)
        {
        }
        public new Page CurrentPage
        {
            get
            {
                Func<Page> getCurrentPage = () =>
                {
                    Page modalPage = Application.MainPage.Navigation
                        .ModalStack
                        .LastOrDefault();

                    if (modalPage != null)
                        return modalPage;

                    Page contentPage = Application.MainPage.Navigation
                        .NavigationStack
                        .LastOrDefault();

                    return contentPage ?? Application.MainPage;
                };

                Page currentPage = getCurrentPage();
                var tabbedPage = currentPage as TabbedPage;

                if (tabbedPage != null)
                {
                    currentPage = tabbedPage.CurrentPage;
                }
                else
                {
                    var mdPage = currentPage as MasterDetailPage;
                    if (mdPage != null)
                    {
                        currentPage = mdPage.Detail;
                    }
                }

                return currentPage;
            }
        }

        public async void CloseCurrentPage()
        {
            // if popup stack has pages in stack, we assume the popup page is the app's current page
            if (PopupNavigation.PopupStack.Count > 0)
            {
                await CurrentPage.Navigation.PopPopupAsync();
            }
            else
            {
                await CurrentPage.Navigation.PopAsync();
            }
        }

        public  void CloseAllPopupPage()
        {

            PopupNavigation.PopAllAsync();
        }
 
    }
}
