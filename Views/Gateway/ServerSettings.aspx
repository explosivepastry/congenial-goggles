<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.AssignObjectModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ServerSettings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2> <%: Html.TranslateTag("Gateway/ServerSettings|Enter Device Information","Enter Device Information")%></h2>
                <div class="nav navbar-right panel_toolbox">
                    <!-- help button -->
                    <%--<a class="helpIco" data-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-target=".pageHelp"><img src="../../Content/images/iconmonstr-help-2-240 (1).png" style="height:18px;margin: 0px;margin-top:5px;margin-right: 5px;"></a>--%>
                </div>
                <div class="clearfix"></div>
            </div>
            <div class="x_content">

                <form action="/Gateway/ServerSettings" method="post">
                     <%: Html.ValidationSummary(true) %>
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <div id="fullForm">
  
                         <div class="form-group">
                                <div class="bold col-md-6 col-sm-6 col-xs-12">
                                    <h2><%: Html.TranslateTag("Gateway/ServerSettings| Please enter your Device ID and Checkcode."," Please enter your Device ID and Checkcode.")%></h2>
                                </div>
                                <div class="col-md-6 col-sm-6 col-xs-12 mdBox">
                          
                                </div>
                                 <div style="clear: both;"></div>
                            </div>

                           


                            <div class="form-group">
                                <div class="bold col-md-3 col-sm-3 col-xs-12">
                                    <%: Html.TranslateTag("Device ID","Device ID")%>
                                </div>
                                <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                                    <%: Html.TextBoxFor(model => model.ObjectID, new { name="gatewayID" })%>
                                    <%: Html.ValidationMessageFor(model => model.ObjectID)%>
                                     
                                </div>
                                 <div style="clear: both;"></div>
                            </div>



                            <div class="form-group">
                                <div class="bold col-md-3 col-sm-3 col-xs-12">
                                    <%: Html.TranslateTag("Gateway/ServerSettings|Device Code","Device Code")%>
                                </div>
                                <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                                    <%: Html.TextBoxFor(model => model.Code, new { name="gatewayCode" })%>
                                    <%: Html.ValidationMessageFor(model => model.Code)%>
                                    
                                </div>
                                 <div style="clear: both;"></div>
                            </div>


                        </div>


                        <!-- End Half Form -->

                        <%-- <img src="/Content/images/gateway-label.png" style="margin-left: -40px; margin-top: 20px; margin-bottom: 10px;" alt="Gateway ID and Security Code Location" title="Gateway ID and Security Code Location" />--%>




                        <div class="form-group">
                            <div class="bold col-md-5 col-sm-5 col-xs-12">
                            </div>
                            <div class="bold col-md-7 col-sm-7 col-xs-12">
                                <a href="/overview" class="btn btn-default">Cancel</a>
                                <input type="submit" value="Server Settings" class="btn btn-primary">
                                <div style="clear: both;"></div>
                            </div>
                        </div>
                        <!-- Close Buttons Div -->
                       </form>

                    </div>
                    <!-- Close Form -->
             

            </div>
        </div>
   



</asp:Content>
