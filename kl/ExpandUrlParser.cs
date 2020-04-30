#region License
/*
 **************************************************************
 *  Author: Rick Strahl 
 *          © West Wind Technologies, 2008-2012
 *          http://www.west-wind.com/
 * 
 * Created: 09/04/2008
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 **************************************************************  
*/
#endregion

using System.Text.RegularExpressions;

namespace DataReaderToJsonExample.JsonSerializer
{
    internal class ExpandUrlsParser
    {
        public string Target = string.Empty;
        public bool ParseFormattedLinks = false;

        /// <summary>
        /// Expands links into HTML hyperlinks inside of text or HTML.
        /// </summary>
        /// <param name="text">The text to expand</param>    
        /// <returns></returns>
        public string ExpandUrls(string text)
        {
            MatchEvaluator matchEval;
            string pattern;
            string updated;


            // Expand embedded hyperlinks
            var options =RegexOptions.Multiline | RegexOptions.IgnoreCase;
            if (this.ParseFormattedLinks)
            {
                pattern = @"\[(.*?)\|(.*?)]";

                matchEval = this.ExpandFormattedLinks;
                updated = Regex.Replace(text, pattern, matchEval, options);
            }
            else
                updated = text;

            pattern = @"([""'=]|&quot;)?(http://|ftp://|https://|www\.|ftp\.[\w]+)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])";

            matchEval = this.ExpandUrlsRegExEvaluator;
            updated = Regex.Replace(updated, pattern, matchEval, options);

            return updated;
        }

        /// <summary>
        /// Internal RegExEvaluator callback
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string ExpandUrlsRegExEvaluator(Match m)
        {
            string href = m.Value; // M.Groups[0].Value;

            // if string starts within an HREF don't expand it
            if (href.StartsWith("=") ||
                href.StartsWith("'") ||
                href.StartsWith("\"") ||
                href.StartsWith("&quot;"))
                return href;

            string text = href;

            if (href.IndexOf("://") < 0)
            {
                if (href.StartsWith("www."))
                    href = "http://" + href;
                else if (href.StartsWith("ftp"))
                    href = "ftp://" + href;
                else if (href.IndexOf("@") > -1)
                    href = "mailto:" + href;
            }

            string targ = !string.IsNullOrEmpty(this.Target) ? " target='" + this.Target + "'" : string.Empty;

            return "<a href='" + href + "'" + targ +
                    ">" + text + "</a>";
        }

        private string ExpandFormattedLinks(Match m)
        {
            //string Href = M.Value; // M.Groups[0].Value;

            string text = m.Groups[1].Value;
            string href = m.Groups[2].Value;

            if (href.IndexOf("://") < 0)
            {
                if (href.StartsWith("www."))
                    href = "http://" + href;
                else if (href.StartsWith("ftp"))
                    href = "ftp://" + href;
                else if (href.IndexOf("@") > -1)
                    href = "mailto:" + href;
                else
                    href = "http://" + href;
            }

            string targ = !string.IsNullOrEmpty(this.Target) ? " target='" + this.Target + "'" : string.Empty;

            return "<a href='" + href + "'" + targ +
                    ">" + text + "</a>";
        }



    }
}