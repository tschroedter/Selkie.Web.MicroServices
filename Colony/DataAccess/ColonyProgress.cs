namespace Selkie.Web.MicroServices.Colony.DataAccess
{
    public static class ColonyProgress
    {
        public enum Status
        {
            Unknown = 0,
            Creating = 1,
            Created = 2,
            Starting = 3,
            Started = 4,
            Running = 5,
            Stopping = 7,
            Stopped = 8,
            Finished = 9
        }
    }
}