using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieLibraryDataBase.DataModels;


namespace MovieLibrary
{
    public class InputOutputService
    {
        public string Startup()
        {
            Console.WriteLine("Welcome to the Movie Library");
            Console.WriteLine("Options");
            Console.WriteLine("(L): List Movies");
            Console.WriteLine("(A): Add Movies to the library");
            Console.WriteLine("(S): Search Movies");
            Console.WriteLine("(U): Update Movies");
            Console.WriteLine("(D): Delete Movies");
            Console.WriteLine("(X): Exit Program");
            string option = Console.ReadLine().ToUpper();
            return option;
        }

        public string SearchOption()
        {
            Console.WriteLine("Enter a string to search");
            string searchString = Console.ReadLine();
            return searchString;
        }

        public List<Movie> AddMovie(DatabaseManager dbManager)
        {
            List<Movie> movieList = new List<Movie>();
            string title = "";
            do
            {
                List<MovieGenre> movieGenres = new List<MovieGenre>();
                Console.WriteLine("Enter the Movie Title or (X) to Exit and Save");
                title = Console.ReadLine();
                if (title.ToUpper() == "X")
                {
                    break;
                }

                int year = 0;
                int month = 0;
                int day = 0;
                DateTime releaseDate = new DateTime();

                do
                {
                    Console.WriteLine("Enter the release date");
                    try
                    {
                        Console.WriteLine("Enter the year");
                        year = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the month as an integer");
                        month = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the day");
                        day = int.Parse(Console.ReadLine());
                        releaseDate = new DateTime(year, month, day);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Input must be a valid date");
                        year = 0;
                        month = 0;
                        day = 0;
                    }

                } while (year == 0 || month == 0 || day == 0);
                
                Console.WriteLine("Enter the Genres, type (X) to Exit");

                List<Genre> genres = dbManager.ReadGenres();
                string genreName;
                List<string> genreList = new List<string>();
                
                do
                {
                    genreName = Console.ReadLine();

                    if (genreName.ToUpper() == "X")
                    {
                        break;
                    }

                    if (genres.Where(g => g.Name.ToUpper() == genreName.ToUpper()).ToList().Count == 1)
                    {
                        genreList.Add(genreName);
                    }
                    else
                    {
                        Console.WriteLine("Not a valid genre");
                    }
                    
                } while (genreName.ToUpper() != "X");

                Movie movie = new Movie();
                movie.Title = title;
                movie.ReleaseDate = releaseDate;

                for (int i = 0; i < genreList.Count; i++)
                {
                    MovieGenre movieGenre = new MovieGenre();
                    movieGenre.Movie = movie;

                    var genres1 = genres.Where(g => g.Name.ToUpper() == genreList[i].ToUpper()).ToList();

                    if (genres1.Count == 1)
                    {
                        movieGenre.Genre = genres1[0];
                        movieGenres.Add(movieGenre);
                    }
                }
                
                movie.MovieGenres = movieGenres;
                movieList.Add(movie);

            } while (title.ToUpper() != "X");

            return movieList;
        }

