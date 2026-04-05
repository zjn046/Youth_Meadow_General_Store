using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace YouthMeadowGeneralStore.Utilities
{
    public sealed class WrappedDisplayList : ListBox
    {
        private int _lastMeasuredWidth;

        public WrappedDisplayList()
        {
            BorderStyle = BorderStyle.FixedSingle;
            DrawMode = DrawMode.OwnerDrawVariable;
            HorizontalScrollbar = false;
            IntegralHeight = false;
            SelectionMode = SelectionMode.One;
            TabStop = true;
        }

        public void SetItems(IEnumerable<string> items)
        {
            var values = (items ?? Enumerable.Empty<string>()).ToArray();
            BeginUpdate();
            try
            {
                Items.Clear();
                Items.AddRange(values);
                SelectedIndex = Items.Count > 0 ? 0 : -1;
            }
            finally
            {
                EndUpdate();
            }
        }

        public void FocusFirstItem()
        {
            if (Items.Count > 0)
            {
                if (SelectedIndex < 0)
                {
                    SelectedIndex = 0;
                }

                if (IsHandleCreated && Visible)
                {
                    try
                    {
                        TopIndex = 0;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                    }
                }
            }

            if (CanFocus)
            {
                Focus();
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0 && e.Index < Items.Count)
            {
                var text = Items[e.Index]?.ToString() ?? string.Empty;
                var bounds = Rectangle.Inflate(e.Bounds, -6, -4);
                var foreColor = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                    ? SystemColors.HighlightText
                    : ForeColor;

                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    Font,
                    bounds,
                    foreColor,
                    TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);
            }

            e.DrawFocusRectangle();
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
            {
                e.ItemHeight = Font.Height + 10;
                return;
            }

            var text = Items[e.Index]?.ToString() ?? string.Empty;
            var availableWidth = Math.Max(ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 16, 120);
            var measured = TextRenderer.MeasureText(
                e.Graphics,
                text,
                Font,
                new Size(availableWidth, int.MaxValue),
                TextFormatFlags.Left | TextFormatFlags.Top | TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix);

            e.ItemHeight = Math.Max(measured.Height + 10, Font.Height + 10);
            e.ItemWidth = availableWidth;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (!IsHandleCreated || ClientSize.Width == _lastMeasuredWidth)
            {
                return;
            }

            _lastMeasuredWidth = ClientSize.Width;
            RemeasureItems();
        }

        private void RemeasureItems()
        {
            if (Items.Count == 0)
            {
                Invalidate();
                return;
            }

            var selectedIndex = SelectedIndex;
            var values = Items.Cast<object>().ToArray();

            BeginUpdate();
            try
            {
                Items.Clear();
                Items.AddRange(values);
                if (selectedIndex >= 0 && selectedIndex < Items.Count)
                {
                    SelectedIndex = selectedIndex;
                }
            }
            finally
            {
                EndUpdate();
            }

            Invalidate();
        }
    }
}
