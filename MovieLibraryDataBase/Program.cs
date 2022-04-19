using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryDataBase.DataModels;

namespace MovieLibrary
{
    class MovieLibrary
    {
        static void Main(string[] args)
        {
            Dependency dep = new Dependency();

            InputOutputService service = dep.GetInputOutputService();
            MediaFormatter formatter = dep.GetFormatter();
            Search mediaSearch = dep.getSearch();
            DatabaseManager dbManager = dep.GetDatabaseManager();
            
            string option;
            
            do
            {
                option = service.Startup();

                switch (option)
                {
                    case "L":
                        List<string> movies = formatter.FormatMovieToString(dbManager.ReadMedia(), dbManager.ReadMovieGenres(), dbManager.ReadGenres());
                        
                        for (int i = 0; i < movies.Count; i++)
                        {
                            Console.WriteLine(movies[i]);
                        }
                        Console.WriteLine();
                        
                        break;
                    case "A":
                        dbManager.WriteMedia(service.AddMovie(dbManager));
                        Console.WriteLine();
                        
                        break;
                    case "S":

                        Console.WriteLine();
                        string searchString = service.SearchOption().ToUpper();
                        Console.WriteLine();

                        List<Movie> movies1 = dbManager.ReadMedia();
                        List<Movie> moviesSearched = mediaSearch.SearchMovies(movies1, searchString);
                           
                        if (moviesSearched.Count > 0)
                        {
                            Console.WriteLine($"{moviesSearched.Count} results found");

                            List<string> moviesString = formatter.FormatMovieToString(moviesSearched, dbManager.ReadMovieGenres(), dbManager.ReadGenres());
                                
                            for (int i = 0; i < moviesString.Count; i++)
                            {
                                Console.WriteLine(moviesString[i]);
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("No results found");
                        }

                        Console.WriteLine();
                        break;
                    case "U":
                        service.UpdateMovie(mediaSearch, dbManager, formatter);
                        break;
                    case "D":
                        service.DeleteMovie(mediaSearch, dbManager, formatter);
                        break;
                }
            } while ( option != "X");
        }
    }
}