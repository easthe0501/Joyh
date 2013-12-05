package com.joyh.framework.process;

import com.joyh.framework.process.IStep;
import flash.errors.Error;
import flash.events.IOErrorEvent;

/**
 * ...
 * @author hd
 */
class StepBase
{
	private var _key:String;
	private var _retryCount:Int = 0;	
	public var index:Int;
	public var process:StepProcess;

	public function new() 
	{
		
	}
	
	public var key(get, null):String;
	private inline function get_key():String
	{
		return _key;
	}
	
	private function retry(e:IOErrorEvent):Void 
	{
		if(++_retryCount > 5)
			throw new Error("Failed for 5 times.\n" + e.toString());
		if(Std.is(this, IStep))
			cast(this, IStep).run();
	}
	
}