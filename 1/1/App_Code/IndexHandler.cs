using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class IndexHandler : IHttpHandler
{
    public bool IsReusable
    {
        get { return true; }
    }

    public void ProcessRequest(HttpContext context)
    {
        context.Response.WriteFile("index.html");
    }
}
