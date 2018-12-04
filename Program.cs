using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp5_Final
{
	class Song
	{
		public string Name { get; set; }
		public string Artist { get; set; }
		public string Album { get; set; }
		public string Genre { get; set; }
		public string Size { get; set; }
		public int Time { get; set; }
		public int Year { get; set; }
		public int Plays { get; set; }
		public string getSong()
		{
			return ("	Name: " + Name + ", Artist: " + Artist + ", Album: " + Album + ", Genre: " + Genre + ", Size: " + Size + ", Time: " + Time + ", Year: " + Year + ", Plays " + Plays);
		}
	}
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("Please enter an appropriate number of arguements(2)\nEx. MusicPlaylistAnalyzer <music_playlist_file_path> <report_file_path>");
				System.Environment.Exit(0);
			}
			string Q1 = "1)Songs that recieved 200 or more plays: ";
			string Q2 = "2)Number of songs in the playlist with the Genre of 'Alternative': ";
			string Q3 = "3)Number of songs in the playlist with the Genre of 'Hip-Hop/Rap': ";
			string Q4 = "4)Songs in the playlist from the album 'Welcome to the Fishbowl': ";
			string Q5 = "5)Songs in the playlist from before 1970: ";
			string Q6 = "6)Song names in the playlist that are more than 85 characters long: ";
			string Q7 = "7)The longest song: ";
			string fileName1 = args[0];
			string fileName2 = args[1];
			bool good = false;
			while (good == false)
			{
				if (File.Exists(fileName1))
				{
					good = true;
				}
				else
				{
					Console.WriteLine("Invalid file name. Please use a valid file.");
					fileName1 = Console.ReadLine();
				}
			}
			var songs = 
				from line in System.IO.File.ReadAllLines(fileName1).Skip(1)
				let parts = line.Split('\t')
				select new Song
			{ 
				Name = parts[0], 
				Artist = parts[1], 
				Album = parts[2], 
				Genre = parts[3],
				Size = parts[4],
				Time = int.Parse(parts[5]),
				Year = int.Parse(parts[6]),
				Plays = int.Parse(parts[7])
			};
			if (!fileName2.Contains(".txt"))
			{
				fileName2 = fileName2 + ".txt";
			}
			try
			{
				StreamWriter outputer = File.AppendText(fileName2);
				outputer.WriteLine(Q1);
				var L1 =
				from song in songs
				where song.Plays > 200
				select song;
				foreach (var song in L1)
				{
					outputer.WriteLine(song.getSong());
				}
				var L2 =
					from song in songs
					where song.Genre == "Alternative"
					select song;
				Q2 = Q2 + L2.Count();
				outputer.WriteLine(Q2);
				var L3 =
					from song in songs
					where song.Genre == "Hip-Hop/Rap"
					select song;
				Q3 = Q3 + L3.Count();
				outputer.WriteLine(Q3);
				outputer.WriteLine(Q4);
				var L4 =
					from song in songs
					where song.Album == "Welcome to the Fishbowl"
					select song;
				foreach (var song in L4)
				{
					outputer.WriteLine(song.getSong());
				}
				outputer.WriteLine(Q5);
				var L5 =
					from song in songs
					where song.Year < 1970
					select song;
				foreach (var song in L5)
				{
					outputer.WriteLine(song.getSong());
				}
				outputer.WriteLine(Q6);
				var L6 =
					from song in songs
					where song.Name.Length > 85
					select song;
				foreach (var song in L6)
				{
					outputer.WriteLine("	{0}", song.Name);
				}
				var L7 =
					from song in songs
					orderby song.Time descending
					select song;
				var longestSong = L7.Cast<Song>().First();
				outputer.WriteLine(Q7);
				outputer.WriteLine(longestSong.getSong());
				outputer.Close();
				Console.WriteLine("{0} has succesfully been saved!", fileName2);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error!");
				Console.WriteLine("{0}", e);
			}
		}
	}
}
