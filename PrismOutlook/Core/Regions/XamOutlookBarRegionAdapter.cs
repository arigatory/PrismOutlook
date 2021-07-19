using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Windows.OutlookBar;
using Prism.Regions;

namespace PrismOutlook.Core.Regions
{
    public class XamOutlookBarRegionAdapter : RegionAdapterBase<XamOutlookBar>
    {
        public XamOutlookBarRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, XamOutlookBar regionTarget)
        {
            region.Views.CollectionChanged += ((x, y) =>
            {
                switch (y.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                    {
                        foreach (OutlookBarGroup group in y.NewItems)
                        {
                            regionTarget.Groups.Add(group);
                            if (regionTarget.Groups[0] == group)
                            {
                                regionTarget.SelectedGroup = group;
                            }
                        }
                        break;
                    }
                    case NotifyCollectionChangedAction.Remove:
                    {
                        foreach (OutlookBarGroup group in y.OldItems)
                        {
                            regionTarget.Groups.Remove(group);
                        }
                        break;
                    }
                }
            });
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}
