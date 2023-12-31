﻿namespace VideoRentalShop_Blazor_Server_.Services
{
    public class CartService
    {
        public event Action OnChange;

        private bool _showCart = false;
        private bool _startAnimation = false;

        public bool ShowCart
        {
            get => _showCart;
            set
            {
                _showCart = value;
                NotifyStateChanged();
            }
        }

        public bool StartAnimation
        {
            get => _startAnimation;
            set
            {
                _startAnimation = value;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
