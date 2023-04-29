using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SharedResource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using AssignProject.Modules.Amplitude.Views;
using System.Threading.Tasks;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class RateSelectViewModel : BindableBase, IDialogAware
    {
        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private readonly ObservableRateList rateList;

        private int selectedRateItemIndex;
        public int SelectedRateItemIndex { get=> selectedRateItemIndex; set
            {
                selectedRateItemIndex = value;
                RaisePropertyChanged("SelectedRateItemIndex");
            }
        }

        public bool btnREnabled { get; set; }

        public int? RateSelected { get; set; }
        public string Title => "";

        public ObservableCollection<RateTable> rateTables { get; set; }

        public DelegateCommand CloseCommand { get; set; }

        public DelegateCommand AcceptCommand { get; set; }

        public event Action<IDialogResult> RequestClose;

        public int previousRate;

        public RateSelectViewModel()
        {
            this.rateList = new ObservableRateList(_adpDC);
            this.rateTables = rateList;//new ObservableCollection<RateTable>();
            
            previousRate = this.rateList[SelectedRateItemIndex].RateValue.Value;
            this.CloseCommand = new DelegateCommand(this.CloseDialog);
            this.AcceptCommand = new DelegateCommand(this.AcceptRateResult);
            RaisePropertyChanged(nameof(RateSelected));
            RaisePropertyChanged(nameof(SelectedRateItemIndex));
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        private void CloseDialog()
        {
            RateSelected = 130;
            this.RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel, new DialogParameters{{DialogNames.RateSelected, RateSelected} }));
        }

        public void OnDialogClosed()
        {
            //throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            SelectedRateItemIndex = parameters.GetValue<int>("RateIndex");
            this.RateSelected = parameters.GetValue<int>(DialogNames.RateSelected);
            btnREnabled = parameters.GetValue<bool>(DialogNames.RateSaveButtonEnable);
            this.RaisePropertyChanged(nameof(RateSelected));
            //RateSelected = this.rateList[SelectedRateItemIndex].RateValue.Value;
            //this.AcceptRateResult();
        }
        private void AcceptRateResult()
        {
            if(SelectedRateItemIndex == 0)
            {
                btnREnabled = false;
            }
            else
            {
                btnREnabled = true;
            }

            RateSelected = this.rateList[SelectedRateItemIndex].RateValue.Value;

            DialogParameters rpar = new DialogParameters();
            rpar.Add(DialogNames.RateSelected, RateSelected);
            rpar.Add("RateSaveButtonEnable", btnREnabled);

            this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK , rpar));
        }
    }
}
