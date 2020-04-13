(function ($) {

    //浏览器本地session，关闭浏览器失效
    var session = {
        get: function (key) {
            var value = window.sessionStorage[key];
            var data = null;
            if (value && (value.substr(0, 1) === '{' || value.substr(0, 1) === '[')) {
                data = JSON.parse(value);
            }
            return data ? data : value;
        },
        set: function (key, value) {
            if (typeof (value) === 'object') {
                value = JSON.stringify(value);
            }
            window.sessionStorage[key] = value;
        },
        remove: function (key) {
            window.sessionStorage.removeItem(key);
        },
        clear: function () {
            window.sessionStorage.clear();
        }
    };

    //浏览器本地存储，关闭浏览器保持
    var storage = {
        get: function (key) {
            var value = window.localStorage[key];
            var data = null;
            if (value && (value.substr(0, 1) === '{' || value.substr(0, 1) === '[')) {
                data = JSON.parse(value);
            }
            return data ? data : value;
        },
        set: function (key, value) {
            if (typeof value === 'object') {
                value = JSON.stringify(value);
            }
            window.localStorage[key] = value;
        },
        remove: function (key) {
            window.localStorage.removeItem(key);
        }
    };

    //封装ajax请求，用于WebApi请求
    var token = storage.get('AccessToken');
    
    $.ajaxSetup({
        statusCode: {
            504: function () {
                $alert('远程服务器不可用');
            },
            400: function () {
                $alert('错误的请求参数');
            },
            401: function () {
                $alert('当前登录用户已失效，请重新登录 <a href="/home/login">[重新登录]</a>');
            },
            404: function () {
                $alert('请求路径不存在');
            },
            408: function () {
                $alert('服务请求超时，请检查远程服务器是否可用');
            },
            500: function (response) {
                //$alert('远程服务执行错误，请检查系统错误日志');
                $alert(response.responseJSON.ExceptionMessage);
            }
        }
    });

    jQuery.ajaxSetup({
        headers: {
            Authorization: token ? 'Basic ' + token : ''
        }
    });

    var http = {
        defaults: {
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                Authorization: token ? 'Basic ' + token : ''
            }
        },

        request: function (url, options) {
            options = options || {};
            options = $.extend({}, this.defaults, options);

            if (options.contentType == 'application/json' &&
                options.method == 'POST' && options.data) {
                options.data = JSON.stringify(options.data);
            }

            return $.ajax(url, options);
        },

        //get请求快捷方式
        get: function (url, options) {
            options = options || {};
            options.method = 'GET';
            return this.request(url, options);
        },

        //post请求快捷方式
        post: function (url, options) {
            options = options || {};
            options.method = 'POST';
            return this.request(url, options);
        },

        //ajax上传文件
        upload: function (url, formData) {
            var options = {
                method: 'POST',
                contentType: false,
                data: formData,
                processData: false,
                cache: false
            };
            return this.request(url, options);
        }
    };

    ///修改Fancy默认参数
    if (window.Fancy) {
        Fancy.DEBUG = true;
        Fancy.MODULESDIR = '/assets/vendor/fancygrid/modules/';
        Fancy.Grid.prototype.emptyText = '暂无数据';
        Fancy.Grid.prototype.i18n = 'zh-CN';
        Fancy.Grid.prototype.theme = 'bootstrap';
        Fancy.Grid.prototype.textSelection = true;
        Fancy.Grid.prototype.multiSortLimit = 2;
        Fancy.Grid.prototype.resizable = true;

        var _ajax = Fancy.Ajax;        

        Fancy.Ajax = function (options) {
            options = options || {};
            if (options.method == 'POST') {
                options.sendJSON = true;
            }
            options.headers['Authorization'] = token ? 'Basic ' + token : '';
            _ajax(options);
        };
    }

    

    if ($.fn.tree) {      
        $.fn.tree.defaults.loader = function (param, success, error) {
            var opts = $(this).tree('options');
            if (!opts.url) return false;

            $.ajax({
                contentType: 'application/json',
                headers: {
                    //WebApi采用Basic认证
                    Authorization: token ? 'Basic ' + token : ''
                },
                type: opts.method,
                url: opts.url,
                data: param,
                dataType: 'json',
                success: function(data){
                    success(data);
                },
                error: function(){
                    error.apply(this, arguments);
                }
            });
        }
    }



    var app = {
        http: http,
        session: session,
        storage: storage,
        token: token,
        identity: storage.get('Identity'),

        QueryPara: function (name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);   //匹配目标参数
            if (r != null) return unescape(r[2]); return null; //返回参数值
        }
    };

    window.$app = app;


})(window.jQuery);


