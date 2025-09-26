<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    Monnit.Gateway gateway = Monnit.Gateway.LoadBySensorID(Model.SensorID);
%>
<div class="x-content">
    <form class="form-horizontal form-label-left" action="/Overview/InterfaceEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
        <%: Html.ValidationSummary(false)%>
        <input type="hidden" value="/Overview/InterfaceEdit/<%:Model.SensorID %>" name="returns" id="returns" />

        <%Html.RenderPartial("~/Views/Gateway/GatewayEdit/Default/_WillCallExpiration.ascx", gateway); %>
        
        <div class="formBody mobileNoMargin">
            <div id="tabs">
                <div class="col-12">
                    <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link active"
                                id="pills-snmp-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-snmp"
                                type="button"
                                role="tab"
                                aria-controls="pills-snmp"
                                aria-selected="false">
                                <%: Html.TranslateTag("SNMP","SNMP")%>
                            </button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link"
                                id="pills-modbus-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-modbus"
                                type="button"
                                role="tab"
                                aria-controls="pills-modbus"
                                aria-selected="false">
                                <%: Html.TranslateTag("Modbus","Modbus")%>
                            </button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link"
                                id="pills-sntp-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-sntp"
                                type="button"
                                role="tab"
                                aria-controls="pills-sntp"
                                aria-selected="false">
                                <%: Html.TranslateTag("SNTP","SNTP")%>
                            </button>
                        </li>
                        <li class="nav-item" role="presentation">
                            <button
                                class="nav-link"
                                id="pills-http-tab"
                                data-bs-toggle="pill"
                                data-bs-target="#pills-http"
                                type="button"
                                role="tab"
                                aria-controls="pills-http"
                                aria-selected="false">
                                <%: Html.TranslateTag("HTTP","HTTP")%>
                            </button>
                        </li>
                    </ul>
                    <div class="tab-content ms-1" id="pills-tabContent">
                        <div class="tab-pane fade show active" id="pills-snmp" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate SNMP Interface","Activate SNMP Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.SNMPInterface1Active ? "checked" : "" %> id="snmp" name="SNMPInterface1Active">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                    <%: Html.ValidationMessageFor(g => gateway.SNMPInterface1Active)%>
                                </div>
                            </div>
                            <div class="snmp">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceSNMP.ascx", gateway); %>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="pills-modbus" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate Modbus Interface","Activate Modbus Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.ModbusInterfaceActive ? "checked" : "" %> id="modbus" name="ModbusInterfaceActive">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                    <%: Html.ValidationMessageFor(g => gateway.ModbusInterfaceActive)%>
                                </div>
                            </div>

                            <div class="modbus">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceMB.ascx", gateway); %>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="pills-sntp" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate SNTP Interface","Activate SNTP Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.NTPInterfaceActive ? "checked" : "" %> id="sntp" name="NTPInterfaceActive">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                </div>
                                <%: Html.ValidationMessageFor(g => gateway.NTPInterfaceActive)%>
                            </div>
                            <div class="sntp">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceNTP.ascx", gateway); %>
                            </div>
                        </div>
                        <div class="tab-pane fade show" id="pills-http" role="tabpanel" aria-labelledby="pills-snmp-tab">
                            <div class="row sensorEditForm">
                                <div class="col-12 col-md-3">
                                    <strong><%: Html.TranslateTag("Gateway/_Interface|Activate Http Interface","Activate Http Interface")%></strong>
                                </div>
                                <div class="col sensorEditFormInput">
                                    <div class="form-check form-switch d-flex align-items-center ps-0">
                                        <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                                        <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=gateway.HTTPInterfaceActive ? "checked" : "" %> id="http" name="HTTPInterfaceActive">
                                        <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                                    </div>
                                    <%: Html.ValidationMessageFor(g => gateway.HTTPInterfaceActive)%>
                                </div>
                            </div>

                            <div class="http">
                                <% Html.RenderPartial("~/Views/Gateway/GatewayEdit/type_035/_GatewayInterfaceHTTP.ascx", gateway); %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12 text-end">
                <button type="button" onclick="$(this).hide();$('#saving').show();postForm($('#simpleEdit_<%:Model.SensorID %>'));" value="<%: Html.TranslateTag("Save", "Save")%>" class="btn btn-primary">Save</button>
                <button class="btn btn-primary" id="saving" style="display:none;" type="button" disabled >
                    <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true" ></span>
                    Loading...
                </button>
            </div>
        </div>
    </form>
</div>

<script type="text/javascript">


        if ($('#snmp').is(":checked"))
            $('.snmp').show();
        else
            $('.snmp').hide();

        if ($('#modbus').is(":checked"))
            $('.modbus').show();
        else
            $('.modbus').hide();

        if ($('#sntp').is(":checked"))
            $('.sntp').show();
        else
            $('.sntp').hide();

        if ($('#http').is(":checked"))
            $('.http').show();
        else
            $('.http').hide();

        $('.form-check-input').click(function () {
            let id = $(this).attr('id');
            $(`#${id}`).is(":checked") ? $(`.${id}`).show() : $(`.${id}`).hide()
        })
</script>
