<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="header">
	<div id="logo">
		&nbsp;
	</div>

    <div id="logo2">
        &nbsp;
    </div>
	<!-- logo -->

	<div id="logindisplay">
		<% Html.RenderPartial("LogOnUserControl"); %>
	</div>
	<!-- logindisplay -->

 
	<div id="menucontainer">
		<% Html.RenderPartial("TopNavigation"); %>
	</div>
	<!-- menucontainer -->
</div>
<!-- header -->

