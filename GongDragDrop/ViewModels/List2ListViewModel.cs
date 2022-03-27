using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GongDragDrop.ViewModels
{
    public class List2ListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SampleListBoxData> _sampleLists;
        public ObservableCollection<SampleListBoxData> SampleLists
        {
            get => _sampleLists;
            set
            {
                _sampleLists = value;
                OnPropertyChanged();
            }
        }

        public List2ListViewModel()
        {
            SampleLists = new ObservableCollection<SampleListBoxData>();
            SampleLists.Add(new SampleListBoxData(1, "a"));
            SampleLists.Add(new SampleListBoxData(2, "b"));
            SampleLists.Add(new SampleListBoxData(3, "c"));
            SampleLists.Add(new SampleListBoxData(4, "d"));
            SampleLists.Add(new SampleListBoxData(5, "e"));
            SampleLists.Add(new SampleListBoxData(6, "f"));
            SampleLists.Add(new SampleListBoxData(7, "g"));
            SampleLists.Add(new SampleListBoxData(8, "h"));
            SampleLists.Add(new SampleListBoxData(9, "i"));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class SampleListBoxData
    {
        public int SampleId { get; set; }
        public string SampleText { get; set; }

        public SampleListBoxData(int id, string text)
        {
            SampleId = id;
            SampleText = text;
        }
    }
}