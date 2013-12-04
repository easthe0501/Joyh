package com.joyh.framework.process;
import com.joyh.framework.process.StepProcess;

/**
 * ...
 * @author hd
 */
interface IStep
{
	var process:StepProcess;
	var key(get, null):String;
	function run():Void;
	function dispose():Void;
}