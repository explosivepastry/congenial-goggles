<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Notification>>" %>

<%
    bool isGate = (bool)ViewBag.isGateway;
    long gatewayID = 0;
    long sensorID = 0;
    if (isGate)
        gatewayID = ((Gateway)ViewBag.Gateway).GatewayID;
    else
        sensorID = ((Sensor)ViewBag.Sensor).SensorID;
    %>


<div class="choose-task " id="existingRuleDiv" <%= Model.Count == 0 ? "style=\"display:none;\"" : ""%>>
    <div class=" rule-card_container" style="width:100%;">
        <div class="card_container__top">
            <div class="rule-title" style="margin-bottom: 10px;  border-bottom: 1px solid #ccc; width:100%; ">
                <%: Html.TranslateTag("Or use an existing rule.")%>
            </div>

            <br />
        </div>

        <div class="hasScroll-rule" id="existingRulesList">
            <%foreach (Notification item in Model)
                { %>
            <div class="toggleRule super_small_card" onclick="addDeviceToRule(<%:item.NotificationID%>)" >
                <div class="">
                    <div class=" triggerDevice__name" style="font-size:.9rem;">
                        <strong><%:System.Web.HttpUtility.HtmlDecode(item.Name) %></strong>
                    </div>

                    <div class="col-1" style="text-align: center;">
                        <div>
                            <div class="dropleft" style="width: 50px;">
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <%} %>
        </div>
    </div>
</div>

<script>

    var isGate = ("<%=isGate.ToString().ToLower()%>" == "true");

    function addDeviceToRule(notiID) {
        let params = {};
        var url = "/Rule/AddDevicetoExistingNotification";
        if (isGate) {
            params.gatewayID = Number('<%=gatewayID%>');
        } else {
            params.sensorID = Number('<%=sensorID%>');
        }
        params.notificationIDs = notiID;
        params.datumindex = 0;

        $.post(url, params, function (data) {
            if (data == "Success") {
                window.location.href = "/Rule/RuleComplete/" + notiID;
            }
        });

    }

</script>