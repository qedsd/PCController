using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCController.UserUI.Models
{
    /// <summary>
    /// 按键组合流程
    /// </summary>
    public class Shortcut
    {
        public string Name { get; set; }
        public ObservableCollection<KeyStatus> KeyStatuses { get; set; }
    }
}
