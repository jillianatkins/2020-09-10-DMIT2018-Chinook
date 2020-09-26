﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Additional Namespaces
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChinookSystem.ENTITIES
{
    [Table("Tracks")]
    internal class Track
    {
        //private date members
        private string _Composer;

        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "(Entity) Name is required")]
        [StringLength(200, ErrorMessage = "(Entity) Name is limited to 200 characters")]
        public string Name { get; set; }
        public int? AlbumId { get; set; }

        [Required(ErrorMessage = "(Entity) Media Type is required")]
        public int MediaTypeId { get; set; }
        public int? GenreId { get; set; }

        [StringLength(220, ErrorMessage = "(Entity) Composer is limited to 220 characters")]
        public string Composer
        {
            get { return _Composer; }
            set { _Composer = string.IsNullOrEmpty(value) ? null : value; }
        }

        [Required(ErrorMessage = "(Entity) Milliseconds is required")]
        public int Milliseconds { get; set; }
        public int? Bytes { get; set; }

        [Required(ErrorMessage = "(Entity) UnitPrice is required")]
        public decimal UnitPrice { get; set; }

        //navigational properties
        public virtual Album Album { get; set; }
    }
}
