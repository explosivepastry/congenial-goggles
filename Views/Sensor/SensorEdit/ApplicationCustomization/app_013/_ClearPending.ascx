<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_013|Clear Pending","Clear Pending")%>
    </div>
    <div class="col sensorEditFormInput" id="hyst3">
        <input type="button" data-url="/Sensor/ClearPendingNotifierHistory?sensorID=<%: Model.SensorID%>&newLook=true" class="btn btn-secondary btn-sm" id="clearPendingCommandHist" value="Clear Pending" />
        <span id="successfulClear"></span> 
    </div>
</div>

<script type="text/javascript">

    $('#clearPendingCommandHist').click(function (e) {
        e.preventDefault();
        var Url = $(this).data('url');
        $.ajax({
            url: Url,
            context: document.body
        }).done(function (result) {
            result == 'Success' ? $('#successfulClear').text("Clear Successful") : '';
        });
    });

</script>

<style>
    #successfulClear {
        color: forestgreen;
    }
</style>