var abp = abp || {};
(function () {

    /* Swagger */

    abp.swagger = abp.swagger || {};

    abp.swagger.addAuthToken = function () {
        var authToken = abp.auth.getToken();
        if (!authToken) {
            return false;
        }

        var cookieAuth = new SwaggerClient.ApiKeyAuthorization(abp.auth.tokenHeaderName, 'Bearer ' + authToken, 'header');
        swaggerUi.api.clientAuthorizations.add('bearerAuth', cookieAuth);
        return true;
    }

    abp.swagger.addCsrfToken = function () {
        var csrfToken = abp.security.antiForgery.getToken();
        if (!csrfToken) {
            return false;
        }
        var csrfCookieAuth = new SwaggerClient.ApiKeyAuthorization(abp.security.antiForgery.tokenHeaderName, csrfToken, 'header');
        swaggerUi.api.clientAuthorizations.add(abp.security.antiForgery.tokenHeaderName, csrfCookieAuth);
        return true;
    }

    function loginUserInternal(callback) {
        var usernameOrEmailAddress = document.getElementById('userID').value;
        if (!usernameOrEmailAddress) {
            alert('userID is required, please try with a valid value !');
            return false;
        }

        var password = document.getElementById('userPWD').value;
        if (!password) {
            alert('Password is required, please try with a valid value !');
            return false;
        }

        var version = document.getElementById('version').value;
        if (!version) {
            alert('version is required, please try with a valid value !');
            return false;
        }

        var deviceID = document.getElementById('deviceID').value;
        if (!deviceID) {
            alert('deviceID is required, please try with a valid value !');
            return false;
        }
        var xhr = new XMLHttpRequest();

        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    var responseJSON = JSON.parse(xhr.responseText);
                    var result = responseJSON.data;
                    var expireDate = new Date(Date.now() + (result.expireInSeconds * 1000));
                    abp.auth.setToken(result.accessToken, expireDate);
                    callback();   
                } else {
                    alert('Login failed !');
                }
            }
        };

        xhr.open('POST', '/api/TokenAuth/Authenticate', true);
        xhr.setRequestHeader('Content-type', 'application/json');
        xhr.send("{" + "userID:'" + usernameOrEmailAddress + "'," + "userPWD:'" + password + "'," + "version:'" + version + "'," + "deviceID:'" + deviceID +"'}");
    };

    abp.swagger.login = function (callback) {
        loginUserInternal(callback); // Login for host
    };

    abp.swagger.logout = function () {
        abp.auth.clearToken();
    }

    abp.swagger.closeAuthDialog = function () {
        if (document.getElementById('abp-auth-dialog')) {
            document.getElementsByClassName("swagger-ui")[1].removeChild(document.getElementById('abp-auth-dialog'));
        }
    }

    abp.swagger.openAuthDialog = function (loginCallback) {
        abp.swagger.closeAuthDialog();

        var abpAuthDialog = document.createElement('div');
        abpAuthDialog.className = 'dialog-ux';
        abpAuthDialog.id = 'abp-auth-dialog';

        document.getElementsByClassName("swagger-ui")[1].appendChild(abpAuthDialog);

        // -- backdrop-ux
        var backdropUx = document.createElement('div');
        backdropUx.className = 'backdrop-ux';
        abpAuthDialog.appendChild(backdropUx);

        // -- modal-ux
        var modalUx = document.createElement('div');
        modalUx.className = 'modal-ux';
        abpAuthDialog.appendChild(modalUx);

        // -- -- modal-dialog-ux
        var modalDialogUx = document.createElement('div');
        modalDialogUx.className = 'modal-dialog-ux';
        modalUx.appendChild(modalDialogUx);

        // -- -- -- modal-ux-inner
        var modalUxInner = document.createElement('div');
        modalUxInner.className = 'modal-ux-inner';
        modalDialogUx.appendChild(modalUxInner);

        // -- -- -- -- modal-ux-header
        var modalUxHeader = document.createElement('div');
        modalUxHeader.className = 'modal-ux-header';
        modalUxInner.appendChild(modalUxHeader);

        var modalHeader = document.createElement('h3');
        modalHeader.innerText = 'Authorize';
        modalUxHeader.appendChild(modalHeader);

        // -- -- -- -- modal-ux-content
        var modalUxContent = document.createElement('div');
        modalUxContent.className = 'modal-ux-content';
        modalUxInner.appendChild(modalUxContent);

        modalUxContent.onkeydown = function (e) {
            if (e.keyCode === 13) {
                //try to login when user presses enter on authorize modal
                abp.swagger.login(loginCallback);
            }
        };

        //Inputs
        createInput(modalUxContent, 'userID', 'UserID');
        createInput(modalUxContent, 'userPWD', 'UserPWD', 'password');
        createInput(modalUxContent, 'version', 'Version');
        createInput(modalUxContent, 'deviceID', 'DeviceID');
        //Buttons
        var authBtnWrapper = document.createElement('div');
        authBtnWrapper.className = 'auth-btn-wrapper';
        modalUxContent.appendChild(authBtnWrapper);

        //Close button
        var closeButton = document.createElement('button');
        closeButton.className = 'btn modal-btn auth btn-done button';
        closeButton.innerText = 'Close';
        closeButton.style.marginRight = '5px';
        closeButton.onclick = abp.swagger.closeAuthDialog;
        authBtnWrapper.appendChild(closeButton);

        //Authorize button
        var authorizeButton = document.createElement('button');
        authorizeButton.className = 'btn modal-btn auth authorize button';
        authorizeButton.innerText = 'Login';
        authorizeButton.onclick = function() {
            abp.swagger.login(loginCallback);
        };
        authBtnWrapper.appendChild(authorizeButton);
    }

    function createInput(container, id, title, type) {
        var wrapper = document.createElement('div');
        wrapper.className = 'wrapper';
        container.appendChild(wrapper);

        var label = document.createElement('label');
        label.innerText = title;
        wrapper.appendChild(label);

        var section = document.createElement('section');
        section.className = 'block-tablet col-10-tablet block-desktop col-10-desktop';
        wrapper.appendChild(section);

        var input = document.createElement('input');
        input.id = id;
        input.type = type ? type : 'text';
        input.style.width = '100%';

        section.appendChild(input);
    }

})();