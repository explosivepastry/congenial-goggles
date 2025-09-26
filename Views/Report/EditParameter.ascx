<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.ReportParameter>" %>

<form action="/Report/EditParameter" method="post">
  <%--  <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>--%>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>ReportParameter</legend>

        <%: Html.HiddenFor(model => model.ReportParameterID) %>
        <%: Html.HiddenFor(model => model.ReportQueryID) %>
        <%: Html.HiddenFor(model => model.SortOrder) %>

        <div class="editor-label">
            Parameter Type
        </div>
        <div class="editor-field">
			<%--<select id="ReportParameterTypeID" name="ReportParameterTypeID">
				<% List<ReportParameterType> parameterTypesList = ReportParameterType.LoadAll();
				for (int i = 0; i < parameterTypesList.Count; i++)
				{%>
					<option id='parameterType_<%:i%>' value='<%:i%>'><%:parameterTypesList.ElementAt(i) %></option>
				<%}%>
			</select>--%>
			<%: Html.DropDownList("ReportParameterTypeID",new SelectList(ReportParameterType.LoadAll(),"ReportParameterTypeID","Name",Model.ReportParameterTypeID), "Select One") %>
			<br />
            <%: Html.ValidationMessageFor(model => model.ReportParameterTypeID) %>
        </div>

        <div class="editor-label">
            Parameter Name
        </div>
        <div class="editor-field">
             <input type="text" id="ParamName" name="ParamName" value="<%= Model.ParamName %>" /><br />
            <%: Html.ValidationMessageFor(model => model.ParamName) %>
        </div>

        <div class="editor-label">
            Label Text
        </div>
        <div class="editor-field">
           <input type="text" id="LabelText" name="LabelText" value="<%= Model.LabelText %>" /><br />
            <%: Html.ValidationMessageFor(model => model.LabelText) %>
        </div>

        <div class="editor-label">
            Help Text
        </div>
        <div class="editor-field">
            <input type="text" id="HelpText" name="HelpText" value="<%= Model.HelpText %>" /><br />
            <%: Html.ValidationMessageFor(model => model.HelpText) %>
        </div>

        <div class="editor-label">
            Default Value
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.DefaultValue) %><br />
            <%: Html.ValidationMessageFor(model => model.DefaultValue) %>
        </div>
		
		<div class="editor-label">
            Predefined Values
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.PredefinedValues) %><br />
            <%: Html.ValidationMessageFor(model => model.PredefinedValues) %>
			<img alt="help" class="helpIcon" src="/Content/images/help.png" 
				title="Input: 1|2|3<br/>Output:{{key:1, value:1}, {key:2, value:2}, ...}<br/><br/>Input:1:Taco Sauce|2:Amps|3:three<br/>Output: {{key:1, value:Taco Sauce}, {key:2, value:Amps}, ...}"/>
        </div>

        <div style="clear:both;"></div>
        <p>
            <input type="button" class="submitModal" value="Save" />
            <input type="button" value="Cancel" onclick="hideModal();" />
        </p>
    </fieldset>
</form>


<script>
	$(function () {
		$('.helpIcon').tipTip();

        $('.submitModal').click(function () {
            var form = modalDiv.children('form');
            $.post(form.attr("action"), form.serialize(), function (data) {
                modalDiv.html(data);
                if (data == "Success")
                    window.location.href = window.location.href;
            }, "text");

            modalDiv.html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
        });
    });
</script>
