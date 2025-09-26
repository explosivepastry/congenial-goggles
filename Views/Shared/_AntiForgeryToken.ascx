<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<% 
    try
    {
%>

        <%: Html.AntiForgeryToken() %>

<%      
    }
    catch (Exception ex)
    {
        ex.Log("Html.AntiForgeryToken()");
        throw ex;
    }


%>
