<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<PreferenceType>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Preferences
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Customer cust = (Customer)ViewBag.customer; %>
        <%Html.RenderPartial("UserLink", cust); %>

        <form action="/Settings/UserPreference/<%=cust.CustomerID %>" method="post" style="width: 100%;">
            <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
            <div class="col-12" style="display: flex; justify-content: center;">
                <div style="padding: 2rem !important;" class="x_panel shadow-sm rounded adjustable-card-AB">
                    <div class="card_container__top">
                        <div class="card_container__top__title" style="position: sticky;">
                            <%: Html.TranslateTag("Settings/UserPreference|User Preferences","User Preferences")%>
                            <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small; position: sticky; margin-left: 30px;">
                                [<%= cust.FirstName%> <%= cust.LastName%>] - <%=cust.UserName%>
                            </div>
                        </div>
                        <a class="helpIco" style="cursor: pointer!important;" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Details") %>" data-bs-target=".pageHelp">
                            <div class="help-hover"><%=Html.GetThemedSVG("circleQuestion") %></div>
                        </a>
                    </div>

                    <!-- pageHelp button modal -->
                    <div class="modal fade pageHelp" style="z-index: 2000!important;" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="pageHelp"><%: Html.TranslateTag("Overview/SensorHome|Sensor Edit Settings","User Preferences")%></h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="word-choice">
                                            Maintenance Pop Up Level
                                        </div>
                                        <div class="word-def">
                                            <div style="margin-top: -0.25rem; margin-bottom: 0.75rem;">
                                                Customize how often you receive maintenance pop-up notifications.
                                            </div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Never: </span>No notifications for maintenance activities will be shown.</div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Seldom: </span>Only be notified for maintenance activities where system downtime is expected to occur.</div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Often: </span>(Default setting) Also be notified for maintenance activities where system degradation or downtime is possible.</div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Always: </span>Receive all maintenance activity notifications including routine maintenance where no system degradation is anticipated.</div>
                                            <div class="lil-m-on-x"><i>Note: This configuration only addresses maintenance notifications.  Notifications configured in Rules will not be affected by this setting.</i></div>
                                            <hr>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="word-choice">
                                            Maintenance Notification Level
                                        </div>
                                        <div class="word-def">
                                            <div style="margin-top: -0.25rem; margin-bottom: 0.75rem;">
                                                Customize how often you receive maintenance email notifications.
                                            </div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Never: </span>No notifications for maintenance activities will be shown.</div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Seldom: </span>Only be notified for maintenance activities where system downtime is expected to occur.</div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Often: </span>(Default setting) Also be notified for maintenance activities where system degradation or downtime is possible.</div>
                                            <div class="lil-m-on-x"><span class="word-subheader">Always: </span>Receive all maintenance activity notifications including routine maintenance where no system degradation is anticipated.</div>
                                            <div class="lil-m-on-x"><i>Note: This configuration only addresses maintenance notifications. Notifications configured in Rules will not be affected by this setting.</i></div>
                                            <hr>
                                        </div>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="x_content powertour-hook" id="hook-one" style="background-color: white">
                        <% foreach (var item in Model)
                            {
                                string val = "";
                                Preference pref = Preference.LoadByPreferenceTypeIDandCustomerID(item.PreferenceTypeID, cust.CustomerID);

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
                                }

                                else
                                {
                                    //Check the account preference/default
                                    Preference secpref = Preference.LoadByPreferenceTypeIDandAccountID(item.PreferenceTypeID, cust.AccountID);
                                    if (secpref != null)
                                        val = secpref.Value;
                                    else
                                    {
                                        //If the account is null  then check the Theme preference default
                                        AccountTheme theme = AccountTheme.Find(cust.Account);
                                        AccountThemePreferenceTypeLink link = AccountThemePreferenceTypeLink.LoadByPreferenceTypeIDandAccountThemeID(item.PreferenceTypeID, theme.AccountThemeID);
                                        //if not link is nothing then val
                                        if (link != null)
                                            val = link.DefaultValue;
                                    }
                                }

                        %>
                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%:Html.TranslateTag("Settings/UserPreference|" + item.DisplayName,item.DisplayName) %>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select id="<%:item.Name %>" name="<%:item.Name %>" class="form-select" style="max-width: 1600px">
                                    <%foreach (PreferenceTypeOption opt in PreferenceTypeOption.LoadByPreferenceTypeID(item.PreferenceTypeID))
                                        {
                                            //if (item.PreferenceTypeID == 15)
                                            //{
                                            //    if (opt.Value == "1" || opt.Value == "2" || opt.Value == "6")
                                            //    {
                                            //        continue;
                                            //    }
                                            //}
                                    %>
                                    <option value="<%=opt.Value == null ? item.DefaultValue : opt.Value %>" <%= opt.Value == val ?"selected='selected'":"" %>><%= Html.TranslateTag("Settings/UserPreference|" + opt.Name,opt.Name) %></option>
                                    <%} %>
                                </select>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <%
                            }%>

                        <div class="row sensorEditForm">
                            <div class="col-12 col-md-3">
                                <%: Html.TranslateTag("Settings/UserPreference|Landing Page URL","Landing Page URL")%>
                            </div>
                            <div class="col sensorEditFormInput">
                                <select class="form-select" style="max-width: 1600px" disabled>
                                    <option value="<%= string.IsNullOrEmpty(cust.HomepageLink) ? "/Overview" : cust.HomepageLink %>"> <%= string.IsNullOrEmpty(cust.HomepageLink) ? "/Overview" : cust.HomepageLink %></option>
                                </select>
                                <input id="setdefaulthomepage" type="button" value="<%: Html.TranslateTag("Settings/UserPreference|Set to Default","Set to Default")%>" class="btn btn-secondary btn-sm" />
                            </div>
                        </div>

                        <div style="clear: both;" />
                    </div>


                    <div class="bold text-center" style="margin-top: 4rem; margin-bottom: 2rem;">
                        <button type="submit" style="width: 50%; max-width: 300px" value="<%: Html.TranslateTag("Save","Save") %>" onclick="$(this).hide();$('#saving').show();" class="btn btn-primary">
                            <%: Html.TranslateTag("Save","Save") %>
                        </button>

                        <button class="btn btn-primary" id="saving" style="display: none; width: 50%; max-width: 300px" type="button" disabled>
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                            <%: Html.TranslateTag("Saving","Saving")%>...
                        </button>
                    </div>
                </div>
            </div>
        </form>
    </div>

    <style type="text/css">
        .lil-m-on-x {
            margin: 0.75rem 0;
        }

        @media (max-width: 240px) {
            body {
                font-size: 0.75em;
            }
        }

        .adjustable-card-AB {
            max-width: 800px;
        }
    </style>


    <script type="text/javascript">

        $(document).ready(function () {

            $('#setdefaulthomepage').click(function () {
                SetDefaultHomepage()
            });

            function SetDefaultHomepage() {
                $.get('/Overview/SetHomepage?customerid=' + <%: cust.CustomerID%> + '&link=', function (data) {
                    window.location.reload();
                });
            }
        });

        if (`<%=ViewBag.prefMessage%>`.length > 1) {
            toastBuilder(`<%=ViewBag.prefMessage%>`);
        }

    </script>

</asp:Content>
