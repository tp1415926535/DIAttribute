using DIAttribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp.Core.Demo
{
    [DIRegister]
    class MainWindowViewModel
    {
        public string Text { get; set; } = "Hello World!";
    }
}
