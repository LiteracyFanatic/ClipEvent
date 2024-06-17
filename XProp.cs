using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ClipEvent;

public record WindowProperties(int Id, int Pid, string Name, string Instance, string Class);

public static class XProp
{
    public static WindowProperties GetWindowProperties(int windowId)
    {
        var psi = new ProcessStartInfo("xprop", ["-id", windowId.ToString()]) { RedirectStandardOutput = true };
        using var process = Process.Start(psi);
        if (process == null)
        {
            throw new Exception("Failed to start xdotool");
        }

        var output = process.StandardOutput.ReadToEnd();

        var name = Regex.Match(output, "WM_NAME\\(STRING\\) = \"(.*)\"").Groups[1].Value;
        var pid = Regex.Match(output, "_NET_WM_PID\\(CARDINAL\\) = (\\d+)").Groups[1].Value;
        var wmClass = Regex.Match(output, "WM_CLASS\\(STRING\\) = \"(.*)\", \"(.*)\"");
        var instance = wmClass.Groups[1].Value;
        var windowClass = wmClass.Groups[2].Value;

        return new WindowProperties(windowId, int.Parse(pid), name, instance, windowClass);
    }
}
