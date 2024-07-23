using System.Runtime.InteropServices;

namespace MidiControl.WindowManipulatorDir;

public static partial class WindowManipulator
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetForegroundWindow();
    
    [DllImport("user32.dll")]
    private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    
    // Define the WM_CLOSE message
    private const uint WM_CLOSE = 0x0010;
    
    public static void CloseFocusedWindow()
    {
        // Get the handle of the foreground window
        IntPtr hWnd = GetForegroundWindow();

        // Check if we got a valid window handle
        if (hWnd != IntPtr.Zero)
        {
            // Send the WM_CLOSE message to the window
            PostMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("Focused window closed.");
        }
        else
        {
            Console.WriteLine("No window is currently in focus.");
        }
    }
}