<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td>Notify when Moisture Pressure is </td>
    <td>
        <%: Html.DropDownList("CompareType", Model != null ? Model.CompareType : Monnit.eCompareType.Greater_Than_or_Equal)%>
        (to) 
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:Model.CompareValue %>"> <%: Html.Label("centibars")%>
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>