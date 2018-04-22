using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVideoPopup.Types;

namespace WebVideoPopup.Models
{
    public class WebVideoWrapper
    {
        public string TargetUrl { get; set; }
        public string HtmlCode { get; set; }
        public WebVideoType WebVideoType { get; set; }
    }
}
