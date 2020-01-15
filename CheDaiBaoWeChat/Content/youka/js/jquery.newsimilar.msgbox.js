(function () {
    $.MsgBox = {
        Alert: function (title, msg) {
            GenerateHtml("alert", title, msg);
            btnOk(); //alert只是弹出消息，因此没必要用到回调函数callback
            btnNo();
        },
        Confirm: function (title, msg, callback) {
            GenerateHtml("confirm", title, msg);
            btnOk(callback);
            btnNo();
        }
    }

    //生成Html
    var GenerateHtml = function (type, title, msg) {

        var _html = "";

        _html += '<div id="mb_box"></div><div id="mb_con"><span id="mb_tit">' + title + '</span>';
        _html += '<a id="mb_ico">x</a><div id="mb_msg">' + msg + '</div><div id="mb_btnbox">';

        if (type == "alert") {
            _html += '<input id="mb_btn_ok" type="button" value="确定" />';
        }
        if (type == "confirm") {
            _html += '<input id="mb_btn_ok" type="button" value="确定" />';
            _html += '<input id="mb_btn_no" type="button" value="取消" />';
        }
        _html += '</div></div>';

        //必须先将_html添加到body，再设置Css样式
        $("body").append(_html); GenerateCss();
    }

    //生成Css
    var GenerateCss = function () {

        $("#mb_box").css({
            width: '100%', height: '100%', zIndex: '99999', position: 'fixed',
            filter: 'Alpha(opacity=60)', backgroundColor: 'black', top: '0', left: '0', opacity: '0.6'
        });

        $("#mb_con").css({
            zIndex: '999999', width: '6rem', position: 'absolute',
            backgroundColor: 'White', borderRadius: '15px'
        });

        $("#mb_tit").css({
            display: 'block', fontSize: '0.4rem', color: '#444', padding: '0.3rem 0.5rem',
            backgroundColor: 'rgb(243, 243, 243)', borderRadius: '15px 15px 0 0',
            borderBottom: '3px solid #DDD', fontWeight: 'bold'
        });

        $("#mb_msg").css({
            padding: '0.6rem', lineHeight: '0.6rem',
            borderBottom: '1px dashed #DDD', fontSize: '0.4rem'
        });

        $("#mb_ico").css({
            display: 'block', position: 'absolute', right: '0.2rem', top: '0.2rem', fontSize: '0.8rem',
            border: '1px solid Gray', width: '0.6rem', height: '0.6rem', textAlign: 'center', color: 'Gray',
            lineHeight: '0.45rem', cursor: 'pointer', borderRadius: '12px', fontFamily: '微软雅黑'
        });

        $("#mb_btnbox").css({ margin: '0.5rem 0 0.3rem 0', textAlign: 'center' });
        $("#mb_btn_ok,#mb_btn_no").css({ width: '2rem', height: '1rem', color: 'white', border: 'none' });
        $("#mb_btn_ok").css({ backgroundColor: 'rgb(3, 193, 97)', fontSize: '0.4rem', borderRadius: '0' });
        $("#mb_btn_no").css({ backgroundColor: 'gray', marginLeft: '20px', fontSize: '0.4rem', borderRadius: '0' });


        //右上角关闭按钮hover样式
        $("#mb_ico").hover(function () {
            $(this).css({ backgroundColor: 'Red', color: 'White' });
        }, function () {
            $(this).css({ backgroundColor: '#DDD', color: 'black' });
        });

        var _widht = document.documentElement.clientWidth; //屏幕宽
        var _height = document.documentElement.clientHeight; //屏幕高

        var boxWidth = $("#mb_con").width();
        var boxHeight = $("#mb_con").height();


        //让提示框居中
        $("#mb_con").css({ top: (_height - boxHeight) / 2 + "px", left: (_widht - boxWidth) / 2 + "px" });
        var login = document.getElementById("mb_con");
        login.addEventListener("mousedown", drag, false);
        function drag(e) {

            var e = e || window.event;
            var _this = this;
            var diffX = e.clientX - _this.offsetLeft;
            var diffY = e.clientY - _this.offsetTop;

            document.addEventListener("mousemove", move, false);
            document.addEventListener("mouseup", up, false);

            function move(e) {

                var left = e.clientX - diffX;
                var top = e.clientY - diffY;

                if (left < 0) {
                    left = 0;
                } else if (left > document.documentElement.clientWidth - e.clientX) {
                    //没有使用document.body.clientWidth因为此时页面的高度只有100多，而现在要求弹窗在整个可视区中移动
                    left = document.documentElement.clientWidth - _this.offsetWidth;
                }

                if (top < 0) {
                    top = 0;
                } else if (top > document.documentElement.clientHeight - e.clientY) {
                    top = document.documentElement.clientHeight - _this.offsetHeight;
                }

                _this.style.left = left + 'px';
                _this.style.top = top + 'px';
            }
            function up() {

                document.removeEventListener("mousemove", move, false);
                document.removeEventListener("mouseup", up, false);
            }

        }

    }


    //确定按钮事件
    var btnOk = function (callback) {
        $("#mb_btn_ok").click(function () {
            $("#mb_box,#mb_con").remove();
            if (typeof (callback) == 'function') {
                callback();
            }
        });
    }


    //取消按钮事件
    var btnNo = function () {
        $("#mb_btn_no,#mb_ico").click(function () {
            $("#mb_box,#mb_con").remove();
        });
    }
})();