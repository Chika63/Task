    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class TextBoxEx : TextBox
    {
        private string _placeholder = string.Empty;

        // （プロパティ）
        public string Placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value;
                Invalidate();
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == 15) //  WM_PAINT == 15
            {
                if (this.Enabled && !this.ReadOnly && !this.Focused && (_placeholder != null) && (_placeholder.Length > 0) && (this.TextLength == 0))
                {
                    using (var g = this.CreateGraphics())
                    {
                        // 描画を一旦消してしまう
                        g.FillRectangle(new System.Drawing.SolidBrush(this.BackColor), this.ClientRectangle);

                        // プレースホルダのテキスト色を、前景色と背景色の中間として文字列を描画する
                        var placeholderTextColor = System.Drawing.Color.FromArgb((this.ForeColor.A >> 1 + this.BackColor.A >> 1), (this.ForeColor.R >> 1 + this.BackColor.R >> 1), ((this.ForeColor.G >> 1 + this.BackColor.G) >> 1), (this.ForeColor.B >> 1 + this.BackColor.B >> 1));
                        g.DrawString(_placeholder, this.Font, new System.Drawing.SolidBrush(placeholderTextColor), 1, 1);
                    }
                }
            }
        }
    }
}
