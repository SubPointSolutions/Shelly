using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Core.Utils;
using SubPointSolutions.Shelly.Desktop.Utils;

namespace SubPointSolutions.Shelly.Desktop.Services
{
    public class ShBindService<TSrc>
        where TSrc : class, new()
    {
        #region constructors

        public ShBindService(TSrc src)
        {
            Src = src;
        }

        #endregion


        #region properties

        public TSrc Src { get; set; }

        #endregion

        public ShBindService<TSrc> AutoBindProperty<TControl>(Expression<Func<TSrc, object>> srcExp,
            TControl control)
            where TControl : Control
        {
            return AutoBindProperty(srcExp, control, null);
        }

        #region methods

        public ShBindService<TSrc> AutoBindProperty<TControl>(Expression<Func<TSrc, object>> srcExp,
           TControl control, object dataSource)
           where TControl : Control
        {
            var type = typeof(TControl);

            if (typeof(TextBox).IsAssignableFrom(type))
            {
                var textBox = control as TextBox;
                BindProperty(srcExp, textBox, c => c.Text, dataSource);
            }
            else if (typeof(CheckBox).IsAssignableFrom(type))
            {
                var checkbox = control as CheckBox;
                BindProperty(srcExp, checkbox, c => c.Checked, dataSource);
            }
            else if (typeof(Label).IsAssignableFrom(type))
            {
                var label = control as Label;
                BindProperty(srcExp, label, c => c.Text, dataSource);
            }
            else if (typeof(Button).IsAssignableFrom(type))
            {
                var button = control as Button;
                BindProperty(srcExp, button, c => c.Text, dataSource);
            }
            else if (typeof(ComboBox).IsAssignableFrom(type))
            {
                var comboBox = control as ComboBox;

                if (dataSource != null)
                {
                    comboBox.DataSource = dataSource;
                }

                var srcProp = Src.GetExpressionValue<TSrc, object>(srcExp);

                comboBox.SelectedIndexChanged += (s, e) =>
                {
                    if (comboBox.SelectedIndex >= 0)
                    {
                        var value = comboBox.Items[comboBox.SelectedIndex];
                        var prop = Src.GetType().GetProperty(srcProp.Name);

                        prop.SetValue(Src, value);
                    }
                };


                //BindProperty(srcExp, comboBox, c => c.SelectedItem, dataSource);
            }
            else if (ShReflectionUtils.HasProperty(control, "Text"))
            {
                BindProperty(srcExp, control, "Text", dataSource, true);
            }
            else
            {
                throw new Exception(
                    string.Format("Autobinding isn't supported for control type:[{0}]", type));
            }

            return this;
        }

        public ShBindService<TSrc> BindProperty<TControl>(Expression<Func<TSrc, object>> srcExp,
            TControl control,
            Expression<Func<TControl, object>> dstExp)
            where TControl : Control
        {
            return BindProperty(srcExp, control, dstExp, null);
        }

        public ShBindService<TSrc> BindProperty<TControl>(Expression<Func<TSrc, object>> srcExp,
            TControl control,
            Expression<Func<TControl, object>> dstExp,
            object dataSource)
            where TControl : Control
        {
            return BindProperty(srcExp, control, dstExp, dataSource, true);
        }

        public ShBindService<TSrc> BindProperty<TControl>(Expression<Func<TSrc, object>> srcExp,
            TControl control,
            Expression<Func<TControl, object>> dstExp,
            object dataSource,
            bool clearExisting)
              where TControl : Control
        {
            var srcProp = Src.GetExpressionValue<TSrc, object>(srcExp);
            var dstProp = control.GetExpressionValue<TControl, object>(dstExp);

            BindProperty(control, clearExisting, srcProp.Name, dstProp.Name);

            return this;
        }

        public ShBindService<TSrc> BindProperty<TControl>(Expression<Func<TSrc, object>> srcExp,
            TControl control,
            string dstPropName,
            object dataSource,
            bool clearExisting)
              where TControl : Control
        {
            var srcProp = Src.GetExpressionValue<TSrc, object>(srcExp);
            control.DataBindings.Add(dstPropName, Src, srcProp.Name, false, DataSourceUpdateMode.OnPropertyChanged);

            return this;
        }

        public ShBindService<TSrc> BindProperty<TControl>(TControl control, bool clearExisting,
            string srcPropName, string dstPropName)
            where TControl : Control
        {
            if (clearExisting)
                control.DataBindings.Clear();

            control.DataBindings.Add(dstPropName, Src, srcPropName, false, DataSourceUpdateMode.OnPropertyChanged);

            return this;
        }





        public void BindEvent<TControl>(TControl control, Action<TControl> action)
        {
            action(control);
        }
        #endregion
    }
}
