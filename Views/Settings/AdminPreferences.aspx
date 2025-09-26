<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<AccountThemePreferenceTypeLink>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ThemePreferences
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%AccountTheme acctTheme = AccountTheme.Load(ViewBag.AccountThemeID);%>
        <%Account account = Account.Load(acctTheme.AccountID); %>
        <% Html.RenderPartial("_WhiteLabelLink", acctTheme); %>

        <div class="">
            <div class="dfac" style="width: 100%; display: flex; align-items: baseline;">
                <%=Html.GetThemedSVG("preferences") %>
                <h2 class="ms-2">
                    <%: Html.TranslateTag("Settings/AdminPreferences|Account Theme Preferences","Account Theme Preferences")%>
                </h2>
                <p style="margin-left: 10px; color: var(--help-highlight-color);"><%: Html.TranslateTag("*These settings will only default to newly created users") %></p>
            </div>
        </div>
        <form id="prefForm" method="post">
            <div class="x_content">
                <%foreach (PreferenceType pref in PreferenceType.LoadAll())
                    {
                        AccountThemePreferenceTypeLink link = Model.Where(l => l.PreferenceTypeID == pref.PreferenceTypeID).FirstOrDefault();
                        List<PreferenceTypeOption> prefOption = PreferenceTypeOption.LoadByPreferenceTypeID(pref.PreferenceTypeID);

                        if (prefOption.Count == 0)
                        {
                            continue;
                        }


                        if (link == null)
                        {
                            link = new AccountThemePreferenceTypeLink();
                            link.AccountCanOverride = false;
                            link.CustomerCanOverride = false;
                            link.DefaultValue = pref.DefaultValue;
                        }

                        
                        if (pref.PreferenceTypeID == 15)
                        {
                            if (link.DefaultValue == "1" || link.DefaultValue == "2")
                            {
                                link.DefaultValue = "3";
                            }
                            if (link.DefaultValue == "6")
                            {
                                link.DefaultValue = "5";
                            }
                        }
                %>

                <div class="x_panel shadow-sm rounded mb-4">
                    <div class="form-group">
                        <div class="x_title col-12">
                            <div class="card_container__top__title">
                                <%=pref.Name%>
                            </div>
                        </div>
                        <div class="x_content">
                            <div class="bold col-lg-2 col-md-3 col-sm-5 col-12 aSettings__title">
                                <%: Html.TranslateTag("Settings/AdminPreferences|Set Default Value","Set Default Value")%>
                            </div>
                            <div class="col-lg-2 col-md-4 col-sm-7 col-12">
                                <select class="form-select w-75 w-sm-25 w-md-50 w-lg-25" name="<%=pref.Name + "_DefaultVal"%>">
                                    <% foreach (PreferenceTypeOption option in prefOption)

                                        {
                                            if (pref.PreferenceTypeID == 15)
                                            {
                                                if (option.Value == "1" || option.Value == "2" || option.Value == "6")
                                                {
                                                    continue;
                                                }
                                            }
                                    %>
                                    <option value="<%=option.Value%>" <%= option.Value == link.DefaultValue ? "selected='selected'" : ""%>><%=option.Name%></option>
                                    <%} %>

                                    <%--<%Request.Form["option.Name"]%>
                                             
                                         <%} %>--%>
                                </select>
                            </div>
                        </div>

                        <div class="x_content dfac">
                            <div class="bold col-lg-2 col-md-3 col-sm-5 col-11 aSettings__title" style="padding-top: 10px;">
                                <%: Html.TranslateTag("Settings/AdminPreferences|Account Can Override","Account Can Override")%>
                            </div>
                            <div class="col-lg-2 col-md-4 col-sm-7 col-1">
                                <input type="checkbox" class="checkbox checkbox-info" name="<%=pref.Name + "_AccountCan"%>" <%= link.AccountCanOverride == true ? "Checked='Checked'" : ""%> />
                            </div>
                        </div>

                        <div class="x_content dfac">
                            <div class="bold col-lg-2 col-md-3 col-sm-5 col-11 aSettings__title">
                                <%: Html.TranslateTag("Settings/AdminPreferences|Customer Can Override","Customer Can Override")%>
                            </div>
                            <div class="col-lg-2 col-md-4 col-sm-7 col-1">
                                <input type="checkbox" class="checkbox checkbox-info" name="<%=pref.Name + "_CustomerCan"%>" <%= link.CustomerCanOverride == true ? "Checked='Checked'" : ""%> />
                            </div>
                        </div>
                    </div>
                </div>

                <%}%>
            </div>
            <div class="clearfix"></div>
            <div style="padding-bottom: 15px; margin-left: 20px;">
            </div>

            <div class="text-end" id="submitdiv">
                <a href="/Settings/AdminSearch/" class="btn btn-light me-2"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                <button id="prefSave" type="button" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary">
                    <%: Html.TranslateTag("Save","Save")%>
                </button>
                <div style="clear: both;"></div>
            </div>
            <div class="clearfix"></div>
            <br />
        </form>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#prefSave').click(function (e) {
                $.post('/Settings/EditThemePreference/<%=ViewBag.accountThemeID%>', $("#prefForm").serialize(), function (data) {

                    toastBuilder(data);
                    //alert(data);
                    //$('#submitdiv').append(data);
                });
            });
        });
    </script>
    <style>
        #svg_preferences {
            fill: var(--options-icon-color);
        }
    </style>

</asp:Content>
