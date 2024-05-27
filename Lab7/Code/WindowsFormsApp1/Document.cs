using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class Document : FastColoredTextBox
    {
        public FileInfo FileInfo { get; set; }
        public bool IsChange { get; set; }
    }
}
