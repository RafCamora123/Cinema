using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

public class Bioscoop
{
    public string Location;
    public List<Film>? allMovies { get; private set; }

    private bool movieChosen = false;

    public Bioscoop(string location)
    {
        Location = location;
        MakeMovies();
    }

    public void ChooseMovies(Zaal zaal)
    {
        int returnCode = ShowMovies();
        if (returnCode != -1 && returnCode != -2) // -1 is geen film gekozen en -2 is geen films aanwezig
        {
            zaal.chooseChairs();
        }

    }


    public int ShowMovies()
    {
        if (allMovies != null)
        {
            int currentMovieID = 0;
            if (movieChosen)
            {
                Console.Clear();
                movieChosen = false;

                foreach (Film movie in allMovies)
                {
                    movie.isCurrentMovie = false;
                }
                allMovies[0].isCurrentMovie = true;
            }
            else
            {
                Console.Clear();
                //currentMovieID = 0;
                allMovies[currentMovieID].isCurrentMovie = true;

            }
            allMovies[currentMovieID].PrintControllInfo();
            allMovies[currentMovieID].PrintMovie();
            allMovies[currentMovieID + 1].PrintMovie();
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.W or ConsoleKey.UpArrow:
                        if (currentMovieID > 0)
                        {
                            allMovies[currentMovieID].isCurrentMovie = false;
                            currentMovieID--;
                            allMovies[currentMovieID].isCurrentMovie = true;
                        }
                        break;
                    case ConsoleKey.S or ConsoleKey.DownArrow:
                        if (currentMovieID < allMovies.Count - 1)
                        {
                            allMovies[currentMovieID].isCurrentMovie = false;
                            currentMovieID++;
                            allMovies[currentMovieID].isCurrentMovie = true;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Console.Clear();
                        movieChosen = true;
                        Console.WriteLine($"chosen movie is {allMovies[currentMovieID].Title}");
                        return allMovies[currentMovieID].ID;// movie chosen
                    case ConsoleKey.Backspace:
                        Console.Clear();
                        movieChosen = false;
                        return -1; //no movie chosen
                }
                allMovies[currentMovieID].PrintControllInfo();
                for (int i = 0; i < allMovies.Count; i++)
                {
                    if (allMovies[i].isCurrentMovie && i != 0 && i < allMovies.Count - 1)
                    {
                        allMovies[i - 1].PrintMovie();
                        allMovies[i].PrintMovie();
                        allMovies[i + 1].PrintMovie();
                    }
                    else if (allMovies[i].isCurrentMovie && i != 0 && i == allMovies.Count - 1)
                    {
                        allMovies[i - 1].PrintMovie();
                        allMovies[i].PrintMovie();
                    }
                    else if (allMovies[i].isCurrentMovie && i == 0 && i < allMovies.Count - 1)
                    {
                        allMovies[i].PrintMovie();
                        allMovies[i + 1].PrintMovie();
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("no movies");
            return -2;// no movies 
        }

    }


    public void MakeMovies()
    {
        string filePath = "movies.json";
        if (File.Exists(filePath))
        {
            Console.WriteLine("right filepath");
            string jsonContent = File.ReadAllText(filePath);
            allMovies = JsonConvert.DeserializeObject<List<Film>>(jsonContent)!;
        }
        else
        {
            Console.WriteLine("wrong filepath");
        }



    }
}