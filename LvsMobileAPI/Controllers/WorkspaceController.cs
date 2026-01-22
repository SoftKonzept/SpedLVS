using Common.ApiModels;
using Common.Models;
using LvsMobileAPI.Services;
using Microsoft.AspNetCore.Mvc;
using AllowAnonymousAttribute = LvsMobileAPI.Authorization.AllowAnonymousAttribute;

namespace LvsMobileAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/[controller]")]
    public class WorkspaceController : Controller
    {
        public IWorkspaceService _workspaceService;
        public WorkspaceController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        //-------------------------------------------------- GET  -------------------------------------------------------------------

        [AllowAnonymous]
        [HttpGet("GET_Workspace/{WorkspaceId}")]
        public IActionResult GET_Workspace(int WorkspaceId)
        {
            ResponseWorkspace resWorkspace = new ResponseWorkspace();
            resWorkspace.Success = false;

            if (WorkspaceId > 0)
            {
                var workspace = _workspaceService.GET_Workspace(WorkspaceId);
                if ((workspace is Workspaces) && (workspace.Id > 0))
                {
                    resWorkspace.Success = true;
                    resWorkspace.Workspace = workspace.Copy();
                    return Ok(resWorkspace);
                }
                else
                {
                    resWorkspace.Success = false;
                    resWorkspace.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                    return NotFound(resWorkspace);
                }
            }
            else
            {
                resWorkspace.Success = false;
                resWorkspace.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resWorkspace);
            }
        }

        [AllowAnonymous]
        [HttpGet("GET_Workspacelist")]
        public IActionResult GET_Workspacelist()
        {
            ResponseWorkspace resWorkspace = new ResponseWorkspace();
            resWorkspace.Success = false;
            resWorkspace.Error = string.Empty;
            resWorkspace.Info = string.Empty;

            var response = _workspaceService.GET_WorkspaceList();

            if ((response != null) && (response.Count > 0))
            {
                resWorkspace.Success = true;
                resWorkspace.ListWorkspaces = new List<Workspaces>(response);
                resWorkspace.Info = "Die Addressliste konnte ermittelt werden!";
                return Ok(resWorkspace);
            }
            else
            {
                resWorkspace.Success = false;
                resWorkspace.Error = "Achtung - Es konnte keine Daten ermittelt werden!";
                return NotFound(resWorkspace);
            }
        }


    }
}
