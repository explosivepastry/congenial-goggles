<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Dictionary<long, string>>" %>

<%foreach (long key in Model.Keys)
    { %>
<%:key %>:<%:Model[key] %><br />
<%} %>