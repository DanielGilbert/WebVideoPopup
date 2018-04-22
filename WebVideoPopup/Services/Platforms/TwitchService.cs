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
    public class TwitchService : IWebVideoService
    {
        public WebVideoType WebVideoType => WebVideoType.Url;

        public const string TWITCH_EMBEDDED = "http://player.twitch.tv/?{0}={1}";
     
        public bool CanHandle(string url)
        {
            return String.IsNullOrWhiteSpace(url) ? false : url.ToLowerInvariant().Contains("https://www.twitch.tv/");
        }

        public WebVideoWrapper Parse(string url)
        {
            string channel = url.Replace(@"https://www.twitch.tv/", String.Empty).Replace("/", String.Empty);

            if (!String.IsNullOrWhiteSpace(channel))
            {
                return new WebVideoWrapper
                {
                    WebVideoType = WebVideoType.Url,
                    HtmlCode = null,
                    TargetUrl = String.Format(TWITCH_EMBEDDED, "channel", channel)
                };
            }
            
            return null;
        }
    }
}
