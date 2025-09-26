<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<div class="daterangePicker">
    <span>Date Range:</span><input name="historyFromDate" class="historyFromDate" value="<%:MonnitSession.HistoryFromDate.ToString("MM/dd/yyyy") %>" /> - 
    <input name="historyToDate" class="historyToDate" value="<%:MonnitSession.HistoryToDate.ToString("MM/dd/yyyy") %>" />
</div>
<script>
    $(function () {
        setDateRangePicker()
    });
</script>