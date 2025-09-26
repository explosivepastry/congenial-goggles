<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ErrorModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ErrorDisplay
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="col-md-12 col-xs-12">
            <div class="x_panel">
                <div class="x_title">
                    <h2><span class="fa fa-exclamation-triangle"></span>&nbsp;<%=Html.TranslateTag(Model.ErrorTranslateTag + Model.ErrorTitle,Model.ErrorTitle)%></h2>

                    <div class="clearfix"></div>
                </div>
                <div class="x_content bold" id="errorMessage" style="color: red;">
                    <div style="font-size: 1.4em">
                        <%=Html.TranslateTag(Model.ErrorTranslateTag + Model.ErrorMessage,Model.ErrorMessage)%>
                    </div>
                </div>
 
                <br />
                <br />
                <a role="button" href="/Overview" class="btn btn-danger btn-large" style="margin-left: 40%; margin-right: 40%; width: 20%; height: 40px; border-radius: 3px!important; font-size: 18px; align-content: center; vertical-align: middle!important; background: linear-gradient(to bottom, #335c80 0%,#207cca 0%,#335c80 100%,#1e5799 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */ border-color: #335c80;"><%=Html.TranslateTag("Home","Home")%></a>
            </div>
        </div>
    </div>

<%--
    <script type="text/javascript">

        let errorTitle           = '<%: Model.ErrorTitle        %>';
        let errorTranslateTag    = '<%: Model.ErrorTranslateTag %>';
        let errorMessage         = '<%: Model.ErrorMessage      %>';
        let errorSubject         = '<%: Model.ErrorSubject      %>';
        let errorLocation        = '<%: Model.ErrorLocation     %>';

        $(document).ready(function () {
            let errorModalMessage =
                `<div class='row'><div class='col-md-3'> errorTitle         = </div><div class='col-md-8'> ${errorTitle}          </div></div>
                 <div class='row'><div class='col-md-3'> errorTranslateTag  = </div><div class='col-md-8'> ${errorTranslateTag}   </div></div>
                 <div class='row'><div class='col-md-3'> errorMessage       = </div><div class='col-md-8'> ${errorMessage}        </div></div>
                 <div class='row'><div class='col-md-3'> errorSubject       = </div><div class='col-md-8'> ${errorSubject}        </div></div>
                 <div class='row'><div class='col-md-3'> errorLocation      = </div><div class='col-md-8'> ${errorLocation}       </div></div>`

            $('#alertModalMessage').after(errorModalMessage);   // #alertModalMessage is a <p>. 
            $('#alertModalMessage').remove();                   // I want to control formatting but don't want to break any existing uses
            $('#alertModal .modal-dialog').css('max-width', '600px');
            $('#alertModal').modal('show');
        });

    </script>
--%>
    
</asp:Content>
