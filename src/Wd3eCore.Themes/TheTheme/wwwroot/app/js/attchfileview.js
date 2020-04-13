
(function ($) {
    $.fn.attchfileview = function (method) {
        if ($.fn.attchfileview.methods[method]) {
            return $.fn.attchfileview.methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return $.fn.attchfileview.methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.tooltip');
        }
    };
    $.fn.attchfileview.defaults = {
        dataId: "",
        datatype:"",
        width: "500",//宽
        height: "250",//高
        isView:false,
    };
    $.fn.attchfileview.methods = {
        init: function (option)//初始化
        {
            return this.each(function () {
                var $t = $(this);
                var p = $.extend({}, $.fn.attchfileview.defaults, option);
                p.width = p.width == "auto" ? $("body").width() : p.width;
                p.height = p.height == "auto" ? $("body").height() : p.height;
                $t.data("option", p); ''
                $t.empty();
                var html = '<iframe id="fileid" frameborder=0 style="width:100%;height:'+(p.height+70)+'px; src=""></iframe>';
                $t.append(html);

                $("#fileid").removeAttr("src");
                var str = "";
                if (p.isView==true) {
                    str = '/CM/AttachmentInfo/ViewIndex?dataId=' + p.dataId + "&datatype=" + p.datatype + "&height=" + p.height + "&width=" + p.width;
                }
                else {
                    str = '/CM/AttachmentInfo/Index?dataId=' + p.dataId + "&datatype=" + p.datatype+ "&height=" + p.height + "&width=" + p.width;
                }
                $("#fileid").attr("src", str);
            });
        }
    };

})(jQuery);
