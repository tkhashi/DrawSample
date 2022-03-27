using System;
using System.IO;
using System.Reactive.Disposables;
using System.Windows;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace GongDragDrop.ViewModels
{
    public class Explorer2ListViewModel : BindableBase, IDisposable
    {
        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
        public string Title => "Drag & Drop Sample Application";

        public ReactivePropertySlim<string> DropFile { get; }

        public ReactiveCommand<DragEventArgs> FileDropCommand { get; private set; }

        public Explorer2ListViewModel()
        {
            DropFile = new ReactivePropertySlim<string>().AddTo(Disposable);

            FileDropCommand = new ReactiveCommand<DragEventArgs>().AddTo(Disposable);
            FileDropCommand.Subscribe(e =>
            {
                if (e != null)
                {
                    OnFileDrop(e);
                }
            });
        }

        private void OnFileDrop(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            var dropFiles = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (dropFiles == null)
                return;

            if (File.Exists(dropFiles[0]))
            {
                DropFile.Value = dropFiles[0];
            }
            else
            {
                DropFile.Value = "ドロップされたものはファイルではありません";
            }
        }


        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}