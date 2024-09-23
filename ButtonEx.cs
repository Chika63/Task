using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class ButtonEx : Button
    {
        //[System.Security.Permissions.UIPermission(
        //    System.Security.Permissions.SecurityAction.Demand,
        //    Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //    //左キーが押されているか調べる
        //    if ((keyData & Keys.KeyCode) == Keys.Left)
        //    {
        //        MessageBox.Show("左キーが押されました。");
        //        //左キーの本来の処理（左側のコントロールにフォーカスを移す）を
        //        //させたくないときは、trueを返す
        //        //return true;
        //    }

        //    return base.ProcessDialogKey(keyData);
        //}
    }
}
