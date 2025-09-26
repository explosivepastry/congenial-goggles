<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<iMonnit.Models.UpdateEulas>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Updated Terms and Conditions
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="row centered-form">
            <div class="col-1 col-md-2"></div>
            <div class="col-10 col-md-8 panel panel-primary panel-body" style="border: 0px; width: 300px; height: auto; margin-top: 0 auto; position: absolute; left: 50%; top: 35%; margin-left: -150px; margin-top: -225px; padding: 0px;">
                <br />

                <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                <img style="display: block; margin-left: auto; margin-right: auto;" align="middle" src="/Overview/Logo" />
                <%}else{%>
                <img style="display: block; margin-left: auto; margin-right: auto; max-width:250px; max-height:100px;" align="middle" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                <%} %>

                <hr style="width: 100%; margin-bottom: 5px; margin-top: 20px;">
                <div class="col-12">
                    <div class="x_panel shadow-sm rounded">
                        <div class="x_title">
                            <h2><%: Html.TranslateTag("Overview/UpdateEULA|Updated Terms and Conditions","Updated Terms and Conditions")%></h2>
                            <div class="nav navbar-right panel_toolbox">
                            </div>
                            <div class="clearfix"></div>
                        </div>

                        <div class="x_content">
                            <form action="/Overview/UpdateEULA" method="post">
                                <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                                <div class="form-group">
                                    <div class="bold col-md-12 col-sm-12 col-12">
                                        <%: Html.TranslateTag("Overview/UpdateEula|I acknowledge that I have read","I acknowledge that I have read")%>
                                        <%: Html.TranslateTag("Overview/UpdateEula|and agree to the","and agree to the")%> <a style="text-decoration: underline;" href="/Overview/Legal" target="_blank"><%: Html.TranslateTag("Overview/UpdateEula|Terms and Conditions","Terms and Conditions")%></a>  
                                        <%: Html.CheckBoxFor(m => m.EULAUpdate,new {style = "vertical-align:sub;"})%> <%: Html.ValidationMessage("Required", new { style = "color:red;" })%>
                                    </div>
                                </div>

                                <div class="clear"></div>
                                <br />
                                <div class="editor-field">
                                </div>

                                <div>
                                    <input type="submit" class="btn btn-primary btn-sm" value="<%: Html.TranslateTag("Overview/UpdateEULA|Agree to Terms","Agree to Terms")%>" />
                                    <div style="clear: both;"></div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

