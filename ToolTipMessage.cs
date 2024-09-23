using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class ToolTipMessageBase
    {
        public static ToolTip ToolTipMessage()
        {
            //ToolTipを作成する
            ToolTip ToolTip1 = new ToolTip();

            //ToolTipの設定を行う
            //ToolTip1.Active = true;

            //ToolTipが表示されるまでの時間
            ToolTip1.InitialDelay = 1;
            //ToolTipが表示されている時に、別のToolTipを表示するまでの時間
            ToolTip1.ReshowDelay = 1000;
            //ToolTipを表示する時間
            ToolTip1.AutoPopDelay = 10000;
            //フォームがアクティブでない時でもToolTipを表示する
            ToolTip1.ShowAlways = true;

            return ToolTip1;
        }
    }
}
