using Sitecore.Pathfinder.Tasks.Building;

namespace Sitecore.Pathfinder.Tasks
{
    public class UpdateDataProvider : WebBuildTaskBase
    {
        public UpdateDataProvider() : base("update-mappings")
        {
        }

        public override void Run(IBuildContext context)
        {
            context.Trace.TraceInformation(Msg.M1009, Texts.Updating_the_project_website_mappings_on_the_website___);

            var webRequest = GetWebRequest(context).AsTask("UpdateProjectWebsiteMappings");

            Post(context, webRequest);
        }

        public override void WriteHelp(HelpWriter helpWriter)
        {
            helpWriter.Summary.Write("Updates the project/website mapping on the website.");
            helpWriter.Remarks.WriteLine("The 'project-website-mappings' settings enables the serializing data provider on the website to serialize changed items back to the projects.");
            helpWriter.Remarks.WriteLine("This task should be called when the 'project-website-mappings' settings have been changed (or you can just kill the 'w3wp.exe' process or restart IIS).");
        }
    }
}