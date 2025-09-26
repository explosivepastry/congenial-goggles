<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<input hidden class="aSettings__input_input" type="hidden" <%=Model.CanUpdate ? "" : "disabled"  %> name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ActiveStateInterval %>" />
<%: Html.ValidationMessageFor(model => model.ActiveStateInterval)%>    


<script type="text/javascript">


    function CheckAwareVSReport() {
        if (isANumber($('#ReportInterval').val())) {
            $('#ActiveStateInterval').val(Number($('#ReportInterval').val()));
        }
    }

</script>
