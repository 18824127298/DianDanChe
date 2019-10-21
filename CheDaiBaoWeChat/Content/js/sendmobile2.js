function checkmobile(id) {
    if ($(id).val() == "") {
        alert("不能为空");
    }
    else {
        str = $(id).val();
        str = str.match(/^1[3|4|5|7|8][0-9]{9}$/);
        if (str == null) {
            alert("手机号不正确");
            return false;
        } else {
           
            return true;
        }
    }
}

function checkon() {
    if (checkmobile('#cellphone')) {
        document.getElementById("buttonx").disabled = true;
        secs = 60; // Number of secs to delay -CHINA-studio      
        wait = secs * 1000;
        $.ajax({
            url: "../ajax/msg2.php",
            type: "POST",
            dataType: "json",
            data: "phone=" + $("#cellphone").val(),
            success: function (a) {
                if (a.flag == "1") {

                    alert(a.msg);
                    for (i = 1; i <= (wait / 1000); i++) {
                        window.setTimeout("doUpdate(" + i + ")", i * 1000);
                    }
                    window.setTimeout("Timer()", wait);
                } else {

                    alert(a.msg);
                }
            }
        });
    }
}



function checkon2( phone2 ) {
    if (checkmobile('#cellphone')) {
        document.getElementById("buttonx").disabled = true;
        secs = 60; // Number of secs to delay -CHINA-studio      
        wait = secs * 1000;
        $.ajax({
            url: "../ajax/msg3.php",
            type: "POST",
            dataType: "json",
            data: "phone=" + $("#cellphone").val()+"&phone2="+phone2,
            success: function (a) {
                if (a.flag == "1") {

                    alert(a.msg);
                    for (i = 1; i <= (wait / 1000); i++) {
                        window.setTimeout("doUpdate(" + i + ")", i * 1000);
                    }
                    window.setTimeout("Timer()", wait);
                } else {

                    alert(a.msg);
                }
            }
        });
    }
}


function doUpdate(num) {
    if (num == (wait / 1000)) {
        //document.forms.register.Submit.value = "验证码";  
        $("#buttonx").val("获取手机验证码");
    } else {
        wut = (wait / 1000) - num;
        //  document.forms.register.Submit.value = "验证码(" + wut + ")";  
        $("#buttonx").val("获取手机验证码(" + wut + ")");
    }
}

function Timer() {
    //document.forms.register.Submit.disabled =false;  
    document.getElementById("buttonx").disabled = false;
} 