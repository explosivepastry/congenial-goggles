<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <%: Html.Hidden("CompareType", eCompareType.Equal, Monnit.eCompareType.Equal)%>
    <td>Notify when vehicle is </td>
    <td> 
        <select id="CompareValue" name="CompareValue">
            <option value="1" <%:Convert.ToInt32(Model.CompareValue == "" ? "1" : Model.CompareValue) == 1 ? "selected" : " " %> >Detected</option>
            <option value="0" <%:Convert.ToInt32(Model.CompareValue == "" ? "1" : Model.CompareValue) == 0 ? "selected" : " " %> >Undetected</option>
         </select>
    </td>
    <td></td>
</tr>
