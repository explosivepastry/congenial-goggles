<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%if (Model.Count > 0)
	{ %>
<div class="x_panel shadow-sm rounded scrollParentSmall" id="hook-three" style="padding-top: 0px!important;">
	<div class="card_container__top" style="padding: 8px;">
		<div class="card_container__top__title dfac">
			<%=Html.GetThemedSVG("sensor") %>
			&nbsp;
            <%: Html.TranslateTag("Overview/Index|Sensors","Sensors")%>
		</div>
	</div>
	<div class="x_content col-12 sensorList-dash hasScroll-sm">
		<div class="">
			<%foreach (Sensor s in Model)
				{
					ViewData["Sensor"] = s;
					DataMessage dm = s.LastDataMessage;
					string data = (dm == null) ? "" : dm.DisplayData.ToString();
					string lastcommdate = s.LastCommunicationDate.OVToLocalDateTimeShort();
			%>
			<div class="col-lg-12 media_desktop dfac" style="padding: 10px; width: 100%;">
				<span class="col-3 overflow_title"><a class="pbh-color" href="/Overview/SensorChart/<%:s.SensorID%>"><%=s.SensorName%></a></span>
				<span class="col-3 overflow_data_medium"><%:data%></span>
				<div class="col-3 dfac">
					<%if (dm != null)
						{%>
					<div class="grey-back dfac">
						<%=lastcommdate%>
					</div>
					<%} %>
				</div>
				<span class="col-3 d-flex text-end">
					<a class="btn btn-primary btn-sm ms-auto" href="/Overview/SensorChart/<%:s.SensorID%>">
						<%: Html.TranslateTag("Overview/Index|View Sensor","View Sensor")%>
					</a>
				</span>
			</div>

			<div class="media_mobile dashList_row">
				<div class="dashList_row__name"><a style="color: #2699fb;" href="/Overview/SensorChart/<%:s.SensorID%>"><%=s.SensorName%></a></div>
				<%--				<div class=""><%:data%></div>--%>
				<div class="dashList_row__time dfac">
					<%if (dm != null)
						{%>
					<div class="grey-back">
						<%=lastcommdate%>
					</div>
					<%} %>
				</div>
				<div class=" dfjcfeac dashList_row__btn" style="text-align: center;">
					<a href="/Overview/SensorChart/<%:s.SensorID%>">
						<svg xmlns="http://www.w3.org/2000/svg" width="22" height="15" viewBox="0 0 22 15">
							<path id="ic_remove_red_eye_24px" d="M12,4.5A11.827,11.827,0,0,0,1,12a11.817,11.817,0,0,0,22,0A11.827,11.827,0,0,0,12,4.5ZM12,17a5,5,0,1,1,5-5A5,5,0,0,1,12,17Zm0-8a3,3,0,1,0,3,3A3,3,0,0,0,12,9Z" transform="translate(-1 -4.5)" class="dash-icon-fill" />
						</svg>
					</a>
				</div>
			</div>
			<%           
				}
			%>
		</div>
	</div>
</div>
<%} %>
