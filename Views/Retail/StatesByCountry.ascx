<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<XElement>>" %>

<select class="form-control tzSelect" style="height: 25px;width:100%;" name="state" id="state">
    <%for (int i = 0; i < Model.Count; i++)
      {
        XElement element = Model[i];
        string id = element.Attribute("ID").Value;
        string name = element.Attribute("Name").Value; %>
        <option <%=i == 0 ? "selected='selected'" : "" %> value="<%=id %>"><%=name%></option>
    <%} %>
</select>