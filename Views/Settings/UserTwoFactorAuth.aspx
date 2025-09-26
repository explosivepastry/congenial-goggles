<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Monnit.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    User Settings
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">

        <%Html.RenderPartial("UserLink", Model); %>

        <div class="col-lg-6 col-12 ps-0 pe-lg-2 mb-4">
            <div class="x_panel col-md-12 shadow-sm rounded pe-lg-2" style="min-height: 560px;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Settings/UserTwoFactorAuth|Two-Factor Authentication","Two-Factor Authentication")%>
                        <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; color: #707070; font-size: small; position: sticky; margin-left: 30px;">
                            [<%= Model.FirstName%> <%= Model.LastName%>] - <%=Model.UserName%>
                        </div>
                    </div>
                </div>

                <div style="background: #eee; padding: 10px;">
                    <h5><%: Html.TranslateTag("Settings/UserTwoFactorAuth|Why is two-factor authentication (2FA) required","Why is two-factor authentication (2FA) required")%>?</h5>
                    <p><%: Html.TranslateTag("Settings/UserTwoFactorAuth|We recognize that stolen, reused and weak passwords remain a leading cause of security breaches. 2FA adds an extra layer of security to your account by requiring you to enter a verification code at login. This additional step along with your password reduces the risk of your account being taken over","We recognize that stolen, reused and weak passwords remain a leading cause of security breaches. 2FA adds an extra layer of security to your account by requiring you to enter a verification code at login. This additional step along with your password reduces the risk of your account being taken over")%>.</p>
                </div>

                <div class="accordion accordion-flush" id="accordionFlush">
                    <div class="accordion-item">
                        <h2 class="accordion-header mt-0" id="flush-headingOne">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseOne" aria-expanded="false" aria-controls="flush-collapseOne">
                                <b><%: Html.TranslateTag("SMS","SMS") %></b>
                            </button>
                        </h2>

                        <div id="flush-collapseOne" class="accordion-collapse collapse" aria-labelledby="flush-headingOne" data-bs-parent="#accordionFlush">
                            <div class="accordion-body">
                                <%if (Model.NotificationPhone != "")
                                    { %>
                                <p><%: Html.TranslateTag("Settings/UserTwoFactorAuth|When authenticating with SMS your message will be sent to","When authenticating with SMS your message will be sent to")%> <b><%=Model.NotificationPhone %>.</b></p>
                                <% }
                                    else
                                    { %>
                                <p><%: Html.TranslateTag("Settings/UserTwoFactorAuth|You currently do not have your SMS number setup","You currently do not have your SMS number setup")%>.</p>
                                <% } %>
                                <a type="button" href="/Settings/UserNotification/<%:Model.CustomerID %>/?focus" class="btn btn-primary btn-sm"><%= Model.NotificationPhone != "" ? "Update Number" : "Setup SMS" %></a>
                            </div>
                        </div>
                    </div>

                    <div class="accordion-item">
                        <h2 class="accordion-header mt-0" id="flush-headingTwo">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseTwo" aria-expanded="false" aria-controls="flush-collapseTwo">
                                <b><%: Html.TranslateTag("Email","Email")%></b>
                            </button>
                        </h2>
                        <div id="flush-collapseTwo" class="accordion-collapse collapse" aria-labelledby="flush-headingTwo" data-bs-parent="#accordionFlush">
                            <div class="accordion-body">
                                <p>
                                    <%: Html.TranslateTag("Settings/UserTwoFactorAuth|When authenticating by email your message will be sent to","When authenticating by email your message will be sent to")%> <b><%=Model.NotificationEmail %>.</b>
                                    <%: Html.TranslateTag("Settings/UserTwoFactorAuth|This email address is where you recieve email notifications","This email address is where you recieve email notifications")%>.
                                </p>
                                <a type="button" href="/Settings/UserNotification/<%:Model.CustomerID %>/?email" class="btn btn-primary btn-sm"><%: Html.TranslateTag("Settings/UserTwoFactorAuth|Change Email","Change Email")%></a>
                            </div>
                        </div>
                    </div>

                    <div class="accordion-item">
                        <h2 class="accordion-header mt-0" id="flush-headingThree">
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#flush-collapseThree" aria-expanded="false" aria-controls="flush-collapseThree">
                                <b><%: Html.TranslateTag("Settings/UserTwoFactorAuth|Authenticator App","Authenticator App")%></b>
                            </button>
                        </h2>

                        <div id="flush-collapseThree" class="accordion-collapse collapse" aria-labelledby="flush-headingThree" data-bs-parent="#accordionFlush">
                            <div class="accordion-body">
                                <%if (ViewBag.hideAuthApp == true)
                                    { %>

                                <h5 style="margin-top: 0;">This user has <%=Model.TOTPSecret != "" ? "" : "not" %> <%: Html.TranslateTag("Settings/UserTwoFactorAuth|setup their account with an Authentication App","setup their account with an Authentication App")%></h5>

                                <% }
                                    else
                                    { %>
                                <p><%: Html.TranslateTag("Settings/UserTwoFactorAuth|To enable Two-Factor Authentication","To enable Two-Factor Authentication via an Authenticator app, downloading the app from the Google Playstore, or Apple App Store. Or use an existing one on your device")%>.</p>
                                <a href="https://www.apple.com/us/search/Authenticator?page=1&sel=explore&src=serp" target="_blank">
                                    <img src="../../Content/images/appstore.png" width="100px" height="auto"></a>
                                <a href="https://play.google.com/store/search?q=Authenticator" target="_blank">
                                    <img src="../../Content/images/playstore.png" width="100px" height="auto"></a>

                                <div class="totpInfo">
                                    <%if (Model.TOTPSecret != "" && Model.TOTPSecret != null)
                                        { %>
                                    <a class="btn btn-primary btn-sm me-2" id="disabled"><%: Html.TranslateTag("Settings/UserTwoFactorAuth|Setup Authenticator App","Setup Authenticator App")%></a>
                                    <% } %>
                                    <a class="btn btn-primary btn-sm" id="generate"><%: Html.TranslateTag("Settings/UserTwoFactorAuth|Generate New Code","Generate New Code")%></a>
                                </div>

                                <form class="enabled" style="margin-left: 20px;">
                                    <div class="step">
                                        <div>
                                            <p>1</p>
                                        </div>
                                        <h3><%:Html.TranslateTag("Settings/UserTwoFactorAuth|Scan the barcode","Scan the barcode")%></h3>
                                    </div>

                                    <p><%:Html.TranslateTag("Settings/UserTwoFactorAuth|Scan the barcode below with your authenticator app or type in the code","Scan the barcode below with your authenticator app or type in the code")%>:</p>
                                    <div id="qrCode"></div>

                                    <p class="totpCode"></p>
                                    <div class="step step2">
                                        <div>
                                            <p>2</p>
                                        </div>
                                        <h3><%:Html.TranslateTag("Enter the verification code","Enter the verification code")%></h3>
                                    </div>

                                    <p><%:Html.TranslateTag("Settings/UserTwoFactorAuth|Once you have scanned the barcode or entered the above code, enter the 6-digit verification code generated by the authenticator application","Once you have scanned the barcode or entered the above code, enter the 6-digit verification code generated by the authenticator application")%>.</p>

                                    <div>
                                        <input class="form-control aSettings__input_input" id="code" name="Code" placeholder="<%:Html.TranslateTag("verification code","verification code")%>" required="required" type="number" />
                                    </div>

                                    <div class="form-group has-error">
                                        <p id="error"></p>
                                    </div>
                                </form>

                                <div style="margin-left: 20px;" class="form-group center enabled">
                                    <button class="gen-btn" id="submitBtn">
                                        <%:Html.TranslateTag("Settings/UserTwoFactorAuth|Verify Code","Verify Code")%>
                                    </button>
                                </div>

                                <p style="margin-left: 20px;" class="enabled"><strong><%:Html.TranslateTag("Settings/UserTwoFactorAuth|This device will be remembered for 90 days","This device will be remembered for 90 days")%>.</strong></p>
                                <% }  %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-6 col-12 px-0 ps-lg-2" id="rightCard">
            <div class="x_panel col-md-12 shadow-sm rounded scrollParentLarge ps-lg-2" style="min-height: 560px;">
                <div class="card_container__top">
                    <div class="card_container__top__title">
                        <%:Html.TranslateTag("Settings/UserTwoFactorAuth|Remembered Devices","Remembered Devices")%>
                    </div>
                </div>

                <div class="hasScroll">
                    <table class="table table-striped table-hover align-middle">
                        <thead>
                            <tr>
                                <th scope="col"></th>
                                <th scope="col"><%:Html.TranslateTag("Settings/UserTwoFactorAuth|Device Name","Device Name")%></th>
                                <th scope="col"><%:Html.TranslateTag("Settings/UserTwoFactorAuth|Last Logged In","Last Logged In")%></th>
                                <th scope="col"><%:Html.TranslateTag("Settings/UserTwoFactorAuth|Re-Auth In","Re-Auth In")%></th>
                                <th scope="col" id="deleteAllDevices">
                                    <%=Html.GetThemedSVG("delete") %>
                                </th>
                            </tr>
                        </thead>
                        <tbody>

                            <%
                                string SecretString = "";
                                HttpCookie deviceCookie = iMonnit.ControllerBase.AccountControllerBase.GetTwoFactorAuthCodeCookie(Model, System.Web.HttpContext.Current);
                                if (deviceCookie != null)
                                {
                                    SecretString = deviceCookie.Value.ToString();
                                }
                                foreach (AuthenticateDevice device in ViewBag.devices)
                                {
                                    if (device.CreateDate.AddDays(90) > DateTime.UtcNow)
                                    {
                                        TimeSpan difference = device.CreateDate.AddDays(90) - DateTime.UtcNow;
                                        bool CurrentDevice = StructuralComparisons.StructuralEqualityComparer.Equals(MonnitUtil.GenerateHash(SecretString, device.Salt, 1), device.DeviceHash);  //Change this work factor to device.WorkFactor after July 15 2022 
                            %>

                            <tr class="<%=CurrentDevice ? "info" : "" %>" title="<%=CurrentDevice ? "Current Device" : "" %>">
                                <td style="cursor: pointer; padding-right: 0;" class="editDevice" id="<%=device.AuthenticateDeviceID %>">
                                    <span title="<%: Html.TranslateTag("Edit Device","Edit Device")%>"><%=Html.GetThemedSVG("edit") %></span>
                                </td>
                                <td style="padding-left: 0;">
                                    <input style="border: none; background: transparent;" disabled name="name" onblur="update(<%=device.AuthenticateDeviceID %>)" id="editDevice_<%=device.AuthenticateDeviceID %>" value="<%=device.DisplayName %>" title="<%=device.DeviceName%>" />
                                    <i class="fa fa-check check_<%=device.AuthenticateDeviceID %>" onclick="update(<%=device.AuthenticateDeviceID %>)" style="color: green; font-size: 20px; cursor: pointer;"></i>
                                </td>
                                <td><%=Monnit.TimeZone.GetLocalTimeById(device.LastLoginDate, MonnitSession.CurrentCustomer.Account.TimeZoneID) %></td>
                                <td><%=difference.TotalDays.ToInt() %> <%: Html.TranslateTag("days","days")%></td>
                                <td style="cursor: pointer;" class="deleteDevice" id="<%=device.AuthenticateDeviceID %>"><%=Html.GetThemedSVG("delete") %>
                                </td>
                            </tr>
                            <% }
                                } %>
                        </tbody>
                    </table>

                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" src="../../Scripts/QRCode_Generator/qrcode.js"></script>

    <script type="text/javascript">

        var notSaved = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Device name was not saved.")%>";
        var failedDelete = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Device was not able to be removed.")%>";
        var removeDevice = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Are you sure you want to remove this device")%>?";
        var removeAllDevice = "<%: Html.TranslateTag("Settings/_Mobile DeviceList|Are you sure you want to remove all devices")%>?";

        $('.enabled').hide();
        $('.collapseContent').hide();
        $('.fa-check').hide();

        let editField;

        $('#disabled').click(function (event) {
            $('#generate').hide();
            $.ajax({
                url: '/Settings/EnableTOTP',
                data: { generate: false }
            }).then(e => {
                let value = JSON.parse(e);
                new QRCode(document.getElementById('qrCode'),
                    {
                        text: value.qr,
                        width: 120,
                        height: 120
                    });
                $('#disabled').hide();
                $('.totpCode').html(value.secretCode);
                $('.disabled').hide();
                $('.enabled').show();
            })
        })

        $('#generate').click(function (event) {
            $('#generate').hide();
            $.ajax({
                url: '/Settings/EnableTOTP',
                data: { generate: true }
            }).then(e => {
                let value = JSON.parse(e);
                new QRCode(document.getElementById('qrCode'),
                    {
                        text: value.qr,
                        width: 120,
                        height: 120
                    });
                $('#disabled').hide();
                $('.totpCode').html(value.secretCode);
                $('.disabled').hide();
                $('.enabled').show();
            })
        })

        $('.deleteDevice').click(function () {
            let values = {};
            values.url = `/Settings/DeleteRememberedDevice?id=${$(this).attr('id')}`;
            values.text = removeDevice;
            openConfirm(values);
        });

        $('#deleteAllDevices').click(function () {
            let values = {};
            var ids = '';
            var deviceCount = $('.deleteDevice').length;
            $('.deleteDevice').each(function (index) {
                if (index == deviceCount - 1)
                    ids += $(this).attr('id');
                else
                    ids += $(this).attr('id') + ",";
            });

            values.url = `/Settings/DeleteRememberedDevices?ids=${ids}`;
            values.text = removeAllDevice;
            openConfirm(values);
        }).hover(function () {
            $(this).css('cursor', 'pointer');
        });

        $('.editDevice').click(function () {
            var id = $(this).attr('id');
            let field = $(`#editDevice_${id}`);
            field.prop("disabled", false);
            editField = field.val();
            field.focus().val('');
            $(`.check_${id}`).show();
        });

        let update = (value) => {
            $('.fa-check').hide();
            let input = $(`#editDevice_${value}`);
            if (input.val().length < 1) {
                input.val(editField);
                input.prop("disabled", true);
            }
            else {
                $.ajax({
                    url: '/Settings/UpdateRememberedDevice',
                    data: { id: value, deviceName: input.val() },
                }).then(e => {
                    if (e == 'Success') {
                        window.location.href = window.location.href;
                    }
                    else {
                        $('#error').text(notSaved);
                    }
                })
            }
        }

        $("#submitBtn").click(function (event) {
            var code = $('#code').val();
            $.ajax({
                url: '/Settings/VerifyTotp',
                data: { code: code },
            }).then(e => {
                if (e == 'Success') {
                    window.location.href = window.location.href;
                }
                else {
                    $('#error').text(e);
                }
            })
        })

    </script>

    <style type="text/css">
        @media only screen and (min-width: 1200px) {
            #leftCard {
                padding-right: 5px;
            }

            #rightCard {
                padding-left: 5px;
            }
        }

        #svg_edit, #svg_delete {
            height: 15px;
            width: 15px;
            margin-top: -5px;
        }

        .collapseButton {
            width: 100%;
            border: .5px solid #ddd;
            height: 45px;
            padding-left: 20px;
            background: #fff;
            text-align: left;
            font-size: 1.2rem;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

            .collapseButton i {
                margin-right: 20px;
            }

        .collapseBtn {
            max-width: 250px;
            margin-right: 10px;
        }

        .totpInfo {
            display: flex;
            margin-top: 20px;
        }

        .table > body {
            vertical-align: middle !important;
        }

        .collapseContent {
            padding: 15px;
            background: #eee;
        }

        #sms {
            border-radius: 3px 3px 0 0;
        }

        #authApp {
            border-radius: 0 0 3px 3px;
        }

        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #error {
            color: red;
        }

        input[type=number] {
            -moz-appearance: textfield;
        }

        h2 {
            font-size: 30px;
        }

        form {
            width: 350px;
        }

        .step div {
            background: #0067ab;
            border-radius: 25px;
            color: white;
            width: 25px;
            height: 25px;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        .step {
            margin-top: 20px;
            display: flex;
            align-items: center;
        }

            .step div {
                to margin-right: 5px;
            }

            .step p {
                margin: auto;
            }

        .totpCode {
            padding: 5px;
            background: #ddd;
            text-align: center;
            margin: 10px 0;
            width: 100%;
        }

        .center {
            text-align: center;
        }
    </style>

</asp:Content>
