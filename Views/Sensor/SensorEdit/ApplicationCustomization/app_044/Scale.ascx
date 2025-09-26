<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="formtitle">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|Sensor Terminal For","Sensor Terminal For")%> <%: Model.SensorName %>
    </div>

    <div class="form-group">
        <div class="bold col-md-3 col-sm-3 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_044|Interpret data as:","Interpret data as:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <% string label = SerialDataBridge.GetLabel(Model.SensorID); %>
            <select id="Label" name="Label">
                <option value="hex" <%:label.ToLower() == "hex" ? "selected=selected" : "" %>>Hex</option>
                <option value="ascii" <%:label.ToLower() == "ascii" ? "selected=selected" : "" %>>ASCII</option>
                <option value="u64bit" <%:label.ToLower() == "unsigned 64 bit" ? "selected=selected" : "" %>>Unsigned 64 Bit Decimal</option>
                <option value="u32bit" <%:label.ToLower() == "unsigned 32 bit" ? "selected=selected" : "" %>>Unsigned 32 Bit Decimal</option>
                <option value="u16bit" <%:label.ToLower() == "unsigned 16 bit" ? "selected=selected" : "" %>>Unsigned 16 Bit Decimal</option>
                <option value="u8bit" <%:label.ToLower() == "unsigned 8 bit" ? "selected=selected" : "" %>>Unsigned 8 Bit Decimal</option>
                <option value="s64bit" <%:label.ToLower() == "signed 64 bit" ? "selected=selected" : "" %>>Signed 64 Bit Decimal</option>
                <option value="s32bit" <%:label.ToLower() == "signed 32 bit" ? "selected=selected" : "" %>>Signed 32 Bit Decimal</option>
                <option value="s16bit" <%:label.ToLower() == "signed 16 bit" ? "selected=selected" : "" %>>Signed 16 Bit Decimal</option>
                <option value="s8bit" <%:label.ToLower() == "signed 8 bit" ? "selected=selected" : "" %>>Signed 8 Bit Decimal</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />



    <div class="col-md-12 col-xs-12">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
    </div>

    <div class="clearfix"></div>


    <div class="" style="text-align: right;">
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
