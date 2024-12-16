using System.Linq;

namespace AoC._2024;

public class Day4
{
	public Day4(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (false)
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
				"XAX",
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
        Puzzle2(); // 1868
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
        int wl = word.Length-1;

		var input2 = input.Select(line => line.ToList()).ToList();
            //Make it multidimensional then can use i and j
        
		for(int i = 0; i < input2.Count; i++)
		{
			for(int j = 0; j < input2[i].Count; j++)
			{
				if(input2[i][j] == word[0])
				{
					//Need to check all around i.e. -word.Length => +word.Length -- only check for "MAS" other 3
					if (i >= wl) //Allowed to check up
                    {
                        //res += String.Concat( input2.GetRange(i - wl, wl+1 ).Select((row,index) => row[j]) ) == word ? 1 : 0;


                        if ($"{input2[i][j]}{input2[i - 1][j]}{input2[i - 2][j]}{input2[i - 3][j]}" == word) { res += 1; }

                        //up-back
                        if (j >= wl) //Allowed to check backward
                        {
                            res += $"{input[i][j]}{input[i-1][j - 1]}{input[i-2][j - 2]}{input[i-3][j - 3]}" == word ? 1 : 0;
                        }

                        //up-frwd
                        if (input2[i].Count - 1 - j >= wl) //Allowed to check forward
                        {
                            res += $"{input[i][j]}{input[i -1][j - -1]}{input[i -2][j - -2]}{input[i -3][j - -3]}" == word ? 1 : 0;
                        }
                    }

                    if (input2.Count-1 -i >= wl) //Allowed to check down
                    {
                        res += $"{input[i][j]}{input[i - - 1][j]}{input[i - - 2][j]}{input[i - - 3][j]}" == word ? 1 : 0;

                        //Logic for diag down-back
                        if (j >= wl) //Allowed to check backward
                        {
                            res += $"{input[i][j]}{input[i - - 1][j - 1]}{input[i - - 2][j - 2]}{input[i - - 3][j - 3]}" == word ? 1 : 0;
                        }
                        //frwd-down
                        if (input2[i].Count - 1 - j >= wl) //Allowed to check forward
                        {
                            res += $"{input[i][j]}{input[i - -1][j - -1]}{input[i - -2][j - -2]}{input[i - -3][j - -3]}" == word ? 1 : 0;
                        }
                    }

                    if (j >= wl) //Allowed to check backward
					{
						res += $"{input[i][j]}{input[i][j - 1]}{input[i][j - 2]}{input[i][j - 3]}" == word ? 1 : 0;
					}

					if (input2[i].Count-1 - j >= wl) //Allowed to check forward
                    {
                        res += $"{input[i][j]}{input[i][j - - 1]}{input[i][j - - 2]}{input[i][j - - 3]}" == word ? 1 : 0;
                    }
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
        string word = "MAS";

        var input2 = input.Select(line => line.ToList()).ToList();
        //Make it multidimensional then can use i and j

        for (int i = 0; i < input2.Count; i++)
        {
            for (int j = 0; j < input2[i].Count; j++)
            {
                if (input2[i][j] == word[1]) //Found 'A' check corners!
                {
                    /*  m.s .m.
                        .a. mas
                        m.s .s.
                    
                        Always a offset of 1 up down left right!
                    */
                    if(i-1 >=0 && i + 1 < input2.Count && j-1>=0 && j+1 < input2[i].Count)
                    {   //Within the grid/array (not at border) is 'A'

                        res += (

                            (
                            //TL=> BR
                            input2[i - 1][j-1] =='M' && input2[i+1][j+1] == 'S'
                            ||
                            //BR => TL
                            input2[i + 1][j + 1] == 'M' && input2[i - 1][j - 1] == 'S'
                            )

                            &&

                            (
                            input2[i+1][j - 1] == 'M' && input2[i-1][j + 1] == 'S'
                            ||
                            input2[i - 1][j + 1] == 'M' && input2[i + 1][j - 1] == 'S'
                            )

                            //Shape of 'X' - no need for + shape checks

                            ) ? 1 : 0;
                    }
                }
            }
        }


        Console.WriteLine($"{this.GetType().Name}::{MethodBase.GetCurrentMethod().Name} : {res}");
		return res;
	}

	void GetInput(){
			//If file doesnt exist.. grab http else  is file
		if (!File.Exists(puzzlePath))
		{
			using (HttpClient client = new HttpClient() )
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
