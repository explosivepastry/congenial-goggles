<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td>Notify when Observation Mode </td>
    <td style="white-space: nowrap">
        
        <input type="hidden" id="CompareType" name="CompareType" value="1" />
     
        <select id="CompareValue" name="CompareValue">
            <option value="start" <%=Model.CompareValue == "start" ? "selected" : "" %>>Starts</option>
            <option value="end" <%=Model.CompareValue == "end" ? "selected" : "" %>>Ends</option>
        </select>
    
      
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>



