<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Blank.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    ProductActivation
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div id="fullForm" style="width: 100%;">
    <div class="formtitle">Product Activation</div>
    <div class="formBody">
        <h3>Offline Activation</h3>
        
        This feature will allow users to activate an offline software product.<br/><br/> 
        To access Manual Activation:
        <ol>
            <li style="margin-bottom: 3px;">Open <b>Enterprise Configuration Utility</b> - Enter Activation Key, and Click "Activate."<br/>
                (If the system is online, it will activate automatically, otherwise, it will generate a Manual Token).</li>
            <li style="margin-bottom: 3px;">Copy the <b>Manual Token</b> that was generated from the Enterprise Configuration Setup.</li>
            <li style="margin-bottom: 3px;">Paste Token into the input box below.</li> 
            <li style="margin-bottom: 3px;">Click on the "OK" button to generate a Manual Activation Code.</li>
        </ol>            
        <div style="width: 70%;">
            <br/>
            <b>Manual Token: </b><input type="text" id="key" placeholder="Paste Manual Token here" />
            <input type="button" id="btn" value="OK"/>
            
            <div>
                <br/>
                <ol start="5">
                    <li style="margin-bottom: 3px;">Copy the new Manual Activation Code (that will appear below).</li>
                    <li>Paste the new Code into the <b>Enterprise Configuration Utility</b> where it says "Manual Key."</li>
                    <li>Click the "Manual Activation" button.</li>
                </ol>
            </div>
            <br/>
            <b>Manual Activation Code:</b>
            <div id="content" />
        </div>        
    </div>
</div>

<script type="text/javascript">
    $('#btn').click(function () {
        var Key = $('#key').val();

        $.post("/Lookup/ProductActivationKey", {key : Key},function (data) {
            if(!data.includes("Failed"))
                $('#content').html('<h4>' + data + '<h4>');
            else
                $('#content').html('<span style="color:red"><b>' + data + '</b></span>');
        });
    });

</script>

</asp:Content>
