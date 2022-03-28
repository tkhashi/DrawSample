using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace DrawRectangle.Behaviors
{
    public class MouseButtonBehavior : Behavior<FrameworkElement>
    {

        private bool _isButtonDown;

        public static readonly DependencyProperty MouseLeftButtonClickedProperty =
            DependencyProperty.Register(
                nameof(MouseLeftButtonClicked),
                typeof(ICommand),
                typeof(MouseButtonBehavior),
                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty MouseLeftButtonUpProperty =
            DependencyProperty.Register(
                nameof(MouseLeftButtonUp),
                typeof(ICommand),
                typeof(MouseButtonBehavior),
                new FrameworkPropertyMetadata());

        public static readonly DependencyProperty MouseDragProperty = 
            DependencyProperty.Register(
                nameof(MouseDrag),
                typeof(ICommand),
                typeof(MouseButtonBehavior),
                new FrameworkPropertyMetadata());

        public ICommand MouseLeftButtonClicked
        {
            get => (ICommand) GetValue(MouseLeftButtonClickedProperty);
            set => SetValue(MouseLeftButtonClickedProperty, value);
        }

        public ICommand MouseLeftButtonUp
        {
            get => (ICommand) GetValue(MouseLeftButtonUpProperty);
            set => SetValue(MouseLeftButtonUpProperty, value);
        }

        public ICommand MouseDrag
        {
            get => (ICommand) GetValue(MouseDragProperty);
            set => SetValue(MouseDragProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            
            AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
            AssociatedObject.MouseMove += OnMouseDrag;
            AssociatedObject.MouseLeftButtonUp += OnMouseLefButtonUp;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
            AssociatedObject.MouseMove -= OnMouseDrag;
            AssociatedObject.MouseLeftButtonUp -= OnMouseLefButtonUp;

            base.OnDetaching();
        }


        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            _isButtonDown = true;

            AssociatedObject.CaptureMouse();
            var prevPoint = e.GetPosition(AssociatedObject);
            var point = new Point(prevPoint.X, prevPoint.Y);

            if (MouseLeftButtonClicked == null || !MouseLeftButtonClicked.CanExecute(point)) return;
            MouseLeftButtonClicked.Execute(point);

        }

        private void OnMouseLefButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isButtonDown = false;
            MouseLeftButtonUp.Execute(e);
        }

        private void OnMouseDrag(object sender, MouseEventArgs e)
        {
            if (!_isButtonDown) return;

            AssociatedObject.CaptureMouse();
            var prevPoint = e.GetPosition(AssociatedObject);
            var point = new Point(prevPoint.X, prevPoint.Y);

            MouseDrag.Execute(point);
        }
    }
}