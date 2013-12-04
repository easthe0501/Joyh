package com.joyh.framework;
import com.joyh.framework.asset.AssetFactory;
import com.joyh.framework.asset.LoadAssetStep;
import com.joyh.framework.client.GameClient;
import com.joyh.framework.command.CommandFactory;
import com.joyh.framework.command.ICommand;
import com.joyh.framework.process.IStep;
import com.joyh.framework.process.StepProcess;
import com.joyh.framework.setting.SettingFacade;
import com.joyh.framework.utils.JsonEx;
import com.joyh.framework.widget.WidgetFacade;
import haxe.Json;
import haxe.macro.Context;
import haxe.macro.Expr;
import openfl.Assets;
#if macro
import sys.io.File;
#end

/**
 * ...
 * @author hd
 */
class JHP
{
	private static var _client:GameClient;
	private static var _settings:SettingFacade;
	private static var _widgets:WidgetFacade;
	private static var _commands:CommandFactory;
	private static var _assets:AssetFactory;
	
	public static function init(commands:Array<ICommand>):Void
	{
		_client = new GameClient();
		_settings = SettingFacade.create();
		_widgets = new WidgetFacade();
		_commands = new CommandFactory(commands);
		_assets = new AssetFactory();
	}	
	
	public static var client(get, null):GameClient;
	private static inline function get_client():GameClient
	{
		return _client;
	}
	
	public static var settings(get, null):SettingFacade;
	private static inline function get_settings():SettingFacade
	{
		return _settings;
	}
	
	public static var widgets(get, null):WidgetFacade;
	private static inline function get_widgets():WidgetFacade
	{
		return _widgets;
	}
	
	public static var commands(get, null):CommandFactory;
	private static inline function get_commands():CommandFactory
	{
		return _commands;
	}
	
	public static var assets(get, null):AssetFactory;
	private static inline function get_assets():AssetFactory
	{
		return _assets;
	}
	
	public static function runCommand(id:Int, args:Dynamic = null):Bool
	{
		return JHP.commands.run(id, args);
	}
	
	public static function loadAccets(urls:Array<String>, stepCallback:Dynamic=null, completeCallback:Dynamic=null):Void
	{
		var steps:Array<IStep> = new Array();
		for (url in urls)
		{
			var step = new LoadAssetStep(url);
			steps.push(step);
		}
		var process = new StepProcess(steps);
		process.onStep = stepCallback;
		process.onComplete = completeCallback;
		process.goon();
	}
}