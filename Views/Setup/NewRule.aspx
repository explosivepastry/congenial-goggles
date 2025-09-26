<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    NewRule
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="setup_device_container">
        <div class="setup_newrule_design">

            <h2 class="heading-color">Congratulation </h2>
            <h2 class="heading-color head-row"><%:Html.TranslateTag ("Your device is setup!")%></h2>

            <div class="notify-row">
                <div class="notify-img"><%=Html.GetThemedSVG("notifications") %>  </div>
                <h4>
                    <strong><%:Html.TranslateTag ("Notifications")%></strong>
                </h4>
            </div>

            <div class="notify-title">
                <p><%:Html.TranslateTag ("Notify me when...")%> </p>
            </div>

            <div class="notify-row2">
                <p class="notify-me-"><%:Html.TranslateTag ("Device is inactive") %></p>
                <div class=" form-switch   ps-0">
                    <input class="form-check-input mx-2" type="checkbox" id="toggle-event" name="DeviceInactive">
                </div>
            </div>

            <div class="notify-title">
                <p>
                    <%:Html.TranslateTag ("How would you like to be notified")%>
                </p>
            </div>
            <div class="notify-me">
                <div class="circle-notify">
                    <div><%=Html.GetThemedSVG("send") %>  </div>
                </div>
                <div id="myBtn1" class="circle-notify " typeof="button">
                    <div  class="notified-img"><%=Html.GetThemedSVG("comment") %>  </div>
                </div>
                <div class="circle-notify" value="html">
                    <div class="notified-img"><%=Html.GetThemedSVG("none") %>  </div>
                </div>
            </div>

            <div class="notify-me">
                <p class="notify-item"><%:Html.TranslateTag ("Email")%></p>
                <p class="notify-item"><%:Html.TranslateTag ("SMS")%></p>
                <p class="notify-item"><%:Html.TranslateTag ("None")%></p>
            </div>
<%-----------------------------------------------------------------%>
            <!-- Trigger/Open The Modal -->


<!-- The Modal -->
<div id="myModal1" class="modal-phone">

  <!-- Modal content -->
  <div class="modal-content-phone">
    <span class="close-me">&times;</span>
    <div class="phone_container-me">
    <p><%:Html.TranslateTag ("Please update phone number to recieve text notifications")%></p>


           <%: Html.TranslateTag("SMS Provider","SMS Provider")%>
                <select id="UISMSCarrierID" name="UISMSCarrierID" class="UISMSCarrierID sms-dropdown">
                    <option value="0"><%: Html.TranslateTag("Settings/UserNotification|Select One","Select One")%></option>         
                    <option value=""><%:Html.TranslateTag ("Carrier")%></option>         
                </select>

        <div class="tel-input ">
                <%: Html.TranslateTag("Mobile Number","Mobile Number")%>:
               <div class="input-group input-field mb-0">
                   <input id="NotificationPhone" onkeydown="phoneNumberFormatter()" name="NotificationPhone" type="text" class="form-control me-0 " <%--value="<%: Model.NotificationPhone %>"--%>>
                   <button class="btn btn-primary" id="testSMS" title="<%:Html.TranslateTag("Send Test","Send Test")%>" value="Test" style="cursor: pointer; background-color: #AD7011">
                       <%=Html.GetThemedSVG("send") %>
                   </button>
               </div>
            <button class="btn btn-primary  update-btn"><%:Html.TranslateTag ("Update")%></button>
</div>
        </div>
  </div>

</div>

         

<%-------------------------------------------------------%>







            <button class="btn btn-primary  btn-end"><%:Html.TranslateTag ("Complete")%></button>

        </div>
    </div>


    <style>
        .active1 {
            background-color: rgb(17,104,173);
        }
    </style>
    <script>

        $(function () {

            $(".circle-notify").click(function (e) {

                if ($(this).hasClass('active1')) {
                    $(this).removeClass('active1');
                }
                else {
                    $(this).addClass('active1');
                }

            });

        })










        // Get the modal
        var modal = document.getElementById("myModal1");

        // Get the button that opens the modal
        var btn = document.getElementById("myBtn1");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close-me")[0];

        // When the user clicks on the button, open the modal
        btn.onclick = function () {
            modal.style.display = "block";
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }

        /*-------Phone Number Formatter-------*/

        function formatPhoneNumber(value) {
            if (!value) return value;
            const phoneNumber = value.replace(/[^\d]/g, '');
            const phoneNumberLength = phoneNumber.length;
            if (phoneNumberLength < 4) return phoneNumber;
            if (phoneNumberLength < 7) {
                return `(${phoneNumber.slice(0, 3)}) ${phoneNumber.slice(3)}`;
            }
            return `(${phoneNumber.slice(0, 3)}) ${phoneNumber.slice(3, 6,)}-${phoneNumber.slice(6, 9)}`;
        }

        function phoneNumberFormatter() {
            const inputField = document.getElementById('NotificationPhone');
            const formattedInputValue = formatPhoneNumber(inputField.value);
            inputField.value = formattedInputValue;
        }



    </script>

</asp:Content>
