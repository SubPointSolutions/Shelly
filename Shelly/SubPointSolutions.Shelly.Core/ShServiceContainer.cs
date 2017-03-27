using SubPointSolutions.Shelly.Core.Interfaces;
using SubPointSolutions.Shelly.Core.Services;

namespace SubPointSolutions.Shelly.Core
{
    public class ShServiceContainer
    {
        #region static

        static ShServiceContainer()
        {
            Instance = new ShServiceContainer();
        }

        public static ShServiceContainer Instance { get; private set; }

        #endregion

        #region constructors

        internal ShServiceContainer()
        {
            
        }

        #endregion

        #region internal

        public ShAppMetadataService AppMetadataService { get; set; }

        #endregion

        #region public API

        //public QPluralizationDataService QPluralizationAppDataService
        //{
        //    get { return QuarkAppMetadataService.GetAppDataService<QPluralizationDataService>(); }
        //}

        public ShTraceServiceBase TraceService
        {
            get
            {
                return AppMetadataService.GetAppDataService<ShTraceServiceBase>();
            }
        }

        public IAppEventService EventsService
        {
            get
            {
                return AppMetadataService.GetAppDataService<ShAppEventDataService>();
            }
        }

        //public QSolutionDataService SolutionService
        //{
        //    get
        //    {
        //        return QuarkAppMetadataService.GetAppDataService<QSolutionDataService>();
        //    }
        //}

        //public QConnectionsDataService ConnectionsService
        //{
        //    get
        //    {
        //        return QuarkAppMetadataService.GetAppDataService<QConnectionsDataService>();
        //    }
        //}
        public ShSerializationDataService SerializationService
        {
            get
            {
                return AppMetadataService.GetAppDataService<ShSerializationDataService>();
            }
        }

        //public QBusinessEntityDataService BusinessEntityService
        //{
        //    get
        //    {
        //        return QuarkAppMetadataService.GetAppDataService<QBusinessEntityDataService>();
        //    }
        //}

        public TService GetAppDataService<TService>()
            where TService : ShAppDataServiceBase
        {
            return AppMetadataService.GetAppDataService<TService>();
        }

        #endregion
    }
}
