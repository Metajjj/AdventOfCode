using System.Text.RegularExpressions;

namespace AoC._2015;

public class Day6
{
	public Day6(){

        //Minature Day title
        //Decorating Year to stand out
        //BufferWidth = includes offscreen, window = curr visible??
        Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));


        //Example..
        if (false)
		{
            int Exs = 0;
            input = ["turn on 0,0 through 999,999"];
			Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1000*1000}");
			input=["toggle 0,0 through 999,999"];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 2000000}");

			input = ["turn on 0,0 through 999,999", "turn off 499,499 through 500,500"];
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1000*1000 -4}");
			input = ["turn on 0,0 through 0,0"];
            Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); //377891
        Puzzle2(); //14110788
    }
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day6_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];
	
	void GetInput(){
			//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Cookie", "session=" + sessionId);
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{6}/input").Result;

				//var res = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent("Test results true") };

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
							//Extra "\n" at end - affects specific puzzles
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	

	int Puzzle1(){
		//Puzzle1
		var res = 0;

		//CODE HERE
		/*
			0=>999 x 0=>999 grid (starts off)
			'turn on'
			'turn off'
			'toggle'
		*/
		var lights = new Dictionary<(int X, int Y), bool> ();
		for (int i = 0; i <= 999; i++) {
		 for (int j = 0; j <= 999; j++) {
				lights.Add((i, j), false);
		}}
		//Setup light grid

		foreach (var input in input)
		{
			var startCoords = Regex.Matches(input, "\\d+,\\d+")[0].Value;
            var endCoords = Regex.Matches(input, "\\d+,\\d+")[1].Value;
			(int sX, int sY) = ( int.Parse(startCoords.Split(",")[0]), int.Parse(startCoords.Split(",")[1]) );
            (int eX, int eY) = (int.Parse(endCoords.Split(",")[0]), int.Parse(endCoords.Split(",")[1]));

			for (int i=sX; i <= eX; i++) { 
			 for(int j = sY; j<=eY; j++) {
                    lights[(i, j)] = 
						input.Contains("on")
						? true 
						: input.Contains("off")
							? false 
							: !lights[(i, j)];

                    //if (input.Contains("on")) { lights[(sX,sY)] = true; }
					//else if ( input.Contains("off")) { lights[(sX, sY)] = false; }
					//else { lights[(sX, sY)] = !lights[(sX, sY)]; }
			}}
        }
			//Count all that is true
		res = lights.Count(kvp => kvp.Value);
		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;

        //CODE HERE
        /*
			on = +1
			off = -1
			toggle = +2
		*/
        var lights = new Dictionary<(int X, int Y), int>();
        for (int i = 0; i <= 999; i++)
        {
            for (int j = 0; j <= 999; j++)
            {
                lights.Add((i, j), 0);
            }
        }
        //Setup light grid

        foreach (var input in input)
        {
            var startCoords = Regex.Matches(input, "\\d+,\\d+")[0].Value;
            var endCoords = Regex.Matches(input, "\\d+,\\d+")[1].Value;
            (int sX, int sY) = (int.Parse(startCoords.Split(",")[0]), int.Parse(startCoords.Split(",")[1]));
            (int eX, int eY) = (int.Parse(endCoords.Split(",")[0]), int.Parse(endCoords.Split(",")[1]));

            for (int i = sX; i <= eX; i++)
            {
                for (int j = sY; j <= eY; j++)
                {
                    lights[(i, j)] +=
                        input.Contains("on")
                        ? 1
                        : input.Contains("off")
                            ? -1
                            : 2;
					//Lowest is 0
					if (lights[(i, j)] < 0) { lights[(i, j)] = 0; }

                    //if (input.Contains("on")) { lights[(sX,sY)] = true; }
                    //else if ( input.Contains("off")) { lights[(sX, sY)] = false; }
                    //else { lights[(sX, sY)] = !lights[(sX, sY)]; }
                }
            }
        }

		res = lights.Aggregate(0, (prev, curr) => prev + curr.Value);

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}
}
