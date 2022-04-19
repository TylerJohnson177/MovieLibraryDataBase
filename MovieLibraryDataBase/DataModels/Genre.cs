using System;
using System.Collections.Generic;
using MovieLibraryDataBase.DataModels;

namespace MovieLibraryDataBase.DataModels
{
    public class Genre
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<MovieGenre> MovieGenres {get;set;}
    }
}
