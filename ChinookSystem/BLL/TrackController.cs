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

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> List_TracksForPlaylistSelection(string tracksby, string arg)
        {
            using (var context = new ChinookSystemContext())
            {
                IEnumerable<TrackList> results = null;
                if (tracksby.Equals("Artist"))
                {
                    results = from x in context.Tracks
                              where x.Album.Artist.Name.Contains(arg)
                              orderby x.Album.Artist.Name, x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else if (tracksby.Equals("Album"))
                {
                    results = from x in context.Tracks
                              where x.Album.Title.Contains(arg)
                              orderby x.Album.Title, x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else if (tracksby.Equals("MediaType"))
                {
                    int narg = int.Parse(arg);
                    results = from x in context.Tracks
                              where x.MediaTypeId == narg
                              orderby x.MediaType.Name, x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
                }
                else if (tracksby.Equals("Genre"))
                {
                    int narg = int.Parse(arg);
                    results = from x in context.Tracks
                              where x.GenreId == narg
                              orderby x.Genre.Name, x.Name
                              select new TrackList
                              {
                                  TrackID = x.TrackId,
                                  Name = x.Name,
                                  Title = x.Album.Title,
                                  ArtistName = x.Album.Artist.Name,
                                  MediaName = x.MediaType.Name,
                                  GenreName = x.Genre.Name,
                                  Composer = x.Composer,
                                  Milliseconds = x.Milliseconds,
                                  Bytes = x.Bytes,
                                  UnitPrice = x.UnitPrice
                              };
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
        }//eom
        #endregion
    }
}
