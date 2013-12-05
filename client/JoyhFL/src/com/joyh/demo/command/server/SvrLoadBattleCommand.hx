package com.joyh.demo.command.server;

import com.haxepunk.HXP;
import com.joyh.framework.JHP;
import com.joyh.framework.command.CommandBase;
import com.joyh.demo.define.Commands;
import com.joyh.framework.process.IStep;
import com.joyh.framework.scene.GameScene;

/**
 * ...
 * @author hd
 */
class SvrLoadBattleCommand extends CommandBase
{
	override private function get_id():Int 
	{
		return Commands.LoadBattle;
	}
	
	override private function run(args:Dynamic):Void 
	{
		var id = cast(args, Int);
		JHP.call(onLoadComplete, ["battle.jpg", "a004.png", "d004.png", "m004.png"]);
	}
	
	private function onLoadComplete():Void
	{
		HXP.scene = new GameScene();
		JHP.alert("battle reloaded.");
		//JHP.widgets.showLoading("test", 1, 12);
	}
}