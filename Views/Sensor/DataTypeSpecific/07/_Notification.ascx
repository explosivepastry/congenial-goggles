<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td>Notify when sensors reading is</td>
    <td>
        <%  double CompareValue = Model.CompareValue.ToDouble(); %>
        <select class="tzSelect" id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        (to) 
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:Model.CompareValue %>">
        degrees
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td colspan="2"><div id="CompareValue_Slider" style="margin-top:8px;"></div></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td>
        <script type="text/javascript">
            
            $('#CompareValue_Slider').slider({
                value: <%:CompareValue%>,
                min: -180,
                max: 180,
                                    <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                slide: function (event, ui) {
                    //update the amount by fetching the value in the value_array at index ui.value
                    $('#CompareValue').val(ui.value);
                }
            });
            $("#CompareValue").addClass('editField editFieldMedium');
            $("#CompareValue").change(function () {
                if ($("#CompareValue").val() < -180)
                    $("#CompareValue").val(-180);
                if ($("#CompareValue").val() > 180)
                    $("#CompareValue").val(180)
                $('#CompareValue_Slider').slider("value", $("#CompareValue").val());
            });
        </script>
    </td>
</tr>
