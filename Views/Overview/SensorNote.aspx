<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<DataMessage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorNote
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Sensor sensor = Sensor.Load(Model.SensorID);%>

        <%Html.RenderPartial("SensorLink", sensor); %>

        <div class="col-12">
            <div class="rule-card_container w-100">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Overview/SensorNote|Sensor Message","Sensor Message") %>:  <%=Model.MessageDate.OVToLocalDateTimeShort() %>
                        <div class="nav navbar-right panel_toolbox">
                        </div>
                    </div>
                </div>
                <div class="x_content">
                    <div class="col-12">
                        <div class="x_content">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <div class="glance-text">
                                            <%if (Model != null)
                                                { %>
                                            <div class="glance-reading" style="font-size: 1rem">
                                                <span style="font-size: 1rem; font-weight: bold;"><%: Html.TranslateTag("Overview/SensorNote|Reading","Reading") %> :</span> <%= Model.DisplayData%><br />
                                                <%} %>
                                            </div>
                                        </div>
                                    </td>
                                    <td width="90" class="extra" style="text-align: center;">
                                        <div>
                                            <%MvcHtmlString SignalIcon = new MvcHtmlString("");
                                                if (Model != null)
                                                {
                                                    if (sensor.IsPoESensor)
                                                    {%>
                                            <div class="icon icon-ethernet-b"></div>
                                            <%}
                                                else
                                                {
                                                    int Percent = DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, Model.SignalStrength);
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
                                            <%=SignalIcon %>
                                            <%}

                                                }%>
                                        </div>

                                        <td width="90" class="extra" style="text-align: center;">
                                            <%MvcHtmlString PowerIcon = new MvcHtmlString("");
                                                string battLevel = "";
                                                string battType = "";
                                                string battModifier = "";
                                                if (Model != null)
                                                {
                                                    if (Model.Battery <= 0)
                                                    {
                                                        battLevel = "-0";
                                                        battModifier = " batteryCritical batteryLow";
                                                        PowerIcon = Html.GetThemedSVG("bat-dead");
                                                    }
                                                    else if (Model.Battery <= 10)
                                                    {
                                                        battLevel = "-1";
                                                        battModifier = " batteryCritical batteryLow";
                                                        PowerIcon = Html.GetThemedSVG("bat-low");
                                                    }
                                                    else if (Model.Battery <= 25)
                                                    {
                                                        battLevel = "-2";
                                                        PowerIcon = Html.GetThemedSVG("bat-low");
                                                    }
                                                    else if (Model.Battery <= 50)
                                                    {
                                                        battLevel = "-3";
                                                        PowerIcon = Html.GetThemedSVG("bat-half");
                                                    }
                                                    else if (Model.Battery <= 75)
                                                    {
                                                        battLevel = "-4";
                                                        PowerIcon = Html.GetThemedSVG("bat-full-ish");
                                                    }
                                                    else
                                                    {
                                                        battLevel = "-5";
                                                        PowerIcon = Html.GetThemedSVG("bat-ful");
                                                    }

                                                    if (sensor.PowerSourceID == 3 || Model.Voltage == 0)
                                                    {
                                                        battType = "-line";
                                                        battLevel = "";
                                                        PowerIcon = Html.GetThemedSVG("plugsensor1");
                                                    }
                                                    else if (sensor.PowerSourceID == 4)
                                                    {
                                                        battType = "-volt";
                                                        battLevel = "";
                                                    }
                                                    else if (sensor.PowerSourceID == 1 || sensor.PowerSourceID == 14)
                                                        battType = "-cc";
                                                    else if (sensor.PowerSourceID == 2 || sensor.PowerSourceID == 8 || sensor.PowerSourceID == 10 || sensor.PowerSourceID == 13 || sensor.PowerSourceID == 15 || sensor.PowerSourceID == 17 || sensor.PowerSourceID == 19)
                                                        battType = "-aa";
                                                    else if (sensor.PowerSourceID == 6 || sensor.PowerSourceID == 7 || sensor.PowerSourceID == 9 || sensor.PowerSourceID == 16 || sensor.PowerSourceID == 18)
                                                        battType = "-ss";
                                                    else
                                                        battType = "-gateway";

                                                }%>

                                            <div class="battIcon ">
                                                <%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", Model.Voltage) : "" %>            <%=PowerIcon %>
                                            </div>
                                        </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-12">
                            <div class="col-12">
                                <div class="" style="margin-top: 15px;">
                                    <div class="x_title">
                                        <h2><%: Html.TranslateTag("Overview/SensorNote|Create Note","Create Note") %></h2>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <form id="noteForm">
                                            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                                            <div class="col-12">
                                                <div class="form-group">
                                                    <label class="col-xs-9" for="subject">
                                                        <%: Html.TranslateTag("Overview/SensorNote|Write a short note, then click add","Write a short note, then click add") %>
                                                    </label>
                                                </div>
                                                <div class="clearfix"></div>
                                                <div class="form-group" style="margin-top: 1em;">
                                                    <div class="col-12">
                                                        <textarea class="text-box-msg" id="note_<%:Model.DataMessageGUID %>" name="note" placeholder="<%: Html.TranslateTag("Type your notes here", "Type your notes here")%>" rows="5" class="w-100"></textarea>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="clearfix"></div>
                                            <div class="col-12 text-end">
                                                <button id="add_<%:Model.DataMessageGUID %>" onclick="postNote('<%:Model.DataMessageGUID %>'); return false;" class="btn btn-primary mt-3"><%: Html.TranslateTag("Add Note", "Add Note")%></button>
                                                <button class="btn btn-primary mt-3" id="adding_<%:Model.DataMessageGUID %>" style="display: none;" type="button" disabled>
                                                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                                    Adding...
                                                </button>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>


                            <div class="col-sm-6 col-12">
                                <div class="" style="margin-top: 15px;">
                                    <div class="x_title">
                                        <h2><%: Html.TranslateTag("Overview/SensorNote|Notes","Notes") %></h2>
                                        <div class="nav navbar-right panel_toolbox">
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content" id="noteList">
                                        <% List<DataMessageNote> notes = DataMessageNote.LoadByDataMessageGUID(Model.DataMessageGUID); %>
                                        <%=Html.Partial("DataMessageNoteList",notes)%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- help button modal -->
    <div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/SensorNote|Sensor Message Notes Settings","Overview/SensorNote|Sensor Message Notes Settings")%></h4>

                    <button type="button" class="btn-close" data-dismiss="modal" aria-label="Close">
                    </button>

                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <!-- End help button modal -->

    <script type="text/javascript">

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

        function loadMessageNoteList() {
            var notiID = '<%:Model.DataMessageGUID %>';
            $.post("/Overview/MessageNoteList", { id: notiID }, function (data) {
                $('#noteList').html(data);
            });
        }
    </script>


</asp:Content>
