using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawApp
{
    /// <summary>
    /// Interaction logic for Circle.xaml
    /// </summary>
    public partial class Circle : UserControl
    {
        private Brush _previousFill = null;

        public Circle()
        {
            InitializeComponent();
        }

        public Circle(Circle c)
        {
            InitializeComponent();
            CircleUI.Height = c.CircleUI.Height;
            CircleUI.Width = c.CircleUI.Width;
            CircleUI.Fill = c.CircleUI.Fill;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData(DataFormats.StringFormat, CircleUI.Fill.ToString());
                data.SetData("Double", CircleUI.Height);
                data.SetData("Object", this);

                DragDrop.DoDragDrop(this, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);

            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                Mouse.SetCursor(Cursors.Cross);
            }
            else if (e.Effects.HasFlag(DragDropEffects.Move))
            {

                Mouse.SetCursor(Cursors.Pen);
            }
            else
            {
                Mouse.SetCursor(Cursors.No);
            }

            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            if(e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string dataString = (string)e.Data.GetData(DataFormats.StringFormat);

                BrushConverter converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    Brush newFill = (Brush) converter.ConvertFromString(dataString);
                    CircleUI.Fill = newFill;

                    if (e.KeyStates.HasFlag(DragDropKeyStates.ControlKey))
                    {
                        e.Effects  = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effects = DragDropEffects.Move;
                    }

                }
            }

            e.Handled = true;
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            base.OnDragOver(e);
            e.Effects = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                var dataString = (string) e.Data.GetData(DataFormats.StringFormat);
                var converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    e.Effects = e.KeyStates.HasFlag(DragDropKeyStates.ControlKey)
                        ? DragDropEffects.Copy
                        : DragDropEffects.Move;
                }
            }

            e.Handled = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            _previousFill = CircleUI.Fill;

            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                var dataString = (string) e.Data.GetData(DataFormats.StringFormat);
                var converter = new BrushConverter();
                if (converter.IsValid(dataString))
                {
                    var newFill = (Brush) converter.ConvertFromString(dataString.ToString());
                    CircleUI.Fill = newFill;
                }
            }
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            CircleUI.Fill = _previousFill;
        }
    }
}