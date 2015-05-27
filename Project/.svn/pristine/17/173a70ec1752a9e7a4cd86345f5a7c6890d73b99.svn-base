var IMG = function(BOX,LEFT,RIGHT,WIDTH,HHUM,PLAY,SPEED,AOUT){
	var html = BOX.html();
	BOX.append(html); 
	var _count    = HHUM.length;
	var _Index    = 0;
	var _time     = null; 	//初始计时器
	//开始移动
	var Start = function (){
		if(_Index < 0){
			var l_width = WIDTH * _count;
			BOX.stop(false,true).stop().animate({'top':'-'+l_width+'px'}, 0);
			_Index = (_count) - 1;
		}else if (_Index >= (_count)+1){
			BOX.stop(false,true).stop().animate({'top':0+'px'}, 0);
			_Index = 1;
		}
		move();
	}
	var move = function (){
		Stop();
		_target = -1 * WIDTH * _Index;
		BOX.stop(false,true).stop().animate({'top':_target+'px'}, SPEED);
		if (AOUT) _time = setTimeout( function(){ _Index++; Start(); }, PLAY );
	};
	LEFT.click(function(){
		_Index--;
		Start();
		}
	);
	RIGHT.click(function(){
		_Index++;
		Start();
		}
	);
	var Stop = function (){ clearTimeout(_time); }
	Start();
}