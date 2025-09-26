<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>


<% if (new Version(Model.GatewayFirmwareVersion) > new Version("1.0.2.0"))
   { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_ResetInterval|Auto Reset","Auto Reset")%> (<%: Html.TranslateTag("Hours","Hours")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" id="ResetInterval" name="ResetInterval" value="<%=  Model.ResetInterval == int.MinValue ? 168 : Model.ResetInterval%>" />
        <a id="resetIntNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.ResetInterval)%>
    </div>
</div>


<script type="text/javascript">

    var resetLabel = '<%: Html.TranslateTag("Hours","Hours")%>';
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    var resetInterval_array = [1, 2, 6, 12, 24, 48, 72, 168, 720, 2191, 4383, 8760];
    $(document).ready(function () {

        $('#ResetInterval').change(function () {
            if ($('#ResetInterval').val() < 0)
                $('#ResetInterval').val(0);

            if ($('#ResetInterval').val() > 8760)
                $('#ResetInterval').val(8760);
        });

        createSpinnerModal("resetIntNum", resetLabel, "ResetInterval", resetInterval_array);

    });

</script>

<%} %>