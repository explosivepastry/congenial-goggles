<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td>Notify when data is</td>
    <td style="white-space: nowrap">
        <label>Equal</label>
        <input type="hidden" id="CompareType" name="CompareType" value="1" />
        to
        <input class="short" id="CompareValue" name="CompareValue" type="text" value="<%:Model.CompareValue %>">
        <span class="baseLabel">(Hex)</span>
    </td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>



