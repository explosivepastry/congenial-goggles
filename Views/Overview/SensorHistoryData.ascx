<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    string DataMsg = string.Empty;
    List<DataMessage> dm = ViewBag.dm;
    Sensor sensor = (Sensor)ViewBag.Sensor;
    if (dm.Count > 0)
        DataMsg = dm.Last().DataMessageGUID.ToString();
%>

<style>
    .table-row-box {
        display: flex;
        width: 100%;
    }

    .historyReading1 {
        font-size: small;
        padding-top: 4px;
        width: 30%;
        flex-wrap: wrap;
        display: flex;
        word-break: break-word;
    }

    .historyDate1 {
        font-size: small;
        padding-top: 4px;
    }
</style>

<!-- History -->
<div id="insideData">

    <div class="table-row-box">
    </div>

    <%	foreach (var item in dm)
        {%>
    <div id="dataInfo" class="long-card d-flex  w-100 p-1 <%: item.DisplayData.Length > 45? "longstring": "shortstring"  %>" title="<%=item.GatewayID %>">

        <div class="" onclick="toggleMessageNote('<%:item.DataMessageGUID %>');">
            <span class="notetaken" style="display: <%:item.HasNote ? "inline;" : "none;"%>" id="hasNotes_<%:item.DataMessageGUID %>"><%=Html.GetThemedSVG("note") %></span>
            <span style="cursor: pointer; display: <%:item.HasNote ? "none;" : "inline;"%>" id="noNotes_<%:item.DataMessageGUID %>"><%=Html.GetThemedSVG("emptyNote") %></span>
        </div>

        <div class="historyReading1 text-wrap" data-guid="<%= DataMsg %>"><%:item.DisplayData %></div>

        <div class="historyDate1"><%:item.MessageDate.OVToLocalDateTimeShort() %></div>

        <div class="d-flex" style="gap: 10px;">


            <%if (new Version(sensor.FirmwareVersion) >= new Version("25.45.0.0") || sensor.SensorPrintActive)
                {
                    if (sensor.SensorPrintActive && item != null)
                    {
                        if (item.IsAuthenticated)
                        {%>
            <%=Html.GetThemedSVG("printCheck") %>
            <%}
                else
                {%>
            <%=Html.GetThemedSVG("printFail") %>
            <%}
                    }
                }%>


            <% MvcHtmlString SignalIcon = new MvcHtmlString("");
                if (item != null)
                {
                    if (sensor.IsPoESensor)
                    {
                        //show nothing						
                        //<div class="icon icon-ethernet-b"></div>						
                    }
                    else
                    {
                        int Percent = DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, item.SignalStrength);
                        string signal = "";

                        if (Percent <= 0)
                            SignalIcon = Html.GetThemedSVG("signal-none");
                        else if (Percent <= 10)
                            SignalIcon = Html.GetThemedSVG("signal-1");
                        else if (Percent <= 25)
                            SignalIcon = Html.GetThemedSVG("signal-2");
                        else if (Percent <= 50)
                            SignalIcon = Html.GetThemedSVG("signal-3");
                        else if (Percent <= 75)
                            SignalIcon = Html.GetThemedSVG("signal-4");
                        else
                            SignalIcon = Html.GetThemedSVG("signal-5");
            %>
            <div title="Signal Strength: <%=Percent %>%"><%= SignalIcon %></div>

            <%}
                }%>
            <%--            <%=SignalIcon %>--%>

            <%  MvcHtmlString PowerIcon = new MvcHtmlString("");
                string battLevel = "";
                string battType = "";
                string battModifier = "";
                if (item != null)
                {
                    if (item.Battery <= 0)
                    {
                        battLevel = "-0";
                        battModifier = " batteryCritical batteryLow";
                        PowerIcon = Html.GetThemedSVG("bat-dead");
                    }
                    else if (item.Battery <= 10)
                    {
                        battLevel = "-1";
                        battModifier = " batteryCritical batteryLow";
                        PowerIcon = Html.GetThemedSVG("bat-low");
                    }
                    else if (item.Battery <= 25)
                    {
                        battLevel = "-2";
                        PowerIcon = Html.GetThemedSVG("bat-low");
                    }
                    else if (item.Battery <= 50)
                    {
                        battLevel = "-3";
                        PowerIcon = Html.GetThemedSVG("bat-half");
                    }
                    else if (item.Battery <= 75)
                    {
                        battLevel = "-4";
                        PowerIcon = Html.GetThemedSVG("bat-full-ish");
                    }
                    else
                    {
                        battLevel = "-5";
                        PowerIcon = Html.GetThemedSVG("bat-ful");
                    }

                    if (ViewBag.Sensor.PowerSourceID == 3 || item.Voltage == 0)
                    {
                        battType = "-line";
                        battLevel = "";
                    }
                    else if (ViewBag.Sensor.PowerSourceID == 4)
                    {
                        battType = "-volt";
                        battLevel = "";
                    }
                    else if (ViewBag.Sensor.PowerSourceID == 1 || ViewBag.Sensor.PowerSourceID == 14)
                        battType = "-cc";
                    else if (ViewBag.Sensor.PowerSourceID == 2 || ViewBag.Sensor.PowerSourceID == 8 || ViewBag.Sensor.PowerSourceID == 10 || ViewBag.Sensor.PowerSourceID == 13 || ViewBag.Sensor.PowerSourceID == 15 || ViewBag.Sensor.PowerSourceID == 17 || ViewBag.Sensor.PowerSourceID == 19)
                        battType = "-aa";
                    else if (ViewBag.Sensor.PowerSourceID == 6 || ViewBag.Sensor.PowerSourceID == 7 || ViewBag.Sensor.PowerSourceID == 9 || ViewBag.Sensor.PowerSourceID == 16 || ViewBag.Sensor.PowerSourceID == 18)
                        battType = "-ss";
                    else
                        battType = "-gateway";

                }%>

            <div title="Battery: <%=item.Battery %>%, Voltage: <%=item.Voltage %> V">
                <%=PowerIcon %>
            </div>
        </div>
    </div>

    <div id="noteContainer_<%:item.DataMessageGUID %>" style="display: none;">
        <div id="noteForm_<%:item.DataMessageGUID %>">

            <div class="note-body p-3">
                <div class="col-12 col-md-6">
                    <ul>
                        <div class="x_content" id="noteList_<%:item.DataMessageGUID %>">
                        </div>
                    </ul>
                </div>
                <div class="col-12 col-md-6">
                    <textarea class="text-box-msg" id="note_<%:item.DataMessageGUID %>" name="note" placeholder="<%: Html.TranslateTag("Type your notes here", "Type your notes here")%>" rows="5" class="w-100"></textarea>
                </div>

                <div class="col-12 text-end">
                    <button id="add_<%:item.DataMessageGUID %>" onclick="postNote('<%:item.DataMessageGUID %>'); return false;" class="btn btn-primary mt-3"><%: Html.TranslateTag("Add Note", "Add Note")%></button>
                    <button class="btn btn-primary mt-3" id="adding_<%:item.DataMessageGUID %>" style="display: none;" type="button" disabled>
                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                        Adding...
                    </button>
                </div>
            </div>
        </div>
    </div>

    <%} %>
