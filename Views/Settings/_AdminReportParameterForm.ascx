<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportParameter>" %>
<!-- Hidden ReportQueryID-->
<input type="text" name="ReportQueryID" value="<%: Model.ReportQueryID %>" hidden />

<!-- Parameter Type -->
<div class="x_content col-md-12 col-sm-12 col-xs-12">
    <div class="bold">
        <%: Html.TranslateTag("Settings/_AdminReportParameterForm|Parameter Type","Parameter Type")%>
    </div>
    <div class=" col-md-3 col-sm-6 col-xs-6">
        <%string name = Html.TranslateTag("Name", "Name");%>
        <%string listHeader = Html.TranslateTag("Settings/_AdminReportParameterForm|Select One", "Select One");%>
        <%: Html.DropDownList("ReportParameterTypeID",new SelectList(ReportParameterType.LoadAll(),"ReportParameterTypeID",name ,Model.ReportParameterTypeID), listHeader) %>
        <%: Html.ValidationMessageFor(model => model.ReportParameterTypeID) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Parameter Name -->
<div class="x_content col-md-12 col-sm-12 col-xs-12">
    <div class="bold">
        <%: Html.TranslateTag("Settings/_AdminReportParameterForm|Parameter Name","Parameter Name")%>
    </div>
    <div class=" col-md-3 col-sm-6 col-xs-6">
        <input type="text" id="Text1" class="form-control aSettings__input_input" name="ParamName" value="<%= Model.ParamName %>" />
        <%: Html.ValidationMessageFor(model => model.ParamName) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Label Text -->
<div class="x_content col-md-12 col-sm-12 col-xs-12">
    <div class="bold">
        <%: Html.TranslateTag("Settings/_AdminReportParameterForm|Label Text","Label Text")%>
    </div>
    <div class=" col-md-3 col-sm-6 col-xs-6">
        <input type="text" id="LabelText" name="LabelText" class="form-control aSettings__input_input" value="<%= Model.LabelText %>" />
        <%: Html.ValidationMessageFor(model => model.LabelText) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Help Text -->
<div class="x_content col-md-12 col-sm-12 col-xs-12">
    <div class="bold">
        <%: Html.TranslateTag("Settings/_AdminReportParameterForm|Help Text","Help Text")%>
    </div>
    <div class=" col-md-3 col-sm-6 col-xs-6">
        <input type="text" id="HelpText" name="HelpText" class="form-control aSettings__input_input" value="<%= Model.HelpText %>" />
        <%: Html.ValidationMessageFor(model => model.HelpText) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Default Value -->
<div class="x_content col-md-12 col-sm-12 col-xs-12">
    <div class="bold">
        <%: Html.TranslateTag("Settings/_AdminReportParameterForm|Default Value","Default Value")%>
    </div>
    <div class=" col-md-3 col-sm-6 col-xs-6">
<%--        <%: Html.EditorFor(model => model.DefaultValue) %>--%>
        <input class="text-box single-line form-control aSettings__input_input" id="DefaultValue" name="DefaultValue" type="text" value="">
        <%: Html.ValidationMessageFor(model => model.DefaultValue) %>
    </div>
    <div class="clearfix"></div>
</div>
<!-- Help Text -->
<div class="x_content col-md-12 col-sm-12 col-xs-12">
    <div class="bold">
        <%: Html.TranslateTag("Settings/_AdminReportParameterForm|Predefined Values","Predefined Values")%>
    </div>
    <div class=" col-md-3 col-sm-6 col-xs-6">
<%--        <%: Html.EditorFor(model => model.PredefinedValues) %>--%>
        <input class="text-box single-line form-control aSettings__input_input" id="PredefinedValues" name="PredefinedValues" type="text" value="">
        <%: Html.ValidationMessageFor(model => model.PredefinedValues) %>
        <%--<img alt="help" class="helpIcon" src="/Content/images/help.png" 
				title="Input: 1|2|3<br/>Output:{{key:1, value:1}, {key:2, value:2}, ...}<br/><br/>Input:1:Taco Sauce|2:Amps|3:three<br/>Output: {{key:1, value:Taco Sauce}, {key:2, value:Amps}, ...}"/>--%>
    </div>
    <div class="clearfix"></div>
</div>
<script>
    $("#ReportParameterTypeID").addClass("form-control");
    $("#PredefinedValues").addClass("form-control");
    $("#DefaultValue").addClass("form-control");
</script>
