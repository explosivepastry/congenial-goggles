<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.MaintenanceWindow>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Maintenance Window
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="fullForm" style="width: 100%;">
        <div class="formtitle">Maintenance Window</div>


        <form action="/Admin/EditMaintenanceWindow" method="post">
       
        <%: Html.ValidationSummary(true) %>
        <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
        <div class="formBody">


            <%: Html.HiddenFor(model => model.MaintenanceWindowID) %>

            <div>
                Display from
            <div style="display: inline-block;">
                <input id="DisplayDate" name="DisplayDate" value="<%: Model.DisplayDate.ToShortDateString() %>" class="datepicker" style="width: 80px;" /><br />
                <%: Html.ValidationMessageFor(model => model.DisplayDate) %>
            </div>

                Until
            <div style="display: inline-block;">
                <input id="HideDate" name="HideDate" value="<%: Model.HideDate.ToShortDateString() %>" class="datepicker" style="width: 80px;" /><br />
                <%: Html.ValidationMessageFor(model => model.HideDate) %>
            </div>
            </div>
            
            <div class="editor-label">
                <select id="SeverityLevel" name="SeverityLevel" class="form-select" >
                    <%foreach (eSeverityLevel eLevel in Enum.GetValues(typeof(eSeverityLevel)))
                      { %>
                        <option value="<%=eLevel%>" <%= Model.SeverityLevel == eLevel ? "selected='selected'" : "" %>>
                            <%= Html.TranslateTag("Settings/EditMaintenanceWindow|" + eLevel, eLevel.ToString()) %>
                        </option>
                    <%} %>
                </select>                
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.SeverityLevel) %>
            </div>

            <div class="editor-label">
                <textarea id="editor"><%:Model.Description %></textarea>
                <%: Html.HiddenFor(model => model.Description)%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.Description) %>
            </div>


            <div class="editor-label">
                <textarea id="emailBody"><%:Model.EmailBody %></textarea>
                <%: Html.HiddenFor(model => model.EmailBody)%>
            </div>
            <div class="editor-error">
                <%: Html.ValidationMessageFor(model => model.EmailBody) %>
            </div>
            
        </div>


        <div class="buttons">
            <a type="button" href="/Admin/MaintenanceWindows" class="greybutton">Cancel</a>
            <input type="submit" value="Save" class="bluebutton" />
            <div style="clear: both;"></div>
        </div>
        </form>
    </div>

    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>

    
    <script type="text/javascript">
        $(document).ready(function () {

            $('form').submit(function (e) {
                $('#Description').val(sunObjEditor.getContents());
                $('#EmailBody').val(sunEmailObjEditor.getContents());
            });
            $('#editor').before("<span>Description (Default 8000 Characters): </span>");
            $('#emailBody').before("<span>Email Body (Default 8000 Characters): </span>");
            $('#smsEditor').html("<span>SMS Description (Default 120 Characters): </span>");
            
            $(".datepicker").datepicker();

            $('.sf-with-ul').removeClass('currentPage');
            $('#MenuMaint').addClass('currentPage');

            sunObjEditor = createSunEditor('editor');
            sunEmailObjEditor = createSunEditor('emailBody');
        });

        var sunObjEditor;
        var sunEmailObjEditor;
        
    </script>

</asp:Content>
