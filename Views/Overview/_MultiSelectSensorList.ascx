<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%="" %>

<%

    List<Sensor> sensors = Sensor.LoadByAccountIDAndApplicationID(MonnitSession.CurrentCustomer.AccountID, Model.ApplicationID);
%>

<div class="actionsDeviceContainer">
    <div style="max-height: 87vh!important; margin-top: 1rem;">
        <div class="trigger-device__top">
            <p class="useAwareState"><%: Html.TranslateTag("Events/Triggers| Apply Settings To Multiple Sensors")%></p>
            <div class="clearfix"></div>
        </div>

        <div id="triggerSensors" class="hasScroll-rule" style="margin-top: .5rem; padding: .5rem 0; display: flex; flex-wrap: wrap; height: fit-content; gap: 1rem;">
            <%foreach (Sensor item in sensors)
                {%>
            <a class="activate-card-holder" onclick="toggleSensor(<%:item.SensorID%>);" style="margin:0">
                <div class="active-card-contents">

                    <div class="icon-color" style="width: 30px; height: 30px; margin-left: 5px;">
                        <%=Html.GetThemedSVG("app" + item.ApplicationID) %>
                    </div>
                    <div class="activate-name">
                        <strong><%:System.Web.HttpUtility.HtmlDecode(item.SensorName) %></strong>
                    </div>
                    <div style="width: 50px;" class="gridPanel-sensor triggerDevice__status <%:item.SensorID == Model.SensorID ? "ListBorderActive" : "ListBorderNotActive" %>" id="sensor_<%:item.SensorID%>" data-id="<%:item.SensorID %>">
                        <%=Html.GetThemedSVG("circle-check") %>
                    </div>
                </div>
            </a>
            <%} %>
        </div>
        <div class="d-flex" style="justify-content: flex-end">
            <button class="btn btn-primary mx-2" type="button" id="SaveMultiSensorSelection" <%=Model.CanUpdate ? "" : "disabled" %> value="<%: Html.TranslateTag("Save", "Save")%>">
                <%: Html.TranslateTag("Save", "Save")%>
            </button>
        </div>

    </div>
</div>

<script>
    function toggleSensor(sensorID) {
        var obj = $('#sensor_' + sensorID);
        var add = obj.hasClass('ListBorderNotActive');

        if (add) {
            obj.removeClass('ListBorderNotActive').addClass('ListBorderActive');
        } else {
            obj.removeClass('ListBorderActive').addClass('ListBorderNotActive');
        }
    }
</script>
