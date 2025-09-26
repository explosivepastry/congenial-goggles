<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<!--DatumType 02 Temperature-->

<div class="rule-card" >
    <div class=" rule-title">
        <%: Html.TranslateTag("When the temperature reading is","When the temperature reading is")%>
    </div>
    <div>
        <% bool isF = false;
            double CompareValue = ((String.IsNullOrEmpty(Model.CompareValue)) || Model.CompareValue == double.MinValue.ToString()) ? 0.0d : Model.CompareValue.ToDouble();
            if (Model.Scale == "F")
            {
                CompareValue = Monnit.Application_Classes.DataTypeClasses.TemperatureData.CelsiusToFahrenheit(CompareValue);
                isF = true;
            }
            CompareValue = Math.Round(CompareValue, 2);

            if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
                Model.CompareType = eCompareType.Less_Than;
        %>
        <select class="form-select user-dets grt-less"  id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
    </div>
    <div class=" degree-input ">
        <input class="form-control user-dets" style="width: 60px;" id="CompareValue" name="CompareValue" type="text" value="<%:CompareValue %>">
        <div>
            <a href="#" class="toggle-me <%= isF ? "toggle--on" : "toggle--off" %> "></a>
        </div>
    </div>
    <input type="hidden" id="scale" name="scale" value="<%=Model.Scale %>" />
</div>

<style>
    @charset "UTF-8";

    *,
    *:before,
    *:after {
        box-sizing: border-box;
    }

    h1 {
        font-size: 64px;
        margin-top: 0;
        margin-bottom: 50px;
    }
</style>


<%: Html.ValidationMessageFor(model => model.CompareValue)%>
<script type="text/javascript">
    function datumConfigs() {
        var settings = "compareType=" + $('#CompareType').val();
        settings += "&compareValue=" + $('#CompareValue').val();
        settings += "&scale=" + $('#scale').val();
        return settings;
    }



    $(document).ready(function () {

        if ($('.toggle-me').hasClass("toggle--on")) {
            $('#scale').val("F");
        } else {
            $('#scale').val("C");
        }

        $('.toggle-me').click(function (e) {
            var toggle = this;

            e.preventDefault();

            $(toggle).toggleClass('toggle--on')
                .toggleClass('toggle--off')
                .addClass('toggle--moving');

            if ($(toggle).hasClass("toggle--on")) {
                $('#scale').val("F");
            } else {
                $('#scale').val("C");
            }

            setTimeout(function () {
                $(toggle).removeClass('toggle--moving');
            }, 200)
        });

    });
</script>
