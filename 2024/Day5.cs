namespace AoC._2024;

public class Day5
{
	public Day5(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (true)
		{
            int Exs = 0;
            
			Console.WriteLine($"===== Puzzle part {++Exs} =====");
			input = ["47|53",
							"97|13",
							"97|61",
							"97|47",
							"75|29",
							"61|13",
							"75|53",
							"29|13",
							"97|29",
							"53|29",
							"61|53",
							"97|53",
							"61|29",
							"47|13",
							"75|47",
							"97|75",
							"47|61",
							"75|61",
							"47|29",
							"75|13",
							"53|13",
							"",
							"75,47,61,53,29",
							"97,61,53,29,13",
							"75,29,13",
							"75,97,47,61,53",
							"61,13,29",
							"97,13,75,29,47",];
			Console.WriteLine($"\tSuccess : {Puzzle1() == 143}");
			input = [""];
			//Console.WriteLine($"\tSuccess : {Puzzle1() == 1}");


			Console.WriteLine($"===== Puzzle part {++Exs} =====");
			input=[""];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");
			input = [""];
			//Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");



			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); 
		Puzzle2(); 
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2024/2024_day5_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];
	
	int Puzzle1(){
		//Puzzle1
		var res = 0;

		//CODE HERE
		/*
			X|Y page rules (X must be before Y)
				gap
			x,y updateOrder (must follow rules)

			Calc safe ones.. get sum of median!
		*/
		var Rule = input.TakeWhile(x=>!String.IsNullOrWhiteSpace(x)).ToList();
		var Order = input.SkipWhile((x) => !String.IsNullOrWhiteSpace(x)).ToList();
			Order.RemoveAt(0);

		List<string> safeRules = [];

        foreach (var order in Order)
		{
			var o = order.Split(",");
			for(int i=0; i<o.Length; i++)
			{
				//Check if within rules.. if not then break! else safe and add!
				List<string> antiRule = [];
                antiRule.AddRange( Rule.Where(r => r.Split("|")[1] == o[i]).Select(r => r.Split("|")[0]) );

				//Gets list of nums that cannot be before current num

				if( antiRule.Any(test => o[0..i].Any(r=>test==r)))
				{
					//rule violation
					break;
				}
				else if(i==o.Length-1)
				{
					Order.Add(order);
				}
			}
			//Order.Add(order);
		}

		res = Order.Select(c => int.Parse( c.Split(",")[ (int) (c.Split(",").Count() / 2)] ) ).Sum();
		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;

		//CODE HERE
		/*
			
		*/
		foreach (var input in input)
		{

		}

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
				var res = client.GetAsync($"https://adventofcode.com/{2024}/day/{5}/input").Result;

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	
}
