using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Regions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AssignProject.Core.Regions
{
    public class StackPanelRegionAdaptor : RegionAdapterBase<StackPanel>
    {
        public StackPanelRegionAdaptor(RegionBehaviorFactory behaviorFactory):
            base(behaviorFactory)
        { }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    foreach (FrameworkElement item in e.NewItems)
                    {
                        regionTarget.Children.Add(item);
                    }
                }
                else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    foreach (FrameworkElement item in e.OldItems)
                    {
                        regionTarget.Children.Remove(item);
                    }
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new Region();
        }
    }
}
