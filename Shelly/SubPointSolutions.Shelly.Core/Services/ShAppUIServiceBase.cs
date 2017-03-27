namespace SubPointSolutions.Shelly.Core.Services
{
    public abstract class ShAppUIServiceBase : ShServiceBase
    {
        #region methods
        public abstract void Init(object uiHostControl);

        public virtual TControl CreateControlInstance<TControl>()
            where TControl : class, new()
        {
            return new TControl();
        }

        #endregion
    }
}
