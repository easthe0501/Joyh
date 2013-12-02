package com.joyh.framework.command;
import haxe.ds.HashMap.HashMap;

/**
 * ...
 * @author hd
 */
interface ICommand
{
	var id(get, null):Int;
	function run(args:Dynamic):Void;
}