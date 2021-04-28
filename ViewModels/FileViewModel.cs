using Microsoft.AspNetCore.Http;
using System.IO;

#nullable enable

namespace Clouds.ViewModels
{
    public class FileViewModel
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDir { get; set; }

        public FileViewModel() { }
        public FileViewModel(string path)
        {
            Path = path;
        }

    }
}
