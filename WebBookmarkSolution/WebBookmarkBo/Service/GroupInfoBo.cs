using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBookmarkService.DAL;

namespace WebfolderBo.Service
{
    public class GroupInfoBo
    {

        private static GroupInfoDAL DAL = new GroupInfoDAL();
        public static bool DeleteGroupInfo(long groupID)
        {
           return DAL.DeleteByGroupInfoID(groupID) > 0;
           
        }
    }
}
