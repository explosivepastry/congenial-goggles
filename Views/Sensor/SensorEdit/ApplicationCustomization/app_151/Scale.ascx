<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <%    
        string datumName0 = Model.GetDatumName(0);
        string datumName1 = Model.GetDatumName(1);
        string datumName2 = Model.GetDatumName(2);
        string datumName3 = Model.GetDatumName(3);
        string datumName4 = Model.GetDatumName(4);
    %>

    <div class="formBody">
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 1 Name")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" class="form-control user-dets" <%=Model.CanUpdate ? "" : "disabled" %> id="datumName0" name="datumName0" value="<%= Model.GetDatumName(0)%>" />
                <%: Html.ValidationMessageFor(model => datumName0)%>
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 1 Closed  Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact1zeroValueLabel" name="Contact1zeroValueLabel" value="<%:  (FiveInputDryContact.GetContact_ZeroValue(Model.SensorID,1)) %>" />
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 1 Open Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact1oneValueLabel" name="Contact1oneValueLabel" value="<%: (FiveInputDryContact.GetContact_OneValue(Model.SensorID,1)) %>" />
            </div>
        </div>
        <hr />
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 2 Name")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" class="form-control user-dets" id="datumName1" name="datumName1" value="<%= Model.GetDatumName(1)%>" />
                <%: Html.ValidationMessageFor(model => datumName1)%>
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 2 Closed  Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact2zeroValueLabel" name="Contact2zeroValueLabel" value="<%:  (FiveInputDryContact.GetContact_ZeroValue(Model.SensorID,2)) %>" />
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 2 Open Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact2oneValueLabel" name="Contact2oneValueLabel" value="<%: (FiveInputDryContact.GetContact_OneValue(Model.SensorID,2)) %>" />
            </div>
        </div>
                <hr />
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 3 Name")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" class="form-control user-dets" id="datumName2" name="datumName2" value="<%= Model.GetDatumName(2)%>" />
                <%: Html.ValidationMessageFor(model => datumName2)%>
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 3 Closed  Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact3zeroValueLabel" name="Contact3zeroValueLabel" value="<%:  (FiveInputDryContact.GetContact_ZeroValue(Model.SensorID,3)) %>" />
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 3 Open Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact3oneValueLabel" name="Contact3oneValueLabel" value="<%: (FiveInputDryContact.GetContact_OneValue(Model.SensorID,3)) %>" />
            </div>
        </div>
                <hr />
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 4 Name")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" class="form-control user-dets" id="datumName3" name="datumName3" value="<%= Model.GetDatumName(3)%>" />
                <%: Html.ValidationMessageFor(model => datumName3)%>
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 4 Closed  Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact4zeroValueLabel" name="Contact4zeroValueLabel" value="<%:  (FiveInputDryContact.GetContact_ZeroValue(Model.SensorID,4)) %>" />
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 4 Open Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact4oneValueLabel" name="Contact4oneValueLabel" value="<%: (FiveInputDryContact.GetContact_OneValue(Model.SensorID,4)) %>" />
            </div>
        </div>
                        <hr />
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 5 Name")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input type="text" class="form-control user-dets" id="datumName4" name="datumName4" value="<%= Model.GetDatumName(4)%>" />
                <%: Html.ValidationMessageFor(model => datumName4)%>
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 5 Closed  Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact5zeroValueLabel" name="Contact5zeroValueLabel" value="<%:  (FiveInputDryContact.GetContact_ZeroValue(Model.SensorID,5)) %>" />
            </div>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_151|Contact 5 Open Value")%>:
            </div>
            <div class="col sensorEditFormInput">
                <input class="form-control" type="text" id="Contact5oneValueLabel" name="Contact5oneValueLabel" value="<%: (FiveInputDryContact.GetContact_OneValue(Model.SensorID,5)) %>" />
            </div>
        </div>
    </div>

    <div class="col-md-12 col-xs-12">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
    </div>

    <div class="clearfix"></div>


    <div class="text-end">
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>
