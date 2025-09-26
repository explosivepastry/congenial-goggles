<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    RapidCalculator
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function addRow(tableID) {
            var table = document.getElementById(tableID);
            var rowCount = table.rows.length;
            var row = table.insertRow(rowCount);
            var colCount = table.rows[0].cells.length;

            for (var i = 0; i < colCount; i++) {
                var newRow = row.insertCell(i);

                newRow.innerHTML = table.rows[0].cells[i].innerHTML;
                newRow.childNodes[0].value = "10";
            }

            $($('.inputval')[0]).trigger('change');
        }

        function deleteRow(row) {
            var table = document.getElementById("data");
            var rowCount = table.rows.length;
            if (rowCount > 1) {
                var rowIndex = row.parentNode.parentNode.rowIndex;
                document.getElementById("data").deleteRow(rowIndex);
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("Please specify at least one value")%>");
            }

            var inputValObj = $('.inputval')[0];
            if (inputValObj != undefined) {
                $(inputValObj).trigger('change');
            }
        }

    </script>

    <div class="top-add-btn-row-left">
        <a class="back-icon add-btn-2" href="/Retail/MessageCredit/<%:Model.AccountID%>">
            <%=Html.GetThemedSVG("arrowLeft") %>
                &nbsp; &nbsp; <%: Html.TranslateTag("Retail/RapidCalculator|HX Credits","HX Credits")%>
        </a>
    </div>

    <div class="hb_container">

        <div class="HBheader">
            <div class="HBheader__top"><%: Html.TranslateTag("Retail/RapidCalculator|HX Messaging","HX Messaging")%></div>
            <div class="HBheader__middle"><%: Html.TranslateTag("Retail/RapidCalculator|Calculate your HX Package","Calculate your HX Package")%></div>
            <div class="HBheader__bottom" style="text-align: center;">
                <%: Html.TranslateTag("Retail/RapidCalculator|An iMonnit HX credit is equal to one heartbeat, or one data message. You may assign all of the sensors to have faster heartbeats, or assign a faster heartbeat to only a few sensors.","An iMonnit HX credit is equal to one heartbeat, or one data message. You may assign all of the sensors to have faster heartbeats, or assign a faster heartbeat to only a few sensors")%>. <br />
                <%: Html.TranslateTag("Retail/RapidCalculator|The more sensors assigned a faster heartbeat, the quicker the credit bundles are consumed. A credit will ONLY be consumed when a sensor heartbeat (data message) occurs that is set between 1 and 9 minutes.","The more sensors assigned a faster heartbeat, the quicker the credit bundles are consumed. A credit will ONLY be consumed when a sensor heartbeat (data message) occurs that is set between 1 and 9 minutes")%>.
            </div>
        </div>
        <%-------------------------------------
                          Heartbeat Calc & Add new
                    ---------------------------------------%>
        <div class="hb-calc-add">
            <div class="HBcalculator">
                <table id="data" style="width: 100%;">
                    <tr class="tableRow borderme" id="main">

                        <td class="HBcalculator__container">
                            <div class="HBcalculator__container__title">
                                 <%: Html.TranslateTag("Retail/RapidCalculator|Heartbeat Frequency","Heartbeat Frequency")%>
                            </div>
                            
                            <div class="HBcalculator__container__iContainer">
                                <div class="HBcalculator__iContainer__wrapper">
                                    <div class="HBcalculator__iContainer__wrapper__icon">
                                        <svg id="Group_112" data-name="Group 112" xmlns="http://www.w3.org/2000/svg" width="21" height="21" viewBox="0 0 21.545 21.075">
                                            <g id="Group_16" data-name="Group 16">
                                                <path id="ic_favorite_24px" d="M12.773,24.075l-1.562-1.516C5.663,17.2,2,13.658,2,9.317A6.071,6.071,0,0,1,7.925,3a6.3,6.3,0,0,1,4.848,2.4A6.3,6.3,0,0,1,17.62,3a6.071,6.071,0,0,1,5.925,6.317c0,4.341-3.663,7.879-9.211,13.254Z" transform="translate(-2 -3)" fill="#ff735a" />
                                            </g>
                                        </svg>
                                    </div>
                                </div>
                                <input type="number" value="10" min="1" class="heartbeat_qty inputval form-control user-dets" />
                            </div>
                        </td>

                        <td class="HBcalculator__container">
                            <div class="HBcalculator__container__title">
                                <%: Html.TranslateTag("Retail/RapidCalculator|Number of Sensors","Number of Sensors")%>
                            </div>
                            
                            <div class="HBcalculator__container__iContainer">
                                <div class="HBcalculator__iContainer__wrapper">
                                    <div class="HBcalculator__iContainer__wrapper__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="30" height="21" viewBox="0 0 30 22">
                                            <g id="Group_110" data-name="Group 110" transform="translate(-0.21 -49.516)">
                                                <g id="Group_20" data-name="Group 20" transform="translate(0.21 49.516)">
                                                    <g id="Group_17" data-name="Group 17">
                                                        <path id="Path_750" data-name="Path 750" d="M.214-2.145h6.6L9.542-6.414a1.654,1.654,0,0,1,1.032-.406,2.173,2.173,0,0,1,1.073.565L14.1,2.777l4.252-15.3a2.31,2.31,0,0,1,.991-.388,3.44,3.44,0,0,1,1.053.388L22.9-2.163h5.923s.738.532.681,1.006a2.058,2.058,0,0,1-.681.988l-7.182.088a1.346,1.346,0,0,1-.6-.212A1.328,1.328,0,0,1,20.667-.8l-1.424-5.61L15.218,8.669a2.462,2.462,0,0,1-1.114.423,2.152,2.152,0,0,1-.991-.423L10-2.48,8.758-.645a1.367,1.367,0,0,1-.578.476,3.305,3.305,0,0,1-.908.088H.214a2.279,2.279,0,0,1-.7-1.006C-.589-1.591.214-2.145.214-2.145Z" transform="translate(0.497 12.907)" fill="#fff" />
                                                    </g>
                                                </g>
                                            </g>
                                        </svg>
                                    </div>
                                </div>
                                <input type="number" value="1" min="1" class="sensor_qty inputval form-control user-dets" />
                            </div>
                        </td>

                        <td>
                            <div class="dfac bottomTableRow">
                                <span class="rowValues"></span>
                                <a type="button" value="<%: Html.TranslateTag("Retail/RapidCalculator|Delete","Delete")%>" onclick="deleteRow(this)">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="14" height="18" viewBox="0 0 14 18">
                                        <path id="ic_delete_24px" d="M6,19a2.006,2.006,0,0,0,2,2h8a2.006,2.006,0,0,0,2-2V7H6ZM19,4H15.5l-1-1h-5l-1,1H5V6H19Z" transform="translate(-5 -3)" fill="#ff4d2d" />
                                    </svg>
                                </a>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="HBaddNew">
                <div class="HBaddNew__text" onclick="addRow('data')">
                    <div class="HBaddNew__text__plus">+</div>
                    <%: Html.TranslateTag("Retail/RapidCalculator|Add New","Add New")%>
                </div>
            </div>
        </div>

        <%-- --------------------
                           CALENDAR
                   ------------------------%>

        <div class="  HBslider">

            <div class="HBslidecontainer">
                <div class="HBoutputContainer">
                    <div class="containSlide">
                        <input type="radio" name="slider" id="radioDay" name="calculateVal" value="<%: Html.TranslateTag("Retail/RapidCalculator|radioDay","radioDay")%>" checked>
                        <input type="radio" name="slider" id="radioMonth" name="calculateVal" value="<%: Html.TranslateTag("Retail/RapidCalculator|radioMonth","radioMonth")%>">
                        <input type="radio" name="slider" id="radioYear" name="calculateVal" value="<%: Html.TranslateTag("Retail/RapidCalculator|radioYear","radioYear")%>">

                        <div class="cards">
                            <label class="card11" for="<%: Html.TranslateTag("radioDay","radioDay")%>" id="cal-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 40 40">
                                    <g id="Group_1003" data-name="Group 1003" transform="translate(-172.529 -950)">
                                        <path id="ic_date_range_24px" d="M38.556,6H36.333V2H31.889V6H14.111V2H9.667V6H7.444a4.218,4.218,0,0,0-4.422,4L3,38a4.238,4.238,0,0,0,4.444,4H38.556A4.252,4.252,0,0,0,43,38V10A4.252,4.252,0,0,0,38.556,6Zm0,32H7.444V16H38.556Z" transform="translate(169.529 948)" />
                                        <text id="_1" data-name="1" transform="translate(188 981)" font-size="18" font-family="Roboto-Bold, Roboto" font-weight="700">
                                            <tspan x="0" y="0">1</tspan>
                                        </text>
                                    </g>
                                </svg>
                                <span id="totalqtyday"></span>
                            </label>

                            <label class="card11" for="<%: Html.TranslateTag("radioMonth","radioMonth")%>" id="cal-2">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 40 40">
                                    <g id="Group_1005" data-name="Group 1005" transform="translate(-172.529 -950)">
                                        <path id="ic_date_range_24px" d="M38.556,6H36.333V2H31.889V6H14.111V2H9.667V6H7.444a4.218,4.218,0,0,0-4.422,4L3,38a4.238,4.238,0,0,0,4.444,4H38.556A4.252,4.252,0,0,0,43,38V10A4.252,4.252,0,0,0,38.556,6Zm0,32H7.444V16H38.556Z" transform="translate(169.529 948)" />
                                        <text id="_30" data-name="30" transform="translate(184 981)" font-size="18" font-family="Roboto-Bold, Roboto" font-weight="700">
                                            <tspan x="0" y="0">30</tspan>
                                        </text>
                                    </g>
                                </svg>
                                <span class="HBradioSpan" id="totalqtymo"></span>
                            </label>

                            <label class="card11" for="<%: Html.TranslateTag("radioYear","radioYear")%>" id="cal-3">
                                <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" viewBox="0 0 40 40">
                                    <g id="Group_1004" data-name="Group 1004" transform="translate(-172.529 -950)">
                                        <path id="ic_date_range_24px" d="M38.556,6H36.333V2H31.889V6H14.111V2H9.667V6H7.444a4.218,4.218,0,0,0-4.422,4L3,38a4.238,4.238,0,0,0,4.444,4H38.556A4.252,4.252,0,0,0,43,38V10A4.252,4.252,0,0,0,38.556,6Zm0,32H7.444V16H38.556Z" transform="translate(169.529 948)" />
                                        <text id="_365" data-name="365" transform="translate(179 981)" font-size="18" font-family="Roboto-Bold, Roboto" font-weight="700">
                                            <tspan x="0" y="0">365</tspan>
                                        </text>
                                    </g>
                                </svg>
                                <span id="totalqtyyr"></span>
                            </label>
                        </div>

                        <div style="display: flex; width: 100%; justify-content: space-evenly; max-width: 430px;">
                        </div>
                    </div>
                </div>

                <div class="HBoutput">
                    <span><%: Html.TranslateTag("Retail/RapidCalculator|Overages:","Overages")%>:</span>
                    <div class="msg-overages ">
                        <span id="messageOverages"></span>
                        <div id="outputCalendar"></div>
                    </div>
                </div>
            </div>


            <%-- --------------------
                           PACKAGES
                   ------------------------%>

            <div class="packageLabel"></div>
            <div class="packageCards">

                <%----Card 1-----%>
                <div class=" l-container">
                    <div class="b-game-card">
                        <div class="b-game-card__cover">
                            <div class="packageCard__iconHolder">
                                <div class="packageCard__iconHolder__icon">

                                    <svg id="Group_72" data-name="Group 72" xmlns="http://www.w3.org/2000/svg" width="80" height="80" viewBox="0 0 90 90">
                                        <g id="circle-ballon" data-name="Group 18">
                                            <g id="Group_2" data-name="Group 2">
                                                <circle id="Ellipse_1" data-name="Ellipse 1" cx="45" cy="45" r="45" fill="rgba(125,125,126,0.74)" />
                                            </g>
                                        </g>
                                        <g id="balloons" transform="translate(31.431 24.138)">
                                            <path id="Path_752" data-name="Path 752" d="M60.576,41,58.1,35.729s7.271-6.374,9.961-10.971A20.54,20.54,0,0,0,71.037,15c0-8.284-6.044-15-13.5-15s-13.5,6.716-13.5,15a20.56,20.56,0,0,0,2.72,9.294c2.39,4.506,10.259,11.444,10.259,11.444L54.473,41Z" transform="translate(-44.037)" fill="#fff" />
                                        </g>
                                    </svg>
                                </div>
                            </div>

                            <div class="packageCard__title">Premiere</div>
                            <div class="packageCard__results">
                                <div class="packageCard__results__top">
                                    <div class="packageCard__results__top__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16">
                                            <path id="ic_favorite_24px" d="M10,19,8.84,17.849C4.72,13.777,2,11.092,2,7.8A4.566,4.566,0,0,1,6.4,3,4.64,4.64,0,0,1,10,4.822,4.64,4.64,0,0,1,13.6,3,4.566,4.566,0,0,1,18,7.8c0,3.3-2.72,5.981-6.84,10.062Z" transform="translate(-2 -3)" fill="#ff735a" />
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__top__text">
                                       <%: Html.TranslateTag("Retail/RapidCalculator|Over 10 Min Included","Over 10 Min Included")%>
                                    </div>
                                </div>

                                <div class="packageCard__results__bottom">
                                    <div class="packageCard__results__bottom__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 18 18">
                                            <g id="Group_130" data-name="Group 130" transform="translate(-3.129 -25)">
                                                <path id="ic_query_builder_24px" d="M10.991,2A9,9,0,1,0,20,11,9,9,0,0,0,10.991,2ZM11,18.2A7.2,7.2,0,1,1,18.2,11,7.2,7.2,0,0,1,11,18.2Zm.45-11.7H10.1v5.4l4.725,2.835.675-1.107-4.05-2.4Z" transform="translate(1.129 23)" fill="#fff" />
                                            </g>
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__bottom__text">
                                        <%: Html.TranslateTag("Retail/RapidCalculator|Over 10 Minutes Included","Over 10 Min Included")%>"
                                    </div>
                                </div>
                            </div>

                            <div class="DBaddBtn__container">
                                <div class="DBaddBtn">
                                    <div class="DBaddBtn__text">Included</div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--   ---  CARD 2  ------%>

                    <div class="b-game-card">
                        <div class="b-game-card__cover" style="background-image: linear-gradient(120deg, #2585C5 0%, #0067ab 100%)">
                            <div class="packageCard__iconHolder">
                                <div class="packageCard__iconHolder__icon">
                                    <svg id="Group_73" data-name="Group 73" xmlns="http://www.w3.org/2000/svg" width="80" height="80" viewBox="0 0 90 90">
                                        <g id="Group_18" data-name="Group 18">
                                        <g id="Group_2" data-name="Group 2">
                                            <circle id="Ellipse_1" data-name="Ellipse 1" cx="45" cy="45" r="45" fill="#0766a6" />
                                        </g>#0766a6
                                    </g>
                                        <path id="paper-plane-solid" d="M37.2.222.986,21.108a1.876,1.876,0,0,0,.172,3.374l8.306,3.484L31.911,8.189a.468.468,0,0,1,.672.648L13.761,31.762V38.05a1.875,1.875,0,0,0,3.321,1.234l4.961-6.038,9.735,4.077A1.88,1.88,0,0,0,34.357,35.9L39.982,2.159A1.875,1.875,0,0,0,37.2.222Z" transform="translate(23.436 25.558)" fill="#fff" />
                                    </svg>
                                </div>
                            </div>

                            <div class="packageCard__title"><%: Html.TranslateTag("Retail/RapidCalculator|250 Thousand","250 Thousand")%></div>
                            <div class="packageCard__results">
                                <div class="packageCard__results__top">
                                    <div class="packageCard__results__top__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16">
                                            <path id="ic_favorite_24px" d="M10,19,8.84,17.849C4.72,13.777,2,11.092,2,7.8A4.566,4.566,0,0,1,6.4,3,4.64,4.64,0,0,1,10,4.822,4.64,4.64,0,0,1,13.6,3,4.566,4.566,0,0,1,18,7.8c0,3.3-2.72,5.981-6.84,10.062Z" transform="translate(-2 -3)" fill="#ff735a" />
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__top__text">
                                        <%: Html.TranslateTag("Retail/RapidCalculator|250k Added Heartbeats","250k Added Heartbeats")%>
                                    </div>
                                </div>

                                <div class="packageCard__results__bottom">
                                    <div class="packageCard__results__bottom__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 18 18">
                                            <g id="Group_130" data-name="Group 130" transform="translate(-3.129 -25)">
                                                <path id="ic_query_builder_24px" d="M10.991,2A9,9,0,1,0,20,11,9,9,0,0,0,10.991,2ZM11,18.2A7.2,7.2,0,1,1,18.2,11,7.2,7.2,0,0,1,11,18.2Zm.45-11.7H10.1v5.4l4.725,2.835.675-1.107-4.05-2.4Z" transform="translate(1.129 23)" fill="#fff" />
                                            </g>
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__bottom__text">
                                        <span id="duration1"></span>
                                    </div>
                                </div>
                            </div>

                            <div class="DBaddBtn__container">
                                <div class="DBaddBtn">
                                    <a href="/Retail/Checkout/<%=Model.AccountID%>?productType=HxCredit&sku=MNW-HX-250K">
                                        <div class="DBaddBtn__btn" style="color: #2585C5;"><%: Html.TranslateTag("Retail/RapidCalculator|Purchase","Purchase")%></div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--Card 3--%>

                    <div class="b-game-card">
                        <div class="b-game-card__cover" style="background-image: linear-gradient(120deg, #0067ab 0%, #074D7B 100%)">
                            <div class="packageCard__iconHolder">
                                <div class="packageCard__iconHolder__icon">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="80" height="80" viewBox="0 0 90 90">
                                        <g id="Group_201" data-name="Group 201" transform="translate(-55.333 -25)">
                                            <g id="Group_2" data-name="Group 2" transform="translate(55.333 25)">
                                                <circle id="Ellipse_1" data-name="Ellipse 1" cx="45" cy="45" r="45" fill="#054672" />
                                            </g>
                                            <g id="ic_flight_24px" transform="translate(101.549 39.011) rotate(42)">
                                                <path id="Path_93" data-name="Path 93" d="M0,0" transform="translate(17.221 14.7)" fill="#fff" />
                                                <path id="Path_94" data-name="Path 94" d="M40,29.4V25.2L23.158,14.7V3.15a3.158,3.158,0,0,0-6.316,0V14.7L0,25.2v4.2l16.842-5.25V35.7l-4.211,3.15V42L20,39.9,27.368,42V38.85L23.158,35.7V24.15Z" transform="translate(0 0)" fill="#fff" />
                                            </g>
                                        </g>
                                    </svg>
                                </div>
                            </div>

                            <div class="packageCard__title">1.3 Million</div>
                            <div class="packageCard__results">
                                <div class="packageCard__results__top">
                                    <div class="packageCard__results__top__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16">
                                            <path id="ic_favorite_24px" d="M10,19,8.84,17.849C4.72,13.777,2,11.092,2,7.8A4.566,4.566,0,0,1,6.4,3,4.64,4.64,0,0,1,10,4.822,4.64,4.64,0,0,1,13.6,3,4.566,4.566,0,0,1,18,7.8c0,3.3-2.72,5.981-6.84,10.062Z" transform="translate(-2 -3)" fill="#ff735a" />
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__top__text">
                                         <%: Html.TranslateTag("Retail/RapidCalculator|1.3M Added Heartbeats","1.3M Added Heartbeats")%>
                                    </div>
                                </div>

                                <div class="packageCard__results__bottom">
                                    <div class="packageCard__results__bottom__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 18 18">
                                            <g id="Group_130" data-name="Group 130" transform="translate(-3.129 -25)">
                                                <path id="ic_query_builder_24px" d="M10.991,2A9,9,0,1,0,20,11,9,9,0,0,0,10.991,2ZM11,18.2A7.2,7.2,0,1,1,18.2,11,7.2,7.2,0,0,1,11,18.2Zm.45-11.7H10.1v5.4l4.725,2.835.675-1.107-4.05-2.4Z" transform="translate(1.129 23)" fill="#fff" />
                                            </g>
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__bottom__text">
                                        <span id="duration2"></span>
                                    </div>
                                </div>
                            </div>

                            <div class="DBaddBtn__container">
                                <div class="DBaddBtn">
                                    <a href="/Retail/Checkout/<%=Model.AccountID%>?productType=HxCredit&sku=MNW-HX-1M">
                                        <div class="DBaddBtn__btn" style="color: #0067ab;"><%: Html.TranslateTag("Retail/RapidCalculator|Purchase","Purchase")%></div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%--Card 4--%>
                    <div class="b-game-card">
                        <div class="b-game-card__cover" style="background-image: linear-gradient(120deg, #074D7B 0%,#053a5c 100%)">
                            <div class="packageCard__iconHolder">
                                <div class="packageCard__iconHolder__icon">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="80" height="80" viewBox="0 0 90 90">
                                        <g id="Group_202" data-name="Group 202" transform="translate(-55.333 -25)">
                                            <g id="Group_2" data-name="Group 2" transform="translate(55.333 25)">
                                                <circle id="Ellipse_1" data-name="Ellipse 1" cx="45" cy="45" r="45" fill="#05283e" />
                                            </g>
                                            <path id="iconmonstr-rocket-12" d="M14.019,10.876c-2.994-.016-6.755,1.606-9.623,4.475a15.491,15.491,0,0,0-2.8,3.8c2.588-1.958,5.35-2.573,8.58-.76A37.922,37.922,0,0,1,14.019,10.876Zm17.124,17.1a41.238,41.238,0,0,1-7.52,3.867c1.813,3.234,1.2,5.992-.759,8.579a15.406,15.406,0,0,0,3.806-2.8c2.877-2.874,4.5-6.643,4.473-9.642ZM41.909.073Q40.827,0,39.786,0C24.733,0,16.165,11.476,13.046,20.708l8.274,8.276C30.819,25.578,42,17.327,42,2.433v-.1q-.005-1.1-.091-2.259ZM24.6,17.416a1.75,1.75,0,1,1,2.475,0,1.748,1.748,0,0,1-2.475,0Zm4.949-4.949a3.5,3.5,0,1,1,4.949,0A3.5,3.5,0,0,1,29.554,12.467ZM18.119,30.734C15.509,37.4,7.964,41.6,0,42c.383-7.506,4.65-15.183,11.62-17.768l1.409,1.41C5.449,30.464,4.886,35,4.865,37.149c2.219-.026,7.094-.6,11.861-7.812Z" transform="translate(79.371 46.82)" fill="#fff" />
                                        </g>
                                    </svg>
                                </div>
                            </div>

                            <div class="packageCard__title"><%: Html.TranslateTag("Retail/RapidCalculator|5.5 Million","5.5 Million")%></div>
                            <div class="packageCard__results">
                                <div class="packageCard__results__top">
                                    <div class="packageCard__results__top__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16">
                                            <path id="ic_favorite_24px" d="M10,19,8.84,17.849C4.72,13.777,2,11.092,2,7.8A4.566,4.566,0,0,1,6.4,3,4.64,4.64,0,0,1,10,4.822,4.64,4.64,0,0,1,13.6,3,4.566,4.566,0,0,1,18,7.8c0,3.3-2.72,5.981-6.84,10.062Z" transform="translate(-2 -3)" fill="#ff735a" />
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__top__text">
                                       <%: Html.TranslateTag("Retail/RapidCalculator|5.5M Added Heartbeats","5.5M Added Heartbeats")%>
                                    </div>
                                </div>

                                <div class="packageCard__results__bottom">
                                    <div class="packageCard__results__bottom__icon">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 18 18">
                                            <g id="Group_130" data-name="Group 130" transform="translate(-3.129 -25)">
                                                <path id="ic_query_builder_24px" d="M10.991,2A9,9,0,1,0,20,11,9,9,0,0,0,10.991,2ZM11,18.2A7.2,7.2,0,1,1,18.2,11,7.2,7.2,0,0,1,11,18.2Zm.45-11.7H10.1v5.4l4.725,2.835.675-1.107-4.05-2.4Z" transform="translate(1.129 23)" fill="#fff" />
                                            </g>
                                        </svg>
                                    </div>

                                    <div class="packageCard__results__bottom__text">
                                        <span id="duration3"></span>
                                    </div>
                                </div>
                            </div>

                            <div class="DBaddBtn__container">
                                <div class="DBaddBtn">
                                    <a href="/Retail/Checkout/<%=Model.AccountID%>?productType=HxCredit&sku=MNW-HX-5M">
                                        <div class="DBaddBtn__btn" style="color: #074D7B;"><%: Html.TranslateTag("Retail/RapidCalculator|Purchase","Purchase")%></div>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </div>

        <style type="text/css">

            @import url("https://fonts.googleapis.com/css?family=DM+Sans:400,500,700&display=swap");
            * {
                box-sizing: border-box;
            }

            .card11 > svg {
                fill: var(--primary-color);
                width: 61px;
                height: 61px;
            }

            input[type=radio] {
                display: none;
            }

            .card11 {
                position: absolute;
                width: 60%;
                height: 100%;
                left: 0;
                right: 0;
                margin: auto;
                transition: transform .4s ease;
                cursor: pointer;
            }


            .cards {
                position: relative;
                width: 100%;
                height: 100%;
                margin-bottom: 20px;
            }


            #radioDay:checked ~ .cards #cal-3, #radioMonth:checked ~ .cards #cal-1, #radioYear:checked ~ .cards #cal-2 {
                transform: translatex(-40%) scale(.8);
                opacity: .4;
                z-index: 0;
            }

            #radioDay:checked ~ .cards #cal-2, #radioMonth:checked ~ .cards #cal-3, #radioYear:checked ~ .cards #cal-1 {
                transform: translatex(40%) scale(.8);
                opacity: .4;
                z-index: 0;
            }

            #radioDay:checked ~ .cards #cal-1, #radioMonth:checked ~ .cards #cal-2, #radioYear:checked ~ .cards #cal-3 {
                transform: translatex(0) scale(1);
                opacity: 1;
                z-index: 1;
            }
        </style>

        <script type="text/javascript">

            $(document).ready(function () {
                $('body').on('change', '.inputval', function () {
                    var rows = document.getElementsByClassName("tableRow");
                    var outputCalendar = document.getElementById("outputCalendar")
                    var radioDay = document.getElementById("radioDay")
                    var radioMonth = document.getElementById("radioMonth");
                    var radioYear = document.getElementById("radioYear");
                    var outputTotalVal = document.getElementById("outputTotalVal");

                    var days = 1;
                    if ($(radioDay).is(':checked')) {
                        days = 1;
                    } else if ($(radioMonth).is(':checked')) {
                        days = 30;
                    } else if ($(radioYear).is(':checked')) {
                        days = 365;
                    }

                    var qty = 0;
                    var qtyInputs = $(".sensor_qty");
                    var hb = 0;
                    var hbInputs = $(".heartbeat_qty");
                    var rowTotals = 0;
                    var rowTotalObjs = $(".rowValues");

                    var allowedDaily = 150;
                    var overageTotal = 0;
                    var dailyOverageTotal = 0;

                    for (var i = 0; i < qtyInputs.length; i++) {
                        var sensorQtyObj = $(qtyInputs[i]);
                        var sensorQty = Number(sensorQtyObj.val());
                        qty += sensorQty;

                        var heartbeatInput = $(hbInputs[i]);
                        var heartbeatValue = Number(heartbeatInput.val());
                        hb += heartbeatValue;

                        var rowObj = $(rowTotalObjs[i]);
                        var rowValue = Math.round((1440 / heartbeatValue) * sensorQty);
                        rowObj.html(rowValue);
                        rowTotals += rowValue;

                        if (heartbeatValue < 10) {
                            var messageCount = ((1440.0 * days) / heartbeatValue);
                            var dailyAllowed = allowedDaily * days;
                            var total = (messageCount - dailyAllowed) * sensorQty;
                            overageTotal += total;

                            var dailymessageCount = (1440.0 / heartbeatValue);
                            var dailydailyAllowed = allowedDaily;
                            var dailytotal = (dailymessageCount - dailydailyAllowed) * sensorQty;
                            dailyOverageTotal += dailytotal;
                        }
                    }
                    var row = $(".tableRow");
                    var rowCalc = 0;
                    var totalMessageCount = rowTotals * days;
                    var tableData = $("data");
                    $(row).append(rows);

                    $(outputTotalVal).html(totalMessageCount.toFixed(0));

                    if ($(radioDay).is(':checked')) {
                        $(outputCalendar).html("<span class='daymonthyearOutput'>/day</span>");
                        $('#messageOverages').html("<span class='totaldaymonthyearOutput'> " + overageTotal.toFixed(0) + "</span>");
                    } else if ($(radioMonth).is(':checked')) {
                        $(outputCalendar).html("<span class='daymonthyearOutput'>/month</span>");
                        $('#messageOverages').html("<span class='totaldaymonthyearOutput'> " + overageTotal.toFixed(0) + "</span>");
                    } else if ($(radioYear).is(':checked')) {
                        $(outputCalendar).html("<span class='daymonthyearOutput' >/year</span>");
                        $('#messageOverages').html("<span class='totaldaymonthyearOutput'> " + overageTotal.toFixed(0) + "</span>");
                    }


                    $("#messageOverages span").each(function () {
                        var num = $(this).text();
                        var commaNum = numberWithCommas(num);
                        $(this).text(commaNum);
                    });

                    $(".rowValues").each(function () {
                        var num = $(this).text();
                        var commaNum = numberWithCommas(num);
                        $(this).text(commaNum);
                    });


                    var maximumMessagesPerYear = allowedDaily * 365;
                    var packageLimit1 = maximumMessagesPerYear;
                    var packageLimit2 = 1299999; // 1,299,999
                    var packageLimit3 = 5499999; // 5,499,999

                    var packageSelector = 1;
                    if (overageTotal == 0) {
                        packageSelector = 1;
                    } else if (overageTotal < packageLimit2) {
                        packageSelector = 2;
                    } else if (overageTotal < packageLimit3) {
                        packageSelector = 3;
                    } else {
                        packageSelector = 4;
                    }

                    var smallpack = document.getElementById('duration1');
                    var medpack = document.getElementById('duration2');
                    var largepack = document.getElementById('duration3');
                    var package1 = 250000;
                    var package2 = 1300000;
                    var package3 = 5500000;

                    var duration250 = '';
                    var duration13 = '';
                    var duration55 = '';

                    var year250 = dailyOverageTotal == 0 ? 0 : package1 / (dailyOverageTotal * 365);
                    var year13 = dailyOverageTotal == 0 ? 0 : package2 / (dailyOverageTotal * 365);
                    var year55 = dailyOverageTotal == 0 ? 0 : package3 / (dailyOverageTotal * 365);
                    var month250 = dailyOverageTotal == 0 ? 0 : package1 / (dailyOverageTotal * 30);
                    var month13 = dailyOverageTotal == 0 ? 0 : package2 / (dailyOverageTotal * 30);
                    var month55 = dailyOverageTotal == 0 ? 0 : package3 / (dailyOverageTotal * 30);
                    var day250 = dailyOverageTotal == 0 ? 0 : package1 / dailyOverageTotal;
                    var day13 = dailyOverageTotal == 0 ? 0 : package2 / dailyOverageTotal;
                    var day55 = dailyOverageTotal == 0 ? 0 : package3 / dailyOverageTotal;
                    var hour250 = dailyOverageTotal == 0 ? 0 : package1 / (dailyOverageTotal / 12);
                    var hour13 = dailyOverageTotal == 0 ? 0 : package2 / (dailyOverageTotal / 12);
                    var hour55 = dailyOverageTotal == 0 ? 0 : package3 / (dailyOverageTotal / 12);

                    day250 = Math.floor(day250) - (Math.floor(month250) * 30);
                    month250 = Math.floor(month250) - (Math.floor(year250) * 12);
                    year250 = Math.floor(year250);
                    hour250 = Math.floor(hour250);

                    if (year250 > 0 && month250 > 0 && day250 > 0) {
                        duration250 += year250 + ' year(s) ' + month250 + ' month(s) ' + day250 + ' day(s)';
                    } else if (year250 > 0 && month250 > 0) {
                        duration250 += year250 + ' year(s) ' + month250 + ' month(s) ';
                    } else if (year250 > 0) {
                        duration250 += year250 + ' year(s)';
                    }
                    else if (month250 > 0 && day250 > 0) {
                        duration250 += month250 + ' month(s) ' + day250 + ' day(s)';
                    } else if (month250 > 0) {
                        duration250 += month250 + ' month(s) ';
                    }
                    else if (day250 > 0) {
                        duration250 += day250 + ' day(s)';
                    } else if (hour250 > 0) {
                        duration250 += hour250 + ' hour(s)';
                    } else if (overageTotal != 0) {
                        duration250 += "< 1 hour.  Call in!";
                    }

                    day13 = Math.floor(day13) - (Math.floor(month13) * 30);
                    month13 = Math.floor(month13) - (Math.floor(year13) * 12);
                    year13 = Math.floor(year13);
                    hour13 = Math.floor(hour13);

                    if (year13 > 0 && month13 > 0 && day13 > 0) {
                        duration13 += year13 + ' year(s) ' + month13 + ' month(s) ' + day13 + ' day(s)';
                    } else if (year13 > 0 && month13 > 0) {
                        duration13 += year13 + ' year(s) ' + month13 + ' month(s) ';
                    } else if (year13 > 0) {
                        duration13 += year13 + ' year(s)';
                    }
                    else if (month13 > 0 && day13 > 0) {
                        duration13 += month13 + ' month(s) ' + day13 + ' day(s)';
                    } else if (month13 > 0) {
                        duration13 += month13 + ' month(s) ';
                    }
                    else if (day13 > 0) {
                        duration13 += day13 + ' day(s)';
                    } else if (hour13 > 0) {
                        duration13 += hour13 + ' hour(s)';
                    } else if (overageTotal != 0) {
                        duration13 += "< 1 hour.  Call in!";
                    }

                    day55 = Math.floor(day55) - (Math.floor(month55) * 30);
                    month55 = Math.floor(month55) - (Math.floor(year55) * 12);
                    year55 = Math.floor(year55);
                    hour55 = Math.floor(hour55);

                    if (year55 > 0 && month55 > 0 && day55 > 0) {
                        duration55 += year55 + ' year(s) ' + month55 + ' month(s) ' + day55 + ' day(s)';
                    } else if (year55 > 0 && month55 > 0) {
                        duration55 += year55 + ' year(s) ' + month55 + ' month(s) ';
                    } else if (year55 > 0) {
                        duration55 += year55 + ' year(s)';
                    }
                    else if (month55 > 0 && day55 > 0) {
                        duration55 += month55 + ' month(s) ' + day55 + ' day(s)';
                    } else if (month55 > 0) {
                        duration55 += month55 + ' month(s) ';
                    }
                    else if (day55 > 0) {
                        duration55 += day55 + ' day(s)';
                    } else if (hour55 > 0) {
                        duration55 += hour55 + ' hour(s)';
                    } else if (overageTotal != 0) {
                        duration55 += "< 1 hour.  Call in!";
                    }

                    $(smallpack).html(duration250);
                    $(medpack).html(duration13);
                    $(largepack).html(duration55);
                });

                $('#radioDay').click(function () {
                    $('#outputCalendar').html("<span style='padding-left:10px;font-size:35px;color:#9E9D9D;''>/day</span>");
                    $($('.inputval')[0]).trigger('change');
                });

                $('#radioMonth').click(function () {
                    $('#outputCalendar').html("<span style='padding-left:10px;font-size:35px;color:#9E9D9D;''>/month</span>");
                    $($('.inputval')[0]).trigger('change');
                });

                $('#radioYear').click(function () {
                    $('#outputCalendar').html("<span style='padding-left:10px;font-size:35px;color:#9E9D9D;''>/year</span>");
                    $($('.inputval')[0]).trigger('change');
                });

                $($('.inputval')[0]).trigger('change');
            });

            function numberWithCommas(number) {
                var parts = number.toString().split(".");
                parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                return parts.join(".");
            }

            /* calendar Slider*/
            $('input').on('change', function () {
                $('body').toggleClass('blue');
            });


        </script>

</asp:Content>
