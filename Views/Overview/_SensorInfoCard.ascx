<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>


<%-------------------------------------
                Sensor Info Card
    ------------------------------%>
<div class=" col-12 col-lg-6" style="display: flex; height: inherit; padding-right:0 !important">
    <div class="rule-card_container marginLeftOnLgScreen w-100" style="height: inherit; margin-top: 0;">
        <div class="card_container__top ">
            <div class="card_container__top__title docTitle">
                <div style="display: flex; align-items: baseline; width: 100%; justify-content: space-between;">
                    <div class="hidden-xs">
                        <%: Html.TranslateTag("Sensor Info", "Sensor Info")%>
                    </div>
                    <div style="font-size: 14px; font-weight: 400;"><%= Model.ApplicationName%></div>
                </div>
            </div>
        </div>


        <div class="Gate-details" style="font-size: 1em; height: 100%;">

            <div class="pump d-flex">
                <div class="titleGate"><strong><%:Html.TranslateTag("Sensor ID/Code") %> :</strong></div>
                <div><% Html.RenderPartial("~/Views/Shared/DeviceIDAndCheckCode.ascx", Model.SensorID); %></div>
            </div>
            <div class="pump d-flex">

                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|Firmware", "Firmware")%> :</strong>  </div>
                <div><%: Model.FirmwareVersion %></div>
            </div>

            <%if (MonnitSession.CurrentTheme.Theme == "Default")
                { %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|RadioBand", "RadioBand")%> : </strong></div>
                <div><%=Model.RadioBand %></div>

            </div>

            <%if (!string.IsNullOrEmpty(Model.SKU))
                { %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|SKU", "SKU")%> :</strong>  </div>
                <div><%: Model.SKU.ToUpper() %></div>
            </div>
            <%} %>

            <%} %>

            <%if (MonnitSession.CurrentTheme.Theme != "Default")
                { %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|Generation", "Generation")%> :</strong>  </div>
                <div><%: Model.GenerationType %></div>
            </div>
            <%} %>

            <%if (Model.IsCableEnabled && Model.CableID >= 0)
                { %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|Cable ID", "Cable ID")%> :</strong>  </div>
                <div><%: Model.CableID %></div>
            </div>
            <%} %>

            <%Gateway gateway = Gateway.LoadBySensorID(Model.SensorID); %>

            <%if (Model.SensorTypeID == 8)
                {
            %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|MAC Address", "MAC Address")%> :</strong>  </div>
                <div><%: gateway.MacAddress %></div>
            </div>
            <%} %>

            <%if (Model.SensorTypeID == 8 && gateway.IsUnlocked)
                {
            %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|Server Address", "Server Address")%> :</strong>  </div>
                <div><%: gateway.ServerHostAddress %></div>
            </div>
            <%} %>


            <%--Bluetooth MacAddress--%>    
            <%-- <%if (Model.SensorTypeID == 8)
                {
            %>
            <div class="pump d-flex">
                <div class="titleGate"><strong><%: Html.TranslateTag("Sensor/Edit|Bluetooth MAC Address", "Bluetooth MAC Address")%> :</strong>  </div>
                <div><%:  %></div>
            </div>
            <%} %>--%>


        </div>

    </div>
</div>

