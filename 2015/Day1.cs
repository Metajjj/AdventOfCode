namespace AoC._2015;

public class Day1
{
	public Day1(){
        Console.WriteLine("\n" + "".PadLeft(Console.WindowWidth / 2, '='));
        string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

        GetInput();

		Puzzle1(); 
		Puzzle2(); 
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day1_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];

	void GetInput()
	{
		//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Cookie", "session=" + sessionId);
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{1}/input").Result;

				//var res = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent("Test results true") };

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result);
			}
		}

		input = File.ReadAllLines(puzzlePath);

	}

	void Puzzle1(){
		//Puzzle1

		var input = String.Join("", this.input);

        var res = input.Count(c => c+"" == "(") - input.Count(c => c+"" == ")");

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
	}

	void Puzzle2(){
        //Puzzle2
        var input = String.Join("", this.input);

        var res = 0;

        for (int i = 0; i < input.Length; i++)
        {
            res += input[i] + "" == "(" ? 1 : -1;
            if (res < 0) { res = i + 1; break; }
        }

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
    }
}
