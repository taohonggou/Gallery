var regEmail = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
var regMobilePhone = /^1[3|4|5|7|8|][0-9]{9}$/;

//验证是否是邮箱
function isEmail(email) {
    return regEmail.test(email);
}

function isMobilePhone(phone) {
    return regMobilePhone.test(phone);
}