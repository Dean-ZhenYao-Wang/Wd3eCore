
(function ($) {
    $.fn.documentview = function (method) {
        if ($.fn.documentview.methods[method]) {
            return $.fn.documentview.methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return $.fn.documentview.methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.tooltip');
        }
    };
    $.fn.documentview.defaults = {
        title: undefined,//文本
        width: "auto",//宽
        height: "auto",//高
        maximizable: true,
        isopennewwin: true,//是否打开新窗体
        resizable: true,
        fileModel: null
    };
    $.fn.documentview.methods = {
        init: function (option)//初始化
        {
            return this.each(function () {
                var $t = $(this);
                var p = $.extend({}, $.fn.documentview.defaults, option);
                p.width = p.width == "auto" ? $("body").width() : p.width;
                p.height = p.height == "auto" ? $("body").height() : p.height;
                $t.data("option", p);
               
                if (p.fileModel==null) {
                    alert("附件模型参数没有传入");
                    return;
                }
                var type = "jpeg|png|jpg|bmp|gif|tif|pdf";
                if (type.indexOf(p.fileModel.AttachmentType) < 0) {
                    alert("该类型附件不支持预览");
                    return;
                }
                $app.http.post('/api/CM/AttachmentInfo/GetViewFileUrl', {
                    data: p.fileModel
                }).then(function (res) {
                  
                    if (p.isopennewwin) {
                        window.open(res.url, p.title, "width=" + p.width + ",height=" + p.height + ",toolbar=no,scrollbars=no,menubar=no");
                    } else {
                        ShowWindow(res.url, p.title, p.width, p.height);
                    }
                });
            });
        }
    };

})(jQuery);
