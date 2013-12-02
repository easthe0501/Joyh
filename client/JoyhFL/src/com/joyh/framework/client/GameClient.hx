package com.joyh.framework.client;

/**
 * ...
 * @author hd
 */
class GameClient
{
	private var _data:IPlayerData;

	public function new() 
	{
		
	}
	
	public var data(get, null):IPlayerData;
	private inline function get_data():IPlayerData
	{
		return _data;
	}
}