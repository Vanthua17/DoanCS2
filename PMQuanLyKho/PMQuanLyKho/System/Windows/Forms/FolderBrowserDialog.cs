
namespace System.Windows.Forms
{
    internal class FolderBrowserDialog : IDisposable
    {
        public string Description { get; internal set; }
        public bool ShowNewFolderButton { get; internal set; }
        public string SelectedPath { get; internal set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal object ShowDialog()
        {
            throw new NotImplementedException();
        }
    }
}