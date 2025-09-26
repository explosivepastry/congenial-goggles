<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorCertificateExpiring
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid" style="height: 100vh;">
        <div class="rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%=Html.GetThemedSVG("certificate") %>
                    &nbsp;
                    <%: Html.TranslateTag("Sensors With Expiring Certificates", "Sensors With Expiring Certificates")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content px-2">
                <div style="background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px; border-radius:5px;">
                    <span style="color: gray;"><%: Html.TranslateTag("Network/SensorsToUpdate|Click The Sensor to View it's Certificate")%></span>
                </div>
                <div class="col-12 hasScroll text-center">
                    <div id="UpdateSpinner" class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                </div>
                <div class="col-12" style="background:#eee;">
                    <div class="ov-scroll250 " style="margin-top:-20px;">
                        <br />
                        <div id="UpdateSensorList" class="p-2" style="background: white;">
                        </div>
                    </div>
                </div>
            </div>

        </div>

    </div>

    <script type="text/javascript">



        $(document).ready(function () {

            LoadSensorList();


        });

        function LoadSensorList() {
            $("#UpdateSpinner").show();
            $('#UpdateSensorList').hide();

            //Load list of sensors with available updates
            $.post("/Network/LoadCertificateExpiringSensors/<%=ViewBag.AccountID%>", { nameFilter: $('#NameFilter').val(), applicationFilter: $('#ApplicationFilter').val() }, function (data) {
                $("#UpdateSpinner").hide();
                $('#UpdateSensorList').html(data);
                $('#UpdateSensorList').show();
            });
        }



    </script>

    <style>
        .triggerDevice__container {
            margin: 0;
            padding: 10px 10px;
        }

        #UpdateSensorList {
            display: flex;
            flex-wrap: wrap;
            margin-right: 0px;
        }
    </style>
</asp:Content>
