<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<GatewayTestingModel>" %>

<link rel="stylesheet" type="text/css" href="/Content/Testing/css/testingHistory.css" />
<script type="text/javascript" src="/Content/Testing/js/testingHistory.js" ></script>

<%
    Gateway gw = (Gateway)ViewBag.Gateway;
%>

<div class="col-12 device_detailsRow__card">
    <div class="x_panel scrollParentLarge" style="height: 800px;">
        <div class="x_title">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div>
                        <span>
                            <%=Html.GetThemedSVG("gateway") %>
                        </span>
                        <span class="ms-1">
                            <%: Html.TranslateTag("Gateway History", "Gateway History")%>
                        </span>
                    </div>
                    <div>
                        <span style="font-weight: 800">
                            <%: Html.TranslateTag("# Records Loaded")%>:
                        </span>
                        <span id="testingHistoryRecordCount" class="ms-1">
                            <%:Model.Messages.Count %>
                        </span>
                    </div>
                    <div>
                        <span style="font-weight: 800">
                            GatewayID:
                        </span>
                        <span style="color: #0094ff" class="ms-1">
                            <%:gw.GatewayID %>
                        </span>
                    </div>
                    <div>
                        <div id="textMinus" class="btn btn-secondary">&minus;</div>
                        <div id="textPlus" class="btn btn-secondary">&plus;</div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>

        <%--        <table class="table" style="width: 100%;">
            <thead>
                <tr>
                    <th style="width: 10%" scope="col"><%: Html.TranslateTag("Date", "Date")%></th>
                    <th style="width: 3%" scope="col"><%: Html.TranslateTag("Type", "Type")%></th>
                    <th style="width: 7%" scope="col"><%: Html.TranslateTag("Device ID", "Device ID")%></th>
                    <th style="width: 5%" scope="col"><%: Html.TranslateTag("Content", "Content")%></th>
                </tr>
            </thead>
        </table>--%>

        <div class="row testingHistoryHeader">
            <div class="col-3"><%: Html.TranslateTag("Date", "Date")%></div>
            <div class="col-2"><%: Html.TranslateTag("Type", "Type")%></div>
            <div class="col-2"><%: Html.TranslateTag("Device ID", "Device ID")%></div>
            <div class="col-5"><%: Html.TranslateTag("Content", "Content")%></div>
        </div>

        <div class="x_content hasScroll" id="gatewayHistory" style="margin-top: 0px; height: 685px;">
            <div id="testingGatewayHistoryTable" data-id="<%= gw.GatewayID %>">
                <%--            <table class="table table-hover" style="width: 100%;">
                <tr>
                </tr>
                <tbody>--%>
                <div class="noDataMessagesRow col-12" style="<%= Model.Messages.Count > 0 ? "display:none;" : "" %>"><%: Html.TranslateTag("No Data") %></div>
                <%
                    //List<DateTime> gatewayTestingMessageKeys = Model.Messages.Keys.OrderByDescending(x => x).ToList();
                    //foreach (DateTime messageDate in gatewayTestingMessageKeys)
                    //{
                    //    List<GatewayTestingMessage> msgModel = Model.Messages[messageDate];
                    foreach(GatewayTestingMessage msg in Model.Messages)
                    { 
                %>
                <%--<%= iMonnit.Controllers.TestingController.TestingGatewayHistoryRow(gw, msgModel) %>--%>
                <%= iMonnit.Controllers.TestingController.TestingGatewayHistoryRow(gw, msg) %>
                <%
                    }%>
                <%--                </tbody>
            </table>--%>
            </div>
        </div>
        <div class="text-center" id="loading" style="display:none;">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
        </div>
    </div>
</div>
<script>
    <%= ExtensionMethods.LabelPartialIfDebug("LoadGatewayHistory.ascx") %>

    //var ss = document.querySelector('link[href*=history]');
    //var s = document.querySelector('link[href*=history]').sheet;
    //let fff = () => {
    //    for (const rule of s.cssRules) {
    //        if (rule.selectorText == '.testingHistoryRow') {
    //            return rule
    //        }
    //    }
    //}

    $(document).ready(function () {
        //$('.testingHistoryRow').css('font-size', <%= MonnitSession.TestingToolSession.FontSize %>);
        //console.log(<%= MonnitSession.TestingToolSession.FontSize %>);
        changeFontSize('.testingHistoryRow', "<%= MonnitSession.TestingToolSession.FontSize %>px");
    });

    //$('#textMinus,#textPlus').click(
    //    function (e) {
    //        e.stopPropagation();
    //        this.blur();

    //        var i = 0;
    //        if (this.id === 'textMinus')
    //            i = -1;
    //        if (this.id === 'textPlus')
    //            i = 1;

    //        var cur = parseInt($('.testingHistoryRow').css('font-size'));
    //        var nxt = cur + i;
    //        console.log(`${cur} => ${nxt}`);
    //        //$('.testingHistoryRow').css('font-size', nxt);
    //        changeFontSize('.testingHistoryRow', nxt + 'px');

    //        $(this).toggleClass('btn-secondary btn-primary');

    //        setTimeout(
    //            () => {
    //                $(this).toggleClass('btn-secondary btn-primary');
    //            }
    //            , 500
    //        );

    //        $.post('/Testing/SetTestingToolFontSizePx/'
    //            , { fontSize: nxt }
    //            , (data) => {
    //                changeFontSize('.testingHistoryRow', data);
    //            }
    //        );
    //    }
    //)


</script>