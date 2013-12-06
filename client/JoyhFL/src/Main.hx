import com.haxepunk.Engine;
import com.haxepunk.HXP;
import com.joyh.demo.command.client.CliLoadBattleCommand;
import com.joyh.demo.command.server.SvrLoadBattleCommand;
import com.joyh.demo.define.Widgets;
import com.joyh.framework.asset.LoadAssetStep;
import com.joyh.framework.JHP;
import com.joyh.framework.process.StepProcess;
import haxe.macro.Context;
import openfl.Assets;
import ru.stablex.ui.UIBuilder;
import ru.stablex.ui.widgets.Button;
import ru.stablex.ui.widgets.Text;

class Main extends Engine
{	
	public function new() 
	{		
		UIBuilder.regClass("com.joyh.framework.JHP");
		UIBuilder.regClass("com.joyh.demo.define.Commands");
		UIBuilder.regClass("com.joyh.demo.define.Widgets");
		
		UIBuilder.init();
		UIBuilder.regSkins("assets/ui/skins.xml");
		
		UIBuilder.buildFn("assets/ui/widgets/demo.xml")();
		UIBuilder.buildFn("assets/ui/widgets/alert.xml")();
		UIBuilder.buildFn("assets/ui/widgets/loading.xml")();
		
		JHP.init([new CliLoadBattleCommand()], [new SvrLoadBattleCommand()]);
		super();
	}
	
	override public function init()
	{
#if debug
		HXP.console.enable();
#end
		HXP.screen.fixedScale = true;
		
		var uiDemo = JHP.widgets.open(Widgets.Demo);
		//JHP.alert("xxyyzz");
		//JHP.widgets.showLoading("content", 12, 18);
	}

	public static function main()
	{ 		
		new Main(); 
	}
}