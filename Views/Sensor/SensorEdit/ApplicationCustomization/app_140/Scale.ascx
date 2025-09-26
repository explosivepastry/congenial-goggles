<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <div class="formtitle">
        <%: Html.TranslateTag("Overview/SensorScale|Temperature Scale","Temperature Scale")%>   
    </div>
    <div class="formBody">

        <%  bool isFahrenheit = true;
            if (ViewData["TempScale"] != null)
            {
                isFahrenheit = ViewData["TempScale"].ToStringSafe() == "F";
            }
            else
            {
                isFahrenheit = Temperature.IsFahrenheit(Model.SensorID);
            }
        %>
     
        <div class="row">

            <div class="col-sm-9 col-12">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Show Temperature Values in","Show Temperature Values in")%>
                <select  id="TempScale" name="tempscale" class="form-select">
                    <option value="<%="on"%>" <%=isFahrenheit ? "selected='selected'" : "" %> ><%: Html.TranslateTag("Fahrenheit","Fahrenheit")%></option>
                    <option value="<%="off"%>"  <%=isFahrenheit ? "" : "selected='selected'" %> ><%: Html.TranslateTag("Celsius","Celsius")%></option>
                </select>
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
    <div class="ln_solid"></div>

    <div class="dfac" >
        <input class="gen-btn" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        $(document).ready(function () {

            //$('#TempScale').mobiscroll().select({
            //    theme: 'ios',
            //    display: popLocation,
            //    onSet: function (event, inst) {
            //        $('#save').click();
                   
            //    }
                
            //});

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });


        });

    </script>
</form>
