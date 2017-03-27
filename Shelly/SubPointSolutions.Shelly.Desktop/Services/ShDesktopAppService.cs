using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Core;
using SubPointSolutions.Shelly.Core.Services;
using SubPointSolutions.Shelly.Desktop.Events.App;

namespace SubPointSolutions.Shelly.Desktop.Services
{
    public class ShDesktopAppService : ShAppMetadataService
    {
        #region constructors

        public ShDesktopAppService()
        {
            Args = new List<string>();
        }

        #endregion

        #region properties

        public List<string> Args { get; set; }

        public Form AppForm { get; set; }

        #endregion


        #region methods

        protected virtual void InitInternal()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            InitAppDataServices();
            ConfigureAppDataServices();

            InitAppPlugins();
            CreateAppPlugins();
            CreateAppUIServices();

            AppForm = CreateAppForm();

            AppForm.FormClosing += form_FormClosing;

            InitAppUIServices(AppForm);
            ConfigureAppUIServices();

            ConfigureAppForm(AppForm);
        }

        public override void Run()
        {
            InitInternal();
            Application.Run(AppForm);
        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            RaiseEvent<ShOnAppExitEvent>();
        }

        protected virtual void CreateAppPlugins()
        {

        }


        protected virtual void ConfigureAppUIServices()
        {

        }

        protected virtual void ConfigureAppDataServices()
        {

        }

        protected virtual Form CreateAppForm()
        {
            var uiService = ShServiceContainer.Instance.AppMetadataService.AppUIServices.FirstOrDefault();

            if (uiService != null)
                return uiService.CreateControlInstance<Form>();

            return new Form();
        }

        protected virtual void ConfigureAppForm(Form form)
        {
            SetApplicationTitle(form, string.Format("{0} - {1}", AppName, AppVersion));

            form.Width = 800;
            form.Height = 600;

            form.StartPosition = FormStartPosition.CenterScreen;

            form.Load += (ss, ee) => { RaiseEvent(new ShOnAppLoadCompletedEvent()); };
        }

        public void SetApplicationTitle(string title)
        {
            SetApplicationTitle(AppForm, title);
        }

        private void SetApplicationTitle(Form form, string title)
        {
            form.Text = title;
            Application.DoEvents();
            form.Refresh();
        }

        #endregion


    }
}