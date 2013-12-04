package com.joyh.framework.setting;
import haxe.macro.Context;
import haxe.xml.Fast;

/**
 * ...
 * @author hd
 */
class SettingFacade
{	
	public static function create():SettingFacade
	{
		var content = getContent();
		var xml = Xml.parse(content);
		var fast = new Fast(xml.firstElement());
		
		var settings = new SettingFacade();
		settings.cdn.root = fast.node.cdn.att.root;
		
		return settings;
	}
	
	macro private static function getContent()
	{
		var content = sys.io.File.getContent("settings.xml");
		return Context.makeExpr(content, Context.currentPos());
	}
	
	private var _cdn:CdnSetting; 
	
	private function new() 
	{
		_cdn = new CdnSetting();
	}
	
	public var cdn(get, null):CdnSetting;
	private inline function get_cdn():CdnSetting
	{
		return _cdn;
	}
	
	public function getUrl(url:String):String
	{
		return _cdn.root + url;
	}
}