<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.AccountTheme>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminContacts
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <%Account account = Account.Load(Model.AccountID);%>
        <% Html.RenderPartial("_WhiteLabelLink", Model); %>
        <div class="pt-0" style="width: 100%;">
        
        <div>
            <div class="col-md-6 col-12 pe-md-2 ps-0 mb-4">
                <div class="rule-card_container w-100">
                    <div class="card_container__top__title">
                            <div class="d-flex align-items-center">
                <svg xmlns="http://www.w3.org/2000/svg" width="25" height="22" viewBox="0 0 18 15">
                    <path id="address-card-regular" d="M16.5,32H1.5A1.557,1.557,0,0,0,0,33.607V45.393A1.557,1.557,0,0,0,1.5,47h15A1.557,1.557,0,0,0,18,45.393V33.607A1.557,1.557,0,0,0,16.5,32Zm0,13.393H1.5V33.607h15ZM6.5,39.5a2.148,2.148,0,0,0,0-4.286,2.148,2.148,0,0,0,0,4.286ZM3.7,43.786H9.3a.673.673,0,0,0,.7-.643V42.5a2.02,2.02,0,0,0-2.1-1.929,7.1,7.1,0,0,1-1.4.268c-.841,0-1.044-.268-1.4-.268A2.02,2.02,0,0,0,3,42.5v.643A.673.673,0,0,0,3.7,43.786Zm7.55-2.143h3.5a.26.26,0,0,0,.25-.268v-.536a.26.26,0,0,0-.25-.268h-3.5a.26.26,0,0,0-.25.268v.536A.26.26,0,0,0,11.25,41.643Zm0-2.143h3.5a.26.26,0,0,0,.25-.268V38.7a.26.26,0,0,0-.25-.268h-3.5A.26.26,0,0,0,11,38.7v.536A.26.26,0,0,0,11.25,39.5Zm0-2.143h3.5a.26.26,0,0,0,.25-.268v-.536a.26.26,0,0,0-.25-.268h-3.5a.26.26,0,0,0-.25.268v.536A.26.26,0,0,0,11.25,37.357Z" transform="translate(0 -32)" class="main-page-icon-fill" />
                </svg>
                &nbsp;
                &nbsp;
 
                <div class="clearfix"></div>
            </div>
                    <%: Html.TranslateTag("Settings/AdminContacts|Account Contact List","Account Contact List")%>
                        <div class="nav navbar-right panel_toolbox">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content" id="themeContactList">

                        <% Html.RenderPartial("_AdminThemeContactList", Model); %>
                    </div>
                </div>
            </div>
            <div class="col-md-6 col-12 ps-md-2">
                <div class="rule-card_container w-100">
                    <div class="card_container__top__title">
                        <%: Html.TranslateTag("Settings/AdminContacts|Create Contact","Create Contact")%>
                        <div class="nav navbar-right panel_toolbox">
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <form id="ActionForm">
                            <div class="col-12">
                                <div class="form-group">
                                    <div class="col-12 form-group createContact"  >
                                        <div class="col-lg-3 col-sm-4 col-12 bold aSettings__title"><%: Html.TranslateTag("First Name","First Name")%></div>
                                        <div class="col-lg-9 col-sm-8 col-12">
                                            <input type="text" id="fname" name="FirstName" class="form-control aSettings__input_input user-dets" required placeholder="First Name" />
                                        </div>
                                    </div>
                                
                                    <div class="col-md-12 col-12 form-group  createContact">
                                        <div class="col-lg-3 col-sm-4 col-12 bold aSettings__title"><%: Html.TranslateTag("Last Name","Last Name")%></div>
                                        <div class="col-lg-9 col-sm-8 col-12">
                                            <input type="text" id="lname" name="LastName" class="form-control aSettings__input_input user-dets" required placeholder="Last Name" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-12 form-group  createContact">
                                        <div class="col-lg-3 col-sm-4 col-12 bold aSettings__title"><%: Html.TranslateTag("Email","Email")%></div>
                                        <div class="col-lg-9 col-sm-8 col-12">
                                            <input type="text" id="email" name="Email" class="form-control aSettings__input_input user-dets" required placeholder="Email" />
                                        </div>
                                    </div>
                                    <div class="col-md-12 col-12 form-group  createContact">
                                        <div class="col-lg-3 col-sm-4 col-12 bold aSettings__title"><%: Html.TranslateTag("Other","Other")%></div>
                                        <div class="col-lg-9 col-sm-8 col-12">
                                            <input type="text" name="Other" class="form-control aSettings__input_input user-dets" placeholder="Other" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <br />
                            <div class="col-12 text-end">
                                <button id="AddButton" type="button" class="btn btn-primary " value="<%: Html.TranslateTag("Add","Add")%>">
                                    <%: Html.TranslateTag("Add","Add")%>
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            $('#AddButton').click(function () {
                var Email = $('#email').val();
                if (!Email.includes('@')) {
                    showSimpleMessageModal("<%=Html.TranslateTag("Invalid Email")%>");
                    return;
                }
                var body = $('#ActionForm').serialize();
                var href = "/Settings/SetThemeContact/<%=Model.AccountThemeID%>";
                $.post(href, body, function (data) {
                    if (data == "Failed") {
                        showSimpleMessageModal("<%=Html.TranslateTag("Failed to Create Contact")%>");
                    }
					else {
						$('#ActionForm').get(0).reset();
						toastBuilder("Success");
                        $('#themeContactList').html(data);
                    }
                });
            });
        });
    </script>
</asp:Content>
