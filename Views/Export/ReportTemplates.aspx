<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<ReportQuery>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report Templates
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
	<div class="rule-card_container w-100">
        <div class="card_container__top">
            <div class="card_container__top__title">
                <%: Html.TranslateTag("Export/ReportTemplates|Choose Report Template","Choose Report Template") %>
            </div>
        </div>		
    	<div class="card_container__body">  
		    <div class="card_container__body__content">
                <div class="row">
                
                    <%foreach (ReportQuery Report in Model){
                        
                        if (!string.IsNullOrEmpty(Report.Tags))
                        {
                            bool Matches = false;
                            string[] Tags = Report.Tags.Split('|');
                            foreach (string Tag in Tags)
                            {
                                if (MonnitSession.CurrentCustomer.Account.Tags.Contains(Tag))
                                {
                                    Matches = true;
                                    break;
                                }
                            }
                            if (!Matches) continue;                
                        }%>

                        <a class="col-12 py-2" href="/Export/CreateNewReport?queryID=<%:Report.ReportQueryID %>"> 
                            <div class="d-flex align-items-center">
				                <div class="col-md-11 col-9">
									<div class="glance-name"><%:Report.Name%></div>
									<div class="glance-reading"><%: Html.TranslateTag("Description","Description") %>: <%:Report.Description %></div>
								</div>

								<div class="col-md-1 col-3 text-end">
									<%=Html.GetThemedSVG("page") %>
								</div>
                            </div>
                        </a>

                    <hr class="mb-0" />
                    <%}%>
			    </div>
            </div>
        </div>
    </div>
 </div>
		
	<div class="buttons" style="margin-top: 10px; height: 15px;"></div>

    <style type="text/css">
        a:hover {
            background-color: #eee;
            border-radius:5px;
      /*      margin: 5px 0; */
        }
    </style>

</asp:Content>