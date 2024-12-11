using System.Security.Cryptography;

namespace AoC._2015;

public class Day4
{
	public Day4(){

        Console.WriteLine("\n" + "".PadLeft(Console.WindowWidth / 2, '='));
        string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

        //Example..
        if (false)
		{
            int Exs = 0;
            input = ["abcdef"];
            //abcdef => abcdef609043 => MD5 hash : 000001dbbfa (starts w 5 0s)
            Console.WriteLine($"===== Example input {++Exs} =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 609043}");
			//Console.WriteLine($"\tSuccess : {Puzzle2() == -1}");

			input = ["pqrstuv"];
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1048970}");
			//Console.WriteLine($"\tSuccess : {Puzzle2() == -1}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); // 346386
        Puzzle2(); // 9958218
    }
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2015/2015_day4_puzzle";
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
				var res = client.GetAsync($"https://adventofcode.com/{2015}/day/{4}/input").Result;

				//var res = new HttpResponseMessage(System.Net.HttpStatusCode.Accepted) { Content = new StringContent("Test results true") };

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	

	int Puzzle1(){
		//Puzzle1
		var res = 0;

        //CODE HERE
        //GET MD5 hash that starts with 5 0s
        var test = MD5.Create();
        for (res = 0; res <= int.MaxValue; res++)
        {
            test.ComputeHash((input[0] +res).Select(s => Convert.ToByte(s)).ToArray());

				//Need bit converter, Convert.ToByte doesnt work!
            var HexHash = BitConverter.ToString(test.Hash).Replace("-", "");

            //Console.WriteLine(res + " : " + HexHash );

			//if( res > 609043 + 5) { break; }

            if ( HexHash.StartsWith("00000") )
            {
                break; //Found!
            }
        }

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
        //Puzzle2
        var res = 0;

        //CODE HERE
        //GET MD5 hash that starts with 5 0s
        var test = MD5.Create();
        for (res = 0; res <= int.MaxValue; res++)
        {
            test.ComputeHash((input[0] + res).Select(s => Convert.ToByte(s)).ToArray());

            //Need bit converter, Convert doesnt work!
            var HexHash = BitConverter.ToString(test.Hash).Replace("-", "");

            //Console.WriteLine(res + " : " + HexHash );

            //if( res > 609043 + 5) { break; }

            if (HexHash.StartsWith("000000"))
            {
                break; //Found!
            }
        }

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
        return res;
    }
}
