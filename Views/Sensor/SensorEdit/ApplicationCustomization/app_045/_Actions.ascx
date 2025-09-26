<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
    </div>
    <div class="col sensorEditFormInput">
        <input type="button" class="btn btn-primary" style="min-width:250px" <%=Model.CanUpdate ? "" : "disabled" %> id="ReformRanger" value="<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_045|Reform","Reform")%>" />
        <input type="hidden" id="Reform" />
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
    </div>
    <div class="col sensorEditFormInput">
        <input type="button" class="btn btn-info"<%=Model.CanUpdate ? "" : "disabled" %> style="min-width:250px" id="List" value="<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_045|Request Sensor List","Request Sensor List")%>" />
        <input type="hidden" id="SensorList" />
    </div>
</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
    </div>
    <div class="col sensorEditFormInput">
        <input type="button" class="btn btn-dark" <%=Model.CanUpdate ? "" : "disabled" %> style="min-width:250px" id="Que" value="<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_045|Erase Message Queue","Erase Message Queue")%>" />
        <input type="hidden" id="EraseQue" />
    </div>
</div>


<script type="text/javascript">

    var sensorList =  '<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_045|Are you sure you want to request sensor list this SmartRanger?","Are you sure you want to request sensor list this SmartRanger?")%>';
    var erase =  '<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_045|Are you sure you want to erase the SmartRanger message que?","Are you sure you want to erase the SmartRanger message que?")%>';
    var reform =  '<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_045|Are you sure you want to reform this SmartRanger?","Are you sure you want to reform this SmartRanger?")%>';



    $(function ()
    {

        $('#List').click(function (e) {
            e.preventDefault();
            var repeaterID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
                        
            if(confirm(sensorList))
            {
                $("#SensorList").val(4);


                var form = $('#simpleEdit_<%:Model.SensorID %>').serialize();
                              
                $.post('/Sensor/Calibrate/', { id:repeaterID, SensorList: 0x04, returns:returnUrl} ,function(data) {
                    pID.html(data); 
                });
            }
        });

        $('#Que').click(function (e) {
            e.preventDefault();
            var repeaterID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
                        
            if(confirm(erase))
            {
                $("#EraseQue").val(3);


                var form = $('#simpleEdit_<%:Model.SensorID %>').serialize();
                              
                  $.post('/Sensor/Calibrate/', { id:repeaterID, EraseQue: 0x03, returns:returnUrl} ,function(data) {
                      pID.html(data); 
                  });
              }
        });

        $('#ReformRanger').click(function (e) {
            e.preventDefault();
            var repeaterID = <%: Model.SensorID%>;
            var returnUrl = $('#returns').val();
            var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
                        
            if(confirm(reform))
            {

                $("#Reform").val(6);

                            
                              
                $.post('/Sensor/Calibrate/', { id:repeaterID, Reform: 0x06, returns:returnUrl} ,function(data) {
                    pID.html(data); 
                });
            }
        });
    });
</script>

