<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    StatusVerification
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%

        //purgeme
        Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));

        //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));

        Response.Cache.SetValidUntilExpires(false);
        Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    %>

    <div class="container-fluid">
        <%Html.RenderPartial("_SetupStepper", Model.SensorID); %>



        <div id="cover" class="find_gateway_container">


            <h2 class="step-head"><%: Html.TranslateTag("Sensor Setup")%></h2>

            <%if (Model.WitType == eWitType.Wit_2)
                { %>
            <%:Html.Partial("_StatusVerificationAA") %>
            <%}
                else if (Model.WitType == eWitType.Wit_Industrial)
                { %>
            <%:Html.Partial("_StatusVerificationInd") %>
            <%}
                else if (Model.WitType == eWitType.Wit && (Model.PowerSourceID == 1 || Model.PowerSourceID == 14))
                { %>
            <%:Html.Partial("_StatusVerificationCoin") %>
            <%}
                else if (Model.IsWiFiSensor && Model.SensorTypeID == 4)
                { %>
            <%:Html.Partial("_StatusVerificationWiFi") %>

            <%}
                else if (Model.IsWiFiSensor && Model.SensorTypeID == 8)
                { %>
            <%:Html.Partial("_StatusVerificationWiFi2") %>
            <%}
                else if (Model.IsPoESensor)
                { %>
            <%:Html.Partial("_StatusVerificationPoE") %>
            <%}
                else if (Model.IsLTESensor)
                { %>
            <%:Html.Partial("_StatusVerificationGeneric") %>
            <%}
                else
                { //Anything else Special%>
            <%:Html.Partial("_StatusVerificationGeneric") %>
            <%} %>
        </div>
    </div>


    <script type="text/javascript">

        $("document").ready(function () {
            showTabNext('first');
            var interval = setInterval(refreshSensorBox, 5000);
        });

        function showTabNext(tabtoShow) {

            $('.tab').hide();
            $('.tab-' + tabtoShow).show();

        }

        function refreshSensorBox() {
            $.get("/Setup/StatusVerificationSensorRefresh/<%:Model.SensorID%>", function (data) {
                var JsonObj = JSON.parse(data);
                //console.log(JsonObj);
                if (JsonObj.GatewayOnline == 'True') {
                    $('.gwOnlineCheck').hide();
                    $('.gwOnlineComplete').show();
                }

                if (JsonObj.GatewayCheckedInFresh == 'True') {
                    $('.gwListCheck').hide();
                    $('.gwListComplete').show();
                }

                if (JsonObj.SensorOnline == 'True') {
                    $('.senComCheck').hide();
                    $('.battCheckComplete').click();
                    $('.senComComplete').show();
                }

            });
        }

        function confirmModal() {
            let values = {};
            values.text = 'Are you sure you want to continue?';
            values.redirect = '/Setup/SensorComplete/<%:Model.SensorID%>';
            openConfirm(values);
        }

    </script>

</asp:Content>
