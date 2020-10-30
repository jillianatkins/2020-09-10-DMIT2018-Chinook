using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using System.ComponentModel;
using ChinookSystem.DAL;
using ChinookSystem.VIEWMODELS;
using ChinookSystem.ENTITIES;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        #region Queries
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackViewModel> Track_List()
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.Tracks
                              select new TrackViewModel
                              {
                                  TrackName = x.Name,
                                  TrackComposer = x.Composer,
                                  TrackMilliseconds = x.Milliseconds,
                                  TrackBytes = x.Bytes,
                                  TrackUnitPrice = x.UnitPrice
                              };
                return results.ToList();
            }
        }
        #endregion
    }
}
