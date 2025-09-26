<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
        string label = "";
        double offset = unchecked((sbyte)(CurrentZeroToOneFiftyAmp.GetHystThirdByte(Model))) / 100d;
        MonnitApplicationBase.ProfileLabelForScale(Model, out label);        
     
%>

<div class="row sensorEditForm" style="display:none;">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Offset","Offset")%>
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Offset_Hidden" id="Offset_Hidden" value="<%=offset %>" />
    </div>
</div>


<script type="text/javascript">
    $('#Offset_Hidden').addClass("editField editFieldMedium");


    $('#Offset_Hidden').change(function () {
        if (isANumber($("#Offset_Hidden").val())) {
            if ($('#Offset_Hidden').val() < -1.27)
                $('#Offset_Hidden').val(-1.27);

            if ($('#Offset_Hidden').val() > 1.27)
                $('#Offset_Hidden').val(1.27);
        }
        else {
            $('#Offset_Hidden').val(<%: offset%>);
        }
        });
</script>
