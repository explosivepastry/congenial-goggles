<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.EmailTemplate>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    TemplateCreate
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
    <%AccountTheme theme = Account.Load(Model.AccountID).getTheme(); %>
    <% Html.RenderPartial("_WhiteLabelLink", theme); %>

    <form id="tempForm" method="post">
        <div class="col-12">
            <div class="x_panel shadow-sm rounded">
                <div class="x_title">
                    <h2><%: Html.TranslateTag("Template","Template")%></h2>
                    <div class="clearfix"></div>
                </div>

                <div class="x_content" id="templateList">

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Name","Name")%>
                        </div>

                        <div class="col sensorEditFormInput">
                            <div class="editor-field">
                                <input type="text" id="Name" name="Name" class="form-control" value="<%= Model.Name %>" />
                            </div>
                            <div class="editor-error">
                                <%: Html.ValidationMessageFor(model => model.Name)%>
                            </div>
                            <br />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Subject","Subject")%>
                        </div>

                        <div class="col sensorEditFormInput">
                            <div class="editor-field">
                                <input type="text" id="Subject" name="Subject" class="form-control" value="<%= Model.Subject %>" />
                            </div>

                            <div class="editor-error">
                                <%: Html.ValidationMessageFor(model => model.Subject)%>
                            </div>
                            <br />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                            <%: Html.TranslateTag("Flags","Flags")%>
                        </div>

                        <div class="col input-group">
                            <%: Html.DropDownList("FlagOptions", eEmailTemplateFlag.Generic) %>
                            <input id="AddFlag" type="button" class="btn btn-secondary btn-sm" value="Add" />
                        </div>
                    </div>

                    <div class="row sensorEditForm">
                        <div class="col-12 col-md-3">
                        </div>
                      
                        <div class="col sensorEditFormInput">
                            <div class="col-sm-9 col-12 ps-0 lgBox">
                                <input id="Flags" name="Flags" type="text" value="" readonly="readonly"  class="form-control">
                            </div>
                        
                            <div class="col-sm-3 col-12">
                                <input type="button" class="btn btn-info" value="Clear" onclick="$('#Flags').val('');" />
                            </div>
                            <div class="editor-error">
                                <%: Html.ValidationMessageFor(model => model.Flags)%>
                            </div>
                            <br />
                        </div>

                    </div>

                    <br /><br />

                    <div class="card bg-light mb-3" style="max-width: 38rem;">
                            <div class="card-header"><%: Html.TranslateTag("Template","Template - HTML Source Code")%>:</div>
                                    <div class="card-body">
                  <div><textarea id="htmltext" class="form-control card-text" contenteditable="true" placeholder="<%: Html.TranslateTag("Write HTML Source Code here...","Write HTML Source Code here")%>..."><%:Model.Template %></textarea></div>
        </div>
</div>
        
<br />

<div class="row">
    <div class="prevreset">
    <input type="button" id="preview" class="btn btn-sm btn-primary active" value="<%: Html.TranslateTag("Preview","Preview")%>"  onclick="PreviewHtml();" title="<%: Html.TranslateTag("HTML Viewer","HTML Viewer")%>" />
    <input type="button" id="cleartextarea" class="btn btn-sm btn-secondary" onclick="ResetHtml();" value="<%: Html.TranslateTag("Reset","Reset")%>" title="<%: Html.TranslateTag("Reset to Default HTML values from DB","Reset to Default HTML values from DB")%>" />
    <input type="button" id="clear" class="btn btn-sm btn-info" value="<%: Html.TranslateTag("Clear","Clear")%>" onclick="ClearTextViewer();" title="<%: Html.TranslateTag("Clear Html Source Code","Clear Html Source Code")%>"  />
  </div>
</div> 

<br />

        <div class="row">
        <div>
        <p id="output" style="display: none; font-weight: 600"><%: Html.TranslateTag("Output","Output")%>:</p>
           <div class="col-md-4 border-dark auto">
                <div style="display: table;" id="tableviewer" >
		                <div style="display: table-row;">
		                <div style="height: 300px; display: table-cell; border: 0px solid #eae1e1;" id="viewer" ></div>

        <%:  Html.Hidden("Template",Model.Template) %>
        <%: Html.ValidationMessageFor(model => model.Template) %>

        </div>  
     </div> 
   </div> 
 </div> 
</div>  

             <div style="clear: both;"></div>
            <div class="col-12 mdBox text-end" >
            <%--    <div class="col-sm-9 col-12 text-end">--%>
                    <a href="/Settings/AdminEmailTemplate/<%=theme.AccountThemeID%>" class="btn btn-secondary"><%: Html.TranslateTag("Cancel","Cancel")%></a>
                    <a onclick="submitTemplate();" class="btn btn-primary me-3"><%: Html.TranslateTag("Save","Save")%></a>
                </div>

          <div class="form-group d-flex">
               <div class="bold col-3" id="messageDiv" style="color: darkgreen; font-weight: 600;">
          </div>
</div>

</div>  
</div>   
</div>  
</form>
</div>   
 
    <script type="text/javascript">

        $('#FlagOptions').addClass('form-select');

        $(document).ready(function () {
            $('#Template').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#Flags').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#FlagOptions').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#Subject').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            $('#Name').keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

                      
            $('#Flags').attr("readonly", "true");
            $('#Flags').css("background-color", "#eee");

            $('#AddFlag').click(function () {
                if ($('#Flags').val() == "")
                    $('#Flags').val($('#FlagOptions').val());
                else
                    $('#Flags').val($('#Flags').val() + "|" + $('#FlagOptions').val());
            });
        });

        function submitTemplate() {
            var sourcecode = $(document.getElementById("htmltext")).val();
                  $('#Template').val(sourcecode);
                  $.post("/Settings/TemplateCreate/<%=Model.AccountID%>", $('#tempForm').serialize(), function (data) {
                       if (data != "Success") {
                               $('#messageDiv').html(data);
                             }
                else {
                    window.location.href = '/Settings/AdminEmailTemplate/<%=theme.AccountThemeID%>';
                }
            });
        }
              
         function PreviewHtml() {
             var text = document.getElementById("htmltext");
             var previewhtml = document.getElementById("viewer");
             previewhtml.innerHTML = text.value;
             $("#output").show("slow");
             setTimeout(function () {
                 $("#messageDiv").show();
                 $('#messageDiv').fadeOut('slow');
             }, 7000);
         }

         function ResetHtml() {
            //document.getElementById('htmltext').value = "";
             $("#viewer").empty();
                 //$("#viewer").fadeIn(1)
                     location.reload(true);
                 //$("#output").hide();
             $("#htmltext").focus();
         };

         function ClearTextViewer() {
             document.getElementById('htmltext').value = "";
             $("#viewer").empty();
                 $("#output").hide();
             $("#htmltext").focus();
             setTimeout(function () {
                 $('#messageDiv').fadeOut('slow');
             }, 7000);

         };

    </script>

    <style type="text/css">

         #htmltext
        {
            font-family: 'Courier New', monospace;  
            resize: none; 
            background-color: #F8F9FA;
            cursor: auto;
            color: #000000; 
            margin-left: 1px;
            overflow-y: visible ; 
            width: auto; 
            min-width: 425px; 
            max-width: 500px; 
            height: 350px;
            display: block;
        }

        .prevreset {
            width: 50%;
            margin-left: 5px;
        }

        .form-select {
            max-width: 250px;
        }

    </style>

</asp:Content>




