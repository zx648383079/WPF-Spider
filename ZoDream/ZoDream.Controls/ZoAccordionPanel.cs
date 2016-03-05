using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZoDream.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZoDream.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZoDream.Controls;assembly=ZoDream.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:ZoAccordionPanel/>
    ///
    /// </summary>
    public class ZoAccordionPanel : Panel
    {
        static ZoAccordionPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZoAccordionPanel), new FrameworkPropertyMetadata(typeof(ZoAccordionPanel)));
        }

        /// <summary>
        /// Gets/Sets Which child to fill the rest space
        /// </summary>
        public UIElement ChildToFill
        {
            get { return (UIElement)GetValue(ChildToFillProperty); }
            set { SetValue(ChildToFillProperty, value); }
        }

        public static readonly DependencyProperty ChildToFillProperty = DependencyProperty.Register(
            "ChildToFill", typeof(UIElement), typeof(ZoAccordionPanel),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            UIElementCollection internalChildren = base.InternalChildren;

            int count = internalChildren.Count;

            // If ChildToFill is not specified, set it to the last child
            int childToFillIndex = ChildToFill == null ? count - 1 : internalChildren.IndexOf(ChildToFill);

            double y = 0.0;

            Rect rectForFill = new Rect(0, 0, arrangeSize.Width, arrangeSize.Height);

            if (childToFillIndex != -1)
            {
                // Arrange elements before ChildToFill in sequence
                for (int i = 0; i < childToFillIndex + 1; i++)
                {
                    UIElement element = internalChildren[i];
                    if (element != null)
                    {
                        Size desiredSize = element.DesiredSize;
                        Rect finalRect = new Rect(0, y, Math.Max(0.0, arrangeSize.Width), Math.Max(0.0, arrangeSize.Height - y));
                        if (i < childToFillIndex)
                        {
                            finalRect.Height = desiredSize.Height;
                            y += desiredSize.Height;
                            element.Arrange(finalRect);
                        }
                        else
                        {
                            // The rect for the rest of children
                            rectForFill = finalRect;
                        }
                    }
                }

                y = 0.0;

                // Arrange the elements after ChildToFill in negative sequence（Including ChildToFill）
                for (int i = count - 1; i > childToFillIndex; i--)
                {
                    UIElement element = internalChildren[i];
                    if (element != null)
                    {
                        Size desiredSize = element.DesiredSize;
                        Rect finalRect = new Rect(0, arrangeSize.Height - y - desiredSize.Height, Math.Max(0.0, arrangeSize.Width), Math.Max(0.0, desiredSize.Height));

                        element.Arrange(finalRect);
                        y += desiredSize.Height;
                    }
                }
                rectForFill.Height -= y;
                InternalChildren[childToFillIndex].Arrange(rectForFill);
            }

            return arrangeSize;
        }

        // Modified from DockPanel
        protected override Size MeasureOverride(Size constraint)
        {
            UIElementCollection internalChildren = base.InternalChildren;
            double num = 0.0;
            double num2 = 0.0;
            double num3 = 0.0;
            double num4 = 0.0;
            int index = 0;
            int count = internalChildren.Count;

            while (index < count)
            {
                UIElement element = internalChildren[index];
                if (element != null)
                {
                    Size availableSize = new Size(Math.Max((double)0.0, (double)(constraint.Width - num3)), Math.Max((double)0.0, (double)(constraint.Height - num4)));
                    element.Measure(availableSize);
                    Size desiredSize = element.DesiredSize;

                    num = Math.Max(num, num3 + desiredSize.Width);
                    num4 += desiredSize.Height;

                }
                index++;
            }
            num = Math.Max(num, num3);
            return new Size(num, Math.Max(num2, num4));
        }
    }
}
