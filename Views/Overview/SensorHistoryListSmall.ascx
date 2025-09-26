<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<div id="dataList" class="dataList">
    
    <%
        //DateTime fromDate = DateTime.UtcNow.AddDays(-1);
        //if ( fromDate < Model.StartDate )
        //{
        //    fromDate = Model.StartDate;
        //}
        List<DataMessage> dm = DataMessage.LoadBySensorAndDateRange(Model.SensorID, DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddMinutes(60), 20,Guid.Empty);
        foreach (var item in dm)
        {%>

    <!-- History -->
    <div  class="data-info-read" id="dataInfo" style="cursor: pointer;" title="<%=item.GatewayID %>" onclick="location.href='/Overview/SensorNote/<%=item.DataMessageGUID %>';">
        <div class="col-1 col-md-1 col-sm-1">

                        <span class="notetaken" style="display:<%:item.HasNote ? "inline;" : "none;"%>" id="hasNotes_<%:item.DataMessageGUID %>"><%=Html.GetThemedSVG("note") %></span>
                            <span style="cursor:pointer;display:<%:item.HasNote ? "none;" : "inline;"%>" id="noNotes_<%:item.DataMessageGUID %>"><%=Html.GetThemedSVG("emptyNote") %></span>

        </div>
        <div class="  historyReading"><%:item.DisplayData %></div>

        <div class="col-4 col-md-4 col-sm-4 historyDate"><%:item.MessageDate.OVToLocalDateTimeShort() %></div>
        <div class=" reading-icon-transform d-flex" style="align-items:baseline; float:right;" >
            <%--<div class="col-6 col-md-6 col-sm-2 gatewaySignal sigIcon" style="font-size: xx-small!important; padding-top: 5px;">--%>
                <%if (new Version(Model.FirmwareVersion) >= new Version("25.45.0.0") || Model.SensorPrintActive)
                {
                    if (Model.SensorPrintActive && item != null)
                    {
                        if (item.IsAuthenticated)
                        {%>
                            <%=Html.GetThemedSVG("printCheck") %>
                        <%}
                        else
                        {%>
                            <%=Html.GetThemedSVG("printFail") %>
                        <%}
                    } 
                }%>

                <%MvcHtmlString SignalIcon = new MvcHtmlString("");
                    if (item != null)
                {
                        if (Model.IsPoESensor)
                        {
                            //show nothing						
                            //<div class="icon icon-ethernet-b"></div>						
                        }
                        else
                        {
                            int Percent = DataMessage.GetSignalStrengthPercent(Model.GenerationType, Model.SensorTypeID, item.SignalStrength);
                            string signal = "";

                                           if (Percent <= 0)
                                SignalIcon = Html.GetThemedSVG("signal-none");
                            else if (Percent <= 10)
                                SignalIcon =  Html.GetThemedSVG("signal-1");
                            else if (Percent <= 25)
                                  SignalIcon =  Html.GetThemedSVG("signal-2");
                            else if (Percent <= 50)
                                 SignalIcon =  Html.GetThemedSVG("signal-3");
                            else if (Percent <= 75)
                               SignalIcon =  Html.GetThemedSVG("signal-4");
                            else
                               SignalIcon =  Html.GetThemedSVG("signal-5");
                        %>
                        <div title="Signal Strength: <%= Percent %>%" style="width:30px; height:30px;"> <%=SignalIcon %></div>
                        <%} %>
               <%} %>
            <%--</div>--%>
            <%--        &nbsp;&nbsp;--%>
            <% MvcHtmlString PowerIcon = new MvcHtmlString("");
                string battLevel = "";
                string battType = "";
                string battModifier = "";
                if (item != null)
                {
                    if (item.Battery <= 0)
                    {
                        battLevel = "-0";
                        battModifier = " batteryCritical batteryLow";
                        PowerIcon = Html.GetThemedSVG("bat-dead");
                    }
                    else if (item.Battery <= 10)
                    {
                        battLevel = "-1";
                        battModifier = " batteryCritical batteryLow";
                        PowerIcon = Html.GetThemedSVG("bat-low");
                    }
                    else if (item.Battery <= 25)
                    {
                        PowerIcon = Html.GetThemedSVG("bat-low");
                        battLevel = "-2";
                    }
                    else if (item.Battery <= 50)
                    {
                        PowerIcon = Html.GetThemedSVG("bat-half");
                        battLevel = "-3";
                    }
                    else if (item.Battery <= 75)
                    {

                        PowerIcon = Html.GetThemedSVG("bat-full-ish");
                        battLevel = "-4";
                    }
                    else
                    {
                        PowerIcon = Html.GetThemedSVG("bat-ful");
                        battLevel = "-5";
                    }

                    if (Model.PowerSourceID == 3 || item.Voltage == 0)
                    {
                        battType = "-line";
                        battLevel = "";
                    }
                    else if (Model.PowerSourceID == 4)
                    {
                        battType = "-volt";
                        battLevel = "";
                    }
                    else if (Model.PowerSourceID == 1 || Model.PowerSourceID == 14)
                        battType = "-cc";
                    else if (Model.PowerSourceID == 2 || Model.PowerSourceID == 8 || Model.PowerSourceID == 10 || Model.PowerSourceID == 13 || Model.PowerSourceID == 15 || Model.PowerSourceID == 17 || Model.PowerSourceID == 19)
                        battType = "-aa";
                    else if (Model.PowerSourceID == 6 || Model.PowerSourceID == 7 || Model.PowerSourceID == 9 || Model.PowerSourceID == 16 || Model.PowerSourceID == 18)
                        battType = "-ss";
                    else
                        battType = "-gateway";

                }%>

            <div class=" col-md-4 col-sm-4 col-4 battIcon  " title="Battery: <%=item.Battery %>%, Voltage: <%=item.Voltage %> V">
                          <%=PowerIcon %>
            </div>
            <%--            <div style="font-size: 2em" class=" col-md-6 col-sm-6 col-6 battIcon  <%:battType %><%:battLevel %><%:battModifier %>" title="<%=item.Voltage %>">--%>
            <%--                <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24"><path d="M8 24l3-9h-9l14-15-3 9h9l-14 15z"/></svg>--%>
            <%--            <%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", item.Voltage) : "" %>--%>
        </div>
    </div>


    <div class="clearfix"></div>
    <!-- History-->
    <hr style="margin: 5px;" />

    <%} %>
</div>
