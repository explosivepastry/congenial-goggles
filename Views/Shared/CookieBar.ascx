<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<style>

.bar {
    bottom: 0;
    width: 100%;
    text-align: center;
    padding: 10px 0;
    background-color: rgba(0, 0, 0, 0.9);
    color: #fff;
    font: 16px arial, sans-serif;
    position: fixed;
    z-index: 10;
    }

#cookieLink {
  color: #09f;
}
#cookieLink:hover {
    color: #D21717;
}

.acceptBtn {
    background-color: #054AD1;
    border: none;
    color: white;
    padding: 5px 15px;
    font-size: 14px;
    text-align: center;
    text-decoration: none;
    border-radius: 4px;
    margin-left: 20px;
}    
.acceptBtn:hover {
    background-color: #256FD5;
    color: white;
    }

</style>


<script>

	$(function () {
		
		$(".acceptBtn").click(function () {
            $('.bar').fadeOut('slow');

            $.post('/Customer/CookieAcceptance', function (data) {
            });
        });
    });

</script>

<div id='cookieBar' class='bar'>
    <p>
        This website uses cookies to enhance your user experience. By using this site you are agreeing to our <a id="cookieLink" href="/Account/Cookie" target="_blank">cookie policy</a>.  
        <button type="button" class="acceptBtn" data-userid="">I ACCEPT</button>
    <p>  

</div>

