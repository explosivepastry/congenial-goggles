<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Notification>" %>

<%="" %>

<%List<eDatumStruct> eDatumStructs = ViewBag.eDatumStructList;
    Sensor sens = ViewBag.SensorPicked;%>

<div class="FILE" hidden>_RuleCreateApplication</div>
<%if (eDatumStructs.Count > 1)
    { %>

<div id="datumOrConditionDiv" class="datumGrid">

    <div class="container-fluid">
        <div class="rules_container pick-condition w-100">
            <div class=" temp_container">
                <div class="card_container__top ">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Pick Which Reading to Monitor")%>
                    </div>
                </div>
                <hr style="margin: 0 0 10px;">
                <div class="rule-card">
                    <div class="rule-title">
                        <%: Html.TranslateTag("Pick a Reading Type","Pick a Reading Type")%>
                    </div>
                    <div class="temp-card_container">
                        <%      
                            foreach (eDatumStruct obj in eDatumStructs)
                            {
                                if (obj.val.StartsWith("15&")) continue;
                                string exampleReading = "";
                                if (sens.LastDataMessage != null)
                                {
                                    try
                                    {
                                        var deserialized = sens.LastDataMessage.AppBase._Deserialize(sens.FirmwareVersion, sens.LastDataMessage.Data);
                                        exampleReading = deserialized.GetPlotValues(Model.SensorID)[obj.datumindex] + " " + deserialized.Datums[obj.datumindex].data.datatype.ToString().Replace("Percentage", "%");
                                    }
                                    catch
                                    {
                                        exampleReading = "";
                                    }
                                }
                        %>
                        <a onclick="selectDatum('<%: obj.val %>');">
                            <div class="temp-card">
                                <div>
                                    <span class="sensor icon icon-app<%:sens.ApplicationID %>"></span>

                                </div>

                                <div>
                                    <div class="temp-name"><%=obj.name %></div>
                                    <div class="glance-reading">
                                        <%= !string.IsNullOrEmpty(exampleReading) ? Html.TranslateTag("Example Reading") + ": " + exampleReading : ""%>
                                    </div>
                                </div>
                            </div>
                        </a>
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>

</div>

<%}
    else
    { %>
<div id="datumOrConditionDiv" class="datumGrid"></div>
<script>

    $(document).ready(function () {

        selectDatum('<%=eDatumStructs[0].val %>');
    });
</script>

<%}%>

<div id="baseConversionMessage"></div>
<script>
    function selectDatum(DatumVal) {
        var val = DatumVal.split("&");

        $.get("/Rule/CreateApplicationCondition/" + val[0] + "?datumIndex=" + val[1], function (partialView) {
            $("#datumOrConditionDiv").html(partialView);
            getAvailableRulesBySensorDatum(<%=sens.SensorID%>, val[0]);
        });
    }

    function getAvailableRulesBySensorDatum(sensorID, datumType) {
        $.get("/Rule/AvailableRulesBySensorDatum/" + sensorID + "?datumType=" + datumType, function (partialView) {
            $("#existingRulesList").html(partialView);
        });
    }

</script>

