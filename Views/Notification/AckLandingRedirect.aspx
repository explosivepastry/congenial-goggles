<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.NotificationRecorded>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NotificationAcknowledgePage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">Acknowledge Notification</div>
        <table>
            <tr>
                <td></td>
                <td>
                    <h2>For security purposes, You must be logged in as the user this notification was sent to. Redirecting you to your account's acknowledgement page.</h2>
                </td>
                <td></td>
            </tr>
        </table>
    </div>


    <script type="text/javascript">

        window.setTimeout(function () {
            window.location.href = '/Notification';
        }, 11000);
    </script>
</asp:Content>
