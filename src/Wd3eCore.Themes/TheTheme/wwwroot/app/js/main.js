var appMain = new Vue({
    el: '#app',
    data: {
        menus: [],
        sideExpand: false,
        tabLoading: false,
        tabs: [{
            id: 'welcome',
            title: '首页',
            url: '/TheTheme/Home/Welcome',
            active: true,
            loaded: true,
            closable: false
        }],
        messageCount: 0,
        navOffset: 0,
        password: {
            LoginName: '',
            OldPassword: '',
            NewPassword: '',
            ConfirmPassword: '',
            loading: false
        },

        workTeam: '',
        workTeamList: []
    },

    computed: {
        activeTab: function () {
            var tab = {};
            this.tabs.forEach(function (t) {
                if (t.active) {
                    tab = t;
                }
            });
            return tab;
        },

        navStyle: function () {
            return { 'transform': 'translate(' + this.navOffset + 'px, 0)' };
        }
    },

    watch: {
        tabs: {
            deep: true,
            handler: function (val) {
                $app.session.set('main-tabs', val);
            }
        }
    },

    methods: {
        toggleSide: function () {
            this.sideExpand = !this.sideExpand;
        },

        open: function (route, refresh) {
            this.route = route;

            var exist = false;
            var self = this;

            this.tabs.forEach(function (t) {
                if (t.id == route.MenuId) {
                    exist = true;
                    if (route.MenuUrl && route.MenuUrl != t.url) {
                        t.url = route.MenuUrl;
                    }
                    t.active = true;
                } else {
                    t.active = false;
                }
            });

            if (!exist) {
                this.tabLoading = true;
                this.tabs.push({
                    id: route.MenuId,
                    parent: route.ParentId,
                    title: route.MenuTitle,
                    url: route.MenuUrl,
                    active: true,
                    loaded: true,
                    closable: true
                });
            }

            if (refresh) {
                self.$nextTick(function () {
                    self.refresh();
                });
            }
        },

        openUrl: function (id, title, url, refresh) {
            this.open({
                MenuId: id,
                ParentId: '',
                MenuTitle: title,
                MenuUrl: url
            }, refresh);
        },

        openMenu: function (menuId, refresh) {
            var mid = menuId.split('.')[0];
            var parent = this.menus.find(function (t) {
                return t.MenuId == mid;
            });

            if (parent) {
                var menu = parent.Children.find(function (t) {
                    return t.MenuId == menuId;
                });

                if (menu) {
                    this.open(menu, refresh);
                }
            }
        },

        active: function (index) {
            var self = this;
            this.tabs.forEach(function (t, i) {
                t.active = (i == index);
                if (t.active && !t.loaded) {
                    t.loaded = true;
                }
            });
        },

        remove: function (index) {
            this.tabLoading = false;
            var active = this.tabs[index].active;
            this.tabs.splice(index, 1);
            if (this.tabs.length > 0 && active) {
                var lastTab = this.tabs[this.tabs.length - 1];
                lastTab.active = true;
                lastTab.loaded = true;
            }
        },

        closeSelf: function () {
            if (this.tabs.length == 1) return;
            var self = this;
            var index = self.tabs.findIndex(function (t) {
                return t.active;
            });

            if (index > -1) {
                self.remove(index);
                self.$nextTick(function () {
                    self.refresh();
                });
            }
        },

        frameLoad: function () {
            this.tabLoading = false;
        },

        loadMessage: function () {
            var self = this;
            $app.http.get('/api/QM/Messageapi/GetNotReadCount').then(function (res) {
                self.messageCount = res.totalCount;
            });
        },

        openMessage: function () {
            this.openUrl('message', '消息查看', '/qm/Message/Index');
        },

        refresh: function () {
            var iframe = $('iframe.active')[0];
            if (iframe) {
                iframe.contentWindow.location.reload(true);
            }
        },

        logout: function () {
            $app.storage.remove('Identity');
            $app.storage.remove('AccessToken');
            Cookies.remove('AccessToken');
            $app.http.get('/api/identity/Logout');
            window.location = '/home/login';
        },

        moveNav: function (offset) {
            var diff = this.$refs.menuNav.clientWidth - this.$refs.menu.clientWidth;
            if (diff <= 0) {
                this.navOffset = 0;
                return;
            }
            offset = this.navOffset + offset;
            if (offset > 0) {
                offset = 0;
            }
            if (offset < -diff) {
                offset = -diff;
            }
            this.navOffset = offset;
        },

        fullScreen: function () {
            var docElm = document.documentElement;
            if (docElm.requestFullscreen) {
                docElm.requestFullscreen();
            }

            else if (docElm.mozRequestFullScreen) {
                docElm.mozRequestFullScreen();
            }

            else if (docElm.webkitRequestFullScreen) {
                docElm.webkitRequestFullScreen();
            }

            else if (docElm.msRequestFullscreen) {
                docElm.msRequestFullscreen();
            }
        },

        resetPassword: function () {
            $('#editPasswordModal').modal('show');
        },

        savePassword: function () {
            if (!$('#editPasswordForm').valid()) {
                return false;
            }

            var self = this;
            self.password.loading = true;

            $app.http.post('/api/identity/ResetPassword', {
                data: self.password
            }).then(function (res) {
                self.password.loading = false;
                if (res.State == 0) {
                    $('#editPasswordModal').modal('hide');
                    $notify('密码已修改');
                } else {
                    $alert(res.Message);
                }
            });
        },

        initMenu: function (data) {
            data.forEach(function (t) {
                var groups = {};
                t.Children.forEach(function (c) {
                    if (!groups[c.GroupName]) {
                        groups[c.GroupName] = [];
                    }

                    groups[c.GroupName].push(c);
                });

                t.Children = [];
                Object.keys(groups).forEach(function (key) {
                    t.Children = t.Children.concat(groups[key]);
                });
            });

            this.menus = data;
        },

        switchWorkTeam: function () {
            $('#switchWorkshopModal').modal('show');
        },

        saveWorkTeam: function () {
            if (this.workTeam) {
                var self = this;
                var data = this.workTeamList.find(function (t) {
                    return t.WorkTeamCode == self.workTeam;
                });
                if (data) {
                    $app.http.post('/api/identity/SwitchWorkshop', {
                        data: data
                    }).then(function () {
                        window.location.reload();
                    });
                }
            }
        }
    },

    mounted: function () {
        var tabs = $app.session.get('main-tabs');
        if (tabs) {
            tabs.forEach(function (t, index) {
                t.loaded = t.active;
            });
            this.tabs = tabs;
        }

        var identity = $app.storage.get('Identity');
        this.password.LoginName = identity.LoginName;
        this.workTeam = identity.WorkTeamCode;

        $("#editPasswordForm").validate({
            rules: {
                OldPassword: "required",
                NewPassword: "required",
                ConfirmPassword: {
                    required: true,
                    equalTo: "#newPassword"
                }
            },
            messages: {
                OldPassword: '请输入原始密码',
                NewPassword: '请输入新的密码',
                ConfirmPassword: {
                    required: '请再次输入新的密码',
                    equalTo: '两次输入的密码不一致'
                }
            }
        });

        if (identity.LoginName.toUpperCase() == 'ADMIN') {
            var self = this;
            $app.http.get('/api/Common/Workshop/ListWorkTeam').then(function (data) {
                data.push({
                    WorkshopCode: '',
                    WorkshopName: '',
                    WorkTeamCode: '10000113',
                    WorkTeamName: '生产部'
                });
                self.workTeamList = data;
            });
        }

        this.initMenu(menuData);

        //this.loadMessage();
        //setInterval(this.loadMessage, 5 * 60 * 1000);
    }
});