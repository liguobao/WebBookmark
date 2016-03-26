using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBookmarkUI.Models
{
    public class UIShowUserInfo :SearchUserInfo
    {
        public List<UIWebFolderInfo> WebFolderList { get; set; }

        public int BookmarkCount { get; set; }


        public int FolderCount { get; set; }

    }
}