        public void UpdateMovie(Search mediaSearch, DatabaseManager dbManager, MediaFormatter formatter)
        {
            Console.WriteLine();
            
                        List<MovieGenre> movieGenres = new List<MovieGenre>();
            
                        Console.WriteLine("Search for the movie you would like to update");
                        string searchString1 = Console.ReadLine().ToUpper();
                        List<Movie> moviesSearched1;
                        moviesSearched1 = mediaSearch.SearchMovies(dbManager.ReadMedia(), searchString1);

                        if (moviesSearched1.Count > 0)
                        {
                            Console.WriteLine($"{moviesSearched1.Count} results found");
                            Console.WriteLine("Enter the ID of the movie you would like to update");
                            
                            List<string> moviesString = formatter.FormatMovieToString(moviesSearched1, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                            for (int i = 0; i < moviesString.Count; i++)
                            {
                                Console.WriteLine(moviesString[i]);
                            }

                            int Id = 0;
                            
                            try
                            {
                                Id = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Input must be an integer");
                            }

                            List<Movie> movieList = moviesSearched1.Where(m => m.Id == Id).ToList();

                            if (movieList.Count != 1)
                            {
                                Console.WriteLine("The ID you selected was not in the search results");
                            }
                            else
                            {
                                Movie movie = movieList[0];
                                Console.WriteLine("Enter the Movie Title");
                                string title = Console.ReadLine();
                                
                                int year = 0;
                                int month = 0;
                                int day = 0;
                                DateTime releaseDate = new DateTime();

                                do
                                {
                                    Console.WriteLine("Enter the release date");
                                    try
                                    {
                                        Console.WriteLine("Enter the year");
                                        year = int.Parse(Console.ReadLine()); 
                                        Console.WriteLine("Enter the month as an integer");
                                        month = int.Parse(Console.ReadLine());
                                        Console.WriteLine("Enter the day"); 
                                        day = int.Parse(Console.ReadLine());
                                        releaseDate = new DateTime(year, month, day);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Input must be a valid date");
                                        year = 0;
                                        month = 0;
                                        day = 0;
                                    }

                                } while (year == 0 || month == 0 || day == 0);

                                
                                Console.WriteLine("Enter the Genres, type (X) to Exit");

                                List<Genre> genres = dbManager.ReadGenres();
                                string genreName;
                                List<string> genreList = new List<string>();
                
                                do
                                {
                                    genreName = Console.ReadLine();

                                    if (genreName.ToUpper() == "X")
                                    {
                                        break;
                                    }

                                    if (genres.Where(g => g.Name.ToUpper() == genreName.ToUpper()).ToList().Count == 1)
                                    {
                                        genreList.Add(genreName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not a valid genre");
                                    }
                    
                                } while (genreName.ToUpper() != "X");
                                
                                movie.Title = title;
                                movie.ReleaseDate = releaseDate;

                                for (int i = 0; i < genreList.Count; i++)
                                {
                                    MovieGenre movieGenre = new MovieGenre();
                                    movieGenre.Movie = movie;

                                    var genres1 = genres.Where(g => g.Name.ToUpper() == genreList[i].ToUpper()).ToList();

                                    if (genres1.Count == 1)
                                    {
                                        movieGenre.Genre = genres1[0];
                                        movieGenres.Add(movieGenre);
                                    }
                                }

                                movie.MovieGenres = movieGenres;
                                dbManager.Update(movie);
                                Console.WriteLine("Movie Updated");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No results found");
                        }

                        Console.WriteLine();
        }

        public void DeleteMovie(Search mediaSearch, DatabaseManager dbManager, MediaFormatter formatter)
        {
            Console.WriteLine();
                        Console.WriteLine("Search for the movie you would like to delete");
                        string searchString2 = Console.ReadLine().ToUpper();
                        List<Movie> moviesSearched2;
                        moviesSearched2 = mediaSearch.SearchMovies(dbManager.ReadMedia(), searchString2);

                        if (moviesSearched2.Count > 0)
                        {
                            Console.WriteLine($"{moviesSearched2.Count} results found");
                            Console.WriteLine("Enter the ID of the movie you would like to delete");

                            List<string> moviesString = formatter.FormatMovieToString(moviesSearched2, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                            for (int i = 0; i < moviesString.Count; i++)
                            {
                                Console.WriteLine(moviesString[i]);
                            }

                            int Id = 0;

                            try
                            {
                                Id = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Input must be an integer");
                            }

                            List<Movie> movieList = moviesSearched2.Where(m => m.Id == Id).ToList();

                            if (movieList.Count != 1)
                            {
                                Console.WriteLine("The ID you selected was not in the search results");
                            }
                            else
                            {
                                Movie deleteMovie = movieList[0];
                                dbManager.Delete(deleteMovie);
                                Console.WriteLine("Movie Deleted");
                            }

                        }
                        else
                        {
                            Console.WriteLine("No results found");
                        }

                        Console.WriteLine();
        }
    }
}