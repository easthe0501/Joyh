package com.joyh.framework.asset;

import com.joyh.framework.process.IStep;
import com.joyh.framework.JHP;
import com.joyh.framework.process.StepBase;
import flash.display.Loader;
import flash.events.Event;
import flash.events.IOErrorEvent;
import flash.net.URLRequest;
import flash.net.URLRequestMethod;

/**
 * ...
 * @author hd
 */
class LoadAssetStep extends StepBase implements IStep
{
	private var _request:URLRequest;
	private var _loader:Loader;

	public function new(url:String)
	{
		super();
		_key = url;
		_request = new URLRequest();
		_request.url = JHP.settings.getUrl(url);
		_request.method = URLRequestMethod.GET;
		
		_loader = new Loader();
		_loader.contentLoaderInfo.addEventListener(Event.COMPLETE, onAssetLoaded);
		_loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, retry);
		
	}
	
	public function run():Void
	{
		if(JHP.assets.get(_key) == null)
			_loader.load(_request);
		else
			this.process.goon();
	}
	
	public function dispose():Void
	{
		_loader.contentLoaderInfo.removeEventListener(Event.COMPLETE, onAssetLoaded);
		_loader.contentLoaderInfo.removeEventListener(IOErrorEvent.IO_ERROR, retry);
	}
	
	private function onAssetLoaded(e:Event):Void
	{
		JHP.assets.set(_key, e.currentTarget.content);
		this.process.goon();
	}
	
}