//实例化一个plupload上传对象
var uploader = new plupload.Uploader({
    browse_button: 'browse', //触发文件选择对话框的按钮，为那个元素id
    url: '/Upload/PostUpload', //服务器端的上传页面地址
    flash_swf_url: '~/Scripts/plupload/Moxie.swf', //swf文件，当需要使用swf方式进行上传时需要配置该参数
    silverlight_xap_url: '~/Scripts/plupload/Moxie.xap', //silverlight文件，当需要使用silverlight方式进行上传时需要配置该参数
    filters: {
        mime_types: [ //只允许上传图片文件
            { title: "图片文件", extensions: "jpg,gif,png" }
        ]
    },

    multipart_params: {
        photoGalleryId: $('#gallery').val(),
        PhotoCategoryId: $('#category').val()
    }
});

//在实例对象上调用init()方法进行初始化
uploader.init();

var isCanUpload = false;

//绑定文件添加进队列事件
uploader.bind('FilesAdded', function (uploader, files) {
    isCanUpload = true;
    for (var i = 0, len = files.length; i < len; i++) {
        var file_name = files[i].name; //文件名
        //构造html来更新UI
        var html = '<li id="file-' + files[i].id + '" class="list-group-item" style="float:left;width:33%;height:300px;"><p class="file-name">' + file_name + '</p><p class="progress"></p><input type="button" onclick="removeFile(&quot;' + files[i].id + '&quot;)" value="删除" /></li>';
        $(html).appendTo('#file-list');
        !function (i) {
            previewImage(files[i], function (imgsrc) {
                $('#file-' + files[i].id).append('<img style="max-width:100%;max-height:76%;" src="' + imgsrc + '" />');
            })
        }(i);
    }
});

//移除照片
function removeFile(id) {
    
    $('#file-'+id).remove();
    var toremove = '';
    //var id = $(this).attr("data-val");
    for (var i in uploader.files) {
        if (uploader.files[i].id === id) {
            toremove = i;
        }
    }
    uploader.files.splice(toremove, 1);
    //alert(id);
    //uploader.removeFile(id);
}

//异常事件
uploader.bind('Error', function (uploader, errObject) {
    console.log(errObject);
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
    if (uploader.files.length > 0)
        uploader.start(); //开始上传
    else
        return layer.msg("请选择图片");

});


uploader.bind("UploadComplete", function (uploader, file) {
    $('#file-list').html("");
    exit();
});

function exit() {
    //window.parent.location.href = window.parent.location.href;
    window.parent.location.href = "/UserCenter/Photos";
}

function change() {
    uploader.settings.multipart_params = {
        photoGalleryId: $('#gallery').val(),
        PhotoCategoryId: $('#category').val()
    }
}

