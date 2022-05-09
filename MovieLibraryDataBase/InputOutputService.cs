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
            Console.WriteLine("(1): List Movies");
            Console.WriteLine("(2): Add Movies to the library");
            Console.WriteLine("(3): Search Movies");
            Console.WriteLine("(4): Update Movies");
            Console.WriteLine("(5): Delete Movies");
            Console.WriteLine("(6): Add User");
            Console.WriteLine("(7): Display Users");
            Console.WriteLine("(8): Rate a movie");
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

        public void AddUser(DatabaseManager dbManager)
        {
            int age;
            List<User> users = new List<User>();
            do
            {
                User user = new User();
                Console.WriteLine("Enter the age of the user, or enter 0 to exit");
                age = -1;
                string gender = "";
                string zip = "0";
                string occupationName = "";

                do
                {
                    try
                    {
                        age = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine("Invalid Age");
                        age = -1;
                    }

                    if (age < 0)
                    {
                        if (age != -1)
                        {
                            Console.WriteLine("Invalid Age");
                            age = -1;
                        }
                    }
                    
                } while (age == -1);

                if (age != 0)
                {
                    do
                    {
                        Console.WriteLine("Enter the gender (M): Male (F): Female");
                        gender = Console.ReadLine().ToUpper();

                        if (gender != "M" && gender != "F")
                        {
                            Console.WriteLine("Invalid Gender");
                        }

                    } while (gender != "M" && gender != "F");

                    do
                    {
                        Console.WriteLine("Enter the zip code");
                        zip = Console.ReadLine();
                        Exception? exception = null;
                        bool isFiveChars = false;

                        try
                        {
                            int zipInt = int.Parse(zip);
                        }
                        catch (Exception e)
                        {
                            exception = e;
                        }

                        if (zip.Length == 5)
                        {
                            isFiveChars = true;
                        }

                        if (!isFiveChars || exception != null)
                        {
                            zip = "0";
                            Console.WriteLine("Invalid Zip Code");
                        }

                    } while (zip == "0");
                    
                    Console.WriteLine("Enter the occupation");
                    occupationName = Console.ReadLine();
                    
                    List<Occupation> occupations = dbManager.ReadOccupations();
                    List<string> occupationList = new List<string>();
                    Occupation occupation = new Occupation();

                    for (int i = 0; i < occupations.Count; i++)
                    {
                        occupationList.Add(occupations[i].Name);
                    }

                    if (occupationList.Contains(occupationName))
                    {
                        List<Occupation> occupations1 = occupations.Where(o => o.Name == occupationName).ToList();

                        if (occupations1.Count == 1)
                        {
                            occupation = occupations1[0];
                        }
                    }
                    else
                    {
                        occupation.Name = occupationName;
                    }

                    user.Occupation = occupation;
                    user.Age = age;
                    user.Gender = gender;
                    user.ZipCode = zip;
                    users.Add(user);

                    Console.WriteLine();
                    Console.WriteLine($"User Age: {age}");
                    Console.WriteLine($"User Gender: {gender}");
                    Console.WriteLine($"User Zipcode: {zip}");
                    Console.WriteLine($"User occupation: {occupation.Name}");
                    Console.WriteLine();

                }
                
            } while (age != 0);

            dbManager.AddUser(users);
        }

        public void DisplayUsers(List<string> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                Console.WriteLine(users[i]);
            }
        }

        public void RateMovie(DatabaseManager dbManager, Search mediaSearch, MediaFormatter formatter)
        {
            int userId = -1;
            int movieId = -1;
            User user = new User();
            Movie movie = new Movie();
            int Rating = 0;
            
            
            do
            {
                Console.WriteLine("Enter the ID of the user");

                try
                {
                    userId = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Input must be an integer");
                }

                List<User> users = dbManager.ReadUsers().Where(u => u.Id == userId).ToList();

                if (users.Count == 1)
                {
                    user = users[0];
                }
                else
                {
                    Console.WriteLine("Invalid User ID");
                    userId = -1;
                }

            } while (userId == -1);

            do
            {
                string searchString = "";
                Console.WriteLine("Search for the movie you would like to rate");
                searchString = Console.ReadLine().ToUpper();

                List<Movie> movies = mediaSearch.SearchMovies(dbManager.ReadMedia(), searchString);
                
                if (movies.Count > 0)
                {
                    Console.WriteLine($"{movies.Count} results found");
                    Console.WriteLine("Enter the ID of the movie you would like to rate");

                    List<string> moviesString = formatter.FormatMovieToString(movies, dbManager.ReadMovieGenres(), dbManager.ReadGenres());

                    for (int i = 0; i < moviesString.Count; i++)
                    {
                        Console.WriteLine(moviesString[i]);
                    }
                    
                    try
                    {
                        movieId = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Input must be an integer");
                    }

                    List<Movie> movieList = movies.Where(m => m.Id == movieId).ToList();

                    if (movieList.Count != 1)
                    {
                        Console.WriteLine("The ID you selected was not in the search results");
                        movieId = -1;
                    }
                    else
                    {
                        movie = movieList[0];
                    }

                }
                else
                {
                    Console.WriteLine("No results found");
                    movieId = -1;
                }
                
            } while (movieId == -1);

            do
            {
                Console.WriteLine($"Enter the rating for the movie: {movie.Title}");

                try
                {
                    Rating = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Input must be an integer");
                }

                if (Rating > 5 || Rating < 1)
                {
                    Console.WriteLine("Invalid Rating");
                    Rating = 0;
                }
                
            } while (Rating == 0);

            UserMovie userMovie = new UserMovie();
            userMovie.Movie = movie;
            userMovie.Rating = Rating;
            userMovie.User = user;
            userMovie.RatedAt = DateTime.Now;
            dbManager.AddUserMovie(userMovie);

            List<Movie> movie1 = new List<Movie>();
            movie1.Add(movie);
            List<String> movieString = formatter.FormatMovieToString(movie1, dbManager.ReadMovieGenres(), dbManager.ReadGenres());
            string displayString = "";

            if (movieString.Count == 1)
            {
                displayString = movieString[0];
            }

            List<User> user1 = new List<User>();
            user1.Add(user);
            List<string> userString = formatter.FormatUserToString(user1, dbManager.ReadOccupations());
            string userDisplay = "";

            if (userString.Count == 1)
            {
                userDisplay = userString[0];
            }

            Console.WriteLine("Successfully rated movie");
            Console.WriteLine();
            Console.WriteLine("Movie details");
            Console.WriteLine($"{displayString}, Rating: {Rating}");
            Console.WriteLine();
            Console.WriteLine("User Details");
            Console.WriteLine(userDisplay);
            
        }
        
    }
}