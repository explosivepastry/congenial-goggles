<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <%
        
        string Hours = CurrentZeroToTwentyAmp.GetLabel(Model.SensorID);
        string volts = CurrentZeroToTwentyAmp.GetVoltValue(Model.SensorID).ToString();
    %>



        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Label (default: Ah)","Label (default: Ah)")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                  <select id="label" name="label">
            <option value="Ah" <%: Hours == "Ah"?"selected":"" %>>Amp Hours</option>
            <option value="Wh" <%: Hours == "Wh"?"selected":"" %>>Watt Hours</option>
            <option value="kWh" <%: Hours == "kWh"?"selected":"" %>>Kilowatt Hours</option>
        </select>
            </div>
        </div>
        <div class="clear"></div>
        <br />


        <div class="form-group" id="voltRow">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Volts","Volts")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <%: Html.TextBox("voltValue",volts) %>
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
                var lab = $('#label').val();

                $('#voltRow').hide();

                if (lab != "Ah")
                    $('#voltRow').show();

                $('#label').change(function () {
                    var lable = $('#label').val();

                    if (lable != "Ah")
                        $('#voltRow').show();
                    else
                        $('#voltRow').hide();
                });



                $('#save').click(function () {
                    postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });
        </script>
</form>
