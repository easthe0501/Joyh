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
	
	public var onStep:IStep->Int->Void;
	public var onComplete:Void->Void;
	
	public function new(steps:Array<IStep>) 
	{
		_steps = steps;
		if(_steps == null || _steps.length == 0)
			throw new Error("Empty process.");
		var i = 0;
		for (step in _steps)
		{
			step.index = i++;
			step.process = this;
		}
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
		if(onStep != null)
			onStep(curStep, _steps.length);
		if(curStep != null)
		{
			_index++;
			curStep.run();
		}
		else if(_index >= _steps.length)
		{
			if(onComplete != null)
				onComplete();
			_steps = null;
		}
	}
}