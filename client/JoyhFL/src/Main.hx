import com.haxepunk.Engine;
import com.haxepunk.HXP;
import com.joyh.demo.command.server.SvrLoadBattleCommand;
import com.joyh.demo.command.TestCommand;
import com.joyh.demo.define.Widgets;
import com.joyh.framework.asset.LoadAssetStep;
import com.joyh.framework.JHP;
import com.joyh.framework.process.StepProcess;
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
		
		JHP.init([new TestCommand(), new SvrLoadBattleCommand()]);
		super();
	}
	
	override public function init()
	{
#if debug
		HXP.console.enable();
#end
		HXP.screen.fixedScale = true;
		
		JHP.widgets.open(Widgets.Demo);
	}

	public static function main()
	{ 		
		new Main(); 
	}
}