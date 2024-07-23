using System.Runtime.InteropServices;

namespace MidiControl.WindowManipulatorDir;

public static partial class WindowManipulator
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll")]
    private static extern bool IsZoomed(IntPtr hWnd);

    // Constants for ShowWindow function
    private const int SW_RESTORE = 9;
    private const int SW_MAXIMIZE = 3;

    public static void ToggleMaximizeCurrentWindow()
    {
        // Get the handle of the current foreground window
        IntPtr hWnd = GetForegroundWindow();

        if (hWnd != IntPtr.Zero)
        {
            // Check if the window is currently maximized
            if (IsZoomed(hWnd))
            {
                // If it is maximized, restore it
                ShowWindow(hWnd, SW_RESTORE);
                Console.WriteLine("Window restored.");
            }
            else
            {
                // If it is not maximized, maximize it
                ShowWindow(hWnd, SW_MAXIMIZE);
                Console.WriteLine("Window maximized.");
            }
        }
        else
        {
            Console.WriteLine("No window is currently in focus.");
        }
    }
}