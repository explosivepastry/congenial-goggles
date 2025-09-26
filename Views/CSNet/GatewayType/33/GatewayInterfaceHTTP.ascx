<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>



<div class="formBody">

    <div class="editor-label">
        Auto Disable (Minutes)
    </div>
    <div class="editor-field">
        <select name="HTTPServiceTimeout" id="HTTPServiceTimeout">
            <option value="1" <%= Model.HTTPServiceTimeout < 5 ? "selected=''" : ""%>>Short (1 min)</option>
            <option value="5" <%= Model.HTTPServiceTimeout >= 5 && Model.HTTPServiceTimeout < 30 ? "selected=''" : ""%>>Default (5 min)</option>
            <option value="30" <%= Model.HTTPServiceTimeout >= 30 && Model.HTTPServiceTimeout < 1092 ? "selected=''" : ""%>>Long (30 min)</option>
            <option value="1092.25" <%= Model.HTTPServiceTimeout >= 1092 ? "selected=''" : ""%>>Always Available</option>
        </select>
    </div>
    <div class="editor-error">
        <%: Html.ValidationMessageFor(model => model.HTTPServiceTimeout)%>
    </div>


</div>



