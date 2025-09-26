<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Account>" %>

<div class="col-12 ps-0">
    <div class="x_panel gridPanel shadow-sm rounded mb-4">
        <div class="card_container__top">
            <div class="card_container__top__title" style="border-bottom: none;">
                <span>
                    <b><%: Html.TranslateTag("Payment Profile","Payment Profile")%>:</b>
                    <%=MonnitSession.CurrentStoreLinkInfo.UserName %>
                </span>
                <button onclick="RemoveStoreLink();" class="btn btn-secondary ms-4">
                    <%: Html.TranslateTag("Retail/PaymentOption|Unlink Account","Unlink Account")%>
                    <%=Html.GetThemedSVG("unlink") %>
                </button>
            </div>
        </div>

        <%--<div class="card_container__body">
            <div class="card_container__body__content">
                <div class="dfjcsbac">
                    <div>
                        <div class="">
                            <%if (!Request.IsSensorCertMobile() && MonnitSession.CurrentTheme.Theme == "Default")
                                {%>
                            <a style="background: #7f7f7f;" class="btn btn-secondary d-flex" target="_blank" href="/Retail/RetrieveStoreLoginGuid/<%=Model.AccountID%>">
                                <span style="color: white; margin-right: 5px; font-size: 14px;"><%: Html.TranslateTag("Visit Sensor Store","Visit Sensor Store")%></span>

                                <span style="display: flex;">
                                    <%=Html.GetThemedSVG("cart") %>
                                </span>
                            </a>
                            <%} %>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
</div>

<script type="text/javascript">



    function RemoveStoreLink() {

        $.get('/Retail/RemoveStoreLink/<%=Model.AccountID%>', function (data) {

            if (data == "Success") {
                window.location.href = "/Retail/LoginToStore/<%=Model.AccountID%>";
            } else {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }

        });

    }


</script>
