<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    string SelectedValue = "";
    eCOGasDisplay Display = eCOGasDisplay.Concentration;
    if (ViewData["Display"] != null)
    {
        Display = (eCOGasDisplay)ViewData["Display"];
    }
    else
    {
        Display = Gas_CO.GetDisplay(Model.SensorID);
    }


    SelectList select = new SelectList(new string[3] { "Concentration", "Time Weighted Average", "All" }, SelectedValue);
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Display Mode","Display Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="Display" name="Display" class="form-select"<%=Model.CanUpdate ? "" : "disabled" %>>
            <option value="Concentration" selected="selected"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Concentration")%></option>
            <option value="Time_Weighted_Average"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|Time Weighted Average")%></option>
            <option value="All"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_034|All")%></option>
        </select>
        <script>
            $('#Display').addClass('form-select');
            $(function () {
                $('#Display').change(function () {
                    $('.Thres34').hide();
                    $('.Thres34_' + $('#Display').val()).show();
                });

                $('.Thres34').hide();
                $('.Thres34_' + $('#Display').val()).show();
            });
        </script>
    </div>
</div>

<script type="text/javascript">

    //MobiScroll
    $(function () {
                <% if (Model.CanUpdate)
    { %>

        //$('#Display').mobiscroll().select({
        //    theme: 'ios',
        //    display: popLocation,
        //    minWidth: 200
        //});

    <%}%>

    });
</script>

<style>
    #Display {
        margin-left: 0;
    }
</style>
