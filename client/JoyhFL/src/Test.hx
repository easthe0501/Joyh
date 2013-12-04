package ;
import haxe.macro.Context;
import haxe.macro.Expr;

/**
 * ...
 * @author hd
 */
class Test
{
	private var _text:String;

	public function new() 
	{
		_text = getFileContent("project.xml");
	}
	
	public function getText():String
	{
		return _text;
	}
	
	macro public static function getFileContent( fileName : String )
	{
		var content = sys.io.File.getContent(fileName);
		//var content = Date.now().toString();
		return Context.makeExpr(content,Context.currentPos());
	}
}