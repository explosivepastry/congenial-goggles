<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.Sensor>>" %>

<% 
    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    if (Model.Count < 1)
    {
        Response.Write("No sensors selected for edit.");
        return;
    }

    bool OneOrMoreWithPendingTransaction = false;
    foreach (Monnit.Sensor s in Model)
    {
        if (s.CanUpdate)
        {
            OneOrMoreWithPendingTransaction = true;
            break;
        }
    }
%>

<% //This is loading the drop down options, with only the networks that current have that sensor.
    List<CSNet> temp = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);

    List<CSNet> networks = new List<CSNet>();
    CSNet oldCsNet = null;
    foreach (var sensor in Model.OrderBy(netID => { return netID.CSNetID; }))
    {
        foreach (var currentCSNet in temp)
        {
            if (sensor.CSNetID == currentCSNet.CSNetID)
            {

                if (networks.Count == 0 && oldCsNet == null)
                {
                    networks.Add(currentCSNet);
                    oldCsNet = currentCSNet;
                }
                else if (oldCsNet.CSNetID != currentCSNet.CSNetID)
                {
                    networks.Add(currentCSNet);
                    oldCsNet = currentCSNet;
                }
            }
        }
    }     
%>

<div class="MultiEditHolder">
    <div>Update sensors to match the configuration of sensor <%: (ViewBag.SensorConfigs as Sensor).SensorName %>.</div>
    
    <input type="hidden" id="SensorID<%:(ViewBag.SensorConfigs as Sensor).SensorID %>">
    <input type="hidden" id="monnitAppID" value="<%:(ViewBag.SensorConfigs as Sensor).ApplicationID %>">
    <br />
    <br />

    <div>Select which sensors you want to update.</div>
    <div>
        <input type='button' value='Check All' onclick='selectAll();' />&nbsp;
        <input type='button' value='Uncheck All' onclick='deSelectAll();' />&nbsp;  
        <select id="classFilter" style="float: right; margin-right: 400px;">
            <option value="All">All Networks</option>
            <% foreach (var item in networks)//foreach (var item in (ViewData.networks as List<CSNet>))
               { %>
            <option value="<%= item.CSNetID %>"><%= item.Name %></option>
            <%} %>
        </select>
    </div>
    <br />

    <%if (OneOrMoreWithPendingTransaction)
      { %>
    <div>Sensors with fields waiting to be written are not available for edit until pending transaction is complete.</div>
    <br />
    <% } %>
    <div id="sensorListContainer">
       <% Html.RenderPartial("~/Views/Sensor/MassEditSensorList.ascx", Model);%>
        </div>   
    </div>
    <div style="clear: both;"></div>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#classFilter').change(function () {                
                if (isNaN(parseInt($(this).val())))               
                    location.reload();                
                else                
                  refreshSensorList(parseInt($(this).val()), $('#monnitAppID').val());                   
                
            });
        });

        function refreshSensorList(csNetID, monnitProfID)
        {            
            $.get('/Sensor/MassEditSensorList?csnetID=' + csNetID + '&monnitProfID=' + monnitProfID, function (data) {
                $('#sensorListContainer').html(data);
            });            
        }

        function checkFormMass() {

            var selected = $('.MultiEditHolder:visible');

            var ids = "";

            selected.find('.SelectSensorsDiv:visible input:checked:visible').each(function () { ids += '|' + this.id });

            if (ids.length > 0) {
                ids = ids.substring(1);
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("One or more sensors must be selected")%>");
                return;
            }

            postMassEdit(selected, { id: '<%:(ViewBag.SensorConfigs as Sensor).SensorID%> ', sensorIDs: ids });
        }

        function postMassEdit(div, values) {
            div.hide('fast', function () {
                div.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
                div.show('fast');

                $.post('/Sensor/MassEdit', values, function (data) {

                    if (confirm(data)) {
                        window.location.href = "/Overview/Index";
                    }
                });
            });
        }

        function selectAll() {
            $('.SelectSensorsDiv:visible input:not(checked):visible').each(function () { this.checked = true; });
        }

        function deSelectAll() {
            $('.SelectSensorsDiv:visible input:checked:visible').each(function () { this.checked = false; });
        }

        function setVisibility() {

            }

        function CancelMassEdit() {
            window.location.href = "/Overview/";
        }

        $(function () {

            $('.checkSensor').show();
            $('#SensorID<%:Model[0].ApplicationID %>').change(setVisibility);
            setVisibility();
        });
       
    </script>


    <div class="buttons">
        <a href="#" onclick="CancelMassEdit(); return false;" class="greybutton">Cancel</a>
        <input type="button" onclick="checkFormMass();" value="Apply Configuration" class="bluebutton" />
        <div style="clear: both;"></div>
    </div>
</div>

