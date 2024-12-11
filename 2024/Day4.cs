using System.Linq;

namespace AoC._2024;

public class Day4
{
	public Day4(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (true)
		{
            int Exs = 0;
            
			Console.WriteLine($"===== Puzzle part {++Exs} =====");
            input = [
                "MMMSXXMASM",
                "MSAMXMSMSA",
                "AMXSXMAAMM",
                "MSAMASMSMX",
                "XMASAMXAMM",
                "XXAMMXXAMA",
                "SMSMSASXSS",
                "SAXAMASAAA",
                "MAMMMXMMMM",
                "MXMXAXMASX",
            ];
            Console.WriteLine($"\tSuccess : {Puzzle1() == 18}");
			input=[
				"MXS",
				"XAX" +
				"MXS"
				];
			Console.WriteLine($"\tSuccess : {Puzzle2() == 1}");

            Console.WriteLine($"===== Example input {++Exs} =====");
            input = [
				"XMAS",
                "XMAS",
                "XMAS",
                "XMAS",
            ];
            Console.WriteLine($"\tSuccess : {Puzzle1() == 6}");
            input = [
                "MMMSXXMASM",
                "MSAMXMSMSA",
                "AMXSXMAAMM",
                "MSAMASMSMX",
                "XMASAMXAMM",
                "XXAMMXXAMA",
                "SMSMSASXSS",
                "SAXAMASAAA",
                "MAMMMXMMMM",
                "MXMXAXMASX",
            ];
            Console.WriteLine($"\tSuccess : {Puzzle2() == 9}");


            Console.WriteLine($"===== Actual input =====");
		}

        //Nrml..
        GetInput();
        Puzzle1(); // 2447   2347 < ans
        Puzzle2(); 
	}
	
	String puzzlePath = Directory.GetParent(Assembly.GetEntryAssembly().Location)
		+"/2024/2024_day4_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];
	
	int Puzzle1(){
		//Puzzle1
		var res = 0;

		//CODE HERE
		/*
			Find XMAS (any direction, can overlap) - in wordsearch
		*/

		string word = "XMAS";

		var input2 = input.Select(line => line.Select(s=>s+"").ToList()).ToList();
            //Make it multidimensional then can use i and j
        
		for(int i = 0; i < input2.Count; i++)
		{
			for(int j = 0; j < input2[i].Count; j++)
			{
				if(input2[i][j] == "X")
				{
					//Need to check all around i.e. -word.Length => +word.Length -- only check for "MAS" other 3
					if (i >= 3) //Allowed to check up
                    {
						res += $"{input[i][j]}{input[i - 1][j]}{input[i - 2][j]}{input[i - 3][j]}" == word ? 1 : 0;

                        //Logic for diag up-back
                        if (j >= 3) //Allowed to check backward
                        {
                            res += $"{input[i][j]}{input[i-1][j - 1]}{input[i-2][j - 2]}{input[i-3][j - 3]}" == word ? 1 : 0;
                        }

                        //frwd-up
                        if (input2[i].Count - 1 - j >= 3) //Allowed to check forward
                        {
                            res += $"{input[i][j]}{input[i -1][j - -1]}{input[i -2][j - -2]}{input[i -3][j - -3]}" == word ? 1 : 0;
                        }
                    }

                    if (input2.Count-1 -i >= 3) //Allowed to check down
                    {
                        res += $"{input[i][j]}{input[i - - 1][j]}{input[i - - 2][j]}{input[i - - 3][j]}" == word ? 1 : 0;

                        //Logic for diag down-back
                        if (j >= 3) //Allowed to check backward
                        {
                            res += $"{input[i][j]}{input[i - - 1][j - 1]}{input[i - - 2][j - 2]}{input[i - - 3][j - 3]}" == word ? 1 : 0;
                        }
                        //frwd-down
                        if (input2[i].Count - 1 - j >= 3) //Allowed to check forward
                        {
                            res += $"{input[i][j]}{input[i - -1][j - -1]}{input[i - -2][j - -2]}{input[i - -3][j - -3]}" == word ? 1 : 0;
                        }
                    }

                    if (j >= 3) //Allowed to check backward
					{
						res += $"{input[i][j]}{input[i][j - 1]}{input[i][j - 2]}{input[i][j - 3]}" == word ? 1 : 0;
					}

					if (input2[i].Count-1 - j >= 3) //Allowed to check forward
                    {
                        res += $"{input[i][j]}{input[i][j - - 1]}{input[i][j - - 2]}{input[i][j - - 3]}" == word ? 1 : 0;
                    }

					//Diag frd
                }
            }
		}
		
		Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	int Puzzle2(){
		//Puzzle2
		var res = 0;

        //CODE HERE
        /*
			find MAS as cross shape
			Can check by 3x3 sections at a time
			"A" in middle of grid
			"M" and "S" near the other pair of MAS
		*/

        var input2 = input.Select(line => line.Select(s => s + "").ToList()).ToList();

        for (int i =0; i < input2.Count -3 ; i++)
        {for (int j = 0; i < input2[i].Count - 3; j++)
            {
                                           //0,0 1,0 2,0 0,1, 1,1..
                //Split into pure string? ["X", "S2, "A", "A"]

                var grid = input2.GetRange(i, 3).Select(row => String.Join(",",row.GetRange(j,3))).ToList();

                    //GetRange to get the 3x3 sections

                //Check there is 1 A, 2 M, 2 S

            }
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
				var res = client.GetAsync($"https://adventofcode.com/{2024}/day/{4}/input").Result;

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
							//Extra "\n" at end - affects specific puzzles
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	
}
