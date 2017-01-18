using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using global::Toggl.Services;

namespace Mooboo.ToggleWrapper.Services
{
    using global::Toggl.QueryObjects;
    using global::Toggl.Services;
    using Toggl;

    public class BaseAPI
    {
        protected WorkspaceService WorkspaceService;
        protected ClientService ClientService;
        protected TaskService TaskService;
        protected ProjectService ProjectService;
        protected TagService TagService;
        protected UserService UserService;
        protected TimeEntryService TimeEntryService;
        protected ReportService ReportService;

        protected int DefaultWorkspaceId;
        protected int GoodProjectId;
        protected int BadProjectId;

        public BaseAPI(string token)
        {
            WorkspaceService = new WorkspaceService(token);
            var workspaces = WorkspaceService.List();

            // init all possible services
            ClientService = new ClientService(token);
            TaskService = new TaskService(token);
            TagService = new TagService(token);
            ProjectService = new ProjectService(token);
            UserService = new UserService(token);
            TimeEntryService = new TimeEntryService(token);
            ReportService = new ReportService(token);

            DefaultWorkspaceId = GetMoobooWorkspaceId();

            GoodProjectId = GetGoodProjectId();
            BadProjectId = GetBadProjectId();
        }


        private int GetMoobooWorkspaceId()
        {
            var moobooWorkspace = WorkspaceService.List().Find(w => String.Equals(w.Name, "Mooboo"));
            if (moobooWorkspace != null)
                return moobooWorkspace.Id.Value;

            //TODO: find a way to create new workspace
            throw new Exception();
        }

        private int GetGoodProjectId()
        {
            return GetOrCreateProject("Good", 5);
        }

        private int GetBadProjectId()
        {
            return GetOrCreateProject("Bad", 13);
        }

        private int GetOrCreateProject(string name, int color)
        {
            var match = ProjectService.List().Find(x => string.Equals(x.Name, name));
            if (match != null)
                return match.Id.Value;

            var project = ProjectService.Add(new Project
            {
                IsBillable = true,
                WorkspaceId = DefaultWorkspaceId,
                Name = name,
                IsAutoEstimates = false
            });

            return project.Id.Value;
        }
    }
}
