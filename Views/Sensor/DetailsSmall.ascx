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

    string imagePath = "";
    switch (Model.Status)
    {
        case Monnit.eSensorStatus.OK:
            imagePath = Html.GetThemedContent("/images/good.png");
            break;
        case Monnit.eSensorStatus.Warning:
            imagePath = Html.GetThemedContent("/images/Alert.png");
            break;
        case Monnit.eSensorStatus.Alert:
            imagePath = Html.GetThemedContent("/images/alarm.png");
            break;
        //case Monnit.eSensorStatus.Inactive:
        //    imagePath = Html.GetThemedContent("/images/inactive.png");
        //    break;
        //case Monnit.eSensorStatus.Sleeping:
        //    imagePath = Html.GetThemedContent("/images/sleeping.png");
        //    break;
        case Monnit.eSensorStatus.Offline:
            imagePath = Html.GetThemedContent("/images/sleeping.png");
            break;
    }

    string pauseIcon = "";
    //if (Model.isPaused())
    //{
    //    pauseIcon = Html.GetThemedContent(RefreshSensorModel.pauseIcon);
    //}

    string dirtyIcon = "";
    if (Model.IsDirty)
    {
        if (imagePath == "/images/inactive.png")
            dirtyIcon = Html.GetThemedContent("/images/inactive-dirty.png");
        else if (imagePath == "/images/good.png")
            dirtyIcon = Html.GetThemedContent("/images/good-dirty.png");
        else if (imagePath == "/images/sleeping.png")
            dirtyIcon = Html.GetThemedContent("/images/sleeping-dirty.png");
        else if (imagePath == "/images/Alert.png")
            dirtyIcon = Html.GetThemedContent("/images/Alert-dirty.png");
    }
%>
<div style="text-align:center;"><%=Model.SensorName %></div>
<div style="text-align:center;">
    <img src="<%: imagePath %>" alt="<%: Model.Status %>" /> <%: Model.Status %>
    <%if(!String.IsNullOrEmpty(pauseIcon)) {%> <img src="<%:pauseIcon %>" class="statusIcon" alt="Status" /> <%} %>
    <%if(!String.IsNullOrEmpty(dirtyIcon)) {%> <img src="<%:dirtyIcon %>" class="statusIcon" alt="Status" /> <%} %>
</div>
<div style="text-align:center;"><%:Display %></div>
