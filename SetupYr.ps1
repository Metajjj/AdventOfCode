<#
	multi line comment
#>

$AoC = $PSScriptRoot+"/";

# $year = 2015;
$year = Read-Host -Prompt "Enter year";
$DAY = Read-Host -Prompt "Enter num of days (0-x) | empty = 25 (max)";
if([String]::IsNullOrWhiteSpace($DAY)){
	$DAY=25;
}

#Create dir if doesn't exist
If(-not (Test-Path -Path "$AoC/$year") ){
	New-Item "$AoC/$year" -ItemType Directory;
}

for($i = 1; $i -le $DAY; $i++){

	#If exists, skip!! no overwriting!
	If(Test-Path -Path "$year/Day$i.cs"){
		continue;
	}

    Set-Content -Path "$year/Day$i.cs" -Value  @"
namespace AoC._$year;

public class Day$i
{
	public Day$i(){

		Console.WriteLine("\n"+"".PadLeft(Console.WindowWidth/2, '='));
		string day = this.GetType().Name; Console.CursorTop--;
        Console.WriteLine(day.PadLeft(Console.WindowWidth / 4 - day.Length / 4, '='));

		//Example..
		if (false)
		{
            int Exs = 0;
            
			Console.WriteLine($"===== Puzzle part {++Exs} =====");
			input = [""];
			Console.WriteLine($"\tSuccess : {Puzzle1() == 1}");
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
		+"/$year/$($year)_day$($i)_puzzle";
		//Year per puzzle sets

	string sessionId = Secret.secret; 

	String[] input = [];
	
	int Puzzle1(){
		//Puzzle1
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
				var res = client.GetAsync($"https://adventofcode.com/{$year}/day/{$i}/input").Result;

				File.WriteAllText(puzzlePath, res.Content.ReadAsStringAsync().Result.Trim());
			}
		}

		input = File.ReadAllLines(puzzlePath);
	} 	
}
"@
};

#Setup the Year File to run all the days

$i=0;
	Set-Content -Path "$year/$year.cs" -Value  @"
namespace AoC._$year;

public class _$year
{
	public void Start()
	{
		//Decorating Year to stand out
			//BufferWidth = includes offscreen, window = curr visible??
		Console.WriteLine("".PadLeft(Console.WindowWidth, '='));
		string year = this.GetType().Name.Substring(1); Console.CursorTop--;
		Console.WriteLine(year.PadLeft(Console.WindowWidth/2 - year.Length/2,'='));

		//Setup Puzzle storing folder else err on file create!
		if(! Directory.Exists(Directory.GetParent(Assembly.GetEntryAssembly().Location) + "/$year"))
		{
			Directory.CreateDirectory(Directory.GetParent(Assembly.GetEntryAssembly().Location) + "/$year");
		}


	   	$(
			$o="";
			foreach($item in Get-ChildItem -Path "$year/Day*.cs" ){
				$o += "new $($item.Name.Split(".")[0])(); Console.WriteLine(); GC.Collect();`n`t`t";
				#ForEach auto adds whitespaceto `n ??
			}
			$o;
		)
	}
}
"@