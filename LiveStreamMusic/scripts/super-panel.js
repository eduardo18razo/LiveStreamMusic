
var panelOptions =
{
    panelId: "panel1",
    slideInFrom: "left",
    speed: 200,
    showMode: "default",
    transparentLayer: true,
    resizeCallback: null
};

var panel1 = new McSuperPanel(panelOptions);

/* Super Panel v2016.4.18. Copyright www.menucool.com */
function McSuperPanel(e) {
    "use strict";
    var z = "replace", l = "display", h = "className", d = "length", r = "addEventListener", b = "style";
    if (typeof String.prototype.trim !== "function")
        String.prototype.trim = function() {
            return this[z](/^\s+|\s+$/g, "")
        };
    var m = document, w = "getElementById", i = setTimeout, q = function(a, b) { return m[a](b) },
        L = function(a, d) {
            if (typeof getComputedStyle != "undefined") var c = getComputedStyle(a, null);
            else if (a.currentStyle) c = a.currentStyle;
            else c = a[b];
            return c[d]
        },
        v = 0,
        p,
        a,
        f = [],
        c = 0,
        j = 0,
        g = 0,
        D = function(a) {
            if (g) {
                g[b].opacity = 0;
                if (a) {
                    g[b][l] = "block";
                    i(function() { g[b].opacity = 1 }, 0)
                } else i(function() { g[b][l] = "none" }, 350)
            }
        },
        n = function(a, c, b) {
            if (a[r]) a[r](c, b, false);
            else a.attachEvent && a.attachEvent("on" + c, b)
        },
        K = function(a, c) {
            var b = a[d];
            while (b--) if (a[b] === c) return true;
            return false
        },
        k = function(b, a) { return K(b[h].split(" "), a) },
        y = function(a, b, c) {
            if (!k(a, b))
                if (!a[h]) a[h] = b;
                else if (c) a[h] = b + " " + a[h];
                else a[h] += " " + b
        },
        E = function(c, f) {
            if (c[h]) {
                for (var e = "", b = c[h].split(" "), a = 0, g = b[d]; a < g; a++) if (b[a] !== f) e += b[a] + " ";
                c[h] = e.trim()
            }
        },
        G = function(a) {
            if (a && a.stopPropagation) a.stopPropagation();
            else if (window.event) window.event.cancelBubble = true
        },
        J = function(b) {
            var a = b || window.event;
            if (a.preventDefault) a.preventDefault();
            else if (a) a.returnValue = false
        },
        t = function(h) {
            h && o(0);
            clearTimeout(c.g);
            for (var g = 0; g < f[d]; g++) E(f[g], "active");
            if (!v)
                if (c.a == "default") a[b].opacity = 0;
                else {
                    a[b].opacity = 0;
                    var e = "0,0";
                    switch (c.a) {
                    case "top":
                        e = "0,-" + c.b;
                        break;
                    case "bottom":
                        e = "0," + c.b;
                        break;
                    case "right":
                        e = c.b + ",0";
                        break;
                    case "left":
                        e = "-" + c.b + ",0"
                    }
                    a[b].transform = a[b].WebkitTransform = "translate(" + e + ") translateZ(0)"
                }
            c.g = i(function() {
                E(a, "active");
                a[b][l] = "none";
                o(p)
            }, p + 20);
            D(0)
        },
        x = function(g) {
            g && o(0);
            clearTimeout(c.g);
            for (var e = 0; e < f[d]; e++) y(f[e], "active");
            y(a, "active");
            a[b][l] = "";
            if (!v)
                if (c.a === 0) i(function() { a[b].opacity = 1 }, 0);
                else
                    i(function() {
                        a[b].opacity = 1;
                        a[b].transform = a[b].WebkitTransform = "translate(0,0) translateZ(0)";
                        o(p)
                    }, 0);
            D(1)
        },
        H = function(e) {
            var a = e.target || e.srcElement;
            if (!k(a, "panel-button")) {
                a = a.parentNode;
                if (a && !k(a, "panel-button")) a = a.parentNode;
                if (a && !k(a, "panel-button")) return
            }
            if (k(f[0], "active")) t();
            else {
                var b = a.getAttribute("data-click");
                if (b) {
                    b = s(b);
                    window[b[0]] && window[b[0]].apply(b.splice(0, 1), b)
                }
                var c = a.getAttribute("data-ajax");
                if (c) {
                    c = s(c);
                    debugger;
                    var d = new XMLHttpRequest;
                    d.onreadystatechange = function() {
                        if (d.readyState == 4 && d.status == 200) {
                            var a = d.responseText, b = /^[\s\S]*<body[^>]*>([\s\S]+)<\/body>[\s\S]*$/i;
                            if (b.test(a)) a = a[z](b, "$1");
                            a = a.trim();
                            //window[c[1]].apply(c.splice(0, 2, a), c)
                        }
                    };
                    d.open("GET", c[0], true);
                    d.send()
                }
                (b || c) && a.getAttribute("href") == "#" && J(e);
                x()
            }
            G(e)
        },
        B = function() { return window.innerWidth || m.documentElement.clientWidth || m.body.clientWidth },
        C = function() { return window.screen.width },
        A = function() {
            var a = c.e[0], e = c.e[1], h = c.f === 1;
            if (c.c) {
                var i = c.d ? C() : B();
                if (i > c.c) {
                    a = c.e[2];
                    e = c.e[3]
                }
                if (c.f[0] != a || c.f[1] != e) {
                    h = 1;
                    c.f = [a, e]
                }
            }
            if (h) {
                for (var g = 0; g < f[d]; g++) f[g][b][l] = a ? "" : "none";
                if (e) x(1);
                else t(1)
            }
        },
        o = function(c) { a[b].transition = a[b].WebkitTransition = a[b].msTransition = "all " + c + "ms ease-in" },
        M = function() {
            var g, i, c;
            if (m.getElementsByClassName) g = q("getElementsByClassName", "panel-button");
            else {
                g = [];
                var l = m.getElementsByTagName("*");
                c = l[d];
                while (c--) k(l[c], "panel-button") && g.push(l[c])
            }
            var n = 0, h = a.getElementsByTagName("*");
            c = h[d];
            while (c--)
                if (k(h[c], "panel-button")) {
                    n = 1;
                    h[c][b].zIndex = j + 1;
                    h[c][b].cssFloat = "none"
                }
            c = g[d];
            while (c--) {
                i = g[c].getAttribute("data-panel");
                if (!i || i == e.panelId) {
                    if (!n) g[c][b].zIndex = j + 1;
                    f.push(g[c])
                }
            }
        },
        u = function(c, b) {
            var a = false;
            if (c[d] > b) {
                a = c.charAt(b).toLowerCase() == "y";
                if (!b && !f) a = false
            }
            return a
        },
        s = function(a) { return a[z](/\s/g, "").split(",") },
        F = function() {
            a = q(w, e.panelId);
            if (a) {
                v = typeof a[b].transition == "undefined" && typeof a[b].WebkitTransition == "undefined";
                p = v ? 0 : e.speed;
                j = L(a, "zIndex");
                if (/^[\d\-]+$/.test(j)) j = parseInt(j);
                else j = 2;
                M();
                var r, x, k, l, h = [];
                r = k = f ? 1 : 0;
                x = l = 0;
                if (e.showMode && e.showMode != "default") {
                    h = s(e.showMode);
                    if (h[d] > 0) {
                        r = k = u(h[0], 0);
                        x = l = u(h[0], 1);
                        if (h[d] > 2) {
                            k = u(h[2], 0);
                            l = u(h[2], 1)
                        }
                    }
                }
                var m = s(e.slideInFrom), B = m[d] > 1 ? m[1] : "120%";
                c = { a: m[0], b: B, c: h[d] > 1 ? parseInt(h[1]) : 0, d: h[d] > 1 ? h[1].indexOf("d") != "-1" : 0, e: [r, x, k, l], f: 1, g: 1 };
                if (e.transparentLayer) {
                    g = q("createElement", "div");
                    y(g, "transparent-layer");
                    a.parentNode.insertBefore(g, a);
                    g[b].zIndex = j - 1;
                    n(a, "click", G);
                    e.transparentLayer === true && n(g, "click", function() { t(0) })
                }
                for (var z = 0; z < f[d]; z++) n(f[z], "click", H);
                o(p);
                A();
                c.c && n(window, "resize", A);
                a[b].visibility = "visible"
            }
            typeof e.initCallback == "function" && i(e.initCallback, 0);
            if (e.resizeCallback) {
                n(window, "resize", e.resizeCallback);
                i(e.resizeCallback, 0)
            }
        },
        I = function(c) {
            var a = 0;

            function b() {
                if (a) return;
                a = 1;
                i(c, 14)
            }

            if (m[r]) m[r]("DOMContentLoaded", b, false);
            else n(window, "load", b)
        };
    I(F);
    return {
        init: function() { !a && F() },
        getDeviceWidth: C,
        getBrowserWidth: B,
        show: function(d) {
            var c = q(w, d);
            if (c)
                if (c == a) x(1);
                else c[b][l] = ""
        },
        hide: function(d) {
            var c = q(w, d);
            if (c)
                if (c == a) t(1);
                else c[b][l] = "none"

        }

    }
}