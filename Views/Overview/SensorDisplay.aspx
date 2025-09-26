<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<iMonnit.Models.SensorGroupSensorModel>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SensorDisplay
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <style>
            input.onoffinput[type=checkbox] {
                height: 0;
                width: 0;
                visibility: hidden;
            }

            label.onoff {
                cursor: pointer;
                text-indent: -9999px;
                width: 200px;
                height: 100px;
                background: grey;
                display: block;
                border-radius: 100px;
                position: relative;
            }

                label.onoff:after {
                    content: '';
                    position: absolute;
                    top: 5px;
                    left: 5px;
                    width: 90px;
                    height: 90px;
                    background: #fff;
                    border-radius: 90px;
                    transition: 0.3s;
                }

            input.onoffinput:checked + label.onoff {
                background: #21CE99;
            }

                input.onoffinput:checked + label.onoff:after {
                    left: calc(100% - 5px);
                    transform: translateX(-100%);
                }

            label.onoff:active:after {
                width: 130px;
            }

            .slidecontainer {
                width: 100%;
            }

            .slider {
                -webkit-appearance: none;
                width: 100%;
                height: 20px;
                background: #d3d3d3;
                outline: none;
                opacity: 0.7;
                -webkit-transition: .2s;
                transition: opacity .2s;
                border-radius: 10px;
            }

                .slider:hover {
                    opacity: 1;
                }

                .slider::-webkit-slider-thumb {
                    -webkit-appearance: none;
                    appearance: none;
                    width: 25px;
                    height: 25px;
                    background: #37BC9B;
                    cursor: pointer;
                    border-radius: 50px;
                }

                .slider::-moz-range-thumb {
                    width: 25px;
                    height: 25px;
                    background: #4CAF50;
                    cursor: pointer;
                }

            .label_container {
                background: #d3d3d3;
                font-size: 12px;
                font-weight: 500;
            }

            input[type=radio]:checked + .label_container {
                background: #2699FB;
                color: white;
            }

            .fontWarning {
                color: white;
            }

            .fontOK {
                color: white;
            }

            .fontSleeping, .fontOffline {
                color: #000;
            }

            .backAlert {
                background: #EE3D18;
            }

                .backAlert .glance-name, .backAlert .glance-reading {
                    color: white;
                }

                .backAlert svg path {
                    fill: white;
                }
        </style>

        <div class="dfac">
            <%
                List<CSNet> CSNetList = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(null);
                if (CSNetList.Count > 1)
                {%>
            <select class="networkSelect form-select" style="width: 300px;">
                <option value="-1" data-networkname="<%:"All Networks"%>" data-csnetid="-1">All Networks</option>
                <%foreach (CSNet Network in CSNetList)
                    { %>
                <option value="<%:Network.CSNetID%>" data-networkname="<%:Network.Name%>" data-csnetid="<%:Network.CSNetID%>" <%:MonnitSession.SensorListFilters.CSNetID == Network.CSNetID ? "selected=selected" : "" %>><%:Network.Name.Length > 0 ? Network.Name : Network.CSNetID.ToString() %></option>
                <% } %>
            </select>
            <% } %>
            <div class="dropdown" style="margin: 10px;">
                <button class="btn btn-secondary dropdown-toggle" style="background: #fff; background: #2699FB; color: white; font-weight: 500; font-size: 13px; display: flex; align-items: center; justify-content: center; margin: 0px; padding: 0px; padding: 5px 10px; height: auto;"
                    type="button" id="dropdownMenuButton" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Preferences
                </button>
                <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <div>Side Bar:</div>
                    <span class="dfjcac input-group">
                        <label class="btn btn-secondary btn-sm" for="sidebarHide">
                            Hide
                        </label>
                        <input type="radio" id="sidebarHide" name="radio" class="hideSidebar" hidden>
                        <input type="radio" id="sideBarShow" checked="checked" name="radio" class="showSidebar" hidden>
                        <label class="btn btn-primary btn-sm" for="sideBarShow">
                            Show
                        </label>
                    </span>
                    <span>Display Size:<span id="demo"></span>%</span>
                    <input type="range" min="75" max="200" value="100" class="slider form-control" id="myRange">

                    <span>Sensor Profile:</span>
                    <div id="sensorProfiles">
                        <%Html.RenderPartial("_SensorProfiles", MonnitApplication.LoadByCSNetID(MonnitSession.SensorListFilters.CSNetID));%>
                    </div>
                </div>
            </div>
        </div>
        <div id="DisplayDataPartial">
            <%Html.RenderPartial("_DataDisplay", Model);%>
        </div>

    </div>
    <script type="text/javascript">
        var slider = document.getElementById("myRange");
        var output = document.getElementById("demo");
        output.innerHTML = slider.value;
        slider.oninput = function () {
            output.innerHTML = this.value;
        }

        $(document).ready(function () {
            setInterval(selectApplication, 30000);  // 30 seconds

            $("input#myRange").change(function () {
                $(".right_col").css("zoom", slider.value + "%");
            });

            $("input.hideSidebar").click(function () {
                $(".main_container").css("grid-template-columns", "8fr");
                $("#sideNav").css("display", "none");
            });
            $("input.showSidebar").click(function () {
                $(".main_container").css("grid-template-columns", "1fr 7fr");
                $("#sideNav").css("display", "block");
            });



            $('.networkSelect').change(function () {
                var id = $('.networkSelect option:selected').val();

                selectNetwork(id);
                setPreferenceAppList(id);
            });

            $('#statusFilter').change(function () {
                $.get('/Overview/FilterGatewayStatus', { status: $(this).val() }, function (data) {
                    getMain('/m/Overview/Gateways', 'Gateways', true);
                });
            });

        });
        var nameTimeout = null;
        function updateList() {
            $.get('/m/Overview/GatewaysRefresh', function (data) {
                $.each(eval(data), function (index, value) {
                    var tr = $('.viewGatewayDetails.' + value.GatewayID);
                    tr.find('.statusIcon').attr("src", value.StatusImageUrl);
                    tr.find('.overviewDate').html(value.Date);
                    tr.find('.sigIcon img').attr("src", value.SignalImageUrl);
                    tr.find('.battIcon img').attr("src", value.PowerImageUrl);
                });
            });
        }
        function AckAllButton(anchor) {
            var href = $(anchor).attr('href');
            if (confirm('Are you sure you want to acknowledge all active notifications?')) {
                $.post(href, function (data) {
                    if (data == "Success") {
                        window.location.href = window.location.href;
                    }
                    else if (data == "Unauthorized") {
                        showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to acknowledge notifications")%>");
                    }
                    else
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to acknowledge notification")%>");
                });
            }
        }

        function selectNetwork(id) {

            $.get('/Overview/LoadDisplayData/' + id, function (data) {
                $('#DisplayDataPartial').html(data);
            });
        }

        //function selectApplication(id, appid) {
        function selectApplication() {
            var appid = $('.profileSelect option:selected').val();
            var id = $('.networkSelect option:selected').val();
            $.get('/Overview/LoadDisplayData?id=' + id + '&appid=' + appid, function (data) {
                $('#DisplayDataPartial').html(data);
            });
        }

        function setPreferenceAppList(id) {
            $.get('/Overview/SensorProfiles/' + id, function (data) {
                $('#sensorProfiles').html(data);
            });
        }

        //window.setInterval('updateList()', 3000);
        //setInterval(function () { 'updateList()' }, 3000);
        //setInterval('$("#DisplayDataPartial").load("OverviewController/LoadDisplayData" + id + "&appid=" + appid) ', 6000);
       // setInterval(selectApplication(1, 2), 6000);
    </script>

</asp:Content>
