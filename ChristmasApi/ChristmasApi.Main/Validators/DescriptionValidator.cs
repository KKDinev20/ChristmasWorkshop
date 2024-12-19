namespace ChristmasApi.Main.Validators;

using System.Text.RegularExpressions;

public static class DescriptionValidator
{
    public static bool CheckDescription(string description)
    {
        if (!CheckForSymbols(description) ||
            !CheckForKeyWords(description) ||
            string.IsNullOrWhiteSpace(description))
        {
            return false;
        }

        return true;
    }

    private static bool CheckForSymbols(string description)
    {
        if (!Regex.IsMatch(description, @"^[a-zA-Z0-9\s.,?!]*$"))
        {
            return false;
        }

        return true;
    }

    private static bool CheckForKeyWords(string description)
    {
        foreach (var c in description)
        {
            Console.WriteLine($"Character: {c}, ASCII: {(int)c}");
        }

        string pattern = @"\b(alert|console|script|eval|javascript|const)\b";
        if (Regex.IsMatch(description, pattern, RegexOptions.IgnoreCase))
        {
            return false;
        }

        return true;
    }
}