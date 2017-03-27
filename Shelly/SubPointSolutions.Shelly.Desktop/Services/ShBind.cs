namespace SubPointSolutions.Shelly.Desktop.Services
{
    public class ShBinder
    {
        public static ShBindService<TSrc> NewBinder<TSrc>(TSrc src)
            where TSrc : class, new()
        {
            return new ShBindService<TSrc>(src);
        }
    }
}
