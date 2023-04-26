using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignProject.Modules.Amplitude
{
    public class ObservableSavedSettingsList : ObservableCollection<SavedSetting>
    {
        public ObservableSavedSettingsList(AssignDatabaseDataContext assign)
        {
            foreach(SavedSetting savedSetting in assign.SavedSettings)
            {
                this.Add(savedSetting);
                
            }
            
        }

        //public void SavedSettings(AssignDatabaseDataContext assign,string id, double curAmp, int pw, int rate, string leadtype,string port, string hemisphere, int rating, double tarAmp,string rampspeed)
        //{
        //    SavedSetting savedSetting = new SavedSetting();
        //    savedSetting.ID = id;
        //    savedSetting.MinAmplitude = curAmp;
        //    savedSetting.Pulse_Width = pw;
        //    savedSetting.Rate = rate;
        //    savedSetting.SLeadType = leadtype;
        //    savedSetting.SPortType = port;
        //    savedSetting.SHemisphere = hemisphere;
        //    savedSetting.Rating = rating;
        //    savedSetting.MaxAmplitude = tarAmp;
        //    savedSetting.RampSpeed = rampspeed;
        //}
    }
}
