﻿using System.Collections.Generic;
using System.Linq;
using MovieLibraryDataBase.DataModels;

namespace MovieLibrary
{
    public class Search
    {
        public List<Movie> SearchMovies(List<Movie> movies, string searchString)
        {
            return movies.Where(m => m.Title.ToUpper().Contains(searchString)).ToList();
        }
        
    }
}