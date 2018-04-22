using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebVideoPopup.Models;
using WebVideoPopup.Services.Interfaces;
using WebVideoPopup.Types;

namespace WebVideoPopup.Services.Platforms
{
    public class YoutubeService : IWebVideoService
    {
        public WebVideoType WebVideoType => WebVideoType.Url;

        public const string YOUTUBE_EMBEDDED = @"https://www.youtube.com/embed/{0}";
        public static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

        public bool CanHandle(string url)
        {
            return String.IsNullOrWhiteSpace(url) ? false : url.ToLowerInvariant().Contains("youtube.com/watch?v=");
        }

        public WebVideoWrapper Parse(string url)
        {
            Match youtubeMatch = YoutubeVideoRegex.Match(url);
            string id = string.Empty;

            if (youtubeMatch.Success)
            {
                id = youtubeMatch.Groups[youtubeMatch.Groups.Count-1].Value;
                return new WebVideoWrapper
                {
                    WebVideoType = WebVideoType.Url,
                    HtmlCode = null,
                    TargetUrl = String.Format(YOUTUBE_EMBEDDED, id)
                };
            }

            return null;
        }
    }
}