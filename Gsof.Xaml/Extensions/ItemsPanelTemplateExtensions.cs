using System.Windows;
using System.Windows.Controls;

namespace Gsof.Xaml.Extensions
{
    public static class ItemsPanelTemplateExtensions
    {
        public static ItemsPanelTemplate CreateItemsPanelTemplate<T>() where T : Panel
        {
            ItemsPanelTemplate template = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(T)));
            template.Seal();
            return template;
        }
    }
}
