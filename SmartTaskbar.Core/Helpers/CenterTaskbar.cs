//using System;
//using System.Collections.Generic;
//using System.Windows.Automation;

//namespace SmartTaskbar.Core.Helpers
//{
//    internal static class CenterTaskbar
//    {
//        private const string MsTaskListWClass = "MSTaskListWClass";

//        private static readonly Dictionary<AutomationElement, double> Lasts =
//            new Dictionary<AutomationElement, double>();

//        private static readonly Dictionary<AutomationElement, AutomationElement> Children =
//            new Dictionary<AutomationElement, AutomationElement>();

//        static CenterTaskbar()
//        {
//        }

//        public static void Start()
//        {
//            var condition =
//                new OrCondition(new PropertyCondition(AutomationElement.ClassNameProperty, Constant.MainTaskbar),
//                    new PropertyCondition(AutomationElement.ClassNameProperty, Constant.SubTaskbar));

//            var cacheRequest = new CacheRequest();
//            cacheRequest.Add(AutomationElement.NameProperty);
//            cacheRequest.Add(AutomationElement.BoundingRectangleProperty);

//            using (cacheRequest.Activate())
//            {
//                var lists = AutomationElement.RootElement.FindAll(TreeScope.Children, condition);
//                if (lists == null)
//                {
//                    return;
//                }

//                Lasts.Clear();
//                foreach (AutomationElement trayWnd in lists)
//                {
//                    var tasklist = trayWnd.FindFirst(TreeScope.Descendants,
//                        new PropertyCondition(AutomationElement.ClassNameProperty, MsTaskListWClass));
//                    if (tasklist == null)
//                    {
//                        continue;
//                    }

//                    Automation.AddAutomationPropertyChangedEventHandler(tasklist, TreeScope.Element,
//                        OnUIAutomationEvent, AutomationElement.BoundingRectangleProperty);

//                    Children.Add(trayWnd, tasklist);
//                }
//            }

//        }


//        private static void OnUIAutomationEvent(object src, AutomationEventArgs e)
//        {
//            AutomationElement tasklist = Children[trayWnd];
//            AutomationElement last = TreeWalker.ControlViewWalker.GetLastChild(tasklist);
//            if (last == null)
//            {
//                return;
//            }

//            Rect trayBounds = trayWnd.Cached.BoundingRectangle;
//            bool horizontal = (trayBounds.Width > trayBounds.Height);

//            double lastChildPos =
//                (horizontal
//                    ? last.Current.BoundingRectangle.Left
//                    : last.Current.BoundingRectangle
//                        .Top); // Use the left/top bounds because there is an empty element as the last child with a nonzero width

//            if ((Lasts.ContainsKey(trayWnd) && lastChildPos == Lasts[trayWnd]))
//            {
//                return false;
//            }
//            else
//            {
//                Lasts[trayWnd] = lastChildPos;

//                AutomationElement first = TreeWalker.ControlViewWalker.GetFirstChild(tasklist);
//                if (first == null)
//                {
//                    return true;
//                }

//                double scale = horizontal
//                    ? (last.Current.BoundingRectangle.Height / trayBounds.Height)
//                    : (last.Current.BoundingRectangle.Width / trayBounds.Width);
//                double size =
//                    (lastChildPos - (horizontal
//                        ? first.Current.BoundingRectangle.Left
//                        : first.Current.BoundingRectangle.Top)) / scale;
//                if (size < 0)
//                {
//                    return true;
//                }

//                AutomationElement tasklistcontainer = TreeWalker.ControlViewWalker.GetParent(tasklist);
//                if (tasklistcontainer == null)
//                {
//                    return true;
//                }

//                Rect tasklistBounds = tasklist.Current.BoundingRectangle;

//                double barSize = horizontal
//                    ? trayWnd.Cached.BoundingRectangle.Width
//                    : trayWnd.Cached.BoundingRectangle.Height;
//                double targetPos = Math.Round((barSize - size) / 2) + (horizontal ? trayBounds.X : trayBounds.Y);

//                double delta = Math.Abs(targetPos - (horizontal ? tasklistBounds.X : tasklistBounds.Y));
//                // Previous bounds check
//                if (delta <= 1)
//                {
//                    // Already positioned within margin of error, avoid the unneeded MoveWindow call
//                    return false;
//                }

//                // Right bounds check
//                int rightBounds = sideBoundary(false, horizontal, tasklist);
//                if ((targetPos + size) > (rightBounds))
//                {
//                    // Shift off center when the bar is too big
//                    double extra = (targetPos + size) - rightBounds;
//                    targetPos -= extra;
//                }

//                // Left bounds check
//                int leftBounds = sideBoundary(true, horizontal, tasklist);
//                if (targetPos <= (leftBounds))
//                {
//                    // Prevent X position ending up beyond the normal left aligned position
//                    Reset(trayWnd);
//                    return true;
//                }

//                IntPtr tasklistPtr = (IntPtr) tasklist.Current.NativeWindowHandle;

//                if (horizontal)
//                {
//                    SetWindowPos(tasklistPtr, IntPtr.Zero, (relativePos(targetPos, horizontal, tasklist)), 0, 0, 0,
//                        SWP_NOZORDER | SWP_NOSIZE | SWP_ASYNCWINDOWPOS);
//                }
//                else
//                {
//                    SetWindowPos(tasklistPtr, IntPtr.Zero, 0, (relativePos(targetPos, horizontal, tasklist)), 0, 0,
//                        SWP_NOZORDER | SWP_NOSIZE | SWP_ASYNCWINDOWPOS);
//                }

//                Lasts[trayWnd] =
//                    (horizontal ? last.Current.BoundingRectangle.Left : last.Current.BoundingRectangle.Top);


//            }

//        }

//        private static int RelativePos(double x, bool horizontal, AutomationElement element)
//        {
//            int adjustment = SideBoundary(true, horizontal, element);

//            double newPos = x - adjustment;

//            if (newPos < 0)
//            {
//                newPos = 0;
//            }

//            return (int)newPos;
//        }

//        private static int SideBoundary(bool left, bool horizontal, AutomationElement element)
//        {
//            double adjustment = 0;
//            AutomationElement prevSibling = TreeWalker.ControlViewWalker.GetPreviousSibling(element);
//            AutomationElement nextSibling = TreeWalker.ControlViewWalker.GetNextSibling(element);
//            AutomationElement parent = TreeWalker.ControlViewWalker.GetParent(element);
//            if ((left && prevSibling != null))
//            {
//                adjustment = (horizontal ? prevSibling.Current.BoundingRectangle.Right : prevSibling.Current.BoundingRectangle.Bottom);
//            }
//            else if (!left && nextSibling != null)
//            {
//                adjustment = (horizontal ? nextSibling.Current.BoundingRectangle.Left : nextSibling.Current.BoundingRectangle.Top);
//            }
//            else if (parent != null)
//            {
//                if (horizontal)
//                {
//                    adjustment = left ? parent.Current.BoundingRectangle.Left : parent.Current.BoundingRectangle.Right;
//                }
//                else
//                {
//                    adjustment = left ? parent.Current.BoundingRectangle.Top : parent.Current.BoundingRectangle.Bottom;
//                }

//            }
//            return (int)adjustment;
//        }

//        public static void ResetTaskbar()
//        {

//        }
//    }
//}
