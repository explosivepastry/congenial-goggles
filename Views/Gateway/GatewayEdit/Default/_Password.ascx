<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%string Password = string.Empty;
  if (Model.Password.Length > 0)
  {
    Password = MonnitSession.UseEncryption ? Model.Password.Decrypt() : Model.Password;
  }%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_Password|Cellular Network Password","Cellular Network Password")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBox("Password", Password)%>
        <%: Html.ValidationMessageFor(model => model.Password)%>
    </div>
</div>

