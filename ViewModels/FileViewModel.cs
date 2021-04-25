using Microsoft.AspNetCore.Http;
using System.IO;

#nullable enable

namespace Clouds.ViewModels
{
    public class FileViewModel
    {
        public string? Login { get; set; }
        public string? Path { get; set; }
        public IFormFile Upload { get; set; }

        public FileViewModel() { }
        public FileViewModel(string? path, string login)
        {
            Login = login;
            Path = path;
        }
    }
}
