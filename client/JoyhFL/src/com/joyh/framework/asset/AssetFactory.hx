package com.joyh.framework.asset;
import com.haxepunk.graphics.Backdrop;
import com.haxepunk.graphics.Spritemap;
import com.joyh.framework.JHP;
import com.joyh.framework.process.IStep;
import com.joyh.framework.process.StepProcess;
import flash.display.BitmapData;
import flash.display.Bitmap;

/**
 * ...
 * @author hd
 */
class AssetFactory
{
	private var _assets:Map<String, Dynamic>;

	public function new() 
	{
		_assets = new Map<String, Dynamic>();
	}
	
	public function get(key:String):Dynamic
	{
		return _assets.get(key);
	}
	
	public function set(key:String, value:Dynamic):Void
	{
		_assets.set(key, value);
	}
	
	public function getBitmap(key:String):BitmapData
	{
		var bitmap = cast(get(key), Bitmap);
		return bitmap != null ? bitmap.bitmapData : null;
	}
	
	public function createBackdrop(key:String, repeatX:Bool = false, repeatY:Bool = false):Backdrop
	{
		return new Backdrop(getBitmap(key), repeatX, repeatY);
	}
	
	public function createSpritemap(key:String, cellWidth:Int, cellHeight:Int):Spritemap
	{
		return new Spritemap(getBitmap(key), cellWidth, cellHeight, null, key);
	}
	
	public function createStepProcess(urls:Array<String>):StepProcess
	{
		var steps:Array<IStep> = new Array();
		for (url in urls)
		{
			var step = new LoadAssetStep(url);
			steps.push(step);
		}
		var process = new StepProcess(steps);
		process.onStep = onProcessStep;
		return process;
	}
	
	dynamic public function onProcessStep(currentStep:IStep, totalCount:Int):Void
	{
		if (currentStep != null)
			JHP.widgets.showLoading(currentStep.key, currentStep.index+1, totalCount);
		else
			JHP.widgets.closeLoading();
	}
}