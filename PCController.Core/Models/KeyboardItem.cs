using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.Core.Models
{
    /// <summary>
    /// 键盘按键
    /// </summary>
    public class KeyboardItem
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }

        public static KeyboardItem FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            KeyboardItem item = new KeyboardItem();
            item.Name = values[0];
            item.Code = values.Length >= 2 ? values[1] : null;
            item.Note = values.Length >= 3 ? values[2] : null;
            return item;
        }
    }
}
