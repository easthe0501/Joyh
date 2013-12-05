package com.joyh.framework.widget;
import com.haxepunk.HXP;
import ru.stablex.ui.UIBuilder;
import ru.stablex.ui.widgets.Text;
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
	
	public function current():Widget
	{
		return _current;
	}
	
	public function opened():List<Widget>
	{
		return _opened;
	}
	
	public function open(id:String, closeOther:Bool=true):Widget
	{
		if (Lambda.exists(_opened, function(v) { return v.id == id; } ))
			return find(id);
		var widget = find(id);
		if (closeOther)
			closeAll();
		HXP.engine.addChild(widget);
		_opened.add(widget);
		_current = widget;
		return _current;
	}
	
	public function close(id:String):Void
	{
		var xx = id;
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
	
	public function showLoading(content:String, curStep:Int, maxStep:Int):Void
	{
		var uiLoading = open("loading", false);
		uiLoading.getChildAs("content", Text).text = content;
		uiLoading.getChildAs("step", Text).text = curStep + " / " + maxStep;
		var progress = uiLoading.getChildAs("progress", ru.stablex.ui.widgets.Progress);
		progress.max = maxStep;
		progress.value = curStep;
	}
	
	public function closeLoading():Void
	{
		close("loading");
	}
}