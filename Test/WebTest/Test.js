function register() {
    $.ajax({
        url: 'http://localhost:10682/UserInfo/Register',
        data: { email: $('#userId').val(), md5Pwd: hex_md5($('#md5Pwd').val()) },
        type: 'post',
        dataType: 'json',
        success: function (data) {
            alert(data.Message);
        }
    });
}

function login() {
    $.ajax({
        url: 'http://localhost:10682/UserInfo/login',
        data: { email: $('#email').val(), md5Pwd: hex_md5($('#pwd').val()) },
        type: 'post',
        dataType: 'json',
        success: function (data) {
            alert(data.Message);
        }
    });
}