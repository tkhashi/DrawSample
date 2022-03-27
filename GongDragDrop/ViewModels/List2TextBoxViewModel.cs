using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GongSolutions.Wpf.DragDrop;
using GongSolutions.Wpf.DragDrop.Utilities;

namespace GongDragDrop.ViewModels
{
    public class List2TextBoxViewModel : IDropTarget
    {
        public SampleData SampleLists { get; set; } = new SampleData()
        {
            SampleLists = new ObservableCollection<string>()
            {
                "1",
                "2",
                "3",
                "4",
                "5",
            }
        };

        public void DragOver(IDropInfo dropInfo)
        {
            dropInfo.DropTargetAdorner = typeof(DropTargetHighlightAdorner);
            dropInfo.Effects = DragDropEffects.Move;
        }

        public void Drop(IDropInfo dropInfo)
        {
            if (dropInfo.Data.GetType().Equals(typeof(string)))
            {
                ((TextBox) dropInfo.VisualTarget).Text = ((string) dropInfo.Data);

                // Sourceのリストから削除
                var data = DefaultDropHandler.ExtractData(dropInfo.Data).OfType<object>().ToList();
                var sourceList = dropInfo.DragInfo.SourceCollection.TryGetList();
                if (sourceList != null)
                {
                    foreach (var o in data)
                    {
                        var index = sourceList.IndexOf(o);
                        if (index != -1)
                        {
                            sourceList.RemoveAt(index);
                        }
                    }
                }
            }
        }
    }

    public class DropTargetHighlightAdorner : DropTargetAdorner
    {
        private readonly Pen _pen;
        private readonly Brush _brush;

        public DropTargetHighlightAdorner(UIElement adornedElement, DropInfo dropInfo)
            : base(adornedElement, dropInfo)
        {
            _pen = new Pen(Brushes.Tomato, 0.5);
            _pen.Freeze();
            _brush = new SolidColorBrush(Colors.Coral) {Opacity = 0.2};
            this._brush.Freeze();

            this.SetValue(SnapsToDevicePixelsProperty, true);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var visualTarget = this.DropInfo.VisualTarget;
            if (visualTarget != null)
            {
                var translatePoint = visualTarget.TranslatePoint(new Point(), this.AdornedElement);
                translatePoint.Offset(1, 1);
                var bounds = new Rect(translatePoint,
                    new Size(visualTarget.RenderSize.Width - 2, visualTarget.RenderSize.Height - 2));
                drawingContext.DrawRectangle(_brush, _pen, bounds);
            }
        }

    }

    public class SampleData : INotifyPropertyChanged
    {
        private ObservableCollection<string> _sampleLists;
        public ObservableCollection<string> SampleLists
        {
            get => _sampleLists;
            set
            {
                _sampleLists = value;
                OnPropertyChanged();
            }
        }

        // これが追記したものです
        public List2TextBoxViewModel List2TextBoxViewModel { get; set; } = new List2TextBoxViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}