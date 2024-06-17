using System.Text.Json;
using ClipEvent;

var oldPrimary = XClip.GetSelection("primary");
var oldClipboard = XClip.GetSelection("clipboard");

while (true)
{
    ClipNotify.WaitForEvent();

    var primary = XClip.GetSelection("primary");
    var clipboard = XClip.GetSelection("clipboard");
    var windowProperties = XProp.GetWindowProperties(XdoTool.GetActiveWindow());

    if (!primary.SequenceEqual(oldPrimary))
    {
        var data = new ClipboardEvent("primary", windowProperties, primary);
        var json = JsonSerializer.Serialize(data);
        Console.WriteLine(json);
    }

    if (!clipboard.SequenceEqual(oldClipboard))
    {
        var data = new ClipboardEvent("clipboard", windowProperties, clipboard);
        var json = JsonSerializer.Serialize(data);
        Console.WriteLine(json);
    }

    oldPrimary = primary;
    oldClipboard = clipboard;
}
