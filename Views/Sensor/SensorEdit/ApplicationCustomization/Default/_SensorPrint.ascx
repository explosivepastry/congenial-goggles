<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        if (ConfigData.AppSettings("IsEnterprise").ToBool())
        {
            if (new Version(Model.FirmwareVersion) >= new Version("25.45.0.0") || Model.SensorPrintActive)
            { %>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor Print","Sensor Print") %>
    </div>
    <div class="col sensorEditFormInput">
        <input type="text" value="<%:(Model.SensorPrint.ByteArrayToString())%>" class="form-control" name="SensorPrint_Manual" id="SensorPrint_Manual" placeholder="<%: Html.TranslateTag("Paste Sensor Print...","Paste Sensor Print...")%>" style="margin-top: 5px;">
        <div>
            <%if (Model.SensorPrintActive && Model.LastDataMessage != null)
			{
				if (Model.LastDataMessage.IsAuthenticated)
				{%>
					<%=Html.GetThemedSVG("printCheck") %>
				<%}
				else
				{%>
					<%=Html.GetThemedSVG("printFail") %>
				<%}
			} 
            else
            {%>
                <%=Html.GetThemedSVG("print") %>
			<%}%>
        </div>
    </div>
</div>
<% 
            }
        }
    }
%>
