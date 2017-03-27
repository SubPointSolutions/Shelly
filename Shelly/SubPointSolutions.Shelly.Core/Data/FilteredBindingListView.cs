using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubPointSolutions.Shelly.Core.Data
{
    public class FilteredBindingListView<T> : BindingList<T>
    {
        #region constructors

        public FilteredBindingListView(BindingList<T> source)
            : this(source, null)
        {
        }

        public FilteredBindingListView(BindingList<T> source, Func<T, bool> filter)
        {
            FilterFunc = filter;
            _source = source;

            InitData(_source);
            InitEvents();
        }

        #endregion

        #region properties

        private readonly BindingList<T> _source;
        public Func<T, bool> FilterFunc { get; set; }
        public Func<T, T, int> SortFunc { get; set; }

        #endregion

        #region methods

        private void InitEvents()
        {
            _source.ListChanged += _source_ListChanged;
        }

        private void InitData(IEnumerable<T> data)
        {
            foreach (var item in data)
                InternalAdd(item);
        }

        private void InternalRemove(T item)
        {
            if (FilterFunc != null)
            {
                if (FilterFunc(item))
                    Remove(item);
            }
            else
            {
                Remove(item);
            }
        }

        private void InternalAdd(T item)
        {
            if (FilterFunc != null)
            {
                if (FilterFunc(item))
                {
                    Add(item);
                }
            }
            else
            {
                Add(item);
            }


        }

        protected virtual void WithRaiseListChangedEvents(bool value, Action action)
        {
            var oldValue = RaiseListChangedEvents;

            try
            {
                RaiseListChangedEvents = value;
                action();
            }
            finally
            {
                RaiseListChangedEvents = oldValue;
            }
        }

        private IEnumerable<T> InternalSort()
        {
            IEnumerable<T> result = Enumerable.Empty<T>();

            if (SortFunc != null)
            {
                WithRaiseListChangedEvents(false, () =>
                {
                    var comparer = (IComparer<T>)Comparer<T>.Create((x, y) => SortFunc(x, y));
                    var data = this.ToList();

                    data.Sort(comparer);
                    result = data;
                });
            }

            return result;
        }

        void _source_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                var newItem = _source[e.NewIndex];
                InternalAdd(newItem);
            }

            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                var newItem = _source[e.NewIndex];
                InternalRemove(newItem);
            }
        }

        public void ApplyFiter()
        {
            WithRaiseListChangedEvents(false, () =>
            {
                Clear();
                InitData(_source);
            });
            ResetBindings();
        }

        public void ApplySort()
        {
            WithRaiseListChangedEvents(false, () =>
            {
                var sorteData = InternalSort();

                Clear();
                InitData(sorteData);
            });

            ResetBindings();
        }

        #endregion
    }
}