<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

    <input type="hidden" class="aSettings__input_input" <%=Model.CanUpdate ? "" : "disabled" %> id="RearmTime" name="RearmTime" value="<%=Model.RearmTime %>" />
    <%: Html.ValidationMessageFor(model => model.RearmTime)%>


<script type="text/javascript">

    function AddReArm() {
        if ($("#RearmTime").val() < 1) {
            $("#RearmTime").val(1);
        } else if ($("#RearmTime").val() > 595) {
            $("#RearmTime").val(600)
        } else { $("#RearmTime").val(Number($("#RearmTime").val()) + 5); }
    }

    function SubReArm() {

        if ($("#RearmTime").val() < 5) {
            $("#RearmTime").val(1);
        } else if ($("#RearmTime").val() > 600) {
            $("#RearmTime").val(600)
        } else { $("#RearmTime").val(Number($("#RearmTime").val()) - 5); }
    }

</script>
