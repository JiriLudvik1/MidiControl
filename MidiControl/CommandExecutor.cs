using System.Diagnostics;
using MidiControl.WindowManipulatorDir;

namespace MidiControl;

public static class CommandExecutor
{
    
    private static readonly HashSet<int> _explorerLaunchChord = [60, 63, 67];
    private static readonly HashSet<int> _chromeLaunchChord = [60, 64, 67];
    private static readonly HashSet<int> _azLoginRunChord = [60, 64 ];
    private static readonly HashSet<int> _closeFocusedWindowChord = [48];
    private static readonly HashSet<int> _cycleFocusedWindowBackwardChord = [50];
    private static readonly HashSet<int> _cycleFocusedWindowChord = [51];
    private static readonly HashSet<int> _toggleMaximizeWindowChord = [52];

    private static readonly Dictionary<HashSet<int>, Action> _actionsDictionary = new(new HashSetComparer())
    {
        {_explorerLaunchChord, LaunchExplorer},
        {_chromeLaunchChord, LaunchChrome},
        {_azLoginRunChord, RunAzLogin},
        {_closeFocusedWindowChord, WindowManipulator.CloseFocusedWindow},
        {_cycleFocusedWindowChord, WindowManipulator.CycleWindowsForward},
        {_cycleFocusedWindowBackwardChord, WindowManipulator.CycleWindowsBackwards},
        {_toggleMaximizeWindowChord, WindowManipulator.ToggleMaximizeCurrentWindow}
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

    private static void RunAzLogin()
    {
        try
        {
            var processStartInfo = new ProcessStartInfo()
            {
              UseShellExecute = true,
              FileName = "powershell",
              Arguments = "az login"
            };

            Process.Start(processStartInfo);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
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