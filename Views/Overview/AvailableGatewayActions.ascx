<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<AvailableNotificationByGatewayModel>>" %>
<%
    Gateway gateway = ViewData["Gateway"] as Monnit.Gateway;

    int totalRules = ViewBag.TotalRules;
    int filteredRules = ViewBag.FilteredRules;
%>

<% 	
    foreach (AvailableNotificationByGatewayModel item in Model)
    {
          string svgTemp = "";
            switch (item.Notification.NotificationClass)
            {
                case eNotificationClass.Application:
                    svgTemp = Html.GetThemedSVG("sensor").ToString();
                    break;
                case eNotificationClass.Low_Battery:
                    svgTemp = Html.GetThemedSVG("lowBattery").ToString();
                    break;
                case eNotificationClass.Advanced:
                    svgTemp = Html.GetThemedSVG("gears").ToString();
                    break;
                case eNotificationClass.Inactivity:
                    svgTemp = Html.GetThemedSVG("hourglass").ToString();
                    break;
                case eNotificationClass.Timed:
                    svgTemp = Html.GetThemedSVG("clock").ToString();
                    break;
            }
        
%>


    <div class="small-list-card"  >
        <div class=" existing-rule-data"  style="width:100%; align-items:center;">
             <div class="svgTemp"><%=svgTemp %></div>
            <div class="triggerDevice__name trigger-name"  style="width:150px; align-items:start;">
                <strong  style="font-size:13px"><%=System.Web.HttpUtility.HtmlDecode(item.Notification.Name) %></strong>
            </div>
            <div class="toggleNoti toggleRule " data-notiid="<%:item.Notification.NotificationID%>" > 
            <div class=" ListBorder<%:item.GatewayNotificationID > 0 ? "Active" : "NotActive"%> notiGateway<%:item.GatewayNotificationID%> gatewayID<%:gateway.GatewayID%> circle__status gridPanel-sensor ">
                <%=Html.GetThemedSVG("circle-check") %>
            </div>
                </div>

            <div class="menu-hover menu-fav"  data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">                   
                        <%=Html.GetThemedSVG("menu") %>
                    </div>
              <ul class="dropdown-menu ddm" style="padding: 0;">
              <%bool showtest = true;
                  if (item.Notification.NotificationID > 0 && showtest)
                            {%>
                        <li>
                            <a class="dropdown-item menu_dropdown_item notitest" title="Send Test" style="cursor: pointer;" id="notiTest" onclick="SendTest('<%=item.Notification.NotificationID %>');">
                                <span><%: Html.TranslateTag("Send Test","Send Test")%></span>
                                <%=Html.GetThemedSVG("sendTest") %>
                            </a>
                        </li>
                        <span id="testMessage_<%=item.Notification.NotificationID %>" style="color: red; font-weight: bold; font-size: 0.8em;"></span>
                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                        <%} %>

              
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/History/<%:item.Notification.NotificationID%>">
                                <span><%: Html.TranslateTag("History","History")%></span>
                                <%=Html.GetThemedSVG("list") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/Triggers/<%:item.Notification.NotificationID%>">
                                <span><%: Html.TranslateTag("Conditions")%></span>
                                <%=Html.GetThemedSVG("conditions") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/ChooseTaskToEdit/<%:item.Notification.NotificationID%>">
                                <span><%: Html.TranslateTag("Tasks")%></span>
                                <%=Html.GetThemedSVG("tasks") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" href="/Rule/Calendar/<%:item.Notification.NotificationID%>">
                                <span><%: Html.TranslateTag("Schedule","Schedule")%></span>
                                <%=Html.GetThemedSVG("schedule") %>
                            </a>
                        </li>
                        <%if (MonnitSession.CustomerCan("Notification_Edit"))
                            { %>
                        <hr />
                        <li>
                            <a class="dropdown-item menu_dropdown_item" onclick="deleteConfirmation(<%=item.Notification.NotificationID %>)" id="list">
                                <span>
                                    <%: Html.TranslateTag("Events/Triggers|Delete Rule")%> 
                                </span>
                                <%=Html.GetThemedSVG("delete") %>
                            </a>
                        </li>
                        <%} %>
                    </ul>
        </div>
    </div>

<%
    }
%>

<script>
    <%-- Use sourceURL to give <script> block in partial a label in DevTools. It will appear in the same location as parent. --%>
    //# sourceURL=AvailableGatewayActions.ascx

$(document).ready(function () {
        $('#totalRules').html(<%= totalRules %>);
        $('#filteredRules').html(<%= filteredRules %>);
    });

    $(function () {
        $(".toggleNoti").click(function (e) {
            TogggleExistingNotification($(this),<%:gateway.GatewayID%>, $(this).data('notiid'), $(this).data("dindex"));
        });
    });


    function TogggleExistingNotification(element, gatewayID, notificationID) {
        let checkbox = $(element).find(".gatewayID" + gatewayID);
        if (checkbox.hasClass("ListBorderActive")) {
            RemoveExistingNotification(checkbox, gatewayID, notificationID)
        }
        else {
            AddExistingNotification(checkbox, gatewayID, notificationID);
        }
    }

    function RemoveExistingNotification(border, gatewayID, notificationID) {

        let params = {};
        params.gatewayID = gatewayID;
        params.notificationID = notificationID;

        $.post("/Overview/RemoveExistingNotificationFromGateway", params, (data) => {
            if (data == "Success") {
                border.removeClass("ListBorderActive");
                border.addClass("ListBorderNotActive");
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }

        });
    }

    function AddExistingNotification(border, gatewayID, notificationID) {
        let params = {};
        var url = "/Overview/AddExistingNotificationToGateway";
        params.gatewayID = gatewayID;
        params.notificationID = notificationID;

        $.post(url, params, function (data) {
            if (data == "Success") {
                border.removeClass("ListBorderNotActive");
                border.addClass("ListBorderActive");
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }
</script>
