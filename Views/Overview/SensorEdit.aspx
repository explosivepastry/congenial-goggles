<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Sensor>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sensor Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    </script>

    <div class="container-fluid">
        <%Html.RenderPartial("SensorLink", Model); %>

        <%
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!Model.CanUpdate)
            {
                dic.Add("disabled", "disabled");
                ViewData["disabled"] = true;
            }

            ViewData["HtmlAttributes"] = dic;%>
        <div id="wrapper">
            <div class="rule-card_container w-100">
                <div class="">
                    <div class="x_title">
                        <div class="card_container__top">
                            <div class="card_container__top__title d-flex justify-content-between" style="min-height: 60px; padding-right: 0;">
                                <div style="padding-left: 0;" class="col-sm-3 col-6">
                                    <%: Model.MonnitApplication.ApplicationName%> <%: Html.TranslateTag("Settings","Settings")%>
                                </div>
                                <%--<%if (Model.SensorTypeID == (int)eWitType.PoE_Wit || Model.SensorTypeID == (int)eWitType.LTE_Wit)
                                    {%>
                                <div class="col-md-9 col-sm-9 col-xs-6 top-add-btn-row media_desktop" style="margin-top: -18px; margin-bottom: -12px;">
                                    <a href="/Overview/InterfaceEdit/<%:Model.SensorID%>" class="btn btn-secondary penciledit" style="max-width: 175px; font-size: 14px; padding: 5px 10px;">
                                        <%: Html.TranslateTag("Overview/SensorEdit|Interface Settings","Interface Settings")%>
                                        <%=Html.GetThemedSVG("edit") %>
                                    </a>
                                    <%} %>--%>

                                <div class="nav navbar-right panel_toolbox" style="align-items: center;">
                                    <!-- help button  sensoredit-->
                                    <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-bs-target=".pageHelp">
                                        <div class="help-hover" style="padding: 0.5rem;">
                                            <%=Html.GetThemedSVG("circleQuestion") %>
                                        </div>
                                    </a>

                                    <!-- Three dot dropdown button  sensoredit-->
                                    <div class="home-icon-card dropdown menu-hover three-dot-menu-button" style="margin: 0" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" title="Options">
                                        <%=Html.GetThemedSVG("menu") %>
                                    </div>

                                    <ul class="dropdown-menu ddm" data-bs-auto-close="true" style="min-width: 184px;">

                                        <%if (Model.SensorTypeID == (int)eWitType.PoE_Wit || Model.SensorTypeID == (int)eWitType.LTE_Wit)
                                            {%>
                                        <li>
                                            <a href="/Overview/InterfaceEdit/<%:Model.SensorID%>" class="dropdown-item menu_dropdown_item">
                                                <span><%: Html.TranslateTag("Overview/SensorEdit|Interface Settings","Interface Settings")%></span>
                                                <%=Html.GetThemedSVG("edit") %>
                                            </a>
                                            <%} %>
                                        </li>

                                        <li id="clickToToggleText">
                                            <a class="dropdown-item menu_dropdown_item"
                                                id="ShowMultiSensorSelection" <%=Model.CanUpdate ? "" : "disabled" %> value="Apply to Multiple">
                                                <span id="toggleableText"><%: Html.TranslateTag("Apply To Multiple","Apply To Multiple")%></span>
                                                <%=Html.GetThemedSVG("details") %>
                                            </a>
                                        </li>

                                        <% if (Model.GenerationType.ToUpper().Contains("GEN2") && new Version(Model.FirmwareVersion).Minor >= 23 && Model.ApplicationID != 142)
                                            { %>
                                        <li>
                                            <a class="dropdown-item menu_dropdown_item"
                                                href="/Overview/SensorSchedule/<%:Model.SensorID%>">
                                                <span><%: Html.TranslateTag("Schedule Sensor","Schedule Sensor")%></span>
                                                <%=Html.GetThemedSVG("schedule") %>
                                            </a>
                                        </li>
                                        <%}
                                            if (Model.CanUpdate)
                                            { %>
                                        <li id="DefaultsCalibrate" value="<%: Html.TranslateTag("Default","Default")%>">
                                            <a class="dropdown-item menu_dropdown_item">
                                                <span><%: Html.TranslateTag("Reset To Default","Reset To Default")%></span>
                                                <%=Html.GetThemedSVG("reset") %>
                                            </a>
                                        </li>
                                        <%}%>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>

                <form class="form-horizontal form-label-left" action="/Overview/SensorEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
                    <%: Html.ValidationSummary(false)%>

                    <input type="hidden" value="/Overview/SensorEdit/<%:Model.SensorID %>" name="returns" id="returns" />
                    <input type="hidden" value="<%:Model.SensorID %>" name="ids" id="sensorIDs" />
                    <input type="hidden" value="<%:Model.SensorID %>" name="originalID" id="originalID" />
                    <% 
                        //If specific monnit application edit view exists use that page
                        Account acc = Account.Load(Model.AccountID);
                        if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit"))
                        {

                            string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Edit", Model.ApplicationID.ToString("D3"));
                            if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                            {
                                ViewBag.returnConfirmationURL = ViewToFind;
                                Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
                            }
                            else
                            {
                                Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", Model);
                            }
                        }
                        else //If they don't have permissions to view advanced partials try to load simple edit
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", Model);
                        }
                    %>
                </form>

                <%--                <button class="btn btn-secondary btn-sm" style="width: 135px; margin-top: 20px;" onclick="hideOtherBtns()" type="button" id="ShowMultiSensorSelection" <%=Model.CanUpdate ? "" : "disabled" %> value="Apply to mMultiple">
                    <%: Html.TranslateTag("Apply to multiple","Apply to multiple")%>
                </button>--%>

                <div class="col-12" id="multiSensorSelection" style="display: none;">
                    <%Html.RenderPartial("~\\Views\\Overview\\_MultiSelectSensorList.ascx", Model);%>
                </div>
            </div>
        </div>
    </div>
    <!-- pageHelp button modal -->
    <div class="modal fade pageHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Overview/SensorHome|Sensor Edit Settings","Sensor Edit Settings")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <% 
                        //If specific monnit application edit view exists use that page
                        string HelpViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Help", Model.ApplicationID.ToString("D3"));
                        if (MonnitViewEngine.CheckPartialViewExists(Request, HelpViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = HelpViewToFind;
                            Html.RenderPartial("~\\Views\\Sensor\\" + HelpViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Help.ascx", Model);
                        }
                    %>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <!-- siteSurveyHelp modal -->
    <div class="modal fade siteSurveyHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="siteSurveyHelp"><%: Html.TranslateTag("Overview/SensorHome|Signal Reliability Level","Signal Reliability Level")%></h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="word-choice">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Mission-Critical","Mission-Critical")%>
                        </div>
                        <div class="word-def">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Transmit data the first time, every time. Optimizes deliverability over range.","Transmit data the first time, every time. Optimizes deliverability over range")%>.
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="word-choice">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Strong","Strong")%>
                        </div>
                        <div class="word-def">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Occasional retry is acceptable. Balances deliverability and range.","Occasional retry is acceptable. Balances deliverability and range")%>.
                            <hr />
                        </div>
                    </div>
                    <div class="row">
                        <div class="word-choice">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Functional","Functional")%>
                        </div>
                        <div class="word-def">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Frequent retries is acceptable. Optimizes range over first data transmission deliverability.","Frequent retries is acceptable. Optimizes range over first data transmission deliverability")%>.
                            <hr />
                        </div>
                    </div>

                    <div class="row">
                        <div class="word-choice">
                        </div>
                        <div class="word-def">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Regardless of the setting, sensors log data until successful transmission.","Regardless of the setting, sensors log data until successful transmission")%>.<br />

                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Changes in the environment may affect the timeliness of data delivery.","Changes in the environment may affect the timeliness of data delivery")%>.
                      
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <!-- End help button modal -->
    <style>
        .help-hover svg {
            fill: var(--help-highlight-color);
            width: 30px;
            height: 30px;
        }

        .three-dot-menu-button > svg {
            width: 7px !important;
            height: 25px !important;
            margin-left: .5rem;
        }

        @media screen and (max-width: 500px) {
            .three-dot-menu-button > svg {
                margin-right: 1rem;
            }
        }
    </style>

    <script type="text/javascript">


        $(function () {

            $('#save').click(function () {
                $.get('/Sensor/CanUpdate/<%: Model.SensorID %>', function (data) {
                    if (data.toLowerCase() != 'true') {
                        $('.mainSensorCardInside .pendingsvg').prop('hidden', false);
                    }
                });
            });

            $('#ShowMultiSensorSelection').on('click', function (e) {

                var multiSelectIsHidden = $('#multiSensorSelection').css('display') == "none";

                if (multiSelectIsHidden) {
                    $('#simpleEdit_<%:Model.SensorID %> button').css('display') == "block";
                    $('#multiSensorSelection').show().focus();
                    $(".ln_solid").hide();
                    $('#save').css('display', 'none');
                    $('#wrapperForSometimesSpan').css('padding', '2rem 0');

                } else {
                    $('#simpleEdit_<%:Model.SensorID %> button').css('display') == "none";
                    $('#multiSensorSelection').hide();
                    $('#save').css('display', 'block');
                    $(".ln_solid").show();

                    $('#wrapperForSometimesSpan').css('padding', '0');

                }
            });

            $('#SaveMultiSensorSelection').on('click', function (e) {
                e.preventDefault();

                // To prevent overriding names for all the sensors selected
                $('#SensorName').remove();

                var sensorIDs = "";
                $('.triggerDevice__status.ListBorderActive').each(function (selectedSensor) {
                    sensorIDs += $(this).attr('data-id') + ",";
                });
                sensorIDs = sensorIDs.substring(0, sensorIDs.length - 1);
                $('#sensorIDs').val(sensorIDs);

                var formData = $('#simpleEdit_<%:Model.SensorID %>').serialize();

                $.post('/Overview/MultiSensorEdit/', formData, function (data) {
                    $('#simpleEdit_<%:Model.SensorID %>').parent().html(data);
                });
            });
        });

        function hideOtherBtns() {
            let zz = document.getElementById("wifiHide")
            let z = document.getElementById("sensName");
            let x = document.getElementById("hideMyDiv");
            let y = document.getElementById("ShowMultiSensorSelection");

            if (x.style.display === "none") {
                x.style.display = "block";
            } else {
                x.style.display = "none";
            }
            if (y.style.display === "none") {
                y.style.display = "block";
            } else {
                y.style.display = "none";

            } if (z.style.display === "block") {
                z.style.display = "none";
            } else {
                z.style.display = "none";
            } if (zz.style.display === "block") {
                zz.style.display = "none";
            } else {
                zz.style.display = "none";
            }
        }

        $('.home-icon-card, .dropdown, .menu-hover').click(function (e) {
            e.stopPropagation();
            $(this).closest('.dropdown-menu').toggle();
        });


        $('#clickToToggleText').click(function () {
            if ($('#toggleableText').text() == "Apply To Multiple") {
                $('#toggleableText').text("Switch To Single");
            } else {
                $('#toggleableText').text("Apply To Multiple");
            }
        });

        var DefaultYouSure = "<%: Html.TranslateTag("Are you sure you want to reset this sensor to defaults?","Are you sure you want to reset this sensor to defaults?")%>";

        $(function () {
            $('#DefaultsCalibrate').on("click", function () {
                var SensorID = <%: Model.SensorID%>;
                var pID = $('#simpleEdit_<%:Model.SensorID %>').parent();
                let values = {};
                values.url = '/Overview/SensorDefault/' + SensorID;
                values.text = DefaultYouSure;
                values.callback = (result) => {
                    debugger;
                    pID.html(result);

                    setTimeout(function () {
                        window.location.href = window.location.href;
                    }, 2000);
                };
                openConfirm(values);

                //if (confirm(DefaultYouSure)) {

                //    $.get('/Overview/SensorDefault/' + SensorID, function (result) {
                //        pID.html(result);

                //        setTimeout(function () {
                //            window.location.href = window.location.href;
                //        }, 2000);
                //    });
                //}
            });
        });

    </script>

</asp:Content>
