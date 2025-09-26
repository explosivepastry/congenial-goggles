<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>
<% 

    var notiList = ViewBag.NotiList;
    if (notiList.Count > 0)
    {
%>
<div class="x_panel shadow-sm rounded scrollParentSmall" id="hook-four" style="padding-top: 0px!important; margin-bottom: 20px;">
    <div class="card_container__top" style="padding: 8px;">
        <div class="card_container__top__title">
            <%=Html.GetThemedSVG("notifications") %>
            &nbsp;
			<%: Html.TranslateTag("Overview/Index|Notifications", "Notifications")%>
        </div>
    </div>
    <div class="x_content col-12 sensorList-dash hasScroll-sm">
        <div class="">
            <% 

                foreach (NotificationRecorded n in notiList)
                {

                    NotificationTriggered nt = NotificationTriggered.Load(n.NotificationTriggeredID);

                    if (nt == null)
                        continue;

                    string deviceName = "";
                    Sensor s = Sensor.Load(n.SensorID);
                    Gateway g = Gateway.Load(n.GatewayID);
                    if (s != null)
                    {
                        deviceName = s.SensorName;
                    }
                    else
                    {
                        if (g != null)
                            deviceName = g.Name;
                    }

            %>
            <div class="col-lg-12 d-flex align-items-center" style="padding: 10px; width: 100%;">
                <span class="col-3 overflow_title "><a class="pbh-color" href="/Rule/History/<%:nt.NotificationID%>"><%=deviceName%></a> </span>
                <span class="col-4"><%:nt.Reading%></span>
                <span class="col-3"><%:nt.StartTime.OVToLocalDateTimeShort()%></span>
                <span class="col-2 text-end">
                    <a href="/Rule/History/<%:nt.NotificationID%>" class="btn btn-primary btn-sm viewBtn" type="button"><%: Html.TranslateTag("Details")%>
                    </a>
                    <a class="viewEye" href="/Rule/History/<%:nt.NotificationID%>">
                        <svg xmlns="http://www.w3.org/2000/svg" width="22" height="15" viewBox="0 0 22 15">
                            <path id="ic_remove_red_eye_24px" d="M12,4.5A11.827,11.827,0,0,0,1,12a11.817,11.817,0,0,0,22,0A11.827,11.827,0,0,0,12,4.5ZM12,17a5,5,0,1,1,5-5A5,5,0,0,1,12,17Zm0-8a3,3,0,1,0,3,3A3,3,0,0,0,12,9Z" transform="translate(-1 -4.5)" class="dash-icon-fill" />
                        </svg>
                    </a>
                </span>
            </div>
            <%
                }
            %>
        </div>
    </div>
</div>
<%}%>
<style>
    @media only screen and (max-width: 850px) {
        .viewEye {
            display: block;
        }
        .viewBtn {
            display: none;
        }
    }

    @media only screen and (min-width: 851px) {
        .viewEye {
            display: none;
        }
        .viewBtn {
            display:inline;
        }
    }
</style>
