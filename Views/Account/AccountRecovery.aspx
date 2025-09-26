<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<Account>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AccountRecovery
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>AccountRecovery</h2>

<% Customer customer = ViewBag.Customer;
   string err = ViewBag.errMessage; %>

    <div class="container-fluid">
        <div class="row">
            <div class="col-xs-6 col-sm-6 col-md-6 panel panel-primary panel-body" style="border: 0px; width: 300px; height: 475px; margin-top: 0 auto; position: absolute; left: 50%; top: 50%; margin-left: -150px; margin-top: -225px; padding: 0px;">
                <br />
                <div class="col-12" style="border: 0px;">


                    <div id="Form" class="container">
                        <%if (MonnitSession.CurrentBinaryStyle("Logo") != null && MonnitSession.CurrentBinaryStyle("Logo").Length > 0){%>
                        <img align="middle" style="display: block; margin-left: auto; margin-right: auto;" width="225px" height="auto" src="/Overview/Logo" />
                        <%}else{%>
                        <img align="middle" style="display: block; margin-left: auto; margin-right: auto;" width="225px" height="auto" src="<%= Html.GetThemedContent("/images/Logo_TopBar.png")%>" />
                        <%} %>
                        <hr style="width: 40%; margin-bottom: 5px;">

                        <div id="Recovery" class="row">
                            <div class="col-xs-1 col-sm-1 col-md-2"></div>
                            <div id="loginForm" class="col-xs-10 col-sm-10 col-md-8">
                                <h1 style="font-size: 20px; font-weight: bold; text-align: center;"><%: Html.TranslateTag("Account/LogOnOV|Verification Code","Verification Code")%></h1>

                                <p><%: Html.TranslateTag("A verfication code has been sent to the Primary Contact Email and the Account Recovery Email. If you do not have access to those emails, please contact your account administrator to retreive the verification code.","A verfication code has been sent to the Primary Contact Email and the Account Recovery Email. If you do not have access to those emails, please contact your account administrator to retreive the verification code.")%></p>

                                <div class="form-group" style="text-align: center;">
                                    <input id="Code" class="form-control form-control-lg" name="Code" type="text" required>
                                </div>
                                <input type="button" id="submitBtn" value="<%: Html.TranslateTag("Submit","Submit")%>" class="btn btn-primary btn-block" style="margin-top: 10px" />
                                <a id="cancelBtn" href="/Account/LogOnOV/" class="btn btn-secondary btn-block" style="margin-top: 10px"><%: Html.TranslateTag("Cancel","Cancel")%><a/>

                                <div style="margin-top: 20px; text-align: center;">
                                    <a class="btn btn-default btn-block" onClick="window.location.reload()"><%: Html.TranslateTag("Resend Code Request","Resend Code Request")%></a>
                                </div>

                                <div id="Results" style="margin-top: 20px;color:red;">
                                    <%=err %>
                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


<script type="text/javascript">
    $(function () {
        $("#Code").focus();

        $("#Code").keyup(function (event) {
            if (event.keyCode === 13) {
                $("#submitBtn").click();
            }
        });

        $("#submitBtn").click(function () {
            var code = $("#Code").val();
            var customerID = "<%: customer.CustomerID %>";

            $.post("/Account/AccountRecovery", { code : code, customerID : customerID }, function (data) {
                if (!data.includes("Error:")) {
                    $("#Recovery").html(data);
                } else {
                    $("#Results").html('<span style="color:red">' + data + '</span>')
                    $("#Code").focus();
                }
            
            
            });
        });

    });

</script>



</asp:Content>
