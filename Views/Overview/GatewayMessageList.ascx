<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>

<table class="table table-hover">
    <thead>
        <tr>
            <th scope="col"><%: Html.TranslateTag("Date","Date")%></th>
            <th scope="col"><%: Html.TranslateTag("Type","Type")%></th>
            <th scope="col"><%: Html.TranslateTag("Signal","Signal")%></th>
            <th scope="col"><%: Html.TranslateTag("Power","Power")%></th>
            <th scope="col"><%: Html.TranslateTag("Messages","Messages")%></th>
        </tr>
    </thead>
    <tbody id="dataList">
    </tbody>
</table>

<div class="text-center" id="loading">
    <div class="spinner-border text-primary" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Overview/GatewayMessageList|Loading","Loading")%>...</span>
    </div>
</div>

<%--<div id="dataHistoryLoad"></div>--%>

<script type="text/javascript">

    //$("#dataList").hide();
    var inProcess = null;
    $(document).ready(function () {
        appendData();
    });

    $('#gatewayHistory').scroll(function () {
        if ($(this).scrollTop() + $(this).innerHeight() +1 >= $(this)[0].scrollHeight) {
            if (inProcess == null) {
                appendData();
            }
        }
    });

    function appendData() {
        var gatewayID = '<%= Model.GatewayID %>';
        var dataMsg = $('.gatewayReading').last().attr('data-guid');
        $('#loading').show();

        inProcess = $.get('/Overview/GatewayHistoryData', { gatewayID: gatewayID, dataMsg: dataMsg }, function (data) {
            if (data != null) {
                $('#loading').hide();
                $("#dataList").append(data);
                //$("#dataList").show();
                inProcess = null;
            }
        });
    }

</script>
