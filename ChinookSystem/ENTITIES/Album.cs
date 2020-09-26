using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChinookSystem.ENTITIES
{
    [Table("Albums")]
    internal class Album
    {
        //private date members
        private string _ReleaseLabel;

        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "(Entity) Title is required")]
        [StringLength(160, ErrorMessage = "(Entity) Title is limited to 160 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "(Entity) ArtistId is required")]
        //A foreign key: child of the relationship to the parent Artist
        //means that a record points to A (Singular) artist record.
        //DO NOT USE [ForeignKey] anotation IF names are the same
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "(Entity) Release Year is required")]
        public int ReleaseYear { get; set; }

        [StringLength(50, ErrorMessage = "(Entity) Release Label is limited to 50 characters")]
        public string ReleaseLabel
        {
            get { return _ReleaseLabel; }
            set { _ReleaseLabel = string.IsNullOrEmpty(value) ? null : value; }
        }

        //navigational properties
        public virtual Artist Artist { get; set; }
        public virtual ICollection<Track> Tracks { get; set; }
    }
}
