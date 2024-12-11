namespace AoC._2015;

public class Day3
{
	public Day3(){

        Console.WriteLine("\n" + "".PadLeft(Console.WindowWidth / 2, '='));
        string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

        //Example..
        if (false)
		{
            int Exs = 0;
            input = ["^v"];
            Console.WriteLine($"===== Example input {++Exs} =====");
            Console.WriteLine($"\tSuccess : {Puzzle1() == 2}");
            Console.WriteLine($"\tSuccess : {Puzzle2() == 3}");

            input = ["^>v<"];
			Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 4}");
			Console.WriteLine($"\tSuccess : {Puzzle2() == 3}");

			input = ["^v^v^v^v^v"];
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 2}");
			Console.WriteLine($"\tSuccess : {Puzzle2() == 11}");

            Console.WriteLine($"===== Actual input =====");
        }

        //Nrml..
        GetInput();
        Puzzle1(); //2081
		Puzzle2(); //2341
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day3_puzzle";
		//Year per puzzle sets

	string sessionId =Secret.secret; //Valid till 2025-06

	String[] input = [];
	
	void GetInput(){
			//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Cookie", "session=" + sessionId);
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{3}/input").Result;

				//var res = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent("Test results true") };

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result);
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	

	int Puzzle1(){
		//Puzzle1
		var res = 0;

		int i = 0, j = 0;
		HashSet<(int x, int y)> distinctCoords = [(i,j)] ;
		foreach (char c in input[0])
		{
			i += c switch { '^' => 1, 'v' => -1, _ => 0 };
            j += c switch { '>' => 1, '<' => -1, _ => 0 };
            distinctCoords.Add((i, j));
		}

		res = distinctCoords.Count();

		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}"); 
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;
		/*
		 ^v^v => per instruct = per santa (nrm, robot, nrm, robot)
		*/
		//CODE HERE
		bool robot = false;
        int i=0, j=0, I=0, J=0;
        HashSet<(int x, int y)> distinctCoords = [(i, j)];
		foreach (char c in input[0])
		{
			if (!robot)
			{
				i += c switch { '^' => 1, 'v' => -1, _ => 0 };
				j += c switch { '>' => 1, '<' => -1, _ => 0 };
			}
			else
			{
				I += c switch { '^' => 1, 'v' => -1, _ => 0 };
				J += c switch { '>' => 1, '<' => -1, _ => 0 };
			}
            
            distinctCoords.Add((i, j));
            distinctCoords.Add((I, J));

			robot = !robot;
        }

        res = distinctCoords.Count();



        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}
}
