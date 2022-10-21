namespace TextCompression;

public class TextCompression
{
    public CompressedText Compress(string text)
    {
        var tokens = text.Split(' ');

        var uniqueTokens = tokens.Distinct().ToList();
        var tokenMap = uniqueTokens
            .ToDictionary(token => token, token => uniqueTokens.IndexOf(token));

        var mapped = tokens.Select(token => tokenMap[token]);
        var mappedText = string.Join(' ', mapped);

        return new CompressedText()
        {
            Text = mappedText,
            Tokens = uniqueTokens.ToArray()
        };
    }
}

public class CompressedText
{
    public string Text { get; set; } = "";
    public string[] Tokens { get; set; } = Array.Empty<string>();
}