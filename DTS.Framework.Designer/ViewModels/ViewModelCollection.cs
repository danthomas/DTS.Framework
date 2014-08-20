using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTS.Framework.Designer.ViewModels
{
    public class ViewModelCollection<T> : INotifyCollectionChanged, INotifyPropertyChanged, IEnumerable<T>
    //where T : ItemViewModel
    {
        protected List<T> Items;
        private Func<T, bool> _filter;

        public ViewModelCollection()
        {
            Items = new List<T>();
        }

        public T Add(T item)
        {
            Items.Add(item);

            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }

            return item;
        }

        public void Remove(T item)
        {
            Items.Remove(item);

            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
            }
        }

        public Func<T, bool> Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;

                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new ViewModelCollectionEnumerator<T>(this);
        }

        public IEnumerator GetEnumerator()
        {
            return new ViewModelCollectionEnumerator<T>(this);
        }

        public class ViewModelCollectionEnumerator<T> : IEnumerator<T>
        //  where T : ItemViewModel
        {
            //private readonly ViewModelCollection<T> _viewModelCollection;
            private int _index;
            private List<T> _items;

            public ViewModelCollectionEnumerator(ViewModelCollection<T> viewModelCollection)
            {
                //_viewModelCollection = viewModelCollection;

                if (viewModelCollection.Filter == null)
                {
                    _items = viewModelCollection.Items;
                }
                else
                {
                    _items = viewModelCollection.Items.Where(viewModelCollection.Filter).ToList();
                }

                Reset();
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _items.Count;
            }

            public void Reset()
            {
                _index = -1;
            }

            T IEnumerator<T>.Current
            {
                get { return _items[_index]; }
            }

            public object Current
            {
                get { return _items[_index]; }
            }

            public void Dispose()
            {
            }
        }
    }
}
