using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebVideoPopup.Models;
using WebVideoPopup.Types;

namespace WebVideoPopup.Services.Interfaces
{
    public interface IWebVideoService
    {
        WebVideoType WebVideoType { get; }

        bool CanHandle(string url);

        WebVideoWrapper Parse(string url);
    }
}
