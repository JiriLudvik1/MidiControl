using System.Runtime.InteropServices;
using System.Text;

namespace MidiControl.WindowManipulatorDir;

public static partial class WindowManipulator
{
    // Import the necessary Windows API functions
    [DllImport("user32.dll")]
    private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    // Delegate to filter which windows to enumerate
    private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    private static readonly List<IntPtr> _windowHandles = new List<IntPtr>();
    private static int _currentIndex = 0;

    static bool EnumWindowsCallback(IntPtr hWnd, IntPtr lParam)
    {
        if (IsWindowVisible(hWnd))
        {
            _windowHandles.Add(hWnd);
        }
        return true; // Continue enumerating windows
    }

    public static void CycleWindowsForward()
    {
        // Clear previous list of handles and re-enumerate windows
        _windowHandles.Clear();
        EnumWindows(EnumWindowsCallback, IntPtr.Zero);

        if (_windowHandles.Count == 0)
            return;

        // Bring the next window to the foreground
        SetForegroundWindow(_windowHandles[_currentIndex]);

        // Move to the next window in the list
        _currentIndex = (_currentIndex + 1) % _windowHandles.Count;
        Console.WriteLine($"Current index is: {_currentIndex}");
    }

    public static void CycleWindowsBackwards()
    {
        // Clear previous list of handles and re-enumerate windows
        _windowHandles.Clear();
        EnumWindows(EnumWindowsCallback, IntPtr.Zero);

        if (_windowHandles.Count == 0)
            return;
        
        // Bring the previous window to the foreground
        SetForegroundWindow(_windowHandles[_currentIndex]);

        // Move to the previous window in the list
        _currentIndex = (_currentIndex - 1 + _windowHandles.Count) % _windowHandles.Count;

        Console.WriteLine($"Current index is: {_currentIndex}");
    }
}

