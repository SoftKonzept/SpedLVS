using System;
using Telerik.WinControls.UI;

namespace Sped4.Classes
{
    public class MyGridViewTextBoxColumn : GridViewTextBoxColumn
    {
        public MyGridViewTextBoxColumn() { }

        public MyGridViewTextBoxColumn(string fieldName)
            : base(fieldName)
        {
        }

        public MyGridViewTextBoxColumn(string uniqueName, string fieldName)
            : base(uniqueName, fieldName)
        {
        }

        public override void InitializeEditor(IInputEditor editor)
        {
            base.InitializeEditor(editor);


            if (this.OwnerTemplate.MasterTemplate.CurrentRow is GridViewNewRowInfo)
            {
                BaseGridEditor gridEditor = editor as BaseGridEditor;
                RadDropDownListEditorElement element = gridEditor.EditorElement as RadDropDownListEditorElement;
                element.DataSource = new string[] { "one", "two", "three" };
            }

        }

        public override Type GetDefaultEditorType()
        {
            if (this.OwnerTemplate.MasterTemplate.CurrentRow is GridViewNewRowInfo)
            {
                return typeof(MyRadDropDownListEditor);
            }

            return base.GetDefaultEditorType();
        }
    }
}
