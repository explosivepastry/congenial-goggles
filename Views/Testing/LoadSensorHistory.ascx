<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.DataMessage>>" %>

<link rel="stylesheet" type="text/css" href="/Content/Testing/css/testingHistory.css" />
<script type="text/javascript" src="/Content/Testing/js/testingHistory.js"></script>

<%
    Sensor sensor = (Sensor)ViewBag.Sensor;  //ViewBag:  transfers data from controller to view (get values dynamically: retrieving data (Sensor) from ViewBag)
%>

<div class="col-12 device_detailsRow__card">
    <div class="x_panel  scrollParentLarge" style="height: 800px;">
        <div class="x_title">
            <div class="card_container__top">
                <div class="card_container__top__title d-flex justify-content-between">
                    <div>
                        <span>
                            <%=Html.GetThemedSVG("sensor") %>
                        </span>
                        <span class="ms-1">
                            <%: Html.TranslateTag("Sensor History","Sensor History")%>
                        </span>
                    </div>
                    <div>
                        <span style="font-weight: 800">
                            <%: Html.TranslateTag("# Records Loaded","# Records Loaded")%>:
                        </span>
                        <span id="testingHistoryRecordCount" class="ms-1">
                            <%:Model.Count() %>   
                        </span>
                    </div>
                    <div>
                        <span style="font-weight: 800">
                            <%: Html.TranslateTag("SensorID","SensorID")%>:
                        </span>
                        <span style="color: #0094ff" class="ms-1">
                            <%:sensor.SensorID %>
                        </span>
                    </div>
                    <div>
                        <div id="textMinus" class="btn btn-secondary fa fa-minus"></div>
                        <div id="textPlus" class="btn btn-secondary fa fa-plus"></div>
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>

        <div>
            <% 
                if (sensor.ApplicationID == 12 || sensor.ApplicationID == 76 || sensor.ApplicationID == 158)
                {
                       Account acc = Account.Load(sensor.AccountID);
                        if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit"))
                        {

                            string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Control", sensor.ApplicationID.ToString("D3"));
                            if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                            {
                                ViewBag.returnConfirmationURL = ViewToFind;
                                Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", sensor);
                            }
                            else
                            {
                                Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Control.ascx", Model);
                            }
                        }
                        else //If they don't have permissions to view advanced partials try to load simple edit
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Control.ascx", Model);
                        }
                }
    %>
            <br />

            <script>
                $('#container1 form').submit(function (e) {
                    e.preventDefault();

                    var filteredData = [];

                    let index = 0;

                    $('#container1 form :input').each(function () {
                        if (index === 0) {
                            index++;
                            return; 
                        }

                        let value = $(this).val();
                        if (value !== "0") {
                            filteredData.push($(this).attr('name') + '=' + encodeURIComponent(value));
                        }

                        index++;
                    });

                    var requestData = filteredData.join('&');

                    debugger

                    let date = "<%= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>";
                    let relay = $('#container1 form :input')[1].value
                    let state = $('#container1 form :input')[2].value
                    let minutes = $('#container1 form :input')[3].value
                    let seconds = $('#container1 form :input')[4].value
                    let totalSeconds = (parseInt(minutes) * 60) + parseInt(seconds);
                    //let status = $('#container1 form :input')[].value

                    $.post('/Overview/Control/<%:sensor.SensorID%>', requestData, function (data) {
                        let newRow = `
                         <tr>
                            <td>${date}</td>
                            <td>${relay}</td>
                            <td>${state}</td>
                            <td>${totalSeconds}</td>
                            <td>${""}</td>
                        </tr>`;

                        $('#controlTable tbody').append(newRow);
                    });
                });
            </script>

        </div>


        <div class="row testingHistoryHeader">
            <div class="col-3"><%: Html.TranslateTag("Date","Date")%></div>
            <div class="col-3"><%: Html.TranslateTag("Signal","Signal")%></div>
            <div class="col-3"><%: Html.TranslateTag("Battery","Battery")%></div>
            <div class="col-3"><%: Html.TranslateTag("Data Reading","Data Reading")%></div>
        </div>

        <div class="x_content hasScroll" id="sensorHistory" style="margin-top: 0px; height: 685px;">

            <div id="testingSensorHistoryTable" data-id="<%= sensor.SensorID %>">
                <!-- Sensor History List  -->
                <%--<tbody>--%>
                <div class="noDataMessagesRow col-12" style="<%= Model.Count > 0 ? "display:none;": "" %>"><%: Html.TranslateTag("No Data","No Data") %></div>

                <%
                    foreach (DataMessage item in Model)
                    {
                %>
                <%= iMonnit.Controllers.TestingController.TestingSensorHistoryRow(sensor, item) %>
                <%
                    }
                %>


                <%--</tbody>--%>
            </div>

        </div>
        <div class="text-center" id="loading" style="display: none;">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden"><%: Html.TranslateTag("Loading","Loading")%>...</span>
            </div>
        </div>
    </div>
</div>

<script>
    <%= ExtensionMethods.LabelPartialIfDebug("LoadSensorHistory.ascx") %>

    $(document).ready(function () {
        //$('.testingHistoryRow').css('font-size', <%= MonnitSession.TestingToolSession.FontSize %>);
        console.log(<%= MonnitSession.TestingToolSession.FontSize %>);
        changeFontSize('.testingHistoryRow', "<%= MonnitSession.TestingToolSession.FontSize %>px");
    });

    //$('#textMinus,#textPlus').click(
    //    function (e) {
    //        e.stopPropagation();
    //        this.blur();

    //        var i = 0;
    //        if (this.id === 'textMinus')
    //            i = -1;
    //        if (this.id === 'textPlus')
    //            i = 1;

    //        var cur = parseInt($('.testingHistoryRow').css('font-size'));
    //        var nxt = cur + i;
    //        console.log(`${cur} => ${nxt}`);
    //        //$('.testingHistoryRow').css('font-size', nxt);
    //        //changeFontSize('.testingHistoryRow', nxt + 'px');

    //        $(this).toggleClass('btn-secondary btn-primary');

    //        setTimeout(
    //            () => {
    //                $(this).toggleClass('btn-secondary btn-primary');
    //            }
    //            , 500
    //        );

    //        $.post('/Testing/SetTestingToolFontSizePx/'
    //            , { fontSize: nxt }
    //            , (data) => {
    //                console.log(data);
    //                changeFontSize('.testingHistoryRow', data);
    //            }
    //        );
    //    }
    //)

    //function textSizeBtnFlash (btn, e) {
    //    e.stopPropagation();
    //    $(this).toggleClass('btn-secondary btn-primary');
    //    this.blur();
    //    setTimeout(
    //        () => {
    //            $(this).toggleClass('btn-secondary btn-primary');
    //        }, 500);
    //}

    //$('#textMinus').click(
    //    function (e) {
    //        //e.stopPropagation();
    //        $('.testingHistoryRow').css('font-size', parseInt($('.testingHistoryRow').css('font-size')) - 1);
    //        textSizeBtnFlash(e);
    //    }
    //);

    //$('#textPlus').click(
    //    function (e) {
    //        //e.stopPropagation();
    //        $('.testingHistoryRow').css('font-size', parseInt($('.testingHistoryRow').css('font-size')) + 1);
    //        textSizeBtnFlash(e);
    //    }
    //);


</script>
