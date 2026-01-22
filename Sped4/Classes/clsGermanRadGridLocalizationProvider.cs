using Telerik.WinControls.UI.Localization;

namespace Sped4.Classes
{
    public class clsGermanRadGridLocalizationProvider : RadGridLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            switch (id)
            {
                case RadGridStringId.FilterFunctionBetween: return "zwischen";
                case RadGridStringId.FilterFunctionContains: return "beinhaltet";
                case RadGridStringId.FilterFunctionDoesNotContain: return "beinhaltet nicht";
                case RadGridStringId.FilterFunctionEndsWith: return "ended mit";
                case RadGridStringId.FilterFunctionEqualTo: return "entspricht";
                case RadGridStringId.FilterFunctionGreaterThan: return "größer als";
                case RadGridStringId.FilterFunctionGreaterThanOrEqualTo: return "größer als oder entspricht";
                case RadGridStringId.FilterFunctionIsEmpty: return "ist leer";
                case RadGridStringId.FilterFunctionIsNull: return "ist Null";
                case RadGridStringId.FilterFunctionLessThan: return "weniger als";
                case RadGridStringId.FilterFunctionLessThanOrEqualTo: return "weniger als oder entspricht";
                case RadGridStringId.FilterFunctionNoFilter: return "kein Filter";
                case RadGridStringId.FilterFunctionNotBetween: return "nicht zwischen";
                case RadGridStringId.FilterFunctionNotEqualTo: return "entspricht nicht";
                case RadGridStringId.FilterFunctionNotIsEmpty: return "ist nicht leer";
                case RadGridStringId.FilterFunctionNotIsNull: return "ist nicht Null";
                case RadGridStringId.FilterFunctionStartsWith: return "beginnt mit";
                case RadGridStringId.FilterFunctionCustom: return "Benutzerdefiniert";

                case RadGridStringId.FilterOperatorBetween: return "Between";
                case RadGridStringId.FilterOperatorContains: return "Contains";
                case RadGridStringId.FilterOperatorDoesNotContain: return "NotContains";
                case RadGridStringId.FilterOperatorEndsWith: return "EndsWith";
                case RadGridStringId.FilterOperatorEqualTo: return "Equals";
                case RadGridStringId.FilterOperatorGreaterThan: return "GreaterThan";
                case RadGridStringId.FilterOperatorGreaterThanOrEqualTo: return "GreaterThanOrEquals";
                case RadGridStringId.FilterOperatorIsEmpty: return "IsEmpty";
                case RadGridStringId.FilterOperatorIsNull: return "IsNull";
                case RadGridStringId.FilterOperatorLessThan: return "LessThan";
                case RadGridStringId.FilterOperatorLessThanOrEqualTo: return "LessThanOrEquals";
                case RadGridStringId.FilterOperatorNoFilter: return "No filter";
                case RadGridStringId.FilterOperatorNotBetween: return "NotBetween";
                case RadGridStringId.FilterOperatorNotEqualTo: return "NotEquals";
                case RadGridStringId.FilterOperatorNotIsEmpty: return "NotEmpty";
                case RadGridStringId.FilterOperatorNotIsNull: return "NotNull";
                case RadGridStringId.FilterOperatorStartsWith: return "StartsWith";
                case RadGridStringId.FilterOperatorIsLike: return "Like";
                case RadGridStringId.FilterOperatorNotIsLike: return "NotLike";
                case RadGridStringId.FilterOperatorIsContainedIn: return "ContainedIn";
                case RadGridStringId.FilterOperatorNotIsContainedIn: return "NotContainedIn";
                case RadGridStringId.FilterOperatorCustom: return "Benutzerdefiniert";

                case RadGridStringId.CustomFilterMenuItem: return "Benutzerdefiniert";
                case RadGridStringId.CustomFilterDialogCaption: return "Filter Dialog [{0}]";
                case RadGridStringId.CustomFilterDialogLabel: return "alle Zeilen:";
                case RadGridStringId.CustomFilterDialogRbAnd: return "UND";
                case RadGridStringId.CustomFilterDialogRbOr: return "ODER";
                case RadGridStringId.CustomFilterDialogBtnOk: return "OK";
                case RadGridStringId.CustomFilterDialogBtnCancel: return "Abbrechen";
                case RadGridStringId.CustomFilterDialogCheckBoxNot: return "Nicht";
                case RadGridStringId.CustomFilterDialogTrue: return "True";
                case RadGridStringId.CustomFilterDialogFalse: return "False";

                case RadGridStringId.FilterMenuAvailableFilters: return "verfügbare Filter";
                case RadGridStringId.FilterMenuSearchBoxText: return "Suche...";
                case RadGridStringId.FilterMenuClearFilters: return "Filter zurücksetzen";
                case RadGridStringId.FilterMenuButtonOK: return "OK";
                case RadGridStringId.FilterMenuButtonCancel: return "Abbrechen";
                case RadGridStringId.FilterMenuSelectionAll: return "Alle";
                case RadGridStringId.FilterMenuSelectionAllSearched: return "Alle Suchergebnisse";
                case RadGridStringId.FilterMenuSelectionNull: return "Null";
                case RadGridStringId.FilterMenuSelectionNotNull: return "Not Null";

