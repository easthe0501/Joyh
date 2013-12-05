package com.joyh.framework.command;

/**
 * ...
 * @author hd
 */
class CommandBase implements ICommand
{

	public function new() 
	{
		
	}
	
	/* INTERFACE com.joyh.framework.command.ICommand */
	
	public var id(get, null):Int;	
	private function get_id():Int
	{
		return 10;
	}
	
	public function exec(args:Dynamic):Bool
	{
		if (ready(args))
		{
			run(args);
			return true;
		}
		return false;
	}
	
	private function ready(args:Dynamic):Bool
	{
		return true;
	}
	
	private function run(args:Dynamic):Void 
	{
		
	}
	
}