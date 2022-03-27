using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void panel_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Object"))
            {
                if (e.KeyStates == DragDropKeyStates.ControlKey)
                {
                    e.Effects = e.KeyStates.HasFlag(DragDropKeyStates.ControlKey)
                        ? DragDropEffects.Copy
                        : DragDropEffects.Move;
                }
            }

            e.Handled = true;
        }

        private void panel_Drop(object sender, DragEventArgs e)
        {

            if (e.Handled == false)
            {
                var panel = (Panel) sender;
                var element = (UIElement) e.Data.GetData("Object");

                if (panel != null && element != null)
                {
                    var parent = (Panel) VisualTreeHelper.GetParent(element);
                    if (parent != null)
                    {
                        if (e.KeyStates == DragDropKeyStates.ControlKey&&
                            e.AllowedEffects.HasFlag(DragDropEffects.Copy))
                        {
                            var circle = new Circle((Circle) element);
                            panel.Children.Add(circle);
                            e.Effects = DragDropEffects.Copy;
                        }
                        else if(e.AllowedEffects.HasFlag(DragDropEffects.Move))
                        {
                            parent.Children.Remove(element);
                            panel.Children.Add(element);
                            e.Effects = DragDropEffects.Move;

                        }
                    }
                }
            }
        }
    }
}
