
(function ($) {
    $.fn.workflowview = function (method) {
        if ($.fn.workflowview.methods[method]) {
            return $.fn.workflowview.methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return $.fn.workflowview.methods.init.apply(this, arguments);
        } else {
            $.error('Method ' + method + ' does not exist on jQuery.tooltip');
        }
    };
    $.fn.workflowview.defaults = {
        dataId: "",
        workflowcode:"",
        datatype: "",
        width: "500",//宽
        height: "250",//高
        onSuccess: function (node)
        {

        }
    };
    $.fn.workflowview.methods = {
        init: function (option)//初始化
        {
            return this.each(function () {
                var $t = $(this);
                var p = $.extend({}, $.fn.workflowview.defaults, option);
                p.width = p.width == "auto" ? $("body").width() : p.width;
                p.height = p.height == "auto" ? $("body").height() : p.height;
                $t.data("option", p); ''
                $t.empty();
                var html = '<iframe id="workflowid" frameborder=0 style="width:100%;height:' + (p.height + 70) + 'px; src=""></iframe>';
                $t.append(html);

                $("#workflowid").removeAttr("src");
                var str = '/CM/WorkFlow/Index?dataId=' + p.dataId + "&datatype=" + p.datatype + "&workflowcode=" + p.workflowcode + "&height=" + p.height + "&width=" + p.width;
                $("#workflowid").attr("src", str);

                window.parent.onSuccess = p.onSuccess;

            });
        }
    };

})(jQuery);
