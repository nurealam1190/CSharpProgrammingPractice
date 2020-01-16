/// <reference path="jquery-3.4.1.min.js" /> cooment xxx
function getAccessToken() {

    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }
}

function isUserRegistered(accessToken)
{
    $.ajax({

        url: '/api/account/UserInfo',
        method: 'GET',
        headers: {
            'content-Type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            if (response.HasRegistered) {
                sessionStorage.setItem('accesstoken', accessToken);
                sessionStorage.setItem('UserName', response.Email);
                window.location.href = "Data.html";
            }
            else {
                signUpExternalUser(accessToken);
            }

        }

    });
}

function signUpExternalUser(accessToken) {
    $.ajax({

        url: '/api/account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-Type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function () {
            window.location.href = "/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=https%3A%2F%2Flocalhost%3A44372%2FLogin.html&state=JxPJhwhJj1q-4X-HGTyZekUw8-7atSLG8Uwq9FiD4l41";

        }

    });
}
