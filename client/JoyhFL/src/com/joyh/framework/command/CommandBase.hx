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
	
	public function run(args:Dynamic):Void 
	{
		
	}
	
}