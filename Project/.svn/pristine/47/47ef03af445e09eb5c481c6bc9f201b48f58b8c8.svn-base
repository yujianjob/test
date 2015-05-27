$(function () {
    //左侧帐号焦点
    $('.homeZuoMember .user').focus(function () {
        var arr = $(this).attr('value');
        if (arr == '请输入卡号') {
            $(this).attr('value', '');
        }
    }).blur(function () {
        var arr = $(this).attr('value');
        if (arr == '') {
            $(this).attr('value', '请输入卡号');
        }
    });
    //左侧密码焦点
    $('.homeZuoMember .password').focus(function () {
        var arr = $(this).attr('value');
        if (arr == '请输入密码') {
            $(this).attr('value', '');
        }
    }).blur(function () {
        var arr = $(this).attr('value');
        if (arr == '') {
            $(this).attr('value', '请输入密码');
        }
    });
    //切换
    $('.homeYouShopTitle li').click(function () {
        var li = $(this).index();
        $('.homeYouShopTitle li').removeClass('this');
        $(this).addClass('this');
        $('.homeYouShopTitle div span').hide();
        $('.homeYouShopTitle div span').eq(li).show();
        $('.homeYouShopList ul').hide();
        $('.homeYouShopList ul').eq(li).show();
    })
    //保养
    $('.contentYouList .contentYouListImg').hover(function () {
        $(this).addClass('this');
    }, function () {
        $(this).removeClass('this');
    })
    //左侧导航
    $('.homeZuoNav dd').click(function () {
        $('.homeZuoNav dd').removeClass('this');
        $(this).addClass('this');
    })
    //首页banner
    var _index = 0;
    var a_dt = $(".homeBanner dt");
    var _count = a_dt.length;//6
    var _time = null;
    var bo = true;
    //开始移动
    var Banner = function () {
        if (_index < 0) _index = _count - 1;
        else if (_index >= _count) _index = 0;
        move();
    }
    //移动函数
    var move = function () {
        Stop();
        a_dt.removeClass();
        a_dt.eq(_index).addClass('this');
        $('.homeBanner li').stop(false, true).stop().fadeOut('slow');
        $('.homeBanner li').eq(_index).stop(false, true).stop().fadeIn('slow');
        if (bo) _time = setTimeout(function () { _index++; Banner(); }, 5000);
    };
    //停止
    var Stop = function () { clearTimeout(_time); }
    a_dt.each(function (i) {
        $(this).click(function () {
            _index = i;
            bo = true;
            Banner();
        });
    });
    $(".left-arrow").click(function () {
        _index--;
        if (_index < 0) {
            _index = _count - 1;
        }
        move();
    });
    $(".right-arrow").click(function () {
        _index++;
        if (_index > _count - 1) {

            _index = 0;
        }
        move();
    });
    Banner();
    //主题活动图片切换
    var hdBannerA = $('.hdbanner dt');
    var _hdcount = hdBannerA.length;
    var _hdIndex = 0;
    var _hdtime = null;
    var _hdAout = true;
    //开始移动
    var hdBanner = function () {
        if (_hdIndex < 0) _hdIndex = _hdcount - 1;
        else if (_hdIndex >= _hdcount) _hdIndex = 0;
        hdmove();
    }
    //移动函数
    var hdmove = function () {
        hdStop();
        hdBannerA.removeClass();
        hdBannerA.eq(_hdIndex).addClass('this');
        $('.hdbanner li').stop(false, true).stop().fadeOut('slow');
        $('.hdbanner li').eq(_hdIndex).stop(false, true).stop().fadeIn('slow');
        if (_hdAout) _hdtime = setTimeout(function () { _hdIndex++; hdBanner(); }, 5000);
    };
    //停止
    var hdStop = function () { clearTimeout(_hdtime); }
    $(hdBannerA).click(
        function () {
            _hdIndex = hdBannerA.index(this);
            _hdAout = true; hdBanner();
        }
    );
    //hdBanner();
});