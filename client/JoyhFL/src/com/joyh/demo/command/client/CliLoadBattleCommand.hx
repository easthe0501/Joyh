package com.joyh.demo.command.client;

import com.haxepunk.HXP;
import com.joyh.framework.JHP;
import com.joyh.framework.command.CommandBase;
import com.joyh.framework.process.IStep;
import com.joyh.demo.define.Commands;
import com.joyh.demo.scene.demo.DemoScene;

/**
 * ...
 * @author hd
 */
class CliLoadBattleCommand extends CommandBase
{
	override private function get_id():Int 
	{
		return Commands.LoadBattle;
	}
	
	override private function run(args:Dynamic):Void 
	{
		var id = cast(args, Int);
		JHP.call(onLoadComplete, ["image/battle.jpg", "image/a004.png", "image/m004.png", "image/d004.png"]);
	}
	
	private function onLoadComplete():Void
	{
		HXP.scene = new DemoScene();
		JHP.alert("battle reloaded.");
		//JHP.widgets.showLoading("test", 1, 12);
	}
}