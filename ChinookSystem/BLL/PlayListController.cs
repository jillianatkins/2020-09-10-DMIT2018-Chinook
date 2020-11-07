using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.ENTITIES;
using ChinookSystem.DAL;
using ChinookSystem.VIEWMODELS;
using System.Data.Entity;
using System.Data.SqlClient;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
	[DataObject]
	public class PlayListController
	{
		#region Query for OLTP Demo PlayList for GridView
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<UserPlayListTrack> List_PlayList(string existingOrNew, string existingIDOrNewName)
		{
			using (var context = new ChinookSystemContext())
			{
				IEnumerable<UserPlayListTrack> results = null;
				if (existingOrNew.Equals("Existing"))
				{
					int narg = int.Parse(existingIDOrNewName);
					results = from x in context.PlaylistTracks
							  where x.PlaylistId == narg
							  orderby x.TrackNumber
							  select new UserPlayListTrack
							  {
								  TrackID = x.Track.TrackId,
								  TrackNumber = x.TrackNumber,
								  TrackName = x.Track.Name,
								  Milliseconds = x.Track.Milliseconds,
								  UnitPrice = x.Track.UnitPrice
							  };
					//throw new Exception("PlayListController, List_PlayList, (Existing) NOT implemented yet");
				}
				else if (existingOrNew.Equals("New"))
				{
					throw new Exception("PlayListController, List_PlayList, (New) NOT implemented yet");
				}

				if (results == null)
				{
					return null;
				}
				else
				{
					return results.ToList();
				}
			}
		}
				#endregion

				#region Query for OLTP Demo Existing PlayList DDL
				[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<SelectionList> GetPlayListForDDLByUserName()
		{
			using (var context = new ChinookSystemContext())
			{
				var userName = "RobbinLaw";
				var results = from x in context.Playlists
							  where x.UserName == userName
							  select new SelectionList
							  {
								  IDValueField = x.PlaylistId,
								  DisplayText = x.Name
							  };
				return results.ToList();
			}
		}
		#endregion

		#region Query for Repeater Demo
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<PlayListItem> PlayList_GetPlayListOfSize(int lowestplaylistsize)
		{
			using (var context = new ChinookSystemContext())
			{
				var results = from x in context.Playlists
							  orderby x.UserName
							  where x.PlaylistTracks.Count() >= lowestplaylistsize
							  select new PlayListItem
							  {
								  Name = x.Name,
								  TrackCount = x.PlaylistTracks.Count(),
								  UserName = x.UserName,
								  Songs = from y in x.PlaylistTracks
										  orderby y.Track.Genre.Name, y.Track.Name
										  select new PlayListSong
										  {
											  Song = y.Track.Name,
											  GenreName = y.Track.Genre.Name
										  }
							  };
				return results.ToList();
			}
		}
        #endregion
    }
}
