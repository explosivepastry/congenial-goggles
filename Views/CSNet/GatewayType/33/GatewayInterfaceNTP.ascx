<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>



<div class="formBody">
    
    <div class="editor-label">
        Server IP Address
    </div>
    <div class="editor-field">
        <%: Html.TextBox("NTPServerIP", Model.NTPServerIP)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.NTPServerIP)%>
    </div>

    <div class="editor-label">
        Server Update Interval (Minutes)
    </div>
    <div class="editor-field">
        <%: Html.TextBoxFor(model => model.NTPMinSampleRate)%>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.NTPMinSampleRate)%>
    </div>


</div>



