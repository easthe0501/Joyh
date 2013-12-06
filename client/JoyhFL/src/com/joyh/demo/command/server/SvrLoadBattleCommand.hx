package com.joyh.demo.command.server;

import com.haxepunk.HXP;
import com.joyh.framework.JHP;
import com.joyh.framework.command.CommandBase;
import com.joyh.demo.define.Commands;
import com.joyh.framework.process.IStep;

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
	}
}