<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<div>

    <%Html.RenderPartial("_SetupStepper", Model.SensorID); %>

    <%
        Dictionary<string, object> dic = new Dictionary<string, object>();
        if (!Model.CanUpdate)
        {
            dic.Add("disabled", "disabled");
            ViewData["disabled"] = true;
        }

        ViewData["HtmlAttributes"] = dic;%>
    <div class="col-lg-6 col-12 ps-0">
        <div class="rule-card_container w-100 " id="hook-seven">
            <div class="card_container__top ">
                <div class="card_container__top__title">
                    <div>
                        <%: Model.MonnitApplication.ApplicationName%> <%: Html.TranslateTag("Overview/SensorEdit|Settings","Settings")%>
                    </div>
                    <%if (Model.SensorTypeID == (int)eWitType.PoE_Wit || Model.SensorTypeID == (int)eWitType.LTE_Wit)
                        {%>
                    <div class="col-sm-9 col-6 top-add-btn-row media_desktop" style="margin-top: -18px; margin-bottom: -12px;">
                        <a href="/Overview/InterfaceEdit/<%:Model.SensorID%>" class="btn btn-secondary" style="max-width: 175px; font-size: 14px; padding: 5px 10px;">
                            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Interface Settings", "Interface Settings")%>
                            <svg xmlns="http://www.w3.org/2000/svg" width="10.425" height="10.425" viewBox="0 0 10.425 10.425" style="margin-left: 10px;">
                                <path id="Path_725" fill="#707070" data-name="Path 725" d="M5.313,2,1.6,5.814,0,10.425l4.611-1.5L8.32,5.213Zm4.711-.3L8.721.4a1.215,1.215,0,0,0-1.8,0l-1.1,1.1L8.821,4.611l1.2-1.2a1.271,1.271,0,0,0,.4-.9A1.237,1.237,0,0,0,10.024,1.7Z"></path>
                            </svg>
                        </a>
                    </div>
                    <%} %>
                </div>
                <div class="nav navbar-right panel_toolbox" style="cursor:pointer;">
                    <!-- help button 111-->
                    <a class="helpIco help-hover" data-bs-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-bs-target=".pageHelp">
                        <%=Html.GetThemedSVG("help") %>
                     </a>
                </div>
            </div>
            <div class="x_content">
                <form class="form-horizontal form-label-left" action="/Setup/SensorEdit/<%:Model.SensorID %>" id="simpleEdit_<%:Model.SensorID %>" method="post">
                    <%: Html.ValidationSummary(false)%>
                    <input type="hidden" class="form-control" value="/Setup/SensorEdit/<%:Model.SensorID %>" name="returns" id="returns" />
                    <% 
                        //string ViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\Default_Simple\\_SimpleEdit", Model.ApplicationID.ToString("D3"));
                        string ViewToFind = "SensorEdit\\ApplicationCustomization\\Default_Simple\\_SimpleEdit";
                        if (MonnitViewEngine.CheckPartialViewExists(Request, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                        {
                            ViewBag.returnConfirmationURL = ViewToFind;
                            Html.RenderPartial("~\\Views\\Sensor\\" + ViewToFind + ".ascx", Model);
                        }
                        else
                        {
                            Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Edit.ascx", Model);
                        }
                    %>
                </form>
            </div>
        </div>
    </div>
</div>
<!-- help button modal -->
<div class="modal fade pageHelp" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title">
                    <%: Html.TranslateTag("Overview/SensorHome|Sensor Edit Settings","Sensor Edit Settings")%>
                </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <% 
                    //If specific monnit application edit view exists use that page
                    string HelpViewToFind = string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Help", Model.ApplicationID.ToString("D3"));
                    if (MonnitViewEngine.CheckPartialViewExists(Request, HelpViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
                    {
                        ViewBag.returnConfirmationURL = HelpViewToFind;
                        Html.RenderPartial("~\\Views\\Sensor\\" + HelpViewToFind + ".ascx", Model);
                    }
                    else
                    {
                        Html.RenderPartial("~\\Views\\Sensor\\SensorEdit\\ApplicationCustomization\\Default\\Help.ascx", Model);
                    }
                %>
            </div>
        </div>
    </div>
</div>
