<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DatabaseDetails
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><%= ViewBag.tableName + "||" + ViewBag.Status%></h2>

    <%   string table = ViewBag.tableName; %>
    <%   string status = ViewBag.status; %>


                <div id="dbStatList">

                    <% if(table == "ExternalDataSubscriptionAttempt" && status == "Failure - 1") { %>


                    <div class="bold row">
                        <div class="mobile col-lg-2 col-sm-3 col-xs-6">AccountNumber </div>
                        <div class="mobile col-lg-3 col-sm-4 col-xs-4 visible-lg-block">ExternalDataSubscriptionID </div>
                        <div class="mobile col-lg-1 col-sm-2 col-xs-2">Counts</div>
                        <div class="mobile col-lg-1 col-sm-3">Status</div>
                    </div>


                        <%foreach (iMonnit.Models.DatabaseDetails item in Model)
                        { %>
                            <div class="row">
                                <div class="mobile col-lg-2 col-sm-3 col-xs-6"; style=" overflow-wrap:break-word";><%= item.AccountNumber %></div>
                                <div class="mobile col-lg-3 visible-lg-block"; style="overflow-wrap:break-word"; ><%=item.ExternalDataSubscriptionID %> </div>
                                <div class="mobile col-lg-1 col-sm-2 col-xs-2";  style="overflow-wrap:break-word"; ><%=item.Counts %></div>
                                <div class="mobile col-lg-1 col-sm-3",  style="overflow-wrap:break-word";><%= item.Status  %></div>
                            </div>
                        <% } %>

                    <%} %>

                    <% if(table == "GatewayMessage" && status == "Total Counts") { %>


                    <div class="bold row">
                        <div class="mobile col-lg-2 col-sm-4 col-xs-4">GatewayID </div>
                        <div class="mobile col-lg-2 col-sm-4 col-xs-4">AccountNumber </div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-2">Counts</div>
                    </div>


                        <%foreach (iMonnit.Models.DatabaseDetails item in Model)
                        { %>
                            <div class="row">
                                <div class="mobile col-lg-2 col-sm-4 col-xs-4"; style=" overflow-wrap:break-word";><%= item.GatewayID %></div>
                                <div class="mobile col-lg-2 col-sm-4 col-xs-4"; style="overflow-wrap:break-word"; ><%=item.AccountNumber %> </div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-2"; style="overflow-wrap:break-word";><%=item.Counts %></div>
                            </div>
                        <% } %>

                    <%} %>

                   <% if(table == "NotificationRecorded" && status == "Delivered - Email Sent") { %>


                    <div class="bold row">
                        <div class="mobile col-lg-2 col-sm-2 col-xs-5">AccountNumber </div>
                        <div class="mobile col-lg-1 visible-lg-block">NotificationID </div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-3">SMTP</div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-1">Counts</div>
                    </div>


                        <%foreach (iMonnit.Models.DatabaseDetails item in Model)
                        { %>
                            <div class="row">
                                <div class="mobile col-lg-2 col-sm-2 col-xs-5"; style=" overflow-wrap:break-word";><%= item.AccountNumber %></div>
                                <div class="mobile col-lg-1 visible-lg-block"; style="overflow-wrap:break-word"; ><%=item.NotificationID %> </div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-3";  style="overflow-wrap:break-word";><%=item.SMTP %></div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-1";  style="overflow-wrap:break-word";><%=item.Counts %></div>
                            </div>
                        <% } %>

                    <%} %>

                    <% if(table == "NotificationRecorded" && status == "Not Delivered - Email Failed") { %>


                    <div class="bold row">
                        <div class="mobile col-lg-2 col-sm-2 col-xs-5">AccountNumber </div>
                        <div class="mobile col-lg-1 visible-lg-block">NotificationID </div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-3">SMTP</div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-1">Counts</div>
                    </div>


                        <%foreach (iMonnit.Models.DatabaseDetails item in Model)
                        { %>
                            <div class="row">
                                <div class="mobile col-lg-2 col-sm-2 col-xs-5"; style=" overflow-wrap:break-word";><%= item.AccountNumber %></div>
                                <div class="mobile col-lg-1 visible-lg-block"; style="overflow-wrap:break-word"; ><%=item.NotificationID %> </div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-3";  style="overflow-wrap:break-word";><%=item.SMTP %></div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-1";  style="overflow-wrap:break-word";><%=item.Counts %></div>
                            </div>
                        <% } %>

                    <%} %>

                    <% if (table == "NotificationRecorded" && status == "InActivity Notifications - Unique Notifications")
                       { %>


                    <div class="bold row">
                        <div class="mobile col-lg-2 col-sm-4 col-xs-6">TopLevelAccount </div>
                        <div class="mobile col-lg-3 col-sm-4 col-xs-4">AccountNumber </div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-2">Counts</div>
                    </div>


                        <%foreach (iMonnit.Models.DatabaseDetails item in Model)
                        { %>
                            <div class="row">
                                <div class="mobile col-lg-2 col-sm-4 col-xs-6"; style=" overflow-wrap:break-word";><%= item.Message %></div>
                                <div class="mobile col-lg-3 col-sm-4 col-xs-4"; style="overflow-wrap:break-word"; ><%=item.AccountNumber %> </div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-2";  style="overflow-wrap:break-word";><%=item.Counts %></div>
                            </div>
                        <% } %>

                    <%} %>

                    <% if(table == "ExceptionLog" ) { %>


                    <div class="bold row">
                        <div class="mobile col-lg-2 col-sm-4 col-xs-4";>Message </div>
                        <div class="mobile col-lg-8 col-sm-5 col-xs-7";>Stacktrace </div>
                        <div class="mobile col-lg-1 col-sm-3 col-xs-2";>ExceptionDate</div>
                    </div>


                        <%foreach (iMonnit.Models.DatabaseDetails item in Model)
                        { %>
                            <div class="row">
                                <div class="mobile col-lg-2 col-sm-4 col-xs-4"; style=" overflow-wrap:break-word";><%= item.Message %></div>
                                <div class="mobile col-lg-8 col-sm-5 col-xs-7"; style="overflow-wrap:break-word"; ><%=item.StackTrace %> </div>
                                <div class="mobile col-lg-1 col-sm-3 col-xs-2";  style="overflow-wrap:break-word";><%=item.ExceptionDate %></div>                            </div>
                        <% } %>

                    <%} %>

                </div>


</asp:Content>
