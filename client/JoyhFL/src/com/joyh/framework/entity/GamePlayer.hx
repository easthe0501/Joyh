package com.joyh.framework.entity;

import com.haxepunk.Entity;
import com.haxepunk.Graphic;
import com.haxepunk.graphics.Spritemap;
import com.haxepunk.HXP;
import com.haxepunk.Mask;
import com.haxepunk.utils.Input;
import com.haxepunk.utils.Key;
import com.joyh.framework.JHP;
import openfl.Assets;

/**
 * ...
 * @author hd
 */
class GamePlayer extends Entity
{
	private var _speed:Int = 100;
	private var _bodyWidth:Int = 24;
	private var _leftDir:Bool = true;
	private var _spMove:Spritemap;
	private var _spAttack:Spritemap;
	private var _isAttack:Bool = false;
		
	public function new(x:Float=0, y:Float=0, graphic:Graphic=null, mask:Mask=null) 
	{
		super(x, y, graphic, mask);
		
		//_spMove = new Spritemap(Assets.getBitmapData("gfx/demo/m004.png"), 48, 48, null, "gfx/demo/m004.png");
		_spMove = JHP.assets.createSpritemap("m004.png", 48, 48);
		_spMove.add("run", [4, 5], 5, true);
		_spMove.add("idle", [8], 5, true);
		
		_spAttack = JHP.assets.createSpritemap("a004.png", 64, 64);
		_spAttack.add("attack", [8, 9, 10, 11], 5, false);
		
		this.graphic = _spMove;
	}
	
	override public function update():Void 
	{
		if (_isAttack)
		{
			if (_spAttack.complete)
			{
				_isAttack = false;
				if (_leftDir)
					moveBy(8, 8);
				else
					moveBy(8, 8);
			}
			else
				return;
		}
		
		if (Input.check(Key.J))
		{
			_isAttack = true;
			graphic = _spAttack;
			if (_leftDir)
			{				
				_spAttack.flipped = false;
				moveBy(-8, -8);
			}
			else
			{
				_spAttack.flipped = true;
				moveBy(-8, -8);
			}
			_spAttack.play("attack", true);
		}
		else
		{
			graphic = _spMove;
			var dx:Int = 0;
			var dy:Int = 0;
			var dis:Int = Math.round(_speed * HXP.elapsed);
			if (Input.check(Key.W))
			{
				dy = -dis;
			}
			else if (Input.check(Key.S))
			{
				dy = dis;
			}
			if (Input.check(Key.A))
			{
				dx = -dis;
				_leftDir = true;
			}
			else if (Input.check(Key.D))
			{
				dx = dis;
				_leftDir = false;
			}
			if (dx != 0 || dy != 0)
			{
				_spMove.flipped = !_leftDir;
				_spMove.play("run");
				moveBy(dx, dy);
				HXP.camera.x += dx;
				HXP.camera.y += dy;
			}
			else
				_spMove.play("idle");
		}
	}
}