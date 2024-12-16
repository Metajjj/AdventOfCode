namespace AoC._2024;

public class _2024
{
	public void Start()
	{
		//Decorating Year to stand out
			//BufferWidth = includes offscreen, window = curr visible??
		Console.WriteLine("".PadLeft(Console.WindowWidth, '='));
		string year = this.GetType().Name.Substring(1); Console.CursorTop--;
		Console.WriteLine(year.PadLeft(Console.WindowWidth/2 - year.Length/2,'='));

		//Setup Puzzle storing folder else err on file create!
		if(! Directory.Exists(Directory.GetParent(Assembly.GetEntryAssembly().Location) + "/2024"))
		{
			Directory.CreateDirectory(Directory.GetParent(Assembly.GetEntryAssembly().Location) + "/2024");
		}


	   	new Day1(); Console.WriteLine(); GC.Collect();
		new Day2(); Console.WriteLine(); GC.Collect();
		new Day3(); Console.WriteLine(); GC.Collect();
		new Day4(); Console.WriteLine(); GC.Collect();
		new Day5(); Console.WriteLine(); GC.Collect();
		
	}
}
