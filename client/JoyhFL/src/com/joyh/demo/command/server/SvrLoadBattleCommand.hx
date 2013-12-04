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
	
	override public function run(args:Dynamic):Void 
	{
		var id = cast(args, Int);
		JHP.loadAccets(["battle.jpg", "a004.png", "d004.png", "m004.png"], onLoadStep, onLoadComplete);
	}
	
	private function onLoadStep(currentStep:IStep, totalCount:Int):Void
	{
		
	}
	
	private function onLoadComplete(totalCount:Int):Void
	{
		HXP.scene = new GameScene();
	}
}