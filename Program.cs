using System;
using System.Diagnostics;
using System.IO;

public class Program
{
    public static void Main()
    {
        Console.Title = "SupremeAudioCompressor | Made by https://github.com/GabryB03/";

        if (!Directory.Exists("inputs"))
        {
            Directory.CreateDirectory("inputs");
            Console.WriteLine("Please, load some audio files into the 'inputs' folder.");
            Console.WriteLine("Press the ENTER key in order to exit from the program.");
            Console.ReadLine();
            return;
        }    

        if (Directory.Exists("outputs"))
        {
            Directory.Delete("outputs");
        }

        Directory.CreateDirectory("outputs");
        Console.WriteLine("Compressing all audio files from the 'inputs' folder, please wait a while.");

        foreach (string file in Directory.GetFiles("inputs"))
        {
            string fullPath = Path.GetFullPath("outputs") + "\\" + Path.GetFileNameWithoutExtension(file) + " (" + Path.GetExtension(file).ToUpper().Substring(1) + ").mp3";
            RunFFMpeg($"-threads {Environment.ProcessorCount} -i \"{file}\" -vn -ar 44100 -ac 2 -b:a 96k -map a \"{fullPath}\"");
        }

        Console.WriteLine("Succesfully compressed all audio files from the 'inputs' folder. Exported to 'outputs' folder.");
        Console.WriteLine("Press the ENTER key in order to exit from the program.");
        Console.ReadLine();
    }

    private static void RunFFMpeg(string arguments)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg.exe",
            Arguments = arguments,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden
        }).WaitForExit();
    }
}