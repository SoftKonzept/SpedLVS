using Xamarin.Forms;

namespace LvsScan.Portable.Behaviors
{
    public class EntryNullBehavior : Behavior<Entry>
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
                if (args.NewTextValue != null && args.NewTextValue.Equals(string.Empty))
                {
                    entry.Text = null;
                    //entry.Text = String.Empty;
                }
            }
        }
    }
}
