namespace AoC._2024;

public class Day1
{
	public Day1(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (false)
		{
            int Exs = 0;
            input = [""];
			Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1}");
			//input=[];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

			input = [""];
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1}");
			//input=[];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); // 2970687
        Puzzle2(); // 23963899
    }
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2024/2024_day1_puzzle";
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
				var res = client.GetAsync($"https://adventofcode.com/{2024}/day/{1}/input").Result;

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
			
		*/
		List<string> left = [], right = [];
		foreach (var input in input)
		{
			left.Add(Regex.Matches(input, "\\d+")[0].Value);
            right.Add(Regex.Matches(input, "\\d+")[1].Value);
        }
		left.Sort(); right.Sort();

		res = left.Zip(right).Aggregate(0, (prev, curr) =>
			prev + Math.Abs(int.Parse(curr.First) - int.Parse(curr.Second))
		);
		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;

        //CODE HERE
        /*
			
		*/
        List<string> left = [], right = [];
        foreach (var input in input)
        {
            left.Add(Regex.Matches(input, "\\d+")[0].Value);
            right.Add(Regex.Matches(input, "\\d+")[1].Value);
        }

		res = left.Aggregate(0, (prev, curr) =>
			prev + int.Parse(curr) * right.Count(item => item==curr)
		);

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}
}
