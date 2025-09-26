<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportSchedule>" %>


<div class="col-12 shadow-sm my-4 rounded">
    <div class="view-btns view-btns-half deviceView_btn_row rounded">
        <a href="/Export/ReportIndex" class="btn btn-lg btn-fill">
            <div class=" btn-secondaryToggle btn-lg btn-fill "><%=Html.GetThemedSVG("book") %><span class="extra"><%: Html.TranslateTag("Reports","Reports") %></span></div>
        </a>
        <a href="/Export/ReportHistory/<%:Model.ReportScheduleID %>" class="deviceView_btn_row__device">
            <div class="btn-<%:Request.Path.StartsWith("/Export/ReportHistory")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("list") %>
                <span class="extra">
                    <%: Html.TranslateTag("Export/ReportHeader|Report History","History") %>
                </span>
            </div>
        </a>
        <a href="/Export/ReportEdit?id=<%:Model.ReportScheduleID %>&queryId=<%:Model.ReportQueryID %>" class="deviceView_btn_row__device">
            <div class="btn-<%:Request.Path.StartsWith("/Export/ReportEdit")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("edit") %>
                <span class="extra" onclick="reportEdit()">
                    <%: Html.TranslateTag("Export/ReportHeader|Report Edit","Edit") %>
                </span>
            </div>
        </a>
        <a href="/Export/ReportRecipient/<%:Model.ReportScheduleID %>" class="deviceView_btn_row__device">
            <div class="btn-<%:Request.Path.StartsWith("/Export/ReportRecipient")?"active-fill shadow mb-lg-2":" " %> btn-lg btn-fill btn-secondaryToggle">
                <%=Html.GetThemedSVG("contacts") %>
                <span class="extra">
                    <%: Html.TranslateTag("Export/ReportHeader|Report Recipients","Recipients") %>
                </span>
            </div>
        </a>
    </div>
</div>

<script>
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>

<style>
    .view-btns .svg_icon {
        height: 15px;
        width: 15px;
        fill: black!important;
    }

    .btn-active-fill .svg_icon {
        fill: #fff!important;
    }
</style>
