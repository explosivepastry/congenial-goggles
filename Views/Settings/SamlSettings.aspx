<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<SamlEndpoint>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
<%: Html.TranslateTag("AccountPreference","AccountPreference")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <%Account account = (Account)ViewBag.Account;%>
        <%Html.RenderPartial("AccountLink", account); %>
        <div class="col-12" <%--style="width: 99%; margin-top: 15px;"--%>>
            <div class="top-row-btn-left">
                <a class="btn btn-primary" href="/Settings/AccountEdit/<%=account.AccountID %>">
                    <svg xmlns="http://www.w3.org/2000/svg" width="23.318" height="16" viewBox="0 0 23.318 16" class="desktop_mr15">
                        <g id="Symbol_86" data-name="Symbol 86" transform="translate(16 16) rotate(180)">
                            <path id="Path_10" data-name="Path 10" d="M8,0,6.545,1.455l5.506,5.506H-7.318V9.039h19.37L6.545,14.545,8,16l8-8Z" fill="#fff" class="mobile-icon-dark" />
                        </g>
                    </svg>
                    <span class="media_desktop"><%: Html.TranslateTag("Settings/AccountEdit|Settings","Settings")%></span>
                </a>
            </div>
        </div>

        <div class="col-8 powertour-hook" id="hook-one">
            <div class="x_panel shadow-sm rounded mt-2">
                <form method="post" action="/Settings/SamlSettings/" id="SamlEndpointForm" class="form-horizontal form-label-left">
                    <div class="card_container__top__title" style="position: sticky;" >
                        <%: Html.TranslateTag("Settings/SamlSettings|SAML Settings","SAML Settings")%>
                        <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size :small; position: sticky;  margin-left: 60px;">
                            [<%= account.AccountNumber%>] 
                        </div>
                   </div>

                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <div class="row sensorEditForm">
                        <div class="col-3" style="font-weight: bold; margin-top: 5px; margin-bottom: 10px;">
                             <%:Html.TranslateTag("Settings/SamlSettings|Endpoint Name", "Endpoint Name")%>
                        </div>
                        <div class="col-9 sensorEditFormInput">
                            <input type="text" id="SamlEndpointName" name="Name" value="<%:Model.Name %>" style="width:100%;"/>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    
                    <div class="row sensorEditForm">
                        <div class="col-3" style="font-weight: bold; margin-top: 5px; margin-bottom: 10px;">
                             <%:Html.TranslateTag("Settings/SamlSettings|Endpoint URL", "Endpoint URL")%>
                        </div>
                        <div class="col-9 sensorEditFormInput">
                            <input type="text" id="SamlEndpointUrl" name="EndpointURL" value="<%:Model.EndpointURL %>" style="width:100%;"/>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                    
                    <div class="row sensorEditForm">
                        <div class="col-3" style="font-weight: bold; margin-top: 5px; margin-bottom: 10px;">
                             <%:Html.TranslateTag("Settings/SamlSettings|X509Certificate Value", "X509Certificate Value")%>
                        </div>
                        <div class="col sensorEditFormInput">
                            <textarea rows="10" id="SamlEndpointCertificate" name="Certificate" style="width: 100%;"><%=Model.Certificate %></textarea>
                        </div>
                    </div>
                    <div style="clear: both;"></div>

                    <div class="bold text-end">
                        <%if (Model != null && Model.SamlEndpointID > 0)
                          {%>
                            <button type="button" id="samlTestBtn" class="btn btn-secondary"><%: Html.TranslateTag("Settings/SamlSettings|Test","Test")%> </button>
                            <button type="button" id="deleteSamlEndpoint" class="btn btn-danger"><%: Html.TranslateTag("Settings/SamlSettings|Delete","Delete")%></button>
                        <%}%>

                        <button type="submit" value="<%: Html.TranslateTag("Settings/SamlSettings|Save","Save") %>" id="saveBtn" onclick="$(this).hide();$('#saving').show();" class="btn btn-primary">
                            <%: Html.TranslateTag("Settings/SamlSettings|Save","Save") %>
                        </button>
                        <button class="btn btn-primary" id="saving" style="display: none;" type="button" disabled>
                            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                           <%: Html.TranslateTag("Settings/SamlSettings|Saving...","Saving...")%>
                        </button>

                        <%if (ViewBag.ShowTestMsg != null && ViewBag.ShowTestMsg)
                          {%>
                            <div>
                                <b class="text-danger"><%: Html.TranslateTag("We recommend using the Test button before logging out to verify that your SAML settings have been set properly.","We recommend using the Test button before logging out to verify that your SAML settings have been set properly. Otherwise you will have to contact support to have your account return to the normal login process!")%></b>
                            </div>
                        <%} %>
                        
                        <h3 id="SamlEndpointSaveMsg">
                            <%if (!string.IsNullOrWhiteSpace(ViewBag.SamlTestMessage))
                              {%>
                                <%: Html.TranslateTag("Test Message:","TestMessage:")%>
                                <br />
                                <%:ViewBag.SamlTestMessage %>
                            <%} %>
                        </h3>
                    </div>
                    
                </form>
            </div>
        </div>

        <%List<Customer> customers = ViewBag.CustomerList;
        if (Model != null && Model.SamlEndpointID > 0)
        {%>
            <div class="col-8">
            <div class="x_panel shadow-sm rounded mt-2">
                <div class="card_container__top__title" style="position: sticky;" >
                    <%: Html.TranslateTag("Settings/SamlSettings|User List","User List")%>
                    <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size :small; position: sticky;  margin-left: 60px;">
                        [<%= account.AccountNumber%>] 
                    </div>
                </div>
                <div class="card_container__top__title" style="position: sticky; white-space:unset;" >
                    <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size :small; position: sticky;  margin-left: 60px;">
                        <%: Html.TranslateTag("Settings/SamlThe following is a list of users, whose portal username is in square brackets '[]'.","The following is a list of users, whose portal username is in square brackets '[]'.")%>
                        <br /><%: Html.TranslateTag("Settings/SamlThe input next to it is for you to associate the NameID from your IDP to its portal user.","The input next to it is for you to associate the NameID from your IDP to its portal user.  So that when we receive your SAML Auth Response, we know who to have logged in.")%>
                    </div>
                </div>
                
                <div class="row sensorEditForm border-bottom fw-bold">
                    <div class="col-3">
                        <%: Html.TranslateTag("Settings/SamlCustomer Name [Username]","Customer Name [Username]")%>
                    </div>
                    <div class="col-8 sensorEditFormInput">
                        <%: Html.TranslateTag("Settings/SamlSAML Name ID","SAML Name ID")%>   
                    </div>
                </div>

                <%foreach (Customer customer in customers)
                  {%>
                    <div class="row sensorEditForm">
                        <div class="col-3" style="font-weight: bold; margin-top: 5px; margin-bottom: 10px;">
                            <%:customer.FullName + " [" + customer.UserName + "]" %>
                        </div>
                        <div class="col-8 sensorEditFormInput">
                            <input type="text" id="customerSamlName_<%:customer.CustomerID %>" value="<%:customer.SamlNameID %>" style="width:100%;"/>
                        </div>
                        <div class="col-1">
                            <button type="button" class="samlNameSave btn btn-primary" data-id="<%:customer.CustomerID %>">Save</button>
                        </div>

                        <div class="col-12">
                            <h4 id="samlNameSaveResult_<%:customer.CustomerID %>"></h4>
                        </div>
                    </div>
                    <div style="clear: both;"></div>
                <%} %>
            </div>
        </div>
      <%} %>
    </div>

    <script type="text/javascript">
        function goBack() {
            window.history.back();
        }

        $(function () {

            $('.samlNameSave').mouseover(function () {
                $(this).parent().parent().css('cursor', 'pointer').css('background-color', 'lightgray');
            })
            .mouseout(function () {
                $(this).parent().parent().css('cursor', 'pointer').css('background-color', '');
            });

            $('#SamlEndpointForm').submit(function (e) {
                e.preventDefault();

                var name = $('#SamlEndpointName').val();
                var url = $('#SamlEndpointUrl').val();
                var cert = $('#SamlEndpointCertificate').val();

                if (name.length == 0 && (url.length > 0 || cert.length > 0)) {
                    $('#SamlEndpointName').select().focus();
                    $('#SamlEndpointSaveMsg').html("All fields must be filled, otherwise leave empty.");
                }
                else if (url.length == 0 && (name.length > 0 || cert.length > 0)) {
                    $('#SamlEndpointUrl').select().focus();
                    $('#SamlEndpointSaveMsg').html("All fields must be filled, otherwise leave empty.");
                }
                else if (cert.length == 0 && (name.length > 0 || url.length > 0)) {
                    $('#SamlEndpointCertificate').select().focus();
                    $('#SamlEndpointSaveMsg').html("All fields must be filled, otherwise leave empty.");
                }
                else {
                    $('#SamlEndpointSaveMsg').css("color", "#515356");

                    $.post("/Settings/SamlSettings/", { accountID: '<%=account.AccountID%>', name: name, url: url, certificate: cert }, function (data) {
                    if (data.includes("Success")) {
                        $('#SamlEndpointSaveMsg').css('color', 'green');
                        $('#saveBtn').show();
                        $('#saving').hide();

                        location.href = '/Settings/SamlSettings/<%=account.AccountID%>/?showTestMsg=1';
                    } else {
                        $('#SamlEndpointSaveMsg').css('color', 'red');
                    }
                    $('#SamlEndpointSaveMsg').html(data);
                });
            }
        });

            $('#deleteSamlEndpoint').click(function () {
                var accountID = '<%:account.AccountID%>';
                var endpointID = '<%:Model.SamlEndpointID%>';

                let values = {};
                values.url = "/Settings/RemoveAccountEndpoint/?id=" + endpointID + "&accountID=" + accountID;
                values.text = "Are you sure you want to remove this endpoint?";
                values.partialTag = $('#SamlEndpointSaveMsg');
                values.redirect = '/Settings/AccountEdit/' + accountID;

                openConfirm(values);

                <%--if (confirm("Are you sure you want to remove this endpoint?")) {

                    $.post('/Settings/RemoveAccountEndpoint/', { id: endpointID, accountID: accountID }, function (data) {
                        //alert(data);
                        $('#SamlEndpointSaveMsg').html(data);

                        if (data == 'Success') {
                            location.href = '/Settings/AccountEdit/<%:Model.AccountID%>'
                        }
                    });
                }--%>
            });

            $('.samlNameSave').click(function () {

                var id = $(this).attr('data-id');
                var samlName = $('#customerSamlName_' + id).val();

                $.post('/Settings/UpdateCustomerSamlNameID', { customerID: id, samlNameID: samlName }, function (data) {
                    var cssColor = data == "Success" ? "green" : "red";

                    $('#samlNameSaveResult_' + id).css("color", cssColor).html(data);
                });
            });

            $('#samlTestBtn').click(function (e) {
                e.preventDefault();

                var id = '<%:account.AccountID%>';
                var url = $('#SamlEndpointUrl').val();

                var obj = $(this);
                var oldHtml = $(this).html();
                $(this).html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
                $('#SamlEndpointSaveMsg').css("color", "#515356");

                $.post('/Settings/SamlLoginTest/', { endpointUrl: url, accountID: id }, function (data) {
                    if (data == 'Failed') {
                        $('#SamlEndpointSaveMsg').css("color", "red").html(data);
                        obj.html(oldHtml);
                    } else {
                        window.location.href = data;
                    }
                });
            });
        });

    </script>
    
</asp:Content>