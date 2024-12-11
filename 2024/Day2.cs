using System.ComponentModel;

namespace AoC._2024;

public class Day2
{
	public Day2(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (true)
		{
            int Exs = 0;
            input = ["7 6 4 2 1",
				"1 2 7 8 9",
				"9 7 6 2 1",
				"1 3 2 4 5",
				"8 6 4 4 1",
				"1 3 6 7 9",
                "42 44 47 49 51 52 54 52"	//Failing my logic?!
            ];
			Console.WriteLine("===== Example input " + ++Exs + " =====");
			Console.WriteLine($"\tSuccess : {Puzzle1() == 2}");
			Console.WriteLine($"\tSuccess : {Puzzle2() == 5}");

			Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); //282  //233 < ans < 324 && ans != 302
        Puzzle2();  //329 < ans

        //P2 act 349!
    }
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2024/2024_day2_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];
	
	int Puzzle1(){
		//Puzzle1
		var res = 0;

		//CODE HERE
		/*
			ASC/DESC order
			UNIQUE/DISTINCT
			incr/decre 1-3
		*/

		var input2 = input.Select(i => Regex.Split(i, "\\s+").Select(int.Parse).ToList()).ToList();
			//Transform into int vers

		var orderSafe = input2.Where(report =>
			report.SequenceEqual(report.Order()) || report.SequenceEqual(report.OrderDescending())
		).ToList();
		//Returns with safe orders (ASC/DESC)

		List<List<int>> RangeSafe = [];
		orderSafe.ForEach((report) => {
			int cnt = 0;
			report.Aggregate(report[0]-1, (prev, curr) =>
			{
				if( Enumerable.Range(1,3).Contains( Math.Abs(prev-curr) ))
				{ 
					if (++cnt == report.Count) //SAFE!
					{ RangeSafe.Add(report); }
				}
				return curr;
			});
		});

		res = RangeSafe.Count; 

                        //Convert to int list from the beginning
        /*foreach (var input in input2)
		{
			
            int x = int.MinValue;
            for (int i = 0; i < input.Count; i++)
            {
                var I = input[i];
                if (x != int.MinValue)
                {
                    incrSafe = Math.Abs(x - I) <= 3 && Math.Abs(x - I) >= 1;
                    if (!incrSafe || isAsc == input[i] < input[i - 1]) { break; }
                    //If isnt in order or isnt increment.. leave
                }
                x = I;

				if (x == input.Last()) { res++; }
            }
        }*/
		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2()
	{
        //Puzzle2
       

        //COMPARE
        var realRes = input.Select(j => j.Split(" ").ToList()
			.Select(t => int.Parse(t)).ToList()) 
				//ToList<Ints> for each report in the input

			.Select(j => //Foreach report
				Enumerable.Range(0, j.Count) //For nums in report..
				.Select(del => j.Where((val, index) => index != del).ToList())
					//Generates new list w 1 missing element for all possible combinations
					//i.e. del=0 .. generate list w o element 0, then del=1.. repeat

				.SelectMany(j => new List<List<int>>() { j, 
					j.Select(x => -x).ToList()  
				//Makes individal reports include neg so if DESC then works < comparison
					//then shifts up a layer into normal list rather than internal list of the 2

				}).ToList() //turns into pos n neg vers => flattens into 1 collection of possible combinations neg+pos

            //======Above = restructure | Below = check======

            ).Where(j => j.Any(
				//any neg/pos match criteria

				x => Enumerable.Range(0, x.Count - 1)
				.All(t => x[t] < x[t + 1] && x[t + 1] - x[t] <= 3))
					//DES/ASC order ++ <=3 (fails if 2 elements are same)
						//All nums match criteria = negs + nrms versions
			).ToList();


		var res = realRes.Count;

        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");

        //MINE
        /*foreach (var input in input.Select(i => Regex.Split(i, "\\s+").Select(int.Parse).ToList())
		)
		{
			bool isAsc = input[0] < input[1]; bool incrSafe = true;
			int x = int.MinValue;
			for (int i = 0; i < input.Count; i++)
			{
				var I = input[i];
				if (x != int.MinValue)
				{
					incrSafe = Math.Abs(x - I) <= 3 && Math.Abs(x - I) >= 1;
					if (!incrSafe || isAsc == input[i] < input[i - 1]) { return i; }
					//If isnt in order or isnt increment.. leave
				}
				x = I;
			}

			return -1;
		}*/



        //COMPARE


        return res;

	}

	void GetInput(){
			//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Cookie", "session=" + sessionId);
				var res = client.GetAsync($"https://adventofcode.com/{2024}/day/{2}/input").Result;

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	
}
