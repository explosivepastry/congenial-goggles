<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<% if (new Version(Model.GatewayFirmwareVersion) > new Version("1.0.2.0"))
   { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3" >
        <%: Html.TranslateTag("Gateway/_SingleQueueExpiration|Data Expiration","Data Expiration")%> (<%: Html.TranslateTag("Hours","Hours")%>)
    </div>
    <div class="col sensorEditFormInput" >
        <input class="form-control" type="number" id="SingleQueueExpiration" name="SingleQueueExpiration" value="<%=  Model.SingleQueueExpiration == long.MinValue ? 12 : Model.SingleQueueExpiration%>"/>
        <a id="singleQueExpNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.SingleQueueExpiration)%>
    </div>
</div>


<script type="text/javascript">

    var singleQueExpLabel = '<%: Html.TranslateTag("Hours","Hours")%>';

    var singleQueueExpiration_array = [1, 2, 4, 6, 8, 10, 12, 24, 48, 72];
    $(document).ready(function () {

		var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $('#SingleQueueExpiration').change(function () {
            if ($('#SingleQueueExpiration').val() < 1)
                $('#SingleQueueExpiration').val(1);

            if ($('#SingleQueueExpiration').val() > 65535)
                $('#SingleQueueExpiration').val(65535);
        });

        createSpinnerModal("singleQueExpNum", singleQueExpLabel, "SingleQueueExpiration", singleQueueExpiration_array);

    });

</script>
<%} %>