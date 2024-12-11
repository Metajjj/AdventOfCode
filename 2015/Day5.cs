using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AoC._2015;

public class Day5
{
	public Day5(){

        Console.WriteLine("\n" + "".PadLeft(Console.WindowWidth / 2, '='));
        string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

        //Example..
        if (false)
		{
            int Exs = 0;

            input = ["ugknbfddgicrmopn"];
			Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1}");
			input = ["qjhvhtzxzqqjkmpb"];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

			input = ["haegwjzuvuyypxyu"];
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 0}");
			input = ["uurcxstgmygtbstg"];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 0}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); //258
		Puzzle2(); // 53 NOT 34
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day5_puzzle";
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
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{5}/input").Result;

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
		/* atleast 3 vowels (aeiou)
		   one double letter (aa, xx)
		   no contain ab, cd, pq, xy
		*/
		foreach(var str in input)
		{
			res += Regex.Count(str,"[aeiou]")>=3 && Regex.Count(str,"ab|cd|pq|xy")==0 &&  Regex.Count(str, "(\\w)\\1")>0 ? 1 : 0;
				//(\w) = match letter .. \1 = matches prev capture group <brackets>
		}

		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;

		//CODE HERE
		/*
			double w o overlap i.e. (xyxy or aaaa) NOT (aaa)
			one repeat letter with 1 between (beb | aaa)
		*/
		foreach(var s in input)
		{
				// ?<> names the () capture group so can refer by \k<name> instead of \1-99..
			res += Regex.Count(s, "(?<pair>(\\w){2}).*\\k<pair>") >0 && Regex.Count(s, "(?<chr>\\w)[^\\1]\\k<chr>") >0 ? 1 : 0;
		}

		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}
}
