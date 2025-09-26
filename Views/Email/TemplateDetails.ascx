<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>

        <div class="display-label"><%: Html.LabelFor(model => model.Name) %></div>
        <div class="display-field"><%= Model.Name %></div>
        
        <div class="display-label"><%: Html.LabelFor(model => model.Subject) %></div>
        <div class="display-field"><%= Model.Subject%></div>
        
        <div class="display-label"><%: Html.LabelFor(model => model.Flags)%></div>
        <div class="display-field" style="width:585px; overflow:auto; "><%= Model.Flags%></div>
        
        <div class="display-label">Email Text</div>
        <div style="clear:both; width:585px; overflow:auto;">
             <% string temp = System.Net.WebUtility.HtmlDecode(Model.Template);  %>
            <%:  Html.Raw(temp)%></div>
                

 <div class="buttons" style="margin: 10px -10px -10px -10px;">
     <% if(MonnitSession.IsCurrentCustomerMonnitAdmin){ %>
     <a href="#" onclick="getMain('/Email/TemplateDelete/<%:Model.EmailTemplateID%>', '<%: Model.Name %>'); return false;" class="greybutton">Delete</a>
     <% } %>
    <a href="#" onclick="getMain('/Email/TemplateEdit/<%:Model.EmailTemplateID%>', '<%: Model.Name %>'); return false;" class="bluebutton">Edit</a>
    <div style="clear:both;"></div>
</div>
