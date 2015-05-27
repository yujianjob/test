var imgUrl = "http://wx.landaojia.com/images/fenxiang-1.jpg";
var lineLink = "http://wx.landaojia.com/Top/SpecialEvents";
var descContent = "关注懒到家微信并绑定手机，现金送不停！赶快拉上小伙伴，一起免费洗衣吧。";
var shareTitle = "拉上小伙伴速来领钱啦~点击链接立即领取5元现金券！";
var appid = "wx9ab0f092bce2810f";
var inviteCode = "";

function shareFriend() {
    if (inviteCode == "")
        alert("您尚未绑定，邀请好友将无法获得奖励");
    WeixinJSBridge.invoke("sendAppMessage", {
        "appid": appid,
        "img_url": imgUrl,
        //"img_width": "200",
        //"img_height": "200",
        "link": lineLink,
        "desc": descContent,
        "title": shareTitle
    }, function (res) {
        //_report('send_msg', res.err_msg);
    })
}
function shareTimeline() {
    if (inviteCode == "")
        alert("您尚未绑定，邀请好友将无法获得奖励");
    WeixinJSBridge.invoke("shareTimeline", {
        "img_url": imgUrl,
        //"img_width": "200",
        //"img_height": "200",
        "link": lineLink,
        "desc": descContent,
        "title": shareTitle
    }, function (res) {
        //_report('timeline', res.err_msg);
    });
}
function shareWeibo() {
    if (inviteCode == "")
        alert("您尚未绑定，邀请好友将无法获得奖励");
    WeixinJSBridge.invoke("shareWeibo", {
        "content": descContent,
        "url": lineLink,
    }, function (res) {
        //_report('weibo', res.err_msg);
    });
}

function Share(code) {
    inviteCode = code;
    lineLink = "http://wx.landaojia.com/Top/SpecialEvents?code=" + code;
    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {
            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    } else {
        onBridgeReady();
    }
}

function onBridgeReady() {
    // 发送给好友
    WeixinJSBridge.on('menu:share:appmessage', function (argv) {
        shareFriend();
    });
    // 分享到朋友圈
    WeixinJSBridge.on('menu:share:timeline', function (argv) {
        shareTimeline();
    });
    // 分享到微博
    WeixinJSBridge.on('menu:share:weibo', function (argv) {
        shareWeibo();
    });


    // 通过下面这个API隐藏底部导航栏
    WeixinJSBridge.call('hideToolbar');
}

function AddContact(name) {
    var rtn = "";
    WeixinJSBridge.invoke("addContact", { webtype: "1", username: name }, function (e) {
        WeixinJSBridge.log(e.err_msg);
        rtn = e.err_msg;
    });
    return rtn;
}

function AddContact2() {
    WeixinJSBridge.invoke('profile', {
        "username": "gh_f48133ed2c12",
        "scene": "57",
    }, function (res) {
        //_report('weibo', res.err_msg);
    });
}