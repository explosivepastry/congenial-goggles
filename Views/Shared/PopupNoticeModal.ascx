<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%

    if (MonnitSession.CurrentCustomer != null && MonnitSession.CustomerCan("Customer_Can_Update_Firmware")
        && (
                (MonnitSession.HasOTASuiteGateways(MonnitSession.CurrentCustomer.AccountID) && MonnitSession.HasOTASuiteSensors(MonnitSession.CurrentCustomer.AccountID))
                ||
                MonnitSession.HasUpdateableGateways(MonnitSession.CurrentCustomer.AccountID)
            )
    && MonnitSession.CurrentCustomer.ShowPopupNotice
        )
    {
        PopupNoticeRecord record = null;
        // Only load if there are relevant devices eligible for upgrade
        if (MonnitSession.HasOTASuiteGateways(MonnitSession.CurrentCustomer.AccountID) && MonnitSession.HasOTASuiteSensors(MonnitSession.CurrentCustomer.AccountID))
        {
            record = MonnitSession.GetPopupNotice(ePopupNoticeType.SensorFirmwareUpdate);
        }
        else if (record == null && MonnitSession.HasUpdateableGateways(MonnitSession.CurrentCustomer.AccountID))
        {
            record = MonnitSession.GetPopupNotice(ePopupNoticeType.GatewayFirmwareUpdate);
        }
        
        if (record != null)
        {
            List<PopupNoticeRecord> ignoredFirmwareRecords = PopupNoticeRecord.LoadIgnoredFirmwareVersions(MonnitSession.CurrentCustomer.CustomerID, MonnitSession.CurrentCustomer.AccountID, record.PopupNoticeType);

            Dictionary<string, string[]> ignoredFirmware = ignoredFirmwareRecords
                .GroupBy(r => new { r.SKU })
                .Select(r => new { r.Key.SKU, FWVersions = r.Select(f => f.FirmwareVersionToIgnore).ToArray() })
                .ToDictionary(r => r.SKU, r => r.FWVersions);

            string message = string.Empty;
            string redirectLink = "/";
            if (record.PopupNoticeType == ePopupNoticeType.SensorFirmwareUpdate)
            {
                List<Sensor> updateableSnsrs = MonnitSession.CachedOTASuiteSensors(MonnitSession.CurrentCustomer.AccountID);
                List<Sensor> updateableSnsrsAfterIgnore = new List<Sensor>();
                foreach (Sensor snsr in updateableSnsrs)
                {
                    string[] fwVersionsToIgnore;
                    ignoredFirmware.TryGetValue(snsr.SKU, out fwVersionsToIgnore);
                    if (fwVersionsToIgnore == null || !fwVersionsToIgnore.Contains(snsr.FirmwareVersion))
                    {
                        updateableSnsrsAfterIgnore.Add(snsr);
                    }
                }
                if (updateableSnsrsAfterIgnore.Count > 0)
                {
                    message = "Update Sensors";
                    redirectLink = "/Network/SensorsToUpdate";
                }
                else
                {
                    record.DateLastSeen = DateTime.Now;
                    record.Save();
                    //MonnitSession.PopupNoticeRecords = null;
                    MonnitSession.CurrentCustomer.ShowPopupNotice = false;
                }
            }
            else if (record.PopupNoticeType == ePopupNoticeType.GatewayFirmwareUpdate)
            {
                List<Gateway> updateableGtwys = MonnitSession.CachedUpdateableGateways(MonnitSession.CurrentCustomer.AccountID);
                List<Gateway> updateableGtwysAfterIgnore = new List<Gateway>();
                bool ignoreThis = false;
                string[] fwVersionsToIgnore = System.Array.Empty<string>();

                foreach (Gateway gtwy in updateableGtwys)
                {
                    ignoreThis = false;
                    //fwVersionsToIgnore = null;

                    if (ignoredFirmware.TryGetValue(gtwy.SKU, out fwVersionsToIgnore))
                    {
                        if (fwVersionsToIgnore.Contains(gtwy.GatewayFirmwareVersion) || fwVersionsToIgnore.Contains(gtwy.APNFirmwareVersion))
                        {
                            ignoreThis = true;
                        }

                        //Array.Clear(fwVersionsToIgnore, 0, fwVersionsToIgnore.Length);
                    }

                    if (!ignoreThis)
                    {
                        updateableGtwysAfterIgnore.Add(gtwy);
                    }
                    //if (fwVersionsToIgnore == null || !(fwVersionsToIgnore.Contains(gtwy.GatewayFirmwareVersion) || fwVersionsToIgnore.Contains(gtwy.APNFirmwareVersion)))
                    //{
                    //    updateableGtwysAfterIgnore.Add(gtwy);
                    //}
                }
                if (updateableGtwysAfterIgnore.Count > 0)
                {
                    message = "Update Gateways";
                    redirectLink = "/Network/GatewaysToUpdate";
                    MonnitSession.CurrentCustomer.ShowPopupNotice = false;
                }
                else
                {
                    record.DateLastSeen = DateTime.Now;
                    record.Save();
                    //MonnitSession.PopupNoticeRecords = null;
                    MonnitSession.CurrentCustomer.ShowPopupNotice = false;
                }
            }

            if (!string.IsNullOrEmpty(message))
            {

%>

<style>
    .no-display {
        display: none;
    }

    .scale-in-ver-center {
        -webkit-animation: scale-in-ver-center 0.5s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
        animation: scale-in-ver-center 0.5s cubic-bezier(0.250, 0.460, 0.450, 0.940) both;
    }

    @-webkit-keyframes scale-in-ver-center {
        0% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }
    }

    @keyframes scale-in-ver-center {
        0% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }
    }


    .scale-out-vertical {
        -webkit-animation: scale-out-vertical 0.5s cubic-bezier(0.550, 0.085, 0.680, 0.530) both;
        animation: scale-out-vertical 0.5s cubic-bezier(0.550, 0.085, 0.680, 0.530) both;
    }

    @-webkit-keyframes scale-out-vertical {
        0% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }
    }

    @keyframes scale-out-vertical {
        0% {
            -webkit-transform: scaleY(1);
            transform: scaleY(1);
            opacity: 1;
        }

        100% {
            -webkit-transform: scaleY(0);
            transform: scaleY(0);
            opacity: 1;
        }
    }

    .AB-overlay {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: #00000094;
        z-index: 1100;
    }

    .userUpdateModal {
        width: 350px;
        height: 250px;
        position: fixed;
        top: 19%;
        left: 38%;
        background: white;
        border-radius: 15px;
        z-index: 1200;
        display: none;
        box-shadow: 2px 2px 6px rgb(0 0 0 / 35%);
        padding: .5rem;
    }

    .flex-column-with-spacejam {
        display: flex;
        flex-direction: column;
        justify-content: space-evenly;
        align-items: center;
    }

    .icon-wrapper svg {
        height: 50px;
        width: 50px;
        fill: var(--primary-color, #0469ad);
    }

    .button-wrapper-ab {
        display: flex;
        justify-content: space-around;
        width: 100%;
    }

    .btn-AB {
        padding: .5em;
        border-radius: 1rem;
        box-shadow: 1px 1px 3px rgb(0 0 0 / 35%);
        font-size: 1rem;
    }

    .orange-primary-btn-ab {
        color: white;
        background: var(--help-highlight-color, #f89725);
        border: none;
    }

        .orange-primary-btn-ab:hover {
            opacity: 0.8;
        }

    .blue-secondary-btn-ab {
        border: none;
        color: var(--primary-color, #0469ad);
        background: white;
        font-size: 1rem;
    }

        .blue-secondary-btn-ab:hover {
            opacity: 0.8;
        }

    .clickable:active {
        transform: scale(.9);
    }

    @media screen and (max-width: 575px) {
        .userUpdateModal {
            left: 17%;
        }
    }

    @media screen and (max-width: 425px) {
        .userUpdateModal {
            left: 1%;
        }
    }
</style>

<!-- Update Modal -->
<div class="AB-overlay no-display">
    <section id="userUpdateModal" class="userUpdateModal flex-column-with-spacejam scale-in-ver-center">
        <div class="icon-wrapper">
            <%=Html.GetThemedSVG("downloadFirmware") %>
        </div>

        <h1 style="font-size: 2rem;">Updates Available</h1>
        <p style="font-size: .8rem; font-weight: 600">
            Updating firmware will help better your experience. 
        </p>
        <div class="button-wrapper-ab">
            <button id="closeModal" class="blue-secondary-btn-ab clickable">Do it later...</button>
            <button id="ignoreVersion" class="blue-secondary-btn-ab clickable">Ignore This Version</button>
            <button id="closeModalOnRedirect" class="orange-primary-btn-ab btn-AB clickable"><%= message %></button>
        </div>
    </section>
</div>

<script>
    <%= ExtensionMethods.LabelPartialIfDebug("PopupNotificationModal.ascx") %>

    $(function () {
        $('#closeModal').click(function () {
            AcknowledgePopupNotice();
        });

        $('#ignoreVersion').click(function () {
            AcknowledgePopupNotice(true);
        });

        $('#closeModalOnRedirect').click(function () {
            AcknowledgePopupNotice()
                .then(function (data) {
                    if (data == "Success") {
                        window.location = '<%: redirectLink %>';
                    }
                });
        });

        function AcknowledgePopupNotice(ignoreVersion = false) {
            return $.post('/Admin/AcknowledgePopupNotice/', { customerID: '<%: MonnitSession.CurrentCustomer.CustomerID %>', accountID: '<%: MonnitSession.CurrentCustomer.AccountID %>', popupNoticeType: '<%: record.PopupNoticeType.ToInt() %>', ignoreVersion: ignoreVersion }, function (data) {
                if (data != "Success") {
                    showAlertModal(data);
                }
            });
        }
    });
</script>

<script>
    const createModal = (openButtonSelector, closeButtonSelector, overlaySelector, modalSelector, otherCloseTriggerSelector, ignoreThisVersionSelector) => {

        if (openButtonSelector.length > 1) {
            const openButton = document.querySelector(`#${openButtonSelector}`);
            const closeModalButton = document.querySelector(`#${closeButtonSelector}`);
            const ignoreVersionButton = document.querySelector(`#${ignoreThisVersionSelector}`);
            const overlayElement = document.querySelector(overlaySelector);
            const modal = document.querySelector(`#${modalSelector}`);
            const otherCloseButton = document.querySelector(`#${otherCloseTriggerSelector}`)

            const openModal = () => {
                overlayElement.classList.remove("no-display")
                modal.style.display = "flex";
                modal.classList.remove("scale-out-vertical");
            };

            const closeModal = () => {
                modal.classList.add("scale-out-vertical");
                modal.addEventListener("animationend", onAnimationEnd);

                function onAnimationEnd() {
                    modal.removeEventListener("animationend", onAnimationEnd);
                    overlayElement.classList.add("no-display");
                    modal.style.display = "none";
                }
            };


            if (openButtonSelector === "openOnPageLoad") {
                openModal()
            }

            if (openButton) {
                openButton.addEventListener("click", openModal);
            }

            closeModalButton.addEventListener("click", closeModal);
            /*            overlayElement.addEventListener("click", closeModal);*/
            ignoreVersionButton.addEventListener("click", closeModal);

            if (otherCloseButton) {
                otherCloseButton.addEventListener("click", closeModal);
            }
        }
    }

    if (!window.location.pathname.includes("/Network/SensorsToUpdate") && !window.location.pathname.includes("/Network/GatewaysToUpdate")) {
        createModal("openOnPageLoad", "closeModal", ".AB-overlay", "userUpdateModal", "closeModalOnRedirect", "ignoreVersion")
    } else {
        const modal = document.querySelector("#userUpdateModal");
        modal.style.display = "none";
    }
</script>
<%
            }
        }
    }
%>