/**公共组件**/
//alert,confirm,notify
(function () {
    var $alert = function (text) {
        $('#alertModal').find('.media-body').html(text);
        $('#alertModal').modal('show');
        setTimeout(function () {
            $('#alertModal button[data-dismiss]').trigger('focus');
        }, 200);        
    };

    var $confirm = function (text, callback, cancelfn) {
        $('#confirmModal').find('.media-body').html(text);
        $('#confirmModal').modal('show');
        $('#confirmModal').find('[data-confirm="true"]').one('click', function () {
            $('#confirmModal').modal('hide');
            if (typeof (callback) === 'function') {
                callback();
            }
        });
        $('#confirmModal').find('[data-confirm="false"]').one('click', function () {
            $('#confirmModal').modal('hide');
            if (typeof (cancelfn) === 'function') {
                cancelfn();
            }
        });
    };

    var $notify = function (text, options) {
        var opts = options || {};
        var element = $('#notifyAlert');
        var cls = 'alert-' + (opts.type || 'info');

        element.addClass(cls).html(text);
        element.addClass('show');

        setTimeout(function () {
            element.removeClass('show');
            element.removeClass(cls);
        }, 4000);
    };

    var win = window.parent || window;

    window.$alert = win.$alert || $alert;
    window.$confirm = win.$confirm || $confirm;
    window.$notify = win.$notify || $notify;
})(window.jQuery);

/*日期格式化*/
(function () {
    //示例：yyyy-MM-dd HH:mm:ss
    Date.prototype.format = function (format) {
        var o = {
            "M+": this.getMonth() + 1, //month   
            "d+": this.getDate(), //day   
            "h+": this.getHours(), //hour   
            "m+": this.getMinutes(), //minute   
            "s+": this.getSeconds(), //second   
            "q+": Math.floor((this.getMonth() + 3) / 3), //quarter   
            "S": this.getMilliseconds() //millisecond   
        }
        var week = ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"];
        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }
        if (/(w+)/.test(format)) {
            format = format.replace(RegExp.$1, week[this.getDay()]);
        }
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    }

    Date.prototype.isLeapYear = function () {
        var year = this.getFullYear();
        return (year % 400 == 0) || (year % 4 == 0 && year % 100 != 0);
    }

    Date.prototype.getMonthDays = function () {
        var month = this.getMonth();
        var feb = this.isLeapYear() ? 29 : 28;
        return [31, feb, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][month];
    }

    Date.prototype.add = function (part, value) {
        value *= 1;
        if (isNaN(value)) {
            value = 0;
        }
        switch (part) {
            case "y":
                this.setFullYear(this.getFullYear() + value);
                break;
            case "m":
                this.setMonth(this.getMonth() + value);
                break;
            case "d":
                this.setDate(this.getDate() + value);
                break;
            case "h":
                this.setHours(this.getHours() + value);
                break;
            case "n":
                this.setMinutes(this.getMinutes() + value);
                break;
            case "s":
                this.setSeconds(this.getSeconds() + value);
                break;
        }
        return this;
    }

    Date.prototype.diffDay = function (date) {
        return Math.floor((this.getTime() - date.getTime()) / (1000 * 60 * 60 * 24));
    }
})();

