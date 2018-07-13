using System;
using System.Text.RegularExpressions;

namespace FakeNewsDetectorApp
{
    /// <summary>
    /// Methods to remove HTML from strings.
    /// </summary>
    public static class HtmlRemoval
    {
        //    /// <summary>
        //    /// Remove HTML from string with Regex.
        //    /// </summary>
        //    public static string StripTagsRegex(string source)
        //    {
        //        return Regex.Replace(source, "<.*?>", string.Empty);
        //    }

        //    /// <summary>
        //    /// Compiled regular expression for performance.
        //    /// </summary>
        //    static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        //    /// <summary>
        //    /// Remove HTML from string with compiled Regex.
        //    /// </summary>
        //    public static string StripTagsRegexCompiled(string source)
        //    {
        //        return _htmlRegex.Replace(source, string.Empty);
        //    }

        //    /// <summary>
        //    /// Remove HTML tags from string using char array.
        //    /// </summary>
        //    public static string StripTagsCharArray(string source)
        //    {
        //        char[] array = new char[source.Length];
        //        int arrayIndex = 0;
        //        bool inside = false;

        //        for (int i = 0; i < source.Length; i++)
        //        {
        //            char let = source[i];
        //            if (let == '<')
        //            {
        //                inside = true;
        //                continue;
        //            }
        //            if (let == '>')
        //            {
        //                inside = false;
        //                continue;
        //            }
        //            if (!inside)
        //            {
        //                array[arrayIndex] = let;
        //                arrayIndex++;
        //            }
        //        }
        //        return new string(array, 0, arrayIndex);
        //    }

        private static Regex _tags = new Regex("<[^>]*(>|$)",
    RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled);
        private static Regex _whitelist = new Regex(@"
    ^</?(b(lockquote)?|code|d(d|t|l|el)|em|h(1|2|3)|i|kbd|li|ol|p(re)?|s(ub|up|trong|trike)?|ul)>$|
    ^<(b|h)r\s?/?>$",
            RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
        private static Regex _whitelist_a = new Regex(@"
    ^<a\s
    href=""(\#\d+|(https?|ftp)://[-a-z0-9+&@#/%?=~_|!:,.;\(\)]+)""
    (\stitle=""[^""<>]+"")?\s?>$|
    ^</a>$",
            RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
        private static Regex _whitelist_img = new Regex(@"
    ^<img\s
    src=""https?://[-a-z0-9+&@#/%?=~_|!:,.;\(\)]+""
    (\swidth=""\d{1,3}"")?
    (\sheight=""\d{1,3}"")?
    (\salt=""[^""<>]*"")?
    (\stitle=""[^""<>]*"")?
    \s?/?>$",
            RegexOptions.Singleline | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);


        /// <summary>
        /// sanitize any potentially dangerous tags from the provided raw HTML input using 
        /// a whitelist based approach, leaving the "safe" HTML tags
        /// CODESNIPPET:4100A61A-1711-4366-B0B0-144D1179A937
        /// </summary>
        public static string Sanitize(string html)
        {
            if (String.IsNullOrEmpty(html)) return html;

            string tagname;
            Match tag;

            // match every HTML tag in the input
            MatchCollection tags = _tags.Matches(html);
            for (int i = tags.Count - 1; i > -1; i--)
            {
                tag = tags[i];
                tagname = tag.Value.ToLowerInvariant();

                if (!(_whitelist.IsMatch(tagname) || _whitelist_a.IsMatch(tagname) || _whitelist_img.IsMatch(tagname)))
                {
                    html = html.Remove(tag.Index, tag.Length);
                    System.Diagnostics.Debug.WriteLine("tag sanitized: " + tagname);
                }
            }

            return html;
        }

    }
}
