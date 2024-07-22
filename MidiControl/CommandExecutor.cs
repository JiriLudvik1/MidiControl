using System.Diagnostics;

namespace MidiControl;

public static class CommandExecutor
{
    
    private static readonly HashSet<int> _explorerLaunchChord = [60, 63, 67];
    private static readonly HashSet<int> _chromeLaunchChord = [60, 64, 67];

    private static Dictionary<HashSet<int>, Action> _actionsDictionary = new(new HashSetComparer())
    {
        {_explorerLaunchChord, LaunchExplorer},
        {_chromeLaunchChord, LaunchChrome}
    };

    public static void TryLaunchCommand(HashSet<int> inputSet)
    {
        var matchingHashSet = _actionsDictionary.FirstOrDefault(kvp => kvp.Key.SetEquals(inputSet));
        if (matchingHashSet.Equals(default(KeyValuePair<HashSet<int>, Action>)))
        {
            Console.WriteLine("No match found for the chord");
            return;
        }

        Task.Run(matchingHashSet.Value);
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
    
    private static void LaunchChrome()
    {
        try
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "chrome";
            process.Start();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}