</div>

<style>
    #insideData .accordion .accordion-item .accordion-header {
        margin: 0px;
    }

        #insideData .accordion .accordion-item .accordion-header button {
            padding: 1px !important;
        }
</style>

<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("SensorHistoryData.ascx") %>

    function postNote(id) {
        $('#add_' + id).hide();
        $('#adding_' + id).show();

        $.post('/Overview/AddMessageNote/' + id, { 'note': $('#note_' + id).val() }, function (data) {
            if (data != "Success") {
                console.log(data);
            }

            loadMessageNoteList(id);
            $('#add_' + id).show();
            $('#adding_' + id).hide();
        });
    }

    function loadMessageNoteList(id) {
        var notiID = id;
        $.post("/Overview/MessageNoteList", { id: notiID }, function (data) {
            $(`#noteList_${id}`).html(data);
            $(`#note_${id}`).val('');
            if (!data.includes('svg_delete')) {
                $(`#hasNotes_${id}`).hide();
                $(`#noNotes_${id}`).show();
            }
            else {
                $(`#hasNotes_${id}`).show();
                $(`#noNotes_${id}`).hide();
            }
        });
    }

    function toggleMessageNote(id) {
        var div = $('#noteContainer_' + id);
        if (div.is(":visible")) {
            div.hide();
        }
        else {
            div.show();
            if (!$.trim($('#noteList_' + id).html())) {//check if empty
                loadMessageNoteList(id);
            }
        }
    }
</script>

<style>
    .accordion-button::after {
        display: none;
    }

    @media only screen and (max-width: 700px) {
        .accordion-button {
        }
    }

    .accordion-body::-webkit-scrollbar, .accordion-item::-webkit-scrollbar-track, .accordion-item::-webkit-scrollbar-thumb {
        display: none !important;
        width: 0px !important;
    }

    #svg_emptyNotes {
        height: 20px !important;
        width: 20px !important;
    }

    #svg_note {
        height: 15px !important;
        width: 20px !important;
    }
</style>
