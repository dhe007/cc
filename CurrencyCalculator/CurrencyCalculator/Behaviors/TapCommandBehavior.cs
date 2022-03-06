using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CurrencyRateCalculator.Behaviors
{
    public class TapCommandBehavior : BehaviorBase<View>
    {
        TapGestureRecognizer _tapGestureRecognizer;

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TapCommandBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            _tapGestureRecognizer = new TapGestureRecognizer();
            _tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            bindable.GestureRecognizers.Add(_tapGestureRecognizer);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.GestureRecognizers.Remove(_tapGestureRecognizer);
            _tapGestureRecognizer = null;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(null))
            {
                Command.Execute(null);
            }
        }
    }
}
