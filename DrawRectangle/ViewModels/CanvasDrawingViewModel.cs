using System.Windows;
using System.Windows.Input;
using Reactive.Bindings;

namespace DrawRectangle.ViewModels
{
    public class CanvasDrawingViewModel
    {
        public ReactivePropertySlim<string> Text { get; } = new ReactivePropertySlim<string>("クリックされていないな…");

        public ReactiveCommand ClickLeftCommand { get; } = new ReactiveCommand();

        public CanvasDrawingViewModel()
        {
            ClickLeftCommand.WithSubscribe(() => OnClickLeft());
        }

        public void OnClickLeft()
        {
            Text.Value = "左クリックされたよ！";
        }
    }
}