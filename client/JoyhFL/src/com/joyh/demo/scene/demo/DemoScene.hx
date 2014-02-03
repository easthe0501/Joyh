package com.joyh.demo.scene.demo;

import com.haxepunk.graphics.Backdrop;
import com.haxepunk.HXP;
import com.haxepunk.Scene;
import com.haxepunk.utils.Input;
import com.haxepunk.utils.Key;
import com.joyh.framework.JHP;
import haxe.macro.Context;
import haxe.macro.Expr;

class DemoScene extends Scene
{
	private var _background:Backdrop;
	
	public var width(get_width, null):Int;
	private function get_width():Int
	{
		return _background.width;
	}
	
	public var height(get_height, null):Int;
	private function get_height():Int
	{
		return _background.height;
	}
	
	public override function begin()
	{
		HXP.camera.x = 800;
		HXP.camera.y = 1100;
		
		//_background = new Backdrop("gfx/demo/battle.jpg", false, false);
		_background = JHP.assets.createBackdrop("battle.jpg");
		addGraphic(_background);
		
		var player:DemoPlayer = new DemoPlayer(1080, 1300);
		add(player);
		player.layer = -2;
	}
	
	override public function update() 
	{
		super.update();
			
		//var text:String = new Test().getText();
		HXP.console.log(["x=" + HXP.camera.x, "y=" + HXP.camera.y]);
	}
}