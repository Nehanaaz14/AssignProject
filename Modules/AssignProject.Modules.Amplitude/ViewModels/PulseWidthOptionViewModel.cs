using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SharedResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    class PulseWidthOptionViewModel : BindableBase, IDialogAware 
    {
        public string Title => "";
        public int? PulseWidthSelected { get; set; }
        public int SelectedPulseWidthItemIndex { get; set; }
        public DelegateCommand AcceptButtonCommand { get; set; }

        public DelegateCommand CloseButtonCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private readonly ObservablePulseWidthList pulsewidthList;

        public PulseWidthOptionViewModel()
        {
            this.pulsewidthList = new ObservablePulseWidthList(_adpDC);
            this.CloseButtonCommand = new DelegateCommand(this.CloseDialog);
            this.AcceptButtonCommand = new DelegateCommand(this.AcceptPulseHandler);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            //throw new NotImplementedException();
        }

        public void AcceptPulseHandler()
        {
            this.PulseWidthSelected = pulsewidthList[SelectedPulseWidthItemIndex].PulseWidthValues.Value;
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters { { DialogNames.PulseSelected, PulseWidthSelected } }));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void CloseDialog()
        {
            PulseWidthSelected = 60;
            this.RequestClose.Invoke(new DialogResult(ButtonResult.Cancel, new DialogParameters { { DialogNames.PulseSelected, PulseWidthSelected } }));
        }
    }
}
