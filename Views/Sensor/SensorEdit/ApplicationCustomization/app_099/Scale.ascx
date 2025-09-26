<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />



    <div class="form-group">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|0 Ohms Value:","0 Ohms Value:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox 1111">
            <input class="aSettings__input_input form-control user-dets" type="text" id="lowValue" name="lowValue" value="<%:Monnit.ResistanceDelta.GetLowValue(Model.SensorID) %>" />
        </div>
    </div>
    <div class="clear"></div>
    
    <div class="form-group">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|145000 Ohms Value:","145000 Ohms Value:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input form-control user-dets" type="text" id="highValue" name="highValue" value="<%:Monnit.ResistanceDelta.GetHighValue(Model.SensorID) %>" />
        </div>
    </div>
    <div class="clear"></div>

    <div class="form-group">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_070|Label:","Label:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input form-control user-dets" type="text" id="label" name="label" value="<%:Monnit.ResistanceDelta.GetLabel(Model.SensorID) %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />


    <div class="" style="text-align: right;">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>


    <script>
        $(document).ready(function () {
            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });
    </script>
</form>
