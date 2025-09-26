<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<List<ExternalDataSubscription>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Data Push (Webhooks)

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        ExternalDataSubscription DataEDS = Model.Where(w => w.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.webhook).FirstOrDefault();
        ExternalDataSubscription NtfcEDS = Model.Where(w => w.eExternalDataSubscriptionClass == eExternalDataSubscriptionClass.notification).FirstOrDefault();
    %>

    <div class="container-fluid">
        <div class="col-12">
            <%Html.RenderPartial("_APILink", Model); %>
        </div>

        <div class="x_panel shadow-sm rounded scrollParentSmall" id="hook-four" style="padding-top: 0px!important; margin-bottom: 20px;">
            <div class="card_container__top" style="padding: 8px;">
                <div class="card_container__top__title">
                    <%=Html.GetThemedSVG("api") %>
            &nbsp;
			<%: Html.TranslateTag("Export/DataPushWebhooks|Webhooks","Webhooks")%>
                </div>
            </div>

            <div class="x_content col-12 sensorList-dash hasScroll-sm" >
                <div class="col-lg-12 d-flex align-items-center" style="padding: 10px; width: 100%; ">
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("Export/DataPushWebhooks|Webhook Type", "Webhook Type")%></span>
                    <span class="col-4"><%: Html.TranslateTag("Export/DataPushWebhooks|Last Success / History","Last Success / History")%></span>
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("Export/DataPushWebhooks|Destination URL","Destination URL")%></span>
                    <span class="col-2 text-end"><%: Html.TranslateTag("Export/DataPushWebhooks|Create / Configure","Create / Configure")%></span>
                </div>

                <%
                    if (NtfcEDS != null)
                    {
                %>
                
                <div class="col-lg-12 d-flex align-items-center" style="padding: 10px; width: 100%;">
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("Export/DataPushWebhooks|Rule Webhook", "Rule Webhook")%></span>
                    <span class="col-4"><a style="color: #2699fb;" href="/Export/WebhookHistory/<%:NtfcEDS.ExternalDataSubscriptionID%>"><%:NtfcEDS.LastSuccess.OVToLocalDateTimeShort()%></a></span>
                    <span class="col-3 overflow_title"><a style="color: #2699fb;"><%=NtfcEDS.ConnectionInfo1%></a> </span>
                    <span class="col-2 text-end">
                        <a href="/Export/ConfigureWebhook/<%:NtfcEDS.ExternalDataSubscriptionID%>" class="btn btn-primary btn-sm viewBtn" type="button"><%: Html.TranslateTag("Configure","Configure")%></a>
                        <a class="viewEye" href="/Export/ConfigureWebhook/<%:NtfcEDS.ExternalDataSubscriptionID%>">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22" height="15" viewBox="0 0 22 15">
                                <path id="ic_remove_red_eye_24px" d="M12,4.5A11.827,11.827,0,0,0,1,12a11.817,11.817,0,0,0,22,0A11.827,11.827,0,0,0,12,4.5ZM12,17a5,5,0,1,1,5-5A5,5,0,0,1,12,17Zm0-8a3,3,0,1,0,3,3A3,3,0,0,0,12,9Z" transform="translate(-1 -4.5)" class="dash-icon-fill" />
                            </svg>
                        </a>
                    </span>
                </div>
                <%
                    }
                    else
                    {
                %>

                <div class="col-lg-12 d-flex align-items-center" style="padding: 10px; width: 100%;">
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("Export/DataPushWebhooks|Rule Webhook", "Rule Webhook")%></span>
                    <span class="col-4">-</span>
                    <span class="col-3 overflow_title">-</span>
                    <span class="col-2 text-end">
                        <a href="/Export/CreateWebhook/<%=eExternalDataSubscriptionClass.notification.ToInt()%>" class="btn btn-primary btn-sm viewBtn" type="button"><%: Html.TranslateTag("Create","Create")%></a>
                        <a class="viewEye" href="/Export/CreateWebhook/<%=eExternalDataSubscriptionClass.notification.ToInt()%>">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22" height="15" viewBox="0 0 22 15">
                                <path id="ic_remove_red_eye_24px" d="M12,4.5A11.827,11.827,0,0,0,1,12a11.817,11.817,0,0,0,22,0A11.827,11.827,0,0,0,12,4.5ZM12,17a5,5,0,1,1,5-5A5,5,0,0,1,12,17Zm0-8a3,3,0,1,0,3,3A3,3,0,0,0,12,9Z" transform="translate(-1 -4.5)" class="dash-icon-fill" />
                            </svg>
                        </a>
                    </span>
                </div>

                <%
                    }
                %>
                
                <%
                    if (DataEDS != null)
                    {
                %>
                
                <div class="col-lg-12 d-flex align-items-center" style="padding: 10px; width: 100%;">
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("Export/DataPushWebhooks|Data Webhook", "Data Webhook")%></span>
                    <span class="col-4"><a style="color: #2699fb;" href="/Export/WebhookHistory/<%:DataEDS.ExternalDataSubscriptionID%>"><%:DataEDS.LastSuccess.OVToLocalDateTimeShort()%></a></span>
                    <span class="col-3 overflow_title"><a style="color: #2699fb;"><%=DataEDS.ConnectionInfo1%></a> </span>
                    <span class="col-2 text-end">
                        <a href="/Export/ConfigureWebhook/<%:DataEDS.ExternalDataSubscriptionID%>" class="btn btn-primary btn-sm viewBtn" type="button"><%: Html.TranslateTag("Configure","Configure")%></a>
                        <a class="viewEye" href="/Export/ConfigureWebhook/<%:DataEDS.ExternalDataSubscriptionID%>">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22" height="15" viewBox="0 0 22 15">
                                <path id="ic_remove_red_eye_24px" d="M12,4.5A11.827,11.827,0,0,0,1,12a11.817,11.817,0,0,0,22,0A11.827,11.827,0,0,0,12,4.5ZM12,17a5,5,0,1,1,5-5A5,5,0,0,1,12,17Zm0-8a3,3,0,1,0,3,3A3,3,0,0,0,12,9Z" transform="translate(-1 -4.5)" class="dash-icon-fill" />
                            </svg>
                        </a>
                    </span>
                </div>
                <%
                    }
                    else
                    {
                %>
                
                <div class="col-lg-12 d-flex align-items-center" style="padding: 10px; width: 100%;">
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("Export/DataPushWebhooks|Data Webhook", "Data Webhook")%></span>
                    <span class="col-4"><%: Html.TranslateTag("-","-")%></span>
                    <span class="col-3 overflow_title"><%: Html.TranslateTag("-","-")%></span>
                    <span class="col-2 text-end">
                        <a onclick="$('#createDataWebhook').toggle()" class="btn btn-primary btn-sm viewBtn" type="button"><%: Html.TranslateTag("Export/DataPushWebhooks|Create","Create")%></a>
                        <a class="viewEye" onclick="$('#createDataWebhook').toggle()">
                            <svg xmlns="http://www.w3.org/2000/svg" width="22" height="15" viewBox="0 0 22 15">
                                <path id="ic_remove_red_eye_24px" d="M12,4.5A11.827,11.827,0,0,0,1,12a11.817,11.817,0,0,0,22,0A11.827,11.827,0,0,0,12,4.5ZM12,17a5,5,0,1,1,5-5A5,5,0,0,1,12,17Zm0-8a3,3,0,1,0,3,3A3,3,0,0,0,12,9Z" transform="translate(-1 -4.5)" class="dash-icon-fill" />
                            </svg>
                        </a>
                    </span>
                </div>
                <%
                    }
                %>
            </div>
        </div>

        <div class="container-fluid" id="createDataWebhook" style="display: none">
            <%--<div class="col-12">
            <%Html.RenderPartial("_APILink", Model); %>
        </div>--%>

            <div class="col-12">
                <div class="x_panel shadow-sm rounded mt-md-2">
                    <div class="card_container__top">
                        <div class="card_container__top__title">
                            <%:Html.TranslateTag("Export/DataPushWebhooks|Create Data Type","Create Data Type")%>
                        </div>
                    </div>

                    <div class="x_content">
                        <div class="col-12">
                            <label><%: Html.TranslateTag("Export/DataPushWebhooks|Select Push Type","Select Push Type")%></label>
                            <select class="form-select" id="newPush" style="width: 250px;">
                                <option><%: Html.TranslateTag("Export/DataPushWebhooks|Select Push Type","Select Push Type")%></option>
                                <option value="<%: Html.TranslateTag("webhook","webhook")%>"><%: Html.TranslateTag("Export/DataPushWebhooks|Webhook","Webhook")%></option>
                                <option value="<%: Html.TranslateTag("aws","aws")%>"><%: Html.TranslateTag("Export/DataPushWebhooks|Amazon Gateway","Amazon AWS")%></option>
                                <option value="<%: Html.TranslateTag("azure","azure")%>"><%: Html.TranslateTag("Export/DataPushWebhooks|Azure IOT","Azure IOT")%></option>
                                <option value="<%: Html.TranslateTag("watson","watson")%>"><%: Html.TranslateTag("Export/DataPushWebhooks|Watson IOT","Watson IOT")%></option>
                                <!--<option value="pi_omf"><%: Html.TranslateTag("Export/DataPushWebhooks|PI Integration","PI Integration")%></option>-->
                            </select>
                        </div>

                        <div class="clearfix"></div>
                        <div style="clear: both;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--  </div>--%>
    <% %>
    
    <style type="text/css">
        @media only screen and (max-width: 850px) {
            .viewEye {
                display: block;
            }

            .viewBtn {
                display: none;
            }
        }

        @media only screen and (min-width: 851px) {
            .viewEye {
                display: none;
            }

            .viewBtn {
                display: inline;
            }
        }
    </style>

    <script type="text/javascript">

        $(function () {
            $("#newPush").change(function () {

                var eventType = $("#newPush").val()
                switch (eventType) {
                    case "webhook":
                        window.location.href = "/Export/CreateWebhook/<%=eExternalDataSubscriptionClass.webhook.ToInt()%>";
                        break;
                    case "watson":
                        window.location.href = "/Export/ConfigureWatson/";
                        break;
                    case "aws":
                        window.location.href = "/Export/ConfigureAWS/";
                        break;
                    case "google":
                        window.location.href = "/Export/ConfigureGoogle/";
                        break;
                    case "azure":
                        window.location.href = "/Export/ConfigureAzure/";
                        break;
                    case "pi_omf":
                        window.location.href = "/Export/ConfigurePI/";
                        break;
                }
            });
        });

    </script>

</asp:Content>
