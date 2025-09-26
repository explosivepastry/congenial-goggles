<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
  
 <%
 long templatecsnetid = ConfigData.AppSettings("TemplateCSNetID").ToLong();
 List<Sensor> templatelist = Sensor.LoadByCSNetIDAndApplicationID(templatecsnetid, Model.ApplicationID);
 %>

    <div class="col-12 col-md-3 " >
        <%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_UseCaseOptions|How will you use your sensor?","How will you use your sensor")%>?
    </div>

    <div class="col sensorEditFormInput" style="min-width:250px;">
        <select id="TemplateSensorID" name="TemplateSensorID" onchange="TemplateChanged()" class="form-select user-dets">
            <option style="display: flex; justify-content: space-between;" value="" disabled selected><%: Html.TranslateTag("Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_UseCaseOptions|Select Use Case","Select Use Case")%></option>
            <%foreach (Sensor t in templatelist)
                {
                    //template format: {Template Order_Template Name_ApplicationID} ex: 01_Fridge_002
                    string[] subs = t.SensorName.Split('_'); %>

            <option style="display: flex; justify-content: space-between;" value="<%=t.SensorID%>">
                <%= subs.Length > 1 ? subs[1] : t.SensorName%>
            </option>
            <%}%>
            <option style="display: flex; justify-content: space-between;" value="<%=Model.SensorID%>"><%: Html.TranslateTag("Custom","Custom")%></option>
        </select>
    </div>
</div>

<div class="clearfix"></div>

<script type="text/javascript">

<%= ExtensionMethods.LabelPartialIfDebug("Options.ascx") %>
<%--<%= ExtensionMethods.JSCheckDebugMode() %>--%>

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    function TemplateChanged() {
        TemplateLoad();
        HideOrShow();
    }

    $(document).ready(function() {
        HideOrShow();
    });

    //$('#TemplateSensorID').mobiscroll().select({
    //    theme: 'ios',
    //    display: popLocation,
    //    onSet: function (event, inst) {
    //        TemplateChanged();
    //    }
    //});

    function TemplateLoad() {
        $.get("/Setup/LoadUseCaseTemplateValues", { id: $('#TemplateSensorID').val() }, function (data) {
            $('#ValuesThatGetRefreshed').html(data);
        });
    };

    function HideOrShow() {
        if ($('#TemplateSensorID').val() == null)
            $('#HiddenUntilUseCaseSelected').hide();
        else
            $('#HiddenUntilUseCaseSelected').show();
    }

</script>

