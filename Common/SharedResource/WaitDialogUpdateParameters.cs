using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Services.Dialogs;

namespace SharedResource
{
    public class WaitDialogUpdateParameters
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool? EnableCancel { get; set; }

        public bool? EnableSkip { get; set; }

        public WaitDialogUpdateParameters()
        {
        }

 
        public WaitDialogUpdateParameters(string title, string body)
        {
            this.Title = title;
            this.Body = body;
        }

        public WaitDialogUpdateParameters(string title, string body, bool enableCancel, bool enableSkip)
        {
            this.Title = title;
            this.Body = body;
            this.EnableCancel = enableCancel;
            this.EnableSkip = enableSkip;
        }
    }
}
