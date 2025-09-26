<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>



<%--
    
Toast Notification Documentation for Imonnit
The toastBuilder() function allows you to easily create success and/or error toasts within Imonnit. Follow the steps below to implement toast notifications:

Usage:
Navigate to the view where you want to add the toast notification.
In the corresponding JavaScript file, use the toastBuilder() function.
*If the error message is longer than 155 characters. A generic message will be displayed. This will prevent unintended messages from being displayed to the user.

Examples:
toastBuilder("Success");
toastBuilder("Your custom error message");
For error toasts, pass any other string value(other than Success) to the function. It is recommended to include a meaningful error message for better user understanding.
In cases when you would like a success message to be something other than Success. Pass a second parameter like: toastBuilder("Email sent", "Success"). This will create a green toast with a custom success message.

Live Example:
To observe the toast in action, follow these steps:
Visit https://staging.imonnit.com/Settings/UserPreference/38698.
Click the "Save" button on the form.
This will demonstrate the usage of toast notifications on the specified page.

--%>


<style>
    /* <--toast styles--> */
    .toast-wrapper-AB {
        display: flex;
        width: 340px;
        min-height: 50px;
        max-height: fit-content;
        border-radius: 1rem;
        align-items: center;
        justify-content: space-around;
        position: fixed;
        z-index: 2;
        color: white;
        top: 12%;
        left: 50%;
        transform: translate(-50%, -50%);
    }

        .toast-wrapper-AB div svg {
            fill: white;
            height: 24px;
            width: 24px;
        }

        .toast-wrapper-AB.success {
            background: #43be5ff0;
        }

        .toast-wrapper-AB.error {
            background: #f5504ef7;
        }

    .close-svg-wrapper-AB svg {
        fill: white;
        height: 15px !important;
        width: 15px !important;
    }

        .close-svg-wrapper-AB svg:hover {
            cursor: pointer;
            fill: #eee;
        }

    .scale-up-top {
        -webkit-animation: scale-up-top 0.4s cubic-bezier(0.390, 0.575, 0.565, 1.000) both;
        animation: scale-up-top 0.4s cubic-bezier(0.390, 0.575, 0.565, 1.000) both;
    }

    /**<-- entrance animation scale-up-top--> */
    @-webkit-keyframes scale-up-top {
        0% {
            -webkit-transform: scale(0) translate(-50%, -10%);
            transform: scale(0) translate(-50%, -10%);
        }

        100% {
            -webkit-transform: scale(1) translate(-50%, -10%);
            transform: scale(1) translate(-50%, -10%);
        }
    }

    @keyframes scale-up-top {
        0% {
            -webkit-transform: scale(0) translate(-50%, -10%);
            transform: scale(0) translate(-50%, -10%);
        }

        100% {
            -webkit-transform: scale(1) translate(-50%, -10%);
            transform: scale(1) translate(-50%, -10%);
        }
    }

    .scale-out-top {
        -webkit-animation: scale-out-top 0.5s cubic-bezier(0.550, 0.085, 0.680, 0.530) both;
        animation: scale-out-top 0.5s cubic-bezier(0.550, 0.085, 0.680, 0.530) both;
    }

    /** <--exit animation scale-out-top--> */
    @-webkit-keyframes scale-out-top {
        0% {
            -webkit-transform: scale(1) translate(-50%, -10%);
            transform: scale(1) translate(-50%, -10%);
            opacity: 1;
        }

        100% {
            -webkit-transform: scale(0) translate(-50%, -10%);
            transform: scale(0) translate(-50%, -10%);
            opacity: 1;
        }
    }

    @keyframes scale-out-top {
        0% {
            -webkit-transform: scale(1) translate(-50%, -10%);
            transform: scale(1) translate(-50%, -10%);
            opacity: 1;
        }

        100% {
            -webkit-transform: scale(0) translate(-50%, -10%);
            transform: scale(0) translate(-50%, -10%);
            opacity: 1;
        }
    }

    @media screen and (max-width: 450px) {
        .toast-wrapper-AB {
            top: 20%;
        }
    }

</style>

<script>
    const toastBuilder = (message, type) => {

        /*       <-- success/error toast -->*/
        const successToast = document.querySelector('#successToast');
        const errorToast = document.querySelector('#errorToast');
        const backendMessage = document.querySelector('#backendMessageOnToast')

        if (!successToast.classList.contains('d-none')) {
            hideElement('successToast');
        }

        if (!errorToast.classList.contains('d-none')) {
            hideElement('errorToast');
        }

        if (message.length > 1) { 
            if (type == 'Success') {
                const successCustomMessage = document.querySelector('.backendMessageOnSuccess');
                successCustomMessage.textContent = message;
                if (!successToast.classList.contains('d-none')) return
                successToast.classList.remove('d-none', 'scale-out-top');
                successToast.classList.add('scale-up-top');

                setTimeout(function () {
                    successToast.classList.add('scale-out-top');
                }, 1200);

                setTimeout(function () {
                    successToast.classList.add('d-none');
                }, 2600);
                return;
            }
            if (message == 'Success') {
                if (!successToast.classList.contains('d-none')) return
                successToast.classList.remove('d-none', 'scale-out-top');
                successToast.classList.add('scale-up-top');
                setTimeout(function () {
                    successToast.classList.add('scale-out-top');
                }, 1200);
                setTimeout(function () {
                    successToast.classList.add('d-none');
                }, 2600);
            } else {
                backendMessage.textContent = message;
                if (message.length > 157) {
                    backendMessage.textContent = "Error occurred. Please contact support if it persists.";
                }

                if (!errorToast.classList.contains('d-none')) return
                errorToast.classList.remove('d-none', 'scale-out-top');
                errorToast.classList.add('scale-up-top');
                setTimeout(function () {
                    errorToast.classList.add('scale-out-top');
                }, 20000);
                setTimeout(function () {
                    errorToast.classList.add('d-none');
                }, 22000);
            }
        }
    }

    function hideElement(elementId) {
        const elementToHide = document.querySelector(`#${elementId}`)
        elementToHide.classList.add('scale-out-top');
        setTimeout(function () {
            errorToast.classList.add('d-none');
        }, 1000);
    }
</script>

<%-- <-- success toast --> --%>
<div id="successToast" class="toast-wrapper-AB success d-none">
    <div><%=Html.GetThemedSVG("circle-check")%></div>
    <div class="backendMessageOnSuccess" style="color: white; margin: 0; font-size: 1.15rem; font-weight: 700;">Success!</div>
    <div onclick="hideElement('successToast')" class="close-svg-wrapper-AB"><%=Html.GetThemedSVG("close")%></div>
</div>

<%-- <-- error toast --> --%>
<div id="errorToast" class="toast-wrapper-AB error d-none">
    <div style="min-width: 30px"><%=Html.GetThemedSVG("error")%></div>
    <div id="backendMessageOnToast" style="color: white; margin: 0; max-width: 60%; font-size: 1.15rem; font-weight: 700; white-space: pre-wrap;"></div>
    <div onclick="hideElement('errorToast')" class="close-svg-wrapper-AB"><%=Html.GetThemedSVG("close")%></div>
</div>