/*ES6扩展*/
(function () {    
    if (!Array.prototype.find) {
        Array.prototype.find = function (callback) {
            var item = null;
            for (var i = 0; i < this.length; i++) {
                if (callback(this[i])) {
                    item = this[i];
                    break;
                }
            }

            return item || undefined;
        };
    }

    if (!Array.prototype.findIndex) {
        Array.prototype.findIndex = function (callback) {
            var index = -1;
            for (var i = 0; i < this.length; i++) {
                if (callback(this[i])) {
                    index = i;
                    break;
                }
            }

            return index;
        };
    }

    if (!Object.assign) {
        Object.defineProperty(Object, "assign", {
            enumerable: false,
            configurable: true,
            writable: true,
            value: function (target, firstSource) {
                "use strict";
                if (target === undefined || target === null)
                    throw new TypeError("Cannot convert first argument to object");
                var to = Object(target);
                for (var i = 1; i < arguments.length; i++) {
                    var nextSource = arguments[i];
                    if (nextSource === undefined || nextSource === null) continue;
                    var keysArray = Object.keys(Object(nextSource));
                    for (var nextIndex = 0, len = keysArray.length; nextIndex < len; nextIndex++) {
                        var nextKey = keysArray[nextIndex];
                        var desc = Object.getOwnPropertyDescriptor(nextSource, nextKey);
                        if (desc !== undefined && desc.enumerable) to[nextKey] = nextSource[nextKey];
                    }
                }
                return to;
            }
        });
    }
})();

/*页面控件初始化*/
(function ($) {

    $.validator.messages = {
        required: "这是必填字段",
        number: "请输入有效的数字",
        digits: "只能输入数字",
        date: "请输入有效的日期",
        dateISO: "请输入有效的日期 (YYYY-MM-DD)",
        maxlength: $.validator.format("最多可以输入 {0} 个字符"),
        minlength: $.validator.format("最少要输入 {0} 个字符"),
        rangelength: $.validator.format("请输入长度在 {0} 到 {1} 之间的字符串"),
        range: $.validator.format("请输入范围在 {0} 到 {1} 之间的数值"),
        max: $.validator.format("请输入不大于 {0} 的数值"),
        min: $.validator.format("请输入不小于 {0} 的数值")
    };

    $.validator.setDefaults({
        errorClass: 'is-invalid',
        errorElement: 'div',
        errorPlacement: function (error, element) {
            element.after(error);
            error.addClass('invalid-tooltip');
        }
    });

    $(function () {
        $('form').on('submit', function (event) {
            event.preventDefault();
        });

        var datepicker = ['year', 'month', 'date', 'datetime', 'time'];

        function getOptions(input) {
            return {
                min: $(input).attr('min') || '1900-1-1',
                max: $(input).attr('max') || '2099-12-31'
            };
        }

        datepicker.forEach(function (type) {
            $('input[data-type="' + type + '"]').each(function () {
                var input = this;
                var options = getOptions(input);
                laydate.render({
                    theme: 'bs',
                    type: type,
                    min: options.min,
                    max: options.max,
                    elem: input,
                    done: function (value) {
                        setTimeout(function () {
                            input.dispatchEvent(new Event('input')); 
                        }, 100);                                              
                    },
                    onshow: function (o) {
                        options = getOptions(input);
                        var min = new Date(options.min.replace(/-/g, '/'));
                        var max = new Date(options.max.replace(/-/g, '/'));
                        o.config.min.year = min.getFullYear();
                        o.config.min.month = min.getMonth();
                        o.config.min.date = min.getDate();

                        o.config.max.year = max.getFullYear();
                        o.config.max.month = max.getMonth();
                        o.config.max.date = max.getDate();
                    }
                });
            });            
        });
    });

})(window.jQuery);

if (window.Vue) {
    Vue.directive('table-fixed', {
        inserted: function (el) {
            el.addEventListener('scroll', function () {
                var scrollTop = this.scrollTop;
                $(el).find('thead th, thead td').css({
                    'transform': 'translate(0, ' + scrollTop + 'px)'
                });
            });
        }
    });

    Vue.directive('date-picker', {
        inserted: function (el) {
            laydate.render({
                theme: 'bs',
                type: 'date',
                elem: el,
                done: function (value) {
                    setTimeout(function () {
                        el.dispatchEvent(new Event('input'));
                    }, 100);
                }
            });
        }
    });
}