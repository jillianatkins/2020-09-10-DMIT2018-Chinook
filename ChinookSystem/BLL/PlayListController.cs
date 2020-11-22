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
		#region OLTP Demo
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

		public int AddNewPLaylist(string playlistname, string username)
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
					//A new primary key will be put into exits.PlaylistId
					//after the new Playlist is added to the Playists collection.
					context.Playlists.Add(exists);
					context.SaveChanges();
					return exists.PlaylistId;
				}
				else
				{
					throw new Exception("ERROR: PlayList Already Exists for this User");
				}
			}
		}
		public void SavePlayList(int argplaylistid, List<UserPlayListTrack> argplaylist)
		{
			PlaylistTrack argPlaylistTrack = null;
			using (var context = new ChinookSystemContext())
			{
				//Delete from PlaylistTracks table all items related to playlistid
				//presently in the db.
				var playlistOld = (from x in context.PlaylistTracks
								   where x.PlaylistId == argplaylistid
								   select x).ToList();
				foreach (PlaylistTrack item in playlistOld)
				{
					context.PlaylistTracks.Remove(item);
				}
				//Add to PlaylistTracks table the items from playlist collection
				//argument and relate them to playlistid.
				foreach (UserPlayListTrack item in argplaylist)
                {
					//Instantiate a new entity type.
					argPlaylistTrack = new PlaylistTrack();
					argPlaylistTrack.PlaylistId = argplaylistid;
					argPlaylistTrack.TrackId = item.TrackID;
					argPlaylistTrack.TrackNumber = item.TrackNumber;
					context.PlaylistTracks.Add(argPlaylistTrack);
                }
				context.SaveChanges();
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
