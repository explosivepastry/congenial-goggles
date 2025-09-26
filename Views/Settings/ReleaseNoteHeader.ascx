<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Announcement>" %>

<!-- Detail Buttons -->
<div class="col-md-12 col-xs-12">
    <div class="view-btns">
        <a href="/Settings/AdminAnnouncement" class="btn btn-default btn-lg btn-fill"><i class="fa fa-arrow-left"></i></a>
        <a href="/Settings/AdminAnnouncementDetails/<%:Model.AnnouncementID %>" class="btn <%:Request.Path.StartsWith("/Settings/AdminAnnouncementDetails/")?"btn-primary":"btn-grey" %> btn-lg btn-fill"><%=Html.GetThemedSVG("list") %><span class="extra">&nbsp;<%: Html.TranslateTag("Manage","Manage")%></span></a>
        <a href="/Settings/AdminAnnouncementView/<%:Model.AnnouncementID %>" target="_blank" class="btn <%:Request.Path.StartsWith("/Settings/AdminAnnouncementView/")?"btn-primary":"btn-grey" %> btn-lg btn-fill"><i class="fa fa-eye"></i><span class="extra">&nbsp;<%: Html.TranslateTag("View","View")%></span></a>
        
		<%if(MonnitSession.IsCurrentCustomerMonnitSuperAdmin) { %>
			<a href="/Settings/AdminAnnouncementEdit/<%:Model.AnnouncementID %>" class="btn <%:Request.Path.StartsWith("/Settings/AdminAnnouncementEdit/")?"btn-primary":"btn-grey" %> btn-lg btn-fill"><i class="fa fa-pencil"></i><span class="extra">&nbsp;<%: Html.TranslateTag("Edit","Edit")%></span></a>
		<%} %>
    </div>
</div>
<!-- End Detail Buttons -->