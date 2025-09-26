<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    HomePage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%="" %>

    <%

//long networkID = MonnitSession.SensorListFilters.CSNetID;
//if (ViewBag.netID != null)
//    networkID = ViewBag.netID;
//CSNet network = CSNet.Load(networkID);
    %>

    <%--    <script type="text/javascript" src="https://fastly.jsdelivr.net/npm/echarts@5.4.1/dist/echarts.min.js"></script>--%>


    <div class="home_Grid">
        <%
            Customer customer = MonnitSession.CurrentCustomer;

            Boolean profileNotComplete = false;

            if (customer.FirstName.IsEmpty()
                || customer.LastName.IsEmpty())
                profileNotComplete = true;


            List<SystemHelp> systemHelps = SystemHelp.LoadByAccount(customer.AccountID);
            if (systemHelps.Count > 0 && profileNotComplete)
            { %>


        <%--          HIDE WELCOME FOR MOBILE--%>
        <div style="justify-content: center;" id="WA-Mobile">
            <div class="welcome-profile-account ">
                <div class="welcome-text-index">Welcome to your account</div>
                <div id="" class="animation-bounce  bounce-help" data-bs-toggle="modal" data-bs-target=".pageHelp">
                    <%=Html.GetThemedSVG("help") %>
                </div>
            </div>
        </div>
        <%} %>

        <%-- 
            <!----------------------------------------------------------------------------------------------
                                            Top Grid Container 
                                1. SensorPie / 2. GatewayPie / 3. Profile / 4. Reports
            --------------------------------------------------------------------------------------------------> 
        --%>
        <%--        <div id="top-card-container">--%>

        <% //Html.RenderPartial("_IndexSensorsChart"); %>
        <div class="sensorGrid top_card"></div>
        <script>
            $(document).ready(function () {
                $.get('/Overview/_IndexSensorsChart', function (data) {
                    $('.sensorGrid').html(data)
                })
            })
        </script>
        <% //Html.RenderPartial("_IndexGatewaysChart"); %>
        <div class="gatewayGrid  top_card"></div>
        <script>
            $(document).ready(function () {
                $.get('/Overview/_IndexGatewaysChart', function (data) {
                    $('.gatewayGrid').html(data)
                })
            })
        </script>
        <% //Html.RenderPartial("_IndexAccount"); %>
        <div class="userGrid  top_card" style="min-width: 351px; min-height: 160px; overflow-y: scroll;"></div>
        <script>
            $(document).ready(function () {
                $.get('/Overview/_IndexAccount', function (data) {
                    $('.userGrid').html(data)
                })
            })
        </script>
        <% //Html.RenderPartial("_IndexReports"); %>
        <div class="reportsGrid  top_card" style="min-width: 327px; overflow: hidden"></div>
        <script>
            $(document).ready(function () {
                $.get('/Overview/_IndexReports', function (data) {
                    $('.reportsGrid').html(data)
                })
            })
        </script>
        <%--        </div>--%> 


        <!-- ---------------------------🚫------------- END ------------------------------------------->

        <!----1------------------------------------------------------------------------------------------
                                                                          Left Side Containers
                                                                 Sensor List / Rules Triggered
            -------------------------------------------------------------------------------------------------->

        <% //Html.RenderPartial("_IndexDevices", Model); %>
        <div id="HomePageSensorCard" class="sensorlistGrid side_card "></div>
        <script>
            $(document).ready(function () {
                $.get('/Overview/_IndexDevices/<%: MonnitSession.CurrentCustomer.AccountID %>', function (data) {
                    $('.sensorlistGrid').html(data)
                })
            })
        </script>
        <% //Html.RenderPartial("_IndexRules", Model); %>
        <div id="HomePageRuleCard" class="rulesTriggeredGrid side_card" style="overflow-y: hidden"></div>
        <script>
            $(document).ready(function () {
                $.get('/Overview/_IndexRules/<%: MonnitSession.CurrentCustomer.AccountID %>', function (data) {
                    $('.rulesTriggeredGrid').html(data)
                })
            })
        </script>

        <!---------------------------🚫---------- End  ----------------------------------------------->
        <% //Html.RenderPartial("_IndexFavorites", Model); %>
        <div class="favoritesGrid"></div>
        <script>
            $(document).ready(function (thing) {
                $.get('/Overview/_IndexFavorites', function (data) {
                    $('.favoritesGrid').html(data)
                })
            })
        </script>

        <!-- ==============================================================
                                                                          FAVORITES CONTAINER
   =============================================================  -->


        <!-- FavGrid -->
    </div>

    <style type="text/css">
    	@media (max-width:768px) {
    		.accordion {
    			/* background-color: #eee;*/
    			border-radius: 5px 5px 0 0;
    			color: #444;
    			cursor: pointer;
    			padding: 0 18px 0 0;
    			width: 100%;
    			border: none;
    			text-align: left;
    			outline: none;
    			font-size: 15px;
    			transition: 0.4s;
    		}

    		.indexTabs, .ruleTabs {
    			border-radius: 5px 5px 0 0;
    			color: #444;
    			cursor: pointer;
    			padding: 0 18px 0 0;
    			width: 100%;
    			border: none;
    			text-align: left;
    			outline: none;
    			font-size: 15px;
    			transition: 0.4s;
    		}

    			.ruleTabs:after, .accordion:after {
    				content: '\276F';
    				color: #777;
    				font-weight: bold;
    				float: right;
    				margin-left: 5px;
    				transform: rotate(90deg);
    				transition: .5s ease-in-out !important;
    			}

    			.indexTabs:after {
    				content: '\276F';
    				color: #777;
    				font-weight: bold;
    				float: right;
    				margin-left: 5px;
    				transform: rotate(90deg);
    				transition: .5s ease-in-out !important;
    			}

    		.active:after {
    			content: '\276F';
    			transform: rotate(270deg);
    		}

    		.activeTab:after {
    			content: '\276F';
    			transform: rotate(270deg);
    		}

    		.containRule, .containMe {
    			overflow-y: scroll;
    			padding: 0 18px;
    			background-color: none;
    			/*       margin-top: 20px;*/
    			max-height: 0;
    			transition: max-height 1s ease-out;
    		}

    		.panel {
    			padding: 0 18px;
    			background-color: none;
    			margin-top: 20px;
    			max-height: 0;
    			transition: max-height 0.2s ease-out;
    			margin-bottom: 15px;
    			transition: .5s ease-in-out !important;
    		}

    		.report_name:hover {
    			color: var(--primary-color);
    			cursor: pointer;
    			font-weight: bold;
    		}

    		.home-icon-card > #signal-all {
    			width: 24px;
    		}
    	}
    	/*     animatedText*/
    	.animation-bounce {
    		animation-name: bounce;
    		animation-duration: 1s;
    		animation-iteration-count: infinite;
    	}

    	@keyframes bounce {
    		0% {
    			transform: translateY(0);
    		}

    		30% {
    			transform: translateY(-5px);
    		}

    		100% {
    			transform: translateY(0);
    		}
    	}

    	.welcome-animation-text {
    		margin-bottom: 10px;
    		font-weight: bold;
    		color: var(--primary-color);
    		margin-left: 14px;
    	}

    	.welcome-profile-account {
    		background: white;
    		width: 229px;
    		border-radius: 5px;
    		padding: 8px;
    		display: flex;
    		gap: 4px;
    		align-items: center;
    		justify-content: space-around;
    		box-shadow: rgba(0, 0, 0, 0.18) 0px 2px 4px;
    	}



    	.welcome-text-index {
    		font-weight: bold;
    		color: var(--primary-color);
    	}

    	#WA-Mobile {
    		display: none;
    		grid-row-start: 1;
    	}
    </style>

    <script type="text/javascript">

        /* Favorites header hide/show*/
        $(document).ready(function () {
            var panels = $(".panel");

            panels.each(function () {
                var list = $(this);
                var card = list.find(".small-list-card");

                if (card.length > 0) {
                    list.css("display", "grid");
                } else {
                    list.css("display", "none");
                }
            });
        });





        var areYouSureConfirmString = '<%= Html.TranslateTag("Are you sure you want to reset this rule?")%>';
        var areYouSureAckAllString = '<%= Html.TranslateTag("Are you sure you want to acknowledge this rule?")%>';
        var areYouSureDeleteString = '<%= Html.TranslateTag("Are you sure you want to delete this rule?")%>';
        var enableString = '<%= Html.TranslateTag("Enable")%>';
        var disableString = '<%= Html.TranslateTag("Disable")%>';
        var sendingString = '<%= Html.TranslateTag("Sending")%>';

        $(document).ready(function () {
            $('.ackBellListPage').hover(function () {
                $(this).removeClass('fa fa-bell-o').addClass('fa fa-bell-slash-o');
            }, function () {
                $(this).removeClass('fa fa-bell-slash-o').addClass('fa fa-bell-o');
            });

            $('.resetBellListPage').hover(function () {
                $(this).removeClass('fa fa-refresh').addClass('fa fa-check');
            }, function () {
                $(this).removeClass('fa fa-check').addClass('fa fa-refresh');
            });

            $('.clearSingleCertAck').click(function (e) {
                e.preventDefault();

                var obj = $(this).parent().parent();

                let values = {};
                values.text = '<%=Html.TranslateTag("Proceed with confirmation?")%>';
                values.url = this.href;
                values.callback = function (data) {
                    if (data == "Success") {
                        obj.hide();
                    } else {
                        showSimpleMessageModal(data);
                    }
                }
                openConfirm(values);
            });

            $('.ackAllBtn').click(function (e) {
                e.preventDefault();
                e.stopPropagation();

                AckAllButton(this);
            });
            $('.resetAllPendingBtn').click(function (e) {
                e.preventDefault();
                e.stopPropagation();

                ResetAllPending(this);
            });




        });

        function toggleRuleStatus(anchor) {
            var div = $(anchor).children('div.sensor');
            var enableString = "Enable"; // Make sure to define these variables or replace them with actual values
            var disableString = "Disable";

            if (div.hasClass("sensorStatusOK")) {
                $.get("/Rule/ToggleRule/" + $(anchor).data("id"), { "active": true }, function (data) {
                    if (data == "Success") {
                        div.addClass("sensorStatusInactive");
                        div.removeClass("sensorStatusOK");
                        $("#toggleText_" + $(anchor).data("id")).html(enableString);
                    }
                });
            } else {
                $.get("/Rule/ToggleRule/" + $(anchor).data("id"), { "active": false }, function (data) {
                    if (data == "Success") {
                        div.addClass("sensorStatusOK");
                        div.removeClass("sensorStatusInactive");
                        $("#toggleText_" + $(anchor).data("id")).html(disableString);
                    }
                });
            }
        }

        function clearTestMessage(notificationID) {
            $('#testMess').click();
            $('#testMessage_' + notificationID).html("");
            $("#sendTestListPage_" + notificationID).html('<i style="color: #51535b; margin-right: 0px; font-size: 1.2em;" class="fa fa-share-square-o"></i>');
        }

        function truncateName() {
            var elements = document.querySelectorAll("[data-shorty]");
            elements.forEach(function (element) {
                var name = element.innerHTML;
                if (name.length > 20) {
                    element.innerHTML = name.slice(0, 20) + "...";
                }
            });
        }

        window.onload = function () {
            truncateName();
        };



        /* -------------------------------------------------
                             Accordion Drop downs
         * ---------------------------------------------------------*/


    
            /*      Favorties container*/

            //var acc = document.getElementsByClassName("accordion");
            //var i;

            //if (window.innerWidth <= 769) {
            //    for (let i = 0; i < acc.length; i++) {
            //        acc[i].addEventListener("click", function () {
            //            var panel = this.nextElementSibling;
            //            if (panel.style.maxHeight) {
            //                panel.style.maxHeight = null;
            //                panel.style.overflow = "scroll";
            //            } else {
            //                panel.style.maxHeight = "fit-content";
            //                panel.style.overflow = "";
            //            }
            //        });
            //        acc[i].click();
            //    }
            //}


        ///*        main Containers on page*/
        //var indexTab = document.getElementsByClassName("indexTabs");
        //var tab;

        //for (tab = 0; tab < indexTab.length; tab++) {
        //    indexTab[tab].addEventListener("click", function () {
        //        this.classList.toggle("activeTab");
        //        var containMe = this.nextElementSibling;
        //        if (containMe.style.maxHeight) {
        //            containMe.style.maxHeight = null;
        //            containMe.style.marginTop = "0";

        //        } else {
        //            containMe.style.maxHeight = containMe.scrollHeight + "px";
        //            containMe.style.marginTop = "3%";

        //        }

        //    });
        //    indexTab[tab].click();
        //}

        ///*          rules triggered Container*/
        //var ruleTab = document.getElementsByClassName("ruleTabs");
        //var j;

        //for (j = 0; j < ruleTab.length; j++) {
        //    ruleTab[j].addEventListener("click", function () {
        //        this.classList.toggle("activeTab");
        //        var containRule = this.nextElementSibling;
        //        if (containRule.style.maxHeight) {
        //            containRule.style.maxHeight = null;
        //        } else {
        //            containRule.style.maxHeight = "190px";
        //        }
        //    });
        //    ruleTab[j].click();
        //}

        /*          End of Accordion*/

        //#region Favorite Section JS items
        function AckAllButton(anchor) {
            let values = {};
            values.text = areYouSureAckAllString;
            values.url = $(anchor).attr('href');
            values.callback = function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;
                }
                else if (data == "Unauthorized") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to acknowledge rules")%>");
                }
                else
                    showSimpleMessageModal("<%=Html.TranslateTag("Acknowledge rule failed.")%>");
            }

            openConfirm(values);
        }

        function ResetAllPending(anchor) {
            let values = {};
            values.text = areYouSureConfirmString;
            values.url = $(anchor).attr('href');
            values.callback = function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;
                }
                else if (data == "Unauthorized") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to reset rules")%>");
                }
                else
                    showSimpleMessageModal("<%=Html.TranslateTag("Reset Failed")%>");
            }
            openConfirm(values);
        }

        function SendTest(notificationID) {
            event.stopPropagation();
            $("#sendTestListPage_" + notificationID).html(sendingString + "...");
            $.post('/Notification/Test/' + notificationID, function (data) {
                $('#testMessage_' + notificationID).html(data);
            });
            setTimeout(clearTestMessage(notificationID), 5000)
        }

        function removeSensor(item) {
            let values = {};
            values.url = `/Network/RemoveSensor/${item}`;
            values.text = confirmRemoveSensor;
            openConfirm(values)
        }

        function removeGateway(item) {
            let values = {};
            values.url = `/CSNet/Remove/${item}`;
            values.text = "<%=Html.TranslateTag("Are you sure you want to remove this gateway from the network?","Are you sure you want to remove this gateway from the network?")%>";
            openConfirm(values);
        }

        function removeMap(map) {
            let values = {};
            values.url = `/Map/DeleteMap/${map}`;
            values.text = 'Are you sure you want to remove this map?';
            openConfirm(values);
        }

        function deleteConfirmation(item) {
            let values = {};
            values.url = `/Rule/Delete/${item}`;
            values.text = areYouSureDeleteString;
            openConfirm(values);
        }

        function toggleReportStatus(anchor) {
            var div = $(anchor).children('div.corp-status');
            if (div.hasClass("sensorStatusOK")) {
                $.post("/Export/SetActive", { "id": $(anchor).data("id"), "active": false }, function (data) {
                    if (data == "Success") {
                        div.addClass("sensorStatusInactive");
                        div.removeClass("sensorStatusOK");
                    }
                });
            }
            else {
                $.post("/Export/SetActive", { "id": $(anchor).data("id"), "active": true }, function (data) {
                    if (data == "Success") {
                        div.addClass("sensorStatusOK");
                        div.removeClass("sensorStatusInactive");
                    }
                });
            }
        }

        function removeReport(item) {
            let values = {};
            values.url = `/Export/Delete?id=${item}`;
            values.text = "<%: Html.TranslateTag("Export/ReportDetails|Are you sure you want to delete this report?","Are you sure you want to delete this report?")%>"
            openConfirm(values);
            e.stopImmediatePropagation();
        }

        function goToSensorList(status) {
            window.location.href = "/Overview/SensorIndex?id=-1&status=" + status;
        }

        function goToGatewayList(status) {
            window.location.href = "/Overview/GatewayIndex?id=-1&status=" + status;
        }



    </script>



</asp:Content>
