<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Notification>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Actions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%
            ViewBag.NotiID = Model.NotificationID;

        %>
        <div class="clearfix"></div>

        <div>
            <%:Html.Partial("Header") %>


            <!-- Event List View -->
            <div class="rule-card_container" style="max-width: fit-content; margin-bottom: 1rem;">
                <div class="row">

                    <div class="card_container__top">
                        <div class="card_container__top__title">

                            <%: Html.TranslateTag("Events/Triggers|Conditions")%>
                        </div>
                        <div class="nav navbar-right panel_toolbox">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="card_container__body">
                        <div class="col-12">
                            <div class=" powertour-hook" id="hook-four">
                                <div class="col-12">
                                    <%switch (Model.NotificationClass)
                                        {
                                            case eNotificationClass.Advanced:
                                                Html.RenderPartial("EditAdvancedTrigger");
                                                break;
                                            case eNotificationClass.Application:
                                                Html.RenderPartial("EditApplicationTrigger");
                                                break;
                                            case eNotificationClass.Inactivity:
                                                Html.RenderPartial("EditInactivityTrigger");
                                                break;
                                            case eNotificationClass.Low_Battery:
                                                Html.RenderPartial("EditBatteryTrigger");
                                                break;
                                            case eNotificationClass.Timed:
                                                Html.RenderPartial("EditScheduledTrigger");
                                                break;
                                            case eNotificationClass.Credit:
                                            case eNotificationClass.HVAC:
                                            default:
                                                break;
                                        }%>
                                </div>

                                <div id="save-button" class="text-end">
                                    <button type="button" value="<%: Html.TranslateTag("Save", "Save")%>" class="btn btn-primary" onclick="saveTrigger(this);">
                                        <%: Html.TranslateTag("Save", "Save")%>
                                    </button>
                                    <button class="btn btn-primary" id="saving" type="button" disabled style="display: none;">
                                        <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                        Saving...
                                    </button>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div id="result"></div>

                            <script type="text/javascript">
                                function saveTrigger(btn) {
                                    btn = $(btn);
                                    btn.hide();
                                    $('#saving').show();
                                    var beginAjaxTime = Date.now();

                                    var NotiID = <%=Model.NotificationID%>;
                                    //var NotiName = $('#name').val();
                                    var NotiScaleID = $('#ScaleID').val();
                                    //$.post("/Rule/RuleNameEdit/", { id: NotiID, name: NotiName, scaleID: NotiScaleID  }, function (data) {
                                    $.post("/Rule/RuleNameEdit/", { id: NotiID, scaleID: NotiScaleID }, function (data) {
                                        if (data != "Success") {
                                            $('#saving').hide();
                                            toastBuilder("Named Failed")
/*                                            $('#result').html("Named Failed.");*/
                                        }
                                    });

                                    // triggerUrl and triggerSettings functions defined in trigger class partial referenced above
                                    $.post(triggerUrl(), triggerSettings(), function (partial) {
                                        //Show loader for at leat 500ms
                                        var timeout = 500 - (Date.now() - beginAjaxTime);
                                        if (timeout < 0) timeout = 0;
                                        setTimeout(function () {
                                            $('#saving').hide();
                                            btn.show();
                                            toastBuilder("Success");

                                            $.post('/Rule/GetStringForRuleCard', { id: "<%= Model.NotificationID %>" })
                                                .done(function (data) {
                                                    var $updatedPartialView = $("#RuleInfoCardConditionText");
                                                    if ($updatedPartialView.length) {
                                                        $updatedPartialView.html(data);
                                                    } else {
                                                        console.error("Element with ID 'RuleInfoCardConditionText' not found.");
                                                    }
                                                })
                                                .fail(function (jqXHR, textStatus, errorThrown) {
                                                    console.error("Failed to load partial view:", textStatus, errorThrown);
                                                });

                                        }, timeout);
                                    });
                                }
                            </script>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="clearfix"></div>



        <div id="deviceList">
            <%=Html.Partial("_TriggerDeviceList") %>
        </div>
        <div class="clearfix"></div>
    </div>
    <!-- page content -->

    <!-- Custom Theme Scripts -->

    <script src="/Scripts/events.js"></script>

    <script type="text/javascript">
        <%= ExtensionMethods.LabelPartialIfDebug("Triggers.aspx") %>

        function toggleSensor(sensorID, datumindex) {
            var add = $('.notiSensor' + sensorID + '_' + datumindex).hasClass('ListBorderNotActive');
            var url = "/Notification/ToggleSensor/<%:Model.NotificationID %>";
            var params = "sensorID=" + sensorID;
            params += "&add=" + add;
            params += "&datumindex=" + datumindex;

            $.post(url, params, function (data) {
                if (data == "Success") {
                    if (add) {
                        $('.notiSensor' + sensorID + '_' + datumindex).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                    } else {
                        $('.notiSensor' + sensorID + '_' + datumindex).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                    }
                }
                else {
                    let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                    values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
                    openConfirm(values);
                    $('#modalCancel').hide();
                }

            });
        }

        function toggleGateway(gatewayID) {
            var add = $('.notiGateway' + gatewayID).hasClass('ListBorderNotActive');
            var url = "/Notification/ToggleGateway/<%:Model.NotificationID %>";
            var params = "gatewayID=" + gatewayID;
            params += "&add=" + add;

            $.post(url, params, function (data) {
                if (data == "Success") {
                    if (add)
                        $('.notiGateway' + gatewayID).removeClass('ListBorderNotActive').addClass('ListBorderActive');
                    else
                        $('.notiGateway' + gatewayID).removeClass('ListBorderActive').addClass('ListBorderNotActive');
                }
                else {
                    let values = {};
                    <%--values.redirect = '/Ack/<%:Model.NotificationRecordedID%>/<%:Model.NotificationGUID%>';--%>
                    values.text = "<%=Html.TranslateTag("Oops! That did not work, please refresh your page. If this error continues, contact support.")%>";
                    openConfirm(values);
                    $('#modalCancel').hide();
                }
            });
        }


        const toggleSaveButton = () => {
            const container = document.querySelector('.rule-card_container');
            const saveButton = document.querySelector('#save-button');
            if (container) {
                const hasSelect = container.querySelector('select') !== null;
                const hasInput = container.querySelector('input') !== null;

                if (hasSelect || hasInput) {
                    saveButton.style.display = 'block';
                } else {
                    saveButton.style.display = 'none';
                }
            }
        }

        toggleSaveButton();

    </script>

    <style>
        .form-select {
            max-width: 250px !important;
        }
    </style>
</asp:Content>
