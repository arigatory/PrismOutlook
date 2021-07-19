using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Windows.Ribbon;
using Prism.Regions;

namespace PrismOutlook.Core.Regions
{
    public class XamRibbonRegionAdapter : RegionAdapterBase<XamRibbon>
    {
        public XamRibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamRibbon regionTarget)
        {
            if (region == null)
            {
                throw new ArgumentNullException(nameof(region));
            }

            if (regionTarget == null)
            {
                throw new ArgumentNullException(nameof(regionTarget));
            }

            region.Views.CollectionChanged += ((s, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (var view in e.NewItems)
                    {
                        AddViewToRegion(view, regionTarget);
                    }
                } else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var view in e.OldItems)
                    {
                        RemoveViewfromRegion(view, regionTarget);
                    }
                }
            });
        }

        private void AddViewToRegion(object view, XamRibbon regionTarget)
        {
            if (view is RibbonTabItem ribbonTabItem)
            {
                regionTarget.Tabs.Add(ribbonTabItem);
            }
        }

        private void RemoveViewfromRegion(object view, XamRibbon regionTarget)
        {
            if (view is RibbonTabItem ribbonTabItem)
            {
                regionTarget.Tabs.Remove(ribbonTabItem);
            }
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
