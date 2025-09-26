<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>

<% using (Html.BeginForm())
    {%>
<div id="fullForm" style="width: 100%;">

    <div class="formtitle">
        New User Information
            <a href="/Account/UserList/<%:ViewData["AccountID"]%>" class="greybutton cancelNewUser" style="margin: -3px 20px 0px 0px;">Cancel</a>
    </div>
    <div class="formBody">
        <%: Html.ValidationSummary(true) %>

        <%: Html.Hidden("AccountID", ViewData["AccountID"])%>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.UserName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.UserName) %>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.UserName) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Password) %>
        </div>
        <div class="editor-field">
            <input autocomplete="off" id="Password" name="Password" type="password" />
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.Password) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ConfirmPassword)%>
        </div>
        <div class="editor-field">
            <input autocomplete="off" id="ConfirmPassword" name="ConfirmPassword" type="password" />
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.ConfirmPassword)%>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.FirstName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.FirstName) %>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.FirstName) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.LastName) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.LastName) %>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.LastName) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.NotificationEmail) %>
        </div>
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.NotificationEmail) %>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.NotificationEmail) %>
        </div>

        <%--            <div class="editor-label">
                <%: Html.LabelFor(model => model.NotificationPhone) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.NotificationPhone) %>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.NotificationPhone) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.SMSCarrierID) %>
            </div>
            <div class="editor-field">
                <%: Html.DropDownList("UISMSCarrierID", (SelectList)ViewData["Carriers"], "Select One")%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.UISMSCarrierID)%>
            </div>--%>



        <div class="editor-label">
            Is Administrator
        </div>
        <div class="editor-field">
            <%: Html.CheckBoxFor(model => model.IsAdmin)%>
        </div>
        <div class="editor-error">
            <%: Html.ValidationMessageFor(model => model.IsAdmin)%>
        </div>

    </div>
    <div class="buttons">
        <input type="button" value="Save New User" class="bluebutton saveNewUser" />
        <div style="clear: both;"></div>
    </div>
</div>

<script type="text/javascript">
    $(function () {
        $('.cancelNewUser').click(function (e) {
            e.preventDefault()

            ajaxDiv('userList', $(this).attr("href"));
        });

        $('.saveNewUser').click(function (e) {
            e.preventDefault()

            ajaxPostDiv('userList');
        });
    });
</script>
<% } %>