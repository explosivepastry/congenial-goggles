<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<%

    double valcompare = 0.0;



    if (!string.IsNullOrWhiteSpace(Model.CompareValue))
    {
        switch (Model.Scale)
        {
            case "torr":
                valcompare = (Model.CompareValue.ToDouble() * 0.0075006168).ToDouble();
                break;
            case "psi":
                valcompare = (Model.CompareValue.ToDouble() * 0.0001450377).ToDouble();
                break;
            case "inAq":
                valcompare = (Model.CompareValue.ToDouble() * 0.0040185981).ToDouble();
                break;
            case "inHg":
                valcompare = (Model.CompareValue.ToDouble() * 0.000296134).ToDouble();
                break;
            case "mmHg":
                valcompare = (Model.CompareValue.ToDouble() * 0.0075006376).ToDouble();
                break;
            case "mmwc":
                valcompare = (Model.CompareValue.ToDouble() * 0.1019744289).ToDouble();
                break;

            case "Pascal":
            default:
                valcompare = Model.CompareValue.ToDouble();
                break;

        }
    }

%>

<tr>
    <td>Notify when sensor differential pressure reading is</td>
    <td>
        <%  double CompareValue = Model.CompareValue.ToDouble();
        if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
            Model.CompareType = eCompareType.Less_Than;
        %>
        <select class=" tzSelect" id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:valcompare.ToString("0.##") %>">
        <select class=" tzSelect" name="scale" id="scale" style="width: auto;">
            <%Dictionary<string, string> Scales = DifferentialPressure.NotificationScaleValues();
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
