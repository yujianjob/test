window.hui = {util: {}};

hui.util.insertCssRule = function (className, cssText) {
    var list = document.getElementsByTagName('style'),
        style = list && list.length ? list[list.length - 1] : hui.util.importCssString(''),
        sheet = style.sheet ? style.sheet : style.styleSheet,
        rules = sheet.cssRules || sheet.rules,
        index = rules.length,
        pre = className.indexOf('{'),
        nxt;
    if (pre !== -1) {
        nxt = className.indexOf('}', pre + 1);
        cssText = className.substring(pre + 1, nxt === -1 ? className.length : nxt);
        className = className.substring(0, pre);
    }
    cssText = String(cssText).replace(/(^\s+|\s+$)/g, '');
    if (cssText.indexOf('{') === 0) {
        cssText = cssText.substring(1, cssText.length);
    }
    if (cssText.indexOf('}') === cssText.length - 1) {
        cssText = cssText.substring(0, cssText.length - 2);
    }

    // all browsers, except IE before version 9
    if (sheet.insertRule) {
        sheet.insertRule(className + '{' + cssText + '}', index);
    }
    else {
        // Internet Explorer before version 9
        if (sheet.addRule) {
            sheet.addRule(className, cssText, index);
        }
    }
};
hui.util.addCssRule = hui.util.insertCssRule;

hui.util.preload = function (opt, callback) {
    var me = hui.util.preload,
        arr =  Object.prototype.toString.call(opt) === '[object Array]' ? opt : 
        (opt && Object.prototype.toString.call(opt.list) === '[object Array]' ? opt.list : []) ;
    if (!me.loaded && !me.left) {me.init();}
    
    var str,
        list = [];
    for (var i=0,len=arr.length; i<len; i++) {
        str = arr[i] ? String(arr[i]).replace(/^\s+|\s+$/g, '') : '';
        if (str) {
            list.push(str);
        }
    }
    
    if (!list.length) {
        return callback();
    }
    
    me.onloadAllCallback = callback;
    me.finished = false;
    me.left = list;
    me.loading = [].concat(list);
    me.maxCount = opt.maxCount;
    
    me.loadLeft();
};

hui.util.preload.init = function () {
    hui.util.preload.left = [];
    hui.util.preload.list = [];
    hui.util.preload.loaded = [];
    hui.util.preload.finished = false;
    hui.util.preload.onloadAllCallback = new Function();
    
    var wrap = document.getElementById('preload_wrap');
    if (!wrap) {
        wrap = document.createElement('DIV');
        wrap.id = 'preload_wrap';
        
        var parent = document.body || document.documentElement;
        if (parent.firstChild) {
            parent.insertBefore(wrap, parent.firstChild);
        }
        else {
            parent.appendChild(wrap);
        }
    }
    wrap.style.position = 'absolute';
    wrap.style.zIndex = 999999;
    wrap.style.left = '-299px';
    wrap.style.top = '10px';
    wrap.style.width = '300px';
    wrap.style.height = '400px';
    wrap.style.overflow = 'hidden';
};
hui.util.preload.loadNext = function (list) {
    var wrap = document.getElementById('preload_wrap'),
        container = document.createDocumentFragment(),
        div = document.createElement('DIV'),
        img;
    for (var i=0,ilen=list.length; i<ilen; i++) {
        div.innerHTML = '<img onload="hui.util.preload.onloadCallback(this)" width="10" height="10" />'; 
        img = div.firstChild && div.firstChild.onload ? div.firstChild : div.firstChild.nextSibling;
        img.src = list[i];
        img.setAttribute('str', list[i]);
        container.appendChild(img);
    }
    wrap.appendChild(container);
};

hui.util.preload.loadLeft = function () {
    var me = hui.util.preload,
        count;
    if (me.left && me.left.length) {
        count = me.maxCount || me.left.length;
        me.loadNext(me.left.splice(0, count));
    }
};

hui.util.preload.onloadCallback = function (elem) {
    var me = hui.util.preload,
        src = elem.getAttribute('str');
    if (!me.loaded && !me.left) {
        me.init();
    }
    me.loaded.push(src);
    
    for (var i=me.loading.length-1; i>-1; i--) {
        if (me.loading[i] === src) {
            me.loading.splice(i, 1);
        }
    }
    
    if (!me.finished && me.loading.length < 1 && me.left.length < 1) {
        me.finished = true;
        me.onloadAllCallback();
    }
    else {
        me.loadLeft();
    }
};




window.startOrientationListener = function () {
    if (window.DeviceOrientationEvent) {
        window.addEventListener('deviceorientation', function (e) {
            var leftRightAngle,
                frontBackAngle;
            // 我们从事件e中获取角度值并转化成弧度值。
            leftRightAngle = Math.round(e.gamma / 90.0 * Math.PI / 2 * 100);
            frontBackAngle = Math.round(e.beta / 90.0 * Math.PI / 2 * 100);
            if (window.onorientationchange) {
                window.onorientationchange(frontBackAngle, leftRightAngle);
            }
        }, false);
    } 
    else if (window.OrientationEvent) { //另一个选项是Mozilla版本同样的东西
        window.addEventListener('MozOrientation', function (e) {
            var leftRightAngle,
                frontBackAngle;
            // 在这里将长度值当做一个单位，并转换成角度值，看起来运行的不错。
            leftRightAngle = Math.round(e.x * Math.PI / 2 * 100);
            frontBackAngle = Math.round(e.y * Math.PI / 2 * 100);
            if (window.onorientationchange) {
                window.onorientationchange(frontBackAngle, leftRightAngle);
            }
        }, false);
    }
};