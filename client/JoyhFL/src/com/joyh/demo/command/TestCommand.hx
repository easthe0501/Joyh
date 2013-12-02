package com.joyh.demo.command;

import com.haxepunk.HXP;
import com.haxepunk.Scene;
import com.joyh.demo.define.Commands;
import com.joyh.framework.command.CommandBase;
import com.joyh.framework.command.ICommand;
import com.joyh.framework.scene.GameScene;
import ru.stablex.ui.UIBuilder;

/**
 * ...
 * @author hd
 */
class TestCommand extends CommandBase
{
	override private function get_id():Int 
	{
		return Commands.Test;
	}
	
	override public function run(args:Dynamic):Void 
	{
		if(!Std.is(HXP.scene, GameScene))
			HXP.scene = new GameScene();
		else
			HXP.scene = new Scene();
	}	
}