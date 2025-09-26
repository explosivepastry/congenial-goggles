<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>
<div class="col-md-12 col-xs-12" style="margin-bottom: 2px;">
	<div class="col-md-12 col-xs-12 view-btns_container">
		<div class="view-btns deviceView_btn_row">
			<a id="indexLink" href="/Overview/SensorIndex/<%:Model.CSNetID %>#<%:Model.SensorID %>" class="deviceView_btn_row__device">
				<div class="btn-default btn-lg btn-fill"><i class="fa fa-arrow-left"></i></div>
			</a>				
			
		</div>
	</div>
	<div class="col-lg-12 col-md-12 col-xs-12 device_detailsRow">
		<div class="col-lg-6 col-md-12 col-xs-12 device_detailsRow__card">
			<div class="x_panel">
				<div class="card_container__top ">
					<div class="card_container__top__title">
						<div title="<%=Model.ApplicationID %>" class="hidden-xs">
							<svg xmlns="http://www.w3.org/2000/svg" width="20" height="17" viewBox="0 0 22.707 19.46">
								<path id="Path_696" data-name="Path 696" d="M1.941-9.246h5.11l2.186-4,2.786,12.09,3.916-17.46,1.942,9.373h4.767" transform="translate(-0.941 19.619)" fill="rgba(33,206,153,0)" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" class="card_container__icon-stroke-1" />
							</svg>
							<%: Html.TranslateTag("Sensor","Sensor")%>:
                            &nbsp;
						</div>
						<span style="font-size: 14px; font-weight: 500;"><%= Model.SensorName %></span>

					</div>
				</div>
				<div class="x_content">
					<div class="card__container__body">
						<div class="col-md-12 col-xs-12 card_container__body__content">
							<div class="col-md-12 col-xs-12" style="display: flex; justify-content: space-between; align-items: flex-start;">
								<div class="" style="display: flex; flex-direction: column;">
									<%if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
										{
											if (Model.LastDataMessage != null)
											{
												if (!Model.IsPoESensor)
												{
													if (!Model.IsWiFiSensor)
													{ %>
									<div style="dfac">
										<b class="hidden-xs"><%: Html.TranslateTag("Gateway", "Gateway")%>:</b>
										<a style="color: #007FEB;" href="/Overview/GatewayHome/<%:Model.LastDataMessage.GatewayID%>">
											<%:Model.LastDataMessage.GatewayID > 0 ? Model.LastDataMessage.GatewayID.ToString() : ""%>
										</a>
									</div>

									<%  if (Model.ParentID > 0 && Model.ParentID != Model.LastDataMessage.GatewayID)
										{
											Sensor sens = Sensor.Load(Model.ParentID);
											if (sens != null && sens.ApplicationID == 45)
											{%>
									<b class="hidden-xs">Repeater:  </b><%:Model.ParentID > 0 ? Model.ParentID.ToString() : ""%>
									<%}
											}
										}
										else
										{
											Gateway gate = Gateway.Load(Model.LastDataMessage.GatewayID);%>
									<b class="hidden-xs">MacAddress:  </b><%: gate.MacAddress%>
									<%}
												}
											}

										}%>
									<div class="dfac">
										<b class="hidden-xs"><%: Html.TranslateTag("Network","Network")%>:</b>
										<a style="color: #007FEB;" href="/Network/Edit/<%=Model.CSNetID %>">&nbsp;<%= Model.Network == null ? "" : Model.Network.Name.ToStringSafe() %></a>
									</div>

								</div>
								<div class="dfjcsbac">
									<div id="fourth_element_to_target">
										<div class="dfac">
											<%if (new Version(Model.FirmwareVersion) >= new Version("25.45.0.0") || Model.SensorPrintActive)
											{
												if (Model.SensorPrintActive && Model.LastDataMessage != null)
												{
													if (Model.LastDataMessage.IsAuthenticated)
													{%>
														<%=Html.GetThemedSVG("printCheck") %>
													<%}
													else
													{%>
														<%=Html.GetThemedSVG("printFail") %>
													<%}
												} 
											}%>

											<%if (Model.LastDataMessage != null)
												{
													if (!Model.IsPoESensor)
													{
														int Percent = DataMessage.GetSignalStrengthPercent(Model.GenerationType, Model.SensorTypeID, Model.LastDataMessage.SignalStrength);
														string signal = "";

														if (Percent <= 0)
															signal = "-0";
														else if (Percent <= 10)
															signal = "-1";
														else if (Percent <= 25)
															signal = "-2";
														else if (Percent <= 50)
															signal = "-3";
														else if (Percent <= 75)
															signal = "-4";
														else
															signal = "-5";
											%>
											<div class="card-signal-icon icon icon-signal<%:signal %>"></div>
											<%}
												else
												{%>
											<div class="card-signal-icon icon icon-ethernet-b"></div>
											<%} %>
											<%} %>
										</div>
									</div>
									<div style="min-width: 70px;">
										<div style="text-align: center;" id="fifth_element_to_target">
											<%--                                                                <%if (item.Sensor.SensorPrintDirty || message == null)
                        {%>
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 18 24" style="margin-right:2px;">
                        <path id="iconmonstr-fingerprint-1_1_" data-name="iconmonstr-fingerprint-1 (1)" d="M19.391,17.1a.621.621,0,0,1,.385.833.672.672,0,0,1-.827.374c-2.617-.855-4.357-2.074-5.285-3.693a.638.638,0,0,1,.243-.875.651.651,0,0,1,.883.241c.77,1.341,2.285,2.372,4.6,3.12Zm-8.59-.611a21.471,21.471,0,0,1-5.227,2.186.652.652,0,0,0-.382.956.622.622,0,0,0,.717.285,23.2,23.2,0,0,0,5.142-2.087c.933-.526,1.02-.535,1.9.11a19.092,19.092,0,0,0,4.894,2.477.634.634,0,0,0,.747-.249.649.649,0,0,0-.324-.965,16.538,16.538,0,0,1-5.094-2.715c-.866-.685-1.156-.7-2.377,0Zm-.263,3.068a22.655,22.655,0,0,1-3.251,1.393.652.652,0,0,0-.425.472.636.636,0,0,0,.79.762,23.733,23.733,0,0,0,3.482-1.487,1.268,1.268,0,0,1,1.429,0,33.407,33.407,0,0,0,3.593,1.7.637.637,0,0,0,.856-.714.655.655,0,0,0-.4-.49,32.338,32.338,0,0,1-3.431-1.631c-1.133-.609-1.265-.717-2.642-.009ZM9.844,22.8a.617.617,0,0,0-.358.586.668.668,0,0,0,.693.61.616.616,0,0,0,.26-.055c1.7-.792,1.11-.84,3.027,0a.61.61,0,0,0,.25.05.669.669,0,0,0,.7-.6.617.617,0,0,0-.37-.6c-2.5-1.095-1.827-1.1-4.2,0ZM12.2,8.6a.64.64,0,0,0-.346-.841A.65.65,0,0,0,11,8.1c-1.058,2.5-3.731,4.424-7.253,5.273a.654.654,0,0,0-.495.741.637.637,0,0,0,.78.511C7.505,13.792,10.82,11.852,12.2,8.6Zm.647,4.136a11.3,11.3,0,0,0,1.944-3.071,3.238,3.238,0,0,0-.228-3.153A3.686,3.686,0,0,0,11.51,4.947,3.628,3.628,0,0,0,9.566,5.5,3.756,3.756,0,0,0,8.134,7.228C7.487,8.755,5.79,9.983,3.48,10.639a.658.658,0,0,0-.48.643.637.637,0,0,0,.813.6c2.7-.766,4.714-2.263,5.515-4.153A2.314,2.314,0,0,1,11.51,6.232,2.087,2.087,0,0,1,13.6,9.167c-1.529,3.612-5.11,5.937-9.157,6.958a.654.654,0,0,0-.461.846.634.634,0,0,0,.755.405A17.8,17.8,0,0,0,10.1,15.082a13.468,13.468,0,0,0,2.741-2.348Zm-5.819-6.2A5.618,5.618,0,0,1,8.075,5.12a5.363,5.363,0,0,1,4.2-1.379,4.892,4.892,0,0,1,3.514,1.993c1.08,1.539.809,3.067.547,4.544A5.036,5.036,0,0,0,16.6,13.99,5.388,5.388,0,0,0,19.78,16a.623.623,0,0,0,.785-.439.669.669,0,0,0-.456-.8A4.2,4.2,0,0,1,17.7,13.311a4.039,4.039,0,0,1-.087-2.81c.279-1.571.625-3.526-.759-5.5a6.182,6.182,0,0,0-4.438-2.536A6.657,6.657,0,0,0,7.2,4.18,6.813,6.813,0,0,0,5.907,5.888,3.66,3.66,0,0,1,3.6,7.893a.642.642,0,0,0,.4,1.222A4.845,4.845,0,0,0,7.026,6.534Zm-2.06-1.69.387-.572a7.5,7.5,0,0,1,7.323-2.909c2.886.376,5.256,2.014,6.037,4.173a8.422,8.422,0,0,1,.2,4.7c-.19,1.072-.354,2,.22,2.742a2.26,2.26,0,0,0,1.021.71.622.622,0,0,0,.842-.505.682.682,0,0,0-.42-.707.992.992,0,0,1-.416-.279c-.229-.3-.116-.933.027-1.739A9.642,9.642,0,0,0,19.931,5.1C18.8,1.979,15.185,0,11.477,0A8.717,8.717,0,0,0,4.924,2.746a4.764,4.764,0,0,0-.945,1.409A.612.612,0,0,0,4.157,5a.57.57,0,0,0,.809-.159Z" transform="translate(-3)" fill="#515356"/>
                    </svg>
                    <% } else {
                            if (message.IsAuthenticated)
                            {%>
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 20 24" style="margin-right:2px;">
                      <path id="iconmonstr-fingerprint-15" d="M22,17.5A4.5,4.5,0,1,1,17.5,13,4.5,4.5,0,0,1,22,17.5Zm-2.156-.882-.7-.7-2.115,2.169-.992-.94-.695.7,1.688,1.637,2.811-2.867ZM3.036,14.626a.638.638,0,0,1-.78-.511.653.653,0,0,1,.5-.741C6.273,12.525,8.946,10.6,10,8.1a.65.65,0,0,1,.848-.343A.64.64,0,0,1,11.2,8.6c-1.378,3.253-4.692,5.193-8.162,6.027ZM2.159,8.433a.636.636,0,0,0,.831.681A4.833,4.833,0,0,0,6.025,6.533,5.686,5.686,0,0,1,7.075,5.12a5.362,5.362,0,0,1,4.2-1.379,4.9,4.9,0,0,1,3.515,1.993c1.091,1.555.807,3.092.522,4.675a.652.652,0,0,0,.783.75.636.636,0,0,0,.49-.511C16.821,9.2,17.34,7.116,15.855,5a6.182,6.182,0,0,0-4.438-2.536A6.661,6.661,0,0,0,6.193,4.179,6.9,6.9,0,0,0,4.906,5.888,3.655,3.655,0,0,1,2.594,7.893a.637.637,0,0,0-.435.54Zm1-3.43a.57.57,0,0,0,.809-.159l.388-.572a7.5,7.5,0,0,1,7.322-2.909c2.888.376,5.256,2.014,6.037,4.173a8.521,8.521,0,0,1,.182,4.8.636.636,0,0,0,.535.739.66.66,0,0,0,.744-.539A9.577,9.577,0,0,0,18.932,5.1C17.8,1.979,14.187,0,10.477,0A8.717,8.717,0,0,0,3.924,2.746a4.745,4.745,0,0,0-.945,1.409A.612.612,0,0,0,3.157,5ZM10.62,17.531a.659.659,0,1,0-.568-1.187,21.719,21.719,0,0,1-5.479,2.329.652.652,0,0,0-.382.956.625.625,0,0,0,.718.284A25.023,25.023,0,0,0,10.62,17.531Zm1.225-4.8a11.249,11.249,0,0,0,1.943-3.071,3.228,3.228,0,0,0-.227-3.153,3.687,3.687,0,0,0-3.052-1.563A3.622,3.622,0,0,0,8.566,5.5,3.742,3.742,0,0,0,7.135,7.228C6.487,8.755,4.789,9.983,2.48,10.639a.658.658,0,0,0-.48.643.636.636,0,0,0,.812.6c2.7-.766,4.715-2.263,5.516-4.153A2.312,2.312,0,0,1,10.51,6.232a2.087,2.087,0,0,1,2.085,2.935c-1.528,3.612-5.11,5.937-9.156,6.958a.656.656,0,0,0-.462.846.635.635,0,0,0,.756.405A17.8,17.8,0,0,0,9.1,15.082a13.489,13.489,0,0,0,2.74-2.348ZM13.413,23.4a.616.616,0,0,0-.369-.6c-2.495-1.1-1.827-1.1-4.2,0a.613.613,0,0,0-.357.586A.666.666,0,0,0,9.18,24a.616.616,0,0,0,.26-.055c1.7-.792,1.109-.84,3.026,0a.614.614,0,0,0,.25.05.668.668,0,0,0,.7-.6ZM11.25,20.139a.75.75,0,0,0,.075-.628A.614.614,0,0,0,10.6,19.1a5.143,5.143,0,0,0-1.058.451,22.655,22.655,0,0,1-3.251,1.393.655.655,0,0,0-.426.472.637.637,0,0,0,.79.762,23.683,23.683,0,0,0,3.482-1.487C10.588,20.46,11.021,20.5,11.25,20.139Z" transform="translate(-2)" fill="#22ae73"/>
                    </svg>
                    <%} else {%>
                    <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 20 24" style="margin-right:2px;">
                      <path id="iconmonstr-fingerprint-16" d="M3.036,14.626a.638.638,0,0,1-.78-.511.653.653,0,0,1,.495-.741C6.273,12.525,8.946,10.6,10,8.1a.65.65,0,0,1,.848-.343A.64.64,0,0,1,11.2,8.6c-1.378,3.253-4.692,5.193-8.162,6.027ZM2.159,8.433a.636.636,0,0,0,.831.681A4.833,4.833,0,0,0,6.025,6.533,5.686,5.686,0,0,1,7.075,5.12a5.362,5.362,0,0,1,4.2-1.379,4.9,4.9,0,0,1,3.515,1.993c1.091,1.555.807,3.092.522,4.675a.652.652,0,0,0,.783.75.636.636,0,0,0,.49-.511C16.821,9.2,17.34,7.116,15.855,5a6.182,6.182,0,0,0-4.438-2.536A6.661,6.661,0,0,0,6.193,4.179,6.9,6.9,0,0,0,4.906,5.888,3.655,3.655,0,0,1,2.594,7.893a.637.637,0,0,0-.435.54Zm1-3.43a.57.57,0,0,0,.809-.159l.388-.572a7.5,7.5,0,0,1,7.322-2.909c2.888.376,5.256,2.014,6.037,4.173a8.521,8.521,0,0,1,.182,4.8.636.636,0,0,0,.535.739.66.66,0,0,0,.744-.539A9.577,9.577,0,0,0,18.932,5.1C17.8,1.979,14.187,0,10.477,0A8.717,8.717,0,0,0,3.924,2.746a4.745,4.745,0,0,0-.945,1.409A.612.612,0,0,0,3.157,5ZM10.62,17.531a.659.659,0,1,0-.568-1.187,21.719,21.719,0,0,1-5.479,2.329.652.652,0,0,0-.382.956.625.625,0,0,0,.718.284A25.023,25.023,0,0,0,10.62,17.531Zm1.225-4.8a11.249,11.249,0,0,0,1.943-3.071,3.228,3.228,0,0,0-.227-3.153,3.687,3.687,0,0,0-3.052-1.563A3.622,3.622,0,0,0,8.566,5.5,3.742,3.742,0,0,0,7.135,7.228C6.487,8.755,4.789,9.983,2.48,10.639a.658.658,0,0,0-.48.643.636.636,0,0,0,.812.6c2.7-.766,4.715-2.263,5.516-4.153A2.312,2.312,0,0,1,10.51,6.232,2.087,2.087,0,0,1,12.6,9.167c-1.528,3.612-5.11,5.937-9.156,6.958a.656.656,0,0,0-.462.846.635.635,0,0,0,.756.405,17.8,17.8,0,0,0,5.372-2.294,13.489,13.489,0,0,0,2.74-2.348ZM13.413,23.4a.616.616,0,0,0-.369-.6c-2.495-1.1-1.827-1.1-4.2,0a.613.613,0,0,0-.357.586A.666.666,0,0,0,9.18,24a.616.616,0,0,0,.26-.055c1.7-.792,1.109-.84,3.026,0a.614.614,0,0,0,.25.05.668.668,0,0,0,.7-.6ZM11.25,20.139a.75.75,0,0,0,.075-.628A.614.614,0,0,0,10.6,19.1a5.143,5.143,0,0,0-1.058.451,22.655,22.655,0,0,1-3.251,1.393.655.655,0,0,0-.426.472.637.637,0,0,0,.79.762,23.683,23.683,0,0,0,3.482-1.487C10.588,20.46,11.021,20.5,11.25,20.139ZM22,17.5A4.5,4.5,0,1,1,17.5,13,4.5,4.5,0,0,1,22,17.5Zm-3.086-2.122L17.5,16.792l-1.414-1.414-.707.708L16.793,17.5l-1.414,1.414.707.708L17.5,18.208l1.414,1.414.708-.708L18.208,17.5l1.414-1.414-.708-.708Z" transform="translate(-2)" fill="#ff4d2d"/>
                    </svg>
                        <%}
                        } %>--%>
											<%DataMessage message = Model.LastDataMessage;
												string battLevel = "";
												string battType = "";
												string battModifier = "";
												if (message != null)
												{
													if (message.Battery <= 0)
													{
														battLevel = "-0";
														battModifier = " batteryCritical batteryLow";
													}
													else if (message.Battery <= 10)
													{
														battLevel = "-1";
														battModifier = " batteryCritical batteryLow";
													}
													else if (message.Battery <= 25)
													{
														battLevel = "-2";
													}
													else if (message.Battery <= 50)
														battLevel = "-3";
													else if (message.Battery <= 75)
														battLevel = "-4";
													else
														battLevel = "-5";

													if (Model.PowerSourceID == 3 || message.Voltage == 0)
													{
														battType = "-line";
														battLevel = "";
													}
													else if (Model.PowerSourceID == 4)
													{
														battType = "-volt";
														battLevel = "";
													}
													else if (Model.PowerSourceID == 1 || Model.PowerSourceID == 14)
														battType = "-cc";
													else if (Model.PowerSourceID == 2 || Model.PowerSourceID == 8 || Model.PowerSourceID == 10 || Model.PowerSourceID == 13 || Model.PowerSourceID == 15 || Model.PowerSourceID == 17 || Model.PowerSourceID == 19)
														battType = "-aa";
													else if (Model.PowerSourceID == 6 || Model.PowerSourceID == 7 || Model.PowerSourceID == 9 || Model.PowerSourceID == 16 || Model.PowerSourceID == 18)
														battType = "-ss";
													else
														battType = "-gateway";
												}%>
											<div class="dfjcsbac">
												<div class="card-battery-icon col-xs-6 battIcon  icon icon-battery<%:battType %><%:battLevel %><%:battModifier %>" title="Battery: <%=message == null ? "" : message.Battery.ToString()  %>%, Voltage: <%=message == null ? "" : message.Voltage.ToStringSafe() %> V">


													<%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage) : "" %>
												</div>
												<%if (Model.isPaused())
													{ %>
												<div class="powertour-tooltip powertour-hook" id="hook-eight">
													<div title="Paused" class="col-xs-6 pendingEditIcon pausesvg" style="padding-top: 5px;">
														<svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 20 20">
															<path id="ic_pause_circle_filled_24px" d="M12,2A10,10,0,1,0,22,12,10,10,0,0,0,12,2ZM11,16H9V8h2Zm4,0H13V8h2Z" transform="translate(-2 -2)" />
														</svg>
													</div>
												</div>
												<%} %>
												<%if (!Model.CanUpdate)
													{ %>
												<div class="powertour-tooltip powertour-hook" id="hook-eight">
													<div title="Settings Update Pending" class="col-xs-6 pendingEditIcon pendingsvg" style="padding-top: 5px">
														<%=Html.GetThemedSVG("Pending_Update") %>
													</div>
												</div>
												<%} %>
											</div>
										</div>
									</div>
								</div>
							</div>


							<div>
								<div id="second_element_to_target">
									<div class="divCellCenter holder holder<%:Model.Status.ToString() %>">
										<div class="eventIcon_container">
											<div class="sensor eventIcon eventIconStatus sensorIcon sensorStatus<%:Model.Status.ToString() %>">
											</div>
											<%=Html.GetThemedSVG("app" + Model.ApplicationID) %>
										</div>
									</div>
								</div>
								<div>
									<div class="powertour-hook" id="hook-four">
										<div class="glance-text" id="third_element_to_target">
											<%if (Model.LastDataMessage != null)
												{ %>
											<div class="glance-reading" style="padding-left: 5px;"><%= Model.LastDataMessage.DisplayData%></div>
											<%} %>
										</div>
									</div>
								</div>
							</div>
							<div style="margin-top: 1em">
								<div class="dfac" style="background-color: white; flex-wrap: wrap;">
									<div class="glance-lastDate" style="margin-right: 5px;"><%: Html.TranslateTag("Last Message","Last Message") %> : <%: Model.LastDataMessage == null ?   Html.TranslateTag("Unavailable","Unavailable") : (Model.LastCommunicationDate.ToElapsedMinutes() > 120 ? Model.LastCommunicationDate.OVToLocalDateTimeShort() : (Model.LastCommunicationDate.ToElapsedMinutes()  < 0 ? Html.TranslateTag("Unavailable","Unavailable") :  Model.LastCommunicationDate.ToElapsedMinutes().ToString() + " " + Html.TranslateTag("Minutes ago","Minutes ago")) ) %></div>
									<%: Html.TranslateTag("Overview/SensorHome|Next Check-In","Next Check-in") %>:
                                    <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.NextCommunicationDate > DateTime.UtcNow)
										{
											if (Model.NextCommunicationDate != DateTime.MinValue)
											{ %>
									<span style="margin-right: 5px;" class="title2"><%: Model.NextCommunicationDate.OVToLocalDateShort()%></span>
									<span class="title2"><%: Model.NextCommunicationDate.OVToLocalTimeShort()%></span>
									<%}
										else
										{%>  <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable") %></span> <%}
																																   }
																																   else
																																   { %>
									<span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable") %></span>
									<% } %>
								</div>
							</div>
						</div>
					</div>
				</div>
			</div>
		</div>
		<%if (!MonnitSession.CurrentCustomer.Account.HideData)
			{ %>
		<div class="col-lg-6 col-md-12 col-xs-12 device_detailsRow__card <%:Request.Path.StartsWith("/Overview/SensorChart") ? " " : "disp-none" %> detailsReadings_card">
			<div class="x_panel device_detailsRow__card__inner">
				<div class="x_title x_card_title">
					<i class="fa fa-list-ul main-page-icon-color"></i>
					&nbsp;
                    <%: Html.TranslateTag("Readings", "Readings")%>
				</div>
				<div class="x_body verticalScroll" style="max-height: 160px; overflow-y: scroll;">
					<%if (Request.Path.StartsWith("/Overview/SensorChart"))
                        {%>

					<%Html.RenderPartial("SensorHistoryListSmall", Model); %>

					<%} %>
				</div>
			</div>
		</div>
		<%} %>
	</div>
</div>

<%--    <div class="col-md-4 col-xs-4 device_detailsRow__card <%:Request.Path.StartsWith("/Overview/SensorChart") ? " ":"disp-none" %>">
        <div class="x_panel device_detailsRow__card__inner">
            <div class="x_title x_card_title">
                Actions
            </div>
            <div class="x_body">
            </div>
        </div>
    </div>--%>
<script>
	$('.btn-secondaryToggle').hover(
		function () { $(this).addClass('active-hover-fill') },
		function () { $(this).removeClass('active-hover-fill') }

	)
</script>
