<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<AssignObjectModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AssignDevice
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div class="col-lg-12 col-md-12 col-xs-12" style="display:flex;flex-direction:column;margin-top:20px;">
    <div class="col-lg-6 col-md-6 col-xs-12" id="newDevice">
    </div>

    <div class="row">
        <div class="col-md-6 col-sm-6 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2 style="background-color: white !important; overflow: unset;"><b><%: Html.TranslateTag("Network/AssignDevice|Add Device","Add Device")%></b>
                        <br />
                        <%if (Model.Networks.Count > 1)
                          { %>
                        <a style="cursor: pointer;" href="/network/NetworkSelect?accountID=<%=Model.AccountID %>&networkID=<%=Model.NetworkID %>">
                            <br />
                            <img src="../../Content/images/iconmonstr-connection-8-240.png" width="20px" height="auto" >&nbsp;&nbsp;<b><span style="font-size: 12px;"><%: Html.TranslateTag("Network/AssignDevice|Switch Network","Switch Network")%></span></b></a>
                        <%} %></h2>

                    <div class="nav navbar-right panel_toolbox">
                    </div>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content">

                    <%if (Request.IsSensorCertMobile())
                      { %>
                    <div>

                        <img id="" class="" alt="Scan" src="../../Content/images/flow.png?CacheRelease=1" style="width: 325px;">
                    </div>

                    <a href="/barcodescanner?ID=<%=Model.AccountID %>&networkID=<%=Model.NetworkID %>" id="scanbutton" class="btn btn-success"><%: Html.TranslateTag("Network/AssignDevice|Scan Barcode","Scan Barcode")%></a>

                    <h3>- <%: Html.TranslateTag("Network/AssignDevice|or","or")%> - </h3>

                    <%} %>


                    <div style="margin-bottom:10px;">
                        <b><%: Html.TranslateTag("Network/AssignDevice|Network","Network")%>:</b><span style="font-size: smaller;"> &nbsp;<%=CSNet.Load(Model.NetworkID).Name %></span>
                    </div>
                   

                    <form action="/Network/ManualSubmit">

                        <div class="form-group">
                            <label class="control-label " for="deviceID">
                                <%: Html.TranslateTag("Network/AssignDevice|Device ID","Device ID")%>
                            </label>

                            <div class="" id="hook-one">
                                <input class="aSettings__input_input" type="text" id="ObjectID" required="required" placeholder="<%: Html.TranslateTag("Network/AssignDevice|ID Number","ID Number")%>" name="deviceID" class="form-control col-md-7 col-xs-12">

                                <%: Html.ValidationMessageFor(model => model.ObjectID)%>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <label class="control-label" for="deviceID">
                                <%: Html.TranslateTag("Network/AssignDevice|Code","Code")%>
                            </label>
                            <div class=" powertour-hook" id="hook-two">
                                <input class="aSettings__input_input" name="checkCode" type="text" id="checkCode" placeholder="<%: Html.TranslateTag("Network/AssignDevice|Security Code","Security Code")%>" required="required" class="form-control col-md-7 col-xs-12">

                                <%: Html.ValidationMessageFor(model => model.Code)%>
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="form-group">
                            <div style="color: red !important;" class="bold col-md-6 col-sm-6 col-xs-12" id="messageDiv">
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-12">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <input type="hidden" id="accID" name="ID" value="<%=Model.AccountID %>" />

                        <input type="hidden" id="netID" name="networkID" value="<%=Model.NetworkID %>" />

                        <div class="clearfix"></div>

                        <div class="form-group">

                            <div class="">
                                <span class="powertour-hook" id="hook-three">
                                    <button type="submit" id="manualSubmit hook-three" class="btn gen-btn powertour-hook" style="background-color:#0067ab;margin-left:87px;"><%: Html.TranslateTag("Network/AssignDevice|Add Device","Add Device")%></button>
                                </span>
                            </div>
                        </div>
                    </form>
                

                    <%--<p style="text-align: right;"><%: Html.TranslateTag("Network/DeviceList|Finished adding devices?","Finished adding devices?")%></p>--%>
                    <div style="text-align: right;">
                        <div class="powertour-hook" id="hook-five" style="display:flex;justify-content:flex-end;">
                            <input style="height:28px; width:80px;" type="button" id="continueButton" class="btn gen-btn" value="<%: Html.TranslateTag("Network/DeviceList|Continue","Continue")%>" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


    <script type="text/javascript">

        function SplitIDCode() {
            var objID = $('#ObjectID').val();

            if (objID.indexOf(":") > -1) {
                //if : then remove : and everything after
                $('#ObjectID').val(objID.substr(0, objID.indexOf(":")));
                if ($('#checkCode').val().length == 0) {
                    //if Codebox is empty put everythign after : in code box (.toUpper()?)
                    $('#checkCode').val(objID.substr(objID.indexOf(":") + 1, objID.length - 1 - objID.indexOf(":")));
                }
            }
        }

        $(document).ready(function () {

            $('#ObjectID').change(function (e) {
                e.preventDefault();

                SplitIDCode();

                if (!isANumber($('#ObjectID').val())) {
                    $('#messageDiv').html("Device ID is a number");
                    $('#ObjectID').val("");
                }
            });

            $('#ObjectID').keyup(function (e) {
                if (e.keyCode == 13) {
                    SplitIDCode();
                }
            });

            var href = "/Network/showDeviceDetails";

            if (queryString("sensorID").length > 0) {
                var sid = queryString("sensorID");
                $.post(href, { ID: sid, type: "sensor" }, function (data) {
                    $('#newDevice').html(data);
                });
            }

            if (queryString("gatewayID").length > 0) {
                var gid = queryString("gatewayID");
                $.post(href, { ID: gid, type: "gateway" }, function (data) {

                    $('#newDevice').html(data);
                });
            }

            if (queryString("failed").length > 0) {

                $('#messageDiv').html(queryString("failed"));
            }


            $('#continueButton').click(function () {
                window.location.href = '/network/DeviceList/<%:Model.AccountID %>?networkID=<%:Model.NetworkID%>';
            });
        });

        function queryString(name) {
            name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]").replace(/</g, "&lt;").replace(/>/g, "&gt;");
            var regexS = "[\\?&]" + name + "=([^&#]*)";
            var regex = new RegExp(regexS);
            var results = regex.exec(window.location.href);
            if (results == null)
                return "";
            else
                return decodeURIComponent(results[1].replace(/\+/g, " "));
        }

    </script>

</asp:Content>
