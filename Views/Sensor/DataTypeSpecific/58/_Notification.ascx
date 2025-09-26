<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<% // AirSpeedData  = 58
    double valcompare = 0.0;

    if (!string.IsNullOrWhiteSpace(Model.CompareValue))
    {
        switch (Model.Scale)
        {
            case "mph":
                valcompare = (Model.CompareValue.ToDouble() * 2.23694);
                break;
            case "kmph":
                valcompare = (Model.CompareValue.ToDouble() * 3.6);
                break;
            case "knot":
                valcompare = (Model.CompareValue.ToDouble() * 1.94384);
                break;
            case "mps":
            default:
                valcompare = (Model.CompareValue.ToDouble());
                break;
        }
    }

%>

<tr>
    <td>Notify when sensor Air Speed reading is</td>
    <td>
        <%  double CompareValue = Model.CompareValue.ToDouble();
            if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
                Model.CompareType = eCompareType.Less_Than;
        %>
        <select class=" tzSelect" id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:valcompare %>">
        <select class=" tzSelect" name="scale" id="scale" style="width: auto;">
            <%Dictionary<string, string> Scales = AirSpeed.NotificationScaleValues();
                foreach (string key in Scales.Keys)
                { %>
            <option value="<%:key %>" <%:Model.Scale == key ? "selected='selected'" : "" %>><%:Scales[key]%></option>
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
