using System.Diagnostics;

namespace ClipEvent;

public static class XdoTool
{
    public static int GetActiveWindow()
    {
        var psi = new ProcessStartInfo("xdotool", ["getactivewindow"]) { RedirectStandardOutput = true };
        using var process = Process.Start(psi);
        if (process == null)
        {
            throw new Exception("Failed to start xdotool");
        }

        var output = process.StandardOutput.ReadToEnd();
        return int.Parse(output);
    }
}
