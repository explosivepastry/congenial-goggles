<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td style="white-space: nowrap;"><%: Html.TranslateTag("Notify when temperature reading is","Notify when temperature reading is")%></td>
    <td style="white-space: nowrap;">
        <%  double CompareValue = (String.IsNullOrEmpty(Model.CompareValue)) ? 0.0d : Model.CompareValue.ToDouble();
        if (Model.Scale == "F")
            CompareValue = Monnit.Application_Classes.DataTypeClasses.TemperatureData.CelsiusToFahrenheit(CompareValue);
        CompareValue = Math.Round(CompareValue, 2);

        if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
            Model.CompareType = eCompareType.Less_Than;
        %>
        <select class="tzSelect" id="CompareType" name="CompareType" style="width: auto;">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:CompareValue %>">
         degrees
        <select class="tzSelect" name="scale" id="scale" style="width: auto;">
            <%Dictionary<string, string> TempScales = Temperature.NotificationScaleValues();
                foreach (string key in TempScales.Keys){ %>
            <option value="<%:key %>" <%:Model.Scale == key ? "selected='selected'" : "" %>><%:TempScales[key]%></option>
            <%} %>
        </select>
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>
