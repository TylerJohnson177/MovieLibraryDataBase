using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using MovieLibraryDataBase.DataModels;

namespace MovieLibrary
{
    public class MediaFormatter
    {
        public List<string> FormatMovieToString(List<Movie> movies, List<MovieGenre> movieGenresList, List<Genre> genresList)
        {
            List<string> MovieList = new List<string>();
            
                    for (int i = 0; i < movies.Count; i++)
                    {
                        long id = movies[i].Id;
                        string title = movies[i].Title;
                        string year = movies[i].ReleaseDate.Year.ToString();
                        List<MovieGenre> movieGenres = movieGenresList.Where(g => g.Movie == movies[i]).ToList();
                        List<Genre> genres = new List<Genre>();

                        for (int j = 0; j < movieGenres.Count; j++)
                        {
                            var genresLoop = (genresList.Where(g => g.Id == movieGenres[j].Genre.Id).ToList());

                            for (int k = 0; k < genresLoop.Count; k++)
                            {
                                genres.Add(genresLoop[k]);
                            }
                        }
                        
                        string line;
                        string genre = "";

                        if (title.Contains(","))
                        {

                            if (title.Contains(year))
                            {
                                line = $@"ID: {id}, Title: ""{title}""";
                            }
                            else
                            {
                                line = $@"ID: {id}, Title: ""{title}"" ({year})";
                            }
                            
                        }
                        else
                        {
                            if (title.Contains(year))
                            {
                                line = $"ID: {id}, Title: {title}";
                            }
                            else
                            {
                                line = $"ID: {id}, Title: {title} ({year})";
                            }
                            
                        }

                        for (int j = 0; j < genres.Count; j++)
                        {
                            if (genres.Count != j + 1)
                            {
                                genre += genres[j].Name + "|";
                            }
                            else
                            {
                                genre += genres[j].Name;
                            }
                        }
                        MovieList.Add($"{line}, Genres: {genre}");
                    }

                    return MovieList;
        }
    }
}