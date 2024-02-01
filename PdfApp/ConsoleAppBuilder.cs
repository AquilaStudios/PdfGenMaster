using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfDemo;

namespace PdfApp
{
    public class ConsoleAppBuilder
    {
        public ConsoleAppBuilder() 
        { 

        }

        public bool BuildDocument()
        {
            PdfGenerator pdfGenerator = new();

            pdfGenerator.ProcessPdf();
            return true;
        }

    }
}
