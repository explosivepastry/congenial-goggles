<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

       <input hidden class="aSettings__input_input" type="number" step="any"  <%=Model.CanUpdate ? "" : "disabled"  %> name="ActiveStateInterval" id="ActiveStateInterval" value="<%=Model.ActiveStateInterval %>" />
        <%: Html.ValidationMessageFor(model => model.ActiveStateInterval)%>
