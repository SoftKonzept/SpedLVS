using LVS.Constants;
using Xamarin.Forms;

namespace LvsScan.Portable.Behaviors
{
    public class EntryCheckWeightBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (sender is Entry entry)
            {
                if (args.NewTextValue != null && (!args.NewTextValue.Equals(string.Empty)))
                {
                    int iInput = 0;
                    int.TryParse(args.NewTextValue.ToString(), out iInput);

                    if (constValue_ScanApp_IniDefault.const_ScanApp_Default_MaxArticleWeight > iInput)
                    {
                        entry.Text = iInput.ToString();
                    }
                    else
                    {
                        entry.Text = args.OldTextValue.ToString();
                        //string mesInfo = string.Empty;
                        //string message = string.Empty;
                        //message = "Der eingegebene Wert liegt über dem Konrollwert ("+ constValue_ScanApp_IniDefault.const_ScanApp_Default_MaxArticleWeight + ")!" + Environment.NewLine;
                        //message += "Bitte Eingabe wiederholen und prüfen!";

                        //mesInfo = "ACHTUNG";
                        //MessagingCenter.Send(this, "WeightError", message);

                    }
                }
            }
        }
    }
}
