<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/DefaultAdmin.Master" Inherits="System.Web.Mvc.ViewPage<List<MaintenanceWindow>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    AdminMaintenanceWindows
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        bool alt = true;
    %>
    <%if (MonnitSession.IsCurrentCustomerMonnitAdmin)
        { %>
    <div class="container-fluid">
        <div class="top-add-btn-row media_desktop">
            <a class="btn btn-primary" href="/Settings/AdminMaintenanceWindowsEdit/" style="margin-top: 10px;">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                    <defs>
                        <clipPath id="clip-path">
                            <rect width="16" height="15.999" fill="none" />
                        </clipPath>
                    </defs>
                    <g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
                        <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                    </g>
                </svg>
                &nbsp; &nbsp; 
                <%: Html.TranslateTag("Settings/AdminMaintenanceWindows|Maintenance Window","Maintenance Window")%>
            </a>
        </div>
        <div class="bottom-add-btn-row media_mobile">
            <a class="add-btn-mobile" href="/Settings/AdminMaintenanceWindowsEdit/">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                    <defs>
                        <clipPath id="clip-path">
                            <rect width="16" height="15.999" fill="none" />
                        </clipPath>
                    </defs>
                    <g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
                        <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                    </g>
                </svg>
            </a>
        </div>
        <%}%>
        <div class="rule-card_container w-100">
            <div class="card_container__top">
                <div class="card_container__top__title">
                    <%: Html.TranslateTag("Settings/AdminMaintenanceWindows|Admin Maintenance Windows","Admin Maintenance Windows")%>
                </div>
            </div>

            <%--    <div class="card_container__body">
            <div class="card_container__body__content">--%>
            <table class="newTable2">
                <thead>
                    <tr style="border-bottom: 1px solid #5153561f; background: #d7d7d785">
                        <th scope="col"><%: Html.TranslateTag("Display Date ") %></th>
                        <th scope="col"><%: Html.TranslateTag("Start Date ") %></th>
                        <th scope="col"><%: Html.TranslateTag("Hide Date ") %></th>
                        <th scope="col" class="truncate"><%: Html.TranslateTag("Title") %></th>
                        <th scope="col" class="truncate"><%: Html.TranslateTag("Description") %></th>
                        <th scope="col"></th>


                    </tr>
                </thead>
                <% if (Model != null)
                    {%>

                <% if (Model != null)
                    {
                        foreach (MaintenanceWindow item in Model)
                        {
                            alt = !alt;
                %>

                <tr class="table-cable-hov">
                    <td data-label="Display Date"><%: item.DisplayDate.OVToDateTimeShort()%> UTC</td>
                    <td data-label="Start Date"><%: item.StartDate.OVToDateTimeShort()%> UTC</td>
                    <td data-label="Hide Date"><%: item.HideDate.OVToDateTimeShort()%> UTC</td>
                    <td data-label="Title" data-id="title" class="truncate"><%: Html.Raw(item.Description)%></td>
                    <td data-label="Description" data-id="Description" class="truncate"><%: Html.Raw(item.EmailBody)%></td>
                    <td class="editThis" data-label="Description">
                        <a title="Override" class="goEdit" href="/Settings/AdminMaintenanceWindowsOverride/<%:item.MaintenanceWindowID%>"><%=Html.GetThemedSVG("pencil-square") %></a>

                        <%if (MonnitSession.IsCurrentCustomerMonnitSuperAdmin)
                          { %>
                        <a title="Edit" class="goEdit" href="/Settings/AdminMaintenanceWindowsEdit/<%:item.MaintenanceWindowID%>"><%=Html.GetThemedSVG("edit") %></a>
                        <%} %>
                    </td>
                </tr>

                <!-- Detail Row Insert -->
                <div class="<%: alt ? "alt" : "" %> holdAccountThemeLinkData" style="display: none; border-top-width: 0px;" data-maintenancenoteid="<%:item.MaintenanceWindowID %>">
                </div>
                <!-- Detail Row End -->
                <% }
                        }
                    }%>
                <%else
                    { %>
                <br />
                <%: Html.TranslateTag("Settings/AdminMaintenanceWindows|No Upcoming Maintenance Windows","No Upcoming Maintenance Windows")%>
                <%} %>
            </table>
        </div>
    </div>
    <style>
        p {
            margin: 0;
        }

        .goEdit {
            margin-left: 0.5rem;
            margin-right: 0.5rem;
        }

        .goEdit > svg {
            fill: var(--primary-color);
            width: 23px !important;
            height: 21px;
        }

        .newTable2 {
            border: 1px solid #ccc;
            border-collapse: separate;
			border-spacing: 0;
            margin: 0;
            padding: 0;
            width: 100%;
            table-layout: fixed;
        }

            .newTable2 tr {
                background-color: #f8f8f8;
                border: 1px solid #ddd;
                padding: .35em;
                border-radius: 5px;
            }

            .newTable2 th,
            .newTable2 td {
                padding: .625em;
                text-align: center;
                word-break: break-word;
            }

            .newTable2 th {
                font-size: .8rem;
                letter-spacing: .08em;
                /*  text-transform: uppercase;*/
            }

        .editThis {
            align-items: center;
            display: flex;
            justify-content: center;
        }
        .truncate {
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
		  border-left:10px solid transparent;
		  border-right:10px solid transparent;
        }
        .truncate:hover{
            overflow: visible; 
            white-space: normal;
            height:auto;  /* just added this line */
        }
    </style>
    <script>
        function truncateName() {
            var elements = document.querySelectorAll("[data-id]");
            elements.forEach(function (element) {
                var name = element.innerHTML;
                if (name.length > 20) {
                    element.innerHTML = name.slice(0, 20) + "...";
                }
            });
        }

        window.onload = function () {
            //truncateName();
        };
    </script>
</asp:Content>
