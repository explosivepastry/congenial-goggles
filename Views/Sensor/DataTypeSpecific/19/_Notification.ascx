<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td>Notify when weight value is</td>
    <td>
        <%: Html.DropDownList("CompareType", "short", Model != null ? Model.CompareType : Monnit.eCompareType.Equal)%> 
        (to) 
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:Model.CompareValue %>"> 
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>
