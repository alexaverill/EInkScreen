/*
This is a wrapper around a python library to control an Inky E-Ink screen.

*/
using System.Diagnostics;

class InkyController : IScreenController
{
    public void RunCommandWithBash(string command)
    {
        var psi = new ProcessStartInfo();
        psi.FileName = "/bin/bash";
        psi.Arguments = command;
        psi.RedirectStandardOutput = false;
        psi.UseShellExecute = false;
        psi.CreateNoWindow = false;
        psi.WorkingDirectory = "./scripts";

        using var process = Process.Start(psi);

        // process.WaitForExit();

        // var output = process.StandardOutput.ReadToEnd();
        // Console.WriteLine(output);
        // return output;
    }
    public void Clean()
    {
        throw new NotImplementedException();
    }

    public void SetColor()
    {
        throw new NotImplementedException();
    }

    public void SetImage(string imagePath)
    {
        var command = $"image.sh {imagePath} ";
        // Console.WriteLine(command);
        RunCommandWithBash(command);
    }
}