<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string label = "";
    MonnitApplicationBase.ProfileLabelForScale(Model, out label);

    Dictionary<string, object> dic = new Dictionary<string, object>();
    if (!Model.CanUpdate)
    {
        dic.Add("disabled", "disabled");
        ViewData["disabled"] = true;
    }

    if (Model.MaximumThreshold == 0xFFFFFFFF || Model.MaximumThreshold == 4294967295)
    {
        bool IsDirty = Model.ProfileConfigDirty;
        Model.MaximumThreshold = 0;
        Model.ProfileConfigDirty = IsDirty; //Dont let the above mark the profile dirty.
    }

%>


<%--Maintain Relay Position (Max Thresh)--%>

<%if (new Version(Model.FirmwareVersion) >= new Version("26.55.52.19"))
    {%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3 ">
        <%: Html.TranslateTag("Maintain Relay Position","Maintain Relay Position")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshChk" id="MaximumThreshChk" <%= Model.MaximumThreshold == 1 ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.MaximumThreshold, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>
<%}%>


<script type="text/javascript">

    $('#MaximumThreshChk').change(function () {
        if ($('#MaximumThreshChk').prop('checked')) {
            $('#MaximumThreshold').val(1);
        } else {
            $('#MaximumThreshold').val(0);
        }
    });
</script>
