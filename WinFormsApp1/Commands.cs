using Autodesk.AutoCAD.Runtime;
namespace WinFormsApp1
{
    public class Commands
    {
        [CommandMethod("OPEN_LAYER_POPUP")]
        public void OpenLayerPopup()
        {
            LayerAreaForm form = new LayerAreaForm();
            form.Show();
        }
    }
}
