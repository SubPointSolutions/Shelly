namespace SubPointSolutions.Shelly.Core.Extensibility
{
    public interface IPlugin
    {
        string Title { get; set; }
        string Description { get; set; }

        void Init();
        void ShutDown();
    }
}
