using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AssignProject.Modules.Amplitude.ViewModels
{
    public class ListBoxItemViewModel : BindableBase
    {
        private bool isValid;
        private int parameterValue;
        private string parameterName;
        public bool IsValid
        {
            get => this.isValid;
            set => this.SetProperty(ref this.isValid, value);
        }
        public int ParameterValue
        {
            get => this.parameterValue;
            set => this.SetProperty(ref this.parameterValue, value);
        }

        public string ParameterName
        {
            get => this.parameterName;
            set => this.SetProperty(ref this.parameterName, value);
        }

       public override string ToString()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}:{1};IsValid:{2}",
                this.ParameterName,
                this.ParameterValue,
                this.IsValid);
        }
    }
}
