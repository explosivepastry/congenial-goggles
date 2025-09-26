<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<%: Html.Hidden("CompareType", eCompareType.Equal, Monnit.eCompareType.Equal)%>
<tr>
    <td>Notify when sensor is</td>
    <td><%: Html.DropDownListBool("CompareValue", "Magnet Detected", "No Magnet Detected", Model != null ? (Model.CompareValue == "True") : true)%></td>
    <td></td>
</tr>