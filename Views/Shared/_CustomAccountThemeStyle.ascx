<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.AccountThemeStyleGroup>" %>

<%

    //Available Properties

    string PrimaryColor = MonnitSession.CurrentStyle("PrimaryColor");


    string LeftMenuHover = MonnitSession.CurrentStyle("LeftMenuHover");
    string MainMenuText = MonnitSession.CurrentStyle("MainMenuText");
    string MainMenuTextHover = MonnitSession.CurrentStyle("MainMenuTextHover");



    string OptionMenuColor = MonnitSession.CurrentStyle("OptionMenuColor");
    string OptionsIconColor = MonnitSession.CurrentStyle("OptionsIconColor");
    string OptionsTextColor = MonnitSession.CurrentStyle("OptionsTextColor");
    string OptionsHoverColor = MonnitSession.CurrentStyle("OptionsHoverColor");
    string OptionsIconHover = MonnitSession.CurrentStyle("OptionsIconHover");
    string OptionsTextHover = MonnitSession.CurrentStyle("OptionsTextHover");

    string PrimeBtnColor = MonnitSession.CurrentStyle("PrimeBtnColor");
    string PrimaryButtonHover = MonnitSession.CurrentStyle("PrimaryButtonHover");
    string PrimeBtnText = MonnitSession.CurrentStyle("PrimeBtnText");
    string PrimeBtnTextHover = MonnitSession.CurrentStyle("PrimeBtnTextHover");


    string SecondaryButton = MonnitSession.CurrentStyle("SecondaryButton");
    string SecondaryButtonHover = MonnitSession.CurrentStyle("SecondaryButtonHover");

    string CardColor = MonnitSession.CurrentStyle("CardColor");
    string CardTextColor = MonnitSession.CurrentStyle("CardTextColor");
    string CardIconColor = MonnitSession.CurrentStyle("CardIconColor");


    string HelpHighLight = MonnitSession.CurrentStyle("HelpHighLight");

    string ActiveSensors = MonnitSession.CurrentStyle("ActiveSensors");
    string AlertingSensors = MonnitSession.CurrentStyle("AlertingSensors");
    string ActiveGateways = MonnitSession.CurrentStyle("ActiveGateways");
    string AlertingGateways = MonnitSession.CurrentStyle("AlertingGateways");



    //string SecondaryColor = MonnitSession.CurrentStyle("SecondaryColor");//Removed from StyleSheet (For now)
    //DeleteMe
    //string CancelButton = MonnitSession.CurrentStyle("CancelButton");
    //string CancelButtonHover = MonnitSession.CurrentStyle("CancelButtonHover");

%>

