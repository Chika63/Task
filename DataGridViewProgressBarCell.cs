using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Task
{
    public class DataGridViewProgressBarCell : DataGridViewTextBoxCell
    {
        public DataGridViewProgressBarCell()
        {
            this.ValueType = typeof(int); // セルの値はint型
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            int progressVal = Convert.ToInt32(value); // セルの値をintに変換
            float percentage = ((float)progressVal / 100.0f); // プログレスの割合 (0.0 ~ 1.0)

            // プログレスバーの色
            Brush progressBarBrush = Brushes.LightGreen;

            // プログレスバーの描画
            graphics.FillRectangle(progressBarBrush, cellBounds.X + 2, cellBounds.Y + 2, Convert.ToInt32((percentage * (cellBounds.Width - 4))), cellBounds.Height - 4);

            // プログレスの値を文字で表示
            string text = progressVal.ToString() + "%";
            SizeF textSize = graphics.MeasureString(text, cellStyle.Font);
            Brush textBrush = Brushes.Black;

            // テキストの描画
            graphics.DrawString(text, cellStyle.Font, textBrush, cellBounds.X + (cellBounds.Width - textSize.Width) / 2, cellBounds.Y + (cellBounds.Height - textSize.Height) / 2);
        }
    }

    public class DataGridViewProgressBarColumn : DataGridViewColumn
    {
        public DataGridViewProgressBarColumn()
        {
            this.CellTemplate = new DataGridViewProgressBarCell(); // カスタムセルを設定
        }
    }


}
