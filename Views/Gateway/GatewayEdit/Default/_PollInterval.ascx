<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_PollInterval|Poll Rate Minutes (default","Poll Rate Minutes (default")%>: <%:Math.Round(Model.GatewayType.DefaultPollInterval) %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" id="PollInterval" name="PollInterval" value="<%=  Model.PollInterval%>" />
        <a id="pollNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.PollInterval)%>
    </div>
</div>

<script type="text/javascript">

    var mobiLabel = '<%: Html.TranslateTag("Minutes","Minutes")%>';

    var pollInterval_array = [0, 1, 2, 5, 10, 30];
    $(document).ready(function () {

        $('#PollInterval').change(function () {
            if ($('#PollInterval').val() < 0)
                $('#PollInterval').val(0);

            if ($('#PollInterval').val() > 720)
                $('#PollInterval').val(720);
        });

        createSpinnerModal("pollNum", mobiLabel, "PollInterval", pollInterval_array);

    });

</script>
