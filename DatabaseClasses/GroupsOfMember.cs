#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media;

#endregion

namespace DatabaseClasses {

    public class GroupsOfMember {
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int CutoffDay { get; set; }
        public bool IsApproved { get; set; }
        public Brush ForegroundBrush { get; set; } 

        public static List<GroupsOfMember> TableToList(DataTable dt) {
            if (dt == null) {
                throw new NoNullAllowedException("Table cannot be null");
            }
            var rowCount = dt.Rows.Count;
            var groupList = new List<GroupsOfMember>(rowCount);
            if (dt.Rows.Count == 0) {
                return groupList;
            }
            for (var i = 0; i < rowCount; i++) {
                var group        = new GroupsOfMember();
                group.Name       = dt.Rows[i][0].ToString();
                group.IsAdmin    = Convert.ToBoolean(dt.Rows[i][1].ToString());
                group.UserId     = Convert.ToInt32(dt.Rows[i][2].ToString());
                group.GroupId    = Convert.ToInt32(dt.Rows[i][3].ToString());
                group.IsApproved = Convert.ToBoolean(dt.Rows[i][4].ToString());
                group.CutoffDay  = Convert.ToInt32(dt.Rows[i][5].ToString());
                @group.ForegroundBrush = @group.IsAdmin ? Brushes.Red : Brushes.Black;
                groupList.Add(group);
            }
            return groupList;
        }
    }

}