using System.Text.RegularExpressions;

namespace AoC._2015;

public class Day7
{
	public Day7(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (true)
		{
            int Exs = 0;
            input = ["1 -> b", "2 -> c", "b OR c -> a"];
			Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == (1|2) }");
			//input=[];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

			input = ["456 -> b", "3 -> c", "b LSHIFT c -> a"];
				//Shift = num * 2^LShiftNum  Rshift == /
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == (456<<3)}");
			//input=["","",""];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
		Puzzle1();
		Puzzle2(); 
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day7_puzzle";
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
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{7}/input").Result;

				//var res = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent("Test results true") };

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
							//Extra "\n" at end - affects specific puzzles
			}
		}

		input = File.ReadAllLines(puzzlePath);
	}

	int Puzzle1() {
		//Puzzle1
		var res = 0;

		//CODE HERE
		/*
			0-65535
			what value is 'a' ?

			AND | LSHIFT | OR | NOT | RSHIFT
			 &     <<		|	 ~		>>

			123 -> x
			456 -> y
			x AND y -> d
			x OR y -> e
			x LSHIFT 2 -> f
			y RSHIFT 2 -> g
			NOT x -> h
			NOT y -> i
		*/
		Dictionary<string, int> WireToVal = new (){ {"a",0} };
		foreach (var input in input)
		{
			var targetWire = input.Split("->")[1].Trim();
			var instruct = input.Split("->")[0].Trim();
            //Wires are single-chars ?
            // operations are always with wires ?

            //TODO sort to enter assignments.. then work based on assignments!

            if (Regex.IsMatch(instruct, "\\d+"))
			{ WireToVal[targetWire] = int.Parse(instruct); }
			else if (instruct.ToLower().Contains("not")) //NOT operation
            {
				WireToVal[targetWire] = ~WireToVal[instruct[instruct.Length - 1] + ""];
			}
			else
			{
				string operand1 = Regex.Split(instruct,"\\s+")[0],
						operand2 = Regex.Split(instruct, "\\s+")[2];

				WireToVal[targetWire] = Regex.Split(instruct, "\\s+")[1].ToUpper() switch
				{
					"RSHIFT" => WireToVal[operand1] >> WireToVal[operand2],
					"LSHIFT" => WireToVal[operand1] << WireToVal[operand2],
                    "AND" => WireToVal[operand1] & WireToVal[operand2],
                    "OR" => WireToVal[operand1] | WireToVal[operand2],
                    _ => 0,
				};
				string s = "";
			}
		}

		res = WireToVal["a"];
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
}
