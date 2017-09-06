
var HTTP_SERVER = 'http://localhost:54774/';

function generateUrl(path) {
    return HTTP_SERVER + path;
}

function generateApiUrl(path) {
    return HTTP_SERVER + 'api/' + path;
}

function getCookie(cname) {
    name = cname + "=";
    ca = document.cookie.split(';');
    //console.log(ca);
    for (i = 0; i < ca.length; i++) {
        c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

function _GET( field, url ) {
    var href = url ? url : window.location.href;
    var reg = new RegExp( '[?&]' + field + '=([^&#]*)', 'i' );
    var string = reg.exec(href);
    return string ? string[1] : null;
};