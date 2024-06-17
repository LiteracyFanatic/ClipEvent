namespace ClipEvent;

public record ClipboardEvent(string Selection, WindowProperties Window, SortedDictionary<string, string> Targets);
