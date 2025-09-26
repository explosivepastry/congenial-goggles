<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="daterangePicker">
    <span><%: Html.TranslateTag("Overview/HistoryDatePicker|Date Range","Date Range")%>: </span> <input name="historyFromDate" class="historyFromDate" value="<%:MonnitSession.HistoryFromDate.ToShortDateString() %>" /> - 
    <input name="historyToDate" class="historyToDate" value="<%:MonnitSession.HistoryToDate.ToShortDateString() %>" />
</div>
<script>
    $(function () {
        setDateRangePicker()
    });
</script>