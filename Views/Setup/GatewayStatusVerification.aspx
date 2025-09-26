<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Finding Gateway
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%Html.RenderPartial("_SetupStepper", Model.GatewayID); %>

    <div id="cover" class="find_gateway_container">
        <h2 class="step-head"><%: Html.TranslateTag("Gateway Validation")%></h2>


        <%switch (Model.GatewayTypeID)
            {
                case 1:
                case 2:
                    //USB Logic%>
        <%:Html.Partial("USBGatewaySteps") %>
        <%break;
            case 28:
            case 29:
                //WSA %>
        <%:Html.Partial("WsaSteps") %>
        <%break;
            case 7:
            case 33:
                //Ethernet%>
        <%:Html.Partial("EthernetGatewaySteps") %>
        <%break;
            case 17:
            case 18:
            case 22:
            case 23:
            case 24:
            case 25:
            case 26:
            case 32:
                //Celluar%>
        <%:Html.Partial("CellGatewaySteps") %>
        <%break;
            case 30:
                //IoT%>

        <% if (Model.SKU.Contains("-IN") || Model.SKU.Contains("-in"))
            {%>
        <%:Html.Partial("CellGatewaySteps") %>

        <%}
            else
            {%>
        <%:Html.Partial("IoTGatewaySteps") %>

        <%}%>

        <%break;
            default:
                //Anything else%>
        <%:Html.Partial("GenericGatewaySteps") %>
        <%break;
            }%>
    </div>




    <script>

        $(function () {
            showTabNext('first');
            var interval = setInterval(refreshGatewayBox, 5000);
        });

        function showTabNext(tabtoShow) {
            $('.tab').hide();
            $('.tab-' + tabtoShow).show();
        }


        function refreshGatewayBox() {
            $.get("/Setup/StatusVerificationGatewayRefresh/<%:Model.GatewayID%>", function (data) {

                var JsonObj = JSON.parse(data);
                if (JsonObj.GatewayOnline == 'True') {
                    $('.gwOnlineCheck').hide();
                    $('.gwOnlineComplete').show();
                }
            });
        }

        function confirmModal() {
            let values = {};
            values.text = 'Are you sure you want to continue?  If your gateway is not communicating, your sensors will also not be able to communicate.';
            values.redirect = '/Setup/GatewayComplete/<%=Model.GatewayID + (string.IsNullOrWhiteSpace(Request.Params["reset"]) ? "" : "?reset=" + Request.Params["reset"])%>';
            openConfirm(values);
        }

    </script>
</asp:Content>
