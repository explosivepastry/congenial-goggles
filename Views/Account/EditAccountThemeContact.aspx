<%@ Page  MasterPageFile="~/Views/Shared/Administration.Master" Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<AccountThemeContact>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit Contact
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
<form id="formtosave" action="/Account/SaveAccountThemeContact" method="post">
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <div>
        
        <%: Html.Hidden("AccountThemeContactID", Model.AccountThemeContactID) %>
        <%: Html.Hidden("accountThemeID", Model.AccountThemeID) %>
        
        <div class="formbody" >
            <table width="100%">
                  <tr>
                    <td width="20" ></td>
                    <td width="45"></td>
                    <td width="150"></td>
                    <td width="20"></td>
                    <td width="20"></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td><label for="firstName">First Name</label></td>
                    <td>
                        <%: Html.TextBoxFor(m => m.FirstName) %>
                        <%: Html.ValidationMessageFor(m => m.FirstName) %>
                    </td>
                    <td></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td><label for="lastName">Last Name</label></td>
                    <td>
                        <%: Html.TextBoxFor(m => m.LastName) %>
                        <%: Html.ValidationMessageFor(m => m.LastName) %>
                    </td>
                    <td></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td><label for="email">Email</label></td>
                    <td >           
                        <%: Html.TextBoxFor(m => m.Email) %>
                        <%: Html.ValidationMessageFor(m => m.Email) %>
                    </td>
                    <td></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td><label for="other">Other</label></td>
                    <td>
                        <%: Html.TextBoxFor(m => m.Other) %>
                        <%: Html.ValidationMessageFor(m => m.Other) %>
                    </td>
                    <td></td>
                    <td width="20"></td>
                </tr>
            </table>

            <div class="buttons">
                <div><button type="submit" id="save" class="bluebutton" style="margin: -3px 18px 2px 0px;">Save</button></div>
                <div><a href="/Admin/AccountTheme/<%=Model.AccountThemeID%>" class="greybutton" style="margin: -3px 18px 2px 0px;">Cancel</a></div>
                <div style="clear:both"></div>
            </div>
        </div>
    </div>
</form>
</asp:Content>