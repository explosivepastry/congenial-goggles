var sessionLength = 59 * 60000; //<%:(Session.Timeout - 1) * 60000 %>;//Logout one minute before session expires
var startTimeMS = 0;  // EPOCH Time of event count started
var sessionTimeout = null;
var logoutTimeout = null;

$(document).ready(function () {
    $('body').append('<div id="sessionEndDialog" title="Session Expiring" style="display: none;"><p>Your session is about to expire.  Click OK to renew your session or Logout to logout of the application.</p></div>');
    $('#sessionEndDialog').dialog({
        autoOpen: false,
        bgiframe: true,
        modal: true,
        buttons: {
            OK: function () {
                $(this).dialog('close');
                $.get('/Overview/Blank');
                resetSessionTimeout();
            },
            Logout: logout
        }
    });

    resetSessionTimeout();
});

function resetSessionTimeout() {
    if (sessionTimeout) clearTimeout(sessionTimeout);
    if (logoutTimeout) clearTimeout(logoutTimeout);
    startTimeMS = (new Date()).getTime();
    sessionTimeout = setTimeout(sessionExpiring, sessionLength);
}

function sessionExpiring() {
    logoutTimeout = setTimeout(logout, 30000); //warning dialog for 30 sec
    $('#sessionEndDialog').dialog('open');
}

function getRemainingTime() {
    return sessionLength - ((new Date()).getTime() - startTimeMS);
}

function logout() {
    window.location.href = '/Account/LogOff';
}
