using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignProject.Modules.Amplitude
{
    class ObservableAmplitudeList : ObservableCollection<AmplitudeTable>
    {
        public ObservableAmplitudeList(AssignDatabaseDataContext assign)
        {
            foreach(AmplitudeTable amplitude in assign.AmplitudeTables)
            {
                this.Add(amplitude);
            }
        }

       // public int Amplitude
    }
}
