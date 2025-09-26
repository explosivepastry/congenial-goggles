<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MapDeviceModel>>" %>

<%
    string searchQuery =  ViewData["filterQuery"].ToStringSafe();
    long visualMapID = ViewData["VisualMapID"].ToLong();
    foreach (var item in Model)
    {
        bool isChecked = (item.VisualMapID > 0);

        MvcHtmlString icon;

        if (item.Name.Length > 44)
            item.Name = item.Name.Substring(0, 44) + "...";

        if (item.GatewayTypeID > 0)
            icon = Html.GetThemedSVG("tower-cell");
        else
            icon = Html.GetThemedSVG("app" + item.ApplicationID);

         %>
<a class="gridPanel eventsList rounded" style="cursor: pointer; box-shadow: none;" onclick="toggleMapDevice(<%=item.DeviceID%>,<%=visualMapID %>,<%=(item.GatewayTypeID > 0).ToString().ToLower() %> );">

    <div class="triggerDevice__container" style="justify-content: space-between;">
        <div class="triggerDevice__icon">
           <div style="width: 30px; height: 30px; margin-left: 5px;"><%=icon %></div>
       </div>
   
     <div class="triggerDevice__name" style="width: 200px;">
            <label class="text-wrap" style="font-weight: normal !important; font-size: small !important;  padding: 7px;"><%=item.Name%></label>
     </div>

     <div class="gridPanel triggerDevice__status <%= isChecked?"ListBorderActive":"ListBorderNotActive"%>" id="device_<%:item.DeviceID%>" title="<%:item.Name%>">
            <%=Html.GetThemedSVG("circle-check") %>
     <%--       <svg xmlns="http://www.w3.org/2000/svg" width="25.806" height="19.244" viewBox="0 0 25.806 19.244">
                <path id="check-circle-solid" d="M25.687,38.59,40.525,23.751a1.29,1.29,0,0,0,0-1.825L38.7,20.1a1.29,1.29,0,0,0-1.825,0l-12.1,12.1-5.65-5.65a1.29,1.29,0,0,0-1.825,0l-1.825,1.825a1.29,1.29,0,0,0,0,1.825l8.387,8.387a1.29,1.29,0,0,0,1.825,0Z" transform="translate(-15.097 -19.724)" fill="#fff" />
            </svg>--%>
        </div>
    </div>
</a>
<%} %>

<script type="text/javascript">
    $(document).ready(function () {
        $('#searchQuery').val('<%Response.Write(searchQuery);%>');
    });

    function toggleMapDevice(deviceID, mapID, isGateway) {
        var Add = $('#device_' + deviceID).hasClass('ListBorderNotActive');
        addorRemoveDevice(deviceID, mapID, isGateway, Add);
    }

    function addorRemoveDevice(deviceID,visualMapID,isGateway,Add) {
        $.post("/Map/ToggleGpsDevice/", { deviceId : deviceID, mapID : visualMapID,  isGateway : isGateway , add : Add }, function (data) {
            if (data == "Success") {
                if (Add) {
                    $('#device_' + deviceID).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                }
                else {
                    $('#device_' + deviceID).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                }
                //loadDevices();
            } else {
                showSimpleMessageModal("<%=Html.TranslateTag("Device Failed to Add/Remove")%>");
            }
        });
    }

</script>
