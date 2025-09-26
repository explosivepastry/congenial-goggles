<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Announcement>" %>

<%  
    string accountTheme = (Model.AccountThemeID > 0) ? Model.AccountThemeID.ToString() : "";
   
    string prefDate = MonnitSession.CurrentCustomer.Preferences["Date Format"].ToLower();
    string prefTime = MonnitSession.CurrentCustomer.Preferences["Time Format"];
%>

<div hidden>
    <input type="text" name="AccouncementID" value="<%=Model.AnnouncementID%>" />
</div>

<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|Subject","Subject")%>
    </div>
    <div class="col sensorEditFormInput">
        <input id="subject" name="Subject" class="form-control  a_modalInputs" value="<%= Model.Subject %>" />
    </div>

</div>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|Title","Title")%>
    </div>
    <div class="col sensorEditFormInput">
        <input id="title" name="Title" class="form-control  a_modalInputs" value="<%= Model.Title %>" />
    </div>

</div>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|Image Path","Image Path")%>
    </div>
    <div class="col sensorEditFormInput">
        <input id="image" name="Image" class="form-control " value="<%= Model.Image %>" placeholder="future default path" />
        <i>*<%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|Recommended Width","Recommended Width")%>: 600px</i>
    </div>

</div>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|View Path","View Path")%>
    </div>
    <div class="col sensorEditFormInput">
        <input id="link" name="Link" class="form-control " value="<%= Model.Link %>" placeholder="/Overview/Index" />
        <i>*<%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|Leaving field blank, hides View button","Leaving field blank, hides View button")%></i>
    </div>

</div>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Content","Content")%>
    </div>
    <div class="col sensorEditFormInput">
        <textarea id="editor" class="form-control"><%=Model.Content%></textarea>
        <input type="hidden" id="Content" name="Content" value="<%:Model.Content %>" />
    </div>
</div>
    

<%if (!MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
    {%>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|AccountTheme ID", "AccountTheme ID")%>
    </div>
    <div class="bold col-md-6 col-sm-6 col-xs-12">
        <input id="accountthemeid" name="AccountThemeID" class="form-control " value="<%= accountTheme %>" />
        <i>*<%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|Leaving field blank, sets value to NULL","Leaving field blank, sets value to NULL")%></i>
    </div>
</div>
<%} %>
<%else
    {%>
<input name="AccountThemeID" type="hidden" class="form-control " value="<%=MonnitSession.CurrentTheme.AccountThemeID%>" />
<%} %>
<div class="row sensorEditForm ">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Settings/_AdminReleaseNoteForm|ReleaseDate","Release Date")%>
    </div>
    <div class="col sensorEditFormInput">
        <input id="datePickMobi" name="ReleaseDate" class="form-control " value="<%= Model.ReleaseDate %>" />
        <svg id="datePick" style="cursor: pointer; margin-left: 10px;" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="18" height="18" viewBox="0 0 22 22">
            <image id="NoPath_-_Copy_47_" data-name="NoPath - Copy (47)" width="22" height="22" xlink:href="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGAAAABgCAAAAADH8yjkAAACJElEQVRo3mP4T2PAMGoByRYwoAGCBhBQP2oBeRZgY+OzAJ/6UQtGLRgKFoyCwQFoVtmMWjBqwRCyYBSMlkWjFoxaMGrBqAUELPi3M89ThUMtoHLTP0zJO4L4HUeEBYds4YosjqBLftZhoNSCdpRCNwXVE/+CGSi1YBlIkrd85ekNzfIgZieKbAsDpRacYQPKeTwHs7/mAtlMuxCSvwoYKLYgFiil8hHK+esC5DnB5Z5YMVBswWcuoNRuOPcUkMf9C8L+0szLQLkFC4Eywr/h3B+sQP5pMHOaOEiX4jIKLXi2uTOuDsH9CoqRzQiN8R/vU5yKUMB2kMobMI0yK///p64F/2KACjX+QDSqzv35n8oW/CsEKVwG4ayE2ENFC76vAiVShgrUrEwtC5Yaq4MTJV8/WnlHLQuyIIo41qGXp9SywB2mTHUObSxorupb2GcMVpj9mxYWQMBVB5DKUtpZ8P+9NlAl2wPaWfD/IagwyqehBf/NgUodaWkBKDNLUdOCn3f2vEfm9wGVClHPgn9GjAwMM9FrOG8q+iABKOOKLKABFGilogVrgDLM55HKJJDS/VS04CM3UErnB4x7jw/I1f9OzUheBZJzgWStf0vEgBzBu9TNyaUgSZ7sWYdW1YBbKYw7qFzY/U5GaTqC6mFql0WnrOGKeFq/0qI++Le9MUKfS8m7dP4LDDmqFxWjXahRC0YtGLWAphaMguE5bg2P6FELCAEAiX2+a4qCoeAAAAAASUVORK5CYII="></image>
        </svg>
    </div>
</div>

<% if (ViewBag.error != null) { %>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
    </div>
    <div class="col sensorEditFormInput" style="color:red;">
        <div class="alert alert-danger py-1" role="alert">
          <%:ViewBag.error %>
        </div>
    </div>
</div>
<% } %>

<hr />
<div class="text-end align-text-end">
    <a href="/Settings/AdminAnnouncementIndex/" class="btn btn-light"><%: Html.TranslateTag("Cancel","Cancel")%></a>
    <button id="prefSave" type="submit" value="<%: Html.TranslateTag("Save","Save")%>" class="btn btn-primary me-2">
        <%: Html.TranslateTag("Save","Save")%>
    </button>

    <%if (Model.AnnouncementID > 0)
        {%>
    <span id="deleteBtn" title="Delete Announcement" class="me-2" style="cursor:pointer;">
        <%=Html.GetThemedSVG("delete") %>
    </span>
    <%} %>
</div>


<link href="/suneditor/suneditor.min.css" rel="stylesheet" />
<script src="/suneditor/suneditor.min.js"></script>

    
<script type="text/javascript">
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    $(document).ready(function () {
        sunObjEditor = createSunEditor('editor', true);
    });

    var sunObjEditor;
    $('form').submit(function (e) {
        $('#Content').val(sunObjEditor.getContents());
    });

    var prefDate = '<%= prefDate %>';
    var dFormat = prefDate;

    $('#datePick').click(function () {
        $('#datePickMobi').click();
    });

    $('#datePickMobi').mobiscroll().datepicker({
        theme: 'ios',
        controls: ['calendar'],
        display: popLocation,
        dateFormat: dFormat.toUpperCase(),
        defaultSelection: '<%:Model.ReleaseDate%>'
    });

    $('#deleteBtn').click(function (e) {
        e.preventDefault();
        var lnk = $(this);
        if (confirm('Are you sure you want to delete this announcement?')) {
            DeleteAnnouncement();
        }
        e.stopImmediatePropagation();
    });


    function DeleteAnnouncement() {
        var id = <%=Model.AnnouncementID%>;

        $.get("/Settings/AnnouncementDelete/" + id, function (data) {
            alert(data);
            if (data == "Success") {
                window.location.href = '/Settings/AdminAnnouncementIndex';
            }
        });
    }
</script>
