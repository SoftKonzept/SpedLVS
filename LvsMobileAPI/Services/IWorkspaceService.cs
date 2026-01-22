using Common.Models;
using LVS.ViewData;
using LvsMobileAPI.DataConnection;

namespace LvsMobileAPI.Services
{
    public interface IWorkspaceService
    {
        public Workspaces GET_Workspace(int WorkspaceId);
        public List<Workspaces> GET_WorkspaceList();
    }


    public class WorkspaceService : IWorkspaceService
    {
        private SvrSettings srv;

        public WorkspaceService()
        {
            srv = new SvrSettings();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="WorkspaceId"></param>
        /// <returns></returns>
        public Workspaces GET_Workspace(int WorkspaceId)
        {
            Workspaces retWorkspace = new Workspaces();
            if (WorkspaceId > 0)
            {
                WorkspaceViewData wVD = new WorkspaceViewData(WorkspaceId);
                if (
                        (wVD.Workspace is Workspaces) &&
                        (wVD.Workspace.Id > 0)
                   )
                {
                    retWorkspace = wVD.Workspace.Copy();
                }
            }
            return retWorkspace;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resAdr"></param>
        /// <returns></returns>
        public List<Workspaces> GET_WorkspaceList()
        {
            WorkspaceViewData spaceVD = new WorkspaceViewData();
            spaceVD.GetWorkspaceList();
            return spaceVD.ListWorkspace;
        }
    }


}
