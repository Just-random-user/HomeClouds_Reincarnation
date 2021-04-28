using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace Clouds.ViewModels
{
    public class FormViewModel
    {
        public IFormFile file { get; set; }
        public string? path { get; set; }
        public FormViewModel() { }
        public FormViewModel(string? _path, IFormFile _file)
        {
            file = _file;
            path = _path;
        }
        

    }
}
