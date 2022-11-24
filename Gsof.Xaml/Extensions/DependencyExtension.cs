using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Gsof.Extensions;
using Microsoft.Xaml.Behaviors;

namespace Gsof.Xaml.Extensions
{
    public static class DependencyExtension
    {
        /// <summary>
        /// 获取类型为 T 的第一父节点
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static T ParentOfType<T>(this DependencyObject element, Func<T, bool> p_func = null) where T : DependencyObject
        {
            if (element == null) return null;
            DependencyObject parent = VisualTreeHelper.GetParent(element);
            while (parent != null && (!(parent is T) || (p_func != null && !p_func(parent as T))))
            {
                DependencyObject newVisualParent = VisualTreeHelper.GetParent(parent);
                if (newVisualParent != null)
                {
                    parent = newVisualParent;
                }
                else
                {
                    // try to get the logical parent ( e.g. if in Popup)
                    if (parent is FrameworkElement)
                    {
                        parent = (parent as FrameworkElement).Parent;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return parent as T;
        }

        /// <summary>
        /// 以类型获取第一子节点
        /// </summary>
        /// <typeparam name="T"/><peparam/>
        /// <param name="p_element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static T ChildOfType<T>(this DependencyObject p_element, Func<T, bool> p_func = null) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
            {
                var child = VisualTreeHelper.GetChild(p_element, i);
                if (child == null)
                {
                    continue;
                }

                var t = child as T;

                if (t != null && (p_func == null || p_func(t)))
                {
                    return (T)child;
                }

                var grandChild = child.ChildOfType(p_func);
                if (grandChild != null)
                    return grandChild;
            }

            return null;
        }

        /// <summary>
        /// 获取当前控件树下所在 T 类型节点 （深度遍历）
        /// </summary>
        /// <typeparam name="T"/><peparam/>
        /// <param name="p_element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static IEnumerable<T> ChildrenOfType<T>(this DependencyObject p_element, Func<T, bool> p_func = null) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(p_element); i++)
            {
                var child = VisualTreeHelper.GetChild(p_element, i);
                if (child == null)
                {
                    continue;
                }

                if (child is T)
                {
                    var t = (T)child;
                    if (p_func != null && !p_func(t))
                    {
                        continue;
                    }

                    yield return t;
                }
                else
                {
                    foreach (var c in child.ChildrenOfType(p_func))
                    {
                        yield return c;
                    }
                }
            }
        }

        /// <summary>
        /// 移除行为
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_dependencyObject"></param>
        private static BehaviorCollection RemoveBehaviorInternal<T>(this DependencyObject p_dependencyObject) where T : Behavior, new()
        {
            if (p_dependencyObject == null)
            {
                return null;
            }

            BehaviorCollection itemBehaviors = Interaction.GetBehaviors(p_dependencyObject);
            foreach (var behavior in itemBehaviors)
            {
                if (!(behavior is T))
                {
                    continue;
                }

                itemBehaviors.Remove(behavior);
            }

            return itemBehaviors;
        }

        /// <summary>
        /// 行为应用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_dependencyObject"></param>
        /// <param name="p_onlyRemove">是否只移除</param>
        public static void ApplyBehavior<T>(this DependencyObject p_dependencyObject, bool p_onlyRemove = false) where T : Behavior, new()
        {
            var deo = p_dependencyObject;
            if (deo == null)
            {
                return;
            }

            var itemBehaviors = deo.RemoveBehaviorInternal<T>();
            if (itemBehaviors == null)
            {
                return;
            }

            if (!p_onlyRemove)
            {
                itemBehaviors.Add(new T());
            }
        }

        /// <summary>
        /// 移除行为
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_dependencyObject"></param>
        public static void RemoveBehavior<T>(this DependencyObject p_dependencyObject) where T : Behavior, new()
        {
            p_dependencyObject.RemoveBehaviorInternal<T>();
        }

        /// <summary>
        /// 获取当前窗口
        /// </summary>
        /// <param name="p_dependencyObject"></param>
        /// <returns></returns>
        public static Window GetWindow(this DependencyObject p_dependencyObject)
        {
            if (p_dependencyObject == null)
            {
                throw new ArgumentNullException(nameof(p_dependencyObject));
            }

            return Window.GetWindow(p_dependencyObject);
        }

        /// <summary>
        /// 获取所有绑定
        /// </summary>
        /// <param name="p_element"></param>
        /// <returns></returns>
        public static IEnumerable<BindingExpression> GetBindingExpressions(this DependencyObject p_element)
        {
            var element = p_element;
            if (element == null)
            {
                yield break;
            }

            var dps = element.GetStaticFields(true).Where(x => x.FieldType == typeof(DependencyProperty));

            foreach (var dp in dps)
            {
                BindingExpression be = BindingOperations.GetBindingExpression(element, (DependencyProperty)dp.GetValue(element)!);
                if (be == null)
                {
                    continue;
                }
                yield return be;
            }
        }

        /// <summary>
        /// 获取类型为T，当前控件逻辑树下的子对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_element"></param>
        /// <param name="p_func"></param>
        /// <returns></returns>
        public static IEnumerable<T> LogicalChildrenOfType<T>(this DependencyObject p_element, Func<T, bool>? p_func = null)
        {
            var element = p_element;
            if (element == null)
            {
                yield break;
            }

            var children = LogicalTreeHelper.GetChildren(element);

            foreach (var child in children)
            {
                var dpObj = child as DependencyObject;
                if (dpObj is T)
                {
                    var t = (T)child;
                    if (p_func != null && !p_func(t))
                    {
                        continue;
                    }

                    yield return (T)child;
                }

                if (dpObj == null)
                {
                    continue;
                }

                foreach (var tmp in dpObj.LogicalChildrenOfType<T>())
                {
                    yield return tmp;
                }
            }
        }
    }
}
