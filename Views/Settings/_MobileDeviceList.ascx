<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<%List<CustomerMobileDevice> mobileDevices = new List<CustomerMobileDevice>();
  mobileDevices = CustomerMobileDevice.LoadByCustomerID(Model.CustomerID);
  bool firstLoad = true ;
  %>





    <div class="x_content">
        <%if (mobileDevices.Count > 0)
          { %>


        <%foreach (CustomerMobileDevice device in mobileDevices)
          {
              long deviceID = device.CustomerMobileDeviceID; %>
        <div class="gridPanel col-md-12 col-sm-12 col-xs-12">
            <%if (!string.IsNullOrEmpty(Convert.ToString(device.CustomerMobileDeviceID)))
              { %>
            <div class="sensor sensorIcon <%:device.SendToDevice ? "sensorStatusOK" : "sensorStatusInactive"%> col-md-1 col-sm-1 col-xs-1" onclick="toggleEventStatus(this);" data-cmdid="<%: deviceID %>">

                <%if ((device.MobileDeviceType) == "Android")
                  { %>
                <i class="fa fa-android" style="font-size: 2.2em; padding: 8px;"></i>
                <%}%>
                <%else if ((device.MobileDeviceType) == "IOS")
                  { %>
                <i class="fa fa-apple" style="font-size: 2.2em; padding: 8px;"></i>
                <%}%>
                <%else
                  {%>
                <i class="fa fa-question" style="font-size: 2.2em; padding: 8px;"></i>
                <%}%>
            </div>

            <div class="col-md-3 col-sm-8 col-xs-8 glance-text">
                <div class="glance-name">
                    <input type="text" data-cmdid="<%:deviceID%>" class="form-control input-sm displayName" placeholder="<%: device.MobileDeviceName %>" value="<%: device.MobileDisplayName %>">
                </div>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-4" style="padding: 10px 0px;" onclick="PrimaryDeviceSet(<%: deviceID %>);">
                <input id="toggle-event-primaryDevice" type="checkbox" data-style="<%:(firstLoad ? "quick" : "fast")%>" data-toggle="toggle" data-on="Primary Device" data-off="Set as Primary" data-onstyle="primary" data-offstyle="default" data-width="140">
            </div>
            <script>
            <%if (device.IsPrimary)
              {%>
                $('#toggle-event-primaryDevice_<%:deviceID %>').bootstrapToggle('on')            
                <%}
              else
              {%>
                $('#toggle-event-primaryDevice_<%:deviceID %>').bootstrapToggle('off') 
                <%}%>
            </script>
      
            <div class="col-md-1 col-sm-1 col-xs-3" style="text-align: right; padding: 10px 0px;">
                <i class="fa fa-trash delete removeMobileDevice" data-cmdid="<%:deviceID%>" style="font-size: 2.4em;" title="<%: Html.TranslateTag("Remove","Remove")%>"></i>
            </div>
            <%} %>
        </div>
        <%}%>

        <%} %>
        <%else
          {%>
        <div class="col-md-6 col-sm-6 col-xs-12 bold">
            <%:Html.TranslateTag("Settings/_Mobile DeviceList|To add a mobile device, install the Mobile App on Android or iOS and \'Configure Push\'")%>
        </div>
        <br />
        <br />
        <hr />
        <%} %>
    </div>   


<style>  
  .fast .toggle-group { transition: left 0.1s; -webkit-transition: left 0.1s; }
  .quick .toggle-group { transition: none; -webkit-transition: none; }
</style>

<script>
    $(function () {

        var removeDevice = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Are you sure you want to remove this device?")%>";
        var failedDelete = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Device was not able to be removed.")%>";
        var notSaved = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Device name was not saved.")%>";

        $('.removeMobileDevice').click(function (e) {
            e.preventDefault();
            var device = $(this).attr('data-cmdid');
            if (confirm(removeDevice)) {
                $.get("/Notification/RemoveMobileDevice?cmdID=" + device, function (data, status) {
                    if ("success") {
                        disableUnsavedChangesAlert();
                        window.location = "/Settings/UserDetail/<%:Model.CustomerID%>";
                    }
                    else {
                        showSimpleMessageModal("<%=Html.TranslateTag("Device was not able to be removed")%>");
 }
                })
            }
            e.stopImmediatePropagation();
        });

        $('.displayName').blur(function () {
            var name = $(this).val();
            var device = $(this).attr('data-cmdid');
            $.get("/Notification/SaveDisplayName?name=" + name + "&cmdID=" + device, function (data, status) {
                if ("success") {
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Device name was not saved")%>");
 }
            })
        });
    });


    function PrimaryDeviceSet(device) {
        if ($('#toggle-event-primaryDevice_' + device).prop('checked')) {
            $.post("/Notification/SavePreferredDevice", { isPreferred: false, cmdID: device }, function (data) {
                {
                    $('#loadMobileDiviceList').html(data);
                }
            });
        }
        else {
            $.post("/Notification/SavePreferredDevice", { isPreferred: true, cmdID: device }, function (data) {
                {
                    $('#loadMobileDiviceList').html(data);
                }
            });
        }

    };

    function toggleEventStatus(anchor) {
        var div = $(anchor);
        var device = $(anchor).attr('data-cmdid');
        if (div.hasClass("sensorStatusOK")) {
            $.post("/Notification/SendToDevice", { send: false, cmdID: device }, function (data) {
                {
                    $('#loadMobileDiviceList').html(data);
                }
            });
        }
        else {
            $.post("/Notification/SendToDevice", { send: true, cmdID: device }, function (data) {
                {
                    $('#loadMobileDiviceList').html(data);
                }
            });
        }
    }
</script>
