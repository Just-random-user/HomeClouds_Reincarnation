namespace MVC_Test.ViewModels
{
    public class FileViewModel
    {
        public string path { get; set; }
        public string name { get; set; }

        public FileViewModel() { }
        public FileViewModel(string _path)
        {
            path = _path;
        }
    }
}