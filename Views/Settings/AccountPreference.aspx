<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<PreferenceType>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AccountPreference
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Account account = (Account)ViewBag.Account;%>
        <%Html.RenderPartial("AccountLink", account); %>
        <%-- <div class="col-12">--%>
        <div class="col-12" style="width: 99%; margin-top: 15px;">
            <div class="top-row-btn-left">
                <a class="btn btn-primary" href="/Settings/AccountEdit/<%=account.AccountID %>">
                    <svg xmlns="http://www.w3.org/2000/svg" width="23.318" height="16" viewBox="0 0 23.318 16" class="desktop_mr15">
                        <g id="Symbol_86" data-name="Symbol 86" transform="translate(16 16) rotate(180)">
                            <path id="Path_10" data-name="Path 10" d="M8,0,6.545,1.455l5.506,5.506H-7.318V9.039h19.37L6.545,14.545,8,16l8-8Z" fill="#fff" class="mobile-icon-dark" />
                        </g>
                    </svg>
                    <span class="media_desktop"><%: Html.TranslateTag("Settings/AccountPreference|Settings","Settings")%></span>
                </a>
            </div>
        </div>
        <%-- </div>--%>

        <form method="post" action="/Settings/AccountPreference/<%=account.AccountID %>" class="form-horizontal form-label-left">

            <div class="ol-sm-6 col-12 powertour-hook" id="hook-one">

                <div class="x_panel shadow-sm rounded mt-2">
                    <%-- <div class="x_title form-group ">--%>
                    <div class="card_container__top__title" style="position: sticky;">
                        <%: Html.TranslateTag("Settings/AccountPreference|Account Preferences")%>
                        <div style="display: flex; align-items: center; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small; position: sticky; margin-left: 60px;">
                            [<%= account.AccountNumber%>] 
                            <p style="color: red; margin-left: 10px;"><%: Html.TranslateTag("*These settings will only default to newly created users") %></p>
                        </div>
                    </div>

                    <%-- </div>--%>

                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <% foreach (var item in Model)
                        {
                            string val = "";
                            Preference pref = Preference.LoadByPreferenceTypeIDandAccountID(item.PreferenceTypeID, account.AccountID);  //new

                            if (pref != null)
                            {
                                val = pref.Value;
                                if (item.PreferenceTypeID == 15)
                                {
                                    if (val == "1" || val == "2")
                                    {
                                        val = "3";
                                    }
                                    if (val == "6")
                                    {
                                        val = "5";
                                    }
                                }
                            }%>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3" style="font-weight: bold; margin-top: 5px; margin-bottom: 10px;">
                            <%:Html.TranslateTag("Settings/AccountPreference|" + item.DisplayName,item.DisplayName) %>
                        </div>
                        <div class="col sensorEditFormInput">
                            <%-- <div id="" class="tz">--%>
                            <select id="<%:item.Name%>" name="<%:item.Name %>" class="form-select" style="width: 250px;">
                                <%foreach (PreferenceTypeOption opt in PreferenceTypeOption.LoadByPreferenceTypeID(item.PreferenceTypeID))
                                    {
                                        if (item.PreferenceTypeID == 15)
                                        {
                                            if (opt.Value == "1" || opt.Value == "2" || opt.Value == "6")
                                            {
                                                continue;
                                            }
                                        }
                                %>
                                <option value="<%=opt.Value == null ? item.DefaultValue : opt.Value %>" <%= opt.Value == val ?"selected='selected'":"" %>><%= Html.TranslateTag("Settings/AccountPreference|" + opt.Name,opt.Name) %></option>
                                <%} %>
                            </select>
                        </div>
                    </div>
                    <div style="clear: both;"></div>

                    <%} %>

                    <div class="bold text-end">
                        <div id="prefMessage" style="color: green; margin-right: 8px">
                        </div>
                        <button type="submit" value="<%: Html.TranslateTag("Settings/AccountPreference|Save","Save") %>" onclick="$(this).hide();$('#saving').show();" class="btn btn-primary">
                            <%--;"postForm($('#preMessage_<%Model. %>--%>
                            <%: Html.TranslateTag("Settings/AccountPreference|Save","Save") %>
                        </button>
                        <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%: Html.TranslateTag("Settings/AccountPreference|Saving...","Saving...")%>
                        </button>
                    </div>
                </div>
            </div>
        </form>
    </div>

    <script type="text/javascript">

        $(document).ready(function (event) {

            toastBuilder("<%=ViewBag.prefMessage%>");
        });

        function goBack() {
            window.history.back();
        }
    </script>

</asp:Content>
