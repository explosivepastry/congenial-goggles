<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%string Title = MonnitSession.CurrentStyle("Title"); %>
<%: !String.IsNullOrEmpty(Title) ? Title: "Online Wireless Sensors Portal" %> | 