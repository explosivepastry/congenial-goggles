$(document).ready(function() {
	$('.accountButton').click(function(e) {
		e.preventDefault();
		$("#accountBox").toggle('fast',function() {
				$('#name').focus();
			});
		$(this).toggleClass("accountButtonOn");
		$('#msg').empty();
	});
	
	$('.accountButton').mouseup(function() {
		return false;
	});
	
	$(document).mouseup(function(e) {
		if($(e.target).parents('#accountBox').length==0) {
			$('.accountButton').removeClass('accountButtonOn');
			$('#accountBox').hide('fast');
		};
	});
});

$(document).ready(function() {
	$('.btnexpand').click(function(e) {
		e.preventDefault();
		$("#frmexpand").toggle('fast',function() {
				$('#username').focus();
			});
		$(this).toggleClass("btnexpandon");
		$('#msg').empty();
	});
	
	$('.btnexpand').mouseup(function() {
		return false;
	});
	
	$(document).mouseup(function(e) {
		if($(e.target).parents('#frmexpand').length==0) {
			$('.btnexpand').removeClass('btnexpandon');
			$('#frmexpand').hide('fast');
		};
	});
});