                case RadGridStringId.FilterFunctionSelectedDates: return "Filter nach Datum:";
                case RadGridStringId.FilterFunctionToday: return "Heute";
                case RadGridStringId.FilterFunctionYesterday: return "Gestern";
                case RadGridStringId.FilterFunctionDuringLast7days: return "letzten 7 Tage";

                case RadGridStringId.FilterLogicalOperatorAnd: return "UND";
                case RadGridStringId.FilterLogicalOperatorOr: return "ODER";
                case RadGridStringId.FilterCompositeNotOperator: return "NICHT";

                case RadGridStringId.DeleteRowMenuItem: return "Zeile löschen";
                case RadGridStringId.SortAscendingMenuItem: return "aufsteigend sortieren";
                case RadGridStringId.SortDescendingMenuItem: return "absteigend sortieren";
                case RadGridStringId.ClearSortingMenuItem: return "Sortierung aufheben";
                case RadGridStringId.ConditionalFormattingMenuItem: return "Conditional Formatting";
                case RadGridStringId.GroupByThisColumnMenuItem: return "Gruppieren nach Spalte";
                case RadGridStringId.UngroupThisColumn: return "Gruppierung nach Spalte aufheben";
                case RadGridStringId.ColumnChooserMenuItem: return "Column Chooser";
                case RadGridStringId.HideMenuItem: return "Spalte ausblenden";
                case RadGridStringId.HideGroupMenuItem: return "Gruppe ausblenden";
                case RadGridStringId.UnpinMenuItem: return "Spalte lösen";
                case RadGridStringId.UnpinRowMenuItem: return "Zeile lösen";
                case RadGridStringId.PinMenuItem: return "Verankerungsstatus";
                case RadGridStringId.PinAtLeftMenuItem: return "links verankern";
                case RadGridStringId.PinAtRightMenuItem: return "rechts verankern";
                case RadGridStringId.PinAtBottomMenuItem: return "am Schluß verankern";
                case RadGridStringId.PinAtTopMenuItem: return "am Anfang verankern";
                case RadGridStringId.BestFitMenuItem: return "autom. Spaltenbreite";
                case RadGridStringId.PasteMenuItem: return "einfügen";
                case RadGridStringId.EditMenuItem: return "ändern";
                case RadGridStringId.ClearValueMenuItem: return "Clear Value";
                case RadGridStringId.CopyMenuItem: return "Copy";
                case RadGridStringId.CutMenuItem: return "Cut";
                case RadGridStringId.AddNewRowString: return "Click here to add a new row";
                case RadGridStringId.ConditionalFormattingSortAlphabetically: return "Sort columns alphabetically";
                case RadGridStringId.ConditionalFormattingCaption: return "Conditional Formatting Rules Manager";
                case RadGridStringId.ConditionalFormattingLblColumn: return "Format only cells with";
                case RadGridStringId.ConditionalFormattingLblName: return "Rule name";
                case RadGridStringId.ConditionalFormattingLblType: return "Cell value";
                case RadGridStringId.ConditionalFormattingLblValue1: return "Value 1";
                case RadGridStringId.ConditionalFormattingLblValue2: return "Value 2";
                case RadGridStringId.ConditionalFormattingGrpConditions: return "Rules";
                case RadGridStringId.ConditionalFormattingGrpProperties: return "Rule Properties";
                case RadGridStringId.ConditionalFormattingChkApplyToRow: return "Apply this formatting to entire row";
                case RadGridStringId.ConditionalFormattingChkApplyOnSelectedRows: return "Apply this formatting if the row is selected";
                case RadGridStringId.ConditionalFormattingBtnAdd: return "Add new rule";
                case RadGridStringId.ConditionalFormattingBtnRemove: return "Remove";
                case RadGridStringId.ConditionalFormattingBtnOK: return "OK";
                case RadGridStringId.ConditionalFormattingBtnCancel: return "Cancel";
                case RadGridStringId.ConditionalFormattingBtnApply: return "Apply";
                case RadGridStringId.ConditionalFormattingRuleAppliesOn: return "Rule applies to";
                case RadGridStringId.ConditionalFormattingCondition: return "Condition";
                case RadGridStringId.ConditionalFormattingExpression: return "Expression";
                case RadGridStringId.ConditionalFormattingChooseOne: return "[Choose one]";
                case RadGridStringId.ConditionalFormattingEqualsTo: return "equals to [Value1]";
                case RadGridStringId.ConditionalFormattingIsNotEqualTo: return "is not equal to [Value1]";
                case RadGridStringId.ConditionalFormattingStartsWith: return "starts with [Value1]";
                case RadGridStringId.ConditionalFormattingEndsWith: return "ends with [Value1]";
                case RadGridStringId.ConditionalFormattingContains: return "contains [Value1]";
                case RadGridStringId.ConditionalFormattingDoesNotContain: return "does not contain [Value1]";
                case RadGridStringId.ConditionalFormattingIsGreaterThan: return "is greater than [Value1]";
                case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual: return "is greater than or equal [Value1]";
                case RadGridStringId.ConditionalFormattingIsLessThan: return "is less than [Value1]";
                case RadGridStringId.ConditionalFormattingIsLessThanOrEqual: return "is less than or equal to [Value1]";
                case RadGridStringId.ConditionalFormattingIsBetween: return "is between [Value1] and [Value2]";
                case RadGridStringId.ConditionalFormattingIsNotBetween: return "is not between [Value1] and [Value1]";
                case RadGridStringId.ConditionalFormattingLblFormat: return "Format";

