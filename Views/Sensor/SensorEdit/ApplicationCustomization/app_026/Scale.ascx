<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <div class="formtitle">
        Liquid Level Scale 
    </div>
    <div class="formBody">

            <%  bool isInches = true;
                if (ViewData["tempscale"] != null)
                {
                    isInches = ViewData["tempscale"].ToStringSafe() == "inches";
                }
                else
                {
                    isInches = LiquidLevel24.IsInches(Model.SensorID);
                }
            %>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Scale","Scale")%>
            </div>
            <div class="col sensorEditFormInput">
                <div class="form-check form-switch d-flex align-items-center ps-0" >
                    <label class="form-check-label"><%: Html.TranslateTag("Centimeters","Centimeters")%></label>
                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.IsDirty ? "disabled" : "" %> name="tempscale" id="tempscale" <%: isInches ? "checked='checked'":"" %>>
                    <label class="form-check-label"><%: Html.TranslateTag("Inches","Inches")%></label>
                </div>
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

