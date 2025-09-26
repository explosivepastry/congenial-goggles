<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<tr>
    <td>Notify when battery is below</td>
    <td><input class="aSettings__input_input" id="CompareValue" name="CompareValue" type="number" value="<%:Model.CompareValue %>"> %</td>
    <td></td>
</tr>
<tr>
    <td></td>
    <td><%: Html.ValidationMessageFor(model => model.CompareValue)%></td>
    <td></td>
</tr>