<style>
    :root {
        --primary-color: <%:PrimaryColor%>;
        --secondary-color: <%:SecondaryButton%>;
        --secondary-color-hover: <%:SecondaryButtonHover%>;
        --active-gateways-color: <%:ActiveGateways%>;
        --active-sensors-color:<%:ActiveSensors%>;
        --mainMenu-text-color: <%:MainMenuText%>;
        --mainMenu-text-hover-color: <%:MainMenuTextHover%>;
        --mainMenu-hover-color: <%:LeftMenuHover%>;
        --card-background-color: <%:CardColor%>;
        --card-text-color: <%:CardTextColor%>;
        --card-icon-color: <%:CardIconColor%>;
        --option-menu-color: <%:OptionMenuColor%>;
        --options-icon-color: <%:OptionsIconColor%>;
        --options-text-color: <%:OptionsTextColor%>;
        --options-hover-color: <%:OptionsHoverColor%>;
        --options-icon-hover: <%:OptionsIconHover%>;
        --options-text-hover: <%:OptionsTextHover%>;
        --help-highlight-color: <%:HelpHighLight%>;
        --prime-btn-color: <%:PrimeBtnColor%>;
        --primary-color-hover: <%:PrimaryButtonHover%>;
        --prime-btn-text-color: <%:PrimeBtnText%>;
        --prime-btn-text-hover: <%:PrimeBtnTextHover%>;
    }

    .menuHover:hover > svg {
        fill: <%:MainMenuTextHover%>;
    }

    .small-list-card {
        background-color: <%:CardColor%>;
    }

    .card-edit-details, .stopProp, .card-text, .innerCard-holder__data, .network_small-title, .triggerDevice__name2, .activate-name, .card-data-name, .glance-text, .glance-name, .card-top-title, .temp-name {
        color: <%:CardTextColor%>;
    }

    .fa {
        color: <%:CardIconColor%>;
    }

    .icon-color svg {
        fill: <%:CardIconColor%>;
    }

    .fgw-icon svg {
        fill: #403f3f;
    }

    .pbh-color {
        color: <%:PrimaryButtonHover%>;
    }

    /* ---------New-------*/
    .btn-back {
        background-color: <%:SecondaryButton%>;
        color: white;
    }
    /* -----------------------*/
    /*Delete me*/
    .edit-btn {
        background: <%:SecondaryButton%>;
        color: white;
    }

        .edit-btn:hover {
            background: <%:SecondaryButtonHover%>;
            color: white;
        }



    /* imonnit 3.0 style */
    .login_image, .loginBtn__container__btn, .login_button {
        background-image: linear-gradient( to bottom right, <%:PrimaryColor%>, <%:PrimaryColor%> 151%);
    }

    .login_tab.current, .search-tabs__tab__active {
        color: <%:PrimaryColor%>;
        border-bottom: 2px solid <%:PrimaryColor%>;
    }

        .login_tab.current a, .search-tabs__tab:hover {
            color: <%:PrimaryColor%>;
        }

    .main_leftBar {
        background-color: <%:PrimaryColor%>;
    }

    .menuActive, .menuHover:hover {
        background-color: <%:LeftMenuHover%>;
        color: <%:MainMenuTextHover%> !important;
    }

        .menuActive, .menuHover:hover svg {
            fill: <%:MainMenuTextHover%> !important;
        }

            .menuActive .icon-fill, .menuActive .svg_icon,
            .menuHover:hover .icon-fill, .menuHover:hover .svg_icon {
                fill: <%:MainMenuTextHover%>;
            }


    .networkList_link[selected=selected] {
        color: #47494a;
        font-weight: bold;
        border-bottom: 2px solid <%:PrimaryColor%>;
    }

    .card_container__icon-stroke-1 {
        stroke: <%:PrimaryColor%>;
    }

    .btn-primary, .add-btn, .gen-btn, .btnRight_row a {
        background: <%:PrimeBtnColor%>!important;
        color: <%:PrimeBtnText%>!important;
    }

        .btn-primary:hover, .add-btn:hover, .gen-btn:hover, .btnRight_row a:hover {
            transition: .2s ease;
            color: <%:PrimeBtnTextHover%>;
            background: <%:PrimaryButtonHover%>!important;
            border-color: <%:PrimaryButtonHover%>;
        }

    .btn-secondary {
        background: <%:SecondaryButton%>;
        color: white;
    }

        .btn-secondary:hover {
            transition: .2s ease;
            color: white;
            background: <%:SecondaryButtonHover%>;
            border-color: <%:SecondaryButtonHover%>;
        }

    .btn-active-fill {
        background: <%:PrimaryColor%>;
        box-shadow: 0px 5px 5px 0px rgba(0, 0, 0, 0.1);
    }

    .btn-outline-primary {
        background: white;
        color: <%:PrimaryColor%>;
    }

        .btn-check:checked + .btn-outline-primary, .btn-outline-primary:hover {
            background: <%:PrimaryColor%>;
            border-color: <%:PrimaryColor%>;
            color: white;
        }
    /*
	.dash-btn, .btnRow_right__btn {
	    background: <%:SecondaryButton%>;
    }
    .dash-btn:hover, .btnRow_right__btn:hover {
	    background: <%:SecondaryButtonHover%>;
    }*/

    .view-btns div.active-hover-fill {
        background: <%:PrimaryColor%>;
    }

    .dropdown_newUI input {
        color: <%:PrimaryColor%>;
    }

    .gen-btn-2 {
        background: <%:PrimaryColor%>;
        background-color: <%:PrimaryColor%>;
    }

        .gen-btn-2:hover {
            background: <%:PrimaryColor%>;
            background-color: <%:PrimaryColor%>;
        }

    .btnRight_row a {
        background: <%:PrimaryColor%>;
    }

    .card_container__icon-fill-1, .card_container__top__icon {
        fill: <%:PrimaryColor%>;
    }

    .network_btn_selected {
        background: <%:PrimaryColor%>;
        background-color: <%:PrimaryColor%> !important;
    }

        .network_btn_selected:hover {
            background: <%:PrimaryButtonHover%>;
            border-color: <%:PrimaryButtonHover%>;
        }

    .main-page-icon-stroke {
        stroke: <%:PrimaryColor%>;
    }

    .tzInput:checked + .tzLabel {
        background: <%:PrimaryColor%>;
    }

    .networkList_link[selected=selected] {
        color: #47494a;
        font-weight: bold;
        border-bottom: 2px solid <%:PrimaryColor%>;
    }

    .main-page-icon-color {
        color: <%:PrimaryColor%>;
    }
    /*
    //JqueryUI only used in classic?
    .ui-tabs-active .ui-tabs-anchor {
        background: <%:PrimaryColor%>;
    }*/

    .tabs-left > li > a:hover {
        fill: <%:PrimaryColor%>;
    }

    .ui-tabs-active .ui-tabs-anchor:hover {
        color: <%:PrimaryButtonHover%>;
    }

    a.userPermissions_tabs:focus {
        background-color: #47494a !important;
    }

    .tabs-left > li > a:focus {
        fill: <%:PrimaryColor%>;
    }

    .btn-on {
        background-color: <%:PrimaryColor%>;
    }

    .ListBorderActive {
        /*border: 2px solid <%:PrimaryColor%>;*/
        /*      background-color: <%:PrimaryColor%>;*/
        color: #ffffff;
    }

    .db-1 {
        background-color: #2699FB;
        background-image: linear-gradient(#2699FB, #00000055);
    }

    .dc_activeSensors {
        background-color: #21CE99;
        background-image: linear-gradient(to bottom right, <%:ActiveSensors%>, #FFF 205%); /*Add back in when Gradient tool is built, or Secondary gradient field is added*/
    }

    .dc_alertingSensors {
        background-color: #E24E4E;
        background-image: linear-gradient(to bottom right, <%:AlertingSensors%>, #FFF 205%);
    }

    .dc_activeGateways {
        background-color: #2699FB;
        background-image: linear-gradient(to bottom right, <%:ActiveGateways%>, #FFF 205%);
    }

    .dc_alertingGateways {
        background-color: #9E66FE;
        background-image: linear-gradient(to bottom right, <%:AlertingGateways%>, #FFF 205%);
    }
</style>
