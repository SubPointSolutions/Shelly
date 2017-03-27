using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SubPointSolutions.Shelly.Core.Exceptions;
using SubPointSolutions.Shelly.Desktop.Attributes;
using SubPointSolutions.Shelly.Desktop.Controls;
using SubPointSolutions.Shelly.Desktop.Interfaces;

namespace SubPointSolutions.Shelly.Desktop.Wf.Controls
{
    public partial class WfSettingsEditorControl : ShUserControlBase, ISettingsEditorControl
    {
        public WfSettingsEditorControl()
        {
            InitializeComponent();

            Init();
        }

        private void Init()
        {
            this.Text = "Options";

            pProperties.Padding = new Padding(5, 0, 5, 0);
            tvOptions.AfterSelect += TvOptions_AfterSelect;
        }

        private void TvOptions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedNode = tvOptions.SelectedNode;

            if (selectedNode.Level == 0)
                return;

            if (selectedNode != null && selectedNode.Tag is CategoryItem)
            {
                var categoryItem = (selectedNode.Tag as CategoryItem);

                var obj = categoryItem.Value;

                if (string.IsNullOrEmpty(categoryItem.CustomEditorType))
                {
                    pProperties.SuspendLayout();

                    pProperties.Controls.Clear();

                    var propGrid = new PropertyGrid();

                    propGrid.SelectedObject = obj;
                    propGrid.Dock = DockStyle.Fill;

                    pProperties.Controls.Add(propGrid);

                    pProperties.ResumeLayout();
                }
                else
                {
                    var editorType = Type.GetType(categoryItem.CustomEditorType);

                    if (editorType == null)
                    {
                        throw new ShAppException(
                            string.Format("Cannot find editor type:[{0}]", categoryItem.CustomEditorType));
                    }

                    var control = Activator.CreateInstance(editorType) as UserControl;

                    if (control == null)
                    {
                        throw new ShAppException(
                        string.Format("Cannot cast to UserControl:[{0}]", categoryItem.CustomEditorType));
                    }

                    var entityEditor = control as IEntityEditorControl;

                    if (entityEditor == null)
                    {
                        throw new ShAppException(
                         string.Format("Cannot cast to IEntityEditorControl:[{0}]", categoryItem.CustomEditorType));
                    }

                    pProperties.SuspendLayout();

                    entityEditor.SetEntity(obj);

                    pProperties.Controls.Clear();
                    control.Dock = DockStyle.Fill;
                    pProperties.Controls.Add(control);

                    pProperties.ResumeLayout();
                }
            }
        }

        public void SetOptionsObject(object settings)
        {
            var categories = GetCategories(settings);

            tvOptions.Nodes.Clear();

            foreach (var category in categories.OrderBy(t => t.Order))
            {

                var node = new TreeNode
                {
                    Text = category.Title,
                    Tag = category
                };

                foreach (var sub in category.Items.OrderBy(t => t.Order))
                {
                    var subNode = new TreeNode
                    {
                        Text = sub.Title,
                        Tag = sub
                    };

                    node.Nodes.Add(subNode);
                }

                node.Expand();

                tvOptions.Nodes.Add(node);


            }
        }

        private List<CategoryItem> GetCategories(object settings)
        {
            var result = new List<CategoryItem>();

            var items = GetSettingsArray(settings);

            foreach (var item in items)
            {
                var title = item.GetType().ToString();

                var displayAttr = item.GetType()
                    .GetCustomAttributes(typeof(DisplayNameAttribute), true)
                    .FirstOrDefault() as DisplayNameAttribute;

                if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.DisplayName))
                    title = displayAttr.DisplayName;

                var titleParts = title.Split(',');

                title = titleParts.FirstOrDefault();
                var titleOrder = titleParts.Length > 1 ? int.Parse(titleParts[1]) : int.MaxValue;

                var category = new CategoryItem
                {
                    Title = title,
                    Value = item,
                    Order = titleOrder
                };

                result.Add(category);

                // props
                var props = item.GetType().GetProperties();

                foreach (var prop in props)
                {
                    var propValue = prop.GetValue(item, null);

                    var subSisplayAttr = prop.GetCustomAttributes(typeof(DisplayNameAttribute), true)
                                          .FirstOrDefault() as DisplayNameAttribute;

                    var customEditoAttr = prop.GetCustomAttributes(typeof(ShEntityEditorControlAttribute), true)
                                          .FirstOrDefault() as ShEntityEditorControlAttribute;



                    if (subSisplayAttr != null && !string.IsNullOrEmpty(subSisplayAttr.DisplayName))
                    {
                        var parts = subSisplayAttr.DisplayName.Split(',');

                        var subTitle = parts.FirstOrDefault();
                        var order = parts.Length > 1 ? int.Parse(parts[1]) : int.MaxValue;

                        var subItem = new CategoryItem
                        {
                            Title = subTitle,
                            Value = propValue,
                            Order = order
                        };

                        if (customEditoAttr != null)
                        {
                            subItem.CustomEditorType = customEditoAttr.AssemblyQualifiedName;
                        }

                        category.Items.Add(subItem);
                    }
                }
            }

            return result;
        }

        private List<object> GetSettingsArray(object settings)
        {
            var items = new List<object>();

            if (settings is IEnumerable)
                items.AddRange(((IEnumerable)settings).Cast<object>().ToList());
            else
                items.Add(settings);

            return items;
        }

        public event EventHandler OnOk;
        public event EventHandler OnCancel;

        private class CategoryItem
        {
            public CategoryItem()
            {
                Items = new List<CategoryItem>();
            }

            public object Value { get; set; }
            public string Title { get; set; }

            public override string ToString()
            {
                return Title;
            }

            public List<CategoryItem> Items { get; set; }
            public int Order { get; set; }

            public string CustomEditorType { get; set; }
        }
    }
}
