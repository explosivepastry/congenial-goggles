<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    UploadDeviceCSV
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Assign Multiple Devices</h2>

    <div class="container">
        <div class="col-xl">



            <% 
                List<string> success = ViewBag.SuccessList;
                List<string> failed = ViewBag.FailedList;

            %>

            <% if (ViewBag.ErrorMessage != null)
                {%>
            <div>
                <h4><font color="red"><%: Html.TranslateTag("Network/UploadDeviceCSV|Failed Devices","Failed Devices")%></font></h4>
                <ul class="list-group">
                    <il class="list-group-item borderless"><font color="red"><b><%= ViewBag.ErrorMessage %></b></font></il>
                </ul>
            </div>
            <% }%>

            <% if (success.Count <= 0 && failed.Count <= 0)
                { %>
            <h3><%: Html.TranslateTag("Network/UploadDeviceCSV|No Devices were Added.","No Devices were Added.")%></h3>
            <%} %>

            <% if (success.Count > 0)
                {%>
            <div>
                <h3><font color="green"><%= success.Count %> <%: Html.TranslateTag("Network/UploadDeviceCSV|Devices Added Successfully","Devices Added Successfully")%></font></h3>
            </div>
            <%} %>



            <% if (failed.Count > 0 && ViewBag.ErrorMessage == null)
                {%>
            <div>
                <h4><font color="red"><%= failed.Count %> <%: Html.TranslateTag("Network/UploadDeviceCSV|Failed Devices","Failed Devices")%></font></h4>
                <ul class="list-group">
                    <% foreach (var item in ViewBag.FailedList)
                        { %>

                    <il class="list-group-item borderless"><font color="red"><b><%= item %></b></font></il>

                    <%} %>
                </ul>
            </div>
            <%} %>
            <br />
            <div>
                <div>
                    <input id="backBtn" class="btn btn-primary" type="button" value="<%: Html.TranslateTag("Network/UploadDeviceCSV|Add More Devices","Add More Devices")%>" />
                    <input id="continueBtn" class="btn btn-dark" type="button" value="<%: Html.TranslateTag("Network/UploadDeviceCSV|Finished Adding","Finished Adding")%>" />
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        $('#continueBtn').click(function () {
            window.location.href = '/network/DeviceList/<%:ViewBag.acctID %>?networkID=<%:ViewBag.networkID%>';
        });

        $('#backBtn').click(function () {
            window.location.href = '/network/NetworkSelect?AccountID=<%:ViewBag.acctID %>&networkID=<%:ViewBag.networkID%>';
        });

    </script>


</asp:Content>
