using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Runtime.InteropServices;
using System.Globalization;

namespace DateControlLibrary
{
    public class ThrDateTimePicker : DateTimePicker
    {
        private MaskedTextBox textbox;
        private int buttonWidth;
        private string maskPattern;
        private string datePattern;

        public ThrDateTimePicker()
        {
            textbox = new MaskedTextBox();
            textbox.BorderStyle = BorderStyle.None;
            // textbox.BackColor = Color.Gold;
            maskPattern = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern
                .Replace("dd", "00")
                .Replace("d", "00")
                .Replace("MM", "00")
                .Replace("M", "00")
                .Replace("y", "0");
            datePattern = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern
                .Replace("dd", "1")
                .Replace("d", "1")
                .Replace("MM", "2")
                .Replace("M", "2")
                .Replace("1", "dd")
                .Replace("2", "MM");
            textbox.Mask = maskPattern;
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = ".";
            this.Controls.Add(textbox);

            textbox.Leave += new EventHandler(this.OnTextChange);
        }

        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = textbox.Font = value; }
        }

        protected override void OnResize(EventArgs e)
        {
            if (buttonWidth == 0) measureButtonWidth();
            var margin = (this.ClientSize.Height - textbox.PreferredHeight) / 2;
            textbox.Location = new Point(margin, margin);
            textbox.Width = this.ClientSize.Width - margin - buttonWidth - 11;
            base.OnResize(e);
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Console.Write("DateTimePickerChanged: ");
            // Console.WriteLine(Value);
            textbox.Text = this.Value.ToString(datePattern);
            base.OnValueChanged(eventargs);
        }

        private void OnTextChange(object sender, EventArgs e)
        {
            // Console.Write("TextChanged: ");
            // Console.WriteLine(textbox.Text);
            if (DateTime.TryParse(textbox.Text, out DateTime value))
            {
                try
                {
                    this.Value = value;
                }
                catch (Exception)
                {
                    textbox.Text = this.Value.ToString(datePattern);
                }
            }
            else
            {
                textbox.Text = this.Value.ToString(datePattern);
            }
            // Console.Write("DateTime value now: ");
            // Console.WriteLine(this.Value);
        }

        private void measureButtonWidth()
        {
            if (!Application.RenderWithVisualStyles) buttonWidth = 21;   // TODO: measure
            else
            {
                var renderer = new VisualStyleRenderer("DATEPICKER", 3, 1);
                using (var gr = CreateGraphics())
                {
                    buttonWidth = renderer.GetPartSize(gr, ThemeSizeType.True).Height;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) textbox.Dispose();
            base.Dispose(disposing);
        }
    }
}
