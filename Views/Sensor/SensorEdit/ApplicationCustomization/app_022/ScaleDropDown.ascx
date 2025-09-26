<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%  List<Sensor> SameTypeSensors = Sensor.LoadByApplicationID(Model.ApplicationID, 1000);
    Dictionary<string, SensorAttribute.SensorScaleBadge> DropdownVals = Monnit.ZeroToTwentyMilliamp.GetApplicationScaleOptions(Model.AccountID);
%>

<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <%if (DropdownVals.Count > 0)
        { %>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Scale", "Scale")%>
        </div>

        <div class="col sensorEditFormInput">
            <select id="badgeSelector" onchange="SetScaleValues()" class="form-select">
                <%foreach (var b in DropdownVals)
                    {%>
                <option style="display: flex; justify-content: space-between;" data-low="<%=b.Value.Attribute1%>" data-high="<%=b.Value.Attribute2%>" data-lbl="<%=b.Value.Label%>">
                    <%=b.Value.BadgeText%>
                </option>
                <%}%>
                <option style="display: flex; justify-content: space-between;">New Scale</option>
            </select>
        </div>
    </div>
    <br />
    <%} %>

    <div class="col-12">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
    </div>

    <div class="clearfix"></div>
    <div class="ln_solid"></div>

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        function SetScaleValues() {
            var optionval = $("#badgeSelector option:selected")

            $('#lowValue').val($(optionval).data('low'));
            $('#highValue').val($(optionval).data('high'));
            $('#label').val($(optionval).data('lbl'));
        }

        $(document).ready(function () {

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });

        });

    </script>
</form>

