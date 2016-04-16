﻿//实例化一个plupload上传对象
var uploader = new plupload.Uploader({
    browse_button: 'browse', //触发文件选择对话框的按钮，为那个元素id
    url: '/Upload/PostUpload', //服务器端的上传页面地址
    flash_swf_url: '~/Scripts/plupload/Moxie.swf', //swf文件，当需要使用swf方式进行上传时需要配置该参数
    silverlight_xap_url: '~/Scripts/plupload/Moxie.xap', //silverlight文件，当需要使用silverlight方式进行上传时需要配置该参数
    filters: {
        mime_types: [ //只允许上传图片文件
            { title: "图片文件", extensions: "jpg,gif,png" }
        ]
    }
});

//在实例对象上调用init()方法进行初始化
uploader.init();

//绑定文件添加进队列事件
uploader.bind('FilesAdded', function (uploader, files) {
    for (var i = 0, len = files.length; i < len; i++) {
        var file_name = files[i].name; //文件名
        //构造html来更新UI
        var html = '<li id="file-' + files[i].id + '"><p class="file-name">' + file_name + '</p><p class="progress"></p></li>';
        $(html).appendTo('#file-list');
        !function (i) {
            previewImage(files[i], function (imgsrc) {
                $('#file-' + files[i].id).append('<img src="' + imgsrc + '" />');
            })
        }(i);
    }
});

//plupload中为我们提供了mOxie对象
//有关mOxie的介绍和说明请看：https://github.com/moxiecode/moxie/wiki/API
//如果你不想了解那么多的话，那就照抄本示例的代码来得到预览的图片吧
function previewImage(file, callback) {//file为plupload事件监听函数参数中的file对象,callback为预览图片准备完成的回调函数
    if (!file || !/image\//.test(file.type)) return; //确保文件是图片
    if (file.type == 'image/gif') {//gif使用FileReader进行预览,因为mOxie.Image只支持jpg和png
        var fr = new mOxie.FileReader();
        fr.onload = function () {
            callback(fr.result);
            fr.destroy();
            fr = null;
        }
        fr.readAsDataURL(file.getSource());
    } else {
        var preloader = new mOxie.Image();
        preloader.onload = function () {
            preloader.downsize(300, 300);//先压缩一下要预览的图片,宽300，高300
            var imgsrc = preloader.type == 'image/jpeg' ? preloader.getAsDataURL('image/jpeg', 80) : preloader.getAsDataURL(); //得到图片src,实质为一个base64编码的数据
            callback && callback(imgsrc); //callback传入的参数为预览图片的url
            preloader.destroy();
            preloader = null;
        };
        preloader.load(file.getSource());
    }
}

//绑定文件上传进度事件
uploader.bind('UploadProgress', function (uploader, file) {
    $('#file-' + file.id + ' .progress').css('width', file.percent + '%');//控制进度条
});

//上传按钮
$('#upload-btn').click(function () {
    uploader.start(); //开始上传
});


uploader.bind("UploadComplete", function (uploader, file) {
    $('#file-list').html("");
    exit();
});

function exit() {
    window.parent.location.href = window.parent.location.href;
}