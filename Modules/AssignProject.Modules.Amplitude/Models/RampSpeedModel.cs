using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignProject.Modules.Amplitude.Models
{
    public sealed class RampSpeedModel
    {
        private static readonly Dictionary<RampSpeedItem, string> RampSpeedNameDictionary =
            new Dictionary<RampSpeedItem, string>
            {
                {RampSpeedItem.Slow, "Slow (0.1 mA/sec)"},
                {RampSpeedItem.Medium, "Medium (0.2 mA/sec)"},
                {RampSpeedItem.Fast, "Fast (0.5 mA/sec)"},
                {RampSpeedItem.Instant, "Instant (No Ramp)"}
            };

        private RampSpeedItem rampspeedItemName;

        public RampSpeedItem RampSpeedItemName
        {
            get => this.rampspeedItemName;
            set
            {
                this.rampspeedItemName = value;

                // Update the corresponding RampSpeed Name
                if (RampSpeedNameDictionary.TryGetValue(this.rampspeedItemName, out var rampSpeedName))
                {
                    //this.UpdateRampName(rampSpeedName, CultureInfo.CurrentCulture);
                    this.Name = rampSpeedName;
                }
            }
        }
        public string Name { get; private set; }

        public float Value { get; set; }

        public RampSpeedModel(RampSpeedItem rampSpeedItem, float value)
        {
            this.RampSpeedItemName = rampSpeedItem;
            this.Value = value;
        }


    }
}
