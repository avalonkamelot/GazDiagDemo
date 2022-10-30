using HelixToolkit.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media.Media3D;
using System.Windows;
using TubeDemo.Data.Models;

namespace TubeDemo.Controls.View3D
{
    public class ItemsVisual3D : ModelVisual3D
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(ItemsVisual3D),
            new PropertyMetadata(null, (s, e) => ((ItemsVisual3D)s).ItemsSourceChanged(e)));

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set => this.SetValue(ItemsSourceProperty, value);
        }

        // Using a DependencyProperty as the backing store for RefreshChildrenOnChange.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RefreshChildrenOnChangeProperty = DependencyProperty.Register(
            "RefreshChildrenOnChange", typeof(bool), typeof(ItemsVisual3D), new PropertyMetadata(true));

        public bool RefreshChildrenOnChange
        {
            get => (bool)GetValue(RefreshChildrenOnChangeProperty);
            set => SetValue(RefreshChildrenOnChangeProperty, value);
        }

        public Dictionary<object, Visual3D> Visuals { get; protected set; } = new Dictionary<object, Visual3D>();

        private void ItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldObservableCollection = e.OldValue as INotifyCollectionChanged;
            if (oldObservableCollection != null)
            {
                oldObservableCollection.CollectionChanged -= CollectionChanged;
            }

            var observableCollection = e.NewValue as INotifyCollectionChanged;
            if (observableCollection != null)
            {
                observableCollection.CollectionChanged += this.CollectionChanged;
            }

            if (this.ItemsSource != null)
            {
                AddItems(this.ItemsSource);
            }

            if (RefreshChildrenOnChange)
                RefreshChildren();
        }

        /// <summary>
        /// Re-attaches the instance to the viewport resulting in a refresh.
        /// </summary>
        public void RefreshChildren()
        {
            var viewPort = Visual3DHelper.GetViewport3D(this);
            var index = viewPort.Children.IndexOf(this);
            viewPort.Children.Remove(this);
            viewPort.Children.Insert(index, this);
        }

        private void AddItems(IEnumerable items)
        {
            if (items != null && items.Cast<object>().Any())
            {
                foreach (var item in items)
                    AddItem(item);
            }
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    RemoveItems(e.OldItems);
                    AddItems(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.Children.Clear();
                    this.Visuals.Clear();

                    this.AddItems(ItemsSource);
                    break;

                default:
                    break;
            }

            if (RefreshChildrenOnChange)
                RefreshChildren();
        }

        private void AddItem(object item)
        {
            var visual = CreateVisualFromModel(item);
            if (visual != null)
            {
                // Cannot set DataContext, set bindings manually
                // http://stackoverflow.com/questions/7725313/how-can-i-use-databinding-for-3d-elements-like-visual3d-or-uielement3d
                this.Children.Add(visual);
                //(visual as TubeSegmentVisual).UpdateCuttingPlanes(item as TubeSegmentInfo);

                this.Visuals[item] = visual;
            }
            else
            {
                throw new InvalidOperationException("Cannot create a Model3D from ItemTemplate.");
            }
        }
        private void RemoveItems(IEnumerable items)
        {
            if (items == null)
                return;

            foreach (var rem in items)
            {
                if (Visuals.ContainsKey(rem))
                {
                    if (Visuals[rem] != null)
                    {
                        Children.Remove(Visuals[rem]);
                    }
                }
            }
        }

        private Visual3D CreateVisualFromModel(object item)
        {
            return new TubeSegmentVisual(item as TubeSegmentInfo);
        }
    }
}
