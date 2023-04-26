namespace SharedResource
{
    using System;
    using Prism.Events;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ResetPulseWidthRateValue : PubSubEvent<int>
    {
    }

    public class ResetAmplitudeValue : PubSubEvent<double>
    { }

    public class EnableSaveButtonEvent : PubSubEvent<bool>
    { }

    public class EnableViewButtonEvent : PubSubEvent<bool>
    { }

    public class EnableViewButton : PubSubEvent<bool>
    { }

    public class EnableSettingButtonEvent : PubSubEvent<bool>
    { }

    public class SaveCurrentAmplitude : PubSubEvent<double> { }

    public class SaveTargetAmplitude : PubSubEvent<double> { }

    public class SaveRampSpeed : PubSubEvent<string> { }

    public class SaveRate : PubSubEvent<int> { }

    public class SavePulse : PubSubEvent<int> { }

    public class SavePort : PubSubEvent<string> { }

    public class Savehemisphere : PubSubEvent<string> { }

    public class SaveLead : PubSubEvent<string> { }

    public class ApplyCurrentAmplitude : PubSubEvent<double> { }

    public class ApplyTargetAmplitude : PubSubEvent<double> { }

    public class ApplyRampSpeed : PubSubEvent<string> { }

    public class ApplyPulse : PubSubEvent<int> { }

    public class ApplyRate : PubSubEvent<int> { }

    public class ApplyLead : PubSubEvent<string> { }

    public class ApplyPort : PubSubEvent<string> { }

    public class ApplyHemisphere : PubSubEvent<string> { }
}
