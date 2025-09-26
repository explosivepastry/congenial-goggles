<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Notification>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Rules
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%Sensor sensor = ViewData["Sensor"] as Monnit.Sensor; %>

<div class="container-fluid">
    <%Html.RenderPartial("SensorLink", sensor); %>
</div>

<div style="font-weight: normal !important">
    <div class="rule-card_container" style="width:auto">
        <div class="trigger-device__top">
            <div class="card_container__top" style="border-bottom: none; margin-bottom: -8px;">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Overview/SensorNotification|Add Sensor to Existing Rule","Add Sensor to Existing Rule")%>
                </div>
                <div class="clearfix"></div>
            </div>
            <br />
            <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                <font color="gray">
                    <%: Html.TranslateTag("Overview/SensorNotification|Click rule to enable/disable","Click rule to enable/disable")%>
                </font>

                <a href="#" onclick="$('#filters').toggle(); return false;">
                    <%=Html.GetThemedSVG("filter") %>			
                </a>
            </div>
        </div>
        <div class="row my-2" id="filters" style="display: none;">
                
            <div class="d-flex flex-wrap">
                <div class="d-flex mx-md-2" style="align-items: center;">
                    <strong><%: Html.TranslateTag("Filtered/Total","Filtered/Total")%>: &nbsp;</strong>
                    <span id="filteredRules"></span>/<span id="totalRules"></span>
                </div>
                <input type="text" id="ruleNameFilter" class="NameFilter form-control mx-md-2" placeholder="<%: Html.TranslateTag("Rule Name","Rule Name")%>..." style="width: 250px;" />

                <select id="ruleTypeFilter" class="form-select mx-md-2" style="width: 250px;">
                    <option value=""><%: Html.TranslateTag("Rule Type","Rule Type")%></option>
                    <option value="Application"><%: Html.TranslateTag("Application","Application")%></option>
                    <option value="Inactivity"><%: Html.TranslateTag("Inactivity","Inactivity")%></option>
                    <option value="Low_Battery"><%: Html.TranslateTag("LowBattery","LowBattery")%></option>
                    <option value="Advanced"><%: Html.TranslateTag("Advanced","Advanced")%></option>
                    <option value="Timed"><%: Html.TranslateTag("Scheduled","Scheduled")%></option>
                </select>

                <select class="form-select mx-md-2" id="sensorDatumFilter" name="Datum" style="width: 250px;">
                    <option value=""><%: Html.TranslateTag("All Data Points","All Data Points")%></option>
                    <%foreach (eDatumStruct obj in sensor.GetDatumStructs())
                        {
                            if (obj.val.StartsWith("15&")) continue;// this removes Mode datum from ddl  %>
                    <option value="<%:obj.val %>"><%=obj.name %></option>
                    <%} %>
                </select>

                <select id="ruleActiveFilter" class="form-select mx-md-2" style="width: 250px;">
                    <option value=""><%: Html.TranslateTag("Active Status","Active Status")%></option>
                    <option value="true"><%: Html.TranslateTag("Active","Active")%></option>
                    <option value="false"><%: Html.TranslateTag("Inactive","Inactive")%></option>
                </select>

                <select id="ruleAssignedFilter" class="form-select mx-md-2" style="width: 250px;">
                    <option value=""><%: Html.TranslateTag("Assigned Status","Assigned Status")%></option>
                    <option value="true"><%: Html.TranslateTag("Assigned","Assigned")%></option>
                    <option value="false"><%: Html.TranslateTag("Unassigned","Unassigned")%></option>
                </select>
            </div>
        </div>

        <div id="newEventConfigurationHolder" style="display:flex;flex-wrap:wrap;width: 100%;"></div>
        <div class="text-center" id="loadingSpinner" style="display: none">
            <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
                <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
            </div>
        </div>
    </div>
</div>

 <script type="text/javascript">

        loadActionList();

        $("#ruleTypeFilter").change(function () {
            if ($('#ruleTypeFilter').val() == "Application") {
                $('#sensorDatumFilter').show();
            }
            else {
                $('#sensorDatumFilter').hide();
                loadActionList();
            }
        });

        $("#sensorDatumFilter").change(function () {
            loadActionList();
        });

        $("#ruleNameFilter").keyup(function () {
            filterName(event.key);
        });

        $("#ruleActiveFilter").change(function () {
            loadActionList();
        });

        $("#ruleAssignedFilter").change(function () {
            loadActionList();
        });

        var nameTimeout = null;
        function filterName(key) {
            clearTimeout(nameTimeout);

            if (key === 'Enter') {
                loadActionList();
            } else {
                nameTimeout = setTimeout("loadActionList();", 1000);
            }
        }

        function setNotificationStatusClick() {
            $('.notiStatus').unbind('click').click(function (e) {
                e.preventDefault();
                var lnk = $(this);
                $.get(lnk.attr('href'), function (data) {
                    if (data == "Success")
                        $('.notiStatus' + lnk.attr('id')).toggle();
                    else {
                        console.log(data);
                        showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                    }
                });
            });
        }

        function loadActionList() {

            $("#newEventConfigurationHolder").hide();
            $('#loadingSpinner').show();
            $.post("/Overview/AvailableRulesForSensor/<%: sensor.SensorID %>",
             {
                 eventType: $("#ruleTypeFilter").val(),
                 datumIndex: $('#sensorDatumFilter').val(),
                 nameFilter: $('#ruleNameFilter').val(),
                 status: $('#ruleActiveFilter').val(),
                 assigned: $('#ruleAssignedFilter').val()
             }
             , function (partialView) {
                 $("#newEventConfigurationHolder").html(partialView);
                 $('#loadingSpinner').hide();
                 $("#newEventConfigurationHolder").show();
             });
     }

 </script>
</asp:Content>
