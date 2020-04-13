/*
@license

dhtmlxGantt v.6.0.7 Standard
This software is covered by GPL license. You also can obtain Commercial or Enterprise license to use it in non-GPL project - please contact sales@dhtmlx.com. Usage without proper license is prohibited.

(c) Dinamenta, UAB.

*/
!function (t) {
    var e = {};

    function i(n) {
        if (e[n]) return e[n].exports;
        var r = e[n] = {i: n, l: !1, exports: {}};
        return t[n].call(r.exports, r, r.exports, i), r.l = !0, r.exports
    }

    i.m = t, i.c = e, i.d = function (t, e, n) {
        i.o(t, e) || Object.defineProperty(t, e, {enumerable: !0, get: n})
    }, i.r = function (t) {
        "undefined" != typeof Symbol && Symbol.toStringTag && Object.defineProperty(t, Symbol.toStringTag, {value: "Module"}), Object.defineProperty(t, "__esModule", {value: !0})
    }, i.t = function (t, e) {
        if (1 & e && (t = i(t)), 8 & e) return t;
        if (4 & e && "object" == typeof t && t && t.__esModule) return t;
        var n = Object.create(null);
        if (i.r(n), Object.defineProperty(n, "default", {
            enumerable: !0,
            value: t
        }), 2 & e && "string" != typeof t) for (var r in t) i.d(n, r, function (e) {
            return t[e]
        }.bind(null, r));
        return n
    }, i.n = function (t) {
        var e = t && t.__esModule ? function () {
            return t.default
        } : function () {
            return t
        };
        return i.d(e, "a", e), e
    }, i.o = function (t, e) {
        return Object.prototype.hasOwnProperty.call(t, e)
    }, i.p = "/codebase/", i(i.s = 134)
}([function (t, e, i) {
    var n, r = i(3);
    t.exports = {
        copy: function t(e) {
            var i, n;
            if (e && "object" == typeof e) switch (!0) {
                case r.isDate(e):
                    n = new Date(e);
                    break;
                case r.isArray(e):
                    for (n = new Array(e.length), i = 0; i < e.length; i++) n[i] = t(e[i]);
                    break;
                case r.isStringObject(e):
                    n = new String(e);
                    break;
                case r.isNumberObject(e):
                    n = new Number(e);
                    break;
                case r.isBooleanObject(e):
                    n = new Boolean(e);
                    break;
                default:
                    for (i in n = {}, e) Object.prototype.hasOwnProperty.apply(e, [i]) && (n[i] = t(e[i]))
            }
            return n || e
        }, defined: function (t) {
            return void 0 !== t
        }, mixin: function (t, e, i) {
            for (var n in e) (void 0 === t[n] || i) && (t[n] = e[n]);
            return t
        }, uid: function () {
            return n || (n = (new Date).valueOf()), ++n
        }, bind: function (t, e) {
            return t.bind ? t.bind(e) : function () {
                return t.apply(e, arguments)
            }
        }, event: function (t, e, i, n) {
            t.addEventListener ? t.addEventListener(e, i, void 0 !== n && n) : t.attachEvent && t.attachEvent("on" + e, i)
        }, eventRemove: function (t, e, i, n) {
            t.removeEventListener ? t.removeEventListener(e, i, void 0 !== n && n) : t.detachEvent && t.detachEvent("on" + e, i)
        }
    }
}, function (t, e) {
    function i(t) {
        var e = 0, i = 0, n = 0, r = 0;
        if (t.getBoundingClientRect) {
            var a = t.getBoundingClientRect(), s = document.body,
                o = document.documentElement || document.body.parentNode || document.body,
                l = window.pageYOffset || o.scrollTop || s.scrollTop,
                c = window.pageXOffset || o.scrollLeft || s.scrollLeft, d = o.clientTop || s.clientTop || 0,
                h = o.clientLeft || s.clientLeft || 0;
            e = a.top + l - d, i = a.left + c - h, n = document.body.offsetWidth - a.right, r = document.body.offsetHeight - a.bottom
        } else {
            for (; t;) e += parseInt(t.offsetTop, 10), i += parseInt(t.offsetLeft, 10), t = t.offsetParent;
            n = document.body.offsetWidth - t.offsetWidth - i, r = document.body.offsetHeight - t.offsetHeight - e
        }
        return {
            y: Math.round(e),
            x: Math.round(i),
            width: t.offsetWidth,
            height: t.offsetHeight,
            right: Math.round(n),
            bottom: Math.round(r)
        }
    }

    function n(t) {
        var e = !1, i = !1;
        if (window.getComputedStyle) {
            var n = window.getComputedStyle(t, null);
            e = n.display, i = n.visibility
        } else t.currentStyle && (e = t.currentStyle.display, i = t.currentStyle.visibility);
        return "none" != e && "hidden" != i
    }

    function r(t) {
        return !isNaN(t.getAttribute("tabindex")) && 1 * t.getAttribute("tabindex") >= 0
    }

    function a(t) {
        return !{a: !0, area: !0}[t.nodeName.loLowerCase()] || !!t.getAttribute("href")
    }

    function s(t) {
        return !{
            input: !0,
            select: !0,
            textarea: !0,
            button: !0,
            object: !0
        }[t.nodeName.toLowerCase()] || !t.hasAttribute("disabled")
    }

    function o(t) {
        if (!t) return "";
        var e = t.className || "";
        return e.baseVal && (e = e.baseVal), e.indexOf || (e = ""), d(e)
    }

    var l = document.createElement("div");

    function c(t) {
        return t.tagName ? t : (t = t || window.event).target || t.srcElement
    }

    function d(t) {
        return (String.prototype.trim || function () {
            return this.replace(/^\s+|\s+$/g, "")
        }).apply(t)
    }

    t.exports = {
        getNodePosition: i, getFocusableNodes: function (t) {
            for (var e = t.querySelectorAll(["a[href]", "area[href]", "input", "select", "textarea", "button", "iframe", "object", "embed", "[tabindex]", "[contenteditable]"].join(", ")), i = Array.prototype.slice.call(e, 0), o = 0; o < i.length; o++) {
                var l = i[o];
                (r(l) || s(l) || a(l)) && n(l) || (i.splice(o, 1), o--)
            }
            return i
        }, getScrollSize: function () {
            var t = document.createElement("div");
            t.style.cssText = "visibility:hidden;position:absolute;left:-1000px;width:100px;padding:0px;margin:0px;height:110px;min-height:100px;overflow-y:scroll;", document.body.appendChild(t);
            var e = t.offsetWidth - t.clientWidth;
            return document.body.removeChild(t), e
        }, getClassName: o, addClassName: function (t, e) {
            e && -1 === t.className.indexOf(e) && (t.className += " " + e)
        }, removeClassName: function (t, e) {
            e = e.split(" ");
            for (var i = 0; i < e.length; i++) {
                var n = new RegExp("\\s?\\b" + e[i] + "\\b(?![-_.])", "");
                t.className = t.className.replace(n, "")
            }
        }, insertNode: function (t, e) {
            l.innerHTML = e;
            var i = l.firstChild;
            return t.appendChild(i), i
        }, removeNode: function (t) {
            t && t.parentNode && t.parentNode.removeChild(t)
        }, getChildNodes: function (t, e) {
            for (var i = t.childNodes, n = i.length, r = [], a = 0; a < n; a++) {
                var s = i[a];
                s.className && -1 !== s.className.indexOf(e) && r.push(s)
            }
            return r
        }, toNode: function (t) {
            return "string" == typeof t ? document.getElementById(t) || document.querySelector(t) || document.body : t || document.body
        }, locateClassName: function (t, e, i) {
            var n = c(t), r = "";
            for (void 0 === i && (i = !0); n;) {
                if (r = o(n)) {
                    var a = r.indexOf(e);
                    if (a >= 0) {
                        if (!i) return n;
                        var s = 0 === a || !d(r.charAt(a - 1)),
                            l = a + e.length >= r.length || !d(r.charAt(a + e.length));
                        if (s && l) return n
                    }
                }
                n = n.parentNode
            }
            return null
        }, locateAttribute: function (t, e) {
            if (e) {
                for (var i = c(t); i;) {
                    if (i.getAttribute && i.getAttribute(e)) return i;
                    i = i.parentNode
                }
                return null
            }
        }, getTargetNode: c, getRelativeEventPosition: function (t, e) {
            var n = document.documentElement, r = i(e);
            return {
                x: t.clientX + n.scrollLeft - n.clientLeft - r.x + e.scrollLeft,
                y: t.clientY + n.scrollTop - n.clientTop - r.y + e.scrollTop
            }
        }, isChildOf: function (t, e) {
            if (!t || !e) return !1;
            for (; t && t != e;) t = t.parentNode;
            return t === e
        }, hasClass: function (t, e) {
            return "classList" in t ? t.classList.contains(e) : new RegExp("\\b" + e + "\\b").test(t.className)
        }
    }
}, function (t, e) {
    t.exports = function (t, e) {
        for (var i in e) e.hasOwnProperty(i) && (t[i] = e[i]);

        function n() {
            this.constructor = t
        }

        t.prototype = null === e ? Object.create(e) : (n.prototype = e.prototype, new n)
    }
}, function (t, e) {
    var i = {
        second: 1,
        minute: 60,
        hour: 3600,
        day: 86400,
        week: 604800,
        month: 2592e3,
        quarter: 7776e3,
        year: 31536e3
    };

    function n(t, e) {
        var i = [];
        if (t.filter) return t.filter(e);
        for (var n = 0; n < t.length; n++) e(t[n], n) && (i[i.length] = t[n]);
        return i
    }

    t.exports = {
        getSecondsInUnit: function (t) {
            return i[t] || i.hour
        }, forEach: function (t, e) {
            if (t.forEach) t.forEach(e); else for (var i = t.slice(), n = 0; n < i.length; n++) e(i[n], n)
        }, arrayMap: function (t, e) {
            if (t.map) return t.map(e);
            for (var i = t.slice(), n = [], r = 0; r < i.length; r++) n.push(e(i[r], r));
            return n
        }, arrayFind: function (t, e) {
            if (t.find) return t.find(e);
            for (var i = 0; i < t.length; i++) if (e(t[i], i)) return t[i]
        }, arrayFilter: n, arrayDifference: function (t, e) {
            return n(t, function (t, i) {
                return !e(t, i)
            })
        }, arraySome: function (t, e) {
            if (0 === t.length) return !1;
            for (var i = 0; i < t.length; i++) if (e(t[i], i, t)) return !0;
            return !1
        }, hashToArray: function (t) {
            var e = [];
            for (var i in t) t.hasOwnProperty(i) && e.push(t[i]);
            return e
        }, sortArrayOfHash: function (t, e, i) {
            var n = function (t, e) {
                return t < e
            };
            t.sort(function (t, r) {
                return t[e] === r[e] ? 0 : i ? n(t[e], r[e]) : n(r[e], t[e])
            })
        }, throttle: function (t, e) {
            var i = !1;
            return function () {
                i || (t.apply(null, arguments), i = !0, setTimeout(function () {
                    i = !1
                }, e))
            }
        }, isArray: function (t) {
            return Array.isArray ? Array.isArray(t) : t && void 0 !== t.length && t.pop && t.push
        }, isDate: function (t) {
            return !(!t || "object" != typeof t || !(t.getFullYear && t.getMonth && t.getDate))
        }, isStringObject: function (t) {
            return t && "object" == typeof t && "function String() { [native code] }" === Function.prototype.toString.call(t.constructor)
        }, isNumberObject: function (t) {
            return t && "object" == typeof t && "function Number() { [native code] }" === Function.prototype.toString.call(t.constructor)
        }, isBooleanObject: function (t) {
            return t && "object" == typeof t && "function Boolean() { [native code] }" === Function.prototype.toString.call(t.constructor)
        }, delay: function (t, e) {
            var i, n = function () {
                n.$cancelTimeout(), t.$pending = !0, i = setTimeout(function () {
                    t(), n.$pending = !1
                }, e)
            };
            return n.$pending = !1, n.$cancelTimeout = function () {
                clearTimeout(i), t.$pending = !1
            }, n.$execute = function () {
                t(), t.$cancelTimeout()
            }, n
        }
    }
}, function (t, e) {
    var i = function () {
        this._connected = [], this._silent_mode = !1
    };
    i.prototype = {
        _silentStart: function () {
            this._silent_mode = !0
        }, _silentEnd: function () {
            this._silent_mode = !1
        }
    };
    var n = function (t) {
        var e = [], i = function () {
            for (var i = !0, n = 0; n < e.length; n++) if (e[n]) {
                var r = e[n].apply(t, arguments);
                i = i && r
            }
            return i
        };
        return i.addEvent = function (t) {
            return "function" == typeof t && e.push(t) - 1
        }, i.removeEvent = function (t) {
            e[t] = null
        }, i
    };
    t.exports = function (t) {
        var e = new i;
        t.attachEvent = function (t, i, r) {
            return t = "ev_" + t.toLowerCase(), e[t] || (e[t] = n(r || this)), t + ":" + e[t].addEvent(i)
        }, t.attachAll = function (t, e) {
            this.attachEvent("listen_all", t, e)
        }, t.callEvent = function (t, i, n) {
            if (e._silent_mode) return !0;
            var r = "ev_" + t.toLowerCase();
            return e.ev_listen_all && e.ev_listen_all.apply(n || this, [t].concat(i)), !e[r] || e[r].apply(n || this, i)
        }, t.checkEvent = function (t) {
            return !!e["ev_" + t.toLowerCase()]
        }, t.detachEvent = function (t) {
            if (t) {
                var i = t.split(":");
                e[i[0]].removeEvent(i[1])
            }
        }, t.detachAllEvents = function () {
            for (var t in e) 0 === t.indexOf("ev_") && delete e[t]
        }
    }
}, function (t, e) {
    function i() {
        console.log("Method is not implemented.")
    }

    function n() {
    }

    n.prototype.render = i, n.prototype.set_value = i, n.prototype.get_value = i, n.prototype.focus = i, t.exports = function (t) {
        return n
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = function () {
        };
        return e.prototype = {
            show: function (t, e, i, n) {
            }, hide: function () {
            }, set_value: function (t, e, i, n) {
                this.get_input(n).value = t
            }, get_value: function (t, e, i) {
                return this.get_input(i).value || ""
            }, is_changed: function (t, e, i, n) {
                var r = this.get_value(e, i, n);
                return r && t && r.valueOf && t.valueOf ? r.valueOf() != t.valueOf() : r != t
            }, is_valid: function (t, e, i, n) {
                return !0
            }, save: function (t, e, i) {
            }, get_input: function (t) {
                return t.querySelector("input")
            }, focus: function (t) {
                var e = this.get_input(t);
                e && (e.focus && e.focus(), e.select && e.select())
            }
        }, e
    }
}, function (t, e, i) {
    var n = i(0), r = i(4), a = i(1), s = function () {
        "use strict";

        function t(t, e, i, s) {
            t && (this.$container = a.toNode(t), this.$parent = t), this.$config = n.mixin(e, {headerHeight: 33}), this.$gantt = s, this.$domEvents = s._createDomEventScope(), this.$id = e.id || "c" + n.uid(), this.$name = "cell", this.$factory = i, r(this)
        }

        return t.prototype.destructor = function () {
            this.$parent = this.$container = this.$view = null, this.$gantt.$services.getService("mouseEvents").detach("click", "gantt_header_arrow", this._headerClickHandler), this.$domEvents.detachAll(), this.callEvent("onDestroy", []), this.detachAllEvents()
        }, t.prototype.cell = function (t) {
            return null
        }, t.prototype.scrollTo = function (t, e) {
            1 * t == t && (this.$view.scrollLeft = t), 1 * e == e && (this.$view.scrollTop = e)
        }, t.prototype.clear = function () {
            this.getNode().innerHTML = "", this.getNode().className = "gantt_layout_content", this.getNode().style.padding = "0"
        }, t.prototype.resize = function (t) {
            if (this.$parent) return this.$parent.resize(t);
            !1 === t && (this.$preResize = !0);
            var e = this.$container, i = e.offsetWidth, n = e.offsetHeight, r = this.getSize();
            e === document.body && (i = document.body.offsetWidth, n = document.body.offsetHeight), i < r.minWidth && (i = r.minWidth), i > r.maxWidth && (i = r.maxWidth), n < r.minHeight && (n = r.minHeight), n > r.maxHeight && (n = r.maxHeight), this.setSize(i, n), this.$preResize, this.$preResize = !1
        }, t.prototype.hide = function () {
            this._hide(!0), this.resize()
        }, t.prototype.show = function (t) {
            this._hide(!1), t && this.$parent && this.$parent.show(), this.resize()
        }, t.prototype._hide = function (t) {
            if (!0 === t && this.$view.parentNode) this.$view.parentNode.removeChild(this.$view); else if (!1 === t && !this.$view.parentNode) {
                var e = this.$parent.cellIndex(this.$id);
                this.$parent.moveView(this, e)
            }
            this.$config.hidden = t
        }, t.prototype.$toHTML = function (t, e) {
            void 0 === t && (t = ""), e = [e || "", this.$config.css || ""].join(" ");
            var i = this.$config, n = "";
            i.raw ? t = "string" == typeof i.raw ? i.raw : "" : (t || (t = "<div class='gantt_layout_content' " + (e ? " class='" + e + "' " : "") + " >" + (i.html || "") + "</div>"), i.header && (n = "<div class='gantt_layout_header'>" + (i.canCollapse ? "<div class='gantt_layout_header_arrow'></div>" : "") + "<div class='gantt_layout_header_content'>" + i.header + "</div></div>"));
            return "<div class='gantt_layout_cell " + e + "' data-cell-id='" + this.$id + "'>" + n + t + "</div>"
        }, t.prototype.$fill = function (t, e) {
            this.$view = t, this.$parent = e, this.init()
        }, t.prototype.getNode = function () {
            return this.$view.querySelector("gantt_layout_cell") || this.$view
        }, t.prototype.init = function () {
            var t = this;
            this._headerClickHandler = function (e) {
                a.locateAttribute(e, "data-cell-id") == t.$id && t.toggle()
            }, this.$gantt.$services.getService("mouseEvents").delegate("click", "gantt_header_arrow", this._headerClickHandler), this.callEvent("onReady", [])
        }, t.prototype.toggle = function () {
            this.$config.collapsed = !this.$config.collapsed, this.resize()
        }, t.prototype.getSize = function () {
            var t = {
                height: this.$config.height || 0,
                width: this.$config.width || 0,
                gravity: this.$config.gravity || 1,
                minHeight: this.$config.minHeight || 0,
                minWidth: this.$config.minWidth || 0,
                maxHeight: this.$config.maxHeight || 1e5,
                maxWidth: this.$config.maxWidth || 1e5
            };
            if (this.$config.collapsed) {
                var e = "x" === this.$config.mode;
                t[e ? "width" : "height"] = t[e ? "maxWidth" : "maxHeight"] = this.$config.headerHeight
            }
            return t
        }, t.prototype.getContentSize = function () {
            var t = this.$lastSize.contentX;
            t !== 1 * t && (t = this.$lastSize.width);
            var e = this.$lastSize.contentY;
            return e !== 1 * e && (e = this.$lastSize.height), {width: t, height: e}
        }, t.prototype._getBorderSizes = function () {
            var t = {top: 0, right: 0, bottom: 0, left: 0, horizontal: 0, vertical: 0};
            return this._currentBorders && (this._currentBorders[this._borders.left] && (t.left = 1, t.horizontal++), this._currentBorders[this._borders.right] && (t.right = 1, t.horizontal++), this._currentBorders[this._borders.top] && (t.top = 1, t.vertical++), this._currentBorders[this._borders.bottom] && (t.bottom = 1, t.vertical++)), t
        }, t.prototype.setSize = function (t, e) {
            this.$view.style.width = t + "px", this.$view.style.height = e + "px";
            var i = this._getBorderSizes(), n = e - i.vertical, r = t - i.horizontal;
            this.$lastSize = {
                x: t,
                y: e,
                contentX: r,
                contentY: n
            }, this.$config.header ? this._sizeHeader() : this._sizeContent()
        }, t.prototype._borders = {
            left: "gantt_layout_cell_border_left",
            right: "gantt_layout_cell_border_right",
            top: "gantt_layout_cell_border_top",
            bottom: "gantt_layout_cell_border_bottom"
        }, t.prototype._setBorders = function (t, e) {
            e || (e = this);
            var i = e.$view;
            for (var n in this._borders) a.removeClassName(i, this._borders[n]);
            "string" == typeof t && (t = [t]);
            var r = {};
            for (n = 0; n < t.length; n++) a.addClassName(i, t[n]), r[t[n]] = !0;
            e._currentBorders = r
        }, t.prototype._sizeContent = function () {
            var t = this.$view.childNodes[0];
            t && "gantt_layout_content" == t.className && (t.style.height = this.$lastSize.contentY + "px")
        }, t.prototype._sizeHeader = function () {
            var t = this.$lastSize;
            t.contentY -= this.$config.headerHeight;
            var e = this.$view.childNodes[0], i = this.$view.childNodes[1], n = "x" === this.$config.mode;
            if (this.$config.collapsed) if (i.style.display = "none", n) {
                e.className = "gantt_layout_header collapsed_x", e.style.width = t.y + "px";
                var r = Math.floor(t.y / 2 - t.x / 2);
                e.style.transform = "rotate(90deg) translate(" + r + "px, " + r + "px)", i.style.display = "none"
            } else e.className = "gantt_layout_header collapsed_y"; else e.className = n ? "gantt_layout_header" : "gantt_layout_header vertical", e.style.width = "auto", e.style.transform = "", i.style.display = "", i.style.height = t.contentY + "px";
            e.style.height = this.$config.headerHeight + "px"
        }, t
    }();
    t.exports = s
}, function (t, e, i) {
    var n = i(2), r = i(46);
    t.exports = function (t) {
        var e = i(5)(t);

        function a() {
            return e.apply(this, arguments) || this
        }

        return n(a, e), a.prototype.render = function (t) {
            var e = "<div class='gantt_cal_ltext' style='height:" + ((t.height || "23") + "px") + ";'>";
            return e += r.getHtmlSelect(t.options, [{key: "style", value: "width:100%;"}]), e += "</div>"
        }, a.prototype.set_value = function (t, e, i, n) {
            var r = t.firstChild;
            !r._dhx_onchange && n.onchange && (r.onchange = n.onchange, r._dhx_onchange = !0), void 0 === e && (e = (r.options[0] || {}).value), r.value = e || ""
        }, a.prototype.get_value = function (t) {
            return t.firstChild.value
        }, a.prototype.focus = function (e) {
            var i = e.firstChild;
            t._focus(i, !0)
        }, a
    }
}, function (t, e, i) {
    var n = i(0);
    t.exports = {
        createDropTargetObject: function (t) {
            var e = {targetParent: null, targetIndex: 0, targetId: null, child: !1, nextSibling: !1, prevSibling: !1};
            return t && n.mixin(e, t, !0), e
        }, nextSiblingTarget: function (t, e, i) {
            var n = this.createDropTargetObject();
            return n.targetId = e, n.nextSibling = !0, n.targetParent = i.getParent(n.targetId), n.targetIndex = i.getBranchIndex(n.targetId), i.getParent(t) == n.targetParent && n.targetIndex < i.getBranchIndex(t) && (n.targetIndex += 1), n
        }, prevSiblingTarget: function (t, e, i) {
            var n = this.createDropTargetObject();
            return n.targetId = e, n.prevSibling = !0, n.targetParent = i.getParent(n.targetId), n.targetIndex = i.getBranchIndex(n.targetId), i.getParent(t) == n.targetParent && n.targetIndex > i.getBranchIndex(t) && (n.targetIndex -= 1), n
        }, firstChildTarget: function (t, e, i) {
            var n = this.createDropTargetObject();
            return n.targetId = e, n.targetParent = n.targetId, n.targetIndex = 0, n.child = !0, n
        }, lastChildTarget: function (t, e, i) {
            var n = i.getChildren(e), r = this.createDropTargetObject();
            return r.targetId = n[n.length - 1], r.targetParent = e, r.targetIndex = n.length, r.nextSibling = !0, r
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = [];
        return {
            delegate: function (i, n, r, a) {
                e.push([i, n, r, a]), t.$services.getService("mouseEvents").delegate(i, n, r, a)
            }, destructor: function () {
                for (var i = t.$services.getService("mouseEvents"), n = 0; n < e.length; n++) {
                    var r = e[n];
                    i.detach(r[0], r[1], r[2], r[3])
                }
                e = []
            }
        }
    }
}, function (t, e, i) {
    var n = i(24), r = i(4), a = i(0), s = i(23), o = i(109), l = function (t, e, i, s) {
        this.$config = a.mixin({}, e || {}), this.$scaleHelper = new n(s), this.$gantt = s, r(this)
    };

    function c(t, e) {
        for (var i, n, r, a = 0, s = t.length - 1; a <= s;) if (n = +t[i = Math.floor((a + s) / 2)], r = +t[i - 1], n < e) a = i + 1; else {
            if (!(n > e)) {
                for (; +t[i] == +t[i + 1];) i++;
                return i
            }
            if (!isNaN(r) && r < e) return i - 1;
            s = i - 1
        }
        return t.length - 1
    }

    l.prototype = {
        init: function (t) {
            t.innerHTML += "<div class='gantt_task' style='width:inherit;height:inherit;'></div>", this.$task = t.childNodes[0], this.$task.innerHTML = "<div class='gantt_task_scale'></div><div class='gantt_data_area'></div>", this.$task_scale = this.$task.childNodes[0], this.$task_data = this.$task.childNodes[1], this.$task_data.innerHTML = "<div class='gantt_task_bg'></div><div class='gantt_links_area'></div><div class='gantt_bars_area'></div>", this.$task_bg = this.$task_data.childNodes[0], this.$task_links = this.$task_data.childNodes[1], this.$task_bars = this.$task_data.childNodes[2], this._tasks = {
                col_width: 0,
                width: [],
                full_width: 0,
                trace_x: [],
                rendered: {}
            };
            var e = this.$getConfig(), i = e[this.$config.bind + "_attribute"],
                n = e[this.$config.bindLinks + "_attribute"];
            !i && this.$config.bind && (i = this.$config.bind + "_id"), !n && this.$config.bindLinks && (n = this.$config.bindLinks + "_id"), this.$config.item_attribute = i || null, this.$config.link_attribute = n || null;
            var r = this._createLayerConfig();
            this.$config.layers || (this.$config.layers = r.tasks), this.$config.linkLayers || (this.$config.linkLayers = r.links), this._attachLayers(this.$gantt), this.callEvent("onReady", [])
        }, setSize: function (t, e) {
            var i = this.$getConfig();
            if (1 * t === t && (this.$config.width = t), 1 * e === e) {
                this.$config.height = e;
                var n = Math.max(this.$config.height - i.scale_height);
                this.$task_data.style.height = n + "px"
            }
            if (this.refresh(), this.$task_bg.style.backgroundImage = "", i.smart_rendering && this.$config.rowStore) {
                var r = this.$config.rowStore;
                this.$task_bg.style.height = i.row_height * r.countVisible() + "px"
            } else this.$task_bg.style.height = "";
            for (var a = this._tasks, s = this.$task_data.childNodes, o = 0, l = s.length; o < l; o++) {
                var c = s[o];
                c.hasAttribute("data-layer") && c.style && (c.style.width = a.full_width + "px")
            }
        }, isVisible: function () {
            return this.$parent && this.$parent.$config ? !this.$parent.$config.hidden : this.$task.offsetWidth
        }, getSize: function () {
            var t = this.$getConfig(), e = this.$config.rowStore, i = e ? t.row_height * e.countVisible() : 0,
                n = this._tasks.full_width;
            return {
                x: this.$config.width,
                y: this.$config.height,
                contentX: this.isVisible() ? n : 0,
                contentY: this.isVisible() ? t.scale_height + i : 0,
                scrollHeight: this.isVisible() ? i : 0,
                scrollWidth: this.isVisible() ? n : 0
            }
        }, scrollTo: function (t, e) {
            this.isVisible() && (1 * e === e && (this.$config.scrollTop = e, this.$task_data.scrollTop = this.$config.scrollTop), 1 * t === t && (this.$task.scrollLeft = t, this.$config.scrollLeft = this.$task.scrollLeft, this._refreshScales()))
        }, _refreshScales: function () {
            if (this.isVisible() && this.$getConfig().smart_scales) {
                var t = this.$config.scrollLeft, e = this.$config.width, i = this._scales;
                this.$task_scale.innerHTML = this._getScaleChunkHtml(i, t, t + e)
            }
        }, _createLayerConfig: function () {
            var t = this, e = function () {
                return t.isVisible()
            };
            return {
                tasks: [{
                    expose: !0,
                    renderer: this.$gantt.$ui.layers.taskBar,
                    container: this.$task_bars,
                    filter: [e]
                }, {
                    renderer: this.$gantt.$ui.layers.taskSplitBar,
                    filter: [e],
                    container: this.$task_bars,
                    append: !0
                }, {
                    renderer: this.$gantt.$ui.layers.taskBg, container: this.$task_bg, filter: [function () {
                        return !t.$getConfig().static_background
                    }, e]
                }],
                links: [{expose: !0, renderer: this.$gantt.$ui.layers.link, container: this.$task_links, filter: [e]}]
            }
        }, _attachLayers: function (t) {
            this._taskLayers = [], this._linkLayers = [];
            var e = this, i = this.$gantt.$services.getService("layers");
            if (this.$config.bind) {
                e.$config.rowStore = e.$gantt.getDatastore(e.$config.bind);
                var n = i.getDataRender(this.$config.bind);
                n || (n = i.createDataRender({
                    name: this.$config.bind, defaultContainer: function () {
                        return e.$task_data
                    }
                })), n.container = function () {
                    return e.$task_data
                };
                for (var r = this.$config.layers, a = 0; r && a < r.length; a++) {
                    "string" == typeof (c = r[a]) && (c = this.$gantt.$ui.layers[c]), "function" == typeof c && (c = {renderer: c}), c.host = this;
                    var s = n.addLayer(c);
                    this._taskLayers.push(s), c.expose && (this._taskRenderer = n.getLayer(s))
                }
                this._initStaticBackgroundRender()
            }
            if (this.$config.bindLinks) {
                e.$config.linkStore = e.$gantt.getDatastore(e.$config.bindLinks);
                var o = i.getDataRender(this.$config.bindLinks);
                o || (o = i.createDataRender({
                    name: this.$config.bindLinks, defaultContainer: function () {
                        return e.$task_data
                    }
                }));
                var l = this.$config.linkLayers;
                for (a = 0; l && a < l.length; a++) {
                    var c;
                    "string" == typeof c && (c = this.$gantt.$ui.layers[c]), (c = l[a]).host = this;
                    var d = o.addLayer(c);
                    this._taskLayers.push(d), l[a].expose && (this._linkRenderer = o.getLayer(d))
                }
            }
        }, _initStaticBackgroundRender: function () {
            var t = this, e = o.create(), i = t.$config.rowStore;
            i && (this._staticBgHandler = i.attachEvent("onStoreUpdated", function (i, n, r) {
                if (null === i && t.isVisible()) {
                    var a = t.$getConfig();
                    if (a.static_background) {
                        var s = t.$gantt.getDatastore(t.$config.bind);
                        s && e.render(t.$task_bg, a, t.getScale(), a.row_height * s.countVisible())
                    }
                }
            }), this._initStaticBackgroundRender = function () {
            })
        }, _clearLayers: function (t) {
            var e = this.$gantt.$services.getService("layers"), i = e.getDataRender(this.$config.bind),
                n = e.getDataRender(this.$config.bindLinks);
            if (this._taskLayers) for (var r = 0; r < this._taskLayers.length; r++) i.removeLayer(this._taskLayers[r]);
            if (this._linkLayers) for (r = 0; r < this._linkLayers.length; r++) n.removeLayer(this._linkLayers[r]);
            this._linkLayers = [], this._taskLayers = []
        }, _render_tasks_scales: function () {
            var t = this.$getConfig(), e = "", i = 0, n = 0, r = this.$gantt.getState();
            if (this.isVisible()) {
                var a = this.$scaleHelper, s = this._getScales();
                n = t.scale_height;
                var o = this.$config.width;
                "x" != t.autosize && "xy" != t.autosize || (o = Math.max(t.autosize_min_width, 0));
                var l = a.prepareConfigs(s, t.min_column_width, o, n - 1, r.min_date, r.max_date, t.rtl),
                    c = this._tasks = l[l.length - 1];
                this._scales = l, e = this._getScaleChunkHtml(l, 0, this.$config.width), i = c.full_width + "px", n += "px"
            }
            this.$task_scale.style.height = n, this.$task_data.style.width = this.$task_scale.style.width = i, this.$task_scale.innerHTML = e
        }, _getScaleChunkHtml: function (t, e, i) {
            for (var n = [], r = this.$gantt.$services.templates().scale_row_class, a = 0; a < t.length; a++) {
                var s = "gantt_scale_line", o = r(t[a]);
                o && (s += " " + o), n.push('<div class="' + s + '" style="height:' + t[a].height + "px;position:relative;line-height:" + t[a].height + 'px">' + this._prepareScaleHtml(t[a], e, i) + "</div>")
            }
            return n.join("")
        }, _prepareScaleHtml: function (t, e, i) {
            var n = this.$getConfig(), r = this.$gantt.$services.templates(), a = [], s = null, o = null, l = null;
            (t.template || t.date) && (o = t.template || this.$gantt.date.date_to_str(t.date));
            var d = 0, h = t.count;
            !n.smart_scales || isNaN(e) || isNaN(i) || (d = c(t.left, e), h = c(t.left, i) + 1), l = t.css || function () {
            }, !t.css && n.inherit_scale_class && (l = r.scale_cell_class);
            for (var u = d; u < h && t.trace_x[u]; u++) {
                s = new Date(t.trace_x[u]);
                var g = o.call(this, s), f = t.width[u], _ = t.height, p = t.left[u], v = "", m = "", y = "";
                if (f) {
                    v = "width:" + f + "px;height:" + _ + "px;" + (n.smart_scales ? "position:absolute;left:" + p + "px" : ""), y = "gantt_scale_cell" + (u == t.count - 1 ? " gantt_last_cell" : ""), (m = l.call(this, s)) && (y += " " + m);
                    var k = "<div class='" + y + "'" + this.$gantt._waiAria.getTimelineCellAttr(g) + " style='" + v + "'>" + g + "</div>";
                    a.push(k)
                }
            }
            return a.join("")
        }, dateFromPos: function (t) {
            var e = this._tasks;
            if (t < 0 || t > e.full_width || !e.full_width) return null;
            var i = c(this._tasks.left, t), n = this._tasks.left[i], r = e.width[i] || e.col_width, a = 0;
            r && (a = (t - n) / r, e.rtl && (a = 1 - a));
            var s = 0;
            return a && (s = this._getColumnDuration(e, e.trace_x[i])), new Date(e.trace_x[i].valueOf() + Math.round(a * s))
        }, posFromDate: function (t) {
            if (!this.isVisible()) return 0;
            var e = this.columnIndexByDate(t);
            this.$gantt.assert(e >= 0, "Invalid day index");
            var i = Math.floor(e), n = e % 1, r = this._tasks.left[Math.min(i, this._tasks.width.length - 1)];
            return i == this._tasks.width.length && (r += this._tasks.width[this._tasks.width.length - 1]), n && (i < this._tasks.width.length ? r += this._tasks.width[i] * (n % 1) : r += 1), Math.round(r)
        }, columnIndexByDate: function (t) {
            var e = new Date(t).valueOf(), i = this._tasks.trace_x_ascending, n = this._tasks.ignore_x,
                r = this.$gantt.getState();
            if (e <= r.min_date) return this._tasks.rtl ? i.length : 0;
            if (e >= r.max_date) return this._tasks.rtl ? 0 : i.length;
            for (var a = c(i, e), s = +i[a]; n[s];) s = +i[++a];
            var o = this._tasks.trace_index_transition, l = a;
            if (!s) return o ? o[0] : 0;
            var d = (t - i[a]) / this._getColumnDuration(this._tasks, i[a]);
            return o ? o[l] + (1 - d) : l + d
        }, getItemPosition: function (t, e, i) {
            var n, r, a;
            return this._tasks.rtl ? (r = this.posFromDate(e || t.start_date), n = this.posFromDate(i || t.end_date)) : (n = this.posFromDate(e || t.start_date), r = this.posFromDate(i || t.end_date)), a = Math.max(r - n, 0), {
                left: n,
                top: this.getItemTop(t.id),
                height: this.getItemHeight(),
                width: a
            }
        }, getItemHeight: function () {
            var t = this.$getConfig(), e = t.task_height;
            if ("full" == e) {
                var i = t.task_height_offset || 5;
                e = t.row_height - i
            }
            return e = Math.min(e, t.row_height), Math.max(e, 0)
        }, getScale: function () {
            return this._tasks
        }, _getScales: function () {
            var t = this.$getConfig(), e = this.$scaleHelper, i = [e.primaryScale()].concat(t.subscales);
            return e.sortScales(i), i
        }, _getColumnDuration: function (t, e) {
            return this.$gantt.date.add(e, t.step, t.unit) - e
        }, refresh: function () {
            this.$config.bind && (this.$config.rowStore = this.$gantt.getDatastore(this.$config.bind)), this.$config.bindLinks && (this.$config.linkStore = this.$gantt.getDatastore(this.$config.bindLinks)), this._initStaticBackgroundRender(), this._render_tasks_scales()
        }, destructor: function () {
            var t = this.$gantt;
            this._clearLayers(t), this.$task = null, this.$task_scale = null, this.$task_data = null, this.$task_bg = null, this.$task_links = null, this.$task_bars = null, this.$gantt = null, this.$config.rowStore && (this.$config.rowStore.detachEvent(this._staticBgHandler), this.$config.rowStore = null), this.$config.linkStore && (this.$config.linkStore = null), this.callEvent("onDestroy", []), this.detachAllEvents()
        }
    }, a.mixin(l.prototype, s()), t.exports = l
}, function (t, e) {
    var i = {
        isIE: navigator.userAgent.indexOf("MSIE") >= 0 || navigator.userAgent.indexOf("Trident") >= 0,
        isIE6: !window.XMLHttpRequest && navigator.userAgent.indexOf("MSIE") >= 0,
        isIE7: navigator.userAgent.indexOf("MSIE 7.0") >= 0 && navigator.userAgent.indexOf("Trident") < 0,
        isIE8: navigator.userAgent.indexOf("MSIE 8.0") >= 0 && navigator.userAgent.indexOf("Trident") >= 0,
        isOpera: navigator.userAgent.indexOf("Opera") >= 0,
        isChrome: navigator.userAgent.indexOf("Chrome") >= 0,
        isKHTML: navigator.userAgent.indexOf("Safari") >= 0 || navigator.userAgent.indexOf("Konqueror") >= 0,
        isFF: navigator.userAgent.indexOf("Firefox") >= 0,
        isIPad: navigator.userAgent.search(/iPad/gi) >= 0,
        isEdge: -1 != navigator.userAgent.indexOf("Edge")
    };
    t.exports = i
}, , function (t, e, i) {
}, function (t, e) {
    t.exports = function (t) {
        t.locale = {
            date: {
                month_full: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
                month_short: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                day_full: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
                day_short: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]
            },
            labels: {
                new_task: "New task",
                icon_save: "Save",
                icon_cancel: "Cancel",
                icon_details: "Details",
                icon_edit: "Edit",
                icon_delete: "Delete",
                confirm_closing: "",
                confirm_deleting: "Task will be deleted permanently, are you sure?",
                section_description: "Description",
                section_time: "Time period",
                section_type: "Type",
                column_wbs: "WBS",
                column_text: "Task name",
                column_start_date: "Start time",
                column_duration: "Duration",
                column_add: "",
                link: "Link",
                confirm_link_deleting: "will be deleted",
                link_start: " (start)",
                link_end: " (end)",
                type_task: "Task",
                type_project: "Project",
                type_milestone: "Milestone",
                minutes: "Minutes",
                hours: "Hours",
                days: "Days",
                weeks: "Week",
                months: "Months",
                years: "Years",
                message_ok: "OK",
                message_cancel: "Cancel"
            }
        }
    }
}, function (t, e, i) {
    var n = i(0), r = i(3);

    function a(t, e, i, n, r) {
        return this.date = t, this.unit = e, this.task = i, this.id = n, this.calendar = r, this
    }

    function s(t, e, i, n, r, a) {
        return this.date = t, this.dir = e, this.unit = i, this.task = n, this.id = r, this.calendar = a, this
    }

    function o(t, e, i, n, r, a, s) {
        return this.start_date = t, this.duration = e, this.unit = i, this.step = n, this.task = r, this.id = a, this.calendar = s, this
    }

    function l(t, e, i, n) {
        return this.start_date = t, this.end_date = e, this.task = i, this.calendar = n, this.unit = null, this.step = null, this
    }

    t.exports = function (t) {
        return {
            getWorkHoursArguments: function () {
                var t = arguments[0];
                return t = r.isDate(t) ? {date: t} : n.mixin({}, t)
            }, setWorkTimeArguments: function () {
                return arguments[0]
            }, unsetWorkTimeArguments: function () {
                return arguments[0]
            }, isWorkTimeArguments: function () {
                var e, i = arguments[0];
                return i instanceof a ? i : ((e = i.date ? new a(i.date, i.unit, i.task, null, i.calendar) : new a(arguments[0], arguments[1], arguments[2], null, arguments[3])).unit = e.unit || t.config.duration_unit, e)
            }, getClosestWorkTimeArguments: function (e) {
                var i, n = arguments[0];
                return n instanceof s ? n : (i = r.isDate(n) ? new s(n) : new s(n.date, n.dir, n.unit, n.task, null, n.calendar), n.id && (i.task = n), i.dir = n.dir || "any", i.unit = n.unit || t.config.duration_unit, i)
            }, _getStartEndConfig: function (e) {
                var i, n = l;
                return e instanceof n ? e : (r.isDate(e) ? i = new n(arguments[0], arguments[1], arguments[2], arguments[3]) : (i = new n(e.start_date, e.end_date, e.task), e.id && (i.task = e)), i.unit = i.unit || t.config.duration_unit, i.step = i.step || t.config.duration_step, i.start_date = i.start_date || i.start || i.date, i)
            }, getDurationArguments: function (t, e, i, n) {
                return this._getStartEndConfig.apply(this, arguments)
            }, hasDurationArguments: function (t, e, i, n) {
                return this._getStartEndConfig.apply(this, arguments)
            }, calculateEndDateArguments: function (e, i, n, a) {
                var s, l = arguments[0];
                return l instanceof o ? l : (s = r.isDate(l) ? new o(arguments[0], arguments[1], arguments[2], void 0, arguments[3], void 0, arguments[4]) : new o(l.start_date, l.duration, l.unit, l.step, l.task, null, l.calendar), l.id && (s.task = l), s.unit = s.unit || t.config.duration_unit, s.step = s.step || t.config.duration_step, s)
            }
        }
    }
}, function (t, e) {
    function i(t, e, i) {
        for (var n = 0; n < e.length; n++) t.isLinkExists(e[n]) && (i[e[n]] = t.getLink(e[n]))
    }

    function n(t, e, n) {
        i(t, e.$source, n), i(t, e.$target, n)
    }

    t.exports = {
        getSubtreeLinks: function (t, e) {
            var i = {};
            return t.isTaskExists(e) && n(t, t.getTask(e), i), t.eachTask(function (e) {
                n(t, e, i)
            }, e), i
        }, getSubtreeTasks: function (t, e) {
            var i = {};
            return t.eachTask(function (t) {
                i[t.id] = t
            }, e), i
        }
    }
}, function (t, e, i) {
    var n = i(24);

    function r(t) {
        var e = function (t) {
            var e = t.config.scale_unit, i = t.config.step;
            if (t.config.scale_offset_minimal) {
                var r = new n(t), a = [r.primaryScale()].concat(t.config.subscales);
                r.sortScales(a), e = a[a.length - 1].unit, i = a[a.length - 1].step || 1
            }
            return {unit: e, step: i}
        }(t), i = e.unit, r = e.step, a = function (t, e) {
            var i = {start_date: null, end_date: null};
            if (e.config.start_date && e.config.end_date) {
                i.start_date = e.date[t + "_start"](new Date(e.config.start_date));
                var n = new Date(e.config.end_date), r = e.date[t + "_start"](new Date(n));
                n = +n != +r ? e.date.add(r, 1, t) : r, i.end_date = n
            }
            return i
        }(i, t);
        a.start_date && a.end_date || ((a = function (t) {
            return t.getSubtaskDates()
        }(t)).start_date && a.end_date || (a = {
            start_date: new Date,
            end_date: new Date
        }), a.start_date = t.date[i + "_start"](a.start_date), a.start_date = t.calculateEndDate({
            start_date: t.date[i + "_start"](a.start_date),
            duration: -1,
            unit: i,
            step: r
        }), a.end_date = t.date[i + "_start"](a.end_date), a.end_date = t.calculateEndDate({
            start_date: a.end_date,
            duration: 2,
            unit: i,
            step: r
        })), t._min_date = a.start_date, t._max_date = a.end_date
    }

    t.exports = function (t) {
        r(t), function (t) {
            if (t.config.fit_tasks) {
                var e = +t._min_date, i = +t._max_date;
                if (+t._min_date != e || +t._max_date != i) return t.render(), t.callEvent("onScaleAdjusted", []), !0
            }
        }(t)
    }
}, function (t, e, i) {
    var n = i(0), r = {
        $create: function (t) {
            return n.mixin(t || [], this)
        }, $removeAt: function (t, e) {
            t >= 0 && this.splice(t, e || 1)
        }, $remove: function (t) {
            this.$removeAt(this.$find(t))
        }, $insertAt: function (t, e) {
            if (e || 0 === e) {
                var i = this.splice(e, this.length - e);
                this[e] = t, this.push.apply(this, i)
            } else this.push(t)
        }, $find: function (t) {
            for (var e = 0; e < this.length; e++) if (t == this[e]) return e;
            return -1
        }, $each: function (t, e) {
            for (var i = 0; i < this.length; i++) t.call(e || this, this[i])
        }, $map: function (t, e) {
            for (var i = 0; i < this.length; i++) this[i] = t.call(e || this, this[i]);
            return this
        }, $filter: function (t, e) {
            for (var i = 0; i < this.length; i++) t.call(e || this, this[i]) || (this.splice(i, 1), i--);
            return this
        }
    };
    t.exports = r
}, function (t, e, i) {
    var n = i(19), r = i(0), a = i(4), s = function (t) {
        return this.pull = {}, this.$initItem = t.initItem, this.visibleOrder = n.$create(), this.fullOrder = n.$create(), this._skip_refresh = !1, this._filterRule = null, this._searchVisibleOrder = {}, this.$config = t, a(this), this
    };
    s.prototype = {
        _parseInner: function (t) {
            for (var e = null, i = [], n = 0, r = t.length; n < r; n++) e = t[n], this.$initItem && (e = this.$initItem(e)), this.callEvent("onItemLoading", [e]) && (this.pull.hasOwnProperty(e.id) || (this.fullOrder.push(e.id), i.push(e)), this.pull[e.id] = e);
            return i
        }, parse: function (t) {
            this.callEvent("onBeforeParse", [t]);
            var e = this._parseInner(t);
            this.refresh(), this.callEvent("onParse", [e])
        }, getItem: function (t) {
            return this.pull[t]
        }, _updateOrder: function (t) {
            t.call(this.visibleOrder), t.call(this.fullOrder)
        }, updateItem: function (t, e) {
            if (r.defined(e) || (e = this.getItem(t)), !this._skip_refresh && !1 === this.callEvent("onBeforeUpdate", [e.id, e])) return !1;
            this.pull[t] = e, this._skip_refresh || (this.callEvent("onAfterUpdate", [e.id, e]), this.callEvent("onStoreUpdated", [e.id, e, "update"]))
        }, _removeItemInner: function (t) {
            this._updateOrder(function () {
                this.$remove(t)
            }), delete this.pull[t]
        }, removeItem: function (t) {
            var e = this.getItem(t);
            if (!this._skip_refresh && !1 === this.callEvent("onBeforeDelete", [e.id, e])) return !1;
            this._removeItemInner(t), this._skip_refresh || (this.filter(), this.callEvent("onAfterDelete", [e.id, e]), this.callEvent("onStoreUpdated", [e.id, e, "delete"]))
        }, _addItemInner: function (t, e) {
            if (this.exists(t.id)) this.silent(function () {
                this.updateItem(t.id, t)
            }); else {
                var i = this.visibleOrder, n = i.length;
                (!r.defined(e) || e < 0) && (e = n), e > n && (e = Math.min(i.length, e))
            }
            this.pull[t.id] = t, this._skip_refresh || this._updateOrder(function () {
                -1 === this.$find(t.id) && this.$insertAt(t.id, e)
            }), this.filter()
        }, isVisible: function (t) {
            return this.visibleOrder.$find(t) > -1
        }, getVisibleItems: function () {
            return this.getIndexRange()
        }, addItem: function (t, e) {
            return r.defined(t.id) || (t.id = r.uid()), this.$initItem && (t = this.$initItem(t)), !(!this._skip_refresh && !1 === this.callEvent("onBeforeAdd", [t.id, t])) && (this._addItemInner(t, e), this._skip_refresh || (this.callEvent("onAfterAdd", [t.id, t]), this.callEvent("onStoreUpdated", [t.id, t, "add"])), t.id)
        }, _changeIdInner: function (t, e) {
            this.pull[t] && (this.pull[e] = this.pull[t]);
            var i = this._searchVisibleOrder[t];
            this.pull[e].id = e, this._updateOrder(function () {
                this[this.$find(t)] = e
            }), this._searchVisibleOrder[e] = i, delete this._searchVisibleOrder[t], delete this.pull[t]
        }, changeId: function (t, e) {
            this._changeIdInner(t, e), this.callEvent("onIdChange", [t, e])
        }, exists: function (t) {
            return !!this.pull[t]
        }, _moveInner: function (t, e) {
            var i = this.getIdByIndex(t);
            this._updateOrder(function () {
                this.$removeAt(t), this.$insertAt(i, Math.min(this.length, e))
            })
        }, move: function (t, e) {
            var i = this.getIdByIndex(t), n = this.getItem(i);
            this._moveInner(t, e), this._skip_refresh || this.callEvent("onStoreUpdated", [n.id, n, "move"])
        }, clearAll: function () {
            this.pull = {}, this.visibleOrder = n.$create(), this.fullOrder = n.$create(), this._skip_refresh || (this.callEvent("onClearAll", []), this.refresh())
        }, silent: function (t, e) {
            this._skip_refresh = !0, t.call(e || this), this._skip_refresh = !1
        }, arraysEqual: function (t, e) {
            if (t.length !== e.length) return !1;
            for (var i = 0; i < t.length; i++) if (t[i] !== e[i]) return !1;
            return !0
        }, refresh: function (t, e) {
            var i;
            if (!this._skip_refresh && (i = t ? [t, this.pull[t], "paint"] : [null, null, null], !1 !== this.callEvent("onBeforeStoreUpdate", i))) {
                if (t) {
                    if (!e) {
                        var n = this.visibleOrder;
                        this.filter(), this.arraysEqual(n, this.visibleOrder) || (t = void 0)
                    }
                } else this.filter();
                i = t ? [t, this.pull[t], "paint"] : [null, null, null], this.callEvent("onStoreUpdated", i)
            }
        }, count: function () {
            return this.fullOrder.length
        }, countVisible: function () {
            return this.visibleOrder.length
        }, sort: function (t) {
        }, serialize: function () {
        }, eachItem: function (t) {
            for (var e = 0; e < this.fullOrder.length; e++) {
                var i = this.pull[this.fullOrder[e]];
                t.call(this, i)
            }
        }, filter: function (t) {
            var e = n.$create();
            this.eachItem(function (t) {
                this.callEvent("onFilterItem", [t.id, t]) && e.push(t.id)
            }), this.visibleOrder = e, this._searchVisibleOrder = {};
            for (var i = 0; i < this.visibleOrder.length; i++) this._searchVisibleOrder[this.visibleOrder[i]] = i
        }, getIndexRange: function (t, e) {
            e = Math.min(e || 1 / 0, this.countVisible() - 1);
            for (var i = [], n = t || 0; n <= e; n++) i.push(this.getItem(this.visibleOrder[n]));
            return i
        }, getItems: function () {
            var t = [];
            for (var e in this.pull) t.push(this.pull[e]);
            return t
        }, getIdByIndex: function (t) {
            return this.visibleOrder[t]
        }, getIndexById: function (t) {
            var e = this._searchVisibleOrder[t];
            return void 0 === e && (e = -1), e
        }, _getNullIfUndefined: function (t) {
            return void 0 === t ? null : t
        }, getFirst: function () {
            return this._getNullIfUndefined(this.visibleOrder[0])
        }, getLast: function () {
            return this._getNullIfUndefined(this.visibleOrder[this.visibleOrder.length - 1])
        }, getNext: function (t) {
            return this._getNullIfUndefined(this.visibleOrder[this.getIndexById(t) + 1])
        }, getPrev: function (t) {
            return this._getNullIfUndefined(this.visibleOrder[this.getIndexById(t) - 1])
        }, destructor: function () {
            this.detachAllEvents(), this.pull = null, this.$initItem = null, this.visibleOrder = null, this.fullOrder = null, this._skip_refresh = null, this._filterRule = null, this._searchVisibleOrder = null
        }
    }, t.exports = s
}, function (t, e) {
    t.exports = function (t) {
        function e(e, a) {
            if (!t._isAllowedUnscheduledTask(e)) {
                var s = a.getItemPosition(e), o = a.$getConfig(), l = a.$getTemplates(), c = a.getItemHeight(),
                    d = t.getTaskType(e.type), h = Math.floor((t.config.row_height - c) / 2);
                d == o.types.milestone && o.link_line_width > 1 && (h += 1), d == o.types.milestone && (s.left -= Math.round(c / 2), s.width = c);
                var u = document.createElement("div"), g = Math.round(s.width);
                a.$config.item_attribute && u.setAttribute(a.$config.item_attribute, e.id), o.show_progress && d != o.types.milestone && function (e, i, n, r, a) {
                    var s = 1 * e.progress || 0;
                    n = Math.max(n - 2, 0);
                    var o = document.createElement("div"), l = Math.round(n * s);
                    l = Math.min(n, l), e.progressColor && (o.style.backgroundColor = e.progressColor, o.style.opacity = 1), o.style.width = l + "px", o.className = "gantt_task_progress", o.innerHTML = a.progress_text(e.start_date, e.end_date, e), r.rtl && (o.style.position = "absolute", o.style.right = "0px");
                    var c = document.createElement("div");
                    if (c.className = "gantt_task_progress_wrapper", c.appendChild(o), i.appendChild(c), t.config.drag_progress && !t.isReadonly(e)) {
                        var d = document.createElement("div"), h = l;
                        r.rtl && (h = n - l), d.style.left = h + "px", d.className = "gantt_task_progress_drag", o.appendChild(d), i.appendChild(d)
                    }
                }(e, u, g, o, l);
                var f = function (e, i, n) {
                    var r = document.createElement("div");
                    return t.getTaskType(e.type) != t.config.types.milestone && (r.innerHTML = n.task_text(e.start_date, e.end_date, e)), r.className = "gantt_task_content", r
                }(e, 0, l);
                e.textColor && (f.style.color = e.textColor), u.appendChild(f);
                var _ = function (e, i, n, r) {
                    var a = r.$getConfig(), s = [e];
                    i && s.push(i);
                    var o = t.getState(), l = t.getTask(n);
                    if (t.getTaskType(l.type) == a.types.milestone ? s.push("gantt_milestone") : t.getTaskType(l.type) == a.types.project && s.push("gantt_project"), s.push("gantt_bar_" + t.getTaskType(l.type)), t.isSummaryTask(l) && s.push("gantt_dependent_task"), t.isSplitTask(l) && s.push("gantt_split_parent"), a.select_task && n == o.selected_task && s.push("gantt_selected"), n == o.drag_id && (s.push("gantt_drag_" + o.drag_mode), o.touch_drag && s.push("gantt_touch_" + o.drag_mode)), o.link_source_id == n && s.push("gantt_link_source"), o.link_target_id == n && s.push("gantt_link_target"), a.highlight_critical_path && t.isCriticalTask && t.isCriticalTask(l) && s.push("gantt_critical_task"), o.link_landing_area && o.link_target_id && o.link_source_id && o.link_target_id != o.link_source_id) {
                        var c = o.link_source_id, d = o.link_from_start, h = o.link_to_start,
                            u = t.isLinkAllowed(c, n, d, h), g = "";
                        g = u ? h ? "link_start_allow" : "link_finish_allow" : h ? "link_start_deny" : "link_finish_deny", s.push(g)
                    }
                    return s.join(" ")
                }("gantt_task_line", l.task_class(e.start_date, e.end_date, e), e.id, a);
                (e.color || e.progressColor || e.textColor) && (_ += " gantt_task_inline_color"), u.className = _;
                var p = ["left:" + s.left + "px", "top:" + (h + s.top) + "px", "height:" + c + "px", "line-height:" + Math.max(c < 30 ? c - 2 : c, 0) + "px", "width:" + g + "px"];
                e.color && p.push("background-color:" + e.color), e.textColor && p.push("color:" + e.textColor), u.style.cssText = p.join(";");
                var v = function (t, e, r) {
                    var a = "gantt_left " + n(!e.rtl, t);
                    return i(t, r.leftside_text, a)
                }(e, o, l);
                v && u.appendChild(v), (v = function (t, e, r) {
                    var a = "gantt_right " + n(!!e.rtl, t);
                    return i(t, r.rightside_text, a)
                }(e, o, l)) && u.appendChild(v), t._waiAria.setTaskBarAttr(e, u);
                var m = t.getState();
                return t.isReadonly(e) || (o.drag_resize && !t.isSummaryTask(e) && d != o.types.milestone && r(u, "gantt_task_drag", e, function (t) {
                    var e = document.createElement("div");
                    return e.className = t, e
                }, o), o.drag_links && o.show_links && r(u, "gantt_link_control", e, function (t) {
                    var e = document.createElement("div");
                    e.className = t, e.style.cssText = ["height:" + c + "px", "line-height:" + c + "px"].join(";");
                    var i = document.createElement("div");
                    i.className = "gantt_link_point";
                    var n = !1;
                    return m.link_source_id && o.touch && (n = !0), i.style.display = n ? "block" : "", e.appendChild(i), e
                }, o)), u
            }
        }

        function i(t, e, i) {
            if (!e) return null;
            var n = e(t.start_date, t.end_date, t);
            if (!n) return null;
            var r = document.createElement("div");
            return r.className = "gantt_side_content " + i, r.innerHTML = n, r
        }

        function n(e, i) {
            var n = function (e) {
                return e ? {
                    $source: [t.config.links.start_to_start],
                    $target: [t.config.links.start_to_start, t.config.links.finish_to_start]
                } : {
                    $source: [t.config.links.finish_to_start, t.config.links.finish_to_finish],
                    $target: [t.config.links.finish_to_finish]
                }
            }(e);
            for (var r in n) for (var a = i[r], s = 0; s < a.length; s++) for (var o = t.getLink(a[s]), l = 0; l < n[r].length; l++) if (o.type == n[r][l]) return "gantt_link_crossing";
            return ""
        }

        function r(e, i, n, r, a) {
            var s, o = t.getState();
            +n.start_date >= +o.min_date && ((s = r([i, a.rtl ? "task_right" : "task_left", "task_start_date"].join(" "))).setAttribute("data-bind-property", "start_date"), e.appendChild(s)), +n.end_date <= +o.max_date && ((s = r([i, a.rtl ? "task_left" : "task_right", "task_end_date"].join(" "))).setAttribute("data-bind-property", "end_date"), e.appendChild(s))
        }

        return function (i, n) {
            var r = n.$getConfig().type_renderers[t.getTaskType(i.type)], a = e;
            return r ? r.call(t, i, function (e) {
                return a.call(t, e, n)
            }, n) : a.call(t, i, n)
        }
    }
}, function (t, e, i) {
    var n = i(1), r = i(0), a = i(4), s = i(108), o = i(23), l = function (t, e, i, n) {
        this.$config = r.mixin({}, e || {}), this.$gantt = n, this.$parent = t, a(this), this.$state = {}
    };
    l.prototype = {
        init: function (t) {
            var e = this.$gantt, n = e._waiAria.gridAttrString(), r = e._waiAria.gridDataAttrString();
            t.innerHTML = "<div class='gantt_grid' style='height:inherit;width:inherit;' " + n + "></div>", this.$grid = t.childNodes[0], this.$grid.innerHTML = "<div class='gantt_grid_scale' " + e._waiAria.gridScaleRowAttrString() + "></div><div class='gantt_grid_data' " + r + "></div>", this.$grid_scale = this.$grid.childNodes[0], this.$grid_data = this.$grid.childNodes[1];
            var a = this.$getConfig()[this.$config.bind + "_attribute"];
            if (!a && this.$config.bind && (a = this.$config.bind + "_id"), this.$config.item_attribute = a || null, !this.$config.layers) {
                var o = this._createLayerConfig();
                this.$config.layers = o
            }
            var l = s(e, this);
            l.init(), this._renderHeaderResizers = l.doOnRender, this._mouseDelegates = i(10)(e), this._addLayers(this.$gantt), this._initEvents(), this.callEvent("onReady", [])
        }, _validateColumnWidth: function (t, e) {
            var i = t[e];
            if (i && "*" != i) {
                var n = this.$gantt, r = 1 * i;
                isNaN(r) ? n.assert(!1, "Wrong " + e + " value of column " + t.name) : t[e] = r
            }
        }, setSize: function (t, e) {
            this.$config.width = this.$state.width = t, this.$state.height = e;
            for (var i, n = this.getGridColumns(), r = 0, a = 0, s = n.length; a < s; a++) this._validateColumnWidth(n[a], "min_width"), this._validateColumnWidth(n[a], "max_width"), this._validateColumnWidth(n[a], "width"), r += 1 * n[a].width;
            !isNaN(r) && this.$config.scrollable || (r = i = this._setColumnsWidth(t + 1)), this.$config.scrollable ? (this.$grid_scale.style.width = r + "px", this.$grid_data.style.width = r + "px") : (this.$grid_scale.style.width = "inherit", this.$grid_data.style.width = "inherit"), this.$config.width -= 1;
            var o = this.$getConfig();
            i !== t && (o.grid_width = i, this.$config.width = i - 1);
            var l = Math.max(this.$state.height - o.scale_height, 0);
            this.$grid_data.style.height = l + "px", this.refresh()
        }, getSize: function () {
            var t = this.$getConfig(), e = this.$config.rowStore, i = e ? t.row_height * e.countVisible() : 0,
                n = this._getGridWidth();
            return {
                x: this.$state.width,
                y: this.$state.height,
                contentX: this.isVisible() ? n : 0,
                contentY: this.isVisible() ? t.scale_height + i : 0,
                scrollHeight: this.isVisible() ? i : 0,
                scrollWidth: this.isVisible() ? n : 0
            }
        }, refresh: function () {
            this.$config.bind && (this.$config.rowStore = this.$gantt.getDatastore(this.$config.bind)), this._initSmartRenderingPlaceholder(), this._calculateGridWidth(), this._renderGridHeader()
        }, scrollTo: function (t, e) {
            this.isVisible() && (1 * t == t && (this.$state.scrollLeft = this.$grid.scrollLeft = t), 1 * e == e && (this.$state.scrollTop = this.$grid_data.scrollTop = e))
        }, getColumnIndex: function (t) {
            for (var e = this.$getConfig().columns, i = 0; i < e.length; i++) if (e[i].name == t) return i;
            return null
        }, getColumn: function (t) {
            var e = this.getColumnIndex(t);
            return null === e ? null : this.$getConfig().columns[e]
        }, getGridColumns: function () {
            return this.$getConfig().columns.slice()
        }, isVisible: function () {
            return this.$parent && this.$parent.$config ? !this.$parent.$config.hidden : this.$grid.offsetWidth
        }, getItemHeight: function () {
            return this.$getConfig().row_height
        }, _createLayerConfig: function () {
            var t = this;
            return [{
                renderer: this.$gantt.$ui.layers.gridLine, container: this.$grid_data, filter: [function () {
                    return t.isVisible()
                }]
            }]
        }, _addLayers: function (t) {
            if (this.$config.bind) {
                this._taskLayers = [];
                var e = this, i = this.$gantt.$services.getService("layers"), n = i.getDataRender(this.$config.bind);
                n || (n = i.createDataRender({
                    name: this.$config.bind, defaultContainer: function () {
                        return e.$grid_data
                    }
                }));
                for (var r = this.$config.layers, a = 0; r && a < r.length; a++) {
                    var s = r[a];
                    s.host = this;
                    var o = n.addLayer(s);
                    this._taskLayers.push(o)
                }
                this.$config.bind && (this.$config.rowStore = this.$gantt.getDatastore(this.$config.bind)), this._initSmartRenderingPlaceholder()
            }
        }, _refreshPlaceholderOnStoreUpdate: function (t) {
            var e = this.$getConfig(), i = this.$config.rowStore;
            if (i && null === t && this.isVisible() && e.smart_rendering) {
                var n;
                if (this.$config.scrollY) {
                    var r = this.$gantt.$ui.getView(this.$config.scrollY);
                    r && (n = r.getScrollState().scrollSize)
                }
                if (n || (n = i ? e.row_height * i.countVisible() : 0), n) {
                    this.$rowsPlaceholder && this.$rowsPlaceholder.parentNode && this.$rowsPlaceholder.parentNode.removeChild(this.$rowsPlaceholder);
                    var a = this.$rowsPlaceholder = document.createElement("div");
                    a.style.visibility = "hidden", a.style.height = n + "px", a.style.width = "1px", this.$grid_data.appendChild(a)
                }
            }
        }, _initSmartRenderingPlaceholder: function () {
            var t = this.$config.rowStore;
            t && (this._initSmartRenderingPlaceholder = function () {
            }, this._staticBgHandler = t.attachEvent("onStoreUpdated", r.bind(this._refreshPlaceholderOnStoreUpdate, this)))
        }, _initEvents: function () {
            var t = this.$gantt;
            this._mouseDelegates.delegate("click", "gantt_close", t.bind(function (t, e, i) {
                var r = this.$config.rowStore;
                if (!r) return !0;
                var a = n.locateAttribute(t, this.$config.item_attribute);
                return a && r.close(a.getAttribute(this.$config.item_attribute)), !1
            }, this), this.$grid), this._mouseDelegates.delegate("click", "gantt_open", t.bind(function (t, e, i) {
                var r = this.$config.rowStore;
                if (!r) return !0;
                var a = n.locateAttribute(t, this.$config.item_attribute);
                return a && r.open(a.getAttribute(this.$config.item_attribute)), !1
            }, this), this.$grid)
        }, _clearLayers: function (t) {
            var e = this.$gantt.$services.getService("layers").getDataRender(this.$config.bind);
            if (this._taskLayers) for (var i = 0; i < this._taskLayers.length; i++) e.removeLayer(this._taskLayers[i]);
            this._taskLayers = []
        }, _getColumnWidth: function (t, e, i) {
            var n = t.min_width || e.min_grid_column_width, r = Math.max(i, n || 10);
            return t.max_width && (r = Math.min(r, t.max_width)), r
        }, _getGridWidthLimits: function () {
            for (var t = this.$getConfig(), e = this.getGridColumns(), i = 0, n = 0, r = 0; r < e.length; r++) i += e[r].min_width ? e[r].min_width : t.min_grid_column_width, void 0 !== n && (n = e[r].max_width ? n + e[r].max_width : void 0);
            return [i, n]
        }, _setColumnsWidth: function (t, e) {
            var i = this.$getConfig(), n = this.getGridColumns(), r = 0, a = t;
            e = window.isNaN(e) ? -1 : e;
            for (var s = 0, o = n.length; s < o; s++) r += 1 * n[s].width;
            if (window.isNaN(r)) {
                this._calculateGridWidth(), r = 0;
                for (s = 0, o = n.length; s < o; s++) r += 1 * n[s].width
            }
            var l = a - r, c = 0;
            for (s = 0; s < e + 1; s++) c += n[s].width;
            r -= c;
            for (s = e + 1; s < n.length; s++) {
                var d = n[s], h = Math.round(l * (d.width / r));
                l < 0 ? d.min_width && d.width + h < d.min_width ? h = d.min_width - d.width : !d.min_width && i.min_grid_column_width && d.width + h < i.min_grid_column_width && (h = i.min_grid_column_width - d.width) : d.max_width && d.width + h > d.max_width && (h = d.max_width - d.width), r -= d.width, d.width += h, l -= h
            }
            for (var u = l > 0 ? 1 : -1; l > 0 && 1 === u || l < 0 && -1 === u;) {
                var g = l;
                for (s = e + 1; s < n.length; s++) {
                    var f;
                    if ((f = n[s].width + u) == this._getColumnWidth(n[s], i, f) && (l -= u, n[s].width = f), !l) break
                }
                if (g == l) break
            }
            l && e > -1 && ((f = n[e].width + l) == this._getColumnWidth(n[e], i, f) && (n[e].width = f));
            return this._getColsTotalWidth()
        }, _getColsTotalWidth: function () {
            for (var t = this.getGridColumns(), e = 0, i = 0; i < t.length; i++) {
                var n = parseFloat(t[i].width);
                if (window.isNaN(n)) return !1;
                e += n
            }
            return e
        }, _calculateGridWidth: function () {
            for (var t = this.$getConfig(), e = this.getGridColumns(), i = 0, n = [], r = [], a = 0; a < e.length; a++) {
                var s = parseFloat(e[a].width);
                window.isNaN(s) && (s = t.min_grid_column_width || 10, n.push(a)), r[a] = s, i += s
            }
            var o = this._getGridWidth() + 1;
            if (t.autofit || n.length) {
                var l = o - i;
                if (t.autofit) for (a = 0; a < r.length; a++) {
                    var c = Math.round(l / (r.length - a));
                    r[a] += c, (d = this._getColumnWidth(e[a], t, r[a])) != r[a] && (c = d - r[a], r[a] = d), l -= c
                } else if (n.length) for (a = 0; a < n.length; a++) {
                    c = Math.round(l / (n.length - a));
                    var d, h = n[a];
                    r[h] += c, (d = this._getColumnWidth(e[h], t, r[h])) != r[h] && (c = d - r[h], r[h] = d), l -= c
                }
                for (a = 0; a < r.length; a++) e[a].width = r[a]
            } else {
                var u = o != i;
                this.$config.width = i - 1, t.grid_width = i, u && this.$parent._setContentSize(this.$config.width, this.$config.height)
            }
        }, _renderGridHeader: function () {
            var t = this.$gantt, e = this.$getConfig(), i = this.$gantt.locale, n = this.$gantt.templates,
                r = this.getGridColumns();
            e.rtl && (r = r.reverse());
            for (var a = [], s = 0, o = i.labels, l = e.scale_height - 1, c = 0; c < r.length; c++) {
                var d = c == r.length - 1, h = r[c];
                h.name || (h.name = t.uid() + "");
                var u = 1 * h.width, g = this._getGridWidth();
                d && g > s + u && (h.width = u = g - s), s += u;
                var f = t._sort && h.name == t._sort.name ? "<div class='gantt_sort gantt_" + t._sort.direction + "'></div>" : "",
                    _ = ["gantt_grid_head_cell", "gantt_grid_head_" + h.name, d ? "gantt_last_cell" : "", n.grid_header_class(h.name, h)].join(" "),
                    p = "width:" + (u - (d ? 1 : 0)) + "px;", v = h.label || o["column_" + h.name];
                v = v || "";
                var m = "<div class='" + _ + "' style='" + p + "' " + t._waiAria.gridScaleCellAttrString(h, v) + " data-column-id='" + h.name + "' column_id='" + h.name + "'>" + v + f + "</div>";
                a.push(m)
            }
            this.$grid_scale.style.height = e.scale_height + "px", this.$grid_scale.style.lineHeight = l + "px", this.$grid_scale.innerHTML = a.join(""), this._renderHeaderResizers && this._renderHeaderResizers()
        }, _getGridWidth: function () {
            return this.$config.width
        }, destructor: function () {
            this._clearLayers(this.$gantt), this._mouseDelegates && (this._mouseDelegates.destructor(), this._mouseDelegates = null), this.$grid = null, this.$grid_scale = null, this.$grid_data = null, this.$gantt = null, this.$config.rowStore && (this.$config.rowStore.detachEvent(this._staticBgHandler), this.$config.rowStore = null), this.callEvent("onDestroy", []), this.detachAllEvents()
        }
    }, r.mixin(l.prototype, o()), t.exports = l
}, function (t, e) {
    t.exports = function () {
        return {
            getRowTop: function (t) {
                return t * this.$getConfig().row_height
            }, getItemTop: function (t) {
                if (this.$config.rowStore) {
                    var e = this.$config.rowStore;
                    if (!e) return 0;
                    if (e.getParent && e.exists(t) && e.exists(e.getParent(t))) {
                        var i = e.getItem(e.getParent(t));
                        if (this.$gantt.isSplitTask(i)) return this.getRowTop(e.getIndexById(i.id))
                    }
                    return this.getRowTop(e.getIndexById(t))
                }
                return 0
            }
        }
    }
}, function (t, e, i) {
    var n = i(0);
    t.exports = function (t) {
        var e = t.date, i = t.$services;
        return {
            getSum: function (t, e, i) {
                void 0 === i && (i = t.length - 1), void 0 === e && (e = 0);
                for (var n = 0, r = e; r <= i; r++) n += t[r];
                return n
            }, setSumWidth: function (t, e, i, n) {
                var r = e.width;
                void 0 === n && (n = r.length - 1), void 0 === i && (i = 0);
                var a = n - i + 1;
                if (!(i > r.length - 1 || a <= 0 || n > r.length - 1)) {
                    var s = t - this.getSum(r, i, n);
                    this.adjustSize(s, r, i, n), this.adjustSize(-s, r, n + 1), e.full_width = this.getSum(r)
                }
            }, splitSize: function (t, e) {
                for (var i = [], n = 0; n < e; n++) i[n] = 0;
                return this.adjustSize(t, i), i
            }, adjustSize: function (t, e, i, n) {
                i || (i = 0), void 0 === n && (n = e.length - 1);
                for (var r = n - i + 1, a = this.getSum(e, i, n), s = i; s <= n; s++) {
                    var o = Math.floor(t * (a ? e[s] / a : 1 / r));
                    a -= e[s], t -= o, r--, e[s] += o
                }
                e[e.length - 1] += t
            }, sortScales: function (t) {
                function i(t, i) {
                    var n = new Date(1970, 0, 1);
                    return e.add(n, i, t) - n
                }

                t.sort(function (t, e) {
                    return i(t.unit, t.step) < i(e.unit, e.step) ? 1 : i(t.unit, t.step) > i(e.unit, e.step) ? -1 : 0
                });
                for (var n = 0; n < t.length; n++) t[n].index = n
            }, primaryScale: function () {
                return i.getService("templateLoader").initTemplate("date_scale", void 0, void 0, i.config(), i.templates()), {
                    unit: i.config().scale_unit,
                    step: i.config().step,
                    template: i.templates().date_scale,
                    date: i.config().date_scale,
                    css: i.templates().scale_cell_class
                }
            }, prepareConfigs: function (t, e, i, n, r, a, s) {
                for (var o = this.splitSize(n, t.length), l = i, c = [], d = t.length - 1; d >= 0; d--) {
                    var h = d == t.length - 1, u = this.initScaleConfig(t[d], r, a);
                    h && this.processIgnores(u), this.initColSizes(u, e, l, o[d]), this.limitVisibleRange(u), h && (l = u.full_width), c.unshift(u)
                }
                for (d = 0; d < c.length - 1; d++) this.alineScaleColumns(c[c.length - 1], c[d]);
                for (d = 0; d < c.length; d++) s && this.reverseScale(c[d]), this.setPosSettings(c[d]);
                return c
            }, reverseScale: function (t) {
                t.width = t.width.reverse(), t.trace_x = t.trace_x.reverse();
                var e = t.trace_indexes;
                t.trace_indexes = {}, t.trace_index_transition = {}, t.rtl = !0;
                for (var i = 0; i < t.trace_x.length; i++) t.trace_indexes[t.trace_x[i].valueOf()] = i, t.trace_index_transition[e[t.trace_x[i].valueOf()]] = i;
                return t
            }, setPosSettings: function (t) {
                for (var e = 0, i = t.trace_x.length; e < i; e++) t.left.push((t.width[e - 1] || 0) + (t.left[e - 1] || 0))
            }, _ignore_time_config: function (t, n) {
                if (i.config().skip_off_time) {
                    for (var r = !0, a = t, s = 0; s < n.step; s++) s && (a = e.add(t, s, n.unit)), r = r && !this.isWorkTime(a, n.unit);
                    return r
                }
                return !1
            }, processIgnores: function (t) {
                t.ignore_x = {}, t.display_count = t.count
            }, initColSizes: function (t, i, n, r) {
                var a = n;
                t.height = r;
                var s = void 0 === t.display_count ? t.count : t.display_count;
                s || (s = 1), t.col_width = Math.floor(a / s), i && t.col_width < i && (t.col_width = i, a = t.col_width * s), t.width = [];
                for (var o = t.ignore_x || {}, l = 0; l < t.trace_x.length; l++) if (o[t.trace_x[l].valueOf()] || t.display_count == t.count) t.width[l] = 0; else {
                    var c = 1;
                    "month" == t.unit && (c = Math.round((e.add(t.trace_x[l], t.step, t.unit) - t.trace_x[l]) / 864e5)), t.width[l] = c
                }
                this.adjustSize(a - this.getSum(t.width), t.width), t.full_width = this.getSum(t.width)
            }, initScaleConfig: function (t, e, i) {
                var r = n.mixin({
                    count: 0,
                    col_width: 0,
                    full_width: 0,
                    height: 0,
                    width: [],
                    left: [],
                    trace_x: [],
                    trace_indexes: {},
                    min_date: new Date(e),
                    max_date: new Date(i)
                }, t);
                return this.eachColumn(t.unit, t.step, e, i, function (t) {
                    r.count++, r.trace_x.push(new Date(t)), r.trace_indexes[t.valueOf()] = r.trace_x.length - 1
                }), r.trace_x_ascending = r.trace_x.slice(), r
            }, iterateScales: function (t, e, i, n, r) {
                for (var a = e.trace_x, s = t.trace_x, o = i || 0, l = n || s.length - 1, c = 0, d = 1; d < a.length; d++) {
                    var h = t.trace_indexes[+a[d]];
                    void 0 !== h && h <= l && (r && r.apply(this, [c, d, o, h]), o = h, c = d)
                }
            }, alineScaleColumns: function (t, e, i, n) {
                this.iterateScales(t, e, i, n, function (i, n, r, a) {
                    var s = this.getSum(t.width, r, a - 1);
                    this.getSum(e.width, i, n - 1) != s && this.setSumWidth(s, e, i, n - 1)
                })
            }, eachColumn: function (i, n, r, a, s) {
                var o = new Date(r), l = new Date(a);
                e[i + "_start"] && (o = e[i + "_start"](o));
                var c = new Date(o);
                for (+c >= +l && (l = e.add(c, n, i)); +c < +l;) {
                    s.call(this, new Date(c));
                    var d = c.getTimezoneOffset();
                    c = e.add(c, n, i), c = t._correct_dst_change(c, d, n, i), e[i + "_start"] && (c = e[i + "_start"](c))
                }
            }, limitVisibleRange: function (t) {
                var i = t.trace_x, n = t.width.length - 1, r = 0;
                if (+i[0] < +t.min_date && 0 != n) {
                    var a = Math.floor(t.width[0] * ((i[1] - t.min_date) / (i[1] - i[0])));
                    r += t.width[0] - a, t.width[0] = a, i[0] = new Date(t.min_date)
                }
                var s = i.length - 1, o = i[s], l = e.add(o, t.step, t.unit);
                if (+l > +t.max_date && s > 0 && (a = t.width[s] - Math.floor(t.width[s] * ((l - t.max_date) / (l - o))), r += t.width[s] - a, t.width[s] = a), r) {
                    for (var c = this.getSum(t.width), d = 0, h = 0; h < t.width.length; h++) {
                        var u = Math.floor(r * (t.width[h] / c));
                        t.width[h] += u, d += u
                    }
                    this.adjustSize(r - d, t.width)
                }
            }
        }
    }
}, function (t, e, i) {
    var n = i(2), r = i(1), a = function (t) {
        "use strict";

        function e(e, i, n) {
            var r = t.apply(this, arguments) || this;
            return e && (r.$root = !0), r._parseConfig(i), r.$name = "layout", r
        }

        return n(e, t), e.prototype.destructor = function () {
            this.$container && this.$view && r.removeNode(this.$view);
            for (var e = 0; e < this.$cells.length; e++) {
                this.$cells[e].destructor()
            }
            this.$cells = [], t.prototype.destructor.call(this)
        }, e.prototype._resizeScrollbars = function (t, e) {
            var i, n = !1, r = [], a = [];

            function s(t) {
                t.$parent.show(), n = !0, r.push(t)
            }

            function o(t) {
                t.$parent.hide(), n = !0, a.push(t)
            }

            for (var l = 0; l < e.length; l++) t[(i = e[l]).$config.scroll] ? o(i) : i.shouldHide() ? o(i) : i.shouldShow() ? s(i) : i.isVisible() ? r.push(i) : a.push(i);
            var c = {};
            for (l = 0; l < r.length; l++) r[l].$config.group && (c[r[l].$config.group] = !0);
            for (l = 0; l < a.length; l++) (i = a[l]).$config.group && c[i.$config.group] && s(i);
            return n
        }, e.prototype._syncCellSizes = function (t, e) {
            if (t) {
                var i = {};
                return this._eachChild(function (t) {
                    t.$config.group && "scrollbar" != t.$name && "resizer" != t.$name && (i[t.$config.group] || (i[t.$config.group] = []), i[t.$config.group].push(t))
                }), i[t] && this._syncGroupSize(i[t], e), i[t]
            }
        }, e.prototype._syncGroupSize = function (t, e) {
            if (t.length) for (var i = t[0].$parent._xLayout ? "width" : "height", n = t[0].$parent.getNextSibling(t[0].$id) ? 1 : -1, r = 0; r < t.length; r++) {
                var a = t[r].getSize(),
                    s = n > 0 ? t[r].$parent.getNextSibling(t[r].$id) : t[r].$parent.getPrevSibling(t[r].$id);
                "resizer" == s.$name && (s = n > 0 ? s.$parent.getNextSibling(s.$id) : s.$parent.getPrevSibling(s.$id));
                var o = s.getSize();
                if (s[i]) {
                    var l = a.gravity + o.gravity, c = a[i] + o[i], d = l / c;
                    t[r].$config.gravity = d * e, s.$config[i] = c - e, s.$config.gravity = l - d * e
                } else t[r].$config[i] = e;
                var h = this.$gantt.$ui.getView("grid");
                h && t[r].$content === h && !h.$config.scrollable && (this.$gantt.config.grid_width = e)
            }
        }, e.prototype.resize = function (e) {
            var i = !1;
            if (this.$root && !this._resizeInProgress && (this.callEvent("onBeforeResize", []), i = !0, this._resizeInProgress = !0), t.prototype.resize.call(this, !0), t.prototype.resize.call(this, !1), i) {
                var n = [];
                n = (n = (n = n.concat(this.getCellsByType("viewCell"))).concat(this.getCellsByType("viewLayout"))).concat(this.getCellsByType("hostCell"));
                for (var r = this.getCellsByType("scroller"), a = 0; a < n.length; a++) n[a].$config.hidden || n[a].setContentSize();
                var s = this._getAutosizeMode(this.$config.autosize), o = this._resizeScrollbars(s, r);
                if (this.$config.autosize && (this.autosize(this.$config.autosize), o = !0), o) {
                    this.resize();
                    for (a = 0; a < n.length; a++) n[a].$config.hidden || n[a].setContentSize()
                }
                this.callEvent("onResize", [])
            }
            i && (this._resizeInProgress = !1)
        }, e.prototype._eachChild = function (t, e) {
            if (t(e = e || this), e.$cells) for (var i = 0; i < e.$cells.length; i++) this._eachChild(t, e.$cells[i])
        }, e.prototype.isChild = function (t) {
            var e = !1;
            return this._eachChild(function (i) {
                i !== t && i.$content !== t || (e = !0)
            }), e
        }, e.prototype.getCellsByType = function (t) {
            var i = [];
            if (t === this.$name && i.push(this), this.$content && this.$content.$name == t && i.push(this.$content), this.$cells) for (var n = 0; n < this.$cells.length; n++) {
                var r = e.prototype.getCellsByType.call(this.$cells[n], t);
                r.length && i.push.apply(i, r)
            }
            return i
        }, e.prototype.getNextSibling = function (t) {
            var e = this.cellIndex(t);
            return e >= 0 && this.$cells[e + 1] ? this.$cells[e + 1] : null
        }, e.prototype.getPrevSibling = function (t) {
            var e = this.cellIndex(t);
            return e >= 0 && this.$cells[e - 1] ? this.$cells[e - 1] : null
        }, e.prototype.cell = function (t) {
            for (var e = 0; e < this.$cells.length; e++) {
                var i = this.$cells[e];
                if (i.$id === t) return i;
                var n = i.cell(t);
                if (n) return n
            }
        }, e.prototype.cellIndex = function (t) {
            for (var e = 0; e < this.$cells.length; e++) if (this.$cells[e].$id === t) return e;
            return -1
        }, e.prototype.moveView = function (t, e) {
            if (this.$cells[e] !== t) return window.alert("Not implemented");
            e += this.$config.header ? 1 : 0;
            var i = this.$view;
            e >= i.childNodes.length ? i.appendChild(t.$view) : i.insertBefore(t.$view, i.childNodes[e])
        }, e.prototype._parseConfig = function (t) {
            this.$cells = [], this._xLayout = !t.rows;
            for (var e = t.rows || t.cols || t.views, i = 0; i < e.length; i++) {
                var n = e[i];
                n.mode = this._xLayout ? "x" : "y";
                var r = this.$factory.initUI(n, this);
                r ? (r.$parent = this, this.$cells.push(r)) : (e.splice(i, 1), i--)
            }
        }, e.prototype.getCells = function () {
            return this.$cells
        }, e.prototype.render = function () {
            var t = r.insertNode(this.$container, this.$toHTML());
            this.$fill(t, null), this.callEvent("onReady", []), this.resize(), this.render = this.resize
        }, e.prototype.$fill = function (t, e) {
            this.$view = t, this.$parent = e;
            for (var i = r.getChildNodes(t, "gantt_layout_cell"), n = i.length - 1; n >= 0; n--) {
                var a = this.$cells[n];
                a.$fill(i[n], this), a.$config.hidden && a.$view.parentNode.removeChild(a.$view)
            }
        }, e.prototype.$toHTML = function () {
            for (var e = this._xLayout ? "x" : "y", i = [], n = 0; n < this.$cells.length; n++) i.push(this.$cells[n].$toHTML());
            return t.prototype.$toHTML.call(this, i.join(""), (this.$root ? "gantt_layout_root " : "") + "gantt_layout gantt_layout_" + e)
        }, e.prototype.getContentSize = function (t) {
            for (var e, i, n, r = 0, a = 0, s = 0; s < this.$cells.length; s++) (i = this.$cells[s]).$config.hidden || (e = i.getContentSize(t), "scrollbar" === i.$config.view && t[i.$config.scroll] && (e.height = 0, e.width = 0), i.$config.resizer && (this._xLayout ? e.height = 0 : e.width = 0), n = i._getBorderSizes(), this._xLayout ? (r += e.width + n.horizontal, a = Math.max(a, e.height + n.vertical)) : (r = Math.max(r, e.width + n.horizontal), a += e.height + n.vertical));
            return r += (n = this._getBorderSizes()).horizontal, a += n.vertical, this.$root && (r += 1, a += 1), {
                width: r,
                height: a
            }
        }, e.prototype._cleanElSize = function (t) {
            return 1 * (t || "").toString().replace("px", "") || 0
        }, e.prototype._getBoxStyles = function (t) {
            var e = null,
                i = ["width", "height", "paddingTop", "paddingBottom", "paddingLeft", "paddingRight", "borderLeftWidth", "borderRightWidth", "borderTopWidth", "borderBottomWidth"],
                n = {
                    boxSizing: "border-box" == (e = window.getComputedStyle ? window.getComputedStyle(t, null) : {
                        width: t.clientWidth,
                        height: t.clientHeight
                    }).boxSizing
                };
            e.MozBoxSizing && (n.boxSizing = "border-box" == e.MozBoxSizing);
            for (var r = 0; r < i.length; r++) n[i[r]] = e[i[r]] ? this._cleanElSize(e[i[r]]) : 0;
            var a = {
                horPaddings: n.paddingLeft + n.paddingRight + n.borderLeftWidth + n.borderRightWidth,
                vertPaddings: n.paddingTop + n.paddingBottom + n.borderTopWidth + n.borderBottomWidth,
                borderBox: n.boxSizing,
                innerWidth: n.width,
                innerHeight: n.height,
                outerWidth: n.width,
                outerHeight: n.height
            };
            return a.borderBox ? (a.innerWidth -= a.horPaddings, a.innerHeight -= a.vertPaddings) : (a.outerWidth += a.horPaddings, a.outerHeight += a.vertPaddings), a
        }, e.prototype._getAutosizeMode = function (t) {
            var e = {x: !1, y: !1};
            return "xy" === t ? e.x = e.y = !0 : "y" === t || !0 === t ? e.y = !0 : "x" === t && (e.x = !0), e
        }, e.prototype.autosize = function (t) {
            var e = this._getAutosizeMode(t), i = this._getBoxStyles(this.$container), n = this.getContentSize(t),
                r = this.$container;
            e.x && (i.borderBox && (n.width += i.horPaddings), r.style.width = n.width + "px"), e.y && (i.borderBox && (n.height += i.vertPaddings), r.style.height = n.height + "px")
        }, e.prototype.getSize = function () {
            this._sizes = [];
            for (var e = 0, i = 0, n = 1e5, r = 0, a = 1e5, s = 0, o = 0; o < this.$cells.length; o++) {
                var l = this._sizes[o] = this.$cells[o].getSize();
                this.$cells[o].$config.hidden || (this._xLayout ? (!l.width && l.minWidth ? e += l.minWidth : e += l.width, n += l.maxWidth, i += l.minWidth, r = Math.max(r, l.height), a = Math.min(a, l.maxHeight), s = Math.max(s, l.minHeight)) : (!l.height && l.minHeight ? r += l.minHeight : r += l.height, a += l.maxHeight, s += l.minHeight, e = Math.max(e, l.width), n = Math.min(n, l.maxWidth), i = Math.max(i, l.minWidth)))
            }
            var c = t.prototype.getSize.call(this);
            return c.maxWidth >= 1e5 && (c.maxWidth = n), c.maxHeight >= 1e5 && (c.maxHeight = a), c.minWidth = c.minWidth != c.minWidth ? 0 : c.minWidth, c.minHeight = c.minHeight != c.minHeight ? 0 : c.minHeight, this._xLayout ? (c.minWidth += this.$config.margin * this.$cells.length || 0, c.minWidth += 2 * this.$config.padding || 0, c.minHeight += 2 * this.$config.padding || 0) : (c.minHeight += this.$config.margin * this.$cells.length || 0, c.minHeight += 2 * this.$config.padding || 0), c
        }, e.prototype._calcFreeSpace = function (t, e, i) {
            var n = i ? e.minWidth : e.minHeight, r = e.maxWidth, a = t;
            return a ? (a > r && (a = r), a < n && (a = n), this._free -= a) : ((a = Math.floor(this._free / this._gravity * e.gravity)) > r && (a = r, this._free -= a, this._gravity -= e.gravity), a < n && (a = n, this._free -= a, this._gravity -= e.gravity)), a
        }, e.prototype._calcSize = function (t, e, i) {
            var n = t, r = i ? e.minWidth : e.minHeight, a = i ? e.maxWidth : e.maxHeight;
            return n || (n = Math.floor(this._free / this._gravity * e.gravity)), n > a && (n = a), n < r && (n = r), n
        }, e.prototype._configureBorders = function () {
            this.$root && this._setBorders([this._borders.left, this._borders.top, this._borders.right, this._borders.bottom], this);
            for (var t = this._xLayout ? this._borders.right : this._borders.bottom, e = this.$cells, i = e.length - 1, n = i; n >= 0; n--) if (!e[n].$config.hidden) {
                i = n;
                break
            }
            for (n = 0; n < e.length; n++) if (!e[n].$config.hidden) {
                var r = n >= i, a = "";
                !r && e[n + 1] && "scrollbar" == e[n + 1].$config.view && (this._xLayout ? r = !0 : a = "gantt_layout_cell_border_transparent"), this._setBorders(r ? [] : [t, a], e[n])
            }
        }, e.prototype._updateCellVisibility = function () {
            for (var t, e = this._visibleCells || {}, i = !this._visibleCells, n = {}, r = 0; r < this._sizes.length; r++) t = this.$cells[r], !i && t.$config.hidden && e[t.$id] ? t._hide(!0) : t.$config.hidden || e[t.$id] || t._hide(!1), t.$config.hidden || (n[t.$id] = !0);
            this._visibleCells = n
        }, e.prototype.setSize = function (e, i) {
            this._configureBorders(), t.prototype.setSize.call(this, e, i), i = this.$lastSize.contentY, e = this.$lastSize.contentX;
            var n, r, a = this.$config.padding || 0;
            this.$view.style.padding = a + "px", this._gravity = 0, this._free = this._xLayout ? e : i, this._free -= 2 * a, this._updateCellVisibility();
            for (var s = 0; s < this._sizes.length; s++) if (!(n = this.$cells[s]).$config.hidden) {
                var o = this.$config.margin || 0;
                "resizer" != n.$name || o || (o = -1);
                var l = n.$view, c = this._xLayout ? "marginRight" : "marginBottom";
                s !== this.$cells.length - 1 && (l.style[c] = o + "px", this._free -= o), r = this._sizes[s], this._xLayout ? r.width || (this._gravity += r.gravity) : r.height || (this._gravity += r.gravity)
            }
            for (s = 0; s < this._sizes.length; s++) if (!(n = this.$cells[s]).$config.hidden) {
                var d = (r = this._sizes[s]).width, h = r.height;
                this._xLayout ? this._calcFreeSpace(d, r, !0) : this._calcFreeSpace(h, r, !1)
            }
            for (s = 0; s < this.$cells.length; s++) if (!(n = this.$cells[s]).$config.hidden) {
                r = this._sizes[s];
                var u = void 0, g = void 0;
                this._xLayout ? (u = this._calcSize(r.width, r, !0), g = i - 2 * a) : (u = e - 2 * a, g = this._calcSize(r.height, r, !1)), n.setSize(u, g)
            }
        }, e
    }(i(7));
    t.exports = a
}, function (t, e) {
    t.exports = function (t, e) {
        if (!e) return !0;
        if (t._on_timeout) return !1;
        var i = Math.ceil(1e3 / e);
        return i < 2 || (setTimeout(function () {
            delete t._on_timeout
        }, i), t._on_timeout = !0, !0)
    }
}, function (t, e) {
    t.exports = function (t) {
        t.destructor = function () {
            for (var e in t.callEvent("onDestroy", []), this.clearAll(), this.detachAllEvents(), this.$root && delete this.$root.gantt, this._eventRemoveAll(), this.$layout && this.$layout.destructor(), this.resetLightbox(), this._dp && this._dp.destructor && this._dp.destructor(), this.$services.destructor(), this) 0 === e.indexOf("$") && delete this[e]
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        return function (e, i) {
            e || t.config.show_errors && !1 !== t.callEvent("onError", [i]) && t.message({
                type: "error",
                text: i,
                expire: -1
            })
        }
    }
}, function (t, e, i) {
    var n = i(1), r = i(3);
    t.exports = function (t) {
        var e = i(18);
        t.assert = i(28)(t), t.init = function (t, e, i) {
            e && i && (this.config.start_date = this._min_date = new Date(e), this.config.end_date = this._max_date = new Date(i)), this.date.init(), this.config.scroll_size || (this.config.scroll_size = n.getScrollSize() || 1), this.init = function (t) {
                this.$container && this.$container.parentNode && (this.$container.parentNode.removeChild(this.$container), this.$container = null), this.$layout && this.$layout.clear(), this._reinit(t)
            }, this._reinit(t)
        }, t._reinit = function (i) {
            this.callEvent("onBeforeGanttReady", []), this.resetLightbox(), this._update_flags(), this.$services.getService("templateLoader").initTemplates(this), this._clearTaskLayers(), this._clearLinkLayers(), this.$layout && (this.$layout.destructor(), this.$ui.reset()), this.$root = n.toNode(i), this.$root && (this.$root.innerHTML = ""), this.$root.gantt = this, e(this), this.config.layout.id = "main", this.$layout = this.$ui.createView("layout", i, this.config.layout), this.$layout.attachEvent("onBeforeResize", function () {
                for (var e = t.$services.getService("datastores"), i = 0; i < e.length; i++) t.getDatastore(e[i]).filter()
            }), this.$layout.attachEvent("onResize", function () {
                t.refreshData()
            }), this.callEvent("onGanttLayoutReady", []), this.$layout.render(), t.$container = this.$layout.$container.firstChild, function (t) {
                var e;
                "static" == window.getComputedStyle(t.$root).getPropertyValue("position") && (t.$root.style.position = "relative");
                var i = document.createElement("iframe");
                i.className = "gantt_container_resize_watcher", i.tabIndex = -1, t.$root.appendChild(i), i.contentWindow.addEventListener("resize", function () {
                    clearTimeout(e), e = setTimeout(function () {
                        t.render()
                    }, 300)
                })
            }(t), this.callEvent("onTemplatesReady", []), this.$mouseEvents.reset(this.$root), this.callEvent("onGanttReady", []), this.render()
        }, t.$click = {
            buttons: {
                edit: function (e) {
                    t.showLightbox(e)
                }, delete: function (e) {
                    var i = t.locale.labels.confirm_deleting, n = t.locale.labels.confirm_deleting_title;
                    t._dhtmlx_confirm(i, n, function () {
                        t.isTaskExists(e) ? (t.getTask(e).$new ? (t.silent(function () {
                            t.deleteTask(e, !0)
                        }), t.refreshData()) : t.deleteTask(e), t.hideLightbox()) : t.hideLightbox()
                    })
                }
            }
        }, t.render = function () {
            this.callEvent("onBeforeGanttRender", []), !this.config.sort && this._sort && (this._sort = void 0);
            var i = this.getScrollState(), n = i ? i.x : 0;
            this._getHorizontalScrollbar() && (n = this._getHorizontalScrollbar().$config.codeScrollLeft || n || 0);
            var r = null;
            if (n && (r = t.dateFromPos(n + this.config.task_scroll_offset)), e(this), this.$layout.$config.autosize = this.config.autosize, this.$layout.resize(), this.config.preserve_scroll && i && n) {
                var a = t.getScrollState();
                +r == +t.dateFromPos(a.x) && a.y == i.y || (r && this.showDate(r), i.y && t.scrollTo(void 0, i.y))
            }
            this.callEvent("onGanttRender", [])
        }, t.setSizes = t.render, t.locate = function (t) {
            var e = n.getTargetNode(t);
            if ((n.getClassName(e) || "").indexOf("gantt_task_cell") >= 0) return null;
            var i = arguments[1] || this.config.task_attribute, r = n.locateAttribute(e, i);
            return r ? r.getAttribute(i) : null
        }, t._locate_css = function (t, e, i) {
            return n.locateClassName(t, e, i)
        }, t._locateHTML = function (t, e) {
            return n.locateAttribute(t, e || this.config.task_attribute)
        }, t.getTaskRowNode = function (t) {
            for (var e = this.$grid_data.childNodes, i = this.config.task_attribute, n = 0; n < e.length; n++) {
                if (e[n].getAttribute) if (e[n].getAttribute(i) == t) return e[n]
            }
            return null
        }, t.changeLightboxType = function (e) {
            if (this.getLightboxType() == e) return !0;
            t._silent_redraw_lightbox(e)
        }, t._get_link_type = function (e, i) {
            var n = null;
            return e && i ? n = t.config.links.start_to_start : !e && i ? n = t.config.links.finish_to_start : e || i ? e && !i && (n = t.config.links.start_to_finish) : n = t.config.links.finish_to_finish, n
        }, t.isLinkAllowed = function (t, e, i, n) {
            var r = null;
            if (!(r = "object" == typeof t ? t : {source: t, target: e, type: this._get_link_type(i, n)})) return !1;
            if (!(r.source && r.target && r.type)) return !1;
            if (r.source == r.target) return !1;
            var a = !0;
            return this.checkEvent("onLinkValidation") && (a = this.callEvent("onLinkValidation", [r])), a
        }, t._correct_dst_change = function (e, i, n, a) {
            var s = r.getSecondsInUnit(a) * n;
            if (s > 3600 && s < 86400) {
                var o = e.getTimezoneOffset() - i;
                o && (e = t.date.add(e, o, "minute"))
            }
            return e
        }, t.isSplitTask = function (e) {
            return t.assert(e && e instanceof Object, "Invalid argument <b>task</b>=" + e + " of gantt.isSplitTask. Task object was expected"), this.$data.tasksStore._isSplitItem(e)
        }, t._is_icon_open_click = function (t) {
            if (!t) return !1;
            var e = t.target || t.srcElement;
            if (!e || !e.className) return !1;
            var i = n.getClassName(e);
            return -1 !== i.indexOf("gantt_tree_icon") && (-1 !== i.indexOf("gantt_close") || -1 !== i.indexOf("gantt_open"))
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        function e() {
            var e;
            return t.$ui.getView("timeline") && (e = t.$ui.getView("timeline")._tasks_dnd), e
        }

        t.config.touch_drag = 500, t.config.touch = !0, t.config.touch_feedback = !0, t.config.touch_feedback_duration = 1, t._prevent_touch_scroll = !1, t._touch_feedback = function () {
            t.config.touch_feedback && navigator.vibrate && navigator.vibrate(t.config.touch_feedback_duration)
        }, t.attachEvent("onGanttReady", t.bind(function () {
            if ("force" != this.config.touch && (this.config.touch = this.config.touch && (-1 != navigator.userAgent.indexOf("Mobile") || -1 != navigator.userAgent.indexOf("iPad") || -1 != navigator.userAgent.indexOf("Android") || -1 != navigator.userAgent.indexOf("Touch"))), this.config.touch) {
                var t = !0;
                try {
                    document.createEvent("TouchEvent")
                } catch (e) {
                    t = !1
                }
                t ? this._touch_events(["touchmove", "touchstart", "touchend"], function (t) {
                    return t.touches && t.touches.length > 1 ? null : t.touches[0] ? {
                        target: t.target,
                        pageX: t.touches[0].pageX,
                        pageY: t.touches[0].pageY,
                        clientX: t.touches[0].clientX,
                        clientY: t.touches[0].clientY
                    } : t
                }, function () {
                    return !1
                }) : window.navigator.pointerEnabled ? this._touch_events(["pointermove", "pointerdown", "pointerup"], function (t) {
                    return "mouse" == t.pointerType ? null : t
                }, function (t) {
                    return !t || "mouse" == t.pointerType
                }) : window.navigator.msPointerEnabled && this._touch_events(["MSPointerMove", "MSPointerDown", "MSPointerUp"], function (t) {
                    return t.pointerType == t.MSPOINTER_TYPE_MOUSE ? null : t
                }, function (t) {
                    return !t || t.pointerType == t.MSPOINTER_TYPE_MOUSE
                })
            }
        }, t));
        var i = [];
        t._touch_events = function (n, r, a) {
            for (var s, o = 0, l = !1, c = !1, d = null, h = null, u = null, g = 0; g < i.length; g++) t.eventRemove(i[g][0], i[g][1], i[g][2]);
            (i = []).push([t.$container, n[0], function (i) {
                var n = e();
                if (!a(i) && l) {
                    h && clearTimeout(h);
                    var u = r(i);
                    if (n && (n.drag.id || n.drag.start_drag)) return n.on_mouse_move(u), i.preventDefault && i.preventDefault(), i.cancelBubble = !0, !1;
                    if (!t._prevent_touch_scroll) {
                        if (u && d) {
                            var g = d.pageX - u.pageX, _ = d.pageY - u.pageY;
                            if (!c && (Math.abs(g) > 5 || Math.abs(_) > 5) && (t._touch_scroll_active = c = !0, o = 0, s = t.getScrollState()), c) {
                                t.scrollTo(s.x + g, s.y + _);
                                var p = t.getScrollState();
                                if (s.x != p.x && _ > 2 * g || s.y != p.y && g > 2 * _) return f(i)
                            }
                        }
                        return f(i)
                    }
                    return !0
                }
            }]), i.push([this.$container, "contextmenu", function (t) {
                if (l) return f(t)
            }]), i.push([this.$container, n[1], function (i) {
                if (!a(i)) if (i.touches && i.touches.length > 1) l = !1; else {
                    d = r(i), t._locate_css(d, "gantt_hor_scroll") || t._locate_css(d, "gantt_ver_scroll") || (l = !0);
                    var n = e();
                    h = setTimeout(function () {
                        var e = t.locate(d);
                        n && e && !t._locate_css(d, "gantt_link_control") && !t._locate_css(d, "gantt_grid_data") && (n.on_mouse_down(d), n.drag && n.drag.start_drag && (!function (e) {
                            var i = t._getTaskLayers(), n = t.getTask(e);
                            if (n && t.isTaskVisible(e)) for (var r = 0; r < i.length; r++) if ((n = i[r].rendered[e]) && n.getAttribute(t.config.task_attribute) && n.getAttribute(t.config.task_attribute) == e) {
                                var a = n.cloneNode(!0);
                                u = n, i[r].rendered[e] = a, n.style.display = "none", a.className += " gantt_drag_move ", n.parentNode.appendChild(a)
                            }
                        }(e), n._start_dnd(d), t._touch_drag = !0, t.refreshTask(e), t._touch_feedback())), h = null
                    }, t.config.touch_drag)
                }
            }]), i.push([this.$container, n[2], function (i) {
                if (!a(i)) {
                    h && clearTimeout(h), t._touch_drag = !1, l = !1;
                    var n = r(i), s = e();
                    if (s && s.on_mouse_up(n), u && (t.refreshTask(t.locate(u)), u.parentNode && (u.parentNode.removeChild(u), t._touch_feedback())), t._touch_scroll_active = l = c = !1, u = null, d && o) {
                        var g = new Date;
                        if (g - o < 500) t.$services.getService("mouseEvents").onDoubleClick(d), f(i); else o = g
                    } else o = new Date
                }
            }]);
            for (g = 0; g < i.length; g++) t.event(i[g][0], i[g][1], i[g][2]);

            function f(t) {
                return t && t.preventDefault && t.preventDefault(), (t || event).cancelBubble = !0, !1
            }
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.contrast_white = {
            config: {
                grid_width: 360,
                row_height: 35,
                scale_height: 35,
                link_line_width: 2,
                link_arrow_size: 6,
                lightbox_additional_height: 75
            }, _second_column_width: 100, _third_column_width: 80
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.contrast_black = {
            config: {
                grid_width: 360,
                row_height: 35,
                scale_height: 35,
                link_line_width: 2,
                link_arrow_size: 6,
                lightbox_additional_height: 75
            }, _second_column_width: 100, _third_column_width: 80
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.material = {
            config: {
                grid_width: 411,
                row_height: 34,
                task_height_offset: 6,
                scale_height: 36,
                link_line_width: 2,
                link_arrow_size: 6,
                lightbox_additional_height: 80
            },
            _second_column_width: 110,
            _third_column_width: 75,
            _redefine_lightbox_buttons: {
                buttons_left: ["dhx_delete_btn"],
                buttons_right: ["dhx_save_btn", "dhx_cancel_btn"]
            }
        }, t.attachEvent("onAfterTaskDrag", function (e) {
            var i = t.getTaskNode(e);
            i && (i.className += " gantt_drag_animation", setTimeout(function () {
                var t = i.className.indexOf(" gantt_drag_animation");
                t > -1 && (i.className = i.className.slice(0, t))
            }, 200))
        })
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.broadway = {
            config: {
                grid_width: 360,
                row_height: 35,
                scale_height: 35,
                link_line_width: 1,
                link_arrow_size: 7,
                lightbox_additional_height: 86
            },
            _second_column_width: 90,
            _third_column_width: 80,
            _lightbox_template: "<div class='gantt_cal_ltitle'><span class='gantt_mark'>&nbsp;</span><span class='gantt_time'></span><span class='gantt_title'></span><div class='gantt_cancel_btn'></div></div><div class='gantt_cal_larea'></div>",
            _config_buttons_left: {},
            _config_buttons_right: {gantt_delete_btn: "icon_delete", gantt_save_btn: "icon_save"}
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.terrace = {
            config: {
                grid_width: 360,
                row_height: 35,
                scale_height: 35,
                link_line_width: 2,
                link_arrow_size: 6,
                lightbox_additional_height: 75
            }, _second_column_width: 90, _third_column_width: 70
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.meadow = {
            config: {
                grid_width: 350,
                row_height: 27,
                scale_height: 30,
                link_line_width: 2,
                link_arrow_size: 6,
                lightbox_additional_height: 72
            }, _second_column_width: 95, _third_column_width: 80
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.skins.skyblue = {
            config: {
                grid_width: 350,
                row_height: 27,
                scale_height: 27,
                link_line_width: 1,
                link_arrow_size: 8,
                lightbox_additional_height: 75
            }, _second_column_width: 95, _third_column_width: 80
        }
    }
}, function (t, e) {
    function i(t, e) {
        var i = e.skin;
        if (!i || t) for (var n = document.getElementsByTagName("link"), r = 0; r < n.length; r++) {
            var a = n[r].href.match("dhtmlxgantt_([a-z_]+).css");
            if (a && (e.skins[a[1]] || !i)) {
                i = a[1];
                break
            }
        }
        e.skin = i || "terrace";
        var s = e.skins[e.skin] || e.skins.terrace;
        !function (t, e, i) {
            for (var n in e) (void 0 === t[n] || i) && (t[n] = e[n])
        }(e.config, s.config, t);
        var o = e.getGridColumns();
        o[1] && !e.defined(o[1].width) && (o[1].width = s._second_column_width), o[2] && !e.defined(o[2].width) && (o[2].width = s._third_column_width);
        for (r = 0; r < o.length; r++) {
            var l = o[r];
            "add" == l.name && (l.width || (l.width = 44), e.defined(l.min_width) && e.defined(l.max_width) || (l.min_width = l.min_width || l.width, l.max_width = l.max_width || l.width), l.min_width && (l.min_width = +l.min_width), l.max_width && (l.max_width = +l.max_width), l.width && (l.width = +l.width, l.width = l.min_width && l.min_width > l.width ? l.min_width : l.width, l.width = l.max_width && l.max_width < l.width ? l.max_width : l.width))
        }
        s.config.task_height && (e.config.task_height = s.config.task_height || "full"), s._lightbox_template && (e._lightbox_template = s._lightbox_template), s._redefine_lightbox_buttons && (e.config.buttons_right = s._redefine_lightbox_buttons.buttons_right, e.config.buttons_left = s._redefine_lightbox_buttons.buttons_left), e.resetLightbox()
    }

    t.exports = function (t) {
        t.resetSkin || (t.resetSkin = function () {
            this.skin = "", i(!0, this)
        }, t.skins = {}, t.attachEvent("onGanttLayoutReady", function () {
            i(!1, this)
        }))
    }
}, function (t, e) {
    t.exports = function (t) {
        function e() {
            return t._cached_functions.update_if_changed(t), t._cached_functions.active || t._cached_functions.activate(), !0
        }

        t._cached_functions = {
            cache: {}, mode: !1, critical_path_mode: !1, wrap_methods: function (t, e) {
                if (e._prefetch_originals) for (var i in e._prefetch_originals) e[i] = e._prefetch_originals[i];
                e._prefetch_originals = {};
                for (i = 0; i < t.length; i++) this.prefetch(t[i], e)
            }, prefetch: function (t, e) {
                var i = e[t];
                if (i) {
                    var n = this;
                    e._prefetch_originals[t] = i, e[t] = function () {
                        for (var e = new Array(arguments.length), r = 0, a = arguments.length; r < a; r++) e[r] = arguments[r];
                        if (n.active) {
                            var s = n.get_arguments_hash(Array.prototype.slice.call(e));
                            n.cache[t] || (n.cache[t] = {});
                            var o = n.cache[t];
                            if (n.has_cached_value(o, s)) return n.get_cached_value(o, s);
                            var l = i.apply(this, e);
                            return n.cache_value(o, s, l), l
                        }
                        return i.apply(this, e)
                    }
                }
                return i
            }, cache_value: function (t, e, i) {
                this.is_date(i) && (i = new Date(i)), t[e] = i
            }, has_cached_value: function (t, e) {
                return t.hasOwnProperty(e)
            }, get_cached_value: function (t, e) {
                var i = t[e];
                return this.is_date(i) && (i = new Date(i)), i
            }, is_date: function (t) {
                return t && t.getUTCDate
            }, get_arguments_hash: function (t) {
                for (var e = [], i = 0; i < t.length; i++) e.push(this.stringify_argument(t[i]));
                return "(" + e.join(";") + ")"
            }, stringify_argument: function (t) {
                return (t.id ? t.id : this.is_date(t) ? t.valueOf() : t) + ""
            }, activate: function () {
                this.clear(), this.active = !0
            }, deactivate: function () {
                this.clear(), this.active = !1
            }, clear: function () {
                this.cache = {}
            }, setup: function (t) {
                var e = [], i = ["_isProjectEnd", "_getProjectEnd", "_getSlack"];
                "auto" == this.mode ? t.config.highlight_critical_path && (e = i) : !0 === this.mode && (e = i), this.wrap_methods(e, t)
            }, update_if_changed: function (t) {
                (this.critical_path_mode != t.config.highlight_critical_path || this.mode !== t.config.optimize_render) && (this.critical_path_mode = t.config.highlight_critical_path, this.mode = t.config.optimize_render, this.setup(t))
            }
        }, t.attachEvent("onBeforeGanttRender", e), t.attachEvent("onBeforeDataRender", e), t.attachEvent("onBeforeSmartRender", function () {
            e()
        }), t.attachEvent("onBeforeParse", e), t.attachEvent("onDataRender", function () {
            t._cached_functions.deactivate()
        });
        var i = null;
        t.attachEvent("onSmartRender", function () {
            i && clearTimeout(i), i = setTimeout(function () {
                t._cached_functions.deactivate()
            }, 1e3)
        }), t.attachEvent("onBeforeGanttReady", function () {
            return t._cached_functions.update_if_changed(t), !0
        })
    }
}, function (t, e) {
    t.exports = function (t) {
        t.getTaskType = function (t) {
            return "task"
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t._extend_to_optional = function (e) {
            var i = e, n = {
                render: i.render, focus: i.focus, set_value: function (e, r, a, s) {
                    var o = t._resolve_default_mapping(s);
                    if (!a[o.start_date] || "start_date" == o.start_date && this._isAllowedUnscheduledTask(a)) {
                        n.disable(e, s);
                        var l = {};
                        for (var c in o) l[o[c]] = a[c];
                        return i.set_value.call(t, e, r, l, s)
                    }
                    return n.enable(e, s), i.set_value.call(t, e, r, a, s)
                }, get_value: function (e, n, r) {
                    return r.disabled ? {start_date: null} : i.get_value.call(t, e, n, r)
                }, update_block: function (e, i) {
                    if (t.callEvent("onSectionToggle", [t._lightbox_id, i]), e.style.display = i.disabled ? "none" : "block", i.button) {
                        var n = e.previousSibling.querySelector(".gantt_custom_button_label"), r = t.locale.labels,
                            a = i.disabled ? r[i.name + "_enable_button"] : r[i.name + "_disable_button"];
                        n.innerHTML = a
                    }
                    t.resizeLightbox()
                }, disable: function (t, e) {
                    e.disabled = !0, n.update_block(t, e)
                }, enable: function (t, e) {
                    e.disabled = !1, n.update_block(t, e)
                }, button_click: function (e, i, r, a) {
                    if (!1 !== t.callEvent("onSectionButton", [t._lightbox_id, r])) {
                        var s = t._get_typed_lightbox_config()[e];
                        s.disabled ? n.enable(a, s) : n.disable(a, s)
                    }
                }
            };
            return n
        }, t.form_blocks.duration_optional = t._extend_to_optional(t.form_blocks.duration), t.form_blocks.time_optional = t._extend_to_optional(t.form_blocks.time)
    }
}, function (t, e, i) {
    var n = i(2);
    t.exports = function (t) {
        var e = i(8)(t);

        function r() {
            return e.apply(this, arguments) || this
        }

        function a(e, i) {
            var n = [], r = [];
            i && (n = t.getTaskByTime(), e.allow_root && n.unshift({
                id: t.config.root_id,
                text: e.root_label || ""
            }), n = function (e, i, n) {
                var r = i.filter || function () {
                    return !0
                };
                e = e.slice(0);
                for (var a = 0; a < e.length; a++) {
                    var s = e[a];
                    (s.id == n || t.isChildOf(s.id, n) || !1 === r(s.id, s)) && (e.splice(a, 1), a--)
                }
                return e
            }(n, e, i), e.sort && n.sort(e.sort));
            for (var a = e.template || t.templates.task_text, s = 0; s < n.length; s++) {
                var o = a.apply(t, [n[s].start_date, n[s].end_date, n[s]]);
                void 0 === o && (o = ""), r.push({key: n[s].id, label: o})
            }
            return e.options = r, e.map_to = e.map_to || "parent", t.form_blocks.select.render.apply(this, arguments)
        }

        return n(r, e), r.prototype.render = function (t) {
            return a(t, !1)
        }, r.prototype.set_value = function (e, i, n, r) {
            var s = document.createElement("div");
            s.innerHTML = a(r, n.id);
            var o = s.removeChild(s.firstChild);
            return e.onselect = null, e.parentNode.replaceChild(o, e), t.form_blocks.select.set_value.apply(t, [o, i, n, r])
        }, r
    }
}, function (t, e, i) {
    var n = i(2);
    t.exports = function (t) {
        var e = i(5)(t);

        function r() {
            return e.apply(this, arguments) || this
        }

        function a(e, i) {
            var n = e.getElementsByTagName("select"), r = i._time_format_order, a = 0, s = 0;
            if (t.defined(r[3])) {
                var o = n[r[3]], l = parseInt(o.value, 10);
                isNaN(l) && o.hasAttribute("data-value") && (l = parseInt(o.getAttribute("data-value"), 10)), a = Math.floor(l / 60), s = l % 60
            }
            return new Date(n[r[2]].value, n[r[1]].value, n[r[0]].value, a, s)
        }

        function s(t) {
            var e = t.getElementsByTagName("input")[1];
            return (e = parseInt(e.value, 10)) && !window.isNaN(e) || (e = 1), e < 0 && (e *= -1), e
        }

        return n(r, e), r.prototype.render = function (e) {
            var i = "<div class='gantt_time_selects'>" + t.form_blocks.getTimePicker.call(this, e) + "</div>",
                n = t.locale.labels[t.config.duration_unit + "s"], r = e.single_date ? " style='display:none'" : "",
                a = e.readonly ? " disabled='disabled'" : "",
                s = "<div class='gantt_duration' " + r + "><input type='button' class='gantt_duration_dec' value='−'" + a + "><input type='text' value='5' class='gantt_duration_value'" + a + " " + t._waiAria.lightboxDurationInputAttrString(e) + "><input type='button' class='gantt_duration_inc' value='+'" + a + "> " + n + " <span></span></div>";
            return "<div style='height:" + (e.height || 30) + "px;padding-top:0px;font-size:inherit;' class='gantt_section_time'>" + i + " " + s + "</div>"
        }, r.prototype.set_value = function (e, i, n, r) {
            var o, l, c, d, h = r, u = e.getElementsByTagName("select"), g = e.getElementsByTagName("input"), f = g[1],
                _ = [g[0], g[2]], p = e.getElementsByTagName("span")[0], v = r._time_format_order;

            function m() {
                var i = a.call(t, e, r), o = s.call(t, e, r),
                    l = t.calculateEndDate({start_date: i, duration: o, task: n});
                p.innerHTML = t.templates.task_date(l)
            }

            function y(t) {
                var e = f.value;
                e = parseInt(e, 10), window.isNaN(e) && (e = 0), (e += t) < 1 && (e = 1), f.value = e, m()
            }

            _[0].onclick = t.bind(function () {
                y(-1 * t.config.duration_step)
            }, this), _[1].onclick = t.bind(function () {
                y(1 * t.config.duration_step)
            }, this), u[0].onchange = m, u[1].onchange = m, u[2].onchange = m, u[3] && (u[3].onchange = m), f.onkeydown = t.bind(function (e) {
                var i;
                return (i = (e = e || window.event).charCode || e.keyCode || e.which) == t.constants.KEY_CODES.DOWN ? (y(-1 * t.config.duration_step), !1) : i == t.constants.KEY_CODES.UP ? (y(1 * t.config.duration_step), !1) : void window.setTimeout(m, 1)
            }, this), f.onchange = t.bind(m, this), "string" == typeof (o = t._resolve_default_mapping(r)) && (o = {start_date: o}), l = n[o.start_date] || new Date, c = n[o.end_date] || t.calculateEndDate({
                start_date: l,
                duration: 1,
                task: n
            }), d = Math.round(n[o.duration]) || t.calculateDuration({
                start_date: l,
                end_date: c,
                task: n
            }), t.form_blocks._fill_lightbox_select(u, 0, l, v, h), f.value = d, m()
        }, r.prototype.get_value = function (e, i, n) {
            var r = a(e, n), o = s(e), l = t.calculateEndDate({start_date: r, duration: o, task: i});
            return "string" == typeof t._resolve_default_mapping(n) ? r : {start_date: r, end_date: l, duration: o}
        }, r.prototype.focus = function (e) {
            t._focus(e.getElementsByTagName("select")[0])
        }, r
    }
}, function (t, e, i) {
    var n = i(2);
    t.exports = function (t) {
        var e = i(5)(t);

        function r() {
            return e.apply(this, arguments) || this
        }

        return n(r, e), r.prototype.render = function (t) {
            var e = "<div class='gantt_cal_ltext' style='height:" + ((t.height || "23") + "px") + ";'>";
            if (t.options && t.options.length) for (var i = 0; i < t.options.length; i++) e += "<label><input type='radio' value='" + t.options[i].key + "' name='" + t.name + "'>" + t.options[i].label + "</label>";
            return e += "</div>"
        }, r.prototype.set_value = function (t, e, i, n) {
            var r;
            n.options && n.options.length && (r = t.querySelector("input[type=radio][value='" + e + "']") || t.querySelector("input[type=radio][value='" + n.default_value + "']")) && (!t._dhx_onchange && n.onchange && (t.onchange = n.onchange, t._dhx_onchange = !0), r.checked = !0)
        }, r.prototype.get_value = function (t, e) {
            var i = t.querySelector("input[type=radio]:checked");
            return i ? i.value : ""
        }, r.prototype.focus = function (e) {
            t._focus(e.querySelector("input[type=radio]"))
        }, r
    }
}, function (t, e, i) {
    var n = i(3), r = i(2);
    t.exports = function (t) {
        var e = i(5)(t);

        function a() {
            return e.apply(this, arguments) || this
        }

        return r(a, e), a.prototype.render = function (t) {
            var e = "<div class='gantt_cal_ltext' style='height:" + ((t.height || "23") + "px") + ";'>";
            if (t.options && t.options.length) for (var i = 0; i < t.options.length; i++) e += "<label><input type='checkbox' value='" + t.options[i].key + "' name='" + t.name + "'>" + t.options[i].label + "</label>";
            return e += "</div>"
        }, a.prototype.set_value = function (t, e, i, r) {
            var a = Array.prototype.slice.call(t.querySelectorAll("input[type=checkbox]"));
            !t._dhx_onchange && r.onchange && (t.onchange = r.onchange, t._dhx_onchange = !0), n.forEach(a, function (t) {
                t.checked = !!e && e.indexOf(t.value) >= 0
            })
        }, a.prototype.get_value = function (t) {
            return n.arrayMap(Array.prototype.slice.call(t.querySelectorAll("input[type=checkbox]:checked")), function (t) {
                return t.value
            })
        }, a.prototype.focus = function (e) {
            t._focus(e.querySelector("input[type=checkbox]"))
        }, a
    }
}, function (t, e, i) {
    var n = i(3), r = {
        getHtmlSelect: function (t, e, i) {
            var r = "", s = this;
            return t = t || [], n.forEach(t, function (t) {
                var e = [{key: "value", value: t.key}];
                i == t.key && (e[e.length] = {
                    key: "selected",
                    value: "selected"
                }), t.attributes && (e = e.concat(t.attributes)), r += s.getHtmlOption({innerHTML: t.label}, e)
            }), a("select", {innerHTML: r}, e)
        }, getHtmlOption: function (t, e) {
            return a("option", t, e)
        }, getHtmlButton: function (t, e) {
            return a("button", t, e)
        }, getHtmlDiv: function (t, e) {
            return a("div", t, e)
        }, getHtmlLabel: function (t, e) {
            return a("label", t, e)
        }, getHtmlInput: function (t) {
            return "<input" + s(t || []) + ">"
        }
    };

    function a(t, e, i) {
        return e = e || [], "<" + t + s(i || []) + ">" + (e.innerHTML || "") + "</" + t + ">"
    }

    function s(t) {
        var e = "";
        return n.forEach(t, function (t) {
            e += " " + t.key + "='" + t.value + "'"
        }), e
    }

    t.exports = r
}, function (t, e, i) {
    var n = i(2);
    t.exports = function (t) {
        var e = i(5)(t);

        function r() {
            return e.apply(this, arguments) || this
        }

        return n(r, e), r.prototype.render = function (e) {
            var i = t.form_blocks.getTimePicker.call(this, e),
                n = "<div style='height:" + (e.height || 30) + "px;padding-top:0px;font-size:inherit;text-align:center;' class='gantt_section_time'>";
            return n += i, e.single_date ? (i = t.form_blocks.getTimePicker.call(this, e, !0), n += "<span></span>") : n += "<span style='font-weight:normal; font-size:10pt;'> &nbsp;&ndash;&nbsp; </span>", n += i, n += "</div>"
        }, r.prototype.set_value = function (e, i, n, r) {
            var a = r, s = e.getElementsByTagName("select"), o = r._time_format_order;
            if (a.auto_end_date) for (var l = function () {
                h = new Date(s[o[2]].value, s[o[1]].value, s[o[0]].value, 0, 0), u = t.calculateEndDate({
                    start_date: h,
                    duration: 1,
                    task: n
                }), t.form_blocks._fill_lightbox_select(s, o.size, u, o, a)
            }, c = 0; c < 4; c++) s[c].onchange = l;
            var d = t._resolve_default_mapping(r);
            "string" == typeof d && (d = {start_date: d});
            var h = n[d.start_date] || new Date,
                u = n[d.end_date] || t.calculateEndDate({start_date: h, duration: 1, task: n});
            t.form_blocks._fill_lightbox_select(s, 0, h, o, a), t.form_blocks._fill_lightbox_select(s, o.size, u, o, a)
        }, r.prototype.get_value = function (e, i, n) {
            var r, a = e.getElementsByTagName("select"), s = n._time_format_order, o = t.defined(s[3]);

            function l(t, e, i, n) {
                var r, a = 0, s = 0;
                return n = n || 0, i && (r = parseInt(t[e[3] + n].value, 10), a = Math.floor(r / 60), s = r % 60), new Date(t[e[2] + n].value, t[e[1] + n].value, t[e[0] + n].value, a, s)
            }

            return r = l(a, s, o), "string" == typeof t._resolve_default_mapping(n) ? r : {
                start_date: r,
                end_date: function (e, i, n, r) {
                    var a = l(e, i, n, i.size);
                    return a <= r ? t.date.add(r, t._get_timepicker_step(), "minute") : a
                }(a, s, o, r)
            }
        }, r.prototype.focus = function (e) {
            t._focus(e.getElementsByTagName("select")[0])
        }, r
    }
}, function (t, e, i) {
    var n = i(2);
    t.exports = function (t) {
        var e = i(5)(t);

        function r() {
            return e.apply(this, arguments) || this
        }

        return n(r, e), r.prototype.render = function (t) {
            return "<div class='gantt_cal_ltext' style='height:" + ((t.height || "130") + "px") + ";'><textarea></textarea></div>"
        }, r.prototype.set_value = function (e, i) {
            t.form_blocks.textarea._get_input(e).value = i || ""
        }, r.prototype.get_value = function (e) {
            return t.form_blocks.textarea._get_input(e).value
        }, r.prototype.focus = function (e) {
            var i = t.form_blocks.textarea._get_input(e);
            t._focus(i, !0)
        }, r.prototype._get_input = function (t) {
            return t.querySelector("textarea")
        }, r
    }
}, function (t, e, i) {
    var n = i(2);
    t.exports = function (t) {
        var e = i(5)(t);

        function r() {
            return e.apply(this, arguments) || this
        }

        return n(r, e), r.prototype.render = function (t) {
            return "<div class='gantt_cal_ltext gantt_cal_template' style='height:" + ((t.height || "30") + "px") + ";'></div>"
        }, r.prototype.set_value = function (t, e) {
            t.innerHTML = e || ""
        }, r.prototype.get_value = function (t) {
            return t.innerHTML || ""
        }, r.prototype.focus = function () {
        }, r
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(1), n = i(3), r = i(49)(t), a = i(48)(t), s = i(47)(t), o = i(8)(t), l = i(45)(t), c = i(44)(t),
            d = i(43)(t), h = i(42)(t), u = i(8)(t);

        function g(e, i) {
            var n, r, a = "";
            for (r = 0; r < e.length; r++) n = t.config._migrate_buttons[e[r]] ? t.config._migrate_buttons[e[r]] : e[r], a += "<div " + t._waiAria.lightboxButtonAttrString(n) + " class='gantt_btn_set gantt_left_btn_set " + n + "_set'" + (i ? " style='float:right;'" : "") + "><div dhx_button='1' data-dhx-button='1' class='" + n + "'></div><div>" + t.locale.labels[n] + "</div></div>";
            return a
        }

        function f(e, i, n) {
            var r, a, s, o, l, c, d = "";
            switch (n.timeFormat[i]) {
                case"%Y":
                    for (e._time_format_order[2] = i, e._time_format_order.size++, e.year_range && (isNaN(e.year_range) ? e.year_range.push && (s = e.year_range[0], o = e.year_range[1]) : r = e.year_range), r = r || 10, a = a || Math.floor(r / 2), s = s || n.date.getFullYear() - a, o = o || s + r, l = s; l < o; l++) d += "<option value='" + l + "'>" + l + "</option>";
                    break;
                case"%m":
                    for (e._time_format_order[1] = i, e._time_format_order.size++, l = 0; l < 12; l++) d += "<option value='" + l + "'>" + t.locale.date.month_full[l] + "</option>";
                    break;
                case"%d":
                    for (e._time_format_order[0] = i, e._time_format_order.size++, l = 1; l < 32; l++) d += "<option value='" + l + "'>" + l + "</option>";
                    break;
                case"%H:%i":
                    for (e._time_format_order[3] = i, e._time_format_order.size++, l = n.first, c = n.date.getDate(), e._time_values = []; l < n.last;) d += "<option value='" + l + "'>" + t.templates.time_picker(n.date) + "</option>", e._time_values.push(l), n.date.setTime(n.date.valueOf() + 60 * t._get_timepicker_step() * 1e3), l = 24 * (n.date.getDate() != c ? 1 : 0) * 60 + 60 * n.date.getHours() + n.date.getMinutes()
            }
            return d
        }

        t._lightbox_methods = {}, t._lightbox_template = "<div class='gantt_cal_ltitle'><span class='gantt_mark'>&nbsp;</span><span class='gantt_time'></span><span class='gantt_title'></span></div><div class='gantt_cal_larea'></div>", t.$services.getService("state").registerProvider("lightbox", function () {
            return {lightbox: t._lightbox_id}
        }), t.showLightbox = function (e) {
            if (e && !t.isReadonly(this.getTask(e)) && this.callEvent("onBeforeLightbox", [e])) {
                var i = this.getTask(e), n = this.getLightbox(this.getTaskType(i.type));
                this._center_lightbox(n), this.showCover(), this._fill_lightbox(e, n), this._waiAria.lightboxVisibleAttr(n), this.callEvent("onLightbox", [e])
            }
        }, t._get_timepicker_step = function () {
            if (this.config.round_dnd_dates) {
                var e;
                if (function (t) {
                    var e = t.$ui.getView("timeline");
                    return !(!e || !e.isVisible())
                }(this)) {
                    var i = t.getScale();
                    e = n.getSecondsInUnit(i.unit) * i.step / 60
                }
                return (!e || e >= 1440) && (e = this.config.time_step), e
            }
            return this.config.time_step
        }, t.getLabel = function (t, e) {
            for (var i = this._get_typed_lightbox_config(), n = 0; n < i.length; n++) if (i[n].map_to == t) for (var r = i[n].options, a = 0; a < r.length; a++) if (r[a].key == e) return r[a].label;
            return ""
        }, t.updateCollection = function (e, i) {
            i = i.slice(0);
            var n = t.serverList(e);
            if (!n) return !1;
            n.splice(0, n.length), n.push.apply(n, i || []), t.resetLightbox()
        }, t.getLightboxType = function () {
            return this.getTaskType(this._lightbox_type)
        }, t.getLightbox = function (e) {
            var i, n, r, a, s = "";
            return void 0 === e && (e = this.getLightboxType()), this._lightbox && this.getLightboxType() == this.getTaskType(e) || (this._lightbox_type = this.getTaskType(e), i = document.createElement("div"), s = "gantt_cal_light", n = this._is_lightbox_timepicker(), (t.config.wide_form || n) && (s += " gantt_cal_light_wide"), n && (t.config.wide_form = !0, s += " gantt_cal_light_full"), i.className = s, i.style.visibility = "hidden", r = this._lightbox_template, r += g(this.config.buttons_left), r += g(this.config.buttons_right, !0), i.innerHTML = r, t._waiAria.lightboxAttr(i), t.config.drag_lightbox && (i.firstChild.onmousedown = t._ready_to_dnd, i.firstChild.onselectstart = function () {
                return !1
            }, i.firstChild.style.cursor = "pointer", t._init_dnd_events()), document.body.insertBefore(i, document.body.firstChild), this._lightbox = i, a = this._get_typed_lightbox_config(e), r = this._render_sections(a), i.querySelector("div.gantt_cal_larea").innerHTML = r, function (e) {
                var i, n, r, a, s, o;
                for (o = 0; o < e.length; o++) i = e[o], r = document.getElementById(i.id), i.id && r && (n = r.querySelector("label"), (a = r.nextSibling) && (s = a.querySelector("input, select, textarea")) && (s.id = s.id || "input_" + t.uid(), i.inputId = s.id, n.setAttribute("for", i.inputId)))
            }(a), this.resizeLightbox(), this._init_lightbox_events(this), i.style.display = "none", i.style.visibility = "visible"), this._lightbox
        }, t._render_sections = function (t) {
            for (var e = "", i = 0; i < t.length; i++) {
                var n = this.form_blocks[t[i].type];
                if (n) {
                    t[i].id = "area_" + this.uid();
                    var r = t[i].hidden ? " style='display:none'" : "", a = "";
                    t[i].button && (a = "<div class='gantt_custom_button' data-index='" + i + "'><div class='gantt_custom_button_" + t[i].button + "'></div><div class='gantt_custom_button_label'>" + this.locale.labels["button_" + t[i].button] + "</div></div>"), this.config.wide_form && (e += "<div class='gantt_wrap_section' " + r + ">"), e += "<div id='" + t[i].id + "' class='gantt_cal_lsection'><label>" + a + this.locale.labels["section_" + t[i].name] + "</label></div>" + n.render.call(this, t[i]), e += "</div>"
                }
            }
            return e
        }, t.resizeLightbox = function () {
            if (this._lightbox) {
                var t = this._lightbox.childNodes[1];
                t.style.height = "0px", t.style.height = t.scrollHeight + "px", this._lightbox.style.height = t.scrollHeight + this.config.lightbox_additional_height + "px", t.style.height = t.scrollHeight + "px"
            }
        }, t._center_lightbox = function (t) {
            if (t) {
                t.style.display = "block";
                var e = window.pageYOffset || document.body.scrollTop || document.documentElement.scrollTop,
                    i = window.pageXOffset || document.body.scrollLeft || document.documentElement.scrollLeft,
                    n = window.innerHeight || document.documentElement.clientHeight;
                t.style.top = e ? Math.round(e + Math.max((n - t.offsetHeight) / 2, 0)) + "px" : Math.round(Math.max((n - t.offsetHeight) / 2, 0) + 9) + "px", document.documentElement.scrollWidth > document.body.offsetWidth ? t.style.left = Math.round(i + (document.body.offsetWidth - t.offsetWidth) / 2) + "px" : t.style.left = Math.round((document.body.offsetWidth - t.offsetWidth) / 2) + "px"
            }
        }, t.showCover = function () {
            if (!this._cover) {
                this._cover = document.createElement("DIV"), this._cover.className = "gantt_cal_cover";
                var t = void 0 !== document.height ? document.height : document.body.offsetHeight,
                    e = document.documentElement ? document.documentElement.scrollHeight : 0;
                this._cover.style.height = Math.max(t, e) + "px", document.body.appendChild(this._cover)
            }
        }, t._init_lightbox_events = function () {
            t.lightbox_events = {}, t.lightbox_events.gantt_save_btn = function () {
                t._save_lightbox()
            }, t.lightbox_events.gantt_delete_btn = function () {
                t.callEvent("onLightboxDelete", [t._lightbox_id]) && (t.isTaskExists(t._lightbox_id) ? t.$click.buttons.delete(t._lightbox_id) : t.hideLightbox())
            }, t.lightbox_events.gantt_cancel_btn = function () {
                t._cancel_lightbox()
            }, t.lightbox_events.default = function (i, n) {
                if (n.getAttribute("data-dhx-button")) t.callEvent("onLightboxButton", [n.className, n, i]); else {
                    var r, a, s = e.getClassName(n);
                    if (-1 != s.indexOf("gantt_custom_button")) if (-1 != s.indexOf("gantt_custom_button_")) for (r = n.parentNode.getAttribute("data-index"), a = n; a && -1 == e.getClassName(a).indexOf("gantt_cal_lsection");) a = a.parentNode; else r = n.getAttribute("data-index"), a = n.parentNode, n = n.firstChild;
                    var o = t._get_typed_lightbox_config();
                    r && (r *= 1, t.form_blocks[o[1 * r].type].button_click(r, n, a, a.nextSibling))
                }
            }, this.event(t.getLightbox(), "click", function (i) {
                var n = (i = i || window.event).target ? i.target : i.srcElement, r = e.getClassName(n);
                return r || (n = n.previousSibling, r = e.getClassName(n)), n && r && 0 === r.indexOf("gantt_btn_set") && (n = n.firstChild, r = e.getClassName(n)), !(!n || !r) && (t.defined(t.lightbox_events[n.className]) ? t.lightbox_events[n.className] : t.lightbox_events.default)(i, n)
            }), t.getLightbox().onkeydown = function (i) {
                var n = i || window.event, r = i.target || i.srcElement,
                    a = e.getClassName(r).indexOf("gantt_btn_set") > -1;
                switch ((i || n).keyCode) {
                    case t.constants.KEY_CODES.SPACE:
                        if ((i || n).shiftKey) return;
                        a && r.click && r.click();
                        break;
                    case t.keys.edit_save:
                        if ((i || n).shiftKey) return;
                        a && r.click ? r.click() : t._save_lightbox();
                        break;
                    case t.keys.edit_cancel:
                        t._cancel_lightbox()
                }
            }
        }, t._cancel_lightbox = function () {
            var e = this.getLightboxValues();
            this.callEvent("onLightboxCancel", [this._lightbox_id, e.$new]), t.isTaskExists(e.id) && e.$new && this.silent(function () {
                t.$data.tasksStore.removeItem(e.id), t._update_flags(e.id, null)
            }), this.refreshData(), this.hideLightbox()
        }, t._save_lightbox = function () {
            var t = this.getLightboxValues();
            this.callEvent("onLightboxSave", [this._lightbox_id, t, !!t.$new]) && (t.$new ? (delete t.$new, this.addTask(t)) : this.isTaskExists(t.id) && (this.mixin(this.getTask(t.id), t, !0), this.refreshTask(t.id), this.updateTask(t.id)), this.refreshData(), this.hideLightbox())
        }, t._resolve_default_mapping = function (t) {
            var e = t.map_to;
            return {
                time: !0,
                time_optional: !0,
                duration: !0,
                duration_optional: !0
            }[t.type] && ("auto" == t.map_to ? e = {
                start_date: "start_date",
                end_date: "end_date",
                duration: "duration"
            } : "string" == typeof t.map_to && (e = {start_date: t.map_to})), e
        }, t.getLightboxValues = function () {
            var e = {};
            t.isTaskExists(this._lightbox_id) && (e = this.mixin({}, this.getTask(this._lightbox_id)));
            for (var i = this._get_typed_lightbox_config(), n = 0; n < i.length; n++) {
                var r = document.getElementById(i[n].id);
                r = r ? r.nextSibling : r;
                var a = this.form_blocks[i[n].type];
                if (a) {
                    var s = a.get_value.call(this, r, e, i[n]), o = t._resolve_default_mapping(i[n]);
                    if ("string" == typeof o && "auto" != o) e[o] = s; else if ("object" == typeof o) for (var l in o) o[l] && (e[o[l]] = s[l])
                }
            }
            return e
        }, t.hideLightbox = function () {
            var t = this.getLightbox();
            t && (t.style.display = "none"), this._waiAria.lightboxHiddenAttr(t), this._lightbox_id = null, this.hideCover(), this.callEvent("onAfterLightbox", [])
        }, t.hideCover = function () {
            this._cover && this._cover.parentNode.removeChild(this._cover), this._cover = null
        }, t.resetLightbox = function () {
            t._lightbox && !t._custom_lightbox && t._lightbox.parentNode.removeChild(t._lightbox), t._lightbox = null
        }, t._set_lightbox_values = function (e, i) {
            var n = e, r = i.getElementsByTagName("span"), a = [];
            t.templates.lightbox_header ? (a.push(""), a.push(t.templates.lightbox_header(n.start_date, n.end_date, n)), r[1].innerHTML = "", r[2].innerHTML = t.templates.lightbox_header(n.start_date, n.end_date, n)) : (a.push(this.templates.task_time(n.start_date, n.end_date, n)), a.push((this.templates.task_text(n.start_date, n.end_date, n) || "").substr(0, 70)), r[1].innerHTML = this.templates.task_time(n.start_date, n.end_date, n), r[2].innerHTML = (this.templates.task_text(n.start_date, n.end_date, n) || "").substr(0, 70)), r[1].innerHTML = a[0], r[2].innerHTML = a[1], t._waiAria.lightboxHeader(i, a.join(" "));
            for (var s = this._get_typed_lightbox_config(this.getLightboxType()), o = 0; o < s.length; o++) {
                var l = s[o];
                if (this.form_blocks[l.type]) {
                    var c = document.getElementById(l.id).nextSibling, d = this.form_blocks[l.type],
                        h = t._resolve_default_mapping(s[o]), u = this.defined(n[h]) ? n[h] : l.default_value;
                    d.set_value.call(t, c, u, n, l), l.focus && d.focus.call(t, c)
                }
            }
            e.id && (t._lightbox_id = e.id)
        }, t._fill_lightbox = function (t, e) {
            var i = this.getTask(t);
            this._set_lightbox_values(i, e)
        }, t.getLightboxSection = function (e) {
            for (var i = this._get_typed_lightbox_config(), n = 0; n < i.length && i[n].name != e; n++) ;
            var r = i[n];
            if (!r) return null;
            this._lightbox || this.getLightbox();
            var a = document.getElementById(r.id), s = a.nextSibling, o = {
                section: r, header: a, node: s, getValue: function (e) {
                    return t.form_blocks[r.type].get_value.call(t, s, e || {}, r)
                }, setValue: function (e, i) {
                    return t.form_blocks[r.type].set_value.call(t, s, e, i || {}, r)
                }
            }, l = this._lightbox_methods["get_" + r.type + "_control"];
            return l ? l(o) : o
        }, t._lightbox_methods.get_template_control = function (t) {
            return t.control = t.node, t
        }, t._lightbox_methods.get_select_control = function (t) {
            return t.control = t.node.getElementsByTagName("select")[0], t
        }, t._lightbox_methods.get_textarea_control = function (t) {
            return t.control = t.node.getElementsByTagName("textarea")[0], t
        }, t._lightbox_methods.get_time_control = function (t) {
            return t.control = t.node.getElementsByTagName("select"), t
        }, t._init_dnd_events = function () {
            this.event(document.body, "mousemove", t._move_while_dnd), this.event(document.body, "mouseup", t._finish_dnd), t._init_dnd_events = function () {
            }
        }, t._move_while_dnd = function (e) {
            if (t._dnd_start_lb) {
                document.gantt_unselectable || (document.body.className += " gantt_unselectable", document.gantt_unselectable = !0);
                var i = t.getLightbox(), n = e && e.target ? [e.pageX, e.pageY] : [event.clientX, event.clientY];
                i.style.top = t._lb_start[1] + n[1] - t._dnd_start_lb[1] + "px", i.style.left = t._lb_start[0] + n[0] - t._dnd_start_lb[0] + "px"
            }
        }, t._ready_to_dnd = function (e) {
            var i = t.getLightbox();
            t._lb_start = [parseInt(i.style.left, 10), parseInt(i.style.top, 10)], t._dnd_start_lb = e && e.target ? [e.pageX, e.pageY] : [event.clientX, event.clientY]
        }, t._finish_dnd = function () {
            t._lb_start && (t._lb_start = t._dnd_start_lb = !1, document.body.className = document.body.className.replace(" gantt_unselectable", ""), document.gantt_unselectable = !1)
        }, t._focus = function (e, i) {
            if (e && e.focus) if (t.config.touch) ; else try {
                i && e.select && e.select(), e.focus()
            } catch (t) {
            }
        }, t.form_blocks = {
            getTimePicker: function (e, i) {
                var r, a, s, o = "", l = this.config, c = {
                    first: 0,
                    last: 1440,
                    date: this.date.date_part(new Date(t._min_date.valueOf())),
                    timeFormat: function (e) {
                        var i, r, a;
                        if (e.time_format) return e.time_format;
                        a = ["%d", "%m", "%Y"], i = t.getScale(), r = i ? i.unit : t.config.duration_unit, n.getSecondsInUnit(r) < n.getSecondsInUnit("day") && a.push("%H:%i");
                        return a
                    }(e)
                };
                for (e._time_format_order = {size: 0}, t.config.limit_time_select && (c.first = 60 * l.first_hour, c.last = 60 * l.last_hour + 1, c.date.setHours(l.first_hour)), r = 0; r < c.timeFormat.length; r++) r > 0 && (o += " "), (a = f(e, r, c)) && (s = t._waiAria.lightboxSelectAttrString(c.timeFormat[r]), o += "<select " + (e.readonly ? "disabled='disabled'" : "") + (i ? " style='display:none' " : "") + s + ">" + a + "</select>");
                return o
            },
            _fill_lightbox_select: function (e, i, n, r) {
                if (e[i + r[0]].value = n.getDate(), e[i + r[1]].value = n.getMonth(), e[i + r[2]].value = n.getFullYear(), t.defined(r[3])) {
                    var a = 60 * n.getHours() + n.getMinutes();
                    a = Math.round(a / t._get_timepicker_step()) * t._get_timepicker_step();
                    var s = e[i + r[3]];
                    s.value = a, s.setAttribute("data-value", a)
                }
            },
            template: new r,
            textarea: new a,
            select: new o,
            time: new s,
            duration: new d,
            parent: new h,
            radio: new c,
            checkbox: new l,
            resources: new u
        }, t._is_lightbox_timepicker = function () {
            for (var t = this._get_typed_lightbox_config(), e = 0; e < t.length; e++) if ("time" == t[e].name && "time" == t[e].type) return !0;
            return !1
        }, t._dhtmlx_confirm = function (e, i, n, r) {
            if (!e) return n();
            var a = {text: e};
            i && (a.title = i), r && (a.ok = r), n && (a.callback = function (t) {
                t && n()
            }), t.confirm(a)
        }, t._get_typed_lightbox_config = function (e) {
            void 0 === e && (e = this.getLightboxType());
            var i = function (t) {
                for (var e in this.config.types) if (this.config.types[e] == t) return e;
                return "task"
            }.call(this, e);
            return t.config.lightbox[i + "_sections"] ? t.config.lightbox[i + "_sections"] : t.config.lightbox.sections
        }, t._silent_redraw_lightbox = function (t) {
            var e = this.getLightboxType();
            if (this.getState().lightbox) {
                var i = this.getState().lightbox, n = this.getLightboxValues(), r = this.copy(this.getTask(i));
                this.resetLightbox();
                var a = this.mixin(r, n, !0), s = this.getLightbox(t || void 0);
                this._center_lightbox(this.getLightbox()), this._set_lightbox_values(a, s)
            } else this.resetLightbox(), this.getLightbox(t || void 0);
            this.callEvent("onLightboxChange", [e, this.getLightboxType()])
        }
    }
}, function (t, e, i) {
    var n = i(3);
    t.exports = function (t) {
        t.isUnscheduledTask = function (e) {
            return t.assert(e && e instanceof Object, "Invalid argument <b>task</b>=" + e + " of gantt.isUnscheduledTask. Task object was expected"), !!e.unscheduled || !e.start_date
        }, t._isAllowedUnscheduledTask = function (e) {
            return !(!e.unscheduled || !t.config.show_unscheduled)
        }, t.isTaskVisible = function (e) {
            if (!this.isTaskExists(e)) return !1;
            var i = this.getTask(e), n = i.start_date ? i.start_date.valueOf() : null,
                r = i.end_date ? i.end_date.valueOf() : null;
            return !!(t._isAllowedUnscheduledTask(i) || n && r && n <= this._max_date.valueOf() && r >= this._min_date.valueOf()) && !!(t.getGlobalTaskIndex(e) >= 0)
        }, t._defaultTaskDate = function (e, i) {
            var n = !(!i || i == this.config.root_id) && this.getTask(i), r = "";
            if (n) r = n.start_date; else {
                var a = this.getTaskByIndex(0);
                r = a ? a.start_date ? a.start_date : a.end_date ? this.calculateEndDate({
                    start_date: a.end_date,
                    duration: -this.config.duration_step
                }) : "" : this.config.start_date || this.getState().min_date
            }
            return t.assert(r, "Invalid dates"), new Date(r)
        }, t._set_default_task_timing = function (e) {
            e.start_date = e.start_date || t._defaultTaskDate(e, this.getParent(e)), e.duration = e.duration || this.config.duration_step, e.end_date = e.end_date || this.calculateEndDate(e)
        }, t.createTask = function (e, i, n) {
            (e = e || {}, t.defined(e.id) || (e.id = t.uid()), e.start_date || (e.start_date = t._defaultTaskDate(e, i)), void 0 === e.text && (e.text = t.locale.labels.new_task), void 0 === e.duration && (e.duration = 1), i) && (this.setParent(e, i, !0), this.getTask(i).$open = !0);
            return this.callEvent("onTaskCreated", [e]) ? (this.config.details_on_create ? (e.$new = !0, this.silent(function () {
                t.$data.tasksStore.addItem(e, n)
            }), this.selectTask(e.id), this.refreshData(), this.showLightbox(e.id)) : this.addTask(e, i, n) && (this.showTask(e.id), this.selectTask(e.id)), e.id) : null
        }, t._update_flags = function (e, i) {
            var n = t.$data.tasksStore;
            void 0 === e ? (this._lightbox_id = null, n.silent(function () {
                n.unselect()
            }), this._tasks_dnd && this._tasks_dnd.drag && (this._tasks_dnd.drag.id = null)) : (this._lightbox_id == e && (this._lightbox_id = i), n.getSelectedId() == e && n.silent(function () {
                n.unselect(e), n.select(i)
            }), this._tasks_dnd && this._tasks_dnd.drag && this._tasks_dnd.drag.id == e && (this._tasks_dnd.drag.id = i))
        }, t._get_task_timing_mode = function (t, e) {
            var i = this.getTaskType(t.type), n = {type: i, $no_start: !1, $no_end: !1};
            return e || i != t.$rendered_type ? (i == this.config.types.project ? n.$no_end = n.$no_start = !0 : i != this.config.types.milestone && (n.$no_end = !(t.end_date || t.duration), n.$no_start = !t.start_date, this._isAllowedUnscheduledTask(t) && (n.$no_end = n.$no_start = !1)), n) : (n.$no_start = t.$no_start, n.$no_end = t.$no_end, n)
        }, t._init_task_timing = function (e) {
            var i = t._get_task_timing_mode(e, !0), n = e.$rendered_type != i.type, r = i.type;
            n && (e.$no_start = i.$no_start, e.$no_end = i.$no_end, e.$rendered_type = i.type), n && r != this.config.types.milestone && r == this.config.types.project && this._set_default_task_timing(e), r == this.config.types.milestone && (e.end_date = e.start_date), e.start_date && e.end_date && (e.duration = this.calculateDuration(e)), e.end_date || (e.end_date = e.start_date), e.duration = e.duration || 0
        }, t.isSummaryTask = function (e) {
            t.assert(e && e instanceof Object, "Invalid argument <b>task</b>=" + e + " of gantt.isSummaryTask. Task object was expected");
            var i = t._get_task_timing_mode(e);
            return !(!i.$no_end && !i.$no_start)
        }, t.resetProjectDates = function (t) {
            var e = this._get_task_timing_mode(t);
            if (e.$no_end || e.$no_start) {
                var i = this.getSubtaskDates(t.id);
                this._assign_project_dates(t, i.start_date, i.end_date)
            }
        }, t.getSubtaskDuration = function (e) {
            var i = 0, n = void 0 !== e ? e : t.config.root_id;
            return this.eachTask(function (e) {
                this.getTaskType(e.type) == t.config.types.project || this.isUnscheduledTask(e) || (i += e.duration)
            }, n), i
        }, t.getSubtaskDates = function (e) {
            var i = null, n = null, r = void 0 !== e ? e : t.config.root_id;
            return this.eachTask(function (e) {
                this.getTaskType(e.type) == t.config.types.project || this.isUnscheduledTask(e) || (e.start_date && !e.$no_start && (!i || i > e.start_date.valueOf()) && (i = e.start_date.valueOf()), e.end_date && !e.$no_end && (!n || n < e.end_date.valueOf()) && (n = e.end_date.valueOf()))
            }, r), {start_date: i ? new Date(i) : null, end_date: n ? new Date(n) : null}
        }, t._assign_project_dates = function (t, e, i) {
            var n = this._get_task_timing_mode(t);
            n.$no_start && (t.start_date = e && e != 1 / 0 ? new Date(e) : this._defaultTaskDate(t, this.getParent(t))), n.$no_end && (t.end_date = i && i != -1 / 0 ? new Date(i) : this.calculateEndDate({
                start_date: t.start_date,
                duration: this.config.duration_step,
                task: t
            })), (n.$no_start || n.$no_end) && this._init_task_timing(t)
        }, t._update_parents = function (e, i) {
            if (e) {
                var n = this.getTask(e), r = this.getParent(n), a = this._get_task_timing_mode(n), s = !0;
                if (a.$no_start || a.$no_end) {
                    var o = n.start_date.valueOf(), l = n.end_date.valueOf();
                    t.resetProjectDates(n), o == n.start_date.valueOf() && l == n.end_date.valueOf() && (s = !1), s && !i && this.refreshTask(n.id, !0)
                }
                s && r && this.isTaskExists(r) && this._update_parents(r, i)
            }
        }, t.roundDate = function (e) {
            var i = t.getScale();
            n.isDate(e) && (e = {
                date: e,
                unit: i ? i.unit : t.config.duration_unit,
                step: i ? i.step : t.config.duration_step
            });
            var r, a, s, o = e.date, l = e.step, c = e.unit;
            if (!i) return o;
            if (c == i.unit && l == i.step && +o >= +i.min_date && +o <= +i.max_date) s = Math.floor(t.columnIndexByDate(o)), i.trace_x[s] || (s -= 1, i.rtl && (s = 0)), a = new Date(i.trace_x[s]), r = t.date.add(a, l, c); else {
                for (s = Math.floor(t.columnIndexByDate(o)), r = t.date[c + "_start"](new Date(i.min_date)), i.trace_x[s] && (r = t.date[c + "_start"](i.trace_x[s])); +r < +o;) {
                    var d = (r = t.date[c + "_start"](t.date.add(r, l, c))).getTimezoneOffset();
                    r = t._correct_dst_change(r, d, r, c), t.date[c + "_start"] && (r = t.date[c + "_start"](r))
                }
                a = t.date.add(r, -1 * l, c)
            }
            return e.dir && "future" == e.dir ? r : e.dir && "past" == e.dir ? a : Math.abs(o - a) < Math.abs(r - o) ? a : r
        }, t.correctTaskWorkTime = function (e) {
            t.config.work_time && t.config.correct_work_time && (this.isWorkTime(e.start_date, void 0, e) ? this.isWorkTime(new Date(+e.end_date - 1), void 0, e) || (e.end_date = this.calculateEndDate(e)) : (e.start_date = this.getClosestWorkTime({
                date: e.start_date,
                dir: "future",
                task: e
            }), e.end_date = this.calculateEndDate(e)))
        }, t.attachEvent("onBeforeTaskUpdate", function (e, i) {
            return t._init_task_timing(i), !0
        }), t.attachEvent("onBeforeTaskAdd", function (e, i) {
            return t._init_task_timing(i), !0
        })
    }
}, function (t, e, i) {
    var n = i(0);
    t.exports = {
        create: function (t, e) {
            return {
                getWorkHours: function (t) {
                    return e.getWorkHours(t)
                },
                setWorkTime: function (t) {
                    return e.setWorkTime(t)
                },
                unsetWorkTime: function (t) {
                    e.unsetWorkTime(t)
                },
                isWorkTime: function (t, i, n) {
                    return e.isWorkTime(t, i, n)
                },
                getClosestWorkTime: function (t) {
                    return e.getClosestWorkTime(t)
                },
                calculateDuration: function (t, i, n) {
                    return e.calculateDuration(t, i, n)
                },
                _hasDuration: function (t, i, n) {
                    return e.hasDuration(t, i, n)
                },
                calculateEndDate: function (t, i, n, r) {
                    return e.calculateEndDate(t, i, n, r)
                },
                createCalendar: n.bind(t.createCalendar, t),
                addCalendar: n.bind(t.addCalendar, t),
                getCalendar: n.bind(t.getCalendar, t),
                getCalendars: n.bind(t.getCalendars, t),
                getTaskCalendar: n.bind(t.getTaskCalendar, t),
                deleteCalendar: n.bind(t.deleteCalendar, t)
            }
        }
    }
}, function (t, e) {
    function i(t, e) {
        this.argumentsHelper = e, this.$gantt = t
    }

    i.prototype = {
        getWorkHours: function () {
            return [0, 24]
        }, setWorkTime: function () {
            return !0
        }, unsetWorkTime: function () {
            return !0
        }, isWorkTime: function () {
            return !0
        }, getClosestWorkTime: function (t) {
            return this.argumentsHelper.getClosestWorkTimeArguments.apply(this.argumentsHelper, arguments).date
        }, calculateDuration: function () {
            var t = this.argumentsHelper.getDurationArguments.apply(this.argumentsHelper, arguments), e = t.start_date,
                i = t.end_date, n = t.unit, r = t.step;
            return this._calculateDuration(e, i, n, r)
        }, _calculateDuration: function (t, e, i, n) {
            var r = this.$gantt.date, a = {week: 6048e5, day: 864e5, hour: 36e5, minute: 6e4}, s = 0;
            if (a[i]) s = Math.round((e - t) / (n * a[i])); else {
                for (var o = new Date(t), l = new Date(e); o.valueOf() < l.valueOf();) s += 1, o = r.add(o, n, i);
                o.valueOf() != e.valueOf() && (s += (l - o) / (r.add(o, n, i) - o))
            }
            return Math.round(s)
        }, hasDuration: function () {
            var t = this.argumentsHelper.getDurationArguments.apply(this.argumentsHelper, arguments), e = t.start_date,
                i = t.end_date;
            return !!t.unit && (e = new Date(e), i = new Date(i), e.valueOf() < i.valueOf())
        }, calculateEndDate: function () {
            var t = this.argumentsHelper.calculateEndDateArguments.apply(this.argumentsHelper, arguments),
                e = t.start_date, i = t.duration, n = t.unit, r = t.step;
            return this.$gantt.date.add(e, r * i, n)
        }
    }, t.exports = i
}, function (t, e, i) {
    var n = i(16), r = i(53);

    function a(t) {
        this.$gantt = t.$gantt, this.argumentsHelper = n(this.$gantt), this.calendarManager = t, this.$disabledCalendar = new r(this.$gantt, this.argumentsHelper)
    }

    a.prototype = {
        _getCalendar: function (t) {
            var e;
            if (this.$gantt.$services.config().work_time) {
                var i = this.calendarManager;
                t.task ? e = i.getTaskCalendar(t.task) : t.id ? e = i.getTaskCalendar(t) : t.calendar && (e = t.calendar), e || (e = i.getTaskCalendar())
            } else e = this.$disabledCalendar;
            return e
        }, getWorkHours: function (t) {
            return t = this.argumentsHelper.getWorkHoursArguments.apply(this.argumentsHelper, arguments), this._getCalendar(t).getWorkHours(t.date)
        }, setWorkTime: function (t, e) {
            return t = this.argumentsHelper.setWorkTimeArguments.apply(this.argumentsHelper, arguments), e || (e = this.calendarManager.getCalendar()), e.setWorkTime(t)
        }, unsetWorkTime: function (t, e) {
            return t = this.argumentsHelper.unsetWorkTimeArguments.apply(this.argumentsHelper, arguments), e || (e = this.calendarManager.getCalendar()), e.unsetWorkTime(t)
        }, isWorkTime: function (t, e, i, n) {
            var r = this.argumentsHelper.isWorkTimeArguments.apply(this.argumentsHelper, arguments);
            return this._getCalendar(r).isWorkTime(r)
        }, getClosestWorkTime: function (t) {
            return t = this.argumentsHelper.getClosestWorkTimeArguments.apply(this.argumentsHelper, arguments), this._getCalendar(t).getClosestWorkTime(t)
        }, calculateDuration: function () {
            var t = this.argumentsHelper.getDurationArguments.apply(this.argumentsHelper, arguments);
            return this._getCalendar(t).calculateDuration(t)
        }, hasDuration: function () {
            var t = this.argumentsHelper.hasDurationArguments.apply(this.argumentsHelper, arguments);
            return this._getCalendar(t).hasDuration(t)
        }, calculateEndDate: function (t) {
            t = this.argumentsHelper.calculateEndDateArguments.apply(this.argumentsHelper, arguments);
            return this._getCalendar(t).calculateEndDate(t)
        }
    }, t.exports = a
}, function (t, e) {
    function i() {
        this._cache = {}
    }

    i.prototype = {
        get: function (t, e) {
            var i = -1, n = this._cache;
            if (n && n[t]) {
                var r = n[t], a = e.getTime();
                void 0 !== r[a] && (i = r[a])
            }
            return i
        }, put: function (t, e, i) {
            if (!t || !e) return !1;
            var n = this._cache, r = e.getTime();
            return i = !!i, !!n && (n[t] || (n[t] = {}), n[t][r] = i, !0)
        }, clear: function () {
            this._cache = {}
        }
    }, t.exports = i
}, function (t, e, i) {
    var n = i(55), r = i(0);

    function a(t, e) {
        this.argumentsHelper = e, this.$gantt = t, this._workingUnitsCache = new n
    }

    a.prototype = {
        units: ["year", "month", "week", "day", "hour", "minute"], _getUnitOrder: function (t) {
            for (var e = 0, i = this.units.length; e < i; e++) if (this.units[e] == t) return e
        }, _timestamp: function (t) {
            var e = null;
            return t.day || 0 === t.day ? e = t.day : t.date && (e = Date.UTC(t.date.getFullYear(), t.date.getMonth(), t.date.getDate())), e
        }, _checkIfWorkingUnit: function (t, e, i) {
            return void 0 === i && (i = this._getUnitOrder(e)), void 0 === i || !(i && !this._isWorkTime(t, this.units[i - 1], i - 1)) && (!this["_is_work_" + e] || this["_is_work_" + e](t))
        }, _is_work_day: function (t) {
            var e = this._getWorkHours(t);
            return e instanceof Array && e.length > 0
        }, _is_work_hour: function (t) {
            for (var e = this._getWorkHours(t), i = t.getHours(), n = 0; n < e.length; n += 2) {
                if (void 0 === e[n + 1]) return e[n] == i;
                if (i >= e[n] && i < e[n + 1]) return !0
            }
            return !1
        }, _internDatesPull: {}, _nextDate: function (t, e, i) {
            return this.$gantt.date.add(t, i, e)
        }, _getWorkUnitsBetweenGeneric: function (t, e, i, n) {
            var r = this.$gantt.date, a = new Date(t), s = new Date(e);
            n = n || 1;
            var o, l, c = 0, d = null, h = !1;
            (o = r[i + "_start"](new Date(a))).valueOf() != a.valueOf() && (h = !0);
            var u = !1;
            (l = r[i + "_start"](new Date(e))).valueOf() != e.valueOf() && (u = !0);
            for (var g = !1; a.valueOf() < s.valueOf();) g = (d = this._nextDate(a, i, n)).valueOf() > s.valueOf(), this._isWorkTime(a, i) && ((h || u && g) && (o = r[i + "_start"](new Date(a)), l = r.add(o, n, i)), h ? (h = !1, d = this._nextDate(o, i, n), c += (l.valueOf() - a.valueOf()) / (l.valueOf() - o.valueOf())) : u && g ? (u = !1, c += (s.valueOf() - a.valueOf()) / (l.valueOf() - o.valueOf())) : c++), a = d;
            return c
        }, _getHoursPerDay: function (t) {
            for (var e = this._getWorkHours(t), i = 0, n = 0; n < e.length; n += 2) i += e[n + 1] - e[n] || 0;
            return i
        }, _getWorkHoursForRange: function (t, e) {
            for (var i = 0, n = new Date(t), r = new Date(e); n.valueOf() < r.valueOf();) this._isWorkTime(n, "day") && (i += this._getHoursPerDay(n)), n = this._nextDate(n, "day", 1);
            return i
        }, _getWorkUnitsBetweenHours: function (t, e, i, n) {
            var r = new Date(t), a = new Date(e);
            n = n || 1;
            var s = new Date(r), o = this.$gantt.date.add(this.$gantt.date.day_start(new Date(r)), 1, "day");
            if (a.valueOf() <= o.valueOf()) return this._getWorkUnitsBetweenGeneric(t, e, i, n);
            var l = this.$gantt.date.day_start(new Date(a)), c = a, d = this._getWorkUnitsBetweenGeneric(s, o, i, n),
                h = this._getWorkUnitsBetweenGeneric(l, c, i, n), u = this._getWorkHoursForRange(o, l);
            return u = u / n + d + h
        }, _getCalendar: function () {
            return this.worktime
        }, _setCalendar: function (t) {
            this.worktime = t
        }, _tryChangeCalendarSettings: function (t) {
            var e = JSON.stringify(this._getCalendar());
            return t(), !this._isEmptyCalendar(this._getCalendar()) || (this.$gantt.assert(!1, "Invalid calendar settings, no worktime available"), this._setCalendar(JSON.parse(e)), this._workingUnitsCache.clear(), !1)
        }, _isEmptyCalendar: function (t) {
            var e = !1, i = [], n = !0;
            for (var r in t.dates) e |= !!t.dates[r], i.push(r);
            var a = [];
            for (r = 0; r < i.length; r++) i[r] < 10 && a.push(i[r]);
            a.sort();
            for (r = 0; r < 7; r++) a[r] != r && (n = !1);
            return n ? !e : !(e || t.hours)
        }, getWorkHours: function () {
            var t = this.argumentsHelper.getWorkHoursArguments.apply(this.argumentsHelper, arguments);
            return this._getWorkHours(t.date)
        }, _getWorkHours: function (t) {
            var e = this._timestamp({date: t}), i = !0, n = this._getCalendar();
            return void 0 !== n.dates[e] ? i = n.dates[e] : void 0 !== n.dates[t.getDay()] && (i = n.dates[t.getDay()]), !0 === i ? n.hours : i || []
        }, setWorkTime: function (t) {
            return this._tryChangeCalendarSettings(r.bind(function () {
                var e = void 0 === t.hours || t.hours, i = this._timestamp(t);
                null !== i ? this._getCalendar().dates[i] = e : this._getCalendar().hours = e, this._workingUnitsCache.clear()
            }, this))
        }, unsetWorkTime: function (t) {
            return this._tryChangeCalendarSettings(r.bind(function () {
                if (t) {
                    var e = this._timestamp(t);
                    null !== e && delete this._getCalendar().dates[e]
                } else this.reset_calendar();
                this._workingUnitsCache.clear()
            }, this))
        }, _isWorkTime: function (t, e, i) {
            var n = this._workingUnitsCache.get(e, t);
            return -1 == n && (n = this._checkIfWorkingUnit(t, e, i), this._workingUnitsCache.put(e, t, n)), n
        }, isWorkTime: function () {
            var t = this.argumentsHelper.isWorkTimeArguments.apply(this.argumentsHelper, arguments);
            return this._isWorkTime(t.date, t.unit)
        }, calculateDuration: function () {
            var t = this.argumentsHelper.getDurationArguments.apply(this.argumentsHelper, arguments);
            if (!t.unit) return !1;
            var e = 0;
            return e = "hour" == t.unit ? this._getWorkUnitsBetweenHours(t.start_date, t.end_date, t.unit, t.step) : this._getWorkUnitsBetweenGeneric(t.start_date, t.end_date, t.unit, t.step), Math.round(e)
        }, hasDuration: function () {
            var t = this.argumentsHelper.getDurationArguments.apply(this.argumentsHelper, arguments), e = t.start_date,
                i = t.end_date, n = t.unit, r = t.step;
            if (!n) return !1;
            var a = new Date(e), s = new Date(i);
            for (r = r || 1; a.valueOf() < s.valueOf();) {
                if (this._isWorkTime(a, n)) return !0;
                a = this._nextDate(a, n, r)
            }
            return !1
        }, calculateEndDate: function () {
            var t = this.argumentsHelper.calculateEndDateArguments.apply(this.argumentsHelper, arguments),
                e = t.start_date, i = t.duration, n = t.unit, r = t.step, a = t.duration >= 0 ? 1 : -1;
            return this._calculateEndDate(e, i, n, r * a)
        }, _calculateEndDate: function (t, e, i, n) {
            if (!i) return !1;
            var r = new Date(t), a = 0;
            for (n = n || 1, e = Math.abs(1 * e); a < e;) {
                var s = this._nextDate(r, i, n);
                this._isWorkTime(n > 0 ? new Date(s.valueOf() - 1) : new Date(s.valueOf() + 1), i) && a++, r = s
            }
            return r
        }, getClosestWorkTime: function () {
            var t = this.argumentsHelper.getClosestWorkTimeArguments.apply(this.argumentsHelper, arguments);
            return this._getClosestWorkTime(t)
        }, _getClosestWorkTime: function (t) {
            if (this._isWorkTime(t.date, t.unit)) return t.date;
            var e = t.unit, i = this.$gantt.date[e + "_start"](new Date(t.date)), n = new Date(i), r = new Date(i),
                a = !0, s = 0, o = "any" == t.dir || !t.dir, l = 1;
            "past" == t.dir && (l = -1);
            for (var c = this._getUnitOrder(e), d = this.units[c - 1]; !this._isWorkTime(i, e);) {
                if (d && !this._isWorkTime(i, d)) {
                    var h = this.$gantt.copy(t);
                    return h.date = i, h.unit = d, this._getClosestWorkTime(h)
                }
                o && (i = a ? n : r, l *= -1);
                var u = i.getTimezoneOffset();
                if (i = this.$gantt.date.add(i, l, e), i = this.$gantt._correct_dst_change(i, u, l, e), this.$gantt.date[e + "_start"] && (i = this.$gantt.date[e + "_start"](i)), o && (a ? n = i : r = i), a = !a, ++s > 3e3) return this.$gantt.assert(!1, "Invalid working time check"), !1
            }
            return i != r && "past" != t.dir || (i = this.$gantt.date.add(i, 1, e)), i
        }
    }, t.exports = a
}, function (t, e, i) {
    var n = i(0), r = i(16), a = i(56);

    function s(t) {
        this.$gantt = t, this._calendars = {}
    }

    s.prototype = {
        _calendars: {},
        _getDayHoursForMultiple: function (t, e) {
            for (var i = [], n = !0, r = 0, a = this.$gantt.date.day_start(new Date(e)), s = 0; s < 24; s++) t.reduce(function (t, e) {
                return t && e._is_work_hour(a)
            }, !0) ? (n ? (i[r] = s, i[r + 1] = s + 1, r += 2) : i[r - 1] += 1, n = !1) : n || (n = !0), a = this.$gantt.date.add(a, 1, "hour");
            return i.length || (i = !1), i
        },
        mergeCalendars: function () {
            var t, e = this.createCalendar(), i = [], n = Array.prototype.slice.call(arguments, 0);
            e.worktime.hours = [0, 24], e.worktime.dates = {};
            var r = this.$gantt.date.day_start(new Date(2592e5));
            for (t = 0; t < 7; t++) i = this._getDayHoursForMultiple(n, r), e.worktime.dates[t] = i, r = this.$gantt.date.add(r, 1, "day");
            for (var a = 0; a < n.length; a++) for (var s in n[a].worktime.dates) +s > 1e4 && (i = this._getDayHoursForMultiple(n, new Date(+s)), e.worktime.dates[s] = i);
            return e
        },
        _convertWorktimeSettings: function (t) {
            var e = t.days;
            if (e) {
                t.dates = t.dates || {};
                for (var i = 0; i < e.length; i++) t.dates[i] = e[i], e[i] instanceof Array || (t.dates[i] = !!e[i]);
                delete t.days
            }
            return t
        },
        createCalendar: function (t) {
            var e;
            t || (t = {}), e = t.worktime ? n.copy(t.worktime) : n.copy(t);
            var i = n.copy(this.defaults.fulltime.worktime);
            n.mixin(e, i);
            var s = {id: n.uid() + "", worktime: this._convertWorktimeSettings(e)},
                o = new a(this.$gantt, r(this.$gantt));
            return n.mixin(o, s), o._tryChangeCalendarSettings(function () {
            }) ? o : null
        },
        getCalendar: function (t) {
            return t = t || "global", this.createDefaultCalendars(), this._calendars[t]
        },
        getCalendars: function () {
            var t = [];
            for (var e in this._calendars) t.push(this.getCalendar(e));
            return t
        },
        getTaskCalendar: function (t) {
            var e = this.$gantt.$services.config();
            if (!t) return this.getCalendar();
            if (t[e.calendar_property]) return this.getCalendar(t[e.calendar_property]);
            if (e.resource_calendars) for (var i in e.resource_calendars) {
                var n = e.resource_calendars[i];
                if (t[i]) {
                    var r = n[t[i]];
                    if (r) return this.getCalendar(r)
                }
            }
            return this.getCalendar()
        },
        addCalendar: function (t) {
            if (!(t instanceof a)) {
                var e = t.id;
                (t = this.createCalendar(t)).id = e
            }
            var i = this.$gantt.$services.config();
            return t.id = t.id || n.uid(), this._calendars[t.id] = t, i.worktimes || (i.worktimes = {}), i.worktimes[t.id] = t.worktime, t.id
        },
        deleteCalendar: function (t) {
            var e = this.$gantt.$services.config();
            return !!t && (!!this._calendars[t] && (delete this._calendars[t], e.worktimes && e.worktimes[t] && delete e.worktimes[t], !0))
        },
        restoreConfigCalendars: function (t) {
            for (var e in t) if (!this._calendars[e]) {
                var i = t[e], n = this.createCalendar(i);
                n.id = e, this.addCalendar(n)
            }
        },
        defaults: {
            global: {id: "global", worktime: {hours: [8, 17], days: [0, 1, 1, 1, 1, 1, 0]}},
            fulltime: {id: "fulltime", worktime: {hours: [0, 24], days: [1, 1, 1, 1, 1, 1, 1]}}
        },
        createDefaultCalendars: function () {
            var t = this.$gantt.$services.config();
            this.restoreConfigCalendars(this.defaults), this.restoreConfigCalendars(t.worktimes)
        }
    }, t.exports = s
}, function (t, e, i) {
    var n = i(57), r = i(54), a = i(52), s = i(0);
    t.exports = function (t) {
        var e = new n(t), i = new r(e), o = a.create(e, i);
        s.mixin(t, o)
    }
}, function (t, e, i) {
    var n = i(3);
    t.exports = function (t) {
        t.load = function (e, i, n) {
            this._load_url = e, this.assert(arguments.length, "Invalid load arguments");
            var r = "json", a = null;
            arguments.length >= 3 ? (r = i, a = n) : "string" == typeof arguments[1] ? r = arguments[1] : "function" == typeof arguments[1] && (a = arguments[1]), this._load_type = r, this.callEvent("onLoadStart", [e, r]), this.ajax.get(e, t.bind(function (t) {
                this.on_load(t, r), this.callEvent("onLoadEnd", [e, r]), "function" == typeof a && a.call(this)
            }, this))
        }, t.parse = function (t, e) {
            this.on_load({xmlDoc: {responseText: t}}, e)
        }, t.serialize = function (t) {
            return this[t = t || "json"].serialize()
        }, t.on_load = function (t, e) {
            this.callEvent("onBeforeParse", []), e || (e = "json"), this.assert(this[e], "Invalid data type:'" + e + "'");
            var i = t.xmlDoc.responseText, n = this[e].parse(i, t);
            this._process_loading(n)
        }, t._process_loading = function (t) {
            t.collections && this._load_collections(t.collections), this.$data.tasksStore.parse(t.data);
            var e = t.links || (t.collections ? t.collections.links : []);
            if (this.$data.linksStore.parse(e), this.callEvent("onParse", []), this.render(), this.config.initial_scroll) {
                var i = this.getTaskByIndex(0), n = i ? i.id : this.config.root_id;
                this.isTaskExists(n) && this.showTask(n)
            }
        }, t._load_collections = function (t) {
            var e = !1;
            for (var i in t) if (t.hasOwnProperty(i)) {
                e = !0;
                var n = t[i], r = this.serverList[i];
                if (!r) continue;
                r.splice(0, r.length);
                for (var a = 0; a < n.length; a++) {
                    var s = n[a], o = this.copy(s);
                    for (var l in o.key = o.value, s) if (s.hasOwnProperty(l)) {
                        if ("value" == l || "label" == l) continue;
                        o[l] = s[l]
                    }
                    r.push(o)
                }
            }
            e && this.callEvent("onOptionsLoad", [])
        }, t.attachEvent("onBeforeTaskDisplay", function (t, e) {
            return !e.$ignore
        }), t.json = {
            parse: function (e) {
                return t.assert(e, "Invalid data"), "string" == typeof e && (window.JSON ? e = JSON.parse(e) : t.assert(!1, "JSON is not supported")), e.dhx_security && (t.security_key = e.dhx_security), e
            }, serializeTask: function (t) {
                return this._copyObject(t)
            }, serializeLink: function (t) {
                return this._copyLink(t)
            }, _copyLink: function (t) {
                var e = {};
                for (var i in t) e[i] = t[i];
                return e
            }, _copyObject: function (e) {
                var i = {};
                for (var r in e) "$" != r.charAt(0) && (i[r] = e[r], n.isDate(i[r]) && (i[r] = t.templates.xml_format(i[r])));
                return i
            }, serialize: function () {
                var e = [], i = [];
                t.eachTask(function (i) {
                    t.resetProjectDates(i), e.push(this.serializeTask(i))
                }, t.config.root_id, this);
                for (var n = t.getLinks(), r = 0; r < n.length; r++) i.push(this.serializeLink(n[r]));
                return {data: e, links: i}
            }
        }, t.xml = {
            _xmlNodeToJSON: function (t, e) {
                for (var i = {}, n = 0; n < t.attributes.length; n++) i[t.attributes[n].name] = t.attributes[n].value;
                if (!e) {
                    for (n = 0; n < t.childNodes.length; n++) {
                        var r = t.childNodes[n];
                        1 == r.nodeType && (i[r.tagName] = r.firstChild ? r.firstChild.nodeValue : "")
                    }
                    i.text || (i.text = t.firstChild ? t.firstChild.nodeValue : "")
                }
                return i
            }, _getCollections: function (e) {
                for (var i = {}, n = t.ajax.xpath("//coll_options", e), r = 0; r < n.length; r++) for (var a = i[n[r].getAttribute("for")] = [], s = t.ajax.xpath(".//item", n[r]), o = 0; o < s.length; o++) {
                    for (var l = s[o].attributes, c = {
                        key: s[o].getAttribute("value"),
                        label: s[o].getAttribute("label")
                    }, d = 0; d < l.length; d++) {
                        var h = l[d];
                        "value" != h.nodeName && "label" != h.nodeName && (c[h.nodeName] = h.nodeValue)
                    }
                    a.push(c)
                }
                return i
            }, _getXML: function (e, i, n) {
                n = n || "data", i.getXMLTopNode || (i = t.ajax.parse(i));
                var r = t.ajax.xmltop(n, i.xmlDoc);
                if (!r || r.tagName != n) throw"Invalid XML data";
                var a = r.getAttribute("dhx_security");
                return a && (t.security_key = a), r
            }, parse: function (e, i) {
                i = this._getXML(e, i);
                for (var n = {}, r = n.data = [], a = t.ajax.xpath("//task", i), s = 0; s < a.length; s++) r[s] = this._xmlNodeToJSON(a[s]);
                return n.collections = this._getCollections(i), n
            }, _copyLink: function (t) {
                return "<item id='" + t.id + "' source='" + t.source + "' target='" + t.target + "' type='" + t.type + "' />"
            }, _copyObject: function (t) {
                return "<task id='" + t.id + "' parent='" + (t.parent || "") + "' start_date='" + t.start_date + "' duration='" + t.duration + "' open='" + !!t.open + "' progress='" + t.progress + "' end_date='" + t.end_date + "'><![CDATA[" + t.text + "]]></task>"
            }, serialize: function () {
                for (var e = [], i = [], n = t.json.serialize(), r = 0, a = n.data.length; r < a; r++) e.push(this._copyObject(n.data[r]));
                for (r = 0, a = n.links.length; r < a; r++) i.push(this._copyLink(n.links[r]));
                return "<data>" + e.join("") + "<coll_options for='links'>" + i.join("") + "</coll_options></data>"
            }
        }, t.oldxml = {
            parse: function (e, i) {
                i = t.xml._getXML(e, i, "projects");
                for (var n = {collections: {links: []}}, r = n.data = [], a = t.ajax.xpath("//task", i), s = 0; s < a.length; s++) {
                    r[s] = t.xml._xmlNodeToJSON(a[s]);
                    var o = a[s].parentNode;
                    "project" == o.tagName ? r[s].parent = "project-" + o.getAttribute("id") : r[s].parent = o.parentNode.getAttribute("id")
                }
                a = t.ajax.xpath("//project", i);
                for (s = 0; s < a.length; s++) {
                    (l = t.xml._xmlNodeToJSON(a[s], !0)).id = "project-" + l.id, r.push(l)
                }
                for (s = 0; s < r.length; s++) {
                    var l;
                    (l = r[s]).start_date = l.startdate || l.est, l.end_date = l.enddate, l.text = l.name, l.duration = l.duration / 8, l.open = 1, l.duration || l.end_date || (l.duration = 1), l.predecessortasks && n.collections.links.push({
                        target: l.id,
                        source: l.predecessortasks,
                        type: t.config.links.finish_to_start
                    })
                }
                return n
            }, serialize: function () {
                t.message("Serialization to 'old XML' is not implemented")
            }
        }, t.serverList = function (t, e) {
            return e ? this.serverList[t] = e.slice(0) : this.serverList[t] || (this.serverList[t] = []), this.serverList[t]
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.isReadonly = function (t) {
            return (!t || !t[this.config.editable_property]) && (t && t[this.config.readonly_property] || this.config.readonly)
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = new RegExp("<(?:.|\n)*?>", "gm"), i = new RegExp(" +", "gm");

        function n(t) {
            return (t + "").replace(e, " ").replace(i, " ")
        }

        var r = new RegExp("'", "gm");

        function a(t) {
            return (t + "").replace(r, "&#39;")
        }

        for (var s in t._waiAria = {
            getAttributeString: function (t) {
                var e = [" "];
                for (var i in t) {
                    var r = a(n(t[i]));
                    e.push(i + "='" + r + "'")
                }
                return e.push(" "), e.join(" ")
            }, getTimelineCellAttr: function (e) {
                return t._waiAria.getAttributeString({"aria-label": e})
            }, _taskCommonAttr: function (e, i) {
                e.start_date && e.end_date && (i.setAttribute("aria-label", n(t.templates.tooltip_text(e.start_date, e.end_date, e))), t.isReadonly(e) && i.setAttribute("aria-readonly", !0), e.$dataprocessor_class && i.setAttribute("aria-busy", !0), i.setAttribute("aria-selected", t.getState().selected_task == e.id || t.isSelectedTask && t.isSelectedTask(e.id) ? "true" : "false"))
            }, setTaskBarAttr: function (e, i) {
                this._taskCommonAttr(e, i), !t.isReadonly(e) && t.config.drag_move && (e.id != t.getState().drag_id ? i.setAttribute("aria-grabbed", !1) : i.setAttribute("aria-grabbed", !0))
            }, taskRowAttr: function (e, i) {
                this._taskCommonAttr(e, i), !t.isReadonly(e) && t.config.order_branch && i.setAttribute("aria-grabbed", !1), i.setAttribute("role", "row"), i.setAttribute("aria-level", e.$level), t.hasChild(e.id) && i.setAttribute("aria-expanded", e.$open ? "true" : "false")
            }, linkAttr: function (e, i) {
                var r = t.config.links, a = e.type == r.finish_to_start || e.type == r.start_to_start,
                    s = e.type == r.start_to_start || e.type == r.start_to_finish,
                    o = t.locale.labels.link + " " + t.templates.drag_link(e.source, s, e.target, a);
                i.setAttribute("aria-label", n(o)), t.isReadonly(e) && i.setAttribute("aria-readonly", !0)
            }, gridSeparatorAttr: function (t) {
                t.setAttribute("role", "separator")
            }, lightboxHiddenAttr: function (t) {
                t.setAttribute("aria-hidden", "true")
            }, lightboxVisibleAttr: function (t) {
                t.setAttribute("aria-hidden", "false")
            }, lightboxAttr: function (t) {
                t.setAttribute("role", "dialog"), t.setAttribute("aria-hidden", "true"), t.firstChild.setAttribute("role", "heading")
            }, lightboxButtonAttrString: function (e) {
                return this.getAttributeString({role: "button", "aria-label": t.locale.labels[e], tabindex: "0"})
            }, lightboxHeader: function (t, e) {
                t.setAttribute("aria-label", e)
            }, lightboxSelectAttrString: function (e) {
                var i = "";
                switch (e) {
                    case"%Y":
                        i = t.locale.labels.years;
                        break;
                    case"%m":
                        i = t.locale.labels.months;
                        break;
                    case"%d":
                        i = t.locale.labels.days;
                        break;
                    case"%H:%i":
                        i = t.locale.labels.hours + t.locale.labels.minutes
                }
                return t._waiAria.getAttributeString({"aria-label": i})
            }, lightboxDurationInputAttrString: function (e) {
                return this.getAttributeString({"aria-label": t.locale.labels.column_duration, "aria-valuemin": "0"})
            }, gridAttrString: function () {
                return [" role='treegrid'", t.config.multiselect ? "aria-multiselectable='true'" : "aria-multiselectable='false'", " "].join(" ")
            }, gridScaleRowAttrString: function () {
                return "role='row'"
            }, gridScaleCellAttrString: function (e, i) {
                var n = "";
                if ("add" == e.name) n = this.getAttributeString({
                    role: "button",
                    "aria-label": t.locale.labels.new_task
                }); else {
                    var r = {role: "columnheader", "aria-label": i};
                    t._sort && t._sort.name == e.name && ("asc" == t._sort.direction ? r["aria-sort"] = "ascending" : r["aria-sort"] = "descending"), n = this.getAttributeString(r)
                }
                return n
            }, gridDataAttrString: function () {
                return "role='rowgroup'"
            }, gridCellAttrString: function (t, e) {
                return this.getAttributeString({role: "gridcell", "aria-label": e})
            }, gridAddButtonAttrString: function (e) {
                return this.getAttributeString({role: "button", "aria-label": t.locale.labels.new_task})
            }, messageButtonAttrString: function (t) {
                return "tabindex='0' role='button' aria-label='" + t + "'"
            }, messageInfoAttr: function (t) {
                t.setAttribute("role", "alert")
            }, messageModalAttr: function (t, e) {
                t.setAttribute("role", "dialog"), e && t.setAttribute("aria-labelledby", e)
            }, quickInfoAttr: function (t) {
                t.setAttribute("role", "dialog")
            }, quickInfoHeaderAttrString: function () {
                return " role='heading' "
            }, quickInfoHeader: function (t, e) {
                t.setAttribute("aria-label", e)
            }, quickInfoButtonAttrString: function (e) {
                return t._waiAria.getAttributeString({role: "button", "aria-label": e, tabindex: "0"})
            }, tooltipAttr: function (t) {
                t.setAttribute("role", "tooltip")
            }, tooltipVisibleAttr: function (t) {
                t.setAttribute("aria-hidden", "false")
            }, tooltipHiddenAttr: function (t) {
                t.setAttribute("aria-hidden", "true")
            }
        }, t._waiAria) t._waiAria[s] = function (e) {
            return function () {
                return t.config.wai_aria_attributes ? e.apply(this, arguments) : ""
            }
        }(t._waiAria[s])
    }
}, function (t, e) {
    t.exports = function (t) {
        t.getGridColumn = function (e) {
            for (var i = t.config.columns, n = 0; n < i.length; n++) if (i[n].name == e) return i[n];
            return null
        }, t.getGridColumns = function () {
            return t.config.columns.slice()
        }
    }
}, function (t, e) {
    t.exports = function (t) {
    }
}, function (t, e) {
    t.exports = function (t) {
        function e(e) {
            return function () {
                return !t.config.auto_types || t.getTaskType(t.config.types.project) != t.config.types.project || e.apply(this, arguments)
            }
        }

        function i(e) {
            t.batchUpdate(function () {
                !function e(i) {
                    !function (e) {
                        e = e.id || e;
                        var i = t.getTask(e), n = a(i);
                        !1 !== n && r(i, n)
                    }(i);
                    var n = t.getParent(i);
                    n != t.config.root_id && e(n)
                }(e)
            })
        }

        var n;

        function r(e, i) {
            e.type = i, t.updateTask(e.id)
        }

        function a(e) {
            var i = t.config.types, n = t.hasChild(e.id), r = t.getTaskType(e.type);
            return n && r === i.task ? i.project : !n && r === i.project && i.task
        }

        var s, o, l = !0;

        function c(e) {
            e != t.config.root_id && t.isTaskExists(e) && i(e)
        }

        t.attachEvent("onParse", e(function () {
            l = !1, t.batchUpdate(function () {
                t.eachTask(function (t) {
                    var e = a(t);
                    !1 !== e && r(t, e)
                })
            }), l = !0
        })), t.attachEvent("onAfterTaskAdd", function () {
            l && e(i)
        }), t.attachEvent("onAfterTaskUpdate", function () {
            l && e(i)
        }), t.attachEvent("onBeforeTaskDelete", e(function (e, i) {
            return n = t.getParent(e), !0
        })), t.attachEvent("onAfterTaskDelete", e(function (t, e) {
            c(n)
        })), t.attachEvent("onRowDragStart", e(function (e, i, n) {
            return s = t.getParent(e), !0
        })), t.attachEvent("onRowDragEnd", e(function (t, e) {
            c(s), i(t)
        })), t.attachEvent("onBeforeTaskMove", e(function (e, i, n) {
            return o = t.getParent(e), !0
        })), t.attachEvent("onAfterTaskMove", e(function (t, e, n) {
            document.querySelector(".gantt_drag_marker") || (c(o), i(t))
        }))
    }
}, function (t, e) {
    t.exports = function (t) {
        function e(e) {
            return function () {
                return !t.config.placeholder_task || e.apply(this, arguments)
            }
        }

        function i() {
            var e = t.getTaskBy("type", t.config.types.placeholder);
            if (!e.length || !t.isTaskExists(e[0].id)) {
                var i = {
                    unscheduled: !0,
                    type: t.config.types.placeholder,
                    duration: 0,
                    text: t.locale.labels.new_task
                };
                if (!1 === t.callEvent("onTaskCreated", [i])) return;
                t.addTask(i)
            }
        }

        function n(e) {
            var i = t.getTask(e);
            i.type == t.config.types.placeholder && (i.start_date && i.end_date && i.unscheduled && (i.unscheduled = !1), t.batchUpdate(function () {
                var e = t.copy(i);
                t.silent(function () {
                    t.deleteTask(i.id)
                }), delete e["!nativeeditor_status"], e.type = t.config.types.task, e.id = t.uid(), t.addTask(e)
            }))
        }

        t.config.types.placeholder = "placeholder", t.attachEvent("onDataProcessorReady", e(function (i) {
            i && !i._silencedPlaceholder && (i._silencedPlaceholder = !0, i.attachEvent("onBeforeUpdate", e(function (e, n, r) {
                return r.type != t.config.types.placeholder || (i.setUpdated(e, !1), !1)
            })))
        }));
        var r = !1;
        t.attachEvent("onGanttReady", function () {
            r || (r = !0, t.attachEvent("onAfterTaskUpdate", e(n)), t.attachEvent("onAfterTaskAdd", e(function (e, n) {
                n.type != t.config.types.placeholder && (t.getTaskBy("type", t.config.types.placeholder).forEach(function (e) {
                    t.silent(function () {
                        t.isTaskExists(e.id) && t.deleteTask(e.id)
                    })
                }), i())
            })), t.attachEvent("onParse", e(i)))
        }), t.attachEvent("onBeforeUndoStack", function (e) {
            for (var i = 0; i < e.commands.length; i++) {
                var n = e.commands[i];
                "task" === n.entity && n.value.type === t.config.types.placeholder && (e.commands.splice(i, 1), i--)
            }
            return !0
        })
    }
}, function (t, e, i) {
    var n = i(3);

    function r(t) {
        var e = {};

        function i(e, i) {
            return "function" == typeof e ? function (e) {
                var i = [];
                return t.eachTask(function (t) {
                    e(t) && i.push(t)
                }), i
            }(e) : n.isArray(i) ? r(e, i) : r(e, [i])
        }

        function r(i, r) {
            for (var a, s = r.join("_") + "_" + i, o = {}, l = 0; l < r.length; l++) o[r[l]] = !0;
            return e[s] ? a = e[s] : (a = e[s] = [], t.eachTask(function (e) {
                var r;
                e.type != t.config.types.project && (r = n.isArray(e[i]) ? e[i] : [e[i]], n.forEach(r, function (t) {
                    t && (o[t] || o[t.resource_id]) && a.push(e)
                }))
            })), a
        }

        function a(n, r, a, s) {
            var o = n.id + "_" + r + "_" + a.unit + "_" + a.step;
            return e[o] ? e[o] : e[o] = function (e, n, r, a) {
                _ = "task" == e.$role ? [] : i(n, e.id);
                for (var s = r.unit, o = {}, l = 0; l < _.length; l++) for (var c = _[l], d = t.date[s + "_start"](new Date(c.start_date)); d < c.end_date;) {
                    var h = d;
                    if (d = t.date.add(d, 1, s), t.isWorkTime({date: h, task: c, unit: s})) {
                        var u = h.valueOf();
                        o[u] || (o[u] = []), o[u].push(c)
                    }
                }
                for (var g, f, _, p = [], v = a.$getConfig(), l = 0; l < r.trace_x.length; l++) g = new Date(r.trace_x[l]), f = t.date.add(g, 1, s), ((_ = o[g.valueOf()] || []).length || v.resource_render_empty_cells) && p.push({
                    start_date: g,
                    end_date: f,
                    tasks: _
                });
                return p
            }(n, r, a, s)
        }

        function s(t, e, i, n) {
            var r = 100 * (1 - (1 * t || 0)), a = n.posFromDate(e), s = n.posFromDate(i),
                o = document.createElement("div");
            return o.className = "gantt_histogram_hor_bar", o.style.top = r + "%", o.style.left = a + "px", o.style.width = s - a + 1 + "px", o
        }

        function o(t, e, i) {
            if (t === e) return null;
            var n = 1 - Math.max(t, e), r = Math.abs(t - e), a = document.createElement("div");
            return a.className = "gantt_histogram_vert_bar", a.style.top = 100 * n + "%", a.style.height = 100 * r + "%", a.style.left = i + "px", a
        }

        function l(e, i, n) {
            var r = t.config.resource_property, a = [];
            if (t.getDatastore("task").exists(i)) {
                var s = t.getTask(i);
                a = s[r] || []
            }
            Array.isArray(a) || (a = [a]);
            for (var o = 0; o < a.length; o++) a[o].resource_id == e && n.push({
                task_id: s.id,
                resource_id: a[o].resource_id,
                value: a[o].value
            })
        }

        return t.$data.tasksStore.attachEvent("onStoreUpdated", function () {
            e = {}
        }), {
            renderLine: function (t, e) {
                for (var i = e.$getConfig(), n = e.$getTemplates(), r = a(t, i.resource_property, e.getScale(), e), s = [], o = 0; o < r.length; o++) {
                    var l = r[o], c = n.resource_cell_class(l.start_date, l.end_date, t, l.tasks),
                        d = n.resource_cell_value(l.start_date, l.end_date, t, l.tasks);
                    if (c || d) {
                        var h = e.getItemPosition(t, l.start_date, l.end_date), u = document.createElement("div");
                        u.className = ["gantt_resource_marker", c].join(" "), u.style.cssText = ["left:" + h.left + "px", "width:" + h.width + "px", "height:" + (i.row_height - 1) + "px", "line-height:" + (i.row_height - 1) + "px", "top:" + h.top + "px"].join(";"), d && (u.innerHTML = d), s.push(u)
                    }
                }
                var g = null;
                if (s.length) for (g = document.createElement("div"), o = 0; o < s.length; o++) g.appendChild(s[o]);
                return g
            }, renderHistogram: function (e, i) {
                for (var n = i.$getConfig(), r = i.$getTemplates(), l = a(e, n.resource_property, i.getScale(), i), c = [], d = {}, h = e.capacity || i.$config.capacity || 24, u = 0; u < l.length; u++) {
                    var g = l[u], f = r.histogram_cell_class(g.start_date, g.end_date, e, g.tasks),
                        _ = r.histogram_cell_label(g.start_date, g.end_date, e, g.tasks),
                        p = r.histogram_cell_allocated(g.start_date, g.end_date, e, g.tasks),
                        v = r.histogram_cell_capacity(g.start_date, g.end_date, e, g.tasks);
                    if (d[g.start_date.valueOf()] = v || 0, f || _) {
                        var m = i.getItemPosition(e, g.start_date, g.end_date), y = document.createElement("div");
                        y.className = ["gantt_histogram_cell", f].join(" "), y.style.cssText = ["left:" + m.left + "px", "width:" + m.width + "px", "height:" + (n.row_height - 1) + "px", "line-height:" + (n.row_height - 1) + "px", "top:" + (m.top + 1) + "px"].join(";"), _ && (_ = "<div class='gantt_histogram_label'>" + _ + "</div>"), p && (_ = "<div class='gantt_histogram_fill' style='height:" + 100 * Math.min(p / h || 0, 1) + "%;'></div>" + _), _ && (y.innerHTML = _), c.push(y)
                    }
                }
                var k = null;
                if (c.length) {
                    for (k = document.createElement("div"), u = 0; u < c.length; u++) k.appendChild(c[u]);
                    var b = function (e, i, n) {
                        for (var r = i.getScale(), a = document.createElement("div"), l = 0; l < r.trace_x.length; l++) {
                            var c = r.trace_x[l], d = r.trace_x[l + 1] || t.date.add(c, r.step, r.unit),
                                h = r.trace_x[l].valueOf(), u = Math.min(e[h] / n, 1) || 0;
                            if (u < 0) return null;
                            var g = Math.min(e[d.valueOf()] / n, 1) || 0, f = s(u, c, d, i);
                            f && a.appendChild(f);
                            var _ = o(u, g, i.posFromDate(d));
                            _ && a.appendChild(_)
                        }
                        return a
                    }(d, i, h);
                    b && (b.setAttribute("data-resource-id", e.id), b.style.position = "absolute", b.style.top = m.top + 1 + "px", b.style.height = n.row_height - 1 + "px", b.style.left = 0, k.appendChild(b))
                }
                return k
            }, filterTasks: i, getResourceAssignments: function (e, i) {
                var n = [], r = t.config.resource_property;
                return void 0 !== i ? l(e, i, n) : t.getTaskBy(r, e).forEach(function (t) {
                    l(e, t.id, n)
                }), n
            }
        }
    }

    t.exports = function (t) {
        var e = r(t);
        t.getTaskBy = e.filterTasks, t.getResourceAssignments = e.getResourceAssignments, t.$ui.layers.resourceRow = e.renderLine, t.$ui.layers.resourceHistogram = e.renderHistogram, t.config.resource_property = "owner_id", t.config.resource_store = "resource", t.config.resource_render_empty_cells = !1, t.templates.histogram_cell_class = function (t, e, i, n) {
        }, t.templates.histogram_cell_label = function (t, e, i, n) {
            return n.length + "/3"
        }, t.templates.histogram_cell_allocated = function (t, e, i, n) {
            return n.length / 3
        }, t.templates.histogram_cell_capacity = function (t, e, i, n) {
            return 0
        }, t.templates.resource_cell_class = function (t, e, i, n) {
            return n.length <= 1 ? "gantt_resource_marker_ok" : "gantt_resource_marker_overtime"
        }, t.templates.resource_cell_value = function (t, e, i, n) {
            return 8 * n.length
        }
    }
}, function (t, e) {
    window.dhtmlx && (window.dhtmlx.attaches || (window.dhtmlx.attaches = {}), window.dhtmlx.attaches.attachGantt = function (t, e, i) {
        var n = document.createElement("DIV");
        i = i || window.gantt, n.id = "gantt_" + i.uid(), n.style.width = "100%", n.style.height = "100%", n.cmp = "grid", document.body.appendChild(n), this.attachObject(n.id), this.dataType = "gantt", this.dataObj = i;
        var r = this.vs[this.av];
        r.grid = i, i.init(n.id, t, e), n.firstChild.style.border = "none", r.gridId = n.id, r.gridObj = n;
        return this.vs[this._viewRestore()].grid
    }), void 0 !== window.dhtmlXCellObject && (window.dhtmlXCellObject.prototype.attachGantt = function (t, e, i) {
        i = i || window.gantt;
        var n = document.createElement("DIV");
        return n.id = "gantt_" + i.uid(), n.style.width = "100%", n.style.height = "100%", n.cmp = "grid", document.body.appendChild(n), this.attachObject(n.id), this.dataType = "gantt", this.dataObj = i, i.init(n.id, t, e), n.firstChild.style.border = "none", n = null, this.callEvent("_onContentAttach", []), this.dataObj
    }), t.exports = null
}, function (t, e) {
    window.jQuery && function (t) {
        var e = [];
        t.fn.dhx_gantt = function (i) {
            if ("string" != typeof (i = i || {})) {
                var n = [];
                return this.each(function () {
                    if (this && this.getAttribute) if (this.gantt || window.gantt.$root == this) n.push("object" == typeof this.gantt ? this.gantt : window.gantt); else {
                        var t = window.gantt.$container && window.Gantt ? window.Gantt.getGanttInstance() : window.gantt;
                        for (var e in i) "data" != e && (t.config[e] = i[e]);
                        t.init(this), i.data && t.parse(i.data), n.push(t)
                    }
                }), 1 === n.length ? n[0] : n
            }
            if (e[i]) return e[i].apply(this, []);
            t.error("Method " + i + " does not exist on jQuery.dhx_gantt")
        }
    }(window.jQuery), t.exports = null
}, function (t, e) {
    t.exports = function (t) {
        var e = function (t) {
            return {
                _needRecalc: !0, reset: function () {
                    this._needRecalc = !0
                }, _isRecalcNeeded: function () {
                    return !this._isGroupSort() && this._needRecalc
                }, _isGroupSort: function () {
                    return !(!t._groups || !t._groups.is_active())
                }, _getWBSCode: function (t) {
                    return t ? (this._isRecalcNeeded() && this._calcWBS(), t.$virtual ? "" : this._isGroupSort() ? t.$wbs || "" : (t.$wbs || (this.reset(), this._calcWBS()), t.$wbs)) : ""
                }, _setWBSCode: function (t, e) {
                    t.$wbs = e
                }, getWBSCode: function (t) {
                    return this._getWBSCode(t)
                }, getByWBSCode: function (e) {
                    for (var i = e.split("."), n = t.config.root_id, r = 0; r < i.length; r++) {
                        var a = t.getChildren(n), s = 1 * i[r] - 1;
                        if (!t.isTaskExists(a[s])) return null;
                        n = a[s]
                    }
                    return t.isTaskExists(n) ? t.getTask(n) : null
                }, _calcWBS: function () {
                    if (this._isRecalcNeeded()) {
                        var e = !0;
                        t.eachTask(function (i) {
                            if (e) return e = !1, void this._setWBSCode(i, "1");
                            var n = t.getPrevSibling(i.id);
                            if (null !== n) {
                                var r = t.getTask(n).$wbs;
                                r && ((r = r.split("."))[r.length - 1]++, this._setWBSCode(i, r.join(".")))
                            } else {
                                var a = t.getParent(i.id);
                                this._setWBSCode(i, t.getTask(a).$wbs + ".1")
                            }
                        }, t.config.root_id, this), this._needRecalc = !1
                    }
                }
            }
        }(t);

        function i() {
            return e.reset(), !0
        }

        t.getWBSCode = function (t) {
            return e.getWBSCode(t)
        }, t.getTaskByWBSCode = function (t) {
            return e.getByWBSCode(t)
        }, t.attachEvent("onAfterTaskMove", i), t.attachEvent("onBeforeParse", i), t.attachEvent("onAfterTaskDelete", i), t.attachEvent("onAfterTaskAdd", i), t.attachEvent("onAfterSort", i)
    }
}, function (t, e) {
    function i(t) {
        var e = {}, i = !1;

        function n(t, i) {
            i = "function" == typeof i ? i : function () {
            }, e[t] || (e[t] = this[t], this[t] = i)
        }

        function r(t) {
            e[t] && (this[t] = e[t], e[t] = null)
        }

        function a() {
            for (var t in e) r.call(this, t)
        }

        function s(t) {
            try {
                t()
            } catch (t) {
                window.console.error(t)
            }
        }

        return t.$services.getService("state").registerProvider("batchUpdate", function () {
            return {batch_update: i}
        }, !0), function (t, e) {
            if (i) s(t); else {
                var r, o = this._dp && "off" != this._dp.updateMode;
                o && (r = this._dp.updateMode, this._dp.setUpdateMode("off"));
                var l = {}, c = {
                    render: !0,
                    refreshData: !0,
                    refreshTask: !0,
                    refreshLink: !0,
                    resetProjectDates: function (t) {
                        l[t.id] = t
                    }
                };
                for (var d in function (t) {
                    for (var e in t) n.call(this, e, t[e])
                }.call(this, c), i = !0, this.callEvent("onBeforeBatchUpdate", []), s(t), this.callEvent("onAfterBatchUpdate", []), a.call(this), l) this.resetProjectDates(l[d]);
                i = !1, e || this.render(), o && (this._dp.setUpdateMode(r), this._dp.setGanttMode("tasks"), this._dp.sendData(), this._dp.setGanttMode("links"), this._dp.sendData())
            }
        }
    }

    t.exports = function (t) {
        t.batchUpdate = i(t)
    }
}, function (t, e, i) {
    var n = i(1);
    t.exports = function (t) {
        var e = 50, i = 30, r = 10, a = 50, s = null, o = !1, l = null, c = {started: !1}, d = {};

        function h() {
            var e = !!document.querySelector(".gantt_drag_marker"),
                i = !!document.querySelector(".gantt_drag_marker.gantt_grid_resize_area"),
                n = !!document.querySelector(".gantt_link_direction");
            return o = e && !i && !n, !(!t.getState().drag_mode && !e || i)
        }

        function u(e) {
            if (l && (clearTimeout(l), l = null), e) {
                var i = t.config.autoscroll_speed;
                i && i < 10 && (i = 10), l = setTimeout(function () {
                    s = setInterval(_, i || a)
                }, t.config.autoscroll_delay || r)
            }
        }

        function g(t) {
            t ? (u(!0), c.started || (c.x = d.x, c.y = d.y, c.started = !0)) : (s && (clearInterval(s), s = null), u(!1), c.started = !1)
        }

        function f(e) {
            var i = h();
            if (!s && !l || i || g(!1), !t.config.autoscroll || !i) return !1;
            d = {x: e.clientX, y: e.clientY}, !s && i && g(!0)
        }

        function _() {
            if (!h()) return g(!1), !1;
            var e = n.getNodePosition(t.$task || t.$grid || t.$root), r = d.x - e.x, a = d.y - e.y,
                s = o ? 0 : p(r, e.width, c.x - e.x), l = p(a, e.height, c.y - e.y), u = t.getScrollState(), f = u.y,
                _ = u.inner_height, v = u.height, m = u.x, y = u.inner_width, k = u.width;
            l && !_ ? l = 0 : l < 0 && !f ? l = 0 : l > 0 && f + _ >= v + 2 && (l = 0), s && !y ? s = 0 : s < 0 && !m ? s = 0 : s > 0 && m + y >= k && (s = 0);
            var b = t.config.autoscroll_step;
            b && b < 2 && (b = 2), s *= b || i, l *= b || i, (s || l) && function (e, i) {
                var n = t.getScrollState(), r = null, a = null;
                e && (r = n.x + e, r = Math.min(n.width, r), r = Math.max(0, r));
                i && (a = n.y + i, a = Math.min(n.height, a), a = Math.max(0, a));
                t.scrollTo(r, a)
            }(s, l)
        }

        function p(t, i, n) {
            return t - e < 0 && t < n ? -1 : t > i - e && t > n ? 1 : 0
        }

        t.attachEvent("onGanttReady", function () {
            t.eventRemove(document.body, "mousemove", f), t.event(document.body, "mousemove", f)
        })
    }
}, function (t, e, i) {
    t.exports = function (t) {
        for (var e = [i(71), i(70), i(69), i(68), i(67), i(66), i(65), i(64)], n = 0; n < e.length; n++) e[n] && e[n](t)
    }
}, function (t, e, i) {
    var n = i(0), r = i(4), a = function (t) {
        return this.serverProcessor = t, this.action_param = "!nativeeditor_status", this.object = null, this.updatedRows = [], this.autoUpdate = !0, this.updateMode = "cell", this._tMode = "GET", this._headers = null, this._payload = null, this.post_delim = "_", this._waitMode = 0, this._in_progress = {}, this._invalid = {}, this.mandatoryFields = [], this.messages = [], this.styles = {
            updated: "font-weight:bold;",
            inserted: "font-weight:bold;",
            deleted: "text-decoration : line-through;",
            invalid: "background-color:FFE0E0;",
            invalid_cell: "border-bottom:2px solid red;",
            error: "color:red;",
            clear: "font-weight:normal;text-decoration:none;"
        }, this.enableUTFencoding(!0), r(this), this
    };
    a.prototype = {
        setTransactionMode: function (t, e) {
            "object" == typeof t ? (this._tMode = t.mode || this._tMode, n.defined(t.headers) && (this._headers = t.headers), n.defined(t.payload) && (this._payload = t.payload)) : (this._tMode = t, this._tSend = e), "REST" == this._tMode && (this._tSend = !1, this._endnm = !0), "JSON" != this._tMode && "REST-JSON" != this._tMode || (this._tSend = !1, this._endnm = !0, this._serialize_as_json = !0, this._headers = this._headers || {}, this._headers["Content-type"] = "application/json")
        }, escape: function (t) {
            return this._utf ? encodeURIComponent(t) : escape(t)
        }, enableUTFencoding: function (t) {
            this._utf = !!t
        }, setDataColumns: function (t) {
            this._columns = "string" == typeof t ? t.split(",") : t
        }, getSyncState: function () {
            return !this.updatedRows.length
        }, enableDataNames: function (t) {
            this._endnm = !!t
        }, enablePartialDataSend: function (t) {
            this._changed = !!t
        }, setUpdateMode: function (t, e) {
            this.autoUpdate = "cell" == t, this.updateMode = t, this.dnd = e
        }, ignore: function (t, e) {
            this._silent_mode = !0, t.call(e || window), this._silent_mode = !1
        }, setUpdated: function (t, e, i) {
            if (!this._silent_mode) {
                var n = this.findRow(t);
                i = i || "updated";
                var r = this.obj.getUserData(t, this.action_param);
                r && "updated" == i && (i = r), e ? (this.set_invalid(t, !1), this.updatedRows[n] = t, this.obj.setUserData(t, this.action_param, i), this._in_progress[t] && (this._in_progress[t] = "wait")) : this.is_invalid(t) || (this.updatedRows.splice(n, 1), this.obj.setUserData(t, this.action_param, "")), e || this._clearUpdateFlag(t), this.markRow(t, e, i), e && this.autoUpdate && this.sendData(t)
            }
        }, _clearUpdateFlag: function (t) {
        }, markRow: function (t, e, i) {
            var n = "", r = this.is_invalid(t);
            if (r && (n = this.styles[r], e = !0), this.callEvent("onRowMark", [t, e, i, r]) && (n = this.styles[e ? i : "clear"] + n, this.obj[this._methods[0]](t, n), r && r.details)) {
                n += this.styles[r + "_cell"];
                for (var a = 0; a < r.details.length; a++) r.details[a] && this.obj[this._methods[1]](t, a, n)
            }
        }, getState: function (t) {
            return this.obj.getUserData(t, this.action_param)
        }, is_invalid: function (t) {
            return this._invalid[t]
        }, set_invalid: function (t, e, i) {
            i && (e = {
                value: e, details: i, toString: function () {
                    return this.value.toString()
                }
            }), this._invalid[t] = e
        }, checkBeforeUpdate: function (t) {
            return !0
        }, sendData: function (t) {
            if (!this._waitMode || "tree" != this.obj.mytype && !this.obj._h2) {
                if (this.obj.editStop && this.obj.editStop(), void 0 === t || this._tSend) return this.sendAllData();
                if (this._in_progress[t]) return !1;
                if (this.messages = [], !this.checkBeforeUpdate(t) && this.callEvent("onValidationError", [t, this.messages])) return !1;
                this._beforeSendData(this._getRowData(t), t)
            }
        }, _beforeSendData: function (t, e) {
            if (!this.callEvent("onBeforeUpdate", [e, this.getState(e), t])) return !1;
            this._sendData(t, e)
        }, serialize: function (t, e) {
            if (this._serialize_as_json) return this._serializeAsJSON(t);
            if ("string" == typeof t) return t;
            if (void 0 !== e) return this.serialize_one(t, "");
            var i = [], n = [];
            for (var r in t) t.hasOwnProperty(r) && (i.push(this.serialize_one(t[r], r + this.post_delim)), n.push(r));
            return i.push("ids=" + this.escape(n.join(","))), this.$gantt.security_key && i.push("dhx_security=" + this.$gantt.security_key), i.join("&")
        }, _serializeAsJSON: function (t) {
            if ("string" == typeof t) return t;
            var e = n.copy(t);
            return "REST-JSON" == this._tMode && (delete e.id, delete e[this.action_param]), JSON.stringify(e)
        }, serialize_one: function (t, e) {
            if ("string" == typeof t) return t;
            var i = [], n = "";
            for (var r in t) if (t.hasOwnProperty(r)) {
                if (("id" == r || r == this.action_param) && "REST" == this._tMode) continue;
                n = "string" == typeof t[r] || "number" == typeof t[r] ? t[r] : JSON.stringify(t[r]), i.push(this.escape((e || "") + r) + "=" + this.escape(n))
            }
            return i.join("&")
        }, _applyPayload: function (t) {
            var e = this.$gantt.ajax;
            if (this._payload) for (var i in this._payload) t = t + e.urlSeparator(t) + this.escape(i) + "=" + this.escape(this._payload[i]);
            return t
        }, _sendData: function (t, e) {
            if (t) {
                if (!this.callEvent("onBeforeDataSending", e ? [e, this.getState(e), t] : [null, null, t])) return !1;
                e && (this._in_progress[e] = (new Date).valueOf());
                var i = this, n = this.$gantt.ajax, r = {
                        callback: function (n) {
                            var r, a = [];
                            if (e) a.push(e); else if (t) for (r in t) a.push(r);
                            return i.afterUpdate(i, n, a)
                        }, headers: this._headers
                    },
                    a = this.serverProcessor + (this._user ? n.urlSeparator(this.serverProcessor) + ["dhx_user=" + this._user, "dhx_version=" + this.obj.getUserData(0, "version")].join("&") : ""),
                    s = this._applyPayload(a);
                switch (this._tMode) {
                    case"GET":
                        r.url = s + n.urlSeparator(s) + this.serialize(t, e), r.method = "GET";
                        break;
                    case"POST":
                        r.url = s, r.method = "POST", r.data = this.serialize(t, e);
                        break;
                    case"JSON":
                        var o = {};
                        for (var l in t) l !== this.action_param && "id" !== l && "gr_id" !== l && (o[l] = t[l]);
                        r.url = s, r.method = "POST", r.data = JSON.stringify({
                            id: e,
                            action: t[this.action_param],
                            data: o
                        });
                        break;
                    case"REST":
                    case"REST-JSON":
                        var c = a.replace(/(&|\?)editing=true/, "");
                        switch (o = "", this.getState(e)) {
                            case"inserted":
                                r.method = "POST", r.data = this.serialize(t, e);
                                break;
                            case"deleted":
                                r.method = "DELETE", c = c + ("/" == c.slice(-1) ? "" : "/") + e;
                                break;
                            default:
                                r.method = "PUT", r.data = this.serialize(t, e), c = c + ("/" == c.slice(-1) ? "" : "/") + e
                        }
                        r.url = this._applyPayload(c)
                }
                n.query(r), this._waitMode++
            }
        }, _forEachUpdatedRow: function (t) {
            for (var e = this.updatedRows.slice(), i = 0; i < e.length; i++) {
                var n = e[i];
                this.obj.getUserData(n, this.action_param) && t.call(this, n)
            }
        }, sendAllData: function () {
            if (this.updatedRows.length) {
                this.messages = [];
                var t = !0;
                if (this._forEachUpdatedRow(function (e) {
                    t &= this.checkBeforeUpdate(e)
                }), !t && !this.callEvent("onValidationError", ["", this.messages])) return !1;
                if (this._tSend) this._sendData(this._getAllData()); else {
                    var e = !1;
                    this._forEachUpdatedRow(function (t) {
                        if (!e && !this._in_progress[t]) {
                            if (this.is_invalid(t)) return;
                            this._beforeSendData(this._getRowData(t), t), this._waitMode && ("tree" == this.obj.mytype || this.obj._h2) && (e = !0)
                        }
                    })
                }
            }
        }, _getAllData: function (t) {
            var e = {}, i = !1;
            return this._forEachUpdatedRow(function (t) {
                if (!this._in_progress[t] && !this.is_invalid(t)) {
                    var n = this._getRowData(t);
                    this.callEvent("onBeforeUpdate", [t, this.getState(t), n]) && (e[t] = n, i = !0, this._in_progress[t] = (new Date).valueOf())
                }
            }), i ? e : null
        }, setVerificator: function (t, e) {
            this.mandatoryFields[t] = e || function (t) {
                return "" !== t
            }
        }, clearVerificator: function (t) {
            this.mandatoryFields[t] = !1
        }, findRow: function (t) {
            var e = 0;
            for (e = 0; e < this.updatedRows.length && t != this.updatedRows[e]; e++) ;
            return e
        }, defineAction: function (t, e) {
            this._uActions || (this._uActions = []), this._uActions[t] = e
        }, afterUpdateCallback: function (t, e, i, n) {
            var r = t, a = "error" != i && "invalid" != i;
            if (a || this.set_invalid(t, i), this._uActions && this._uActions[i] && !this._uActions[i](n)) return delete this._in_progress[r];
            "wait" != this._in_progress[r] && this.setUpdated(t, !1);
            var s = t;
            switch (i) {
                case"inserted":
                case"insert":
                    e != t && (this.setUpdated(t, !1), this.obj[this._methods[2]](t, e), t = e);
                    break;
                case"delete":
                case"deleted":
                    return this.obj.setUserData(t, this.action_param, "true_deleted"), this.obj[this._methods[3]](t), delete this._in_progress[r], this.callEvent("onAfterUpdate", [t, i, e, n])
            }
            "wait" != this._in_progress[r] ? (a && this.obj.setUserData(t, this.action_param, ""), delete this._in_progress[r]) : (delete this._in_progress[r], this.setUpdated(e, !0, this.obj.getUserData(t, this.action_param))), this.callEvent("onAfterUpdate", [s, i, e, n])
        }, afterUpdate: function (t, e, i) {
            var n = this.$gantt.ajax;
            if (window.JSON) {
                var r;
                try {
                    r = JSON.parse(e.xmlDoc.responseText)
                } catch (t) {
                    e.xmlDoc.responseText.length || (r = {})
                }
                if (r) {
                    var a = r.action || this.getState(i) || "updated", s = r.sid || i[0], o = r.tid || i[0];
                    return t.afterUpdateCallback(s, o, a, r), void t.finalizeUpdate()
                }
            }
            var l = n.xmltop("data", e.xmlDoc);
            if (!l) return this.cleanUpdate(i);
            var c = n.xpath("//data/action", l);
            if (!c.length) return this.cleanUpdate(i);
            for (var d = 0; d < c.length; d++) {
                var h = c[d];
                a = h.getAttribute("type"), s = h.getAttribute("sid"), o = h.getAttribute("tid");
                t.afterUpdateCallback(s, o, a, h)
            }
            t.finalizeUpdate()
        }, cleanUpdate: function (t) {
            if (t) for (var e = 0; e < t.length; e++) delete this._in_progress[t[e]]
        }, finalizeUpdate: function () {
            this._waitMode && this._waitMode--, ("tree" == this.obj.mytype || this.obj._h2) && this.updatedRows.length && this.sendData(), this.callEvent("onAfterUpdateFinish", []), this.updatedRows.length || this.callEvent("onFullSync", [])
        }, init: function (t) {
            this.obj = t, this.obj._dp_init && this.obj._dp_init(this)
        }, setOnAfterUpdate: function (t) {
            this.attachEvent("onAfterUpdate", t)
        }, enableDebug: function (t) {
        }, setOnBeforeUpdateHandler: function (t) {
            this.attachEvent("onBeforeDataSending", t)
        }, setAutoUpdate: function (t, e) {
            t = t || 2e3, this._user = e || (new Date).valueOf(), this._need_update = !1, this._update_busy = !1, this.attachEvent("onAfterUpdate", function (t, e, i, n) {
                this.afterAutoUpdate(t, e, i, n)
            }), this.attachEvent("onFullSync", function () {
                this.fullSync()
            });
            var i = this;
            window.setInterval(function () {
                i.loadUpdate()
            }, t)
        }, afterAutoUpdate: function (t, e, i, n) {
            return "collision" != e || (this._need_update = !0, !1)
        }, fullSync: function () {
            return this._need_update && (this._need_update = !1, this.loadUpdate()), !0
        }, getUpdates: function (t, e) {
            var i = this.$gantt.ajax;
            if (this._update_busy) return !1;
            this._update_busy = !0, i.get(t, e)
        }, _v: function (t) {
            return t.firstChild ? t.firstChild.nodeValue : ""
        }, _a: function (t) {
            for (var e = [], i = 0; i < t.length; i++) e[i] = this._v(t[i]);
            return e
        }, loadUpdate: function () {
            var t = this.$gantt.ajax, e = this, i = this.obj.getUserData(0, "version"),
                n = this.serverProcessor + t.urlSeparator(this.serverProcessor) + ["dhx_user=" + this._user, "dhx_version=" + i].join("&");
            n = n.replace("editing=true&", ""), this.getUpdates(n, function (i) {
                var n = t.xpath("//userdata", i);
                e.obj.setUserData(0, "version", e._v(n[0]));
                var r = t.xpath("//update", i);
                if (r.length) {
                    e._silent_mode = !0;
                    for (var a = 0; a < r.length; a++) {
                        var s = r[a].getAttribute("status"), o = r[a].getAttribute("id"),
                            l = r[a].getAttribute("parent");
                        switch (s) {
                            case"inserted":
                                e.callEvent("insertCallback", [r[a], o, l]);
                                break;
                            case"updated":
                                e.callEvent("updateCallback", [r[a], o, l]);
                                break;
                            case"deleted":
                                e.callEvent("deleteCallback", [r[a], o, l])
                        }
                    }
                    e._silent_mode = !1
                }
                e._update_busy = !1, e = null
            })
        }, destructor: function () {
            this.callEvent("onDestroy", []), this.detachAllEvents(), this.updatedRows = [], this._in_progress = {}, this._invalid = {}, this._headers = null, this._payload = null, this.obj = null
        }
    }, t.exports = a
}, function (t, e, i) {
    var n = i(0);

    function r(t, e) {
        var i = t.data || this.xml._xmlNodeToJSON(t.firstChild);
        if (this.isTaskExists(e)) {
            var n = this.getTask(e);
            for (var r in i) {
                var a = i[r];
                switch (r) {
                    case"id":
                        continue;
                    case"start_date":
                    case"end_date":
                        a = this.templates.xml_date(a);
                        break;
                    case"duration":
                        n.end_date = this.calculateEndDate({start_date: n.start_date, duration: a, task: n})
                }
                n[r] = a
            }
            this.updateTask(e), this.refreshData()
        }
    }

    function a(t, e, i, n) {
        var r = t.data || this.xml._xmlNodeToJSON(t.firstChild), a = {add: this.addTask, isExist: this.isTaskExists};
        "links" == n && (a.add = this.addLink, a.isExist = this.isLinkExists), a.isExist.call(this, e) || (r.id = e, a.add.call(this, r))
    }

    function s(t, e, i, n) {
        var r = {delete: this.deleteTask, isExist: this.isTaskExists};
        "links" == n && (r.delete = this.deleteLink, r.isExist = this.isLinkExists), r.isExist.call(this, e) && r.delete.call(this, e)
    }

    t.exports = function (t, e) {
        e.attachEvent("insertCallback", n.bind(a, t)), e.attachEvent("updateCallback", n.bind(r, t)), e.attachEvent("deleteCallback", n.bind(s, t))
    }
}, function (t, e, i) {
    var n = i(74), r = i(3);
    t.exports = function (t) {
        var e;
        t.dataProcessor = i(73);
        var a = [];
        t._dp_init = function (s) {
            t.assert(!this._dp, "The dataProcessor is already attached to this gantt instance"), s.setTransactionMode("POST", !0), s.serverProcessor += (-1 != s.serverProcessor.indexOf("?") ? "&" : "?") + "editing=true", s._serverProcessor = s.serverProcessor, s.$gantt = this, s.styles = {
                updated: "gantt_updated",
                order: "gantt_updated",
                inserted: "gantt_inserted",
                deleted: "gantt_deleted",
                invalid: "gantt_invalid",
                error: "gantt_error",
                clear: ""
            }, s._methods = ["_row_style", "setCellTextStyle", "_change_id", "_delete_task"], function (t, i) {
                i.setGanttMode = function (t) {
                    var e = i.modes || {};
                    i._ganttMode && (e[i._ganttMode] = {
                        _in_progress: i._in_progress,
                        _invalid: i._invalid,
                        updatedRows: i.updatedRows
                    });
                    var n = e[t];
                    n || (n = e[t] = {
                        _in_progress: {},
                        _invalid: {},
                        updatedRows: []
                    }), i._in_progress = n._in_progress, i._invalid = n._invalid, i.updatedRows = n.updatedRows, i.modes = e, i._ganttMode = t
                }, e = i.afterUpdate, i.afterUpdate = function () {
                    var t;
                    t = 3 == arguments.length ? arguments[1] : arguments[4];
                    var n = i._ganttMode, r = t.filePath;
                    n = "REST" != this._tMode && "REST-JSON" != this._tMode ? -1 != r.indexOf("gantt_mode=links") ? "links" : "tasks" : r.indexOf("/link") > r.indexOf("/task") ? "links" : "tasks", i.setGanttMode(n);
                    var a = e.apply(i, arguments);
                    return i.setGanttMode(n), a
                }, i._getRowData = t.bind(function (e, n) {
                    var a;
                    a = "tasks" == i._ganttMode ? this.isTaskExists(e) ? this.getTask(e) : {id: e} : this.isLinkExists(e) ? this.getLink(e) : {id: e}, a = t.copy(a);
                    var s = {};
                    for (var o in a) if ("$" != o.substr(0, 1)) {
                        var l = a[o];
                        r.isDate(l) ? s[o] = this.templates.xml_format(l) : s[o] = null === l ? "" : l
                    }
                    var c = this._get_task_timing_mode(a);
                    return c.$no_start && (a.start_date = "", a.duration = ""), c.$no_end && (a.end_date = "", a.duration = ""), s[i.action_param] = this.getUserData(e, i.action_param), s
                }, t)
            }.call(this, t, s), function (t, e) {
                t._change_id = t.bind(function (t, i) {
                    "tasks" != e._ganttMode ? this.changeLinkId(t, i) : this.changeTaskId(t, i)
                }, this), t._row_style = function (i, n) {
                    "tasks" == e._ganttMode && t.isTaskExists(i) && (t.getTask(i).$dataprocessor_class = n, t.refreshTask(i))
                }, t._delete_task = function (t, e) {
                }, t._sendTaskOrder = function (t, i) {
                    i.$drop_target && (e.setGanttMode("tasks"), this.getTask(t).target = i.$drop_target, e.setUpdated(t, !0, "order"), delete this.getTask(t).$drop_target)
                }, this._dp = e
            }.call(this, t, s), function (t, e) {
                function n(i) {
                    for (var n = e.updatedRows.slice(), r = !1, a = 0; a < n.length && !e._in_progress[i]; a++) n[a] == i && ("inserted" == t.getUserData(i, "!nativeeditor_status") && (r = !0), e.setUpdated(i, !1));
                    return r
                }

                a.push(this.attachEvent("onAfterTaskAdd", function (i, n) {
                    t.isTaskExists(i) && (e.setGanttMode("tasks"), e.setUpdated(i, !0, "inserted"))
                })), a.push(this.attachEvent("onAfterTaskUpdate", function (i, n) {
                    t.isTaskExists(i) && (e.setGanttMode("tasks"), e.setUpdated(i, !0), t._sendTaskOrder(i, n))
                }));
                var r = i(17), s = {};
                a.push(this.attachEvent("onBeforeTaskDelete", function (e, i) {
                    return !t.config.cascade_delete || (s[e] = {
                        tasks: r.getSubtreeTasks(t, e),
                        links: r.getSubtreeLinks(t, e)
                    }, !0)
                })), a.push(this.attachEvent("onAfterTaskDelete", function (i, r) {
                    if (e.setGanttMode("tasks"), !n(i)) {
                        if (t.config.cascade_delete && s[i]) {
                            var a = e.updateMode;
                            e.setUpdateMode("off");
                            var o = s[i];
                            for (var l in o.tasks) n(l) || e.setUpdated(l, !0, "deleted");
                            for (var l in e.setGanttMode("links"), o.links) n(l) || e.setUpdated(l, !0, "deleted");
                            s[i] = null, "off" != a && e.sendAllData(), e.setGanttMode("tasks"), e.setUpdateMode(a)
                        }
                        e.setUpdated(i, !0, "deleted"), "off" == e.updateMode || e._tSend || e.sendAllData()
                    }
                })), a.push(this.attachEvent("onAfterLinkUpdate", function (i, n) {
                    t.isLinkExists(i) && (e.setGanttMode("links"), e.setUpdated(i, !0))
                })), a.push(this.attachEvent("onAfterLinkAdd", function (i, n) {
                    t.isLinkExists(i) && (e.setGanttMode("links"), e.setUpdated(i, !0, "inserted"))
                })), a.push(this.attachEvent("onAfterLinkDelete", function (t, i) {
                    e.setGanttMode("links"), !n(t) && e.setUpdated(t, !0, "deleted")
                })), a.push(this.attachEvent("onRowDragEnd", function (e, i) {
                    t._sendTaskOrder(e, t.getTask(e))
                }));
                var o = null, l = null;
                a.push(this.attachEvent("onTaskIdChange", function (i, n) {
                    if (e._waitMode) {
                        var r = t.getChildren(n);
                        if (r.length) {
                            o = o || {};
                            for (var a = 0; a < r.length; a++) {
                                var s = this.getTask(r[a]);
                                o[s.id] = s
                            }
                        }
                        var c = function (t) {
                            var e = [];
                            return t.$source && (e = e.concat(t.$source)), t.$target && (e = e.concat(t.$target)), e
                        }(this.getTask(n));
                        if (c.length) for (l = l || {}, a = 0; a < c.length; a++) {
                            var d = this.getLink(c[a]);
                            l[d.id] = d
                        }
                    }
                })), e.attachEvent("onAfterUpdateFinish", function () {
                    (o || l) && (t.batchUpdate(function () {
                        for (var e in o) t.updateTask(o[e].id);
                        for (var e in l) t.updateLink(l[e].id);
                        o = null, l = null
                    }), o ? t._dp.setGanttMode("tasks") : t._dp.setGanttMode("links"))
                }), e.attachEvent("onBeforeDataSending", function () {
                    var e = this._serverProcessor;
                    if ("REST-JSON" == this._tMode || "REST" == this._tMode) {
                        var i = this._ganttMode.substr(0, this._ganttMode.length - 1);
                        e = e.substring(0, e.indexOf("?") > -1 ? e.indexOf("?") : e.length), this.serverProcessor = e + ("/" == e.slice(-1) ? "" : "/") + i
                    } else this.serverProcessor = e + t.ajax.urlSeparator(e) + "gantt_mode=" + this._ganttMode;
                    return !0
                })
            }.call(this, t, s), s.attachEvent("onDestroy", function () {
                !function (t, i) {
                    delete i.$gantt, delete i.setGanttMode, delete i._getRowData, i.afterUpdate = e, delete t._dp, delete t._change_id, delete t._row_style, delete t._delete_task, delete t._sendTaskOrder, r.forEach(a, function (e) {
                        t.detachEvent(e)
                    }), a = []
                }(t, s)
            }), n(t, s), t.callEvent("onDataProcessorReady", [s])
        }, t.getUserData = function (t, e) {
            return this.userdata || (this.userdata = {}), this.userdata[t] && this.userdata[t][e] ? this.userdata[t][e] : ""
        }, t.setUserData = function (t, e, i) {
            this.userdata || (this.userdata = {}), this.userdata[t] || (this.userdata[t] = {}), this.userdata[t][e] = i
        }
    }
}, function (t, e) {
    t.exports = {
        bindDataStore: function (t, e) {
            var i = e.getDatastore(t), n = function (t, e) {
                var n = e.getLayers(), r = i.getItem(t);
                if (r && i.isVisible(t)) for (var a = 0; a < n.length; a++) n[a].render_item(r)
            }, r = function (t) {
                for (var e = t.getLayers(), n = 0; n < e.length; n++) e[n].clear();
                var r = i.getVisibleItems();
                for (n = 0; n < e.length; n++) e[n].render_items(r)
            };

            function a(t) {
                return !!t.$services.getService("state").getState("batchUpdate").batch_update
            }

            i.attachEvent("onStoreUpdated", function (s, o, l) {
                if (!a(e)) {
                    var c = e.$services.getService("layers").getDataRender(t);
                    c && (s && "move" != l && "delete" != l ? (i.callEvent("onBeforeRefreshItem", [o.id]), n(o.id, c), i.callEvent("onAfterRefreshItem", [o.id])) : (i.callEvent("onBeforeRefreshAll", []), r(c), i.callEvent("onAfterRefreshAll", [])))
                }
            }), i.attachEvent("onItemOpen", function () {
                e.render()
            }), i.attachEvent("onItemClose", function () {
                e.render()
            }), i.attachEvent("onIdChange", function (r, s) {
                if (i.callEvent("onBeforeIdChange", [r, s]), !a(e)) {
                    var o = e.$services.getService("layers").getDataRender(t);
                    !function (t, e, i, n) {
                        for (var r = 0; r < t.length; r++) t[r].change_id(e, i)
                    }(o.getLayers(), r, s, i.getItem(s)), n(s, o)
                }
            })
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = null, i = t._removeItemInner;

        function n(t) {
            e = null, this.callEvent("onAfterUnselect", [t])
        }

        return t._removeItemInner = function (t) {
            return e == t && n.call(this, t), e && this.eachItem && this.eachItem(function (t) {
                t.id == e && n.call(this, t.id)
            }, t), i.apply(this, arguments)
        }, t.attachEvent("onIdChange", function (e, i) {
            t.getSelectedId() == e && t.silent(function () {
                t.unselect(e), t.select(i)
            })
        }), {
            select: function (t) {
                if (t) {
                    if (e == t) return e;
                    if (!this._skip_refresh && !this.callEvent("onBeforeSelect", [t])) return !1;
                    this.unselect(), e = t, this._skip_refresh || (this.refresh(t), this.callEvent("onAfterSelect", [t]))
                }
                return e
            }, getSelectedId: function () {
                return e
            }, unselect: function (t) {
                (t = t || e) && (e = null, this._skip_refresh || (this.refresh(t), n.call(this, t)))
            }
        }
    }
}, function (t, e, i) {
    var n = i(19), r = i(0), a = i(20), s = function (t) {
        return a.apply(this, [t]), this._branches = {}, this.pull = {}, this.$initItem = t.initItem, this.$parentProperty = t.parentProperty || "parent", "function" != typeof t.rootId ? this.$getRootId = function (t) {
            return function () {
                return t
            }
        }(t.rootId || 0) : this.$getRootId = t.rootId, this.$openInitially = t.openInitially, this.visibleOrder = n.$create(), this.fullOrder = n.$create(), this._searchVisibleOrder = {}, this._skip_refresh = !1, this.attachEvent("onFilterItem", function (t, e) {
            var i = !0;
            return this.eachParent(function (t) {
                i = i && t.$open && !this._isSplitItem(t)
            }, e), !!i
        }), this
    };
    s.prototype = r.mixin({
        _buildTree: function (t) {
            for (var e = null, i = this.$getRootId(), n = 0, a = t.length; n < a; n++) e = t[n], this.setParent(e, this.getParent(e) || i);
            for (n = 0, a = t.length; n < a; n++) e = t[n], this._add_branch(e), e.$level = this.calculateItemLevel(e), r.defined(e.$open) || (e.$open = r.defined(e.open) ? e.open : this.$openInitially());
            this._updateOrder()
        }, _isSplitItem: function (t) {
            return "split" == t.render && this.hasChild(t.id)
        }, parse: function (t) {
            this.callEvent("onBeforeParse", [t]);
            var e = this._parseInner(t);
            this._buildTree(e), this.filter(), this.callEvent("onParse", [e])
        }, _addItemInner: function (t, e) {
            var i = this.getParent(t);
            r.defined(i) || (i = this.$getRootId(), this.setParent(t, i));
            var n = this.getIndexById(i) + Math.min(Math.max(e, 0), this.visibleOrder.length);
            1 * n !== n && (n = void 0), a.prototype._addItemInner.call(this, t, n), this.setParent(t, i), t.hasOwnProperty("$rendered_parent") && this._move_branch(t, t.$rendered_parent), this._add_branch(t, e)
        }, _changeIdInner: function (t, e) {
            var i = this.getChildren(t), n = this._searchVisibleOrder[t];
            a.prototype._changeIdInner.call(this, t, e);
            var r = this.getParent(e);
            this._replace_branch_child(r, t, e);
            for (var s = 0; s < i.length; s++) this.setParent(this.getItem(i[s]), e);
            this._searchVisibleOrder[e] = n, delete this._branches[t]
        }, _traverseBranches: function (t, e) {
            e = e || this.$getRootId();
            var i = this._branches[e];
            if (i) for (var n = 0; n < i.length; n++) {
                var r = i[n];
                t.call(this, r), this._branches[r] && this._traverseBranches(t, r)
            }
        }, _updateOrder: function (t) {
            this.fullOrder = n.$create(), this._traverseBranches(function (t) {
                this.fullOrder.push(t)
            }), t && a.prototype._updateOrder.call(this, t)
        }, _removeItemInner: function (t) {
            var e = [];
            this.eachItem(function (t) {
                e.push(t)
            }, t), e.push(this.getItem(t));
            for (var i = 0; i < e.length; i++) this._move_branch(e[i], this.getParent(e[i]), null), a.prototype._removeItemInner.call(this, e[i].id), this._move_branch(e[i], this.getParent(e[i]), null)
        }, move: function (t, e, i) {
            var n = arguments[3];
            if (n) {
                if (n === t) return;
                i = this.getParent(n), e = this.getBranchIndex(n)
            }
            if (t != i) {
                i = i || this.$getRootId();
                var r = this.getItem(t), a = this.getParent(r.id), s = this.getChildren(i);
                if (-1 == e && (e = s.length + 1), a == i) if (this.getBranchIndex(t) == e) return;
                if (!1 !== this.callEvent("onBeforeItemMove", [t, i, e])) {
                    this._replace_branch_child(a, t), (s = this.getChildren(i))[e] ? s = s.slice(0, e).concat([t]).concat(s.slice(e)) : s.push(t), this.setParent(r, i), this._branches[i] = s;
                    var o = this.calculateItemLevel(r) - r.$level;
                    r.$level += o, this.eachItem(function (t) {
                        t.$level += o
                    }, r.id, this), this._moveInner(this.getIndexById(t), this.getIndexById(i) + e), this.callEvent("onAfterItemMove", [t, i, e]), this.refresh()
                }
            }
        }, getBranchIndex: function (t) {
            for (var e = this.getChildren(this.getParent(t)), i = 0; i < e.length; i++) if (e[i] == t) return i;
            return -1
        }, hasChild: function (t) {
            return r.defined(this._branches[t]) && this._branches[t].length
        }, getChildren: function (t) {
            return r.defined(this._branches[t]) ? this._branches[t] : n.$create()
        }, isChildOf: function (t, e) {
            if (!this.exists(t)) return !1;
            if (e === this.$getRootId()) return !0;
            if (!this.hasChild(e)) return !1;
            var i = this.getItem(t), n = this.getParent(t);
            if (this.getItem(e).$level >= i.$level) return !1;
            for (; i && this.exists(n);) {
                if ((i = this.getItem(n)) && i.id == e) return !0;
                n = this.getParent(i)
            }
            return !1
        }, getSiblings: function (t) {
            if (!this.exists(t)) return n.$create();
            var e = this.getParent(t);
            return this.getChildren(e)
        }, getNextSibling: function (t) {
            for (var e = this.getSiblings(t), i = 0, n = e.length; i < n; i++) if (e[i] == t) return e[i + 1] || null;
            return null
        }, getPrevSibling: function (t) {
            for (var e = this.getSiblings(t), i = 0, n = e.length; i < n; i++) if (e[i] == t) return e[i - 1] || null;
            return null
        }, getParent: function (t) {
            var e = null;
            return (e = void 0 !== t.id ? t : this.getItem(t)) ? e[this.$parentProperty] : this.$getRootId()
        }, clearAll: function () {
            this._branches = {}, a.prototype.clearAll.call(this)
        }, calculateItemLevel: function (t) {
            var e = 0;
            return this.eachParent(function () {
                e++
            }, t), e
        }, _setParentInner: function (t, e, i) {
            i || (t.hasOwnProperty("$rendered_parent") ? this._move_branch(t, t.$rendered_parent, e) : this._move_branch(t, t[this.$parentProperty], e))
        }, setParent: function (t, e, i) {
            this._setParentInner(t, e, i), t[this.$parentProperty] = e
        }, eachItem: function (t, e) {
            e = e || this.$getRootId();
            var i = this.getChildren(e);
            if (i) for (var n = 0; n < i.length; n++) {
                var r = this.pull[i[n]];
                t.call(this, r), this.hasChild(r.id) && this.eachItem(t, r.id)
            }
        }, eachParent: function (t, e) {
            for (var i = {}, n = e, r = this.getParent(n); this.exists(r);) {
                if (i[r]) throw new Error("Invalid tasks tree. Cyclic reference has been detected on task " + r);
                i[r] = !0, n = this.getItem(r), t.call(this, n), r = this.getParent(n)
            }
        }, _add_branch: function (t, e, i) {
            var r = void 0 === i ? this.getParent(t) : i;
            this.hasChild(r) || (this._branches[r] = n.$create());
            for (var a = this.getChildren(r), s = !1, o = 0, l = a.length; o < l; o++) if (a[o] == t.id) {
                s = !0;
                break
            }
            s || (1 * e == e ? a.splice(e, 0, t.id) : a.push(t.id), t.$rendered_parent = r)
        }, _move_branch: function (t, e, i) {
            this._replace_branch_child(e, t.id), this.exists(i) || i == this.$getRootId() ? this._add_branch(t, void 0, i) : delete this._branches[t.id], t.$level = this.calculateItemLevel(t), this.eachItem(function (t) {
                t.$level = this.calculateItemLevel(t)
            }, t.id)
        }, _replace_branch_child: function (t, e, i) {
            var r = this.getChildren(t);
            if (r && void 0 !== t) {
                for (var a = n.$create(), s = 0; s < r.length; s++) r[s] != e ? a.push(r[s]) : i && a.push(i);
                this._branches[t] = a
            }
        }, sort: function (t, e, i) {
            this.exists(i) || (i = this.$getRootId()), t || (t = "order");
            var n = "string" == typeof t ? function (e, i) {
                return e[t] == i[t] ? 0 : e[t] > i[t] ? 1 : -1
            } : t;
            if (e) {
                var r = n;
                n = function (t, e) {
                    return r(e, t)
                }
            }
            var a = this.getChildren(i);
            if (a) {
                for (var s = [], o = a.length - 1; o >= 0; o--) s[o] = this.getItem(a[o]);
                s.sort(n);
                for (o = 0; o < s.length; o++) a[o] = s[o].id, this.sort(t, e, a[o])
            }
        }, filter: function (t) {
            for (var e in this.pull) this.pull[e].$rendered_parent !== this.getParent(this.pull[e]) && this._move_branch(this.pull[e], this.pull[e].$rendered_parent, this.getParent(this.pull[e]));
            return a.prototype.filter.apply(this, arguments)
        }, open: function (t) {
            this.exists(t) && (this.getItem(t).$open = !0, this.callEvent("onItemOpen", [t]))
        }, close: function (t) {
            this.exists(t) && (this.getItem(t).$open = !1, this.callEvent("onItemClose", [t]))
        }, destructor: function () {
            a.prototype.destructor.call(this), this._branches = null
        }
    }, a.prototype), t.exports = s
}, function (t, e, i) {
    var n = i(0);
    t.exports = function () {
        return {
            getLinkCount: function () {
                return this.$data.linksStore.count()
            }, getLink: function (t) {
                return this.$data.linksStore.getItem(t)
            }, getLinks: function () {
                return this.$data.linksStore.getItems()
            }, isLinkExists: function (t) {
                return this.$data.linksStore.exists(t)
            }, addLink: function (t) {
                return this.$data.linksStore.addItem(t)
            }, updateLink: function (t, e) {
                n.defined(e) || (e = this.getLink(t)), this.$data.linksStore.updateItem(t, e)
            }, deleteLink: function (t) {
                return this.$data.linksStore.removeItem(t)
            }, changeLinkId: function (t, e) {
                return this.$data.linksStore.changeId(t, e)
            }
        }
    }
}, function (t, e, i) {
    var n = i(0);
    t.exports = function () {
        return {
            getTask: function (t) {
                this.assert(t, "Invalid argument for gantt.getTask");
                var e = this.$data.tasksStore.getItem(t);
                return this.assert(e, "Task not found id=" + t), e
            }, getTaskByTime: function (t, e) {
                var i = this.$data.tasksStore.getItems(), n = [];
                if (t || e) {
                    t = +t || -1 / 0, e = +e || 1 / 0;
                    for (var r = 0; r < i.length; r++) {
                        var a = i[r];
                        +a.start_date < e && +a.end_date > t && n.push(a)
                    }
                } else n = i;
                return n
            }, isTaskExists: function (t) {
                return this.$data.tasksStore.exists(t)
            }, updateTask: function (t, e) {
                n.defined(e) || (e = this.getTask(t)), this.$data.tasksStore.updateItem(t, e), this.isTaskExists(t) && this.refreshTask(t)
            }, addTask: function (t, e, i) {
                return n.defined(t.id) || (t.id = n.uid()), n.defined(e) || (e = this.getParent(t) || 0), this.isTaskExists(e) || (e = 0), this.setParent(t, e), this.$data.tasksStore.addItem(t, i, e)
            }, deleteTask: function (t) {
                return this.$data.tasksStore.removeItem(t)
            }, getTaskCount: function () {
                return this.$data.tasksStore.count()
            }, getVisibleTaskCount: function () {
                return this.$data.tasksStore.countVisible()
            }, getTaskIndex: function (t) {
                return this.$data.tasksStore.getBranchIndex(t)
            }, getGlobalTaskIndex: function (t) {
                return this.assert(t, "Invalid argument"), this.$data.tasksStore.getIndexById(t)
            }, eachTask: function (t, e, i) {
                return this.$data.tasksStore.eachItem(n.bind(t, i || this), e)
            }, eachParent: function (t, e, i) {
                return this.$data.tasksStore.eachParent(n.bind(t, i || this), e)
            }, changeTaskId: function (t, e) {
                this.$data.tasksStore.changeId(t, e);
                var i = this.$data.tasksStore.getItem(e), n = [];
                i.$source && (n = n.concat(i.$source)), i.$target && (n = n.concat(i.$target));
                for (var r = 0; r < n.length; r++) {
                    var a = this.getLink(n[r]);
                    a.source == t && (a.source = e), a.target == t && (a.target = e)
                }
            }, calculateTaskLevel: function (t) {
                return this.$data.tasksStore.calculateItemLevel(t)
            }, getNext: function (t) {
                return this.$data.tasksStore.getNext(t)
            }, getPrev: function (t) {
                return this.$data.tasksStore.getPrev(t)
            }, getParent: function (t) {
                return this.$data.tasksStore.getParent(t)
            }, setParent: function (t, e, i) {
                return this.$data.tasksStore.setParent(t, e, i)
            }, getSiblings: function (t) {
                return this.$data.tasksStore.getSiblings(t).slice()
            }, getNextSibling: function (t) {
                return this.$data.tasksStore.getNextSibling(t)
            }, getPrevSibling: function (t) {
                return this.$data.tasksStore.getPrevSibling(t)
            }, getTaskByIndex: function (t) {
                var e = this.$data.tasksStore.getIdByIndex(t);
                return this.isTaskExists(e) ? this.getTask(e) : null
            }, getChildren: function (t) {
                return this.hasChild(t) ? this.$data.tasksStore.getChildren(t).slice() : []
            }, hasChild: function (t) {
                return this.$data.tasksStore.hasChild(t)
            }, open: function (t) {
                this.$data.tasksStore.open(t)
            }, close: function (t) {
                this.$data.tasksStore.close(t)
            }, moveTask: function (t, e, i) {
                this.$data.tasksStore.move.apply(this.$data.tasksStore, arguments)
            }, sort: function (t, e, i, n) {
                var r = !n;
                this.$data.tasksStore.sort(t, e, i), r && this.render(), this.callEvent("onAfterSort", [t, e, i])
            }
        }
    }
}, function (t, e, i) {
    var n = i(0), r = i(80), a = i(79), s = i(20), o = i(78), l = i(77), c = i(76);

    function d() {
        for (var t = this.$services.getService("datastores"), e = [], i = 0; i < t.length; i++) e.push(this.getDatastore(t[i]));
        return e
    }

    var h = function () {
        return {
            createDatastore: function (t) {
                var e = "treedatastore" == (t.type || "").toLowerCase() ? o : s;
                if (t) {
                    var i = this;
                    t.openInitially = function () {
                        return i.config.open_tree_initially
                    }
                }
                var n = new e(t);
                if (this.mixin(n, l(n)), t.name) {
                    this.$services.setService("datastore:" + t.name, function () {
                        return n
                    });
                    var r = this.$services.getService("datastores");
                    r || (r = [], this.$services.setService("datastores", function () {
                        return r
                    })), r.push(t.name), c.bindDataStore(t.name, this)
                }
                return n
            }, getDatastore: function (t) {
                return this.$services.getService("datastore:" + t)
            }, refreshData: function () {
                var t = this.getScrollState();
                this.callEvent("onBeforeDataRender", []);
                for (var e = d.call(this), i = 0; i < e.length; i++) e[i].refresh();
                (t.x || t.y) && this.scrollTo(t.x, t.y), this.callEvent("onDataRender", [])
            }, isChildOf: function (t, e) {
                return this.$data.tasksStore.isChildOf(t, e)
            }, refreshTask: function (t, e) {
                var i = this.getTask(t);
                if (i && this.isTaskVisible(t)) {
                    if (this.$data.tasksStore.refresh(t, !!this.getState().drag_id), void 0 !== e && !e) return;
                    for (var n = 0; n < i.$source.length; n++) this.refreshLink(i.$source[n]);
                    for (n = 0; n < i.$target.length; n++) this.refreshLink(i.$target[n])
                } else this.isTaskExists(t) && this.isTaskExists(this.getParent(t)) && this.refreshTask(this.getParent(t))
            }, refreshLink: function (t) {
                this.$data.linksStore.refresh(t, !!this.getState().drag_id)
            }, silent: function (t) {
                var e = this;
                e.$data.tasksStore.silent(function () {
                    e.$data.linksStore.silent(function () {
                        t()
                    })
                })
            }, clearAll: function () {
                for (var t = d.call(this), e = 0; e < t.length; e++) t[e].clearAll();
                this._update_flags(), this.userdata = {}, this.callEvent("onClear", []), this.render()
            }, _clear_data: function () {
                this.$data.tasksStore.clearAll(), this.$data.linksStore.clearAll(), this._update_flags(), this.userdata = {}
            }, selectTask: function (t) {
                var e = this.$data.tasksStore;
                return !!this.config.select_task && (t && e.select(t), e.getSelectedId())
            }, unselectTask: function (t) {
                this.$data.tasksStore.unselect(t)
            }, getSelectedId: function () {
                return this.$data.tasksStore.getSelectedId()
            }
        }
    };
    t.exports = {
        create: function () {
            var t = n.mixin({}, h());
            return n.mixin(t, r()), n.mixin(t, a()), t
        }
    }
}, function (t, e, i) {
    var n = i(0), r = i(81), a = i(18);
    t.exports = function (t) {
        var e = r.create();
        n.mixin(t, e);
        var s = t.createDatastore({
            name: "task", type: "treeDatastore", rootId: function () {
                return t.config.root_id
            }, initItem: n.bind(function (e) {
                this.defined(e.id) || (e.id = this.uid()), e.start_date && (e.start_date = t.date.parseDate(e.start_date, "xml_date")), e.end_date && (e.end_date = t.date.parseDate(e.end_date, "xml_date"));
                var i = null;
                return (e.duration || 0 === e.duration) && (e.duration = i = 1 * e.duration), i && (e.start_date && !e.end_date ? e.end_date = this.calculateEndDate(e) : !e.start_date && e.end_date && (e.start_date = this.calculateEndDate({
                    start_date: e.end_date,
                    duration: -e.duration,
                    task: e
                }))), this._isAllowedUnscheduledTask(e) && this._set_default_task_timing(e), this._init_task_timing(e), e.start_date && e.end_date && this.correctTaskWorkTime(e), e.$source = [], e.$target = [], void 0 === e.parent && this.setParent(e, this.config.root_id), e
            }, t)
        }), o = t.createDatastore({
            name: "link", initItem: n.bind(function (t) {
                return this.defined(t.id) || (t.id = this.uid()), t
            }, t)
        });

        function l(e) {
            var i = t.isTaskVisible(e);
            if (!i && t.isTaskExists(e)) {
                var n = t.getParent(e);
                t.isTaskExists(n) && t.isTaskVisible(n) && (n = t.getTask(n), t.isSplitTask(n) && (i = !0))
            }
            return i
        }

        function c(e) {
            if (t.isTaskExists(e.source)) {
                var i = t.getTask(e.source);
                i.$source = i.$source || [], i.$source.push(e.id)
            }
            if (t.isTaskExists(e.target)) {
                var n = t.getTask(e.target);
                n.$target = n.$target || [], n.$target.push(e.id)
            }
        }

        function d(e) {
            if (t.isTaskExists(e.source)) for (var i = t.getTask(e.source), n = 0; n < i.$source.length; n++) if (i.$source[n] == e.id) {
                i.$source.splice(n, 1);
                break
            }
            if (t.isTaskExists(e.target)) {
                var r = t.getTask(e.target);
                for (n = 0; n < r.$target.length; n++) if (r.$target[n] == e.id) {
                    r.$target.splice(n, 1);
                    break
                }
            }
        }

        function h() {
            for (var e = null, i = t.$data.tasksStore.getItems(), n = 0, r = i.length; n < r; n++) (e = i[n]).$source = [], e.$target = [];
            var a = t.$data.linksStore.getItems();
            for (n = 0, r = a.length; n < r; n++) c(a[n])
        }

        function u(t) {
            var e = t.source, i = t.target;
            for (var n in t.events) !function (t, n) {
                e.attachEvent(t, function () {
                    return i.callEvent(n, Array.prototype.slice.call(arguments))
                }, n)
            }(n, t.events[n])
        }

        s.attachEvent("onBeforeRefreshAll", function () {
            for (var e = s.getVisibleItems(), i = 0; i < e.length; i++) {
                var n = e[i];
                n.$index = i, t.resetProjectDates(n)
            }
        }), s.attachEvent("onFilterItem", function (e, i) {
            var n = null, r = null;
            if (t.config.start_date && t.config.end_date) {
                if (t._isAllowedUnscheduledTask(i)) return !0;
                if (n = t.config.start_date.valueOf(), r = t.config.end_date.valueOf(), +i.start_date > r || +i.end_date < +n) return !1
            }
            return !0
        }), s.attachEvent("onIdChange", function (e, i) {
            t._update_flags(e, i)
        }), s.attachEvent("onAfterUpdate", function (e) {
            if (t._update_parents(e), t.getState("batchUpdate").batch_update) return !0;
            for (var i = s.getItem(e), n = 0; n < i.$source.length; n++) o.refresh(i.$source[n]);
            for (n = 0; n < i.$target.length; n++) o.refresh(i.$target[n])
        }), s.attachEvent("onAfterItemMove", function (e, i, n) {
            var r = t.getTask(e);
            null !== this.getNextSibling(e) ? r.$drop_target = this.getNextSibling(e) : null !== this.getPrevSibling(e) ? r.$drop_target = "next:" + this.getPrevSibling(e) : r.$drop_target = "next:null"
        }), s.attachEvent("onStoreUpdated", function (e, i, n) {
            if ("delete" == n && t._update_flags(e, null), !t.$services.getService("state").getState("batchUpdate").batch_update) {
                if (t.config.fit_tasks && "paint" !== n) {
                    var r = t.getState();
                    a(t);
                    var s = t.getState();
                    if (+r.min_date != +s.min_date || +r.max_date != +s.max_date) return t.render(), t.callEvent("onScaleAdjusted", []), !0
                }
                "add" == n || "move" == n || "delete" == n ? t.$layout.resize() : e || o.refresh()
            }
        }), o.attachEvent("onAfterAdd", function (t, e) {
            c(e)
        }), o.attachEvent("onAfterUpdate", function (t, e) {
            h()
        }), o.attachEvent("onAfterDelete", function (t, e) {
            d(e)
        }), o.attachEvent("onBeforeIdChange", function (e, i) {
            d(t.mixin({id: e}, t.$data.linksStore.getItem(i))), c(t.$data.linksStore.getItem(i))
        }), o.attachEvent("onFilterItem", function (e, i) {
            if (!t.config.show_links) return !1;
            var n = l(i.source), r = l(i.target);
            return !(!n || !r || t._isAllowedUnscheduledTask(t.getTask(i.source)) || t._isAllowedUnscheduledTask(t.getTask(i.target))) && t.callEvent("onBeforeLinkDisplay", [e, i])
        }), function () {
            var e = i(17), n = {};
            t.attachEvent("onBeforeTaskDelete", function (i, r) {
                return n[i] = e.getSubtreeLinks(t, i), !0
            }), t.attachEvent("onAfterTaskDelete", function (e, i) {
                n[e] && t.$data.linksStore.silent(function () {
                    for (var i in n[e]) t.$data.linksStore.removeItem(i), d(n[e][i]);
                    n[e] = null
                })
            })
        }(), t.attachEvent("onAfterLinkDelete", function (e, i) {
            t.refreshTask(i.source), t.refreshTask(i.target)
        }), t.attachEvent("onParse", h), u({
            source: o,
            target: t,
            events: {
                onItemLoading: "onLinkLoading",
                onBeforeAdd: "onBeforeLinkAdd",
                onAfterAdd: "onAfterLinkAdd",
                onBeforeUpdate: "onBeforeLinkUpdate",
                onAfterUpdate: "onAfterLinkUpdate",
                onBeforeDelete: "onBeforeLinkDelete",
                onAfterDelete: "onAfterLinkDelete",
                onIdChange: "onLinkIdChange"
            }
        }), u({
            source: s,
            target: t,
            events: {
                onItemLoading: "onTaskLoading",
                onBeforeAdd: "onBeforeTaskAdd",
                onAfterAdd: "onAfterTaskAdd",
                onBeforeUpdate: "onBeforeTaskUpdate",
                onAfterUpdate: "onAfterTaskUpdate",
                onBeforeDelete: "onBeforeTaskDelete",
                onAfterDelete: "onAfterTaskDelete",
                onIdChange: "onTaskIdChange",
                onBeforeItemMove: "onBeforeTaskMove",
                onAfterItemMove: "onAfterTaskMove",
                onFilterItem: "onBeforeTaskDisplay",
                onItemOpen: "onTaskOpened",
                onItemClose: "onTaskClosed",
                onBeforeSelect: "onBeforeTaskSelected",
                onAfterSelect: "onTaskSelected",
                onAfterUnselect: "onTaskUnselected"
            }
        }), t.$data = {tasksStore: s, linksStore: o}
    }
}, function (t, e) {
    t.exports = function () {
        function t(t) {
            return t.$ui.getView("timeline")
        }

        function e(t) {
            return t.$ui.getView("grid")
        }

        function i(t) {
            return t.$ui.getView("scrollVer")
        }

        function n(t) {
            return t.$ui.getView("scrollHor")
        }

        var r = "DEFAULT_VALUE";

        function a(t, e, i, n) {
            var a = t(this);
            return a && a.isVisible() ? a[e].apply(a, i) : n ? n() : r
        }

        return {
            getColumnIndex: function (t) {
                var i = a.call(this, e, "getColumnIndex", [t]);
                return i === r ? 0 : i
            }, dateFromPos: function (e) {
                var i = a.call(this, t, "dateFromPos", Array.prototype.slice.call(arguments));
                return i === r ? this.getState().min_date : i
            }, posFromDate: function (e) {
                var i = a.call(this, t, "posFromDate", [e]);
                return i === r ? 0 : i
            }, getRowTop: function (i) {
                var n = this, s = a.call(n, t, "getRowTop", [i], function () {
                    return a.call(n, e, "getRowTop", [i])
                });
                return s === r ? 0 : s
            }, getTaskTop: function (i) {
                var n = this, s = a.call(n, t, "getItemTop", [i], function () {
                    return a.call(n, e, "getItemTop", [i])
                });
                return s === r ? 0 : s
            }, getTaskPosition: function (e, i, n) {
                var s = a.call(this, t, "getItemPosition", [e, i, n]);
                return s === r ? {left: 0, top: this.getTaskTop(e.id), height: this.getTaskHeight(), width: 0} : s
            }, getTaskHeight: function () {
                var i = this, n = a.call(i, t, "getItemHeight", [], function () {
                    return a.call(i, e, "getItemHeight", [])
                });
                return n === r ? 0 : n
            }, columnIndexByDate: function (e) {
                var i = a.call(this, t, "columnIndexByDate", [e]);
                return i === r ? 0 : i
            }, roundTaskDates: function () {
                a.call(this, t, "roundTaskDates", [])
            }, getScale: function () {
                var e = a.call(this, t, "getScale", []);
                return e === r ? null : e
            }, getTaskNode: function (e) {
                var i = t(this);
                return i && i.isVisible() ? i._taskRenderer.rendered[e] : null
            }, getLinkNode: function (e) {
                var i = t(this);
                return i.isVisible() ? i._linkRenderer.rendered[e] : null
            }, scrollTo: function (t, e) {
                var r = i(this), a = n(this), s = {position: 0}, o = {position: 0};
                r && (o = r.getScrollState()), a && (s = a.getScrollState()), a && 1 * t == t && a.scroll(t), r && 1 * e == e && r.scroll(e);
                var l = {position: 0}, c = {position: 0};
                r && (l = r.getScrollState()), a && (c = a.getScrollState()), this.callEvent("onGanttScroll", [s.position, o.position, c.position, l.position])
            }, showDate: function (t) {
                var e = this.posFromDate(t), i = Math.max(e - this.config.task_scroll_offset, 0);
                this.scrollTo(i)
            }, showTask: function (t) {
                var e, i = this.getTaskPosition(this.getTask(t)),
                    n = Math.max(i.left - this.config.task_scroll_offset, 0), r = this._scroll_state().y;
                e = r ? i.top - (r - this.config.row_height) / 2 : i.top, this.scrollTo(n, e)
            }, _scroll_state: function () {
                var t = {
                    x: !1,
                    y: !1,
                    x_pos: 0,
                    y_pos: 0,
                    scroll_size: this.config.scroll_size + 1,
                    x_inner: 0,
                    y_inner: 0
                }, e = i(this), r = n(this);
                if (r) {
                    var a = r.getScrollState();
                    a.visible && (t.x = a.size, t.x_inner = a.scrollSize), t.x_pos = a.position || 0
                }
                if (e) {
                    var s = e.getScrollState();
                    s.visible && (t.y = s.size, t.y_inner = s.scrollSize), t.y_pos = s.position || 0
                }
                return t
            }, getScrollState: function () {
                var t = this._scroll_state();
                return {
                    x: t.x_pos,
                    y: t.y_pos,
                    inner_width: t.x,
                    inner_height: t.y,
                    width: t.x_inner,
                    height: t.y_inner
                }
            }
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        delete t.addTaskLayer, delete t.addLinkLayer
    }
}, function (t, e, i) {
    var n = i(1), r = function (t) {
        return {
            getVerticalScrollbar: function () {
                return t.$ui.getView("scrollVer")
            }, getHorizontalScrollbar: function () {
                return t.$ui.getView("scrollHor")
            }, _legacyGridResizerClass: function (t) {
                for (var e = t.getCellsByType("resizer"), i = 0; i < e.length; i++) {
                    var n = e[i], r = !1, a = n.$parent.getPrevSibling(n.$id);
                    if (a && a.$config && "grid" === a.$config.id) r = !0; else {
                        var s = n.$parent.getNextSibling(n.$id);
                        s && s.$config && "grid" === s.$config.id && (r = !0)
                    }
                    r && (n.$config.css = (n.$config.css ? n.$config.css + " " : "") + "gantt_grid_resize_wrap")
                }
            }, onCreated: function (e) {
                var i = !0;
                this._legacyGridResizerClass(e), e.attachEvent("onBeforeResize", function () {
                    var r = t.$ui.getView("timeline");
                    r && (r.$config.hidden = r.$parent.$config.hidden = !t.config.show_chart);
                    var a = t.$ui.getView("grid");
                    if (a) {
                        var s = t.config.show_grid;
                        if (i) {
                            var o = a._getColsTotalWidth();
                            !1 !== o && (t.config.grid_width = o), s = s && !!t.config.grid_width, t.config.show_grid = s
                        }
                        if (a.$config.hidden = a.$parent.$config.hidden = !s, !a.$config.hidden) {
                            var l = a._getGridWidthLimits();
                            if (l[0] && t.config.grid_width < l[0] && (t.config.grid_width = l[0]), l[1] && t.config.grid_width > l[1] && (t.config.grid_width = l[1]), r && t.config.show_chart) if (a.$config.width = t.config.grid_width - 1, i) a.$parent.$config.width = t.config.grid_width, a.$parent.$config.group && t.$layout._syncCellSizes(a.$parent.$config.group, a.$parent.$config.width); else if (r && !n.isChildOf(r.$task, e.$view)) {
                                if (!a.$config.original_grid_width) {
                                    var c = t.skins[t.skin];
                                    c && c.config && c.config.grid_width ? a.$config.original_grid_width = c.config.grid_width : a.$config.original_grid_width = 0
                                }
                                t.config.grid_width = a.$config.original_grid_width, a.$parent.$config.width = t.config.grid_width
                            } else a.$parent._setContentSize(a.$config.width, a.$config.height), t.$layout._syncCellSizes(a.$parent.$config.group, t.config.grid_width); else r && n.isChildOf(r.$task, e.$view) && (a.$config.original_grid_width = t.config.grid_width), i || (a.$parent.$config.width = 0)
                        }
                        i = !1
                    }
                }), this._initScrollStateEvents(e)
            }, _initScrollStateEvents: function (e) {
                t._getVerticalScrollbar = this.getVerticalScrollbar, t._getHorizontalScrollbar = this.getHorizontalScrollbar;
                var i = this.getVerticalScrollbar(), n = this.getHorizontalScrollbar();
                i && i.attachEvent("onScroll", function (e, i, n) {
                    var r = t.getScrollState();
                    t.callEvent("onGanttScroll", [r.x, e, r.x, i])
                }), n && n.attachEvent("onScroll", function (e, i, n) {
                    var r = t.getScrollState();
                    t.callEvent("onGanttScroll", [e, r.y, i, r.y])
                }), e.attachEvent("onResize", function () {
                    i && !t.$scroll_ver && (t.$scroll_ver = i.$scroll_ver), n && !t.$scroll_hor && (t.$scroll_hor = n.$scroll_hor)
                })
            }, _findGridResizer: function (t, e) {
                for (var i, n = t.getCellsByType("resizer"), r = !0, a = 0; a < n.length; a++) {
                    var s = n[a];
                    s._getSiblings();
                    var o = s._behind, l = s._front;
                    if (o && o.$content === e || o.isChild && o.isChild(e)) {
                        i = s, r = !0;
                        break
                    }
                    if (l && l.$content === e || l.isChild && l.isChild(e)) {
                        i = s, r = !1;
                        break
                    }
                }
                return {resizer: i, gridFirst: r}
            }, onInitialized: function (e) {
                var i = t.$ui.getView("grid"), n = this._findGridResizer(e, i);
                if (n.resizer) {
                    var r, a = n.gridFirst, s = n.resizer;
                    s.attachEvent("onResizeStart", function (e, i) {
                        var n = t.$ui.getView("grid"), s = n ? n.$parent : null;
                        if (s) {
                            var o = n._getGridWidthLimits();
                            n.$config.scrollable || (s.$config.minWidth = o[0]), s.$config.maxWidth = o[1]
                        }
                        return r = a ? e : i, t.callEvent("onGridResizeStart", [r])
                    }), s.attachEvent("onResize", function (e, i) {
                        var n = a ? e : i;
                        return t.callEvent("onGridResize", [r, n])
                    }), s.attachEvent("onResizeEnd", function (e, i, n, r) {
                        var s = a ? e : i, o = a ? n : r, l = t.$ui.getView("grid"), c = l ? l.$parent : null;
                        c && (c.$config.minWidth = void 0);
                        var d = t.callEvent("onGridResizeEnd", [s, o]);
                        return d && (t.config.grid_width = o), d
                    })
                }
            }, onDestroyed: function (t) {
            }
        }
    };
    t.exports = r
}, function (t, e, i) {
    var n = i(1), r = function (t, e) {
        var i, r, a, s, o;

        function l() {
            return {link_source_id: s, link_target_id: r, link_from_start: o, link_to_start: a, link_landing_area: i}
        }

        var c = e.$services, d = c.getService("state"), h = c.getService("dnd");
        d.registerProvider("linksDnD", l);
        var u = new h(t.$task_bars, {sensitivity: 0, updates_per_second: 60});

        function g(i, n, r, a, s) {
            var o = function (i, n, r) {
                var a = n(i), s = {x: a.left, y: a.top, width: a.width, height: a.height};
                r.rtl ? (s.xEnd = s.x, s.x = s.xEnd + s.width) : s.xEnd = s.x + s.width;
                if (s.yEnd = s.y + s.height, e.getTaskType(i.type) == e.config.types.milestone) {
                    var o = function () {
                        var e = t.getItemHeight();
                        return Math.round(Math.sqrt(2 * e * e)) - 2
                    }();
                    s.x += (r.rtl ? 1 : -1) * (o / 2), s.xEnd += (r.rtl ? -1 : 1) * (o / 2), s.width = a.xEnd - a.x
                }
                return s
            }(i, function (t) {
                return e.getTaskPosition(t)
            }, a), l = {x: o.x, y: o.y};
            n || (l.x = o.xEnd), l.y += e.config.row_height / 2;
            var c = function (t) {
                return e.getTaskType(t.type) == e.config.types.milestone
            }(i) && s ? 2 : 0;
            return r = r || 0, a.rtl && (r *= -1), l.x += (n ? -1 : 1) * r - c, l
        }

        function f(t) {
            var i = l(), n = ["gantt_link_tooltip"];
            i.link_source_id && i.link_target_id && (e.isLinkAllowed(i.link_source_id, i.link_target_id, i.link_from_start, i.link_to_start) ? n.push("gantt_allowed_link") : n.push("gantt_invalid_link"));
            var r = e.templates.drag_link_class(i.link_source_id, i.link_from_start, i.link_target_id, i.link_to_start);
            r && n.push(r);
            var a = "<div class='" + r + "'>" + e.templates.drag_link(i.link_source_id, i.link_from_start, i.link_target_id, i.link_to_start) + "</div>";
            t.innerHTML = a
        }

        function _() {
            s = o = r = null, a = !0
        }

        function p(t, e, i, n) {
            return e >= t ? n <= i ? 1 : 4 : n <= i ? 2 : 3
        }

        u.attachEvent("onBeforeDragStart", e.bind(function (i, r) {
            var a = r.target || r.srcElement;
            if (_(), e.getState().drag_id) return !1;
            if (n.locateClassName(a, "gantt_link_point")) {
                n.locateClassName(a, "task_start_date") && (o = !0);
                var l = e.locate(r);
                s = l;
                var c = e.getTask(l);
                if (e.isReadonly(c)) return _(), !1;
                return this._dir_start = g(c, !!o, 0, t.$getConfig(), !0), !0
            }
            return !1
        }, this)), u.attachEvent("onAfterDragStart", e.bind(function (t, i) {
            e.config.touch && e.refreshData(), f(u.config.marker)
        }, this)), u.attachEvent("onDragMove", e.bind(function (s, o) {
            var c = u.config, d = u.getPosition(o);
            !function (t, e) {
                t.style.left = e.x + 5 + "px", t.style.top = e.y + 5 + "px"
            }(c.marker, d);
            var h = !!n.locateClassName(o, "gantt_link_control"), _ = r, v = i, m = a, y = e.locate(o), k = !0;
            if (n.isChildOf(o.target || o.srcElement, e.$root) || (h = !1, y = null), h && (k = !n.locateClassName(o, "task_end_date"), h = !!y), r = y, i = h, a = k, h) {
                var b = e.getTask(y), $ = t.$getConfig(), x = n.locateClassName(o, "gantt_link_control"), w = 0;
                x && (w = Math.floor(x.offsetWidth / 2)), this._dir_end = g(b, !!a, w, $)
            } else this._dir_end = n.getRelativeEventPosition(o, t.$task_data);
            var S = !(v == h && _ == y && m == k);
            return S && (_ && e.refreshTask(_, !1), y && e.refreshTask(y, !1)), S && f(c.marker), function (i, n, r, a) {
                var s = function () {
                    u._direction || (u._direction = document.createElement("div"), t.$task_links.appendChild(u._direction));
                    return u._direction
                }(), o = l(), c = ["gantt_link_direction"];
                e.templates.link_direction_class && c.push(e.templates.link_direction_class(o.link_source_id, o.link_from_start, o.link_target_id, o.link_to_start));
                var d = Math.sqrt(Math.pow(r - i, 2) + Math.pow(a - n, 2));
                if (!(d = Math.max(0, d - 3))) return;
                s.className = c.join(" ");
                var h = (a - n) / (r - i), g = Math.atan(h);
                2 == p(i, r, n, a) ? g += Math.PI : 3 == p(i, r, n, a) && (g -= Math.PI);
                var f = Math.sin(g), _ = Math.cos(g), v = Math.round(n), m = Math.round(i),
                    y = ["-webkit-transform: rotate(" + g + "rad)", "-moz-transform: rotate(" + g + "rad)", "-ms-transform: rotate(" + g + "rad)", "-o-transform: rotate(" + g + "rad)", "transform: rotate(" + g + "rad)", "width:" + Math.round(d) + "px"];
                if (-1 != window.navigator.userAgent.indexOf("MSIE 8.0")) {
                    y.push('-ms-filter: "' + function (t, e) {
                        return "progid:DXImageTransform.Microsoft.Matrix(M11 = " + e + ",M12 = -" + t + ",M21 = " + t + ",M22 = " + e + ",SizingMethod = 'auto expand')"
                    }(f, _) + '"');
                    var k = Math.abs(Math.round(i - r)), b = Math.abs(Math.round(a - n));
                    switch (p(i, r, n, a)) {
                        case 1:
                            v -= b;
                            break;
                        case 2:
                            m -= k, v -= b;
                            break;
                        case 3:
                            m -= k
                    }
                }
                y.push("top:" + v + "px"), y.push("left:" + m + "px"), s.style.cssText = y.join(";")
            }(this._dir_start.x, this._dir_start.y, this._dir_end.x, this._dir_end.y), !0
        }, this)), u.attachEvent("onDragEnd", e.bind(function () {
            var t = l();
            if (t.link_source_id && t.link_target_id && t.link_source_id != t.link_target_id) {
                var i = e._get_link_type(t.link_from_start, t.link_to_start),
                    n = {source: t.link_source_id, target: t.link_target_id, type: i};
                n.type && e.isLinkAllowed(n) && e.addLink(n)
            }
            _(), e.config.touch ? e.refreshData() : (t.link_source_id && e.refreshTask(t.link_source_id, !1), t.link_target_id && e.refreshTask(t.link_target_id, !1)), u._direction && (u._direction.parentNode && u._direction.parentNode.removeChild(u._direction), u._direction = null)
        }, this))
    };
    t.exports = {
        createLinkDND: function () {
            return {init: r}
        }
    }
}, function (t, e, i) {
    var n = i(1), r = i(0), a = i(26);
    t.exports = {
        createTaskDND: function () {
            var t;
            return {
                extend: function (e) {
                    e.roundTaskDates = function (e) {
                        t.round_task_dates(e)
                    }
                }, init: function (e, i) {
                    return t = function (t, e) {
                        var i = e.$services;
                        return {
                            drag: null,
                            dragMultiple: {},
                            _events: {before_start: {}, before_finish: {}, after_finish: {}},
                            _handlers: {},
                            init: function () {
                                this._domEvents = e._createDomEventScope(), this.clear_drag_state();
                                var t = e.config.drag_mode;
                                this.set_actions(), i.getService("state").registerProvider("tasksDnd", r.bind(function () {
                                    return {
                                        drag_id: this.drag ? this.drag.id : void 0,
                                        drag_mode: this.drag ? this.drag.mode : void 0,
                                        drag_from_start: this.drag ? this.drag.left : void 0
                                    }
                                }, this));
                                var n = {
                                    before_start: "onBeforeTaskDrag",
                                    before_finish: "onBeforeTaskChanged",
                                    after_finish: "onAfterTaskDrag"
                                };
                                for (var a in this._events) for (var s in t) this._events[a][s] = n[a];
                                this._handlers[t.move] = this._move, this._handlers[t.resize] = this._resize, this._handlers[t.progress] = this._resize_progress
                            },
                            set_actions: function () {
                                var i = t.$task_data;
                                this._domEvents.attach(i, "mousemove", e.bind(function (t) {
                                    this.on_mouse_move(t || event)
                                }, this)), this._domEvents.attach(i, "mousedown", e.bind(function (t) {
                                    this.on_mouse_down(t || event)
                                }, this)), this._domEvents.attach(i, "mouseup", e.bind(function (t) {
                                    this.on_mouse_up(t || event)
                                }, this))
                            },
                            clear_drag_state: function () {
                                this.drag = {
                                    id: null,
                                    mode: null,
                                    pos: null,
                                    start_x: null,
                                    start_y: null,
                                    obj: null,
                                    left: null
                                }, this.dragMultiple = {}
                            },
                            _resize: function (i, n, r) {
                                var a = t.$getConfig(), s = this._drag_task_coords(i, r);
                                r.left ? (i.start_date = e.dateFromPos(s.start + n), i.start_date || (i.start_date = new Date(e.getState().min_date))) : (i.end_date = e.dateFromPos(s.end + n), i.end_date || (i.end_date = new Date(e.getState().max_date))), i.end_date - i.start_date < a.min_duration && (r.left ? i.start_date = e.calculateEndDate({
                                    start_date: i.end_date,
                                    duration: -1,
                                    task: i
                                }) : i.end_date = e.calculateEndDate({
                                    start_date: i.start_date,
                                    duration: 1,
                                    task: i
                                })), e._init_task_timing(i)
                            },
                            _resize_progress: function (e, i, n) {
                                var r = this._drag_task_coords(e, n),
                                    a = t.$getConfig().rtl ? r.start - n.pos.x : n.pos.x - r.start, s = Math.max(0, a);
                                e.progress = Math.min(1, s / Math.abs(r.end - r.start))
                            },
                            _find_max_shift: function (t, i) {
                                var n;
                                for (var r in t) {
                                    var a = t[r], s = e.getTask(a.id), o = this._drag_task_coords(s, a),
                                        l = e.posFromDate(new Date(e.getState().min_date)),
                                        c = e.posFromDate(new Date(e.getState().max_date));
                                    if (o.end + i > c) {
                                        var d = c - o.end;
                                        (d < n || void 0 === n) && (n = d)
                                    } else if (o.start + i < l) {
                                        var h = l - o.start;
                                        (h < n || void 0 === n) && (n = h)
                                    }
                                }
                                return n
                            },
                            _move: function (t, i, n) {
                                var r = this._drag_task_coords(t, n), a = e.dateFromPos(r.start + i),
                                    s = e.dateFromPos(r.end + i);
                                a ? s ? (t.start_date = a, t.end_date = s) : (t.end_date = new Date(e.getState().max_date), t.start_date = e.dateFromPos(e.posFromDate(t.end_date) - (r.end - r.start))) : (t.start_date = new Date(e.getState().min_date), t.end_date = e.dateFromPos(e.posFromDate(t.start_date) + (r.end - r.start)))
                            },
                            _drag_task_coords: function (t, i) {
                                return {
                                    start: i.obj_s_x = i.obj_s_x || e.posFromDate(t.start_date),
                                    end: i.obj_e_x = i.obj_e_x || e.posFromDate(t.end_date)
                                }
                            },
                            _mouse_position_change: function (t, e) {
                                var i = t.x - e.x, n = t.y - e.y;
                                return Math.sqrt(i * i + n * n)
                            },
                            _is_number: function (t) {
                                return !isNaN(parseFloat(t)) && isFinite(t)
                            },
                            on_mouse_move: function (t) {
                                if (this.drag.start_drag) {
                                    var i = n.getRelativeEventPosition(t, e.$task_data),
                                        r = this.drag.start_drag.start_x, s = this.drag.start_drag.start_y;
                                    (Date.now() - this.drag.timestamp > 50 || this._is_number(r) && this._is_number(s) && this._mouse_position_change({
                                        x: r,
                                        y: s
                                    }, i) > 20) && this._start_dnd(t)
                                }
                                if (this.drag.mode) {
                                    if (!a(this, 40)) return;
                                    this._update_on_move(t)
                                }
                            },
                            _update_item_on_move: function (t, i, n, r, a) {
                                var s = e.getTask(i), o = e.mixin({}, s), l = e.mixin({}, s);
                                this._handlers[n].apply(this, [l, t, r]), e.mixin(s, l, !0), e.callEvent("onTaskDrag", [s.id, n, l, o, a]), e.mixin(s, l, !0), e.refreshTask(i)
                            },
                            _update_on_move: function (i) {
                                var a = this.drag, s = t.$getConfig();
                                if (a.mode) {
                                    var o = n.getRelativeEventPosition(i, t.$task_data);
                                    if (a.pos && a.pos.x == o.x) return;
                                    a.pos = o;
                                    var l = e.dateFromPos(o.x);
                                    if (!l || isNaN(l.getTime())) return;
                                    var c = o.x - a.start_x, d = e.getTask(a.id);
                                    if (this._handlers[a.mode]) {
                                        if (e.isSummaryTask(d) && e.config.drag_project && a.mode == s.drag_mode.move) {
                                            var h = {};
                                            h[a.id] = r.copy(a);
                                            var u = this._find_max_shift(r.mixin(h, this.dragMultiple), c);
                                            for (var g in void 0 !== u && (c = u), this._update_item_on_move(c, a.id, a.mode, a, i), this.dragMultiple) {
                                                var f = this.dragMultiple[g];
                                                this._update_item_on_move(c, f.id, f.mode, f, i)
                                            }
                                        } else this._update_item_on_move(c, a.id, a.mode, a, i);
                                        e._update_parents(a.id)
                                    }
                                }
                            },
                            on_mouse_down: function (i, r) {
                                if (2 != i.button || void 0 === i.button) {
                                    var a = t.$getConfig(), s = e.locate(i), o = null;
                                    if (e.isTaskExists(s) && (o = e.getTask(s)), !e.isReadonly(o) && !this.drag.mode) {
                                        this.clear_drag_state(), r = r || i.target || i.srcElement;
                                        var l = n.getClassName(r), c = this._get_drag_mode(l, r);
                                        if (!l || !c) return r.parentNode ? this.on_mouse_down(i, r.parentNode) : void 0;
                                        if (c) if (c.mode && c.mode != a.drag_mode.ignore && a["drag_" + c.mode]) {
                                            if (s = e.locate(r), o = e.copy(e.getTask(s) || {}), e.isReadonly(o)) return this.clear_drag_state(), !1;
                                            if (e.isSummaryTask(o) && !a.drag_project && c.mode != a.drag_mode.progress) return void this.clear_drag_state();
                                            c.id = s;
                                            var d = n.getRelativeEventPosition(i, e.$task_data);
                                            c.start_x = d.x, c.start_y = d.y, c.obj = o, this.drag.start_drag = c, this.drag.timestamp = Date.now()
                                        } else this.clear_drag_state(); else if (e.checkEvent("onMouseDown") && e.callEvent("onMouseDown", [l.split(" ")[0]]) && r.parentNode) return this.on_mouse_down(i, r.parentNode)
                                    }
                                }
                            },
                            _fix_dnd_scale_time: function (i, n) {
                                var r = t.$getConfig(), a = e.getScale().unit, s = e.getScale().step;

                                function o(i) {
                                    if (e.config.correct_work_time) {
                                        var n = t.$getConfig();
                                        e.isWorkTime(i.start_date, void 0, i) || (i.start_date = e.calculateEndDate({
                                            start_date: i.start_date,
                                            duration: -1,
                                            unit: n.duration_unit,
                                            task: i
                                        }))
                                    }
                                }

                                r.round_dnd_dates || (a = "minute", s = r.time_step), n.mode == r.drag_mode.resize ? n.left ? (i.start_date = e.roundDate({
                                    date: i.start_date,
                                    unit: a,
                                    step: s
                                }), o(i)) : (i.end_date = e.roundDate({
                                    date: i.end_date,
                                    unit: a,
                                    step: s
                                }), function (i) {
                                    if (e.config.correct_work_time) {
                                        var n = t.$getConfig();
                                        e.isWorkTime(new Date(i.end_date - 1), void 0, i) || (i.end_date = e.calculateEndDate({
                                            start_date: i.end_date,
                                            duration: 1,
                                            unit: n.duration_unit,
                                            task: i
                                        }))
                                    }
                                }(i)) : n.mode == r.drag_mode.move && (i.start_date = e.roundDate({
                                    date: i.start_date,
                                    unit: a,
                                    step: s
                                }), o(i), i.end_date = e.calculateEndDate(i))
                            },
                            _fix_working_times: function (i, n) {
                                var r = t.$getConfig();
                                (n = n || {mode: r.drag_mode.move}).mode == r.drag_mode.resize ? n.left ? i.start_date = e.getClosestWorkTime({
                                    date: i.start_date,
                                    dir: "future",
                                    task: i
                                }) : i.end_date = e.getClosestWorkTime({
                                    date: i.end_date,
                                    dir: "past",
                                    task: i
                                }) : n.mode == r.drag_mode.move && e.correctTaskWorkTime(i)
                            },
                            _finalize_mouse_up: function (t, i, n, r) {
                                var a = e.getTask(t);
                                if (i.work_time && i.correct_work_time && this._fix_working_times(a, n), this._fix_dnd_scale_time(a, n), this._fireEvent("before_finish", n.mode, [t, n.mode, e.copy(n.obj), r])) {
                                    var s = t;
                                    e._init_task_timing(a), this.clear_drag_state(), e.updateTask(a.id), this._fireEvent("after_finish", n.mode, [s, n.mode, r])
                                } else this.clear_drag_state(), t == n.id && (n.obj._dhx_changed = !1, e.mixin(a, n.obj, !0)), e.refreshTask(a.id)
                            },
                            on_mouse_up: function (i) {
                                var n = this.drag;
                                if (n.mode && n.id) {
                                    var r = t.$getConfig(), a = e.getTask(n.id), s = this.dragMultiple;
                                    if (e.isSummaryTask(a) && r.drag_project && n.mode == r.drag_mode.move) for (var o in s) this._finalize_mouse_up(s[o].id, r, s[o], i);
                                    this._finalize_mouse_up(n.id, r, n, i)
                                }
                                this.clear_drag_state()
                            },
                            _get_drag_mode: function (e, i) {
                                var n = t.$getConfig().drag_mode, r = {mode: null, left: null};
                                switch ((e || "").split(" ")[0]) {
                                    case"gantt_task_line":
                                    case"gantt_task_content":
                                        r.mode = n.move;
                                        break;
                                    case"gantt_task_drag":
                                        r.mode = n.resize;
                                        var a = i.getAttribute("data-bind-property");
                                        r.left = "start_date" == a;
                                        break;
                                    case"gantt_task_progress_drag":
                                        r.mode = n.progress;
                                        break;
                                    case"gantt_link_control":
                                    case"gantt_link_point":
                                        r.mode = n.ignore;
                                        break;
                                    default:
                                        r = null
                                }
                                return r
                            },
                            _start_dnd: function (i) {
                                var n = this.drag = this.drag.start_drag;
                                delete n.start_drag;
                                var r = t.$getConfig(), a = n.id;
                                if (r["drag_" + n.mode] && e.callEvent("onBeforeDrag", [a, n.mode, i]) && this._fireEvent("before_start", n.mode, [a, n.mode, i])) {
                                    delete n.start_drag;
                                    var s = e.getTask(a);
                                    e.isSummaryTask(s) && e.config.drag_project && n.mode == r.drag_mode.move && e.eachTask(function (t) {
                                        this.dragMultiple[t.id] = e.mixin({id: t.id, obj: t}, this.drag)
                                    }, s.id, this), e.callEvent("onTaskDragStart", [])
                                } else this.clear_drag_state()
                            },
                            _fireEvent: function (t, i, n) {
                                e.assert(this._events[t], "Invalid stage:{" + t + "}");
                                var r = this._events[t][i];
                                return e.assert(r, "Unknown after drop mode:{" + i + "}"), e.assert(n, "Invalid event arguments"), !e.checkEvent(r) || e.callEvent(r, n)
                            },
                            round_task_dates: function (e) {
                                var i = this.drag, n = t.$getConfig();
                                i || (i = {mode: n.drag_mode.move}), this._fix_dnd_scale_time(e, i)
                            },
                            destructor: function () {
                                this._domEvents.detachAll()
                            }
                        }
                    }(e, i), e._tasks_dnd = t, t.init(i)
                }, destructor: function () {
                    t.destructor(), t = null
                }
            }
        }
    }
}, function (t, e, i) {
    var n = i(0), r = i(87), a = i(86), s = i(1), o = function (t) {
        var e = t.$services;
        return {
            onCreated: function (e) {
                var s = e.$config;
                s.bind = n.defined(s.bind) ? s.bind : "task", s.bindLinks = n.defined(s.bindLinks) ? s.bindLinks : "link", e._linksDnD = a.createLinkDND(), e._tasksDnD = r.createTaskDND(), e._tasksDnD.extend(e), this._mouseDelegates = i(10)(t)
            }, onInitialized: function (e) {
                this._attachDomEvents(t), this._attachStateProvider(t, e), e._tasksDnD.init(e, t), e._linksDnD.init(e, t), "timeline" == e.$config.id && this.extendDom(e)
            }, onDestroyed: function (e) {
                this._clearDomEvents(t), this._clearStateProvider(t)
            }, extendDom: function (e) {
                t.$task = e.$task, t.$task_scale = e.$task_scale, t.$task_data = e.$task_data, t.$task_bg = e.$task_bg, t.$task_links = e.$task_links, t.$task_bars = e.$task_bars
            }, _clearDomEvents: function () {
                this._mouseDelegates.destructor(), this._mouseDelegates = null
            }, _attachDomEvents: function (t) {
                function e(e, i) {
                    if (e && this.callEvent("onLinkDblClick", [e, i])) {
                        var n = this.getLink(e);
                        if (this.isReadonly(n)) return;
                        var r = this.locale.labels.link + " " + this.templates.link_description(this.getLink(e)) + " " + this.locale.labels.confirm_link_deleting;
                        window.setTimeout(function () {
                            t._dhtmlx_confirm(r, "", function () {
                                t.deleteLink(e)
                            })
                        }, this.config.touch ? 300 : 1)
                    }
                }

                this._mouseDelegates.delegate("click", "gantt_task_link", t.bind(function (t, e) {
                    var i = this.locate(t, this.config.link_attribute);
                    i && this.callEvent("onLinkClick", [i, t])
                }, t), this.$task), this._mouseDelegates.delegate("click", "gantt_scale_cell", t.bind(function (e, i) {
                    var n = s.getRelativeEventPosition(e, t.$task_data), r = t.dateFromPos(n.x),
                        a = Math.floor(t.columnIndexByDate(r)), o = t.getScale().trace_x[a];
                    t.callEvent("onScaleClick", [e, o])
                }, t), this.$task) /*,this._mouseDelegates.delegate("doubleclick", "gantt_task_link", t.bind(function (i, n, r) {
                    n = this.locate(i, t.config.link_attribute), e.call(this, n, i)
                }, t), this.$task), this._mouseDelegates.delegate("doubleclick", "gantt_link_point", t.bind(function (t, i, n) {
                    i = this.locate(t);
                    var r = this.getTask(i), a = null;
                    return n.parentNode && s.getClassName(n.parentNode) && (a = s.getClassName(n.parentNode).indexOf("_left") > -1 ? r.$target[0] : r.$source[0]), a && e.call(this, a, t), !1
                }, t), this.$task)*/
            },_attachStateProvider: function (t, i) {
                var n = i;
                e.getService("state").registerProvider("tasksTimeline", function () {
                    return {
                        scale_unit: n._tasks ? n._tasks.unit : void 0,
                        scale_step: n._tasks ? n._tasks.step : void 0
                    }
                })
            }, _clearStateProvider: function () {
                e.getService("state").unregisterProvider("tasksTimeline")
            }
        }
    };
    t.exports = o
}, function (t, e, i) {
    var n = i(1);

    function r(t, e) {
        var i = n.getNodePosition(e.$grid_data);
        return t.x += i.x - e.$grid.scrollLeft, t.y += i.y - e.$grid_data.scrollTop, t
    }

    t.exports = {
        removeLineHighlight: function (t) {
            t.markerLine && t.markerLine.parentNode && t.markerLine.parentNode.removeChild(t.markerLine), t.markerLine = null
        }, highlightPosition: function (t, e, i) {
            var a = function (t, e) {
                var i = n.getNodePosition(e.$grid_data), r = n.getRelativeEventPosition(t, e.$grid_data),
                    a = e.$config.rowStore, s = i.x, o = r.y - 10, l = e.$getConfig();
                o < i.y && (o = i.y);
                var c = a.countVisible() * l.row_height;
                return o > i.y + c - l.row_height && (o = i.y + c - l.row_height), i.x = s, i.y = o, i
            }(t, i);
            e.marker.style.left = "10px", e.marker.style.top = a.y + "px";
            var s = e.markerLine;
            s || ((s = document.createElement("div")).className = "gantt_drag_marker gantt_grid_dnd_marker", s.innerHTML = "<div class='gantt_grid_dnd_marker_line'></div>", s.style.pointerEvents = "none", document.body.appendChild(s), e.markerLine = s), t.child ? function (t, e, i) {
                var n = t.targetParent, a = r({x: 0, y: i.getItemTop(n)}, i);
                e.innerHTML = "<div class='gantt_grid_dnd_marker_folder'></div>", e.style.width = i.$grid_data.offsetWidth + "px", e.style.top = a.y + "px", e.style.left = a.x + "px", e.style.height = i.getItemHeight(n) + "px"
            }(t, s, i) : function (t, e, i) {
                var n = function (t, e) {
                    var i = e.$config.rowStore, n = {x: 0, y: 0}, a = e.$grid_data.querySelector(".gantt_tree_indent"),
                        s = 15, o = 0;
                    if (a && (s = a.offsetWidth), t.targetId !== i.$getRootId()) {
                        var l = e.getItemTop(t.targetId), c = e.getItemHeight(t.targetId);
                        if (o = i.exists(t.targetId) ? i.calculateItemLevel(i.getItem(t.targetId)) : 0, t.prevSibling) n.y = l; else if (t.nextSibling) {
                            var d = 0;
                            i.eachItem(function (t) {
                                -1 !== i.getIndexById(t.id) && d++
                            }, t.targetId), n.y = l + c + d * c
                        } else n.y = l + c, o += 1
                    }
                    return n.x = 40 + o * s, n.width = Math.max(e.$grid_data.offsetWidth - n.x, 0), r(n, e)
                }(t, i);
                e.innerHTML = "<div class='gantt_grid_dnd_marker_line'></div>", e.style.left = n.x + "px", e.style.height = "4px", e.style.top = n.y - 2 + "px", e.style.width = n.width + "px"
            }(t, s, i)
        }
    }
}, function (t, e, i) {
    var n = i(9);
    t.exports = function (t, e, i, r, a) {
        var s;
        if (e !== a.$getRootId()) s = i < .25 ? n.prevSiblingTarget(t, e, a) : !(i > .6) || a.hasChild(e) && a.getItem(e).$open ? n.firstChildTarget(t, e, a) : n.nextSiblingTarget(t, e, a); else {
            var o = a.$getRootId();
            s = a.hasChild(o) && r >= 0 ? n.lastChildTarget(t, o, a) : n.firstChildTarget(t, o, a)
        }
        return s
    }
}, function (t, e, i) {
    var n = i(9);

    function r(t, e, i, r, a) {
        for (var s = e; r.exists(s);) {
            var o = r.calculateItemLevel(r.getItem(s));
            if ((o === i || o === i - 1) && r.getBranchIndex(s) > -1) break;
            s = a ? r.getPrev(s) : r.getNext(s)
        }
        return r.exists(s) ? r.calculateItemLevel(r.getItem(s)) === i ? a ? n.nextSiblingTarget(t, s, r) : n.prevSiblingTarget(t, s, r) : n.firstChildTarget(t, s, r) : null
    }

    function a(t, e, i, n) {
        return r(t, e, i, n, !0)
    }

    function s(t, e, i, n) {
        return r(t, e, i, n, !1)
    }

    t.exports = function (t, e, i, r, o, l) {
        var c;
        if (e !== o.$getRootId()) i < .5 ? o.calculateItemLevel(o.getItem(e)) === l ? c = o.getPrevSibling(e) ? n.nextSiblingTarget(t, o.getPrevSibling(e), o) : n.prevSiblingTarget(t, e, o) : (c = a(t, e, l, o)) && (c = s(t, e, l, o)) : o.calculateItemLevel(o.getItem(e)) === l ? c = n.nextSiblingTarget(t, e, o) : (c = s(t, e, l, o)) && (c = a(t, e, l, o)); else {
            var d = o.$getRootId(), h = o.getChildren(d);
            c = n.createDropTargetObject(), c = h.length && r >= 0 ? a(t, function (t) {
                for (var e = t.getNext(); t.exists(e);) {
                    var i = t.getNext(e);
                    if (!t.exists(i)) return e;
                    e = i
                }
                return null
            }(o), l, o) : s(t, d, l, o)
        }
        return c
    }
}, function (t, e, i) {
    var n = i(1), r = i(9), a = i(91), s = i(90), o = i(89);
    t.exports = {
        init: function (t, e) {
            var i = t.$services.getService("dnd");
            if (e.$config.bind && t.getDatastore(e.$config.bind)) {
                var l = new i(e.$grid_data, {updates_per_second: 60});
                t.defined(e.$getConfig().dnd_sensitivity) && (l.config.sensitivity = e.$getConfig().dnd_sensitivity), l.attachEvent("onBeforeDragStart", t.bind(function (i, n) {
                    var r = c(n);
                    if (!r) return !1;
                    t.hideQuickInfo && t._hideQuickInfo();
                    var a = r.getAttribute(e.$config.item_attribute), s = e.$config.rowStore.getItem(a);
                    return !t.isReadonly(s) && (l.config.initial_open_state = s.$open, !!t.callEvent("onRowDragStart", [a, n.target || n.srcElement, n]) && void 0)
                }, t)), l.attachEvent("onAfterDragStart", t.bind(function (t, i) {
                    var n = c(i);
                    l.config.marker.innerHTML = n.outerHTML;
                    var a = l.config.marker.firstChild;
                    a && (l.config.marker.style.opacity = .4, a.style.position = "static", a.style.pointerEvents = "none"), l.config.id = n.getAttribute(e.$config.item_attribute);
                    var s = e.$config.rowStore, o = s.getItem(l.config.id);
                    l.config.level = s.calculateItemLevel(o), l.config.drop_target = r.createDropTargetObject({
                        targetParent: s.getParent(o.id),
                        targetIndex: s.getBranchIndex(o.id),
                        targetId: o.id,
                        nextSibling: !0
                    }), o.$open = !1, o.$transparent = !0, this.refreshData()
                }, t)), l.attachEvent("onDragMove", t.bind(function (i, n) {
                    var a = d(n);
                    return a && !1 !== t.callEvent("onBeforeRowDragMove", [l.config.id, a.targetParent, a.targetIndex]) || (a = r.createDropTargetObject(l.config.drop_target)), o.highlightPosition(a, l.config, e), l.config.drop_target = a, this.callEvent("onRowDragMove", [l.config.id, a.targetParent, a.targetIndex]), !0
                }, t)), l.attachEvent("onDragEnd", t.bind(function () {
                    var t = e.$config.rowStore, i = t.getItem(l.config.id);
                    o.removeLineHighlight(l.config), i.$transparent = !1, i.$open = l.config.initial_open_state;
                    var n = l.config.drop_target;
                    !1 === this.callEvent("onBeforeRowDragEnd", [l.config.id, n.targetParent, n.targetIndex]) ? i.$drop_target = null : (t.move(l.config.id, n.targetIndex, n.targetParent), this.callEvent("onRowDragEnd", [l.config.id, n.targetParent, n.targetIndex])), t.refresh(i.id)
                }, t))
            }

            function c(t) {
                return n.locateAttribute(t, e.$config.item_attribute)
            }

            function d(t) {
                var i = function (t) {
                        var i = n.getRelativeEventPosition(t, e.$grid_data).y, r = e.$config.rowStore;
                        if ((i = i || 0) < 0) return r.$getRootId();
                        var a = Math.floor(i / e.getItemHeight());
                        return a > r.countVisible() - 1 ? r.$getRootId() : r.getIdByIndex(a)
                    }(t), r = null, o = e.$config.rowStore, c = !e.$getConfig().order_branch_free,
                    d = n.getRelativeEventPosition(t, e.$grid_data).y;
                return i !== o.$getRootId() && (r = (d - e.getItemTop(i)) / e.getItemHeight()), c ? a(l.config.id, i, r, d, o, l.config.level) : s(l.config.id, i, r, d, o)
            }
        }
    }
}, function (t, e, i) {
    var n = i(1);
    t.exports = {
        init: function (t, e) {
            var i = t.$services.getService("dnd");
            if (e.$config.bind && t.getDatastore(e.$config.bind)) {
                var r = new i(e.$grid_data, {updates_per_second: 60});
                t.defined(e.$getConfig().dnd_sensitivity) && (r.config.sensitivity = e.$getConfig().dnd_sensitivity), r.attachEvent("onBeforeDragStart", t.bind(function (i, n) {
                    var o = a(n);
                    if (!o) return !1;
                    t.hideQuickInfo && t._hideQuickInfo();
                    var l = o.getAttribute(e.$config.item_attribute), c = s().getItem(l);
                    return !t.isReadonly(c) && (r.config.initial_open_state = c.$open, !!t.callEvent("onRowDragStart", [l, n.target || n.srcElement, n]) && void 0)
                }, t)), r.attachEvent("onAfterDragStart", t.bind(function (t, i) {
                    var n = a(i);
                    r.config.marker.innerHTML = n.outerHTML;
                    var o = r.config.marker.firstChild;
                    o && (o.style.position = "static"), r.config.id = n.getAttribute(e.$config.item_attribute);
                    var l = s(), c = l.getItem(r.config.id);
                    r.config.index = l.getBranchIndex(r.config.id), r.config.parent = c.parent, c.$open = !1, c.$transparent = !0, this.refreshData()
                }, t)), r.lastTaskOfLevel = function (t) {
                    for (var e = null, i = s().getItems(), n = 0, r = i.length; n < r; n++) i[n].$level == t && (e = i[n]);
                    return e ? e.id : null
                }, r._getGridPos = t.bind(function (t) {
                    var i = n.getNodePosition(e.$grid_data), r = s(), a = i.x, o = t.pos.y - 10, l = e.$getConfig();
                    o < i.y && (o = i.y);
                    var c = r.countVisible() * l.row_height;
                    return o > i.y + c - l.row_height && (o = i.y + c - l.row_height), i.x = a, i.y = o, i
                }, t), r._getTargetY = t.bind(function (t) {
                    var i = n.getNodePosition(e.$grid_data), r = t.pageY - i.y + (e.$state.scrollTop || 0);
                    return r < 0 && (r = 0), r
                }, t), r._getTaskByY = t.bind(function (t, i) {
                    var n = e.$getConfig(), r = s();
                    t = t || 0;
                    var a = Math.floor(t / n.row_height);
                    return (a = i < a ? a - 1 : a) > r.countVisible() - 1 ? null : r.getIdByIndex(a)
                }, t), r.attachEvent("onDragMove", t.bind(function (t, i) {
                    var n = r.config, a = r._getGridPos(i), o = e.$getConfig(), l = s();
                    n.marker.style.left = a.x + 10 + "px", n.marker.style.top = a.y + "px";
                    var c = l.getItem(r.config.id), d = r._getTargetY(i), h = r._getTaskByY(d, l.getIndexById(c.id));

                    function u(t, e) {
                        return !l.isChildOf(g.id, e.id) && (t.$level == e.$level || o.order_branch_free)
                    }

                    if (l.exists(h) || (h = r.lastTaskOfLevel(o.order_branch_free ? c.$level : 0)) == r.config.id && (h = null), l.exists(h)) {
                        var g = l.getItem(h);
                        if (l.getIndexById(g.id) * o.row_height + o.row_height / 2 < d) {
                            var f = l.getIndexById(g.id), _ = l.getNext(g.id), p = l.getItem(_);
                            if (p) {
                                if (p.id == c.id) return o.order_branch_free && l.isChildOf(c.id, g.id) && 1 == l.getChildren(g.id).length ? void l.move(c.id, l.getBranchIndex(g.id) + 1, l.getParent(g.id)) : void 0;
                                g = p
                            } else if (_ = l.getIdByIndex(f), u(p = l.getItem(_), c) && p.id != c.id) return void l.move(c.id, -1, l.getParent(p.id))
                        } else if (o.order_branch_free && g.id != c.id && u(g, c)) {
                            if (!l.hasChild(g.id)) return g.$open = !0, void l.move(c.id, -1, g.id);
                            if (l.getIndexById(g.id) || o.row_height / 3 < d) return
                        }
                        f = l.getIndexById(g.id);
                        for (var v = l.getIdByIndex(f - 1), m = l.getItem(v), y = 1; (!m || m.id == g.id) && f - y >= 0;) v = l.getIdByIndex(f - y), m = l.getItem(v), y++;
                        if (c.id == g.id) return;
                        u(g, c) && c.id != g.id ? l.move(c.id, 0, 0, g.id) : g.$level != c.$level - 1 || l.getChildren(g.id).length ? m && u(m, c) && c.id != m.id && l.move(c.id, -1, l.getParent(m.id)) : l.move(c.id, 0, g.id)
                    }
                    return !0
                }, t)), r.attachEvent("onDragEnd", t.bind(function () {
                    var t = s(), e = t.getItem(r.config.id);
                    e.$transparent = !1, e.$open = r.config.initial_open_state, !1 === this.callEvent("onBeforeRowDragEnd", [r.config.id, r.config.parent, r.config.index]) ? (t.move(r.config.id, r.config.index, r.config.parent), e.$drop_target = null) : this.callEvent("onRowDragEnd", [r.config.id, e.$drop_target]), t.refresh(e.id)
                }, t))
            }

            function a(t) {
                return n.locateAttribute(t, e.$config.item_attribute)
            }

            function s() {
                return t.getDatastore(e.$config.bind)
            }
        }
    }
}, function (t, e, i) {
    var n = i(0), r = i(93), a = i(92), s = function (t) {
        return {
            onCreated: function (e) {
                e.$config = n.mixin(e.$config, {bind: "task"}), "grid" == e.$config.id && (this.extendGantt(e), t.ext.inlineEditors = t.ext._inlineEditors.createEditors(e), t.ext.inlineEditors.init()), this._mouseDelegates = i(10)(t)
            }, onInitialized: function (e) {
                var i = e.$getConfig();
                i.order_branch && ("marker" == i.order_branch ? a.init(e.$gantt, e) : r.init(e.$gantt, e)), this.initEvents(e, t), "grid" == e.$config.id && this.extendDom(e)
            }, onDestroyed: function (e) {
                "grid" == e.$config.id && t.ext.inlineEditors.destructor(), this.clearEvents(e, t)
            }, initEvents: function (t, e) {
                this._mouseDelegates.delegate("click", "gantt_row", e.bind(function (i, n, r) {
                    var a = t.$getConfig();
                    if (null !== n) {
                        var s = this.getTask(n);
                        a.scroll_on_click && !e._is_icon_open_click(i) && this.showDate(s.start_date), e.callEvent("onTaskRowClick", [n, r])
                    }
                }, e), t.$grid), this._mouseDelegates.delegate("click", "gantt_grid_head_cell", e.bind(function (i, n, r) {
                    var a = r.getAttribute("data-column-id");
                    if (e.callEvent("onGridHeaderClick", [a, i])) {
                        var s = t.$getConfig();
                        if ("add" != a) {
                            if (s.sort) {
                                for (var o, l = a, c = 0; c < s.columns.length; c++) if (s.columns[c].name == a) {
                                    o = s.columns[c];
                                    break
                                }
                                if (o && void 0 !== o.sort && !0 !== o.sort && !(l = o.sort)) return;
                                var d = this._sort && this._sort.direction && this._sort.name == a ? this._sort.direction : "desc";
                                d = "desc" == d ? "asc" : "desc", this._sort = {
                                    name: a,
                                    direction: d
                                }, this.sort(l, "desc" == d)
                            }
                        } else e.$services.getService("mouseEvents").callHandler("click", "gantt_add", t.$grid, [i, s.root_id])
                    }
                }, e), t.$grid), this._mouseDelegates.delegate("click", "gantt_add", e.bind(function (i, n, r) {
                    if (!t.$getConfig().readonly) return this.createTask({}, n || e.config.root_id), !1
                }, e), t.$grid)
            }, clearEvents: function (t, e) {
                this._mouseDelegates.destructor(), this._mouseDelegates = null
            }, extendDom: function (e) {
                t.$grid = e.$grid, t.$grid_scale = e.$grid_scale, t.$grid_data = e.$grid_data
            }, extendGantt: function (e) {
                t.getGridColumns = t.bind(e.getGridColumns, e), e.attachEvent("onColumnResizeStart", function () {
                    return t.callEvent("onColumnResizeStart", arguments)
                }), e.attachEvent("onColumnResize", function () {
                    return t.callEvent("onColumnResize", arguments)
                }), e.attachEvent("onColumnResizeEnd", function () {
                    return t.callEvent("onColumnResizeEnd", arguments)
                }), e.attachEvent("onColumnResizeComplete", function (e, i) {
                    t.config.grid_width = i
                })
            }
        }
    };
    t.exports = s
}, function (t, e, i) {
    var n = i(3);
    t.exports = function (t) {
        return function (e, i) {
            var r = i.getGridColumns(), a = i.$getConfig(), s = i.$getTemplates(), o = i.$config.rowStore;
            a.rtl && (r = r.reverse());
            for (var l = [], c = 0; c < r.length; c++) {
                var d, h, u, g = c == r.length - 1, f = r[c];
                "add" == f.name ? (h = "<div " + (y = t._waiAria.gridAddButtonAttrString(f)) + " class='gantt_add'></div>", u = "") : (h = f.template ? f.template(e) : e[f.name], n.isDate(h) && (h = s.date_grid(h, e)), u = h, h = "<div class='gantt_tree_content'>" + h + "</div>");
                var _ = "gantt_cell" + (g ? " gantt_last_cell" : ""), p = [];
                if (f.tree) {
                    for (var v = 0; v < e.$level; v++) p.push(s.grid_indent(e));
                    o.hasChild(e.id) && !t.isSplitTask(e) ? (p.push(s.grid_open(e)), p.push(s.grid_folder(e))) : (p.push(s.grid_blank(e)), p.push(s.grid_file(e)))
                }
                var m = "width:" + (f.width - (g ? 1 : 0)) + "px;";
                this.defined(f.align) && (m += "text-align:" + f.align + ";");
                var y = t._waiAria.gridCellAttrString(f, u);
                p.push(h), a.rtl && (p = p.reverse()), d = "<div class='" + _ + "' data-column-index='" + c + "' data-column-name='" + f.name + "' style='" + m + "' " + y + ">" + p.join("") + "</div>", l.push(d)
            }
            if (_ = t.getGlobalTaskIndex(e.id) % 2 == 0 ? "" : " odd", _ += e.$transparent ? " gantt_transparent" : "", _ += e.$dataprocessor_class ? " " + e.$dataprocessor_class : "", s.grid_row_class) {
                var k = s.grid_row_class.call(t, e.start_date, e.end_date, e);
                k && (_ += " " + k)
            }
            o.getSelectedId() == e.id && (_ += " gantt_selected");
            var b = document.createElement("div");
            b.className = "gantt_row" + _ + " gantt_row_" + t.getTaskType(e.type);
            var $ = i.getItemHeight();
            return b.style.height = $ + "px", b.style.lineHeight = $ + "px", a.smart_rendering && (b.style.position = "absolute", b.style.left = "0px", b.style.top = i.getItemTop(e.id) + "px"), i.$config.item_attribute && b.setAttribute(i.$config.item_attribute, e.id), t._waiAria.taskRowAttr(e, b), b.innerHTML = l.join(""), b
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = {
            current_pos: null,
            dirs: {left: "left", right: "right", up: "up", down: "down"},
            path: [],
            clear: function () {
                this.current_pos = null, this.path = []
            },
            point: function (e) {
                this.current_pos = t.copy(e)
            },
            get_lines: function (t) {
                this.clear(), this.point(t[0]);
                for (var e = 1; e < t.length; e++) this.line_to(t[e]);
                return this.get_path()
            },
            line_to: function (e) {
                var i = t.copy(e), n = this.current_pos, r = this._get_line(n, i);
                this.path.push(r), this.current_pos = i
            },
            get_path: function () {
                return this.path
            },
            get_wrapper_sizes: function (t, e) {
                var i, n = e.$getConfig(), r = n.link_wrapper_width, a = t.y + (n.row_height - r) / 2;
                switch (t.direction) {
                    case this.dirs.left:
                        i = {top: a, height: r, lineHeight: r, left: t.x - t.size - r / 2, width: t.size + r};
                        break;
                    case this.dirs.right:
                        i = {top: a, lineHeight: r, height: r, left: t.x - r / 2, width: t.size + r};
                        break;
                    case this.dirs.up:
                        i = {top: a - t.size, lineHeight: t.size + r, height: t.size + r, left: t.x - r / 2, width: r};
                        break;
                    case this.dirs.down:
                        i = {top: a, lineHeight: t.size + r, height: t.size + r, left: t.x - r / 2, width: r}
                }
                return i
            },
            get_line_sizes: function (t, e) {
                var i, n = e.$getConfig(), r = n.link_line_width, a = n.link_wrapper_width, s = t.size + r;
                switch (t.direction) {
                    case this.dirs.left:
                    case this.dirs.right:
                        i = {height: r, width: s, marginTop: (a - r) / 2, marginLeft: (a - r) / 2};
                        break;
                    case this.dirs.up:
                    case this.dirs.down:
                        i = {height: s, width: r, marginTop: (a - r) / 2, marginLeft: (a - r) / 2}
                }
                return i
            },
            render_line: function (t, e, i) {
                var n = this.get_wrapper_sizes(t, i), r = document.createElement("div");
                r.style.cssText = ["top:" + n.top + "px", "left:" + n.left + "px", "height:" + n.height + "px", "width:" + n.width + "px"].join(";"), r.className = "gantt_line_wrapper";
                var a = this.get_line_sizes(t, i), s = document.createElement("div");
                return s.style.cssText = ["height:" + a.height + "px", "width:" + a.width + "px", "margin-top:" + a.marginTop + "px", "margin-left:" + a.marginLeft + "px"].join(";"), s.className = "gantt_link_line_" + t.direction, r.appendChild(s), r
            },
            _get_line: function (t, e) {
                var i = this.get_direction(t, e), n = {x: t.x, y: t.y, direction: this.get_direction(t, e)};
                return i == this.dirs.left || i == this.dirs.right ? n.size = Math.abs(t.x - e.x) : n.size = Math.abs(t.y - e.y), n
            },
            get_direction: function (t, e) {
                return e.x < t.x ? this.dirs.left : e.x > t.x ? this.dirs.right : e.y > t.y ? this.dirs.down : this.dirs.up
            }
        }, i = {
            path: [], clear: function () {
                this.path = []
            }, current: function () {
                return this.path[this.path.length - 1]
            }, point: function (e) {
                return e ? (this.path.push(t.copy(e)), e) : this.current()
            }, point_to: function (i, n, r) {
                r = r ? {x: r.x, y: r.y} : t.copy(this.point());
                var a = e.dirs;
                switch (i) {
                    case a.left:
                        r.x -= n;
                        break;
                    case a.right:
                        r.x += n;
                        break;
                    case a.up:
                        r.y -= n;
                        break;
                    case a.down:
                        r.y += n
                }
                return this.point(r)
            }, get_points: function (i, n) {
                var r = this.get_endpoint(i, n), a = t.config, s = r.e_y - r.y, o = r.e_x - r.x, l = e.dirs;
                this.clear(), this.point({x: r.x, y: r.y});
                var c = 2 * a.link_arrow_size, d = this.get_line_type(i, n.$getConfig()), h = r.e_x > r.x;
                if (d.from_start && d.to_start) this.point_to(l.left, c), h ? (this.point_to(l.down, s), this.point_to(l.right, o)) : (this.point_to(l.right, o), this.point_to(l.down, s)), this.point_to(l.right, c); else if (!d.from_start && d.to_start) if (h = r.e_x > r.x + 2 * c, this.point_to(l.right, c), h) o -= c, this.point_to(l.down, s), this.point_to(l.right, o); else {
                    o -= 2 * c;
                    var u = s > 0 ? 1 : -1;
                    this.point_to(l.down, u * (a.row_height / 2)), this.point_to(l.right, o), this.point_to(l.down, u * (Math.abs(s) - a.row_height / 2)), this.point_to(l.right, c)
                } else d.from_start || d.to_start ? d.from_start && !d.to_start && (h = r.e_x > r.x - 2 * c, this.point_to(l.left, c), h ? (o += 2 * c, u = s > 0 ? 1 : -1, this.point_to(l.down, u * (a.row_height / 2)), this.point_to(l.right, o), this.point_to(l.down, u * (Math.abs(s) - a.row_height / 2)), this.point_to(l.left, c)) : (o += c, this.point_to(l.down, s), this.point_to(l.right, o))) : (this.point_to(l.right, c), h ? (this.point_to(l.right, o), this.point_to(l.down, s)) : (this.point_to(l.down, s), this.point_to(l.right, o)), this.point_to(l.left, c));
                return this.path
            }, get_line_type: function (e, i) {
                var n = i.links, r = !1, a = !1;
                return e.type == n.start_to_start ? r = a = !0 : e.type == n.finish_to_finish ? r = a = !1 : e.type == n.finish_to_start ? (r = !1, a = !0) : e.type == n.start_to_finish ? (r = !0, a = !1) : t.assert(!1, "Invalid link type"), i.rtl && (r = !r, a = !a), {
                    from_start: r,
                    to_start: a
                }
            }, get_endpoint: function (e, i) {
                var r = i.$getConfig(), a = this.get_line_type(e, r), s = a.from_start, o = a.to_start,
                    l = t.getTask(e.source), c = t.getTask(e.target), d = n(l, i), h = n(c, i);
                return {x: s ? d.left : d.left + d.width, e_x: o ? h.left : h.left + h.width, y: d.top, e_y: h.top}
            }
        };

        function n(e, i) {
            var n = i.$getConfig(), r = i.getItemPosition(e);
            if (t.getTaskType(e.type) == n.types.milestone) {
                var a = t.getTaskHeight(), s = Math.sqrt(2 * a * a);
                r.left -= s / 2, r.width = s
            }
            return r
        }

        return function (n, r) {
            var a = r.$getConfig(), s = i.get_endpoint(n, r), o = s.e_y - s.y;
            if (!(s.e_x - s.x || o)) return null;
            var l = i.get_points(n, r), c = e.get_lines(l, r), d = document.createElement("div"), h = "gantt_task_link";
            n.color && (h += " gantt_link_inline_color");
            var u = t.templates.link_class ? t.templates.link_class(n) : "";
            u && (h += " " + u), a.highlight_critical_path && t.isCriticalLink && t.isCriticalLink(n) && (h += " gantt_critical_link"), d.className = h, r.$config.link_attribute && d.setAttribute(r.$config.link_attribute, n.id);
            for (var g = 0; g < c.length; g++) {
                g == c.length - 1 && (c[g].size -= a.link_arrow_size);
                var f = e.render_line(c[g], c[g + 1], r);
                n.color && (f.firstChild.style.backgroundColor = n.color), d.appendChild(f)
            }
            var _ = c[c.length - 1].direction, p = function (t, i, n) {
                var r = n.$getConfig(), a = document.createElement("div"), s = t.y, o = t.x, l = r.link_arrow_size,
                    c = r.row_height, d = "gantt_link_arrow gantt_link_arrow_" + i;
                switch (i) {
                    case e.dirs.right:
                        s -= (l - c) / 2, o -= l;
                        break;
                    case e.dirs.left:
                        s -= (l - c) / 2;
                        break;
                    case e.dirs.up:
                        o -= l;
                        break;
                    case e.dirs.down:
                        s += 2 * l, o -= l
                }
                return a.style.cssText = ["top:" + s + "px", "left:" + o + "px"].join(";"), a.className = d, a
            }(l[l.length - 1], _, r);
            return n.color && (p.style.borderColor = n.color), d.appendChild(p), t._waiAria.linkAttr(n, d), d
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        return function (e, i) {
            var n = i.$getConfig(), r = i.$getTemplates(), a = i.getScale(), s = a.count,
                o = document.createElement("div");
            if (n.show_task_cells) for (var l = 0; l < s; l++) {
                var c = a.width[l], d = "";
                if (c > 0) {
                    var h = document.createElement("div");
                    h.style.width = c + "px", d = "gantt_task_cell" + (l == s - 1 ? " gantt_last_cell" : ""), (g = r.task_cell_class(e, a.trace_x[l])) && (d += " " + g), h.className = d, o.appendChild(h)
                }
            }
            var u = t.getGlobalTaskIndex(e.id) % 2 != 0, g = r.task_row_class(e.start_date, e.end_date, e),
                f = "gantt_task_row" + (u ? " odd" : "") + (g ? " " + g : "");
            return i.$config.rowStore.getSelectedId() == e.id && (f += " gantt_selected"), o.className = f, n.smart_rendering && (o.style.position = "absolute", o.style.top = i.getItemTop(e.id) + "px", o.style.width = "100%"), o.style.height = n.row_height + "px", i.$config.item_attribute && o.setAttribute(i.$config.item_attribute, e.id), o
        }
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(21)(t);
        return function (i, n) {
            if (t.isSplitTask(i)) {
                for (var r = document.createElement("div"), a = t.getTaskPosition(i), s = t.getChildren(i.id), o = 0; o < s.length; o++) {
                    var l = t.getTask(s[o]), c = e(l, n);
                    if (c) {
                        var d = Math.floor((t.config.row_height - a.height) / 2);
                        c.style.top = a.top + d + "px", c.className += " gantt_split_child", r.appendChild(c)
                    }
                }
                return r
            }
            return !1
        }
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(6)(t), n = i(0);

        function r() {
            return e.apply(this, arguments) || this
        }

        function a(t, e) {
            for (var i = (t || "").split(e.delimiter || ","), n = 0; n < i.length; n++) {
                var r = i[n].trim();
                r ? i[n] = r : (i.splice(n, 1), n--)
            }
            return i.sort(), i
        }

        function s(t, e, i) {
            for (var n = t.$target, r = [], a = 0; a < n.length; a++) {
                var s = i.getLink(n[a]), o = i.getTask(s.source);
                r.push(i.getWBSCode(o))
            }
            return r.join((e.delimiter || ",") + " ")
        }

        function o(e, i) {
            var n = function (e, i) {
                var n = [];
                return i.forEach(function (i) {
                    var r = t.getTaskByWBSCode(i);
                    if (r) {
                        var a = {source: r.id, target: e, type: t.config.links.finish_to_start, lag: 0};
                        t.isLinkAllowed(a) && n.push(a)
                    }
                }), n
            }(e.id, i), r = {};
            e.$target.forEach(function (e) {
                var i = t.getLink(e);
                r[i.source + "_" + i.target] = i.id
            });
            var a = [];
            n.forEach(function (t) {
                var e = t.source + "_" + t.target;
                r[e] ? delete r[e] : a.push(t)
            });
            var s = [];
            for (var o in r) s.push(r[o]);
            return {add: a, remove: s}
        }

        return i(2)(r, e), n.mixin(r.prototype, {
            show: function (t, e, i, n) {
                var r = "<div><input type='text' name='" + e.name + "'></div>";
                n.innerHTML = r
            }, hide: function () {
            }, set_value: function (e, i, n, r) {
                this.get_input(r).value = s(e, n.editor, t)
            }, get_value: function (t, e, i) {
                return a(this.get_input(i).value || "", e.editor)
            }, save: function (e, i, n) {
                var r = o(t.getTask(e), this.get_value(e, i, n));
                (r.add.length || r.remove.length) && t.batchUpdate(function () {
                    r.add.forEach(function (e) {
                        t.addLink(e)
                    }), r.remove.forEach(function (e) {
                        t.deleteLink(e)
                    }), t.autoSchedule && t.autoSchedule()
                })
            }, is_changed: function (e, i, n, r) {
                var o = this.get_value(i, n, r), l = a(s(e, n.editor, t), n.editor);
                return o.join() !== l.join()
            }
        }, !0), r
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(6)(t), n = i(0), r = "%Y-%m-%d", a = null, s = null;
        // var e = i(6)(t), n = i(0), r = "%HH:%MM", a = null, s = null;

        function o() {
            return e.apply(this, arguments) || this
        }

        return i(2)(o, e), n.mixin(o.prototype, {
            show: function (e, i, n, o) {
                a || (a = t.date.date_to_str(r)), s || (s = t.date.str_to_date(r));
                var l = "<div style='width:140px'><input type='time' min='" + a(n.min || t.getState().min_date) + "' max='" + a(n.max || t.getState().max_date) + "' name='" + i.name + "'></div>";
                o.innerHTML = l
            }, set_value: function (t, e, i, n) {//设置时间
                var _hours_minutes=t.getHours()<10?("0"+t.getHours()):t.getHours();
                _hours_minutes+=":";
                _hours_minutes+=t.getMinutes()<10?("0"+t.getMinutes()):t.getMinutes();
                this.get_input(n).value = _hours_minutes;
            }, is_valid: function (t, e, i, n) {//获取时间后
                return !(!t || isNaN(t.getTime()))
            }, get_value: function (t, e, i) {//获取时间
                var n;
                try {


                    n = s(("1970-02-01 "+this.get_input(i).value) || "")
                } catch (t) {
                    n = null
                }
                var d=new Date();
                d.setFullYear(1970);
                d.setMonth(0);
                d.setDate(2);
                var _time=this.get_input(i).value.split(":");
                d.setHours(_time[0])
                d.setMinutes(_time[1])
                console.log(d);
                return d
            }
        }, !0), o
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(6)(t), n = i(0);
        function r() {
            return e.apply(this, arguments) || this
        }

        return i(2)(r, e), n.mixin(r.prototype, {
            show: function (t, e, i, n) {
                for (var r = "<div><select name='" + e.name + "'>", a = [], s = i.options || [], o = 0; o < s.length; o++) a.push("<option value='" + i.options[o].key + "'>" + s[o].label + "</option>");
                r += a.join("") + "</select></div>", n.innerHTML = r
            }, get_input: function (t) {
                return t.querySelector("select")
            }
        }, !0), r
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(6)(t), n = i(0);

        function r() {
            return e.apply(this, arguments) || this
        }

        return i(2)(r, e), n.mixin(r.prototype, {
            show: function (t, e, i, n) {
                var r = "<div><input type='number' min='" + (i.min || 0) + "' max='" + (i.max || 100) + "' name='" + e.name + "'></div>";
                n.innerHTML = r
            }, get_value: function (t, e, i) {
                return this.get_input(i).value || ""
            }, is_valid: function (t, e, i, n) {
                return !isNaN(parseInt(t, 10))
            }
        }, !0), r
    }
}, function (t, e, i) {
    t.exports = function (t) {
        var e = i(6)(t), n = i(0);

        function r() {
            return e.apply(this, arguments) || this
        }

        return i(2)(r, e), n.mixin(r.prototype, {
            show: function (t, e, i, n) {
                var r = "<div><input type='text' name='" + e.name + "'></div>";
                n.innerHTML = r
            }
        }, !0), r
    }
}, function (t, e) {
    t.exports = {
        init: function (t, e) {
            var i = t, n = e.$gantt, r = null, a = n.ext.keyboardNavigation;
            a.attachEvent("onBeforeFocus", function (e) {
                var n = t.locateCell(e);
                if (clearTimeout(r), n) {
                    var a = n.columnName, s = n.id, o = i.getState();
                    if (i.isVisible() && o.id == s && o.columnName === a) return !1
                }
                return !0
            }), a.attachEvent("onFocus", function (e) {
                var n = t.locateCell(e), a = t.getState();
                return clearTimeout(r), !n || n.id == a.id && n.columnName == a.columnName || i.isVisible() && i.save(), !0
            }), t.attachEvent("onHide", function () {
                clearTimeout(r)
            }), a.attachEvent("onBlur", function () {
                return r = setTimeout(function () {
                    i.save()
                }), !0
            }), n.attachEvent("onTaskDblClick", function (e, i) {
                var n = t.getState(), r = t.locateCell(i.target);
                return !r || !t.isVisible() || r.columnName != n.columnName
            }), n.attachEvent("onTaskClick", function (e, i) {
                if (n._is_icon_open_click(i)) return !0;
                var r = t.getState(), a = t.locateCell(i.target);
                return !a || !t.getEditorConfig(a.columnName) || (t.isVisible() && r.id == a.id && r.columnName == a.columnName || t.startEdit(a.id, a.columnName), !1)
            }), n.attachEvent("onEmptyClick", function () {
                return i.save(), !0
            }), a.attachEvent("onKeyDown", function (e, r) {
                var s = t.locateCell(r.target), o = !!s && t.getEditorConfig(s.columnName), l = t.getState(),
                    c = n.constants.KEY_CODES, d = r.keyCode, h = !1;
                switch (d) {
                    case c.ENTER:
                        t.isVisible() ? (t.save(), r.preventDefault(), h = !0) : o && !(r.ctrlKey || r.metaKey || r.shiftKey) && (i.startEdit(s.id, s.columnName), r.preventDefault(), h = !0);
                        break;
                    case c.ESC:
                        t.isVisible() && (t.hide(), r.preventDefault(), h = !0);
                        break;
                    case c.UP:
                    case c.DOWN:
                        break;
                    case c.LEFT:
                    case c.RIGHT:
                        "date" === l.editorType && (h = !0);
                        break;
                    case c.SPACE:
                        t.isVisible() && (h = !0), o && !t.isVisible() && (i.startEdit(s.id, s.columnName), r.preventDefault(), h = !0);
                        break;
                    case c.DELETE:
                        o && !t.isVisible() && (i.startEdit(s.id, s.columnName), h = !0);
                        break;
                    case c.TAB:
                        if (t.isVisible()) {
                            r.shiftKey ? t.editPrevCell(!0) : t.editNextCell(!0);
                            var u = t.getState();
                            u.id && a.focus({
                                type: "taskCell",
                                id: u.id,
                                column: u.columnName
                            }), r.preventDefault(), h = !0
                        }
                        break;
                    default:
                        if (t.isVisible()) h = !0; else if (d >= 48 && d <= 57 || d > 95 && d < 112 || d >= 64 && d <= 91 || d > 185 && d < 193 || d > 218 && d < 223) {
                            var g = e.modifiers, f = g.alt || g.ctrl || g.meta || g.shift;
                            g.alt || f && a.getCommandHandler(e, "taskCell") || o && !t.isVisible() && (i.startEdit(s.id, s.columnName), h = !0)
                        }
                }
                return !h
            })
        }, onShow: function (t, e, i) {
        }, onHide: function (t, e, i) {
            i.$gantt.focus()
        }, destroy: function () {
        }
    }
}, function (t, e) {
    t.exports = {
        init: function (t, e) {
            var i = e.$gantt;
            i.attachEvent("onTaskClick", function (e, n) {
                if (i._is_icon_open_click(n)) return !0;
                var r = t.getState(), a = t.locateCell(n.target);
                return !a || !t.getEditorConfig(a.columnName) || (t.isVisible() && r.id == a.id && r.columnName == a.columnName || t.startEdit(a.id, a.columnName), !1)
            }), i.attachEvent("onEmptyClick", function () {
                return t.hide(), !0
            }), i.attachEvent("onTaskDblClick", function (e, i) {
                var n = t.getState(), r = t.locateCell(i.target);
                return !r || !t.isVisible() || r.columnName != n.columnName
            })
        }, onShow: function (t, e, i) {
            if (!i.$getConfig().keyboard_navigation) {
                var n = i.$gantt;
                e.onkeydown = function (e) {
                    e = e || window.event;
                    var i = n.constants.KEY_CODES;
                    if (!(e.defaultPrevented || e.shiftKey && e.keyCode != i.TAB)) {
                        var r = !0;
                        switch (e.keyCode) {
                            case n.keys.edit_save:
                                t.save();
                                break;
                            case n.keys.edit_cancel:
                                t.hide();
                                break;
                            case i.TAB:
                                e.shiftKey ? t.editPrevCell(!0) : t.editNextCell(!0);
                                break;
                            default:
                                r = !1
                        }
                        r && e.preventDefault()
                    }
                }
            }
        }, onHide: function () {
        }, destroy: function () {
        }
    }
}, function (t, e, i) {
    var n = i(105), r = i(104);
    t.exports = function (t) {
        var e = null;
        return {
            setMapping: function (t) {
                e = t
            }, getMapping: function () {
                return e || (t.config.keyboard_navigation_cells ? r : n)
            }
        }
    }
}, function (t, e, i) {
    var n = i(106), r = i(103), a = i(102), s = i(101), o = i(100), l = i(99), c = i(0), d = i(1), h = i(4);

    function u(t) {
        t.config.editor_types = {
            text: new (r(t)),
            number: new (a(t)),
            select: new (s(t)),
            date: new (o(t)),
            predecessor: new (l(t))
        }
    }

    t.exports = function (t) {
        var e = n(t), i = {};
        h(i);
        var r = {
            init: u, createEditors: function (n) {
                function r(t, e) {
                    var i = function (t, e) {
                        for (var i = n.getItemTop(t), r = n.getItemHeight(t), a = n.getGridColumns(), s = 0, o = 0, l = 0; l < a.length; l++) {
                            if (a[l].name == e) {
                                o = a[l].width;
                                break
                            }
                            s += a[l].width
                        }
                        return {top: i, left: s, height: r, width: o}
                    }(t, e), r = document.createElement("div");
                    return r.className = "gantt_grid_editor_placeholder", r.setAttribute(n.$config.item_attribute, t), r.setAttribute("data-column-name", e), r.setAttribute("data-column-index", n.getColumnIndex(e)), r.style.cssText = ["top:" + i.top + "px", "left:" + i.left + "px", "width:" + i.width + "px", "height:" + i.height + "px"].join(";"), r
                }

                var a = [], s = null, o = {
                    _itemId: null,
                    _columnName: null,
                    _editor: null,
                    _editorType: null,
                    _placeholder: null,
                    locateCell: function (t) {
                        if (!d.isChildOf(t, n.$grid)) return null;
                        var e = d.locateAttribute(t, n.$config.item_attribute),
                            i = d.locateAttribute(t, "data-column-name");
                        if (i) {
                            var r = i.getAttribute("data-column-name");
                            return {id: e.getAttribute(n.$config.item_attribute), columnName: r}
                        }
                        return null
                    },
                    getEditorConfig: function (t) {
                        return n.getColumn(t).editor
                    },
                    init: function () {
                        var t = e.getMapping();
                        t.init && t.init(this, n), s = n.$gantt.getDatastore(n.$config.bind);
                        var i = this;
                        a.push(s.attachEvent("onIdChange", function (t, e) {
                            i._itemId == t && (i._itemId = e)
                        })), a.push(s.attachEvent("onStoreUpdated", function () {
                            n.$gantt.getState("batchUpdate").batch_update || i.isVisible() && !s.isVisible(i._itemId) && i.hide()
                        })), this.init = function () {
                        }
                    },
                    getState: function () {
                        return {
                            editor: this._editor,
                            editorType: this._editorType,
                            placeholder: this._placeholder,
                            id: this._itemId,
                            columnName: this._columnName
                        }
                    },
                    startEdit: function (t, e) {
                        if (this.isVisible() && this.save(), s.exists(t)) {
                            var i = {id: t, columnName: e};
                            !1 !== this.callEvent("onBeforeEditStart", [i]) && (this.show(i.id, i.columnName), this.setValue(), this.callEvent("onEditStart", [i]))
                        }
                    },
                    isVisible: function () {
                        return !(!this._editor || !d.isChildOf(this._placeholder, document.body))
                    },
                    show: function (t, i) {
                        this.isVisible() && this.save();
                        var a = {id: t, columnName: i}, s = n.getColumn(a.columnName), o = this.getEditorConfig(s.name);
                        if (o) {
                            var l = n.$getConfig().editor_types[o.type], c = r(a.id, a.columnName);
                            n.$grid_data.appendChild(c), l.show(a.id, s, o, c), this._editor = l, this._placeholder = c, this._itemId = a.id, this._columnName = a.columnName, this._editorType = o.type;
                            var d = e.getMapping();
                            d.onShow && d.onShow(this, c, n)
                        }
                    },
                    setValue: function () {
                        var t = this.getState(), e = t.id, i = t.columnName, r = n.getColumn(i), a = s.getItem(e),
                            o = this.getEditorConfig(i);
                        if (o) {
                            var l = a[o.map_to];
                            "auto" == o.map_to && (l = s.getItem(e)), this._editor.set_value(l, e, r, this._placeholder), this.focus()
                        }
                    },
                    focus: function () {
                        this._editor.focus(this._placeholder)
                    },
                    getValue: function () {
                        var t = n.getColumn(this._columnName);
                        return this._editor.get_value(this._itemId, t, this._placeholder)
                    },
                    _getItemValue: function () {
                        var e = this.getEditorConfig(this._columnName);
                        if (e) {
                            var i = t.getTask(this._itemId)[e.map_to];
                            return "auto" == e.map_to && (i = s.getItem(this._itemId)), i
                        }
                    },
                    isChanged: function () {
                        var t = n.getColumn(this._columnName), e = this._getItemValue();
                        return this._editor.is_changed(e, this._itemId, t, this._placeholder)
                    },
                    hide: function () {
                        if (this._itemId) {
                            var t = this._itemId, i = this._columnName, r = e.getMapping();
                            r.onHide && r.onHide(this, this._placeholder, n), this._itemId = null, this._columnName = null, this._editorType = null, this._placeholder && (this._editor && this._editor.hide(this._placeholder), this._editor = null, this._placeholder.parentNode && this._placeholder.parentNode.removeChild(this._placeholder), this._placeholder = null, this.callEvent("onEditEnd", [{
                                id: t,
                                columnName: i
                            }]))
                        }
                    },
                    save: function () {
                        if (this.isVisible() && s.exists(this._itemId) && this.isChanged()) {
                            var e = this._itemId, i = this._columnName;
                            if (s.exists(e)) {
                                var r = s.getItem(e), a = this.getEditorConfig(i), o = {
                                    id: e,
                                    columnName: i,
                                    newValue: this.getValue(),
                                    oldValue: this._getItemValue()
                                };
                                if (!1 !== this.callEvent("onBeforeSave", [o]) && this._editor.is_valid(o.newValue, o.id, o.columnName, this._placeholder)) {
                                    var l = a.map_to, c = o.newValue;
                                    "auto" != l ? (r[l] = c, "duration" == l ? r.end_date = t.calculateEndDate(r) : "end_date" == l ? r.start_date = t.calculateEndDate({
                                        start_date: r.end_date,
                                        duration: -r.duration,
                                        task: r
                                    }) : "start_date" == l && (r.end_date = t.calculateEndDate(r)), s.updateItem(e)) : this._editor.save(e, n.getColumn(i), this._placeholder), this.callEvent("onSave", [o])
                                }
                                this.hide()
                            }
                        } else this.hide()
                    },
                    _findEditableCell: function (t, e) {
                        var i = t, r = n.getGridColumns()[i], a = r ? r.name : null;
                        if (a) {
                            for (; a && !this.getEditorConfig(a);) a = this._findEditableCell(t + e, e);
                            return a
                        }
                        return null
                    },
                    getNextCell: function (t) {
                        return this._findEditableCell(n.getColumnIndex(this._columnName) + t, t)
                    },
                    getFirstCell: function () {
                        return this._findEditableCell(0, 1)
                    },
                    getLastCell: function () {
                        return this._findEditableCell(n.getGridColumns().length - 1, -1)
                    },
                    editNextCell: function (t) {
                        var e = this.getNextCell(1);
                        if (e) {
                            var i = this.getNextCell(1);
                            i && this.getEditorConfig(i) && this.startEdit(this._itemId, i)
                        } else if (t && this.moveRow(1)) {
                            var n = this.moveRow(1);
                            (e = this.getFirstCell()) && this.getEditorConfig(e) && this.startEdit(n, e)
                        }
                    },
                    editPrevCell: function (t) {
                        var e = this.getNextCell(-1);
                        if (e) {
                            var i = this.getNextCell(-1);
                            i && this.getEditorConfig(i) && this.startEdit(this._itemId, i)
                        } else if (t && this.moveRow(-1)) {
                            var n = this.moveRow(-1);
                            (e = this.getLastCell()) && this.getEditorConfig(e) && this.startEdit(n, e)
                        }
                    },
                    moveRow: function (e) {
                        return e > 0 ? t.getNext(this._itemId) : t.getPrev(this._itemId)
                    },
                    editNextRow: function () {
                        var t = this.getNextCell(1);
                        t && this.startEdit(t, this._columnName)
                    },
                    editPrevRow: function () {
                        var t = this.getNextCell(-1);
                        t && this.startEdit(t, this._columnName)
                    },
                    destructor: function () {
                        a.forEach(function (t) {
                            s.detachEvent(t)
                        }), s = null, this.hide(), this.detachAllEvents()
                    }
                };
                return c.mixin(o, e), c.mixin(o, i), o
            }
        };
        return c.mixin(r, e), c.mixin(r, i), r
    }
}, function (t, e) {
    t.exports = function (t, e) {
        return {
            init: function () {
            }, doOnRender: function () {
            }
        }
    }
}, function (t, e) {
    t.exports = {
        create: function () {
            return {
                render: function () {
                }
            }
        }
    }
}, function (t, e, i) {
    var n = i(2), r = i(1), a = i(0), s = i(12), o = function (t) {
        "use strict";

        function e(e, i, n, r) {
            var s = t.apply(this, arguments) || this;
            this.$config = a.mixin(i, {scroll: "x"}), s._scrollHorizontalHandler = a.bind(s._scrollHorizontalHandler, s), s._scrollVerticalHandler = a.bind(s._scrollVerticalHandler, s), s._outerScrollVerticalHandler = a.bind(s._outerScrollVerticalHandler, s), s._outerScrollHorizontalHandler = a.bind(s._outerScrollHorizontalHandler, s), s._mouseWheelHandler = a.bind(s._mouseWheelHandler, s), this.$config.hidden = !0;
            var o = r.config.scroll_size;
            return r.env.isIE && (o += 1), this._isHorizontal() ? (s.$config.height = o, s.$parent.$config.height = o) : (s.$config.width = o, s.$parent.$config.width = o), this.$config.scrollPosition = 0, s.$name = "scroller", s
        }

        return n(e, t), e.prototype.init = function (t) {
            t.innerHTML = this.$toHTML(), this.$view = t.firstChild, this.$view || this.init(), this._isVertical() ? this._initVertical() : this._initHorizontal(), this._initMouseWheel(), this._initLinkedViews()
        }, e.prototype.$toHTML = function () {
            return "<div class='gantt_layout_cell " + (this._isHorizontal() ? "gantt_hor_scroll" : "gantt_ver_scroll") + "'><div style='" + (this._isHorizontal() ? "width:2000px" : "height:2000px") + "'></div></div>"
        }, e.prototype._getRootParent = function () {
            for (var t = this.$parent; t && t.$parent;) t = t.$parent;
            if (t) return t
        }, e.prototype._eachView = function () {
            var t = [];
            return function t(e, i) {
                if (i.push(e), e.$cells) for (var n = 0; n < e.$cells.length; n++) t(e.$cells[n], i)
            }(this._getRootParent(), t), t
        }, e.prototype._getLinkedViews = function () {
            for (var t = this._eachView(), e = [], i = 0; i < t.length; i++) t[i].$config && (this._isVertical() && t[i].$config.scrollY == this.$id || this._isHorizontal() && t[i].$config.scrollX == this.$id) && e.push(t[i]);
            return e
        }, e.prototype._initHorizontal = function () {
            this.$scroll_hor = this.$view, this.$domEvents.attach(this.$view, "scroll", this._scrollHorizontalHandler)
        }, e.prototype._initLinkedViews = function () {
            for (var t = this._getLinkedViews(), e = this._isVertical() ? "gantt_layout_outer_scroll gantt_layout_outer_scroll_vertical" : "gantt_layout_outer_scroll gantt_layout_outer_scroll_horizontal", i = 0; i < t.length; i++) r.addClassName(t[i].$view || t[i].getNode(), e)
        }, e.prototype._initVertical = function () {
            this.$scroll_ver = this.$view, this.$domEvents.attach(this.$view, "scroll", this._scrollVerticalHandler)
        }, e.prototype._updateLinkedViews = function () {
        }, e.prototype._initMouseWheel = function () {
            s.isFF ? this.$domEvents.attach(this._getRootParent().$view, "wheel", this._mouseWheelHandler) : this.$domEvents.attach(this._getRootParent().$view, "mousewheel", this._mouseWheelHandler)
        }, e.prototype.scrollHorizontally = function (t) {
            if (!this._scrolling) {
                this._scrolling = !0, this.$scroll_hor.scrollLeft = t, this.$config.codeScrollLeft = t, t = this.$scroll_hor.scrollLeft;
                for (var e = this._getLinkedViews(), i = 0; i < e.length; i++) e[i].scrollTo && e[i].scrollTo(t, void 0);
                var n = this.$config.scrollPosition;
                this.$config.scrollPosition = t, this.callEvent("onScroll", [n, t, this.$config.scroll]), this._scrolling = !1
            }
        }, e.prototype.scrollVertically = function (t) {
            if (!this._scrolling) {
                this._scrolling = !0, this.$scroll_ver.scrollTop = t, t = this.$scroll_ver.scrollTop;
                for (var e = this._getLinkedViews(), i = 0; i < e.length; i++) e[i].scrollTo && e[i].scrollTo(void 0, t);
                var n = this.$config.scrollPosition;
                this.$config.scrollPosition = t, this.callEvent("onScroll", [n, t, this.$config.scroll]), this._scrolling = !1
            }
        }, e.prototype._isVertical = function () {
            return "y" == this.$config.scroll
        }, e.prototype._isHorizontal = function () {
            return "x" == this.$config.scroll
        }, e.prototype._scrollHorizontalHandler = function (t) {
            if (!this._isVertical() && !this._scrolling) {
                if (new Date - (this._wheel_time || 0) < 100) return !0;
                if (!this.$gantt._touch_scroll_active) {
                    var e = this.$scroll_hor.scrollLeft;
                    this.scrollHorizontally(e), this._oldLeft = this.$scroll_hor.scrollLeft
                }
            }
        }, e.prototype._outerScrollHorizontalHandler = function (t) {
            this._isVertical()
        }, e.prototype.show = function () {
            this.$parent.show()
        }, e.prototype.hide = function () {
            this.$parent.hide()
        }, e.prototype._getScrollSize = function () {
            for (var t, e = 0, i = 0, n = this._isHorizontal(), r = this._getLinkedViews(), a = n ? "scrollWidth" : "scrollHeight", s = n ? "contentX" : "contentY", o = n ? "x" : "y", l = this._getScrollOffset(), c = 0; c < r.length; c++) if ((t = r[c]) && t.$content && t.$content.getSize && !t.$config.hidden) {
                var d, h = t.$content.getSize();
                if (d = h.hasOwnProperty(a) ? h[a] : h[s], l) h[s] > h[o] && h[s] > e && d > h[o] - l + 2 && (e = d + (n ? 0 : 2), i = h[o]); else {
                    var u = Math.max(h[s] - d, 0);
                    (d += u) > Math.max(h[o] - u, 0) && d > e && (e = d, i = h[o])
                }
            }
            return {outerScroll: i, innerScroll: e}
        }, e.prototype.scroll = function (t) {
            this._isHorizontal() ? this.scrollHorizontally(t) : this.scrollVertically(t)
        }, e.prototype.getScrollState = function () {
            return {
                visible: this.isVisible(),
                direction: this.$config.scroll,
                size: this.$config.outerSize,
                scrollSize: this.$config.scrollSize || 0,
                position: this.$config.scrollPosition || 0
            }
        }, e.prototype.setSize = function (e, i) {
            t.prototype.setSize.apply(this, arguments);
            var n = this._getScrollSize(),
                r = (this._isVertical() ? i : e) - this._getScrollOffset() + (this._isHorizontal() ? 1 : 0);
            n.innerScroll && r > n.outerScroll && (n.innerScroll += r - n.outerScroll), this.$config.scrollSize = n.innerScroll, this.$config.width = e, this.$config.height = i, this._setScrollSize(n.innerScroll)
        }, e.prototype.isVisible = function () {
            return !(!this.$parent || !this.$parent.$view.parentNode)
        }, e.prototype.shouldShow = function () {
            var t = this._getScrollSize();
            return !(!t.innerScroll && this.$parent && this.$parent.$view.parentNode) && !(!t.innerScroll || this.$parent && this.$parent.$view.parentNode)
        }, e.prototype.shouldHide = function () {
            return !(this._getScrollSize().innerScroll || !this.$parent || !this.$parent.$view.parentNode)
        }, e.prototype.toggleVisibility = function () {
            this.shouldHide() ? this.hide() : this.shouldShow() && this.show()
        }, e.prototype._getScaleOffset = function (t) {
            var e = 0;
            return !t || "timeline" != t.$config.view && "grid" != t.$config.view || (e = t.$content.$getConfig().scale_height), e
        }, e.prototype._getScrollOffset = function () {
            var t = 0;
            if (this._isVertical()) {
                var e = this.$parent.$parent;
                t = Math.max(this._getScaleOffset(e.getPrevSibling(this.$parent.$id)), this._getScaleOffset(e.getNextSibling(this.$parent.$id)))
            } else for (var i = this._getLinkedViews(), n = 0; n < i.length; n++) {
                var r = i[n].$parent.$cells, a = r[r.length - 1];
                if (a && "scrollbar" == a.$config.view && !1 === a.$config.hidden) {
                    t = a.$config.width;
                    break
                }
            }
            return t || 0
        }, e.prototype._setScrollSize = function (t) {
            var e = this._isHorizontal() ? "width" : "height",
                i = this._isHorizontal() ? this.$scroll_hor : this.$scroll_ver, n = this._getScrollOffset(),
                a = i.firstChild;
            n ? this._isVertical() ? (this.$config.outerSize = this.$config.height - n + 3, i.style.height = this.$config.outerSize + "px", i.style.top = n - 1 + "px", r.addClassName(i, this.$parent._borders.top), r.addClassName(i.parentNode, "gantt_task_vscroll")) : (this.$config.outerSize = this.$config.width - n + 1, i.style.width = this.$config.outerSize + "px") : (i.style.top = "auto", r.removeClassName(i, this.$parent._borders.top), r.removeClassName(i.parentNode, "gantt_task_vscroll"), this.$config.outerSize = this.$config.height), a.style[e] = t + "px"
        }, e.prototype._scrollVerticalHandler = function (t) {
            if (!this._scrollHorizontalHandler() && !this._scrolling && !this.$gantt._touch_scroll_active) {
                var e = this.$scroll_ver.scrollTop;
                e != this._oldTop && (this.scrollVertically(e), this._oldTop = this.$scroll_ver.scrollTop)
            }
        }, e.prototype._outerScrollVerticalHandler = function (t) {
            this._scrollHorizontalHandler()
        }, e.prototype._checkWheelTarget = function (t) {
            for (var e = this._getLinkedViews().concat(this), i = 0; i < e.length; i++) {
                var n = e[i].$view;
                if (r.isChildOf(t, n)) return !0
            }
            return !1
        }, e.prototype._mouseWheelHandler = function (t) {
            var e = t.target || t.srcElement;
            if (this._checkWheelTarget(e)) {
                this._wheel_time = new Date;
                var i = {}, n = s.isFF, r = n ? -20 * t.deltaX : 2 * t.wheelDeltaX,
                    a = n ? -40 * t.deltaY : t.wheelDelta;
                if (!t.shiftKey || t.deltaX || t.wheelDeltaX || (r = 2 * a, a = 0), r && Math.abs(r) > Math.abs(a)) {
                    if (this._isVertical()) return;
                    if (i.x) return !0;
                    if (!this.$scroll_hor || !this.$scroll_hor.offsetWidth) return !0;
                    var o = r / -40, l = this._oldLeft, c = l + 30 * o;
                    if (this.scrollHorizontally(c), this.$scroll_hor.scrollLeft = c, l == this.$scroll_hor.scrollLeft) return !0;
                    this._oldLeft = this.$scroll_hor.scrollLeft
                } else {
                    if (this._isHorizontal()) return;
                    if (i.y) return !0;
                    if (!this.$scroll_ver || !this.$scroll_ver.offsetHeight) return !0;
                    o = a / -40;
                    void 0 === a && (o = t.detail);
                    var d = this._oldTop, h = this.$scroll_ver.scrollTop + 30 * o;
                    if (this.scrollVertically(h), this.$scroll_ver.scrollTop = h, d == this.$scroll_ver.scrollTop) return !0;
                    this._oldTop = this.$scroll_ver.scrollTop
                }
                return t.preventDefault && t.preventDefault(), t.cancelBubble = !0, !1
            }
        }, e
    }(i(7));
    t.exports = o
}, function (t, e) {
    t.exports = null
}, function (t, e, i) {
    var n = i(2), r = i(0), a = function (t) {
        "use strict";

        function e(e, i, n) {
            var a = t.apply(this, arguments) || this;
            if (i.view) {
                i.id && (this.$id = r.uid());
                var s = r.copy(i);
                if (delete s.config, delete s.templates, this.$content = this.$factory.createView(i.view, this, s, this), !this.$content) return !1
            }
            return a.$name = "viewCell", a
        }

        return n(e, t), e.prototype.destructor = function () {
            this.clear(), t.prototype.destructor.call(this)
        }, e.prototype.clear = function () {
            if (this.$initialized = !1, this.$content) {
                var e = this.$content.unload || this.$content.destructor;
                e && e.call(this.$content)
            }
            t.prototype.clear.call(this)
        }, e.prototype.scrollTo = function (e, i) {
            this.$content && this.$content.scrollTo ? this.$content.scrollTo(e, i) : t.prototype.scrollTo.call(this, e, i)
        }, e.prototype._setContentSize = function (t, e) {
            var i = this._getBorderSizes(), n = t + i.horizontal, r = e + i.vertical;
            this.$config.width = n, this.$config.height = r
        }, e.prototype.setSize = function (e, i) {
            if (t.prototype.setSize.call(this, e, i), !this.$preResize && this.$content && !this.$initialized) {
                this.$initialized = !0;
                var n = this.$view.childNodes[0], r = this.$view.childNodes[1];
                r || (r = n), this.$content.init(r)
            }
        }, e.prototype.setContentSize = function () {
            !this.$preResize && this.$content && this.$initialized && this.$content.setSize(this.$lastSize.contentX, this.$lastSize.contentY)
        }, e.prototype.getContentSize = function () {
            var e = t.prototype.getContentSize.call(this);
            if (this.$content && this.$initialized) {
                var i = this.$content.getSize();
                e.width = void 0 === i.contentX ? i.width : i.contentX, e.height = void 0 === i.contentY ? i.height : i.contentY
            }
            var n = this._getBorderSizes();
            return e.width += n.horizontal, e.height += n.vertical, e
        }, e
    }(i(7));
    t.exports = a
}, function (t, e, i) {
    var n = i(2), r = i(25), a = i(7), s = function (t) {
        "use strict";

        function e(e, i, n) {
            for (var r = t.apply(this, arguments) || this, a = 0; a < r.$cells.length; a++) r.$cells[a].$config.hidden = 0 !== a;
            return r.$cell = r.$cells[0], r.$name = "viewLayout", r
        }

        return n(e, t), e.prototype.cell = function (e) {
            var i = t.prototype.cell.call(this, e);
            return i.$view || this.$fill(null, this), i
        }, e.prototype.moveView = function (t) {
            var e = this.$view;
            this.$cell && (this.$cell.$config.hidden = !0, e.removeChild(this.$cell.$view)), this.$cell = t, e.appendChild(t.$view)
        }, e.prototype.setSize = function (t, e) {
            a.prototype.setSize.call(this, t, e)
        }, e.prototype.setContentSize = function () {
            var t = this.$lastSize;
            this.$cell.setSize(t.contentX, t.contentY)
        }, e.prototype.getSize = function () {
            var e = t.prototype.getSize.call(this);
            if (this.$cell) {
                var i = this.$cell.getSize();
                if (this.$config.byMaxSize) for (var n = 0; n < this.$cells.length; n++) {
                    var r = this.$cells[n].getSize();
                    for (var a in i) i[a] = Math.max(i[a], r[a])
                }
                for (var s in e) e[s] = e[s] || i[s];
                e.gravity = Math.max(e.gravity, i.gravity)
            }
            return e
        }, e
    }(r);
    t.exports = s
}, function (t, e) {
    t.exports = function (t) {
        var e = t.$services, i = {}, n = {};

        function r(r, a, s) {
            if (n[r]) return n[r];
            a.renderer || t.assert(!1, "Invalid renderer call");
            var o = a.filter;
            return s && s.setAttribute(e.config().layer_attribute, !0), n[r] = {
                render_item: function (e, i) {
                    if (i = i || s, !o || o(e)) {
                        var n = function (t) {
                            return a.renderer.call(this, t, a.host)
                        }.call(t, e);
                        this.append(e, n, i)
                    } else this.remove_item(e.id)
                }, clear: function (t) {
                    this.rendered = i[r] = {}, a.append || this.clear_container(t)
                }, clear_container: function (t) {
                    (t = t || s) && (t.innerHTML = "")
                }, render_items: function (t, e) {
                    e = e || s;
                    var i = document.createDocumentFragment();
                    this.clear(e);
                    for (var n = 0, r = t.length; n < r; n++) this.render_item(t[n], i);
                    e.appendChild(i)
                }, append: function (t, e, i) {
                    e ? (this.rendered[t.id] && this.rendered[t.id].parentNode ? this.replace_item(t.id, e) : i.appendChild(e), this.rendered[t.id] = e) : this.rendered[t.id] && this.remove_item(t.id)
                }, replace_item: function (t, e) {
                    var i = this.rendered[t];
                    i && i.parentNode && i.parentNode.replaceChild(e, i), this.rendered[t] = e
                }, remove_item: function (t) {
                    this.hide(t), delete this.rendered[t]
                }, hide: function (t) {
                    var e = this.rendered[t];
                    e && e.parentNode && e.parentNode.removeChild(e)
                }, restore: function (t) {
                    var e = this.rendered[t.id];
                    e ? e.parentNode || this.append(t, e, s) : this.render_item(t, s)
                }, change_id: function (t, e) {
                    this.rendered[e] = this.rendered[t], delete this.rendered[t]
                }, rendered: i[r], node: s, destructor: function () {
                    this.clear(), delete n[r], delete i[r]
                }
            }, n[r]
        }

        return {
            getRenderer: r, clearRenderers: function () {
                for (var t in n) r(t).destructor()
            }
        }
    }
}, function (t, e, i) {
    var n = i(114), r = i(0), a = i(1);

    function s(t) {
        return t instanceof Array || (t = Array.prototype.slice.call(arguments, 0)), function (e) {
            for (var i = !0, n = 0, r = t.length; n < r; n++) {
                var a = t[n];
                a && (i = i && !1 !== a(e.id, e))
            }
            return i
        }
    }

    t.exports = function (t) {
        var e = n(t);
        return {
            createGroup: function (i, n, o) {
                var l = {
                    tempCollection: [], renderers: {}, container: i, filters: [], getLayers: function () {
                        this._add();
                        var t = [];
                        for (var e in this.renderers) t.push(this.renderers[e]);
                        return t
                    }, getLayer: function (t) {
                        return this.renderers[t]
                    }, _add: function (t) {
                        t && (t.id = t.id || r.uid(), this.tempCollection.push(t));
                        for (var i = this.container(), s = this.tempCollection, o = 0; o < s.length; o++) if (t = s[o], this.container() || t && t.container && a.isChildOf(t.container, document.body)) {
                            var l = t.container, c = t.id, d = t.topmost;
                            if (!l.parentNode) if (d) i.appendChild(l); else {
                                var h = n ? n() : i.firstChild;
                                h ? i.insertBefore(l, h) : i.appendChild(l)
                            }
                            this.renderers[c] = e.getRenderer(c, t, l), this.tempCollection.splice(o, 1), o--
                        }
                    }, addLayer: function (t) {
                        return t && ("function" == typeof t && (t = {renderer: t}), void 0 === t.filter ? t.filter = s(o || []) : t.filter instanceof Array && (t.filter.push(o), t.filter = s(t.filter)), t.container || (t.container = document.createElement("div"))), this._add(t), t ? t.id : void 0
                    }, eachLayer: function (t) {
                        for (var e in this.renderers) t(this.renderers[e])
                    }, removeLayer: function (t) {
                        this.renderers[t] && (this.renderers[t].destructor(), delete this.renderers[t])
                    }, clear: function () {
                        for (var t in this.renderers) this.renderers[t].destructor();
                        this.renderers = {}
                    }
                };
                return t.attachEvent("onDestroy", function () {
                    l.clear(), l = null
                }), l
            }
        }
    }
}, function (t, e, i) {
    var n = i(115);
    t.exports = function (t) {
        var e = n(t);
        return {
            getDataRender: function (e) {
                return t.$services.getService("layer:" + e) || null
            }, createDataRender: function (i) {
                var n = i.name, r = i.defaultContainer, a = i.defaultContainerSibling,
                    s = e.createGroup(r, a, function (t, e) {
                        if (!s.filters) return !0;
                        for (var i = 0; i < s.filters.length; i++) if (!1 === s.filters[i](t, e)) return !1
                    });
                return t.$services.setService("layer:" + n, function () {
                    return s
                }), t.attachEvent("onGanttReady", function () {
                    s.addLayer()
                }), s
            }, init: function () {
                var e = this.createDataRender({
                    name: "task", defaultContainer: function () {
                        return t.$task_data ? t.$task_data : t.$ui.getView("timeline") ? t.$ui.getView("timeline").$task_data : void 0
                    }, defaultContainerSibling: function () {
                        return t.$task_links ? t.$task_links : t.$ui.getView("timeline") ? t.$ui.getView("timeline").$task_links : void 0
                    }, filter: function (t) {
                    }
                }, t), i = this.createDataRender({
                    name: "link", defaultContainer: function () {
                        return t.$task_data ? t.$task_data : t.$ui.getView("timeline") ? t.$ui.getView("timeline").$task_data : void 0
                    }
                }, t);
                return {
                    addTaskLayer: function (t) {
                        return e.addLayer(t)
                    }, _getTaskLayers: function () {
                        return e.getLayers()
                    }, removeTaskLayer: function (t) {
                        e.removeLayer(t)
                    }, _clearTaskLayers: function () {
                        e.clear()
                    }, addLinkLayer: function (t) {
                        return i.addLayer(t)
                    }, _getLinkLayers: function () {
                        return i.getLayers()
                    }, removeLinkLayer: function (t) {
                        i.removeLayer(t)
                    }, _clearLinkLayers: function () {
                        i.clear()
                    }
                }
            }
        }
    }
}, function (t, e, i) {
    var n = function (t) {
        return function (e) {
            var i = {click: {}, doubleclick: {}, contextMenu: {}};

            function n(t, e, n, r) {
                i[t][e] || (i[t][e] = []), i[t][e].push({handler: n, root: r})
            }

            function r(t) {
                t = t || window.event;
                var n = e.locate(t), r = s(t, i.click), a = !0;
                if (null !== n ? a = !e.checkEvent("onTaskClick") || e.callEvent("onTaskClick", [n, t]) : e.callEvent("onEmptyClick", [t]), a) {
                    if (!o(r, t, n)) return;
                    n && e.getTask(n) && e.config.select_task && !e.config.multiselect && e.selectTask(n)
                }
            }

            function a(t) {
                var i = (t = t || window.event).target || t.srcElement, n = e.locate(i),
                    r = e.locate(i, e.config.link_attribute),
                    a = !e.checkEvent("onContextMenu") || e.callEvent("onContextMenu", [n, r, t]);
                return a || (t.preventDefault ? t.preventDefault() : t.returnValue = !1), a
            }

            function s(e, i) {
                for (var n = e.target || e.srcElement, r = []; n;) {
                    var a = t.getClassName(n);
                    if (a) {
                        a = a.split(" ");
                        for (var s = 0; s < a.length; s++) if (a[s] && i[a[s]]) for (var o = i[a[s]], l = 0; l < o.length; l++) o[l].root && !t.isChildOf(n, o[l].root) || r.push(o[l].handler)
                    }
                    n = n.parentNode
                }
                return r
            }

            function o(t, i, n) {
                for (var r = !0, a = 0; a < t.length; a++) {
                    var s = t[a].call(e, i, n, i.target || i.srcElement);
                    r = r && !(void 0 !== s && !0 !== s)
                }
                return r
            }

            function l(t) {
                t = t || window.event;
                var n = e.locate(t), r = s(t, i.doubleclick),
                    a = !e.checkEvent("onTaskDblClick") || e.callEvent("onTaskDblClick", [n, t]);
                if (a) {
                    if (!o(r, t, n)) return;
                   // null !== n && e.getTask(n) && a && e.config.details_on_dblclick && e.showLightbox(n)
                }
            }

            function c(t) {
                if (e.checkEvent("onMouseMove")) {
                    var i = e.locate(t);
                    e._last_move_event = t, e.callEvent("onMouseMove", [i, t])
                }
            }

            var d = e._createDomEventScope();

            function h(t) {
                d.detachAll(), t && (d.attach(t, "click", r), d.attach(t, "dblclick", l), d.attach(t, "mousemove", c), d.attach(t, "contextmenu", a))
            }

            return {
                reset: h, global: function (t, e, i) {
                    n(t, e, i, null)
                }, delegate: n, detach: function (t, e, n, r) {
                    if (i[t] && i[t][e]) {
                        for (var a = i[t], s = a[e], o = 0; o < s.length; o++) s[o].root == r && (s.splice(o, 1), o--);
                        s.length || delete a[e]
                    }
                }, callHandler: function (t, e, n, r) {
                    var a = i[t][e];
                    if (a) for (var s = 0; s < a.length; s++) (n || a[s].root) && a[s].root !== n || a[s].handler.apply(this, r)
                }, onDoubleClick: l, onMouseMove: c, onContextMenu: a, onClick: r, destructor: function () {
                    h(), i = null, d = null
                }
            }
        }
    }(i(1));
    t.exports = {init: n}
}, function (t, e, i) {
    var n = i(0);

    function r(t) {
        n.mixin(this, t, !0)
    }

    function a(t, e) {
        var i = this.$config[t];
        return i ? i instanceof r ? i : (r.prototype = e, this.$config[t] = new r(i), this.$config[t]) : e
    }

    t.exports = function (t, e) {
        n.mixin(t, function (t) {
            var e, i;
            return {
                $getConfig: function () {
                    return e || (e = t ? t.$getConfig() : this.$gantt.config), a.call(this, "config", e)
                }, $getTemplates: function () {
                    return i || (i = t ? t.$getTemplates() : this.$gantt.templates), a.call(this, "templates", i)
                }
            }
        }(e))
    }
}, function (t, e, i) {
    var n = i(0), r = i(118);
    t.exports = {
        createFactory: function (t) {
            var e = {};
            var i = {};

            function a(a, s, o, l) {
                var c = e[a];
                if (!c || !c.create) return !1;
                "resizer" != a || o.mode || (l.$config.cols ? o.mode = "x" : o.mode = "y"), "viewcell" != a || "scrollbar" != o.view || o.scroll || (l.$config.cols ? o.scroll = "y" : o.scroll = "x"), (o = n.copy(o)).id || i[o.view] || (o.id = o.view), o.id && !o.css && (o.css = o.id + "_cell");
                var d = new c.create(s, o, this, t);
                return c.configure && c.configure(d), r(d, l), d.$id || (d.$id = o.id || t.uid()), d.$parent || "object" != typeof s || (d.$parent = s), d.$config || (d.$config = o), i[d.$id] && (d.$id = t.uid()), i[d.$id] = d, d
            }

            return {
                initUI: function (t, e) {
                    var i = "cell";
                    return t.view ? i = "viewcell" : t.resizer ? i = "resizer" : t.rows || t.cols ? i = "layout" : t.views && (i = "multiview"), a.call(this, i, null, t, e)
                }, reset: function () {
                    i = {}
                }, registerView: function (t, i, n) {
                    e[t] = {create: i, configure: n}
                }, createView: a, getView: function (t) {
                    return i[t]
                }
            }
        }
    }
}, function (t, e, i) {
    var n = i(119), r = i(117), a = i(116), s = i(7), o = i(25), l = i(113), c = i(112), d = i(111), h = i(110),
        u = i(11), g = i(22), f = i(22), _ = i(11), p = i(11), v = i(107), m = i(21), y = i(98), k = i(97), b = i(96),
        $ = i(95), x = i(94), w = i(88), S = i(85);
    t.exports = {
        init: function (t) {
            function e(e, i) {
                var n = i(t);
                n.onCreated && n.onCreated(e), e.attachEvent("onReady", function () {
                    n.onInitialized && n.onInitialized(e)
                }), e.attachEvent("onDestroy", function () {
                    n.onDestroyed && n.onDestroyed(e)
                })
            }

            var i = n.createFactory(t);
            i.registerView("cell", s), i.registerView("resizer", d), i.registerView("scrollbar", h), i.registerView("layout", o, function (t) {
                "main" === (t.$config ? t.$config.id : null) && e(t, S)
            }), i.registerView("viewcell", c), i.registerView("multiview", l), i.registerView("timeline", u, function (t) {
                "timeline" !== (t.$config ? t.$config.id : null) && "task" != t.$config.bind || e(t, w)
            }), i.registerView("grid", g, function (t) {
                "grid" !== (t.$config ? t.$config.id : null) && "task" != t.$config.bind || e(t, x)
            }), i.registerView("resourceGrid", f), i.registerView("resourceTimeline", _), i.registerView("resourceHistogram", p);
            var T = a(t), E = v(t);
            return t.ext.inlineEditors = E, t.ext._inlineEditors = E, E.init(t), {
                factory: i,
                mouseEvents: r.init(t),
                layersApi: T.init(),
                render: {gridLine: $(t), taskBg: k(t), taskBar: m(t), taskSplitBar: y(t), link: b(t)},
                layersService: {
                    getDataRender: function (e) {
                        return T.getDataRender(e, t)
                    }, createDataRender: function (e) {
                        return T.createDataRender(e, t)
                    }
                }
            }
        }
    }
}, function (t, e, i) {
    var n = i(0), r = i(1);
    t.exports = function (t) {
        var e = "data-dhxbox", i = null;

        function a(t, e) {
            var n = t.callback;
            _.hide(t.box), i = t.box = null, n && n(e)
        }

        function s(t) {
            if (i) {
                var e = (t = t || event).which || event.keyCode, n = !1;
                if (p.keyboard) {
                    if (13 == e || 32 == e) {
                        var s = t.target || t.srcElement;
                        r.getClassName(s).indexOf("gantt_popup_button") > -1 && s.click ? s.click() : (a(i, !0), n = !0)
                    }
                    27 == e && (a(i, !1), n = !0)
                }
                return n ? (t.preventDefault && t.preventDefault(), !(t.cancelBubble = !0)) : void 0
            }
        }

        function o(t) {
            o.cover || (o.cover = document.createElement("div"), o.cover.onkeydown = s, o.cover.className = "dhx_modal_cover", document.body.appendChild(o.cover)), o.cover.style.display = t ? "inline-block" : "none"
        }

        function l(e, i, n) {
            var r = t._waiAria.messageButtonAttrString(e), a = i.toLowerCase().replace(/ /g, "_");
            return "<div " + r + " class='gantt_popup_button dhtmlx_popup_button " + ("gantt_" + a + "_button dhtmlx_" + a + "_button") + "' data-result='" + n + "' result='" + n + "' ><div>" + e + "</div></div>"
        }

        function c() {
            for (var t = [].slice.apply(arguments, [0]), e = 0; e < t.length; e++) if (t[e]) return t[e]
        }

        function d(r, d, h) {
            var u = r.tagName ? r : function (r, s, o) {
                var d = document.createElement("div"), h = n.uid();
                t._waiAria.messageModalAttr(d, h), d.className = " gantt_modal_box dhtmlx_modal_box gantt-" + r.type + " dhtmlx-" + r.type, d.setAttribute(e, 1);
                var u = "";
                if (r.width && (d.style.width = r.width), r.height && (d.style.height = r.height), r.title && (u += '<div class="gantt_popup_title dhtmlx_popup_title">' + r.title + "</div>"), u += '<div class="gantt_popup_text dhtmlx_popup_text" id="' + h + '"><span>' + (r.content ? "" : r.text) + '</span></div><div  class="gantt_popup_controls dhtmlx_popup_controls">', s && (u += l(c(r.ok, t.locale.labels.message_ok, "OK"), "ok", !0)), o && (u += l(c(r.cancel, t.locale.labels.message_cancel, "Cancel"), "cancel", !1)), r.buttons) for (var g = 0; g < r.buttons.length; g++) {
                    var f = r.buttons[g];
                    u += "object" == typeof f ? l(f.label, f.css || "gantt_" + f.label.toLowerCase() + "_button dhtmlx_" + f.label.toLowerCase() + "_button", f.value || g) : l(f, f, g)
                }
                if (u += "</div>", d.innerHTML = u, r.content) {
                    var _ = r.content;
                    "string" == typeof _ && (_ = document.getElementById(_)), "none" == _.style.display && (_.style.display = ""), d.childNodes[r.title ? 1 : 0].appendChild(_)
                }
                return d.onclick = function (t) {
                    var e = (t = t || event).target || t.srcElement;
                    if (e.className || (e = e.parentNode), "gantt_popup_button" == e.className.split(" ")[0]) {
                        var i = e.getAttribute("data-result");
                        a(r, i = "true" == i || "false" != i && i)
                    }
                }, r.box = d, (s || o) && (i = r), d
            }(r, d, h);
            r.hidden || o(!0), document.body.appendChild(u);
            var g = Math.abs(Math.floor(((window.innerWidth || document.documentElement.offsetWidth) - u.offsetWidth) / 2)),
                f = Math.abs(Math.floor(((window.innerHeight || document.documentElement.offsetHeight) - u.offsetHeight) / 2));
            return "top" == r.position ? u.style.top = "-3px" : u.style.top = f + "px", u.style.left = g + "px", u.onkeydown = s, _.focus(u), r.hidden && _.hide(u), t.callEvent("onMessagePopup", [u]), u
        }

        function h(t) {
            return d(t, !0, !1)
        }

        function u(t) {
            return d(t, !0, !0)
        }

        function g(t) {
            return d(t)
        }

        function f(t, e, i) {
            return "object" != typeof t && ("function" == typeof e && (i = e, e = ""), t = {
                text: t,
                type: e,
                callback: i
            }), t
        }

        t.event(document, "keydown", s, !0);
        var _ = function () {
            var t = f.apply(this, arguments);
            return t.type = t.type || "alert", g(t)
        };
        _.hide = function (i) {
            for (; i && i.getAttribute && !i.getAttribute(e);) i = i.parentNode;
            i && (i.parentNode.removeChild(i), o(!1), t.callEvent("onAfterMessagePopup", [i]))
        }, _.focus = function (t) {
            setTimeout(function () {
                var e = r.getFocusableNodes(t);
                e.length && e[0].focus && e[0].focus()
            }, 1)
        };
        var p = function (e, i, r, a) {
            switch ((e = function (t, e, i, r) {
                return "object" != typeof t && (t = {
                    text: t,
                    type: e,
                    expire: i,
                    id: r
                }), t.id = t.id || n.uid(), t.expire = t.expire || p.expire, t
            }.apply(this, arguments)).type = e.type || "info", e.type.split("-")[0]) {
                case"alert":
                    return h(e);
                case"confirm":
                    return u(e);
                case"modalbox":
                    return g(e);
                default:
                    return function (e) {
                        p.area || (p.area = document.createElement("div"), p.area.className = "gantt_message_area dhtmlx_message_area", p.area.style[p.position] = "5px", document.body.appendChild(p.area)), p.hide(e.id);
                        var i = document.createElement("div");
                        return i.innerHTML = "<div>" + e.text + "</div>", i.className = "gantt-info dhtmlx-info gantt-" + e.type + " dhtmlx-" + e.type, i.onclick = function () {
                            p.hide(e.id), e = null
                        }, t._waiAria.messageInfoAttr(i), "bottom" == p.position && p.area.firstChild ? p.area.insertBefore(i, p.area.firstChild) : p.area.appendChild(i), e.expire > 0 && (p.timers[e.id] = window.setTimeout(function () {
                            p.hide(e.id)
                        }, e.expire)), p.pull[e.id] = i, i = null, e.id
                    }(e)
            }
        };
        p.seed = (new Date).valueOf(), p.uid = n.uid, p.expire = 4e3, p.keyboard = !0, p.position = "top", p.pull = {}, p.timers = {}, p.hideAll = function () {
            for (var t in p.pull) p.hide(t)
        }, p.hide = function (t) {
            var e = p.pull[t];
            e && e.parentNode && (window.setTimeout(function () {
                e.parentNode.removeChild(e), e = null
            }, 2e3), e.className += " hidden", p.timers[t] && window.clearTimeout(p.timers[t]), delete p.pull[t])
        };
        var v = [];
        return t.attachEvent("onMessagePopup", function (t) {
            v.push(t)
        }), t.attachEvent("onAfterMessagePopup", function (t) {
            for (var e = 0; e < v.length; e++) v[e] === t && (v.splice(e, 1), e--)
        }), t.attachEvent("onDestroy", function () {
            o.cover && o.cover.parentNode && o.cover.parentNode.removeChild(o.cover);
            for (var t = 0; t < v.length; t++) v[t].parentNode && v[t].parentNode.removeChild(v[t]);
            v = null, p.area && p.area.parentNode && p.area.parentNode.removeChild(p.area), p = null
        }), {
            alert: function () {
                var t = f.apply(this, arguments);
                return t.type = t.type || "confirm", h(t)
            }, confirm: function () {
                var t = f.apply(this, arguments);
                return t.type = t.type || "alert", u(t)
            }, message: p, modalbox: _
        }
    }
}, function (t, e, i) {
    var n = i(0);
    t.exports = function t(e, i) {
        e = e || n.event, i = i || n.eventRemove;
        var r = [];
        return {
            attach: function (t, i, n, a) {
                r.push({element: t, event: i, callback: n, capture: a}), e(t, i, n, a)
            }, detach: function (t, e, n, a) {
                i(t, e, n, a);
                for (var s = 0; s < r.length; s++) (n = r[s]).element === t && n.event === e && n.callback === n && n.capture === a && (r.splice(s, 1), s--)
            }, detachAll: function () {
                for (var t = 0; t < r.length; t++) i(r[t].element, r[t].event, r[t].callback, r[t].capture), i(r[t].element, r[t].event, r[t].callback, void 0), i(r[t].element, r[t].event, r[t].callback, !1), i(r[t].element, r[t].event, r[t].callback, !0);
                r = []
            }, extend: function () {
                return t(this.event, this.eventRemove)
            }
        }
    }
}, function (t, e, i) {
    var n = i(0);
    t.exports = function () {
        var t = {};
        return {
            getState: function (e) {
                if (e) return t[e].method();
                var i = {};
                for (var r in t) t[r].internal || n.mixin(i, t[r].method(), !0);
                return i
            }, registerProvider: function (e, i, n) {
                t[e] = {method: i, internal: n}
            }, unregisterProvider: function (e) {
                delete t[e]
            }
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = {};

        function i(i, n, r) {
            r = r || i;
            var a = t.config, s = t.templates;
            t.config[i] && e[r] != a[i] && (n && s[r] || (s[r] = t.date.date_to_str(a[i]), e[r] = a[i]))
        }

        return {
            initTemplates: function () {
                var e = t.locale.labels;
                e.gantt_save_btn = e.icon_save, e.gantt_cancel_btn = e.icon_cancel, e.gantt_delete_btn = e.icon_delete;
                var n = t.date, r = n.date_to_str, a = t.config;
                i("date_scale", !0, void 0, t.config, t.templates), i("date_grid", !0, "grid_date_format", t.config, t.templates), i("task_date", !0, void 0, t.config, t.templates), t.mixin(t.templates, {
                    xml_date: n.str_to_date(a.xml_date, a.server_utc),
                    xml_format: r(a.xml_date, a.server_utc),
                    api_date: n.str_to_date(a.api_date),
                    progress_text: function (t, e, i) {
                        return ""
                    },
                    grid_header_class: function (t, e) {
                        return ""
                    },
                    task_text: function (t, e, i) {
                        return i.text
                    },
                    task_class: function (t, e, i) {
                        return ""
                    },
                    grid_row_class: function (t, e, i) {
                        return ""
                    },
                    task_row_class: function (t, e, i) {
                        return ""
                    },
                    task_cell_class: function (t, e) {
                        return ""
                    },
                    scale_cell_class: function (t) {
                        return ""
                    },
                    scale_row_class: function (t) {
                        return ""
                    },
                    grid_indent: function (t) {
                        return "<div class='gantt_tree_indent'></div>"
                    },
                    grid_folder: function (t) {
                        return "<div class='gantt_tree_icon gantt_folder_" + (t.$open ? "open" : "closed") + "'></div>"
                    },
                    grid_file: function (t) {
                        return "<div class='gantt_tree_icon gantt_file'></div>"
                    },
                    grid_open: function (t) {
                        return "<div class='gantt_tree_icon gantt_" + (t.$open ? "close" : "open") + "'></div>"
                    },
                    grid_blank: function (t) {
                        return "<div class='gantt_tree_icon gantt_blank'></div>"
                    },
                    date_grid: function (e, i) {
                        return i && t.isUnscheduledTask(i) && t.config.show_unscheduled ? t.templates.task_unscheduled_time(i) : t.templates.grid_date_format(e)
                    },
                    task_time: function (e, i, n) {
                        return t.isUnscheduledTask(n) && t.config.show_unscheduled ? t.templates.task_unscheduled_time(n) : t.templates.task_date(e) + " - " + t.templates.task_date(i)
                    },
                    task_unscheduled_time: function (t) {
                        return ""
                    },
                    time_picker: r(a.time_picker),
                    link_class: function (t) {
                        return ""
                    },
                    link_description: function (e) {
                        var i = t.getTask(e.source), n = t.getTask(e.target);
                        return "<b>" + i.text + "</b> &ndash;  <b>" + n.text + "</b>"
                    },
                    drag_link: function (e, i, n, r) {
                        e = t.getTask(e);
                        var a = t.locale.labels,
                            s = "<b>" + e.text + "</b> " + (i ? a.link_start : a.link_end) + "<br/>";
                        return n && (s += "<b> " + (n = t.getTask(n)).text + "</b> " + (r ? a.link_start : a.link_end) + "<br/>"), s
                    },
                    drag_link_class: function (e, i, n, r) {
                        var a = "";
                        return e && n && (a = " " + (t.isLinkAllowed(e, n, i, r) ? "gantt_link_allow" : "gantt_link_deny")), "gantt_link_tooltip" + a
                    },
                    tooltip_date_format: n.date_to_str("%Y-%m-%d"),
                    tooltip_text: function (e, i, n) {
                        return "<b>Task:</b> " + n.text + "<br/><b>Start date:</b> " + t.templates.tooltip_date_format(e) + "<br/><b>End date:</b> " + t.templates.tooltip_date_format(i)
                    }
                })
            }, initTemplate: i
        }
    }
}, function (t, e, i) {
    var n = i(4), r = i(0), a = i(26);
    t.exports = function (t) {
        function e(t) {
            return {
                target: t.target || t.srcElement,
                pageX: t.pageX,
                pageY: t.pageY,
                clientX: t.clientX,
                clientY: t.clientY,
                metaKey: t.metaKey,
                shiftKey: t.shiftKey,
                ctrlKey: t.ctrlKey,
                altKey: t.altKey
            }
        }

        function i(i, a) {
            this._obj = i, this._settings = a || {}, n(this);
            var s = this.getInputMethods();
            this._drag_start_timer = null, t.attachEvent("onGanttScroll", r.bind(function (t, e) {
                this.clearDragTimer()
            }, this));
            for (var o = 0; o < s.length; o++) r.bind(function (n) {
                t.event(i, n.down, r.bind(function (a) {
                    n.accessor(a) && (this._settings.original_target = e(a), t.config.touch ? (this.clearDragTimer(), this._drag_start_timer = setTimeout(r.bind(function () {
                        this.dragStart(i, a, n)
                    }, this), t.config.touch_drag)) : this.dragStart(i, a, n))
                }, this)), t.event(document.body, n.up, r.bind(function (t) {
                    n.accessor(t) && this.clearDragTimer()
                }, this))
            }, this)(s[o])
        }

        return i.prototype = {
            traceDragEvents: function (e, i) {
                var n = r.bind(function (t) {
                    return this.dragMove(e, t, i.accessor)
                }, this);
                r.bind(function (t) {
                    return this.dragScroll(e, t)
                }, this);
                var s = r.bind(function (t) {
                    return t && t.preventDefault && t.preventDefault(), (t || event).cancelBubble = !0, !(!r.defined(this.config.updates_per_second) || a(this, this.config.updates_per_second)) || n(t)
                }, this), o = r.bind(function (n) {
                    return t.eventRemove(document.body, i.move, s), t.eventRemove(document.body, i.up, o), this.dragEnd(e)
                }, this);
                t.event(document.body, i.move, s), t.event(document.body, i.up, o)
            }, checkPositionChange: function (t) {
                var e = t.x - this.config.pos.x, i = t.y - this.config.pos.y;
                return Math.sqrt(Math.pow(Math.abs(e), 2) + Math.pow(Math.abs(i), 2)) > this.config.sensitivity
            }, initDnDMarker: function () {
                var t = this.config.marker = document.createElement("div");
                t.className = "gantt_drag_marker", t.innerHTML = "Dragging object", document.body.appendChild(t)
            }, backupEventTarget: function (i, n) {
                if (t.config.touch) {
                    var r = n(i), a = r.target || r.srcElement, s = a.cloneNode(!0);
                    this.config.original_target = e(r), this.config.original_target.target = s, this.config.backup_element = a, a.parentNode.appendChild(s), a.style.display = "none", document.body.appendChild(a)
                }
            }, getInputMethods: function () {
                var e = [];
                if (e.push({
                    move: "mousemove", down: "mousedown", up: "mouseup", accessor: function (t) {
                        return t
                    }
                }), t.config.touch) {
                    var i = !0;
                    try {
                        document.createEvent("TouchEvent")
                    } catch (t) {
                        i = !1
                    }
                    i ? e.push({
                        move: "touchmove", down: "touchstart", up: "touchend", accessor: function (t) {
                            return t.touches && t.touches.length > 1 ? null : t.touches[0] ? {
                                target: document.elementFromPoint(t.touches[0].clientX, t.touches[0].clientY),
                                pageX: t.touches[0].pageX,
                                pageY: t.touches[0].pageY,
                                clientX: t.touches[0].clientX,
                                clientY: t.touches[0].clientY
                            } : t
                        }
                    }) : window.navigator.pointerEnabled ? e.push({
                        move: "pointermove",
                        down: "pointerdown",
                        up: "pointerup",
                        accessor: function (t) {
                            return "mouse" == t.pointerType ? null : t
                        }
                    }) : window.navigator.msPointerEnabled && e.push({
                        move: "MSPointerMove",
                        down: "MSPointerDown",
                        up: "MSPointerUp",
                        accessor: function (t) {
                            return t.pointerType == t.MSPOINTER_TYPE_MOUSE ? null : t
                        }
                    })
                }
                return e
            }, clearDragTimer: function () {
                this._drag_start_timer && (clearTimeout(this._drag_start_timer), this._drag_start_timer = null)
            }, dragStart: function (e, i, n) {
                this.config && this.config.started || (this.config = {
                    obj: e,
                    marker: null,
                    started: !1,
                    pos: this.getPosition(i),
                    sensitivity: 4
                }, this._settings && r.mixin(this.config, this._settings, !0), this.traceDragEvents(e, n), t._prevent_touch_scroll = !0, document.body.className += " gantt_noselect", t.config.touch && this.dragMove(e, i, n.accessor))
            }, dragMove: function (e, i, n) {
                var r = n(i);
                if (r) {
                    if (!this.config.marker && !this.config.started) {
                        var a = this.getPosition(r);
                        if (t.config.touch || this.checkPositionChange(a)) {
                            if (this.config.started = !0, this.config.ignore = !1, !1 === this.callEvent("onBeforeDragStart", [e, this.config.original_target])) return this.config.ignore = !0, !0;
                            this.backupEventTarget(i, n), this.initDnDMarker(), t._touch_feedback(), this.callEvent("onAfterDragStart", [e, this.config.original_target])
                        } else this.config.ignore = !0
                    }
                    return this.config.ignore ? void 0 : (r.pos = this.getPosition(r), this.config.marker.style.left = r.pos.x + "px", this.config.marker.style.top = r.pos.y + "px", this.callEvent("onDragMove", [e, r]), !1)
                }
            }, dragEnd: function (e) {
                var i = this.config.backup_element;
                i && i.parentNode && i.parentNode.removeChild(i), t._prevent_touch_scroll = !1, this.config.marker && (this.config.marker.parentNode.removeChild(this.config.marker), this.config.marker = null, this.callEvent("onDragEnd", [])), this.config.started = !1, document.body.className = document.body.className.replace(" gantt_noselect", "")
            }, getPosition: function (t) {
                var e = 0, i = 0;
                return (t = t || window.event).pageX || t.pageY ? (e = t.pageX, i = t.pageY) : (t.clientX || t.clientY) && (e = t.clientX + document.body.scrollLeft + document.documentElement.scrollLeft, i = t.clientY + document.body.scrollTop + document.documentElement.scrollTop), {
                    x: e,
                    y: i
                }
            }
        }, i
    }
}, function (t, e) {
    t.exports = function (t) {
        var e = {
            init: function () {
                for (var e = t.locale, i = e.date.month_short, n = e.date.month_short_hash = {}, r = 0; r < i.length; r++) n[i[r]] = r;
                for (i = e.date.month_full, n = e.date.month_full_hash = {}, r = 0; r < i.length; r++) n[i[r]] = r
            }, date_part: function (t) {
                var e = new Date(t);
                return t.setHours(0), this.hour_start(t), t.getHours() && (t.getDate() < e.getDate() || t.getMonth() < e.getMonth() || t.getFullYear() < e.getFullYear()) && t.setTime(t.getTime() + 36e5 * (24 - t.getHours())), t
            }, time_part: function (t) {
                return (t.valueOf() / 1e3 - 60 * t.getTimezoneOffset()) % 86400
            }, week_start: function (e) {
                var i = e.getDay();
                return t.config.start_on_monday && (0 === i ? i = 6 : i--), this.date_part(this.add(e, -1 * i, "day"))
            }, month_start: function (t) {
                return t.setDate(1), this.date_part(t)
            }, quarter_start: function (t) {
                this.month_start(t);
                var e, i = t.getMonth();
                return e = i >= 9 ? 9 : i >= 6 ? 6 : i >= 3 ? 3 : 0, t.setMonth(e), t
            }, year_start: function (t) {
                return t.setMonth(0), this.month_start(t)
            }, day_start: function (t) {
                return this.date_part(t)
            }, hour_start: function (t) {
                return t.getMinutes() && t.setMinutes(0), this.minute_start(t), t
            }, minute_start: function (t) {
                return t.getSeconds() && t.setSeconds(0), t.getMilliseconds() && t.setMilliseconds(0), t
            }, _add_days: function (t, e) {
                var i = new Date(t.valueOf());
                return i.setDate(i.getDate() + e), e >= 0 && !t.getHours() && i.getHours() && (i.getDate() <= t.getDate() || i.getMonth() < t.getMonth() || i.getFullYear() < t.getFullYear()) && i.setTime(i.getTime() + 36e5 * (24 - i.getHours())), i
            }, add: function (t, e, i) {
                var n = new Date(t.valueOf());
                switch (i) {
                    case"day":
                        n = this._add_days(n, e);
                        break;
                    case"week":
                        n = this._add_days(n, 7 * e);
                        break;
                    case"month":
                        n.setMonth(n.getMonth() + e);
                        break;
                    case"year":
                        n.setYear(n.getFullYear() + e);
                        break;
                    case"hour":
                        n.setTime(n.getTime() + 60 * e * 60 * 1e3);
                        break;
                    case"minute":
                        n.setTime(n.getTime() + 60 * e * 1e3);
                        break;
                    default:
                        return this["add_" + i](t, e, i)
                }
                return n
            }, add_quarter: function (t, e) {
                return this.add(t, 3 * e, "month")
            }, to_fixed: function (t) {
                return t < 10 ? "0" + t : t
            }, copy: function (t) {
                return new Date(t.valueOf())
            }, date_to_str: function (i, n) {
                i = i.replace(/%[a-zA-Z]/g, function (t) {
                    switch (t) {
                        case"%d":
                            return '"+to_fixed(date.getDate())+"';
                        case"%m":
                            return '"+to_fixed((date.getMonth()+1))+"';
                        case"%j":
                            return '"+date.getDate()+"';
                        case"%n":
                            return '"+(date.getMonth()+1)+"';
                        case"%y":
                            return '"+to_fixed(date.getFullYear()%100)+"';
                        case"%Y":
                            return '"+date.getFullYear()+"';
                        case"%D":
                            return '"+locale.date.day_short[date.getDay()]+"';
                        case"%l":
                            return '"+locale.date.day_full[date.getDay()]+"';
                        case"%M":
                            return '"+locale.date.month_short[date.getMonth()]+"';
                        case"%F":
                            return '"+locale.date.month_full[date.getMonth()]+"';
                        case"%h":
                            return '"+to_fixed((date.getHours()+11)%12+1)+"';
                        case"%g":
                            return '"+((date.getHours()+11)%12+1)+"';
                        case"%G":
                            return '"+date.getHours()+"';
                        case"%H":
                            return '"+to_fixed(date.getHours())+"';
                        case"%i":
                            return '"+to_fixed(date.getMinutes())+"';
                        case"%a":
                            return '"+(date.getHours()>11?"pm":"am")+"';
                        case"%A":
                            return '"+(date.getHours()>11?"PM":"AM")+"';
                        case"%s":
                            return '"+to_fixed(date.getSeconds())+"';
                        case"%W":
                            return '"+to_fixed(getISOWeek(date))+"';
                        case"%w":
                            return '"+to_fixed(getWeek(date))+"';
                        default:
                            return t
                    }
                }), n && (i = i.replace(/date\.get/g, "date.getUTC"));
                var r = new Function("date", "to_fixed", "locale", "getISOWeek", "getWeek", 'return "' + i + '";');
                return function (i) {
                    return r(i, e.to_fixed, t.locale, e.getISOWeek, e.getWeek)
                }
            }, str_to_date: function (e, i) {
                for (var n = "var temp=date.match(/[a-zA-Z]+|[0-9]+/g);", r = e.match(/%[a-zA-Z]/g), a = 0; a < r.length; a++) switch (r[a]) {
                    case"%j":
                    case"%d":
                        n += "set[2]=temp[" + a + "]||1;";
                        break;
                    case"%n":
                    case"%m":
                        n += "set[1]=(temp[" + a + "]||1)-1;";
                        break;
                    case"%y":
                        n += "set[0]=temp[" + a + "]*1+(temp[" + a + "]>50?1900:2000);";
                        break;
                    case"%g":
                    case"%G":
                    case"%h":
                    case"%H":
                        n += "set[3]=temp[" + a + "]||0;";
                        break;
                    case"%i":
                        n += "set[4]=temp[" + a + "]||0;";
                        break;
                    case"%Y":
                        n += "set[0]=temp[" + a + "]||0;";
                        break;
                    case"%a":
                    case"%A":
                        n += "set[3]=set[3]%12+((temp[" + a + "]||'').toLowerCase()=='am'?0:12);";
                        break;
                    case"%s":
                        n += "set[5]=temp[" + a + "]||0;";
                        break;
                    case"%M":
                        n += "set[1]=locale.date.month_short_hash[temp[" + a + "]]||0;";
                        break;
                    case"%F":
                        n += "set[1]=locale.date.month_full_hash[temp[" + a + "]]||0;"
                }
                var s = "set[0],set[1],set[2],set[3],set[4],set[5]";
                i && (s = " Date.UTC(" + s + ")");
                var o = new Function("date", "locale", "var set=[0,0,1,0,0,0]; " + n + " return new Date(" + s + ");");
                return function (e) {
                    return o(e, t.locale)
                }
            }, getISOWeek: function (e) {
                return t.date._getWeekNumber(e, !0)
            }, _getWeekNumber: function (t, e) {
                if (!t) return !1;
                var i = t.getDay();
                e && 0 === i && (i = 7);
                var n = new Date(t.valueOf());
                n.setDate(t.getDate() + (4 - i));
                var r = n.getFullYear(), a = Math.round((n.getTime() - new Date(r, 0, 1).getTime()) / 864e5);
                return 1 + Math.floor(a / 7)
            }, getWeek: function (e) {
                return t.date._getWeekNumber(e, t.config.start_on_monday)
            }, getUTCISOWeek: function (e) {
                return t.date.getISOWeek(e)
            }, convert_to_utc: function (t) {
                return new Date(t.getUTCFullYear(), t.getUTCMonth(), t.getUTCDate(), t.getUTCHours(), t.getUTCMinutes(), t.getUTCSeconds())
            }, parseDate: function (e, i) {
                return e && !e.getFullYear && (t.defined(i) && (i = "string" == typeof i ? t.defined(t.templates[i]) ? t.templates[i] : t.date.str_to_date(i) : t.templates.xml_date), e = e ? i(e) : null), e
            }
        };
        return e
    }
}, function (t, e, i) {
    var n = i(12);
    t.exports = function (t) {
        return {
            cache: !0, method: "get", parse: function (t) {
                return "string" != typeof t ? t : (t = t.replace(/^[\s]+/, ""), window.DOMParser && !n.isIE ? e = (new window.DOMParser).parseFromString(t, "text/xml") : window.ActiveXObject !== window.undefined && ((e = new window.ActiveXObject("Microsoft.XMLDOM")).async = "false", e.loadXML(t)), e);
                var e
            }, xmltop: function (e, i, n) {
                if (void 0 === i.status || i.status < 400) {
                    var r = i.responseXML ? i.responseXML || i : this.parse(i.responseText || i);
                    if (r && null !== r.documentElement && !r.getElementsByTagName("parsererror").length) return r.getElementsByTagName(e)[0]
                }
                return -1 !== n && t.callEvent("onLoadXMLError", ["Incorrect XML", arguments[1], n]), document.createElement("DIV")
            }, xpath: function (t, e) {
                if (e.nodeName || (e = e.responseXML || e), n.isIE) return e.selectNodes(t) || [];
                for (var i, r = [], a = (e.ownerDocument || e).evaluate(t, e, null, XPathResult.ANY_TYPE, null); i = a.iterateNext();) r.push(i);
                return r
            }, query: function (t) {
                this._call(t.method || "GET", t.url, t.data || "", t.async || !0, t.callback, null, t.headers)
            }, get: function (t, e) {
                this._call("GET", t, null, !0, e)
            }, getSync: function (t) {
                return this._call("GET", t, null, !1)
            }, put: function (t, e, i) {
                this._call("PUT", t, e, !0, i)
            }, del: function (t, e, i) {
                this._call("DELETE", t, e, !0, i)
            }, post: function (t, e, i) {
                1 == arguments.length ? e = "" : 2 != arguments.length || "function" != typeof e && "function" != typeof window[e] ? e = String(e) : (i = e, e = ""), this._call("POST", t, e, !0, i)
            }, postSync: function (t, e) {
                return e = null === e ? "" : String(e), this._call("POST", t, e, !1)
            }, getLong: function (t, e) {
                this._call("GET", t, null, !0, e, {url: t})
            }, postLong: function (t, e, i) {
                2 != arguments.length || "function" != typeof e && (window[e], 0) || (i = e, e = ""), this._call("POST", t, e, !0, i, {
                    url: t,
                    postData: e
                })
            }, _call: function (e, i, r, a, s, o, l) {
                var c = window.XMLHttpRequest && !n.isIE ? new XMLHttpRequest : new window.ActiveXObject("Microsoft.XMLHTTP"),
                    d = null !== navigator.userAgent.match(/AppleWebKit/) && null !== navigator.userAgent.match(/Qt/) && null !== navigator.userAgent.match(/Safari/);
                if (a && (c.onreadystatechange = function () {
                    if (4 == c.readyState || d && 3 == c.readyState) {
                        if ((200 != c.status || "" === c.responseText) && !t.callEvent("onAjaxError", [c])) return;
                        window.setTimeout(function () {
                            "function" == typeof s && s.apply(window, [{
                                xmlDoc: c,
                                filePath: i
                            }]), o && (void 0 !== o.postData ? this.postLong(o.url, o.postData, s) : this.getLong(o.url, s)), s = null, c = null
                        }, 1)
                    }
                }), "GET" != e || this.cache || (i += (i.indexOf("?") >= 0 ? "&" : "?") + "dhxr" + (new Date).getTime() + "=1"), c.open(e, i, a), l) for (var h in l) c.setRequestHeader(h, l[h]); else "POST" == e.toUpperCase() || "PUT" == e || "DELETE" == e ? c.setRequestHeader("Content-Type", "application/x-www-form-urlencoded") : "GET" == e && (r = null);
                if (c.setRequestHeader("X-Requested-With", "XMLHttpRequest"), c.send(r), !a) return {
                    xmlDoc: c,
                    filePath: i
                }
            }, urlSeparator: function (t) {
                return -1 != t.indexOf("?") ? "&" : "?"
            }
        }
    }
}, function (t, e) {
    t.exports = function () {
        return {
            layout: {
                css: "gantt_container",
                rows: [{
                    cols: [{view: "grid", scrollX: "scrollHor", scrollY: "scrollVer"}, {
                        resizer: !0,
                        width: 1
                    }, {view: "timeline", scrollX: "scrollHor", scrollY: "scrollVer"}, {
                        view: "scrollbar",
                        id: "scrollVer"
                    }]
                }, {view: "scrollbar", id: "scrollHor", height: 20}]
            },
            links: {finish_to_start: "0", start_to_start: "1", finish_to_finish: "2", start_to_finish: "3"},
            types: {task: "task", project: "project", milestone: "milestone"},
            auto_types: !1,
            duration_unit: "day",
            work_time: !1,
            correct_work_time: !1,
            skip_off_time: !1,
            cascade_delete: !0,
            autosize: !1,
            autosize_min_width: 0,
            autoscroll: !0,
            autoscroll_speed: 30,
            show_links: !0,
            show_task_cells: !0,
            static_background: !1,
            branch_loading: !1,
            branch_loading_property: "$has_child",
            show_loading: !1,
            show_chart: !0,
            show_grid: !0,
            min_duration: 36e5,
            xml_date: "%d-%m-%Y %H:%i",
            api_date: "%d-%m-%Y %H:%i",
            start_on_monday: !0,
            server_utc: !1,
            show_progress: !0,
            fit_tasks: !1,
            select_task: !0,
            scroll_on_click: !0,
            preserve_scroll: !0,
            readonly: !1,
            date_grid: "%Y-%m-%d",
            drag_links: !0,
            drag_progress: !0,
            drag_resize: !0,
            drag_project: !1,
            drag_move: !0,
            drag_mode: {resize: "resize", progress: "progress", move: "move", ignore: "ignore"},
            round_dnd_dates: !0,
            link_wrapper_width: 20,
            root_id: 0,
            autofit: !1,
            columns: [{
                name: "text",
                tree: !0,
                width: 150,
                resize: !0
            }, {
                name: "start_date",
                align: "center",
                resize: !0
            }, {
                name: "end_date",
                align: "center",
                resize: !0
            },{
                name: "duration",
                align: "center"
            },{
                name: "add",
                width: 44
            },{
                name: "update",
                width: 44
            },{
                name: "del",
                width: 44
            }],
            step: 1,
            scale_unit: "day",
            scale_offset_minimal: !0,
            subscales: [],
            inherit_scale_class: !1,
            time_step: 60,
            duration_step: 1,
            date_scale: "%d %M",
            task_date: "%d %F %Y",
            time_picker: "%H:%i",
            task_attribute: "task_id",
            link_attribute: "link_id",
            layer_attribute: "data-layer",
            buttons_left: ["gantt_save_btn", "gantt_cancel_btn"],
            _migrate_buttons: {
                dhx_save_btn: "gantt_save_btn",
                dhx_cancel_btn: "gantt_cancel_btn",
                dhx_delete_btn: "gantt_delete_btn"
            },
            buttons_right: ["gantt_delete_btn"],
            lightbox: {
                sections: [{
                    name: "description",
                    height: 70,
                    map_to: "text",
                    type: "textarea",
                    focus: !0
                }, {name: "time", type: "duration", map_to: "auto"}],
                project_sections: [{
                    name: "description",
                    height: 70,
                    map_to: "text",
                    type: "textarea",
                    focus: !0
                }, {name: "type", type: "typeselect", map_to: "type"}, {
                    name: "time",
                    type: "duration",
                    readonly: !0,
                    map_to: "auto"
                }],
                milestone_sections: [{
                    name: "description",
                    height: 70,
                    map_to: "text",
                    type: "textarea",
                    focus: !0
                }, {name: "type", type: "typeselect", map_to: "type"}, {
                    name: "time",
                    type: "duration",
                    single_date: !0,
                    map_to: "auto"
                }]
            },
            drag_lightbox: !0,
            sort: !1,
            details_on_create: !0,
            details_on_dblclick: !0,
            initial_scroll: !0,
            task_scroll_offset: 100,
            order_branch: !1,
            order_branch_free: !1,
            task_height: "full",
            min_column_width: 70,
            min_grid_column_width: 70,
            grid_resizer_column_attribute: "column_index",
            grid_resizer_attribute: "grid_resizer",
            keep_grid_width: !1,
            grid_resize: !1,
            show_unscheduled: !0,
            readonly_property: "readonly",
            editable_property: "editable",
            calendar_property: "calendar_id",
            resource_calendars: {},
            type_renderers: {},
            open_tree_initially: !1,
            optimize_render: !0,
            prevent_default_scroll: !1,
            show_errors: !0,
            wai_aria_attributes: !0,
            smart_scales: !0,
            rtl: !1,
            placeholder_task: !1
        }
    }
}, function (t, e) {
    t.exports = function () {
        var t = {};
        return {
            services: {config: "config", templates: "templates", locale: "locale"}, setService: function (e, i) {
                t[e] = i
            }, getService: function (e) {
                return t[e] ? t[e]() : null
            }, config: function () {
                return this.getService("config")
            }, templates: function () {
                return this.getService("templates")
            }, locale: function () {
                return this.getService("locale")
            }, destructor: function () {
                for (var e in t) if (t[e]) {
                    var i = t[e];
                    i && i.destructor && i.destructor()
                }
                t = null
            }
        }
    }
}, function (t, e) {
    t.exports = function (t) {
        t.$inject = function (t) {
            return t(this.$services)
        }
    }
}, function (t, e) {
    t.exports = {KEY_CODES: {UP: 38, DOWN: 40, LEFT: 37, RIGHT: 39, SPACE: 32, ENTER: 13, DELETE: 46, ESC: 27, TAB: 9}}
}, function (t, e, i) {
    i(14), t.exports = function () {
        var t = new function () {
            this.constants = i(131), this.version = "6.0.7", this.templates = {}, this.ext = {}, this.keys = {
                edit_save: this.constants.KEY_CODES.ENTER,
                edit_cancel: this.constants.KEY_CODES.ESC
            }
        };
        i(130)(t), t.$services = t.$inject(i(129)), t.config = t.$inject(i(128)), t.ajax = i(127)(t), t.date = i(126)(t);
        var e = i(125)(t);
        t.$services.setService("dnd", function () {
            return e
        }), t.$services.setService("config", function () {
            return t.config
        }), t.$services.setService("date", function () {
            return t.date
        }), t.$services.setService("locale", function () {
            return t.locale
        }), t.$services.setService("templates", function () {
            return t.templates
        });
        var n = i(124)(t);
        t.$services.setService("templateLoader", function () {
            return n
        }), i(4)(t);
        var r = new (i(123));
        r.registerProvider("global", function () {
            return {min_date: t._min_date, max_date: t._max_date, selected_task: t.$data.tasksStore.getSelectedId()}
        }), t.getState = r.getState, t.$services.setService("state", function () {
            return r
        });
        var a = i(0);
        a.mixin(t, a), t.env = i(12);
        var s = i(122)();
        t.event = s.attach, t.eventRemove = s.detach, t._eventRemoveAll = s.detachAll, t._createDomEventScope = s.extend, a.mixin(t, i(121)(t));
        var o = i(120).init(t);
        t.$ui = o.factory, t.$ui.layers = o.render, t.$mouseEvents = o.mouseEvents, t.$services.setService("mouseEvents", function () {
            return t.$mouseEvents
        }), t.mixin(t, o.layersApi), i(84)(t), t.$services.setService("layers", function () {
            return o.layersService
        });
        var l = i(83);
        return t.mixin(t, l()), i(82)(t), i(75)(t), i(72)(t), i(63)(t), i(62)(t), i(61)(t), i(60)(t), i(59)(t), i(58)(t), i(51)(t), i(50)(t), i(41)(t), i(40)(t), i(39)(t), i(38)(t), i(37)(t), i(36)(t), i(35)(t), i(34)(t), i(33)(t), i(32)(t), i(31)(t), i(30)(t), i(15)(t), i(29)(t), i(27)(t), t
    }
}, function (t, e) {
    t.exports = function (t) {
    }
}, function (t, e, i) {
    "use strict";
    Object.defineProperty(e, "__esModule", {value: !0});
    var n = i(133), r = i(132);
    n(window.gantt = r())
}]);
//# sourceMappingURL=dhtmlxgantt.js.map