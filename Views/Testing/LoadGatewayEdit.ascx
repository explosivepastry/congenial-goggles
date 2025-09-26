<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<form class="form-horizontal form-label-left" action="/Testing/GatewayEdit/" id="submitGatewayEdit" method="post">
    <input type="hidden" name="GatewayID" value="<%:Model.GatewayID %>" />
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            Gateway Name
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="Name" name="Name" value="<%:Model.Name %>">
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            Heartbeat Minutes
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" name="ReportInterval" id="ReportInterval" value="<%:Model.ReportInterval %>">
            <a id="reportNum" style="cursor: pointer;" class="mbsc-comp">
                <svg xmlns="http://www.w3.org/2000/svg" id="svg_list" class="svg_icon" viewBox="0 0 15.417 12.5">
                    <path d="M3.75,9.5A1.25,1.25,0,1,0,5,10.75,1.248,1.248,0,0,0,3.75,9.5Zm0-5A1.25,1.25,0,1,0,5,5.75,1.248,1.248,0,0,0,3.75,4.5Zm0,10A1.25,1.25,0,1,0,5,15.75,1.254,1.254,0,0,0,3.75,14.5Zm2.5,2.083H17.917V14.917H6.25Zm0-5H17.917V9.917H6.25Zm0-6.667V6.583H17.917V4.917Z" transform="translate(-2.5 -4.5)"></path>
                </svg>
            </a>
        </div>
    </div>

    <div class="row">
        <div class="col-12 text-end">
            <button type="submit" value="Save" class="btn btn-primary">Save</button>
            <div style="clear: both;"></div>
            <div id="submitGatewayEditResult"></div>
        </div>
    </div>
    <div class="clearfix"></div>
</form>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        var mobiLabel = 'Minutes';

        var reportInterval_array = [1, 5, 10, 20, 30, 60, 120];
        $(document).ready(function () {

            $('#submitGatewayEdit').submit(function (e) {
                e.preventDefault();

                var formData = $(this).serializeArray();

                var obj = $(this);
                var oldHtml = $(this).html();
                $(this).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);

                $.post(this.action, formData, function (data) {
                    if (data == "Success") {
                        $('#gatewayEditTab_<%:Model.GatewayID%>-tab').click();
                    } else {
                        obj.html(oldHtml);
                        $('#submitGatewayEditResult').html(data);
                    }
                });
            });

            $('#ReportInterval').change(function () {
                if ($('#ReportInterval').val() < 0)
                    $('#ReportInterval').val(0);

                if ($('#ReportInterval').val() > 720)
                    $('#ReportInterval').val(720);
            });

            createSpinnerModal("reportNum", mobiLabel, "ReportInterval", reportInterval_array);

        });
    </script>