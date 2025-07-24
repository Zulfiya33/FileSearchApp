
namespace FileSearch.Core.Models
{
    public class FileArgs : EventArgs
    {
        public string FileName { get; }
        public bool Cancel { get; set; }

        public FileArgs(string fileName)
        {
            FileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            Cancel = false;
        }
    }
}
