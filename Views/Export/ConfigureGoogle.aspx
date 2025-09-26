<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ExternalDataSubscription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  ConfigureGoogle
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <div class="container">
		<div class="col-md-12 col-xs-12">
			<%Html.RenderPartial("_APILink", Model); %>
		</div>
	</div>

    <div class="row">
        <div class="col-xs-12">
            <div class="container">
                <div class="x_panel">
                    <div class="x_title">
                        <h2><%:Html.TranslateTag("Export/ConfigureGoogle|Configure Google Cloud","Configure Google Cloud")%></h2>
                        <div class="clearfix"></div>
                    </div>

                    <div class="x_content">
                        <form id="Form1" action="/Export/ConfigureWatson" method="post" class="form-horizontal form-label-left">
                            <div class="form-group">
                                <div class="col-xs-12">
                                </div>
                                 <%:Html.TranslateTag("Export/ConfigureGoogle|Coming Soon!","Coming Soon")%>!
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="bold col-lg-4  col-xs-12">
                                </div>
                                <div class="col-lg-8 col-xs-12">
                                    <input type="text" value="" required name="" id="Text1" />
                                </div>
                                <div class="clearfix"></div>
                            </div>

                            <div class="form-group">
                                <div class="bold col-lg-4  col-xs-12">
                                </div>
                                <div class="col-lg-8 col-xs-12">
                                </div>

                                <div class="clearfix"></div>
                            </div>
                            <input type="hidden" value="<%=Model.ExternalDataSubscriptionID %>" name="externalDataSubscriptionID" />
                        </form>

                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
