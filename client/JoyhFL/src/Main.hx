import com.haxepunk.Engine;
import com.haxepunk.HXP;
import com.joyh.demo.command.TestCommand;
import com.joyh.demo.define.Widgets;
import com.joyh.framework.JHP;
import haxe.macro.Context;
import openfl.Assets;
import com.joyh.framework.scene.GameScene;
import ru.stablex.ui.UIBuilder;
import ru.stablex.ui.widgets.Button;

class Main extends Engine
{	
	public function new() 
	{		
		UIBuilder.regClass("com.joyh.framework.JHP");
		UIBuilder.regClass("com.joyh.demo.define.Commands");
		UIBuilder.regClass("com.joyh.demo.define.Widgets");
		UIBuilder.init();
		UIBuilder.buildFn("assets/ui/demo.xml")();
		
		JHP.init([new TestCommand()]);
		
		super();
	}
	
	override public function init()
	{
#if debug
		HXP.console.enable();
#end
		HXP.screen.fixedScale = true;
		
		var ui = JHP.widgets.find(Widgets.Demo);
		HXP.engine.addChild(ui);
	}

	public static function main()
	{ 		
		new Main(); 
	}
}