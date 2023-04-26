using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharedResource
{
    public class WaitDialogParameters
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public bool EnableCancel { get; set; } = true;

        public bool EnableSkip { get; set; } = true;

        public Action BackgroundAction { get; set; }

        public Action CancelAction { get; set; }

        public Action SkipAction { get; set; }

        public PubSubEvent<WaitDialogUpdateParameters> UpdateDialogEvent { get; set; }
        
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public DialogParameters CreatePrismDialogParameters()
        {
            return new DialogParameters { { DialogNames.WaitDialogInitialization, this } };
        }
    }
}
