<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Unsubscribe to Notifications
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="display: flex; height: 100vh;">

        <div style="background: white; width: 100%; display: flex; align-items: center; justify-content:center;">
            <div class="" id="fullForm" style="background: white; padding: 2rem; max-width: 800px">
                <div style="font-size: 2rem;" class="card_container__top__title"><%: Html.TranslateTag("Unsubscribe to Notifications","Unsubscribe to Notifications")%></div>
                <form action="/Overview/NotificationOptOut" method="post">
                    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
                    <div class="formBody" style="padding: 1rem 0;">
                        <div>
                            <h3 class="rule-title"><%: Html.TranslateTag("Overview/OptOut|By choosing to unsubscribe, notifications will no longer be sent to","By choosing to unsubscribe, notifications will no longer be sent to")%>:</h3>
                            <input type="text" class="form-control user-dets" placeholder="Email..." name="address" value="<%:Request["address"] %> " />
                        </div>
                        <div>
                            <p style="margin: 1rem 0;" class="subsub-text"><%: Html.TranslateTag("Overview/OptOut|We do not want to send email to those that do not want to receive it. Please let us know why you no longer want to receive notifications.","We do not want to send email to those that do not want to receive it.  Please let us know why you no longer want to receive notifications")%>.</p>
                            <input class="checkbox-style" type="checkbox" id="frequency" name="frequency" value="<%: Html.TranslateTag("Overview/OptOut|I do not like the frequency of the emails","I do not like the frequency of the emails")%>." />
                            <%: Html.TranslateTag("Overview/OptOut|I do not like the frequency of the emails","I do not like the frequency of the emails")%>.<br />
                            <br />
                            <input class="checkbox-style" type="checkbox" id="nowork" name="nowork" value="<%: Html.TranslateTag("Overview/OptOut|I do not work at the company, so I no longer need to receive emails","I do not work at the company, so I no longer need to receive emails")%>." />
                            <%: Html.TranslateTag("Overview/OptOut|I do not work at the company, so I no longer need to receive emails","I do not work at the company, so I no longer need to receive emails")%>.<br />
                            <br />
                            <input class="checkbox-style" type="checkbox" id="text" name="text" value="<%: Html.TranslateTag("Overview/OptOut|I already receive text message and no longer need emails","I already receive text message and no longer need emails")%>." />
                            <%: Html.TranslateTag("Overview/OptOut|I already receive text message and no longer need emails","I already receive text message and no longer need emails")%>.<br />
                            <br />
                            <input class="checkbox-style" type="checkbox" id="noproduct" name="noproduct" value="<%: Html.TranslateTag("Overview/OptOut|I no longer use my sensors","I no longer use my sensors")%>." />
                            <%: Html.TranslateTag("Overview/OptOut|I no longer use my sensors","I no longer use my sensors")%>.<br />
                            <br />
                            <input class="checkbox-style" type="checkbox" id="other" name="other" value="<%: Html.TranslateTag("Other","Other")%>" />
                            <%: Html.TranslateTag("Other","Other")%><br />
                            <br />
                            <%: Html.TextArea("otherText") %>
                        </div>
                    </div>
                    <div style="display: flex; justify-content: center;">
                        <input style="width: 200px;" id="unsub" type="submit" value="<%: Html.TranslateTag("Unsubscribe","Unsubscribe")%>" class="btn btn-primary" />
                    </div>
                </form>
            </div>
        </div>

        <div class="login_image hideOnSmAB" style="width: 100%">
            <img src="../../Content/images/login-dashPhone.png" style="width: 100%;" />
        </div>

    </div>

    <style type="text/css">
        textarea {
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #212529;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            appearance: none;
            border-radius: 0.25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .hideOnSmAB {
            display: flex;
        }

        @media screen and (max-width:1000px) {
            .hideOnSmAB {
                display: none;
            }
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#otherText').hide();

            $('#other').change(function () {

                if ($('#other').is(':checked')) {
                    $('#otherText').show();
                    $('#otherText').width('100%');
                    $('#otherText').height('150px');
                }
                else {
                    $('#otherText').hide();
                    $('#otherText').val('');
                }
            });
        });
    </script>

</asp:Content>
