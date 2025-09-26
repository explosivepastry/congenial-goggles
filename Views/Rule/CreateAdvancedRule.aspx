<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateAdvancedRule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%Sensor sensor = ViewData["Sensor"] as Monnit.Sensor; %>
    <%Account acc = MonnitSession.CurrentCustomer.Account;
        acc.ClearSubscritions(); %>


    <%=Html.Partial("_CreateNewRuleProgressBar") %>


    <%--    Advanced Rules--%>

    <div class="container-fluid 1111">
        <div class="x_panel col-12 mt-4 shadow-sm rounded d-flex flex-column" style="">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Select Advanced Rule","Select Advanced Rule")%>
                </div>
            </div>
            <div style="margin: 5px; background: #eee; display: flex; justify-content: space-between; align-items: center; padding: 10px;">
                <font color="gray">
                    <%: Html.TranslateTag("Events/Index|Click Rule to enable","Click Rule to enable")%>
                </font>

            </div>
            <div class="clearfix"></div>
            <div class=" hasScroll-rule">

                <%foreach (AdvancedNotification an in AdvancedNotification.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID))
                    {%>

                <div class="advanced-card-rule">
                        <div class="adv-card">
                            <a class="adv-rule-title" href="/Rule/CreateApplicationRule?ruleClass=Advanced&advancedNotificationID=<%=an.AdvancedNotificationID%>">
                                    <div class="glance-text" style="width: 100%;">
                                        <div class="glance-name"><%=an.Name%></div>
                                      
                                        <div class="glance-reading">
                                        </div>
                                    </div>
                            </a>
                       

                             <div class="adv-rule-info">
                                <a class="helpIco help-hover" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Advanced Rule Help") %>" onclick="showAdvancedInfo('<%=Html.TranslateTag(an.Name) %>','<%=Html.TranslateTag(an.HelpText) %>');" data-bs-target=".advancedRuleHelp">
                                    <%--<img src="../../Content/images/iconmonstr-help-2-240 (1).png" style="height: 18px; margin: 0px; margin-top: 5px; margin-right: 5px; float: right;">--%>
                                     <%=Html.GetThemedSVG("help") %>
                                </a>
                            </div>

                        </div>

                    
                </div>
                <%}%>
            </div>
        </div>
    </div>
    <div class="modal fade advancedRuleHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Rule/ChooseType|Advanced Rules")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" id="advancedModalHelp">
                    <div class="row">
                        <div class="word-choice" id="nameDiv">
                    
                        </div>
                        <div class="word-def"  id="helpTextDiv">
                            
                            <hr />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-bs-dismiss="modal"><%: Html.TranslateTag("Close","Close")%></button>
                </div>
            </div>
        </div>
    </div>

    <script>

        function showAdvancedInfo(advancedNotiName, HelpText) {

            $('#nameDiv').html(advancedNotiName);
            $('#helpTextDiv').html(HelpText);
        }


    </script>

</asp:Content>
