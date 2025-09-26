<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<StoreLoginModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    LoginToStore
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="card_container shadow-sm rounded mt-4">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <div class="dfac">
                        <%--<span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="18" height="16" viewBox="0 0 18 16" style="margin-right: 5px;">
                            <path id="ic_store_mall_directory_24px" d="M20,4H4V6H20Zm1,10V12L20,7H4L3,12v2H4v6H14V14h4v6h2V14Zm-9,4H6V14h6Z" transform="translate(-3 -4)" style="fill: #0067ab;" />
                        </svg>
                    </span>--%>
                        <span style="margin-bottom: 3px; max-width: 100%;"><%: Html.TranslateTag("Retail/LoginToStore|Configure Payment Profile","Configure Payment Profile")%>
                        </span>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-12" style="padding-left: 10px;">
                    <div class="col-md-6 col-12 mt-4" style="padding-left: 10px;">
                        <p><%: Html.TranslateTag("Retail/LoginToStore|It looks like this is the first time you're checking out.  Start by creating a payment profile by entering your email below.","It looks like this is the first time you're checking out.  Start by creating a payment profile by entering your email below.")%></p>
                    </div>

                    <br />
                    <br />
                    <span style="font-size: 1.8em;"><%=Html.ValidationSummary()%></span>
                </div>

                <div class="col-12" style="padding-left: 10px;">
                    <div class="" style="margin-left: 20px; border-left: thin; border-left-color: darkgray;">
                        <span style="font-weight: bold;"><%: Html.TranslateTag("Retail/LoginToStore|Email Address","Email Address")%></span>
                        <div class="input-group">
                            <input class="form-control" style="background-color: white; max-width: 300px;" required type="text" name="storeEmail" id="storeEmail" value="<%=Model.EmailAddress %>" />
                            <input class="btn btn-primary" type="button" value="<%: Html.TranslateTag("Retail/LoginToStore|Send Verification Code","Send Verification Code")%>" id="createButton" />
                            <button class="btn btn-primary" id="createButtonPressed" style="display: none;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                <%: Html.TranslateTag("Retail/LoginToStore|Sending...","Sending...")%>
                            </button>
                        </div>
                        <div id="errMessage"></div>
                    </div>
                </div>
                <div class="col-12" style="padding-left: 10px;">
                    <div class="col-md-6 col-12 mt-4" style="padding-left: 10px;">
                        <p><b><%: Html.TranslateTag("Retail/LoginToStore|Note:","Note")%>:</b><%: Html.TranslateTag("Retail/LoginToStore|If you already have an existing payment profile, your email will link it automatically.","If you already have an existing payment profile, your email will link it automatically")%>. </p>
                    </div>
                    <br />
                </div>
            </div>
            <hr />

            <div class="row">
                <div class="col-12" style="padding-left: 10px;">
                    <div class="col-md-6 col-12" style="padding-left: 10px;">
                        <p>
                            <%: Html.TranslateTag("Retail/LoginToStore|To finalize your payment profile, enter your first and last name along with the Verification Code from the email sent to the address above.","To finalize your payment profile, enter your first and last name along with the Verification Code from the email sent to the address above")%>.
                        </p>
                    </div>
                </div>

                <div class="col-12" style="padding-left: 10px;">
                    <div class="" style="margin-left: 20px; border-left: thin; border-left-color: darkgray;">
                        <span style="font-weight: bold;"><%: Html.TranslateTag("Retail/LoginToStore|First Name","First Name")%></span>
                        <div class="input-group">
                            <input class="form-control" style="background-color: white; max-width: 300px;" required type="text" name="FirstName" id="FirstName" value="<%=MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer.FirstName : "" %>" />
                        </div>
                    </div>
                    <div class="" style="margin-left: 20px; border-left: thin; border-left-color: darkgray;">
                        <span style="font-weight: bold;"><%: Html.TranslateTag("Retail/LoginToStore|Last Name","Last Name")%></span>
                        <div class="input-group">
                            <input class="form-control" style="background-color: white; max-width: 300px;" required type="text" name="LastName" id="LastName" value="<%=MonnitSession.CurrentCustomer != null ? MonnitSession.CurrentCustomer.LastName : "" %>" />
                        </div>
                    </div>

                    <div class="" style="margin-left: 20px; border-left: thin; border-left-color: darkgray;">
                        <span style="font-weight: bold;"><%: Html.TranslateTag("Retail/LoginToStore|Verification Code","Verification Code")%></span>
                        <div class="input-group">
                            <input class="form-control" style="background-color: white; max-width: 300px;" required type="text" name="linkCode" id="linkCode" value="" />
                            <input class="btn btn-primary" type="button" value="<%: Html.TranslateTag("Submit","Submit")%>" id="createStoreLink" />
                            <button class="btn btn-primary" id="createStoreLinkPressed" style="display: none;" type="button" disabled>
                                <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
                                <%: Html.TranslateTag("Retail/LoginToStore|Linking...","Linking...")%>
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            <%string returns = (string)ViewBag.returnURL;
                if (!string.IsNullOrEmpty(returns))
                { %>
            <input type="hidden" name="returnURL" value="/Retail/Checkout/<%=Model.account.AccountID%>?productType=Subscriptions" />
            <%} %>
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#createButton').click(function (e) {
                e.preventDefault();
                var Emall = $('#storeEmail').val();
                $('#createButtonPressed').show();
                $('#createButton').hide();
                $("#errMessage").html('');

                if (isEmail(Emall)) {
                    $.post('/Retail/SendLinkCode/<%=Model.account.AccountID%>', { email: Emall }, function (data) {

                        if (data == "Success") {
                            toastBuilder("<%: Html.TranslateTag("Retail/LoginToStore|Email Sent","Email Sent")%>", "Success");
<%--                            $('#errMessage').html("<%: Html.TranslateTag("Retail/LoginToStore|Email Sent","Email Sent")%>");--%>
                            $('#waitDiv').html("");
                        }
                        else {
                            $('#waitDiv').html("");
                            $('#errMessage').html(data);
                        }
                        $('#createButtonPressed').hide();
                        $('#createButton').show();
                    });


                } else {
                    setTimeout(function () {
<%--                        $("#errMessage").html('<%: Html.TranslateTag("Retail/LoginToStore|Invalid Email Address")%>');--%>
                        toastBuilder('<%: Html.TranslateTag("Retail/LoginToStore|Invalid Email Address")%>')
                        $('#createButtonPressed').hide();
                        $('#createButton').show();
                    }, 500);

                }
            });

            $('#createStoreLink').click(function (e) {

                e.preventDefault();
                $('#createStoreLinkPressed').show();
                $('#createStoreLink').hide();

                <%string returnUrlAfterAdd = (string)ViewBag.ReturnURLAfterAdd;%>

                var KeyCode = $('#linkCode').val();
                var FirstName = $('#FirstName').val();
                var LastName = $('#LastName').val();

                if (!KeyCode || !FirstName || !LastName) {
                    $('#createStoreLinkPressed').hide();
                    $('#createStoreLink').show();
                    toastBuilder("You must complete all fields");
                };

                if (KeyCode.length > 0) {
                    $.post('/Retail/CheckLinkCode/<%=Model.account.AccountID%>', { keyCode: KeyCode, firstName: FirstName, lastName: LastName, returnUrl: '<%=returns%>' }, function (data) {

                        if (data == "Success") {
                            toastBuilder("Success");
                            <%if (string.IsNullOrWhiteSpace(returnUrlAfterAdd))
        {%>
                            window.location.href = "/Retail/PaymentOption/<%=Model.account.AccountID%>?returnUrl=<%=returns %>"
                            <%}
        else
        {%>
                            window.location.href = "/Retail/PaymentOption/<%=Model.account.AccountID%>?returnURLAfterAdd=<%=(string)ViewBag.ReturnURLAfterAdd%>"
                            <%}%>
                        }
                        else {
                            toastBuilder(data);
                        }
                        $('#createStoreLinkPressed').hide();
                        $('#createStoreLink').show();

                    });
                }
            });
        });

        function isEmail(email) {
            var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            return regex.test(email);
        }

    </script>

</asp:Content>
