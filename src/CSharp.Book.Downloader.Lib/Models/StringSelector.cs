namespace CSharp.Book.Downloader.Lib.Models;

/// <summary>
/// Structure that contains the required information to extract a well formed string value from the DOM.
/// </summary>
public class StringSelector {
    /// <summary>
    /// The selector string, used to locate element(s).
    /// </summary>
    public required string Value { get; set; }
    /// <summary>
    /// The type of selector.
    /// </summary>
    public SelectorType Type { get; set; }
    /// <summary>
    /// An optional attribute to extract information from, if this is not provided the element `Text` will be used.
    /// </summary>
    public string? Attribute { get; set; }
    /// <summary>
    /// An optional regex pattern to extract exact information out of the element attribute / text.
    /// </summary>
    public string? Regex { get; set; }
    /// <summary>
    /// An optional template to insert the extracted information from in order to generate custom values.
    /// <para>Note: A wildcard '*' in the template string will be replaced with the extracted value.</para>
    /// </summary>
    public string? Template { get; set; }
}
