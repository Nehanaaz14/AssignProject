using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignProject.Modules.Amplitude.ViewModels;

namespace AssignProject.Modules.Amplitude
{
    public class ObservableRateList : ObservableCollection<RateTable>
    {
        public ObservableRateList(AssignDatabaseDataContext assign)
        {
            foreach(RateTable rate in assign.RateTables)
            {
                this.Add(rate);
            }
        }
    }
}
