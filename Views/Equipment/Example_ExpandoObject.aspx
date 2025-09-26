<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    asdf
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>asdf</h2>
	<%@ Import Namespace="Monnit"%>
	<%@ Import Namespace="System.Dynamic"%>
	<%
		Dictionary<string, object> props = new Dictionary<string, object>();
		props.Add("asdf", "ASDF");
		props.Add("qwer", "QWER");
		props.Add("a1234", "!@#$");
		props["b0987"] = ")(*&";
		props["a2df"] = "nvnvnvnv";

		var expa = new ExpandoObject() as IDictionary<string, object>;
		foreach (var property in props) {
			expa.Add(property.Key, property.Value);
		}
		
		//var dyna = expa as IDictionary<string, object>;	// dyna["asdf"]
		dynamic dyna = expa;								// dyna.asdf

		foreach (dynamic item in dyna) { %>
			<div><%:item.Key%> | <%:item.Value%></div>
		<%}%>
	<div>-----------------------------------------------</div>
	<div><%:dyna.asdf %></div>
	<div><%:dyna.qwer %></div>
	<div><%:dyna.a1234 %></div>
	<div><%:dyna.b0987 %></div>
	<div><%:dyna.a2df %></div>
</asp:Content>