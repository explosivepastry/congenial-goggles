<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SensorGroupSensorModel>>" %>




<%  foreach (SensorGroupSensorModel item in Model)
    {
%>

        <a class="card-rule "  href="/Rule/ChooseSensorTemplate/<%:item.Sensor.SensorID%>?isGateway=false" style="height:auto;">

            <div class=" card-rule__container">   
                <div class="hidden-xs ruleDevice__icon ">
            
                    <div class="icon-color iconMap" style="width: 30px;  margin-left: 5px; margin-right:10px;">
                      
                            <%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %>
         
            </div>
                </div>

                <div class="triggerDevice__name2">
                    <strong><%:System.Web.HttpUtility.HtmlDecode(item.Sensor.SensorName) %></strong>
                </div>

            </div>
        </a>

<%} %>


<script type="text/javascript">

    var refreshRequest = null;
    $(document).ready(function () {
        
        $('#totalSensors').html(<%: ViewBag.TotalSensors %>);
        $('#filteredSensors').html(<%: ViewBag.FilteredSensors %>);

    });

</script>
