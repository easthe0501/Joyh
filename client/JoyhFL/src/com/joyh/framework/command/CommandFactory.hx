package com.joyh.framework.command;
import com.joyh.framework.JHP;
import haxe.ds.HashMap.HashMap;
import haxe.Json;

/**
 * ...
 * @author hd
 */
class CommandFactory
{
	private var _hash:Map<Int,ICommand>;
	
	public function new(commands:Array<ICommand>) 
	{
		_hash = new Map();
		for (command in commands)
		{
			_hash.set(command.id, command);
		}
	}
	
	public function all():Array<ICommand>
	{
		return Lambda.array(_hash);
	}
	
	public function find(id:Int):ICommand
	{
		return _hash.get(id);
	}
	
	public function exec(id:Int, args:Dynamic = null):Bool
	{
		var command = find(id);
		if (command != null)
			return command.exec(args);
		return false;
	}
}