<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EmailTemplate>" %>

<form id="emailform" method="post" action="/Settings/buildEmail">

    <div class="col-12 dfac">
        <div class="bold col-md-2 col-3 aSettings__title"><%: Html.TranslateTag("Settings/_AdminMassEmailBody|Templates:","Templates")%></div>
        <div class="dfac col-md-10 col-9">
            <button id="rtemplate" type="button" value="<%: Html.TranslateTag("Settings/_AdminMassEmailBody|Release Note","Release Note")%>" class="btn btn-secondary me-2">
                <%: Html.TranslateTag("Settings/_AdminMassEmailBody|Release Note","Release Note")%>
            </button>
            <button id="mtemplate" type="button" value="<%: Html.TranslateTag("Settings/_AdminMassEmailBody|Maintenance","Maintenance")%>" class="btn btn-secondary">
                <%: Html.TranslateTag("Settings/_AdminMassEmailBody|Maintenance","Maintenance")%>
            </button>
        </div>
    </div>

    <div class="clearfix"></div>
    
    <br />
    
    <div id="mailbody">
        <%Html.RenderPartial("_AdminMassEmailContent", new EmailTemplate());%>
    </div>

</form>

<div class="text-end mt-2">
    <button id="postEmail" type="button" value="<%: Html.TranslateTag("Settings/_AdminMassEmailBody|Send","Send")%>" class="btn btn-primary">
        <%: Html.TranslateTag("Settings/_AdminMassEmailBody|Send","Send")%>
    </button>
</div>
