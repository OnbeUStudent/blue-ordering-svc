using Dii_OrderingSvc.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Dii_OrderingSvc.Controllers
{
    [Route("api/themes")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        // GET: api/<ThemesController>
        [HttpGet]
        public IEnumerable<WebSiteTheme> Get()
        {
            foreach (string themeName in Enum.GetNames(typeof(ThemeType)))
            {
                yield return new WebSiteTheme() { Name = themeName };
            };
        }
    }
}
