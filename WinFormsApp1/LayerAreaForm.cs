using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Windows.Forms;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Exception = System.Exception;

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

        private void DeleteLayerEntitiesInsideArea(string layerNames)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var ed = doc.Editor;

            try
            {
                // Split comma separated layer names
                var layerList = layerNames.Split(',')
                                          .Select(l => l.Trim())
                                          .Where(l => !string.IsNullOrEmpty(l))
                                          .ToList();

                if (layerList.Count == 0)
                {
                    ed.WriteMessage("\nNo valid layer names provided.");
                    return;
                }

                // Build OR filter for layers
                List<TypedValue> filterValues = new List<TypedValue>();
                filterValues.Add(new TypedValue((int)DxfCode.Operator, "<or"));

                foreach (var layer in layerList)
                {
                    filterValues.Add(new TypedValue((int)DxfCode.LayerName, layer));
                }

                filterValues.Add(new TypedValue((int)DxfCode.Operator, "or>"));

                SelectionFilter selectionFilter = new SelectionFilter(filterValues.ToArray());

                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding =
                    $"\nSelect an area to remove entities on layers: {string.Join(", ", layerList)}";

                PromptSelectionResult selRes = ed.GetSelection(pso, selectionFilter);

                if (selRes.Status != PromptStatus.OK)
                {
                    ed.WriteMessage("\nSelection cancelled or no matching objects found in the area.");
                    return;
                }

                using (doc.LockDocument())
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
                    ed.WriteMessage(
                        $"\nOperation Complete: {erasedCount} entities removed from layers ({string.Join(", ", layerList)})."
                    );
                }
            }
            catch (Exception ex)
            {
                ed.WriteMessage($"\nError: {ex.Message}");
            }
        }


        private void txtLayer_TextChanged(object sender, EventArgs e)
        {
        }
    }
}