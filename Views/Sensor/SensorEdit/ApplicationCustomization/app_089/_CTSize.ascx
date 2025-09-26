<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_089|CT-Size","CT-Size")%>
	</div>
	<div class="col sensorEditFormInput">
		  <select id="ctSize" class="form-select ms-0" name="ctSize" <%: Model.CanUpdate ?"":"disabled" %>>
            <option value="2" <%: Current.GetCalVal3Upper(Model) == 2 ? "selected" : "" %>>Low</option>
            <option value="1" <%: Current.GetCalVal3Upper(Model) == 1 ? "selected" : "" %>>Medium</option>
            <option value="0" <%: Current.GetCalVal3Upper(Model) == 0 ? "selected" : "" %>>High</option>
        </select>
	</div>
</div>


<script type="text/javascript">

    $('#ctSize').addClass("editField editFieldMedium");

    //MobiScroll - CT-Size
    //$(function () {
    //    $('#ctSize').mobiscroll().select({
    //        theme: 'ios',
    //        display: popLocation,
    //        minWidth: 200
    //    });  
    //});

</script>