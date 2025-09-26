<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<%: Html.Hidden("CompareType", eCompareType.Equal, Monnit.eCompareType.Equal)%>
<tr>
    <td>Notify when sensor is</td>
    <td><%: Html.DropDownListBool("CompareValue", "Pressed", "Not Pressed", Model != null ? (Model.CompareValue == "True") : true)%>
       <%-- <select id="CompareValue" name="CompareValue">
            <option value="True" <%: Model.CompareValue == "True" %> >Pressed</option>
            <option value="False" >Not Pressed</option>
        </select>--%>
    </td>
    <td></td>
</tr>