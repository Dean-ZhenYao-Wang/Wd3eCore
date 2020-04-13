$(document).ready(function () {
    treeToken();
    combotreeToken();
});

function treeToken()
{
    if ($.fn.tree) {
        var token = $app.storage.get('AccessToken');

        $.fn.tree.defaults.loader=function (param, success, error) {

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
                success: function (data) {
                    success(data);
                },
                error: function () {
                    error.apply(this, arguments);
                }
            });
        }
    }
 
}

function combotreeToken() {
    if ($.fn.combotree) {
        var token = $app.storage.get('AccessToken');
        $.fn.combotree.defaults.loader=function (param, success, error) {

            var opts = $(this).combotree('options');

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
                success: function (data) {
                    success(data);
                },
                error: function () {
                    error.apply(this, arguments);
                }
            });
        }
    }

}