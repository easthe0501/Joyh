package com.joyh.framework;
import com.joyh.framework.client.GameClient;
import com.joyh.framework.command.CommandFactory;
import com.joyh.framework.command.ICommand;
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
	private static var _settings:SettingFacade;
	private static var _widgets:WidgetFacade;
	private static var _commands:CommandFactory;
	private static var _client:GameClient;
	
	public static function init(commands:Array<ICommand>):Void
	{
		_settings = new SettingFacade();
		_widgets = new WidgetFacade();
		_commands = new CommandFactory(commands);
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
	
	public static var client(get, null):GameClient;
	private static inline function get_client():GameClient
	{
		return _client;
	}
}