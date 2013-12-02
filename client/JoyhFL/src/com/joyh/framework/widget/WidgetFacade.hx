package com.joyh.framework.widget;
import com.haxepunk.HXP;
import ru.stablex.ui.UIBuilder;
import ru.stablex.ui.widgets.Widget;

/**
 * ...
 * @author hd
 */
class WidgetFacade
{
	private var _current:Widget;
	private var _opened:List<Widget>;
	
	public function new()
	{
		_opened = new List();
	}
	
	public function find(id:String):Widget
	{
		return UIBuilder.get(id);
	}
	
	public function findCurrent():Widget
	{
		return _current;
	}
	
	public function findOpened():List<Widget>
	{
		return _opened;
	}
	
	public function open(id:String, closeOther:Bool=true):Void
	{
		if (Lambda.exists(_opened, function(v) { return v.id == id; } ))
			return;
		var widget = find(id);
		if (closeOther)
			closeAll();
		HXP.engine.addChild(widget);
		_opened.add(widget);
		_current = widget;
	}
	
	public function close(id:String):Void
	{
		var widget = _opened.filter(function(v) { return v.id == id; }).first();
		if (widget == null)
			return;
		HXP.engine.removeChild(widget);
		_opened.remove(widget);
		if (_current == widget)
			_current = _opened.last();
	}
	
	public function closeAll():Void
	{
		for (widget in _opened)
			HXP.engine.removeChild(widget);
		_opened.clear();
	}
}