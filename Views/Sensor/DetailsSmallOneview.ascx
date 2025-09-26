<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    Monnit.DataMessage message = Model.LastDataMessage;
    
    string Display = string.Empty;
    if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
    {
        if (message != null)
        {
            Display = message.DisplayData;
            if (Display.Length > 30)
                Display = Display.Substring(0, 30);
        }
        else
        {
            Display = "No data gathered";
        }
    }
    else
    {
        Display = "No current reading";
        message = null;
    }
        
%>
<div style="text-align:left; font-size:1.2em; font-weight: 700; color: rgb(51, 51, 51);"><%=Model.SensorName %></div>

<div style="text-align:left; font-size:.8em; color: rgb(42,63,84); font-family: Helvetica, Arial, sans-serif; line-height: 1.471;">Last Message: <%:Display %></div>
