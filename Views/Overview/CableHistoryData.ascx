<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>

<%
    Sensor sensor = (Sensor)ViewBag.Sensor;
%>


<!-- History -->
<div id="insideData">


    <table class="newTable">
        <thead>
            <tr style="border-bottom: 1px solid #5153561f; background: #d7d7d785">
                <th scope="col"><%: Html.TranslateTag("CableID","CableID")%></th>
                <th scope="col"><%: Html.TranslateTag("Application Name","Application Name")%></th>
                <th scope="col"><%: Html.TranslateTag("Plugged In","Plugged In")%></th>
                <th scope="col"><%: Html.TranslateTag("Unplugged","Unplugged")%></th>
            </tr>
        </thead>
                    <tbody>
            <%
                string ApplicationName = string.Empty;

                for (int i = 0; i < Model.Rows.Count; i++)
                {
                    System.Data.DataRow dr = Model.Rows[i];

                    ApplicationName = dr["ApplicationName"].ToStringSafe();

                    CableAudit item = new CableAudit();
                    item.Load(dr);
                    if (item.AuditType != CableAudit.eCableAuditType.PlugIn)
                    {
                        continue;
                    }

                    CableAudit nextItem = null;

                    if (Model.Rows.Count > i + 1)
                    {
                        nextItem = new CableAudit();
                        item.Load(Model.Rows[i + 1]);
                    }


            %>
            <tr class="table-cable-hov">
                <td scope="row" data-label="Cable ID"> <%:item.CableID %></td>
                <td data-label="Application Name"><%: ApplicationName%></td>
                <td data-label="Plugged In"><%:item.LogDate.OVToLocalDateTimeShort()%></td>

                <% if (nextItem != null && nextItem.CableID == item.CableID && nextItem.AuditType.ToString() == "PlugOut")
                    {%>
                <td title="Plug Out Date" data-label="Unplugged"><%:nextItem.LogDate.OVToLocalDateTimeShort()%> </td>
                <%i++;
                    }
                    else
                    { %>
                <td title="Plug Out Date" data-label="Unplugged"></td>
                <%} %>
            </tr>
            <%} %>
        </tbody>

    </table>

</div>


<style>
    .newTable {
        border: 1px solid #ccc;
        border-collapse: collapse;
        margin: 0;
        padding: 0;
        width: 100%;
        table-layout: fixed;
    }



        .newTable tr {
            background-color: #f8f8f8;
            border: 1px solid #ddd;
            padding: .35em;
            border-radius: 5px;
        }

        .newTable th,
        .newTable td {
            padding: .625em;
            text-align: center;
        }

        .newTable th {
            font-size: .8rem;
            letter-spacing: .08em;
            /*  text-transform: uppercase;*/
        }

    @media screen and (max-width: 600px) {
        .newTable {
            border: 0;
        }



            .newTable thead {
                border: none;
                clip: rect(0 0 0 0);
                height: 1px;
                margin: -1px;
                overflow: hidden;
                padding: 0;
                position: absolute;
                width: 1px;
            }

            .newTable tr {
                border-bottom: 3px solid #ddd;
                display: block;
                margin-bottom: .625em;
                box-shadow: 0 1px 2px rgba(0,0,0,0.05), inset 0px 1px 3px rgba(0,0,0,0.1);
            }

            .newTable td {
                border-bottom: 1px solid #ddd;
                display: block;
                font-size: .8em;
                text-align: right;
            }

                .newTable td::before {
                    content: attr(data-label);
                    float: left;
                    font-weight: bold;
                    text-transform: uppercase;
                    color: var(--primary-color);
                    margin-right: 10px;
                }

                .newTable td:last-child {
                    border-bottom: 0;
                }
    }
</style>

<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("CableHistoryData.ascx") %>

</script>

