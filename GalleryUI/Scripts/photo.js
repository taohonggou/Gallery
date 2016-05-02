var pageIndex = 1, pageSize = 10;
var flag = true;
$(document).ready(function () {
    loadPhotosFirst(pageIndex, pageSize);

    $(window).scroll(function () {
        var scrollTop = document.body.scrollTop;
        var clientHeight = document.documentElement.clientHeight;
        var scrollHeight = document.body.scrollHeight;
        if ( 100>= scrollHeight - clientHeight-scrollTop && flag) {
            pageIndex++;
            loadPhotos(pageIndex, pageSize);
        }
    });
});

function loadPhotos(index, size, isFirst) {
    var data = '';
    $.ajax({
        url: '/Photo/GetRecentPhotos',
        type: 'get',
        data: { pageIndex: index, pageSize: size },
        async: false,
        success: function (data) {
            if (data.StatusCode == 1) {
                data = initPhotos(data.Data.List);
                var $boxes = $(data);
                $container.append($boxes).masonry('appended', $boxes, true);
                $container.imagesLoaded(function () {
                    $container.masonry();
                });//加载完图片后，会实现自动重新排列。【这里是重点】


            }
            else {
                flag = false;
                return layer.msg(data.Message);
            }

        }

    });
    return data;
};
var $container;
function loadPhotosFirst(index, size) {
    $.ajax({
        url: '/Photo/GetRecentPhotos',
        type: 'get',
        data: { pageIndex: index, pageSize: size },
        success: function (data) {
            if (data.StatusCode == 1) {
                var str = initPhotos(data.Data.List);
                $('#con1_1').html(str);

                $container = $('#con1_1');
                $container.imagesLoaded(function () {
                    $container.masonry({
                        itemSelector: '.product_list',
                        columnWidth: 5 //每两列之间的间隙为5像素
                    });
                });
                $container.imagesLoaded(function () {
                    $container.masonry();
                });//加载完图片后，会实现自动重新排列。【这里是重点】
            }
            else
                return layer.msg(data.Message);
        }

    });
};

function initPhotos(json) {
    var str = '';
    $.each(json, function () {
        str += '<div class="product_list"  onclick="redirectDetails(&quot;' + this.PhotoId + '&quot;)"><a href="javascript:;"><img src="' + this.ImgUrl + '"  alt="" /></a><p>' + this.Name + '</p></div>';
    })
    return str;
}

function redirectDetails(photoId) {
    window.location.href = "/Photo/Details?photoId=" + photoId;
}