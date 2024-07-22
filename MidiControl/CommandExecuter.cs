using System.Diagnostics;

namespace MidiControl;

public static class CommandExecuter
{
    private static HashSet<int> _explorerLaunchChord = [60, 63, 67];
    
    public static void TryLaunchCommand(HashSet<int> inputSet)
    {
        if (inputSet.SetEquals(_explorerLaunchChord))
        {
            LaunchExplorer();
        }
    }

    private static void LaunchExplorer()
    {
        try
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "explorer";
            process.Start();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}