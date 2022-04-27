using Sandbox;
using Sandbox.UI;

namespace GM.UI;

public partial class UIEntity : HudEntity<RootPanel>
{
	public UIEntity()
	{
		if ( IsClient )
		{
			RootPanel.StyleSheet.Load( "/UI/UIEntity.scss" );

			// TODO: RootPanel.AddChild<GameControlPanel>();
		}
	}

	[Event( "gm.state" )]
	public void OnStateChanged( GMMain.GMState state )
	{
		// TODO: react somehow

		if ( state == GMMain.GMState.Editor )
			RootPanel.AddChild<EditorPanel>();
	}
}
