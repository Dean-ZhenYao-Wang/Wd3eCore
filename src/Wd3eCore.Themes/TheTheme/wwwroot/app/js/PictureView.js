
(function ($) {
    $.fn.pictureview = function (method) {
        if ($.fn.pictureview.methods[method]) {
            return $.fn.pictureview.methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return $.fn.pictureview.methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.tooltip');
        }
    };
    $.fn.pictureview.defaults = {
        flag: 1,
        code:"",
        datatype: "",
        width: "500",//宽
        height: "250",//高
    };
    $.fn.pictureview.methods = {
        init: function (option)//初始化
        {
            return this.each(function () {
                var $t = $(this);
                var p = $.extend({}, $.fn.pictureview.defaults, option);
                p.width = p.width == "auto" ? $("body").width() : p.width;
                p.height = p.height == "auto" ? $("body").height() : p.height;
                $t.data("option", p); ''
                $t.empty();
                var html = '<iframe id="fileid" frameborder=0 style="width:100%;height:' + (p.height + 70) + 'px; src=""></iframe>';
                $t.append(html);

                $("#fileid").removeAttr("src");
                var str = '/qm/ProcessTest/ViewDrawings?flag=' + p.flag + "&code=" + p.code + "&height=" + p.height + "&width=" + p.width;
                $("#fileid").attr("src", str);
            });
        }
    };

})(jQuery);
