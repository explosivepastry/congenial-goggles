<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<SMSCarrier>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit SMS Carriers List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%AccountTheme acctTheme = MonnitSession.CurrentTheme; %>
        <%Html.RenderPartial("_WhiteLabelLink", acctTheme);%>

        <div class="x_panel shadow-sm rounded mt-4">
            <div class="card_container__top__title">
                <%: Html.TranslateTag("Settings/AdminSMSCarrierList|Edit SMS Carriers List","Edit SMS Carriers List")%>
                <div class="clearfix"></div>
            </div>
            <form id="prefForm" class="mx-auto" method="post">

                <div class="x_content">
                    <input type="hidden" name="currentThemeAccountID" id="currentThemeAccountID" value="<%: MonnitSession.CurrentTheme.AccountThemeID %>" />

                    <div class="bold" style="float: left; font-size: small;"><%: Html.TranslateTag("Settings/AdminSMSCarrierList|(Selecting none will default to show all)","(Selecting none will default to show all)")%></div>
                    <div class="clearfix"></div>
                    <br />
                    <div class="x_content col-12 list ms-4">
                        <% 
                            var appList = SMSCarrier.LoadAll().Where(accttheme => { return accttheme.AccountThemeID == long.MinValue || accttheme.AccountThemeID == MonnitSession.CurrentTheme.AccountThemeID; }).OrderBy(ma => ma.SMSCarrierName);
                            foreach (var item in Model)
                            {
                                bool check = appList.Where(m => m.SMSCarrierID == item.SMSCarrierID).Count() > 0;
                        %>
                        <div class="d-flex text-center">
                            <span class="col-8 text-start">
                                <label id="item_<%: item.SMSCarrierID%>" style="font-weight: normal !important;" for="SMSCarrierID_<%: item.SMSCarrierID %>"><%:item.SMSCarrierName %></label>
                            </span>
                            <span class="col-4 alignleft">
                                <input type="checkbox" id="SMSCarrierID_<%: item.SMSCarrierID %>" name="SMSCarrierID_<%: item.SMSCarrierID %>" value="<%:item.SMSCarrierID %>" <%: check ? "checked" : ""  %> />
                            </span>
                            <span class="col-lg-2 col-md-1 col-2"></span>
                        </div>
                        <%} %>
                    </div>
                </div>
            </form>
            <div class="text-end" id="submitdiv">
                <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                    <%: Html.TranslateTag("Save","Save")%>
                </button>
                <div style="clear: both;"></div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

            $('#prefSave').click(function (e) {
                $.post('/Settings/AdminSMSCarriersList/<%=MonnitSession.CurrentTheme.AccountThemeID%>', $("#prefForm").serialize(), function (data) {
                    window.location.reload();
                });
            });

        });

    </script>

    <style>
        @media only screen and (max-width: 600px) {
            .list {
                column-count: 1;
            }
        }

        @media only screen and (min-width: 600px) and (max-width: 1000px) {
            .list {
                column-count: 2;
            }
        }

        @media only screen and (min-width: 1000px) {
            .list {
                column-count: 4;
            }
        }
    </style>

</asp:Content>
