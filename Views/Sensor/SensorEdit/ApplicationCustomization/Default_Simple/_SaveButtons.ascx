<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool AllowSave = false;
    if (Model.CanUpdate || (!Model.CanUpdate && Model.LastCommunicationDate == DateTime.MinValue))
        AllowSave = true;
%>

<div class="clearfix"></div>
<div class="ln_solid"></div>
<div class="form-group">
    <div class="col-12 col-sm-4">
        <%if (!AllowSave)
            {%>
        <strong><%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_SaveButtons|This Sensor is pending updates","This Sensor is pending updates")%></strong>
        <%} %>
    </div>

    <div class="col-12 col-sm-8 text-end">
        <%if (AllowSave && ViewBag.canEdit == true)
            { %>
        <button class="btn btn-primary" type="button" id="save" onclick="checkForm(<%:Model.SensorID %>, <%:Model.PowerSource.MinimumRecommendedHeartbeat%>, <%:Model.ApplicationID%>);" style="margin-right: 5%;">
            <%: Html.TranslateTag("Save","Save")%>
        </button>

        <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            <%: Html.TranslateTag("Saving...","Saving...")%>
        </button>
        <%}
            else
            {%>
        <button class="btn btn-primary" type="button" id="skip_btn">
            <%: Html.TranslateTag("Skip","Skip")%>
        </button>
        <%} %>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $('#skip_btn').click(function () {
            window.location = '/Setup/StatusVerification/<%=Model.SensorID%>';
        });
    });

</script>
