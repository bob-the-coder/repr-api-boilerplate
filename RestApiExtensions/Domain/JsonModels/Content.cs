namespace Domain.JsonModels;

public interface IViewContent
{
    bool IsValid { get; }
}
public enum TextContentType { Title, Subtitle, Section, Paragraph, Quote }
public enum FontOptions { Regular, Italic, Bold }
public enum HorizontalAlignment { Left, Center, Right }

public record ViewContentContainer(
    IViewContent[] Children
) : IViewContent
{
    public bool IsValid => Children.All(child => child.IsValid);
}

public record TextViewContent(
    string Text,
    TextContentType Type,
    HorizontalAlignment HorizontalAlignment,
    FontOptions FontOptions
) : IViewContent
{
    public bool IsValid => !string.IsNullOrWhiteSpace(Text);
}

public record ImageViewContent(
    string Url,
    int? Height,
    int? Width
) : IViewContent
{
    public bool IsValid => !string.IsNullOrWhiteSpace(Url);
}

public record ListViewContent(
    string[] Items
) : IViewContent
{
    public bool IsValid => Items.All(item => !string.IsNullOrWhiteSpace(item));
}