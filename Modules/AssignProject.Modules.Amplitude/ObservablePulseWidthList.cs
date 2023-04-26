using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignProject.Modules.Amplitude
{
    class ObservablePulseWidthList : ObservableCollection<PulseWidthTable>
    {
        public ObservablePulseWidthList(AssignDatabaseDataContext assign)
        {
            foreach(PulseWidthTable pulseWidth in assign.PulseWidthTables)
            {
                this.Add(pulseWidth);
            }
        }
    }
}
