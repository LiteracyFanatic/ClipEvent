using System.Diagnostics;

namespace ClipEvent;

public static class XClip
{
    public static string GetTarget(string selection, string target)
    {
        var psi = new ProcessStartInfo("xclip", ["-o", "-target", target, "-selection", selection])
        {
            RedirectStandardOutput = true
        };
        using var process = Process.Start(psi);
        if (process == null)
        {
            throw new Exception("Failed to start xclip");
        }

        return process.StandardOutput.ReadToEnd();
    }

    public static string[] GetTargets(string selection)
    {
        var lines = GetTarget(selection, "TARGETS").Split('\n');
        var targetsToIgnore = new[] { "", "TARGETS", "MULTIPLE", "TIMESTAMP" };
        var targets = lines.Where(l => !targetsToIgnore.Contains(l)).ToArray();
        return targets;
    }

    public static SortedDictionary<string, string> GetSelection(string selection)
    {
        var targets = new SortedDictionary<string, string>();
        foreach (var target in GetTargets(selection))
        {
            targets.Add(target, GetTarget(selection, target));
        }

        return targets;
    }
}
