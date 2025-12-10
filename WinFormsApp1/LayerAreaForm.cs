using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace WinFormsApp1
{
    public partial class LayerAreaForm : Form
    {
        public LayerAreaForm()
        {
            InitializeComponent();
        }

        private void btnSelectArea_Click(object sender, EventArgs e)
        {
            string layerName = txtLayer.Text.Trim();

            if (string.IsNullOrEmpty(layerName))
            {
                MessageBox.Show("Please enter a layer name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Hide();

            DeleteLayerEntitiesInsideArea(layerName);

            this.Close();
        }

        private void DeleteLayerEntitiesInsideArea(string layerName)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            try
            {
                TypedValue[] filter = new TypedValue[]
                {
                    new TypedValue((int)DxfCode.LayerName, layerName)
                };
                SelectionFilter selectionFilter = new SelectionFilter(filter);

                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = $"\nSelect an area (Window/Crossing) to remove entities on layer '{layerName}':";

                PromptSelectionResult selRes = ed.GetSelection(pso, selectionFilter);

                if (selRes.Status != PromptStatus.OK)
                {
                    ed.WriteMessage("\nSelection cancelled or no matching objects found in the area.");
                    return;
                }

                using (doc.LockDocument())
                {
                    using (Transaction tr = doc.Database.TransactionManager.StartTransaction())
                    {
                        int erasedCount = 0;

                        foreach (ObjectId id in selRes.Value.GetObjectIds())
                        {
                            Entity ent = tr.GetObject(id, OpenMode.ForWrite) as Entity;

                            if (ent != null && !ent.IsErased)
                            {
                                ent.Erase();
                                erasedCount++;
                            }
                        }

                        tr.Commit();
                        ed.WriteMessage($"\nOperation Complete: {erasedCount} entities on layer '{layerName}' removed.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                ed.WriteMessage($"\nError: {ex.Message}");
            }
        }

        private void txtLayer_TextChanged(object sender, EventArgs e)
        {
        }
    }
}