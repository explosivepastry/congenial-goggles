<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<% 
    double valcompare = 0.0;



    if (!string.IsNullOrWhiteSpace(Model.CompareValue))
    {
        switch (Model.Scale)
        {

            case "Meter":
           
                    valcompare = (Model.CompareValue.ToDouble() * 0.01);
               
                break;
            case "Inch":
               
                    valcompare = (Model.CompareValue.ToDouble() * 0.393701);
        
                break;
            case "Feet":
               
                    valcompare = (Model.CompareValue.ToDouble() * 0.0328084);
     
                break;
            case "Yard":
                
                    valcompare = (Model.CompareValue.ToDouble() * 0.0109361);
             
                break;
            case "Centimeter":
            default:
               
                    valcompare = (Model.CompareValue.ToDouble());
      
                break;
        }
    }
%>

<tr>
    <td>Notify when sensor distance reading is</td>
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