using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using System;
using System.Windows.Input;

namespace Nodify
{
    public class ResizablePanel : ContentControl
    {
        internal static readonly ResizeDirections BoxedResizeDirection = ResizeDirections.All;

        public static readonly StyledProperty<ResizeDirections> DirectionsProperty = AvaloniaProperty.Register<ResizablePanel, ResizeDirections>(nameof(Directions), ResizeDirections.All);

        public static readonly StyledProperty<ICommand?> ResizeStartedCommandProperty = AvaloniaProperty.Register<ResizablePanel, ICommand?>(nameof(ResizeStartedCommand));

        public static readonly StyledProperty<ICommand?> ResizeCompletedCommandProperty = AvaloniaProperty.Register<ResizablePanel, ICommand?>(nameof(ResizeCompletedCommand));

        public ResizeDirections Directions
        {
            get => GetValue(DirectionsProperty);
            set => SetValue(DirectionsProperty, value);
        }

        public ICommand? ResizeStartedCommand
        {
            get => GetValue(ResizeStartedCommandProperty);
            set => SetValue(ResizeStartedCommandProperty, value);
        }

        public ICommand? ResizeCompletedCommand
        {
            get => GetValue(ResizeCompletedCommandProperty);
            set => SetValue(ResizeCompletedCommandProperty, value);
        }

        public ResizablePanel()
        {
            AddHandler(Thumb.DragDeltaEvent, OnResize);
            AddHandler(Thumb.DragStartedEvent, OnDragStarted);
            AddHandler(Thumb.DragCompletedEvent, OnDragCompleted);
        }

        private void OnDragStarted(object? sender, VectorEventArgs e)
        {
            if (ResizeStartedCommand?.CanExecute(null) ?? false)
            {
                ResizeStartedCommand.Execute(null);
            }
        }

        private void OnDragCompleted(object? sender, VectorEventArgs e)
        {
            if (ResizeCompletedCommand?.CanExecute(null) ?? false)
            {
                ResizeCompletedCommand.Execute(null);
            }
        }

        private void OnResize(object? sender, VectorEventArgs e)
        {
            if (e.Source is Resizer resizer)
            {
                double resizeX = 0;
                double resizeY = 0;

                double moveX = 0;
                double moveY = 0;

                if (resizer.Direction.HasFlag(ResizeDirections.Top))
                {
                    moveY = resizeY = ResizeTop(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.Bottom))
                {
                    resizeY = ResizeBottom(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.Left))
                {
                    moveX = resizeX = ResizeLeft(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.Right))
                {
                    resizeX = ResizeRight(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.TopLeft))
                {
                    moveY = resizeY = ResizeTop(e);
                    moveX = resizeX = ResizeLeft(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.TopRight))
                {
                    moveY = resizeY = ResizeTop(e);
                    resizeX = ResizeRight(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.BottomLeft))
                {
                    resizeY = ResizeBottom(e);
                    moveX = resizeX = ResizeLeft(e);
                }

                if (resizer.Direction.HasFlag(ResizeDirections.BottomRight))
                {
                    resizeY = ResizeBottom(e);
                    resizeX = ResizeRight(e);
                }

                OnProcessDelta(ref resizeX, ref resizeY);
                OnProcessDelta(ref moveX, ref moveY);

                OnMove(moveX, moveY);

                Width -= resizeX;
                Height -= resizeY;

                e.Handled = true;
            }
        }

        private double ResizeBottom(VectorEventArgs e)
        {
            return Math.Min(-e.Vector.Y, Bounds.Height - MinHeight);
        }

        private double ResizeTop(VectorEventArgs e)
        {
            return Math.Min(e.Vector.Y, Bounds.Height - MinHeight);
        }

        private double ResizeRight(VectorEventArgs e)
        {
            return Math.Min(-e.Vector.X, Bounds.Width - MinWidth);
        }

        private double ResizeLeft(VectorEventArgs e)
        {
            return Math.Min(e.Vector.X, Bounds.Width - MinWidth);
        }

        protected virtual void OnMove(double x, double y)
        {
            Canvas.SetTop(this, Canvas.GetTop(this) + y);
            Canvas.SetLeft(this, Canvas.GetLeft(this) + x);
        }

        protected virtual void OnProcessDelta(ref double dx, ref double dy)
        {
        }
    }

    public class Resizer : Thumb
    {
        public static readonly StyledProperty<ResizeDirections> DirectionProperty = AvaloniaProperty.Register<Resizer, ResizeDirections>(nameof(Direction), ResizeDirections.All);

        public ResizeDirections Direction
        {
            get => GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }
    }

    [Flags]
    public enum ResizeDirections
    {
        Top = 1,
        Left = 2,
        Bottom = 4,
        Right = 8,
        TopLeft = 16,
        TopRight = 32,
        BottomLeft = 64,
        BottomRight = 128,
        Edges = Top | Left | Bottom | Right,
        Corners = TopLeft | TopRight | BottomLeft | BottomRight,
        All = Edges | Corners
    }
} 