                case RadGridStringId.ConditionalFormattingBtnExpression: return "Expression editor";
                case RadGridStringId.ConditionalFormattingTextBoxExpression: return "Expression";

                case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitive: return "CaseSensitive";
                case RadGridStringId.ConditionalFormattingPropertyGridCellBackColor: return "CellBackColor";
                case RadGridStringId.ConditionalFormattingPropertyGridCellForeColor: return "CellForeColor";
                case RadGridStringId.ConditionalFormattingPropertyGridEnabled: return "Enabled";
                case RadGridStringId.ConditionalFormattingPropertyGridRowBackColor: return "RowBackColor";
                case RadGridStringId.ConditionalFormattingPropertyGridRowForeColor: return "RowForeColor";
                case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignment: return "RowTextAlignment";
                case RadGridStringId.ConditionalFormattingPropertyGridTextAlignment: return "TextAlignment";

                case RadGridStringId.ColumnChooserFormCaption: return "Spaltenauswahl";
                case RadGridStringId.ColumnChooserFormMessage: return "Drag a column header from the\ngrid here to remove it from\nthe current view.";
                case RadGridStringId.GroupingPanelDefaultMessage: return "fügen Sie eine Spalte per Drag and Drop hinzu, um nach der Spalte zu gruppieren.";
                case RadGridStringId.GroupingPanelHeader: return "Gruppieren nach:";
                case RadGridStringId.NoDataText: return "keine Daten vorhanden";
                case RadGridStringId.CompositeFilterFormErrorCaption: return "Filter Error";
                case RadGridStringId.CompositeFilterFormInvalidFilter: return "The composite filter descriptor is not valid.";

                case RadGridStringId.ExpressionMenuItem: return "Expression";
                case RadGridStringId.ExpressionFormTitle: return "Expression Builder";
                case RadGridStringId.ExpressionFormFunctions: return "Functions";
                case RadGridStringId.ExpressionFormFunctionsText: return "Text";
                case RadGridStringId.ExpressionFormFunctionsAggregate: return "Aggregate";
                case RadGridStringId.ExpressionFormFunctionsDateTime: return "Date-Time";
                case RadGridStringId.ExpressionFormFunctionsLogical: return "Logical";
                case RadGridStringId.ExpressionFormFunctionsMath: return "Math";
                case RadGridStringId.ExpressionFormFunctionsOther: return "Other";
                case RadGridStringId.ExpressionFormOperators: return "Operators";
                case RadGridStringId.ExpressionFormConstants: return "Constants";
                case RadGridStringId.ExpressionFormFields: return "Fields";
                case RadGridStringId.ExpressionFormDescription: return "Description";
                case RadGridStringId.ExpressionFormResultPreview: return "Result preview";
                case RadGridStringId.ExpressionFormTooltipPlus: return "Plus";
                case RadGridStringId.ExpressionFormTooltipMinus: return "Minus";
                case RadGridStringId.ExpressionFormTooltipMultiply: return "Multiply";
                case RadGridStringId.ExpressionFormTooltipDivide: return "Divide";
                case RadGridStringId.ExpressionFormTooltipModulo: return "Modulo";
                case RadGridStringId.ExpressionFormTooltipEqual: return "Equal";
                case RadGridStringId.ExpressionFormTooltipNotEqual: return "Not Equal";
                case RadGridStringId.ExpressionFormTooltipLess: return "Less";
                case RadGridStringId.ExpressionFormTooltipLessOrEqual: return "Less Or Equal";
                case RadGridStringId.ExpressionFormTooltipGreaterOrEqual: return "Greater Or Equal";
                case RadGridStringId.ExpressionFormTooltipGreater: return "Greater";
                case RadGridStringId.ExpressionFormTooltipAnd: return "Logical \"AND\"";
                case RadGridStringId.ExpressionFormTooltipOr: return "Logical \"OR\"";
                case RadGridStringId.ExpressionFormTooltipNot: return "Logical \"NOT\"";
                case RadGridStringId.ExpressionFormAndButton: return string.Empty; //if empty, default button image is used
                case RadGridStringId.ExpressionFormOrButton: return string.Empty; //if empty, default button image is used
                case RadGridStringId.ExpressionFormNotButton: return string.Empty; //if empty, default button image is used
                case RadGridStringId.ExpressionFormOKButton: return "OK";
                case RadGridStringId.ExpressionFormCancelButton: return "Cancel";
            }

            return string.Empty;
        }
    }
}
