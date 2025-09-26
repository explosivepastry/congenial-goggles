<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<% 
    string valcompare = "0";

    switch (Model.Scale)
    {
        case "cm":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToCentimeter(Model.CompareValue.ToDouble()).ToString();
            break;
        case "Meters":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToMeter(Model.CompareValue.ToDouble()).ToString();
            break;
        case "mm":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToMillimeters(Model.CompareValue.ToDouble()).ToString();
            break;
        case "Feet":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToFeet(Model.CompareValue.ToDouble()).ToString();
            break;
        case "Yards":
            valcompare = Monnit.Application_Classes.DataTypeClasses.Distance.InchesToYards(Model.CompareValue.ToDouble()).ToString();
            break;
        default:
        case "Inches":
            valcompare = (Model.CompareValue.ToDouble()).ToString();
            break;

    }

    if (valcompare.ToDouble() < 0) valcompare = "0";
%>

<tr>
    <td>Notify when sensor distance reading is</td>
    <td>
        <%  double CompareValue = Model.CompareValue.ToDouble();
        if (Model != null && Model.CompareType == eCompareType.Less_Than_or_Equal)
            Model.CompareType = eCompareType.Less_Than;
        %>
        <select class="tzSelect" id="CompareType" name="CompareType">
            <option value="Greater_Than">Greater Than</option>
            <option value="Less_Than" <%:(Model != null && Model.CompareType == eCompareType.Less_Than) ? "selected=selected" : "" %>><%: Html.TranslateTag("Less Than","Less Than")%></option>
        </select>
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:valcompare %>">
        <select class="tzSelect" name="scale" id="scale" style="width: auto;">
            <%Dictionary<string, string> Scales = UltrasonicRangerIndustrial.NotificationScaleValues();
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