<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<%: Html.Hidden("CompareType", eCompareType.Equal, Monnit.eCompareType.Equal)%>
<tr>
    <td>Notify when sensor reports</td>
    <td><%: Html.DropDownListBool("CompareValue", "Loop Closed", "Loop Open", Model != null ? (Model.CompareValue == "True") : true)%></td>
    <td></td>
</tr>