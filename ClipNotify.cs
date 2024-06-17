using System.Runtime.InteropServices;
using X11;

namespace ClipEvent;

public static class ClipNotify
{
    public static void WaitForEvent()
    {
        var display = Xlib.XOpenDisplay(null);
        if (display == IntPtr.Zero)
        {
            throw new Exception("Failed to open display");
        }

        var root = Xlib.XDefaultRootWindow(display);

        var clipboard = XInternAtom(display, "CLIPBOARD", false);

        const IntPtr XFixesSetSelectionOwnerNotifyMask = 1;
        XFixesSelectSelectionInput(display, root, clipboard, XFixesSetSelectionOwnerNotifyMask);
        XFixesSelectSelectionInput(display, root, Atom.Primary, XFixesSetSelectionOwnerNotifyMask);
        XFixesSelectSelectionInput(display, root, Atom.Secondary, XFixesSetSelectionOwnerNotifyMask);

        var eventReturn = Marshal.AllocHGlobal(24 * sizeof(long));
        Xlib.XNextEvent(display, eventReturn);
        Marshal.FreeHGlobal(eventReturn);

        Xlib.XCloseDisplay(display);
    }

    [DllImport("libX11.so.6")]
    private static extern Atom XInternAtom(IntPtr display, string name, bool onlyIfExists);

    [DllImport("libXfixes.so.3")]
    private static extern Status XFixesSelectSelectionInput(
        IntPtr display,
        Window window,
        Atom selection,
        IntPtr eventMask);
}
