<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Gateway>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Unlock
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-md-12 col-xs-12">
        <div class="x_panel">
            <div class="x_title">
                <h2><%: Html.TranslateTag("Gateway/Unlock|Gateway Unlock","Gateway Unlock")%></h2>
                <div class="nav navbar-right panel_toolbox">
                
                    <!-- help button -->
                <%--<a class="helpIco" data-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-target=".pageHelp"><img src="../../Content/images/iconmonstr-help-2-240 (1).png" style="height:18px;margin: 0px;margin-top:5px;margin-right: 5px;"></a>--%>
                </div>

            <div class="clearfix"></div>
            </div>

            <div class="x_content">
                <form action="/Gateway/Unlock" method="post">
                     <%:  Html.HiddenFor(gate=> gate.GatewayID)%>
                    <%: Html.ValidationSummary(true) %>
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>

                    <div class="form-group">
                        <div class="bold col-md-6 col-sm-6 col-xs-12">
                            <h2><%: Html.TranslateTag("Gateway/Unlock|Please enter your Gateway unlock key","Please enter your Gateway unlock key")%></h2>
                        </div>
                        <div class="col-md-6 col-sm-6 col-xs-12 mdBox">
                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="form-group">
                        <div class="bold col-md-3 col-sm-3 col-xs-12">
                            <%: Html.TranslateTag("Gateway/Unlock|Gateway Unlock Key","Gateway Unlock Key")%>:
                        </div>
                        <div class="col-md-9 col-sm-9 col-xs-12 ">
                            <input type="text" id="AuthKey" name="AuthKey" required="required" style="min-width:300px;" />

                        </div>
                        <div style="clear: both;"></div>
                    </div>

                    <div class="form-group">
                        <div class="bold col-md-5 col-sm-5 col-xs-12">
                        </div>
                        <div class="bold col-md-7 col-sm-7 col-xs-12">
                            <input type="submit" title="<%: Html.TranslateTag("Gateway/Unlock|Save","Save")%>" class="btn btn-primary" style="float: left;" />
                            <a class="btn btn-default" href="/sethost" style="float: left;"><%: Html.TranslateTag("Gateway/Unlock|Go Back","Go Back")%></a>
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
