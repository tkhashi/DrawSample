using Reactive.Bindings;
using System.Windows;

namespace DrawRectangle.ViewModels
{
    public class DrawingRectangleViewModel
    {
        public ReactiveCommand<Point> OnMouseLeftButtonDown { get; } = new ReactiveCommand<Point>();
        public ReactiveCommand<Point> OnMouseDrag { get; } = new ReactiveCommand<Point>();
        public ReactiveCommand OnMouseLeftButtonUp { get; } = new ReactiveCommand();

        public ReactivePropertySlim<double> CanvasWidth { get; } = new ReactivePropertySlim<double>(500);
        public ReactivePropertySlim<double> CanvasHeight { get; } = new ReactivePropertySlim<double>(300);
        public ReactivePropertySlim<double> StartX { get; } = new ReactivePropertySlim<double>();
        public ReactivePropertySlim<double> StartY { get; }= new ReactivePropertySlim<double>();
        public ReactivePropertySlim<double> Width { get; } = new ReactivePropertySlim<double>();
        public ReactivePropertySlim<double> Height { get; } = new ReactivePropertySlim<double>();
        public ReactivePropertySlim<string> AxisText { get; } = new ReactivePropertySlim<string>();


        public DrawingRectangleViewModel()
        {
            OnMouseLeftButtonDown.WithSubscribe(point =>
            {
                StartX.Value = point.X;
                StartY.Value = point.Y;
            });

            OnMouseDrag.WithSubscribe(point =>
            {
                Width.Value = point.X - StartX.Value;
                Height.Value = point.Y - StartY.Value;
                AxisText.Value = $"X: {point.X} Y: {point.Y}";
            });

            OnMouseLeftButtonUp.Subscribe(() =>
            {
                StartX.Value = 0;
                StartY.Value = 0;
                Width.Value = 0;
                Height.Value = 0;
            });
        }
    }
}