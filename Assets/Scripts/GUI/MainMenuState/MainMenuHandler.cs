using System.Linq;
using JoyGodot.Assets.Scripts.GUI.Managed_Assets;
using JoyLib.Code.Graphics;
using JoyLib.Code.States;

namespace JoyLib.Code.Unity.GUI.MainMenuState
{
    public class MainMenuHandler : GUIData
    {
        public override void _Ready()
        {
            ManagedUIElement background = new ManagedUIElement
            {
                AnchorBottom = 1,
                AnchorRight = 1,
                ElementName = "DefaultWindow"
            };

            ISpriteState state = GlobalConstants.GameManager.GUIManager.UISprites["DefaultWindow"];
            
            this.AddChild(background);
            this.MoveChild(background, 0);
            
            //background.AddSpriteState(state);
            GlobalConstants.GameManager.GUIManager.SetupManagedComponents(this);
        }

        public void NewGame()
        {
            GlobalConstants.GameManager.GUIManager.CloseAllGUIs();
            GlobalConstants.GameManager.SetNextState(new CharacterCreationState());
        }
    }
}