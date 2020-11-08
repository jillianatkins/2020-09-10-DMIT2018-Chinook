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
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public bool UserNameIsValid(string playlistusername)
		{
			using (var context = new ChinookSystemContext())
			{
				Playlist exists = (from x in context.Playlists
								   where x.UserName.Equals(playlistusername)
								   select x).FirstOrDefault();
				if (exists == null)
				{
					return false;
				}
                else
                {
					return true;
                }
			}
		}

				
		#region OLTP Demo
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<UserPlayListTrack> ListExistingPlayList(string existingPlayListID)
		{
			using (var context = new ChinookSystemContext())
			{
				IEnumerable<UserPlayListTrack> results = null;
				if(existingPlayListID != "")
                {
					int narg = int.Parse(existingPlayListID);
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
					return results.ToList();
				}
                else
                {
					return null;
                }
			}
		}
		
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public List<SelectionList> GetPlayListForDDLByUserName(string userName)
		{
			using (var context = new ChinookSystemContext())
			{
				//var userName = "RobbinLaw";
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

		public void AddNewPLaylist(string playlistname, string username)
		{
			using (var context = new ChinookSystemContext())
			{
				Playlist exists = (from x in context.Playlists
								   where x.Name.Equals(playlistname)
									&& x.UserName.Equals(username)
								   select x).FirstOrDefault();
				if (exists == null)
				{
					exists = new Playlist()
					{
						//pkey is an identity int key
						Name = playlistname,
						UserName = username
					};
					context.Playlists.Add(exists);
					context.SaveChanges();
				}
				else
				{
					throw new Exception("ERROR: PlayList Already Exists for this User");
				}
			}
		}
		public void SavePlayList(List<UserPlayListTrack> playList)
		{
			using (var context = new ChinookSystemContext())
			{
				throw new Exception("MESSAGE: SavePlayList NOT IMPLEMENTED");
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
