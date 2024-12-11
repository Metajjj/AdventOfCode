namespace AoC._2024;

public class Day3
{
	public Day3(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (true)
		{
            int Exs = 0;
            input = ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];
			Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 161}");
			input=["xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))don't()",
                "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
            ];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 88}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1();  // 159833790
		Puzzle2();  // 89349241		ans < 111360986 < 149496942 
    }
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2024/2024_day3_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];
	
	int Puzzle1(){
		//Puzzle1
		var res = 0;

		//CODE HERE
		/*
			Perform operations from mul(\d,\d) regex -- add results
		*/
		//var combinedInput = String.Join("", input);
		foreach (var combinedInput in input)
		{

			res += Regex.Matches(combinedInput, "mul\\(\\d{1,3},\\d{1,3}\\)")
					.Select(m =>
					{
						var vals = Regex.Matches(m.Value, "\\d+").Select(m => int.Parse(m.Value)).ToList();
						return vals[0] * vals[1];
					}
				).Sum();
		}
		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;

		//CODE HERE
		/*
			!=== ===! digits must be 1-3 else fails!

			don't() disables
			do() enables mul

			//Split into 3 parts?
				1. ^=>don't()
				2. do()=>don't()
				3. do()=>$

			1. ^.*?don't\(\)
				? makes it lazy and only match 1st occurance i.e. 1st don't() in ABdon't()Cdon't()

			2. do\(\).*?don't\(\)
				match do() to don't()
				.*? means to return at first occurance of don't() else repeated-don't() overlap and returns the muls() inside

			3. do\(\)(?!.*don't\(\).*).*$
				match do()
				do not contain don't() at anypoint from start to end
				must be closest to end of line with anything inbetween that doesn't voilate prev capture group
		*/
		string CombinedInput = String.Join("", input);

		//continuing as do() when a don't() preceedes

		var ToParse = String.Join("",
			Regex.Matches(CombinedInput, @"(^.*?don't\(\))|(do\(\).*?don't\(\))|(do\(\)(?!.*don't\(\).*).*$)").Select(m => m.Value)
			);


		res += Regex.Matches(ToParse, "mul\\(\\d{1,3},\\d{1,3}\\)")
				.Select(m =>
				{
					return Regex.Matches(m.Value, "\\d+").Select(m => int.Parse(m.Value)).Aggregate(1, (p, c) => p * c);
				}
		).Sum();

		//FAILS! too large puzzle2






		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	void GetInput(){
			//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Cookie", "session=" + sessionId);
				var res = client.GetAsync($"https://adventofcode.com/{2024}/day/{3}/input").Result;

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
							//Extra "\n" at end - affects specific puzzles
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	
}
