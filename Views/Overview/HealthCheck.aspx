<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html>

<html>
<head>
    <title>Health Check</title>
</head>
<body>
    <%if (((List<string>)TempData["Errors"]).Count == 0){%>
        <div>Healthy</div>
    <%}else{
            Response.StatusCode = 599;// 500-599 is internal server error but this one is not defined generally

            foreach (string errorMessage in (List<string>)TempData["Errors"]) {%>
                <div><%: errorMessage %></div>
        <%}%>
        
    <%} %>
</body>
</html>
