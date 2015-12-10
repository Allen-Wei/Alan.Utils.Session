using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Alan.Utils.Session.Demo.Api
{
    public class TestController : ApiController
    {
        public object Get()
        {
            IEnumerable<string> output = new List<string>();
            if (Request.Headers.TryGetValues("X-Token", out output) && output.Any())
            {
                return Alan.Utils.Session.Singleton.Container.Get<object>(output.First());
            }
            return output;
        }
    }
}