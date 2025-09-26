<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.VisualMap>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
        DevicesToShowStatic
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <link href="/Content/jquery.contextmenu.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/OpenSeaDragon/openseadragon.js" type="text/javascript"></script>

   <div class="container-fluid">
        <div class="clearfix"></div>
        <div class="my-4">
            <%Html.RenderPartial("_MapLink", Model); %>
        </div>
 
       <div class="clearfix"></div>
        
    </div>

    <div class="actionsDeviceContainer">
    <%if (ViewBag.ShowSensors == true)
        { %>
    <%= Html.Partial("_SensorsListWithFilters") %>
    <%} %>
    <%if (ViewBag.ShowGateways == true)
        { %>
    <%= Html.Partial("_GatewaysListWithFilters") %>
    <%} %>
</div>

<!-- End Device Info Panel -->

        <!-- Map View -->
        <div class="col-12">
            <div class="rule-card_container w-100">
                <div class="x_content" id="Div1">
                    <%:Html.Partial("~/Views/Map/_Map.ascx", Model) %>
                </div>
            </div>
        </div>

        <div id="iconMenu">
            <div class="dropdown-menu shadow dropdown-menu-sm py-0" id="context-menu">
                <a class="dropdown-item py-2" id="bigger"><%: Html.TranslateTag("Bigger","Bigger")%></a>
                <a class="dropdown-item py-2" id="smaller"><%: Html.TranslateTag("Smaller","Smaller")%></a>
            </div>
        </div>
    
    <!-- End Map View -->
    
    <script src="/Scripts/OpenSeaDragon/VisualOverview.js" type="text/javascript"></script>
    <script src="/Scripts/OpenSeaDragon/MapEdit.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        //function doDraggable() {
        //    var apple = 'apple';
        //    console.log('apple = ' + apple);
        //    $('.mapIcon').each(function () {
        //        console.log('banana = ' + banana);
        //        draggable(this);
        //        console.log('apple = ' + apple);
        //    });
        //    var banana = 'banana';
        //    console.log('banana = ' + banana);
        //}

        //$(document).ready(function () {
        //    var apple = 'almond';
        //    var banana = 'joy';
        //    console.log('apple = ' + apple);
        //    console.log('banana = ' + banana);
        //    setTimeout(doDraggable, 1 * 1000);
        //    var apple = 'rice';
        //    var banana = 'krispies';
        //    console.log('apple = ' + apple);
        //    console.log('banana = ' + banana);
        //});
        
        $("#fileBtn").click(function () {
            $("#ImageFile").click();
        });

        $('input[type=file]').change(function (e) {
            var removePath = $('#ImageFile').val().replace(/^C:\\fakepath\\/i, '');
            $("#fileLabel").html(removePath);
        });

        function removeMap(item) {
            let values = {};
            values.url = `/Map/DeleteMap/${item}`;
            values.text = "<%: Html.TranslateTag("Are you sure you want to remove this map?","Are you sure you want to remove this map?")%>";
            values.redirect = '/Map/Index';
            openConfirm(values);
        }

</script>

</asp:Content>
