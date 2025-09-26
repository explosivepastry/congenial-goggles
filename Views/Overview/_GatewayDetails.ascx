<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.Gateway>>" %>

<%="" %>

<%if (Model.Count == 0 && ViewBag.TotalGateways == 0)
    { %>
<div class="container">
    <div class="col-md-12 col-xs-12">
        <div class="gridPanel no-sensor-container">
            <% Html.RenderPartial("_LonelyAstronaut"); %>
            <h2 style="text-align: center;"><%: Html.TranslateTag("Overview/_GatewayDetails|There are no Gateways. Click below to add a Gateway.","There are no Gateways. Click below to add a Gateway.")%>.</h2>
            <a class="add-btn" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                    <defs>
                        <clipPath id="clip-path">
                            <rect width="16" height="15.999" fill="none" />
                        </clipPath>
                    </defs>
                    <g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
                        <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                    </g>
                </svg>
                &nbsp; &nbsp; <%: Html.TranslateTag("Overview/_GatewayDetails|Add Gateway", "Add Gateway")%>
            </a>
        </div>
    </div>
</div>
<%}
    else
    { %>
<%
    long networkID = MonnitSession.SensorListFilters.CSNetID;
    List<CSNet> networks = new List<CSNet>();

    string networkName = "";
    if (ViewBag.netID != null)
    {
        networkID = ViewBag.netID;
    }

    CSNet network = CSNet.Load(networkID);

    if (network != null)
    {
        networkName = network.Name.Length > 0 ? network.Name : network.CSNetID.ToString();
        networks = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(network.AccountID);
    }
%>

<div class="card_container__body px-0">
    <div class="glanceView" style="display: flex; flex-wrap: wrap;">
        <%
            bool alt = true;
            bool ShowSignal = false;

            foreach (var item in Model)
            {
                bool isFavorite = MonnitSession.IsGatewayFavorite(item.GatewayID);
                List<Notification> rules = Notification.LoadByGatewayID(item.GatewayID);
                MvcHtmlString WarningIcon = new MvcHtmlString("");
                string WarningLabel = ("");
                string WarningDescription = ("");



                if (item.GatewayTypeID == 10 || item.GatewayTypeID == 11 || item.GatewayTypeID == 35 || item.GatewayTypeID == 36)//don't show wifi gateways here
                    continue;
                //           if (item.GatewayID == eSensorStatus.Offline)
                //           {
                //WarningIcon = Html.GetThemedSVG("db-wifi-off");
                //       WarningLabel = "OFFLINE";
                //       WarningDescription = "Reset or check your wifi/ethernet connection";
                //           }

                GatewayMessage lastGatewayMessage = GatewayMessage.LoadLastByGateway(item.GatewayID);

                //string imagePath = Html.GetThemedContent("/images/good.png");
                //if (item.LastCommunicationDate == DateTime.MinValue)
                //    imagePath = Html.GetThemedContent("/images/sleeping.png");
                //else if (item.ReportInterval != double.MinValue && item.LastCommunicationDate.AddMinutes(item.ReportInterval * 2 + 1) < DateTime.UtcNow)//Missed more than one heartbeat + one minute to take drift into account
                //    imagePath = Html.GetThemedContent("/images/Alert.png");

                MvcHtmlString imagePathPower = Html.GetThemedSVG("plugsensor1");
                if (item.CurrentPower != 0 && item.CurrentPower != 1)
                {
                    if (item.Battery <= 0)
                        imagePathPower = Html.GetThemedSVG("bat-dead");
                    else if (item.Battery <= 10)
                        imagePathPower = Html.GetThemedSVG("bat-low");
                    else if (item.Battery <= 25)
                        imagePathPower = Html.GetThemedSVG("bat-low");
                    else if (item.Battery <= 50)
                        imagePathPower = Html.GetThemedSVG("bat-half");
                    else if (item.Battery <= 75)
                        imagePathPower = Html.GetThemedSVG("bat-full-ish");
                    else if (item.Battery > 75 && item.Battery <= 100)
                        imagePathPower = Html.GetThemedSVG("bat-ful");
                    else if (item.Battery > 100)
                        imagePathPower = Html.GetThemedSVG("plugsensor1");
                }
                if (item.Status == eSensorStatus.Offline)
                {
                    WarningIcon = Html.GetThemedSVG("db-wifi-off");
                    WarningLabel = "OFFLINE";
                    WarningDescription = "Reset or check your wifi/ethernet connection";
                }
                else if (item.Status == eSensorStatus.OK)
                {
                    WarningIcon = Html.GetThemedSVG("circle-check-green");
                    WarningLabel = "Good";
                    WarningDescription = "Your Gateway is working properly";
                }
                else if (item.Status == eSensorStatus.Warning)
                {
                    WarningIcon = Html.GetThemedSVG("db-low-battery");
                    WarningLabel = "Warning";
                    WarningDescription = "Gateway running on Battery, not charging";
                }
        %>
        <style>
            .corp-status[title]:hover {
                color: red !important;
                font-weight: bold;
                border-radius: 5px;
            }
        </style>

        <div class="sensor_card-w-data" id="GatewayHome<%:item.GatewayID %>">
            <div class=" corp-status expandable gatewayStatus<%:item.Status.ToString() %>" style="height: 100%" onclick="expandBox(<%: item.GatewayID %>)" data-title="<%=WarningDescription %>"></div>

            <%------------------------------------------------------------------   
                                      Expanded Box
     ---------------------------------------------------------------%>
            <div id="box_<%: item.GatewayID %>" class="help-cover ">
                <div class="warningLabel">
                    <div style="width: 25px;"><%=WarningIcon %> </div>
                    <p style="margin: 0; font-size: 14px;"><%=WarningLabel %></p>
                </div>

                <div class="rule-container-error" style="margin: 7px 10px;">
                    <div>
                        <p class="sensor_damage"><%=WarningDescription %></p>
                    </div>

                </div>
            </div>
            <%----------------------------   End Expand box--%>

            <div style="height: 100%; width: 100%;">

                <div class="df viewGatewayDetails <%:item.GatewayID %>" id="GatewayHome" style="height: 100%;">

                    <a class="df" style="width: 100%;" href="/Overview/GatewayHome/<%=item.GatewayID %>">
                        <div class="gw-top-icon">
                            <div class="divCellCenter holder holder<%:item.Status.ToString() %> my-2" style="height: fit-content;">
                                <div>

                                    <div class="icon gwicon "><%=Html.GetThemedSVGForGateway(item.GatewayTypeID) %></div>
                                </div>
                            </div>

                            <div style="display: flex; justify-content: right; align-items: center; <%= item.isPaused() ? "": "visibility: hidden;" %>" title="<%: Html.TranslateTag("Paused","Paused")%>" class="pendingEditList pausesvg">
                                <%=Html.GetThemedSVG("pause") %>
                            </div>
                        </div>
                        <div align="left" class="dffdc">
                            <div class="glance-text" style="height: 100%; display: flex; flex-direction: column; align-items: center; width: 100%;">
                                <div style="font-size: 14px; display: flex; justify-content: start; width: 100%;" title="<%= item.Name %>" class="card-data-name"><%= item.Name.Length > 26 ? item.Name.Substring(0, 26).Insert(26, "...") : item.Name %></div>
                                <div class="glance-reading" style="margin-top: auto;">
                                    <div class="glance-lastDate dfac">
                                        <%=Html.GetThemedSVG("clock") %>

                                        <%: GatewayMessage.LoadLastByGateway(item.GatewayID) == null ? Html.TranslateTag("Unavailable", "Unavailable") : (item.LastCommunicationDate.ToElapsedMinutes() > 120 ? item.LastCommunicationDate.OVToLocalDateTimeShort() : item.LastCommunicationDate.ToElapsedMinutes().ToString() + " " + Html.TranslateTag("Minutes ago", "Minutes ago")) %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                    <%MvcHtmlString SignalIcon = new MvcHtmlString("");
                        // SignalStrength
                        if (lastGatewayMessage != null)
                        {
                            double Percent = Math.Round(item.CurrentSignalStrength.ToDouble().LinearInterpolation(0, 0, 28, 100));
                            if (Percent > 100)
                            {
                                Percent = 100;
                            }

                            bool cellGateway = item.GatewayType.GatewayTypeID == 8 || item.GatewayType.GatewayTypeID == 9 || item.GatewayType.GatewayTypeID == 13 || item.GatewayType.GatewayTypeID == 14 || item.GatewayType.GatewayTypeID == 17 || item.GatewayType.GatewayTypeID == 18 || item.GatewayType.GatewayTypeID == 19 || item.GatewayType.GatewayTypeID == 20 || item.GatewayType.GatewayTypeID == 21 || item.GatewayType.GatewayTypeID == 23 || item.GatewayType.GatewayTypeID == 24 || item.GatewayType.GatewayTypeID == 25 || item.GatewayType.GatewayTypeID == 26 || item.GatewayType.GatewayTypeID == 27 || item.GatewayType.GatewayTypeID == 30 || item.GatewayType.GatewayTypeID == 32;

                            if (cellGateway)
                            {
                                if (Percent == 0)
                                {
                                    SignalIcon = Html.GetThemedSVG("signal-none");
                                }
                                else if (Percent <= 10)
                                {
                                    SignalIcon = Html.GetThemedSVG("signal-1");
                                }
                                else if (Percent <= 25)
                                {
                                    SignalIcon = Html.GetThemedSVG("signal-2");
                                }
                                else if (Percent <= 50)
                                {
                                    SignalIcon = Html.GetThemedSVG("signal-3");
                                }
                                else if (Percent <= 75)
                                {
                                    SignalIcon = Html.GetThemedSVG("signal-4");
                                }
                                else
                                {
                                    SignalIcon = Html.GetThemedSVG("signal-5");
                                }
                    %>
                    <div title="Signal: <%=lastGatewayMessage == null ? "" : Percent.ToString()  %>% " style="align-items: center; display: flex; width: 30px;"><%=SignalIcon %></div>
                    <%}
                        else
                        {%>
                    <div style="display: none" class="card-signal-icon icon icon-ethernet-b"></div>
                    <%}
                        }%>

                    <div style="align-items: center; display: flex; width: 30px;"><%=imagePathPower %></div>

                    <div class=" dfac" style="max-width: 42px;">
                        <div class="dropleft dfac" style="height: 100%;">

                            <div class="menu-hover menu-fav" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">

                                <div id="favoriteItem" class="listOfFav favoriteItem liked" style="display: <%= isFavorite ? "flex": "none" %>; align-items: start; justify-content: center;" <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
                                    <div class="listOfFav"><%= Html.GetThemedSVG("heart-beat")  %></div>
                                </div>
                                <%=Html.GetThemedSVG("menu") %>
                            </div>




                            <ul class="dropdown-menu ddm" aria-labelledby="dropdownMenuButton" style="padding: 0;">
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Overview/GatewayHome/<%=item.GatewayID %>">
                                        <span><%: Html.TranslateTag("Messages","Messages")%></span>
                                        <%=Html.GetThemedSVG("messages") %>
                                    </a>
                                </li>

                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Overview/GatewayNotification/<%=item.GatewayID %>">
                                        <span><%: Html.TranslateTag("Rules","Rules")%></span>
                                        <%=Html.GetThemedSVG("rules") %>
                                    </a>
                                </li>

                                <%if (MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
                                    {%>
                                <li>

                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Overview/GatewayEdit/<%=item.GatewayID %>">
                                        <span><%: Html.TranslateTag("Settings","Settings")%></span>
                                        <%=Html.GetThemedSVG("settings") %>
                                    </a>
                                </li>
                                <%} %>

                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Overview/GatewaySensorList/<%=item.GatewayID %>">
                                        <span><%: Html.TranslateTag("Sensors","Sensors")%></span>
                                        <%=Html.GetThemedSVG("sensor") %>
                                    </a>
                                </li>

                                <% if (MonnitSession.CustomerCan("Network_Edit"))
                                    {
                                        if (networks.Count() > 1)
                                        {%>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        href="/Setup/AssignDevice/<%=network.AccountID %>?networkID=<%=network != null ? network.CSNetID : long.MinValue %>&DeviceToMove=<%=item.GatewayID %>:IM<%:MonnitUtil.CheckDigit(item.GatewayID)%>">
                                        <span><%: Html.TranslateTag("Move","Move")%></span>
                                        <%=Html.GetThemedSVG("network") %>
                                    </a>
                                </li>

                                <%} %>
                                <hr />
                                <li>
                                    <a class="dropdown-item menu_dropdown_item" onclick="removeGateway('<%:item.GatewayID %>'); return false;" id="list">
                                        <span>
                                            <%: Html.TranslateTag("Delete","Delete")%> 
                                        </span>
                                        <%=Html.GetThemedSVG("delete") %>
                                    </a>
                                </li>
                                <% } %>
                            </ul>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <% } %>   <%--foreach--%>
    </div>

</div>
<%} %>
<style>
    .sensor_card-w-data {
        min-height: 5rem;
        direction: ltr;
        position: relative;
        display: flex;
        width: 23rem;
        box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 18%);
        background: #f5f5f5;
        align-items: center;
        border-radius: 0.3rem;
        margin: 0.5rem;
        padding: 0.2rem;
        transition: all 3s ease-in-out;
        /*   overflow:hidden;*/
    }

    .inside-card {
        color: black;
        align-items: center;
        /* justify-content:center;*/
        height: 100%;
        width: 100%;
    }

        .inside-card > svg {
            fill: #515356;
        }

    .help-cover {
        width: 93%;
        background: white;
        height: 90%;
        position: absolute;
        display: inline;
        z-index: 999;
        top: 3px;
        right: 4px;
        border-radius: 0 5px 5px 0;
        transform: translateX(-90%);
        pointer-events: none;
        opacity: 0;
        /*   transition: transform 1s cubic-bezier(.49,.08,.76,.96), opacity 1s ease-in-out;*/
        transition: transform 1s cubic-bezier(.17,.67,.83,.67), opacity 0.1s ease 1s;
    }

    .corp-status {
        cursor: pointer;
    }

        .corp-status:hover {
            transform: scale(.93);
            box-shadow: rgb(0 0 0 / 21%) 3px 3px 6px 0px inset, rgba(255, 255, 255, 0.5) -3px -3px 6px 1px inset;
        }

    .help-cover.expand {
        transform: translateX(0%);
        opacity: 1;
        transition: transform 1s cubic-bezier(.49,.08,.76,.96), opacity 1.5s ease-in-out;
        /*         transition: transform 1s ease-in-out, opacity 0.5s ease-in-out;*/
    }

        .help-cover.expand a {
            pointer-events: auto;
        }

    .rules-warn-icon > svg {
        width: 18px !important;
        height: 19px !important;
    }

    .rule-go-arrow > svg {
        cursor: pointer !important;
    }

    .menu-fav {
        display: flex;
        justify-content: space-around;
        flex-direction: column;
        height: 100%;
        cursor: pointer;
        padding: 5px 6px 5px 0;
    }

    @media screen and (max-width: 400px) {
        .sensor_card-w-data {
            margin: 0.5rem 0rem;
            padding: 0.2rem;
        }
    }
