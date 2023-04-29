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

        private int selectedPulseWidthItemIndex;
        public int SelectedPulseWidthItemIndex { get=> selectedPulseWidthItemIndex; set 
            {
                selectedPulseWidthItemIndex = value;
                RaisePropertyChanged(nameof(SelectedPulseWidthItemIndex));
            } 
        }

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
            SelectedPulseWidthItemIndex = parameters.GetValue<int>("PulseIndex");
        }

        public void CloseDialog()
        {
            PulseWidthSelected = pulsewidthList[4].PulseWidthValues.Value;
            this.RequestClose.Invoke(new DialogResult(ButtonResult.Cancel, new DialogParameters { { DialogNames.PulseSelected, PulseWidthSelected } }));
        }
    }
}
