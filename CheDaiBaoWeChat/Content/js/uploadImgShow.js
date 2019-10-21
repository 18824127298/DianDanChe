 function ajaxFileUpload2(id,id2,id3,file) {
	       $(id3).css("display","");
            $.ajaxFileUpload
            (
                {
                    //url: 'text/upload.aspx?id='+file, //用于文件上传的服务器端请求地址
					url: '../../js/upload/action.php?id='+file, //用于文件上传的服务器端请求地址
					
                    secureuri: false, //一般设置为false
                    fileElementId: file, //文件上传空间的id属性  <input type="file" id="file" name="file" />
                    dataType: 'json', //返回值类型 一般设置为json
			 
                    success: function (data, status)  //服务器成功响应处理函数
                    {
                       $(id).attr("src", data.imgurl);
					   $(id2).val(data.imgurl2);
					   $(".bg2").css("display","none");
                        if (typeof (data.error) != 'undefined') {
                            if (data.error != '') {
                              //  alert(data.msg);
                            } else {
                               // alert(data.msg);
                            }
                        }
                    },
                    error: function (data, status, e)//服务器响应失败处理函数
                    {
                        alert(e);
                    }
                }
            )
            return false;
        }