</style>

<script type="text/javascript">

    function removeGateway(item) {
        let values = {};
        values.url = `/CSNet/Remove/${item}`;
        values.text = "<%=Html.TranslateTag("Are you sure you want to remove this gateway from the network? This will also Reform the Gateway.","Are you sure you want to remove this gateway from the network? This will also Reform the Gateway.")%>";
        openConfirm(values);
    }

    $(document).ready(function () {
        $('#filterdGateways').html(<%:Model.Count%>);
        $('#totalGateways').html(<%:ViewBag.TotalGateways%>);

        $('.sortable').click(function (e) {
            e.preventDefault();

            $.get('/Overview/GatewaySortBy', { column: $(this).attr("href"), direction: $(this).attr("data-direction") }, function (data) {
                if (data == "Success")
                    getMain();
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }).css('font-weight', 'bold').css('text-decoration', 'none');
    });
    $("li a").each(function () {
        if (this.href == window.location.href) {
            $(this).addClass("active");
        }
    });

    function expandBox(gatewayId) {
        var mainInside = document.getElementById("GatewayHome" + gatewayId);
        var inside = document.getElementById("inside_" + gatewayId);
        var box = document.getElementById("box_" + gatewayId);
        box.classList.toggle("expand");
        if (box.classList.contains("expand")) {
            mainInside.style.overflow = "hidden";
            inside.style.display = "none";
        } else {
            setTimeout(() => {
                mainInside.style.overflow = "inherit";
            }, 1200);
            inside.style.display = "block";
        }
    }

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

    $(document).ready(function () {
        $(".listOfFav svg").addClass("liked");
    });

</script>
