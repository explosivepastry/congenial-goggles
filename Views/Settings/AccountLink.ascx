<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Account>" %>

<!-- Detail Buttons -->

<div id="aPreferences" class="col-md-12 col-xs-12" style="width: 99%; margin-top: 15px;">
    <div class="top-row-btn-left">
        <a class="add-btn" href="/Settings/AccountEdit/<%=Model.AccountID %>">
            <svg xmlns="http://www.w3.org/2000/svg" width="23.318" height="16" viewBox="0 0 23.318 16" style="margin-right:15px;">
                <g id="Symbol_86" data-name="Symbol 86" transform="translate(16 16) rotate(180)">
                <path id="Path_10" data-name="Path 10" d="M8,0,6.545,1.455l5.506,5.506H-7.318V9.039h19.37L6.545,14.545,8,16l8-8Z" fill="#fff"/>
                </g>
            </svg>
            <span><%:Html.TranslateTag("Settings/AccountEdit|Settings","Settings")%> </span>
        </a>
    </div>
</div>
<!-- End Detail Buttons -->
