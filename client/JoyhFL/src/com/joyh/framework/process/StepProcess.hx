package com.joyh.framework.process;
import flash.errors.Error;

/**
 * ...
 * @author hd
 */
class StepProcess
{		
	private var _steps:Array<IStep>;
	private var _index:Int = 0;
	
	public function new(steps:Array<IStep>) 
	{
		_steps = steps;
		if(_steps == null || _steps.length == 0)
			throw new Error("Empty process.");
		for(step in _steps)
			step.process = this;
	}
			
	public function getPreviousStep():IStep
	{
		var idx:Int = _index - 1;
		return idx < _steps.length ? _steps[idx] : null;
	}
	
	public function getCurrentStep():IStep
	{
		return _index < _steps.length ? _steps[_index] : null;
	}
	
	public var currentIndex(get, null):Int;
	private inline function get_currentIndex():Int
	{
		return _index;
	}
	
	public var totalCount(get, null):Int;
	private inline function get_totalCount():Int
	{
		return _steps.length;
	}
	
	public function goon():Void
	{
		var preStep:IStep = getPreviousStep();
		if(preStep != null)
			preStep.dispose();
		var curStep:IStep = getCurrentStep();
		if(curStep != null)
		{
			onStep(getCurrentStep(), _steps.length);
			_index++;
			curStep.run();
		}
		else if(_index >= _steps.length)
		{
			onComplete(_steps.length);
			_steps = null;
		}
	}
	
	dynamic public function onStep(currentStep:IStep, totalCount:Int):Void
	{
		
	}
	
	dynamic public function onComplete(totalCount:Int):Void
	{
		
	}
}