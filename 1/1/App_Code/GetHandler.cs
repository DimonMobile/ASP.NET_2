using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class GetHandler : IHttpHandler
{
    public static int m_resultValue = 0;
    public static Stack<int> m_resultStack = new Stack<int>();

    public bool IsReusable
    {
        get { return true; }
    }


    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;

        if (request.HttpMethod == "GET")
        {
            int result = m_resultValue;
            if (m_resultStack.Count > 0)
                result += m_resultStack.First();
            response.ContentType = "application/json";
            response.Write("{\"result\": ");
            response.Write(result);
            response.Write("}");
        }
        else if (request.HttpMethod == "POST")
        {
            m_resultValue = Int32.Parse(request.Params["RESULT"]);
            response.ContentType = "application/json";
            response.Write("{\"result\": ");
            response.Write(m_resultValue);
            response.Write("}");
        }
        else if (request.HttpMethod == "PUT")
        {
            response.ContentType = "application/json";
            m_resultStack.Push(Int32.Parse(request.Params["ADD"]));
            response.Write("{\"status\": \"ok\"}");
        }
        else if (request.HttpMethod == "DELETE")
        {
            response.ContentType = "application/json";
            if (m_resultStack.Count == 0)
            {
                response.Write("{\"status\": \"fail\"}");
            }
            else
            {
                m_resultStack.Pop();
                response.Write("{\"status\": \"ok\"}");
            }
        }
    }
}
