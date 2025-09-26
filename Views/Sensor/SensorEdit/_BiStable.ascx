<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<tr>
    <td>Trigger Direction</td>
    <td>
        <%: Html.CheckBox("BiStableChk", Model.BiStable > 0, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.BiStable)%>
        <div style="display: none;"><%: Html.TextBoxFor(model => model.BiStable, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Determines if the sensor triggers on both state changes." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<script type="text/javascript">

    setTimeout('$("#BiStableChk").iButton({ labelOn: "Both" , labelOff: "Single" });', 500);
    $('#BiStableChk').change(function () {
        if ($('#BiStableChk').prop('checked')) {
            $('#BiStable').val(1);
        } else {
            $('#BiStable').val(0);
        }
    });

</script>
