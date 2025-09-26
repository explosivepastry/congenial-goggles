<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Administration.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.ReleaseNote>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create Release Note
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- //purgeclassic --%>
<form action="/Admin/CreateReleaseNote" method="post">
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <div>
        <div class="formtitle">Create Release Note</div>
        <div class="formbody" >
            <table class="releaseNote" width="100%">
                  <tr>
                    <td width="20" ></td>
                    <td width="45"></td>
                    <td width="150"></td>
                    <td width="20"></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td><label for="Version">Version</label></td>
                    <td><%: Html.TextBox("Version",Model.Version) %></td>
                    <td></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td><label for="Description">Features</label></td>
                    <td>
                
                        <%: Html.TextBox("Description",Model.Description) %></td>
                    <td></td>
                    <td width="20"></td>
                </tr>
                <tr>
                    <td width="20" ></td>
                    <td style="vertical-align: top;"><label for="Note">Release Note</label></td>
                    <td >
                        <textarea id="editor"></textarea>
                        <%: Html.Hidden("Note",Model.Note) %></td>
                    <td></td>
                    <td width="20"></td>
                </tr>
            </table>

            <div class="buttons">
                <input style="margin-right: 248px;" type="submit" value="Save" class="bluebutton"  />
                <div style="clear:both"></div>
            </div>
        </div>
    </div>
</form>
    <link href="/suneditor/suneditor.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/suneditor/suneditor.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('form').submit(function (e) {
                $('#Note').val(sunObjEditor.getContents());              
            });
    
            sunObjEditor = createSunEditor('editor');
        });
        var sunObjEditor;


    </script>
</asp:Content>