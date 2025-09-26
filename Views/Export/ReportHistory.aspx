<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ReportSchedule>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report History
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
<!-- page content -->
    <div class="container-fluid">
        <span><%:Html.Partial("_ReportHeader") %></span>
        <!-- Event List View -->
            <div class="report-history_container" id="hook-two">
                <%
					//(List<ReportScheduleResult> ReportScheduleResultList, List<ScheduledReportsToStorage> ScheduledReportsToStorageList) reportHistory = (new List<ReportScheduleResult>(), new List<ScheduledReportsToStorage>());
					var reportHistory = ReportScheduleResult.ReportHistoryByReportScheduleID(Model.ReportScheduleID);
					List<ReportScheduleResult> rsr = reportHistory.Item1; // new List<ReportScheduleResult>(); // reportHistory.ReportScheduleResultList;
					List<ScheduledReportsToStorage> srs = reportHistory.Item2; // new List<ScheduledReportsToStorage>(); // reportHistory.ScheduledReportsToStorageList;

					if (rsr.Count > 0)
					{%>
                <div class="x_title">
                    <h2 style="font-weight: bold;"><%: Html.TranslateTag("Export/ReportHistory|Report History","Report History") %></h2>
                    <div class="clearfix"></div>
                </div>

                   <table class="newTable2">
                    <thead>
                        <tr  style="border-bottom: 1px solid #5153561f; background: #d7d7d785">
                            <th scope="col"><%: Html.TranslateTag("Export/ReportHistory|Report Name","Report Name") %></th>
                            <th scope="col"><%: Html.TranslateTag("Export/ReportHistory|Report Send Date","Report Send Date") %></th>
                            <th scope="col"><%: Html.TranslateTag("Export/ReportHistory|Report Result","Report Result") %></th>
                            <th scope="col"><%: Html.TranslateTag("Export/ReportHistory|Retrieve","Retrieve") %></th>
                        </tr>
                    </thead>
                    <tbody>
                
                        <%
                        foreach (var result in rsr)
                        {
							var files = srs.Where(f => f.ReportScheduleResultID == result.ReportScheduleResultID).ToList();
                            if (files.Count > 0)
                            {
                                foreach (var file in files)
                                {%>
                        <tr class="table-cable-hov">
                            <td scope="row" data-label="Report Name"><%= file.ReportFileName%></td>
                            <td data-label="Send Date"><%:result.RunDate.OVToLocalDateTimeShort()%></td>
                            <td data-label="Result"><%:result.Result%></td>
                            <td data-label="Retrieve">
                                <a class="dlcloud" href="/Export/GetReportFile/?guid=<%:file.GUID%>&ScheduledReportsToStorageID=<%:file.ScheduledReportsToStorageID%>">
                                    <%=Html.GetThemedSVG("download-file") %>
                                </a>
                            </td>
                        </tr>
                        <% }
                        }
                        else
                        {%>
                            <tr class="table-cable-hov">
                            <td scope="row" data-label="Report Name">---</td>
                            <td data-label="Send Date"><%:result.RunDate.OVToLocalDateTimeShort()%></td>
                            <td   data-label="Result"><%:((int)result.ResultType > 1)? "Not Processed" : result.Result%></td>
                            <td data-label="Recipients"><%:result.Recipients%></td>
                        </tr>
                        <% } } %>
                    </tbody>
                </table>
            </div>
            <%}%>
            <br />

            <%if (rsr.Count <= 0)
                { %>
            <div>
                <h2><%: Html.TranslateTag("Export/ReportHistory|No reports recorded for this period.", "No reports recorded for this period.")%></h2>
            </div>
            <%} %>
        </div>

<style type="text/css">

    .newTable2 {
        border: 1px solid #ccc;
        border-collapse: collapse;
        margin: 0;
        padding: 0;
        width: 100%;
        table-layout: fixed;
    }

    .newTable2 tr {
        background-color: #f8f8f8;
        border: 1px solid #ddd;
        padding: .35em;
        border-radius: 5px;
    }

    .newTable2 th,
    .newTable2 td {
        padding: .625em;
        text-align: center;
        word-break:break-word;
    }

    .newTable2 th {
        font-size: .8rem;
        letter-spacing: .08em;
        /*  text-transform: uppercase;*/
    }

    @media screen and (max-width: 600px) {
        .newTable2 {
           border: 0;
        }

    .newTable2 thead {
        border: none;
        clip: rect(0 0 0 0);
        height: 1px;
        margin: -1px;
        overflow: hidden;
        padding: 0;
        position: absolute;
        width: 1px;
    }

    .newTable2 tr {
        border-bottom: 3px solid #ddd;
        display: block;
        margin-bottom: .625em;
        box-shadow: 0 1px 2px rgba(0,0,0,0.05), inset 0px 1px 3px rgba(0,0,0,0.1);
    }

.newTable2 td {
    border-bottom: 1px solid #ddd;
    display: block;
    font-size: .8em;
    text-align: right;
}

    .newTable2 td::before {
        content: attr(data-label);
        float: left;
        font-weight: bold;
        text-transform: uppercase;
        color:var(--primary-color);
        margin-right:10px;
    }

    .newTable2 td:last-child {
        border-bottom: 0;
    }
  }

</style>

</asp:Content>
