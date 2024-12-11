using System.Text.RegularExpressions;

namespace AoC._2015;

public class Day2
{
	public Day2(){
        Console.WriteLine("\n" + "".PadLeft(Console.WindowWidth / 2, '='));
        string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

        GetInput();

		Example();
		Puzzle1(); 
		Puzzle2(); 
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day2_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; //Valid till 2025-06

	String[] input = [];
	
	void GetInput(){
			//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Cookie", "session=" + sessionId);
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{2}/input").Result;

				//var res = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent("Test results true") };

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result);
			}
		}

		input = File.ReadAllLines(puzzlePath);
	}

	void Example() {

		var res = 58;
		

		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res} => Success : {res.Equals(
		 Puzzle1([
			 "2x3x4"
		 ])
		)}");


        Console.WriteLine(Puzzle1(["1x1x10"]) == 43);
	}

	int Puzzle1(String[] input = null){
		//Puzzle1
		bool replaceMe = input == null || input.Count() < 1;

        input = replaceMe ? this.input : input;

		var res = 0;

		/*
		 2x3x4 => 2x(3x4) 2x(2x4) 2x(2x3) => 52 + 6 (smallest) => 58
		*/
		foreach (var s in input)
		{
            List<int> cal = [int.Parse(s.Split("x")[0]), int.Parse(s.Split("x")[1]), int.Parse(s.Split("x")[2])];

            cal.Sort();

            res += 2 * cal[0] * cal[1] + 2 * cal[1] * cal[2] + 2 * cal[0] * cal[2];
			res += cal[0] * cal[1];
        }


		if (replaceMe)
		{ Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}"); }
		return res;


        /*TODO
			ans < 1605716
			ans = 1598415
		*/
    }

    void Puzzle2(){
		//Puzzle2
		var res = 0;

        foreach (var s in input)
        {
            List<int> cal = [int.Parse(s.Split("x")[0]), int.Parse(s.Split("x")[1]), int.Parse(s.Split("x")[2])];

            cal.Sort();

			res += 2 * cal[0] + 2 * cal[1] + cal.Aggregate(1, (s, c) => s * c);
        }

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");

        //ans = 3812909
    }
}
