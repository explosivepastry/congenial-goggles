<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ChooseType
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%
Account acc = MonnitSession.CurrentCustomer.Account;
var GatewayList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
acc.ClearSubscritions(); %>

<%=Html.Partial("_CreateNewRuleProgressBar") %>

    <div class="container-fluid">
        <div class="rule_container">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Rule/ChooseType|Create a Rule","Create a Rule")%>
                </div>
                <div class="nav navbar-right panel_toolbox">
                    <!-- help button  choosetype-->
                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Rule Help","Rule Help") %>" data-bs-target=".ruleHelp">
                   <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div> </a>
                </div>
            </div>
            <h3 class="rule-title"><%: Html.TranslateTag("Rule/ChooseType|Choose the Rule Type","Choose the Rule Type")%></h3>

            <div class="rule-sets ">
                <a href="/Rule/CreateApplicationRule?ruleClass=Application" class="btn-lg  newActionBtn" style="position: relative;">
                    <span class="rule-svg"><%=Html.GetThemedSVG("sensor") %></span>
                    <%: Html.TranslateTag("Rule/ChooseType|Sensor Reading","Sensor Reading")%>
                </a>
            </div>

            <div class="rule-sets">
                <a href="/Rule/CreateApplicationRule?ruleClass=Low_Battery" class="btn-lg my-2 newActionBtn" style="position: relative;">
                    <span class="rule-svg"><%=Html.GetThemedSVG("lowBattery") %></span>
                    <%: Html.TranslateTag("Rule/ChooseType|Device Battery Level","Device Battery Level")%>
                </a>
            </div>

            <div class="rule-sets">
                <a href="/Rule/CreateApplicationRule?ruleClass=Inactivity" class="btn-lg newActionBtn" style="position: relative;">
                    <span class="rule-svg"><%=Html.GetThemedSVG("hourglass") %></span>
                    <%: Html.TranslateTag("Rule/ChooseType|Device Inactivity Status","Device Inactivity Status")%>
                </a>
            </div>

            <%if (acc.CurrentSubscription.AccountSubscriptionType.Can("use_scheduled_notifications"))
                {%>
            <div class="rule-sets">
                <a href="/Rule/CreateApplicationRule?ruleClass=Timed" class="btn-lg newActionBtn" style="position: relative;">
                    <span class="rule-svg"><%=Html.GetThemedSVG("clock") %></span>
                    <%: Html.TranslateTag("Rule/ChooseType|Scheduled Time","Scheduled Time")%>
                </a>
            </div>

            <%} %>
            <%if (acc.CurrentSubscription.AccountSubscriptionType.Can("use_advanced_notifications"))
                {%>
            <div class="rule-sets">
                <a href="/Rule/CreateAdvancedRule" class=" btn-lg  my-2 newActionBtn" style="position: relative;">
                    <span class="rule-svg"><%=Html.GetThemedSVG("gears") %></span>
                    <%: Html.TranslateTag("Rule/ChooseType|Advanced Rule","Advanced Rule")%>
                </a>
            </div>
            <%} %>
        </div>
    </div>

    <!-- pageHelp button modal -->
    <div class="modal fade ruleHelp" style="z-index: 2000!important; background:rgba(0,0,0,0.5);" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/ChooseType|Rule Options","Rule Options")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body">
                    <div class="container">
                        <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Sensor Reading","Sensor Reading")%>
                            </div>

                            <div class=" word-def">
                                <%: Html.TranslateTag("Rule/ChooseType|Rule based on sensor activity or reading.","Rule based on sensor activity or reading")%>.
                            </div>
                        </div>
                        <hr />

                             <div class="row">
                            <div class="col- word-choice">
                                <%: Html.TranslateTag("Rule/ChooseType|Device Battery Level","Device Battery Level")%>
                            </div>
                            <div class="word-def"> 
                                <%: Html.TranslateTag("Rule/ChooseType|Receive a notification when the battery level drops below a certain percentage.","Receive a notification when the battery level drops below a certain percentage")%>.
                            </div>
                        </div>
                        <hr />

                           <div class="row">
                            <div class="col- word-choice">      
                                <%: Html.TranslateTag("Rule/ChooseType|Device Inactivity Status","Device Inactivity Status")%>
                            </div>

                            <div class=" word-def"> 
                                <%: Html.TranslateTag("Rule/ChooseType|Receive a notification when the sensor or gateway doesn’t communicate for an extended period.","Receive a notification when the sensor or gateway doesn’t communicate for an extended period")%>.
                            </div>
                        </div>
                        <hr />

                           <div class="row">
                            <div class="col- word-choice">      
                                  <%: Html.TranslateTag("Rule/ChooseType|Scheduled Time","Scheduled Time")%>
                            </div>

                            <div class=" word-def"> 
                               <%: Html.TranslateTag("Rule/ChooseType|Get a notification on a recurring time and date.","Get a notification on a recurring time and date")%>.
                            </div>
                        </div>
                        <hr />

                          <div class="row">
                            <div class="col- word-choice">      
                             <%: Html.TranslateTag("Rule/ChooseType|Advanced Rule","Advanced Rule")%>
                            </div>

                            <div class=" word-def"> 
                          <%: Html.TranslateTag("Rule/ChooseType|Rules based on predefined conditions in the system. For example, receive notifications based on specific settings like aware and power states.","Rules based on predefined conditions in the system. For example, receive notifications based on specific settings like aware and power states")%>.
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

</asp:Content>
