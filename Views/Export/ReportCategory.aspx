<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<Monnit.ReportType>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Report Category
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <div class="formtitle">
            <div id="MainTitle" style="display: none;"></div>
            <div class="top-add-btn-row media_desktop" style="padding-bottom: 0px;">
            </div>
            <div class="bottom-add-btn-row media_mobile">
            </div>
        </div>
        <!-- End Form Title -->

        <div class="clearfix"></div>
        <!-- Event List View -->
        <div class="rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <span class="col-6 dfac">
                        <%=Html.GetThemedSVG("book") %>
                        &nbsp;
                               
                    <%: Html.TranslateTag("Categories","Categories")%>
                    </span>
                    <div class="col-6" id="newRefresh" style="padding-left: 0px;">
                        <div style="float: right; margin-bottom: 5px;">
                            <div class="btn-group" style="height: 30px;">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" id="settings" style="display: none; padding: 5px 30px 15px 30px; border: 1px solid #dbdbdb;">
            </div>
            <div class=" d-flex flex-wrap">
                <%:Html.Partial("CategoryDetails",Model) %>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>

</asp:Content>





