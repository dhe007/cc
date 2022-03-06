using Xamarin.Forms;



namespace CurrencyRateCalculator.Behaviors
{
    public class BusyGridBehavior : BehaviorBase<Grid>
    {
        Grid _attachedObject;
        ActivityIndicator _activityIndicator;
        Grid _overlayGrid;

        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create("IsBusy", typeof(bool), typeof(BusyGridBehavior), false, propertyChanged: OnIsBusyPropertyChanged);
        static void OnIsBusyPropertyChanged(BindableObject d, object oldValue, object newValue)
        {
            if ((bool)newValue)
            {
                ((BusyGridBehavior)d).ShowIndicator();
            }
            else
            {
                ((BusyGridBehavior)d).HideIndicator();
            }
        }

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        protected override void OnAttachedTo(Grid target)
        {
            _attachedObject = target;
            base.OnAttachedTo(target);
        }

        protected override void OnDetachingFrom(Grid target)
        {
            base.OnDetachingFrom(target);
        }

        private void ShowIndicator()
        {
            if (_activityIndicator == null)
            {
                _overlayGrid = new Grid() { BackgroundColor = Color.Black, Opacity = 0.4 };

                AddGridSpannedChild(_attachedObject, _overlayGrid);

                _activityIndicator = new ActivityIndicator()
                {
                    Color = Color.Blue,
                    IsRunning = true,
                    IsVisible = true,

                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                };
                if (Device.OS == TargetPlatform.Windows)
                {
                    _activityIndicator.WidthRequest = 400;
                    _activityIndicator.HeightRequest = 400;
                }

                    AddGridSpannedChild(_attachedObject, _activityIndicator);
            }
        }

        static void AddGridSpannedChild(Grid grid, View child)
        {
            int rowCount = grid.RowDefinitions.Count;
            if (rowCount > 0)
            {
                Grid.SetRowSpan(child, rowCount);
            }

            int colCount = grid.ColumnDefinitions.Count;
            if (colCount > 0)
            {
                Grid.SetColumnSpan(child, colCount);
            }

            grid.Children.Add(child);
        }

        private void HideIndicator()
        {
            if (_activityIndicator != null)
            {
                var children = (_activityIndicator.Parent as Grid).Children;
                children.Remove(_activityIndicator);
                _activityIndicator = null;

                children.Remove(_overlayGrid);
                _overlayGrid = null;
            }
        }
    }
}

