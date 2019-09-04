using System;
using System.Reactive.Linq;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas.UI.Xaml;
using ReactiveUI;

namespace UwpApp
{
    public sealed partial class MainPage : Page, IViewFor<MainPageViewModel>
    {

        public MainPage()
        {
            this.InitializeComponent();

            this.OneWayBind(ViewModel, vm => vm.Dummy, v => v.TheTextBlock.Text);

            ViewModel = new MainPageViewModel();

        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainPageViewModel) value;
        }

        public MainPageViewModel ViewModel { get; set; }

        private void canvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            args.DrawingSession.DrawEllipse(155, 115, 80, 30, Colors.GreenYellow, 3);
            args.DrawingSession.DrawText("Hello, world!", 100, 100, Colors.Yellow);
        }
    }

    public class MainPageViewModel : ReactiveObject
    {
        private string _dummy = "";

        public MainPageViewModel()
        {
            Observable.Interval(TimeSpan.FromSeconds(1)).Select(l => l.ToString()).ObserveOn(RxApp.MainThreadScheduler).BindTo(this, page => page.Dummy);

        }

        public string Dummy
        {
            get => _dummy;
            set => this.RaiseAndSetIfChanged(ref _dummy, value);
        }
    }
}
