<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Notification>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    DefaultActions
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        String[] segments = HttpContext.Current.Request.Url.Segments;
        long id = segments[3].ToLong();
        long csnetid = long.MinValue;

        bool isSensor = false;
        Sensor sensor = Sensor.Load(id);
        Gateway gateway = Gateway.Load(id);

        if (sensor != null)
        {
            csnetid = sensor.CSNetID;
            isSensor = true;

            //Low Battery
            Notification lowbattery = new Notification();
            lowbattery.NotificationID = -1;
            lowbattery.Name = "Battery below 10%";

            Model.Add(lowbattery);
        }
        else
        {
            csnetid = gateway.CSNetID;
        }

        //Inactivity
        Notification Inactivity = new Notification();
        Inactivity.NotificationID = -2;
        Inactivity.Name = "Device is Inactive";

        Model.Add(Inactivity);
    %>

    <div class="container-fluid">
        <%Html.RenderPartial("_SetupStepper", id); %>

        <div>
            <div class="col-lg-8 col-12 ps-0">
                <div class="x_panel shadow-sm rounded">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <%: Html.TranslateTag("Notify me when", "Notify me when")%>
                        </div>
                    </div>

                    <form id="actionsForm">

                        <div class="form-group">

                            <% foreach (Notification n in Model)
                                {
                            %>

                            <div id="hook-one">
                                <a class="gridPanel eventsList shadow-sm rounded" style="cursor: pointer;" onclick="$('#<%= n.NotificationID %>').toggleClass('ListBorderActive');$('#<%= n.NotificationID %>').toggleClass('ListBorderInactive');">
                                    <div class="triggerDevice__container notificationType" style="width: 100%;">
                                        <div class="triggerDevice__name">
                                            <strong><%:System.Web.HttpUtility.HtmlDecode(n.Name) %></strong>
                                            <%
                                                string name = n.Name;

                                                if (n.NotificationClass == eNotificationClass.Application /*|| (n.NotificationClass == eNotificationClass.Advanced != null && Advanced.UseDatums)*/)
                                                {
                                                    if (name != n.Name)
                                                    {
                                            %>

                                            <span style="font-size: 0.8em;"><%: " : " + name%></span>

                                            <%}
                                                }
                                                else
                                                {%>

                                            <%} %>
                                        </div>
                                        <div id="<%= n.NotificationID %>" class="gridPanel triggerDevice__status  noti_<%:n.NotificationID%> ListBorderNotActive" data-notiid="<%= n.NotificationID %>">
                                            <%=Html.GetThemedSVG("circle-check") %>
                                        </div>
                                    </div>
                                </a>

                            </div>


                            <% } %>

                            <div class="clearfix"></div>
                        </div>

                        <!-- Notification Type Section -->

                        <div class="card_container__top">
                            <div class="card_container__top__title">
                                <%: Html.TranslateTag("How would you like to be notified?", "How would you like to be notified?")%>
                            </div>
                        </div>

                        <div class="d-flex flex-wrap">
                            <%if (MonnitSession.CurrentCustomer.NotificationEmail != "")
                                { %>
                            <a class="gridPanel eventsList shadow-sm rounded mx-1 my-1" onclick="$('#notificationToEmail').toggleClass('actionTypeActive'); $('#notificationToEmail').toggleClass('actionTypeInactive');">
                                <div class="triggerDevice__container notificationType" style="width: 100%;">
                                    <div class="triggerDevice__name" style="display: flex; flex-direction: column;">
                                        <h5><%: Html.TranslateTag("By Email:")%></h5>
                                        <strong><%:MonnitSession.CurrentCustomer.NotificationEmail %></strong>
                                    </div>
                                    <div id="notificationToEmail" class="gridPanel triggerDevice__status actionTypeActive" data-add="notificationToEmail" style="cursor: default;">
                                        <svg style="margin-right: 5px;" xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 18 18">
                                            <path fill="#FFF" id="paper-plane-regular" d="M15.51.252.886,8.686a1.688,1.688,0,0,0,.2,3.02L5.1,13.369v2.967a1.689,1.689,0,0,0,3.044,1.005l1.54-2.078,3.934,1.624a1.691,1.691,0,0,0,2.313-1.3L18.023,1.972A1.691,1.691,0,0,0,15.51.252ZM6.791,16.337V14.065l1.287.531Zm7.474-1.009L8.858,13.1l4.929-7.112a.563.563,0,0,0-.833-.745L5.519,11.717l-3.79-1.568L16.353,1.711Z" transform="translate(-0.043 -0.025)" class="notifyIcons" />
                                        </svg>
                                    </div>
                                </div>
                            </a>
                            <% } %>


                            <%if (MonnitSession.CurrentCustomer.SendSensorNotificationToText)
                                { %>
                            <a class="gridPanel eventsList shadow-sm rounded mx-1 my-1" style="cursor: pointer; box-shadow: none;" onclick="$('#notificationToText').toggleClass('actionTypeActive'); $('#notificationToText').toggleClass('actionTypeInactive');">
                                <div class="triggerDevice__container notificationType" style="width: 100%;">
                                    <div class="triggerDevice__name" style="display: flex; flex-direction: column;">
                                        <h5><%: Html.TranslateTag("By Text:")%></h5>
                                        <strong><%:MonnitSession.CurrentCustomer.NotificationPhone %></strong>
                                    </div>
                                    <div class="gridPanel triggerDevice__status actionTypeInactive" data-add="notificationToText" id="notificationToText">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="16" viewBox="0 0 18 16">
                                            <path fill="#FFF" id="comments-regular" d="M16.616,44.65a5.259,5.259,0,0,0,1.375-3.507c0-2.857-2.39-5.218-5.506-5.639A6.568,6.568,0,0,0,6.493,32c-3.59,0-6.5,2.557-6.5,5.714a5.272,5.272,0,0,0,1.375,3.507A9.448,9.448,0,0,1,.19,43.182a.913.913,0,0,0-.137.893.725.725,0,0,0,.662.5,6.586,6.586,0,0,0,3.912-1.386,7.9,7.9,0,0,0,.887.175,6.548,6.548,0,0,0,5.977,3.493,7.286,7.286,0,0,0,1.869-.243A6.6,6.6,0,0,0,17.273,48a.728.728,0,0,0,.662-.5.921.921,0,0,0-.137-.893A9.187,9.187,0,0,1,16.616,44.65Zm-12.274-3.3-.534.4a6.077,6.077,0,0,1-1.347.764c.084-.168.169-.346.25-.529L3.2,40.875,2.421,40a3.478,3.478,0,0,1-.928-2.286c0-2.168,2.29-4,5-4s5,1.832,5,4-2.29,4-5,4a6,6,0,0,1-1.531-.2Zm11.221,2.075-.772.871.484,1.111c.081.182.166.361.25.529a6.077,6.077,0,0,1-1.347-.764l-.534-.4-.622.164a6,6,0,0,1-1.531.2,5.435,5.435,0,0,1-4.1-1.775c3.165-.386,5.6-2.764,5.6-5.654,0-.121-.012-.239-.022-.357,2.012.518,3.521,2.029,3.521,3.786A3.478,3.478,0,0,1,15.563,43.429Z" transform="translate(0.007 -32)" class="" />
                                        </svg>
                                    </div>
                                </div>
                            </a>
                            <%}%>
                            <%if (MonnitSession.CurrentCustomer.SendSensorNotificationToVoice)
                                { %>
                            <a class="gridPanel eventsList shadow-sm rounded mx-1 my-1" style="cursor: pointer; box-shadow: none;" onclick="$('#notificationToVoice').toggleClass('actionTypeActive'); $('#notificationToVoice').toggleClass('actionTypeInactive');">
                                <div class="triggerDevice__container notificationType" style="width: 100%;">
                                    <div class="triggerDevice__name" style="display: flex; flex-direction: column;">
                                        <h5><%: Html.TranslateTag("By Voice:")%></h5>
                                        <strong><%:MonnitSession.CurrentCustomer.NotificationPhone2 %></strong>
                                    </div>
                                    <div class="gridPanel triggerDevice__status actionTypeInactive" data-add="notificationToVoice" id="notificationToVoice">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="14" height="18" viewBox="0 0 14 18">
                                            <path fill="#FFF" id="phone-volume-solid" d="M3.548,17.823a11.389,11.389,0,0,1,0-16.521.644.644,0,0,1,.775-.085L6.687,2.641a.593.593,0,0,1,.25.737L5.755,6.226a.629.629,0,0,1-.644.377l-2.035-.2a9.025,9.025,0,0,0,0,6.311l2.035-.2a.629.629,0,0,1,.644.377l1.182,2.848a.593.593,0,0,1-.25.737L4.324,17.908A.644.644,0,0,1,3.548,17.823ZM9.01,3.357a2.183,2.183,0,0,1,0,2.287.449.449,0,0,1-.682.091l-.218-.2a.414.414,0,0,1-.082-.507,1.092,1.092,0,0,0,0-1.053.414.414,0,0,1,.082-.507l.218-.2A.449.449,0,0,1,9.01,3.357ZM12.356.151a6.576,6.576,0,0,1,0,8.7.45.45,0,0,1-.64.033l-.211-.2a.412.412,0,0,1-.034-.576,5.48,5.48,0,0,0,0-7.222A.412.412,0,0,1,11.5.313l.211-.2a.45.45,0,0,1,.64.033ZM10.68,1.731a4.38,4.38,0,0,1,0,5.539.45.45,0,0,1-.651.046l-.212-.2a.411.411,0,0,1-.047-.56,3.284,3.284,0,0,0,0-4.118.411.411,0,0,1,.047-.56l.212-.2a.45.45,0,0,1,.651.046Z" transform="translate(0 0)" class="" />
                                        </svg>
                                    </div>
                                </div>
                            </a>
                            <%} %>
                        </div>
                    </form>

                    <div style="text-align: right;">
                        <div class="powertour-hook text-end" id="hook-five">
                            <button
                                id="actionSubmit"
                                class="btn btn-primary">
                                <%: Html.TranslateTag("Done","Done")%>
                            </button>
                        </div>
                    </div>

                    <div id="saveMessage" style="color: red;"></div>
                </div>
            </div>
        </div>

    </div>

    <script type="text/javascript">

       document.getElementById("actionSubmit").addEventListener("click", function (event) {

            var actions = '';
            var isSensor = '<%=isSensor%>';

            $(".ListBorderActive").each(function () {
                actions += '&notificationIDList=' + $(this).data('notiid');
            });

            $(".actionTypeActive").each(function () {
                actions += '&sendMethod=' + $(this).data('add');
            })

            actions = actions.substring(1);


            var href = window.location.href;

            $.post(href, actions, function (data) {
                if (data == "Success") {
                    if (isSensor == 'True') {
                        disableUnsavedChangesAlert();
                        window.location = '/Setup/StatusVerification/<%=id%>';
                    }
                    else {
                        disableUnsavedChangesAlert();
                        window.location = '/Setup/StatusVerification/<%=id%>';
                    }
                } else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            })

        })

        $('#addNewDevice').on("click", function () {
            disableUnsavedChangesAlert();
            window.location = '/Setup/AssignDevice/<%: MonnitSession.CurrentCustomer.AccountID %>?networkID=" + <%: csnetid %>';
        })

<%--        $('#actionSubmit').on("click", function () {

            if (isSensor) {
                window.location = '/Setup/StatusVerification/<%=id%>';
            }

        })--%>
    </script>

    <style>
        .ListBorderInactive {
            background-color: #EEE;
            color: white;
        }

        .sendMethod {
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
            margin: 0 10px;
        }

            .sendMethod p {
                padding-top: 5px;
                font-size: 1.5rem;
            }

        .notiSvg {
            margin-right: 20px;
        }

        .actionTypeActive {
            background: #21CE99;
            color: #ffffff;
        }

        .actionTypeInactive {
            background-color: #EEE;
            color: white;
        }

        .notificationTypeDetails {
            display: flex;
            justify-content: space-around;
            align-items: center;
            margin-top: -10px;
        }

        .gridPanel p {
            font-size: 16px !important;
            padding-top: 8px;
        }


        .notificationType {
            box-shadow: none;
            height: 80px;
            display: flex;
            align-items: center;
            width: 200px;
        }
    </style>

</asp:Content>
