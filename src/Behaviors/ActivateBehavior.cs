﻿using System;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace tabbR.Behaviors
{
    public class ActivateBehavior : Behavior<Window>
    {
        private bool _isActivated;

        public static readonly DependencyProperty ActivatedProperty =
            DependencyProperty.Register(
                "Activated",
                typeof(bool),
                typeof(ActivateBehavior),
                new PropertyMetadata(OnActivatedChanged)
            );

        public bool Activated
        {
            get => (bool)GetValue(ActivatedProperty);
            set => SetValue(ActivatedProperty, value);
        }

        private static void OnActivatedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (ActivateBehavior)dependencyObject;
            if (!behavior.Activated || behavior._isActivated)
                return;

            if (behavior.AssociatedObject.WindowState == WindowState.Minimized)
                behavior.AssociatedObject.WindowState = WindowState.Normal;
            behavior.AssociatedObject.Activate();
        }

        protected override void OnAttached()
        {
            AssociatedObject.Activated += OnActivated;
            AssociatedObject.Deactivated += OnDeactivated;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Activated -= OnActivated;
            AssociatedObject.Deactivated -= OnDeactivated;
        }

        private void OnActivated(object sender, EventArgs eventArgs)
        {
            _isActivated = true;
            Activated = true;
        }

        private void OnDeactivated(object sender, EventArgs eventArgs)
        {
            _isActivated = false;
            Activated = false;
        }
    }
}
