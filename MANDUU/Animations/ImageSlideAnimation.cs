using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace MANDUU.Animations
{
    public static class ImageSlideAnimation
    {
        private const uint AnimationSpeed = 250; // milliseconds

        public static async Task SlideToNextAsync(Image imageView, Action onImageChanged)
        {
            if (imageView == null || onImageChanged == null)
                return;

            // Slide out left + fade
            await Task.WhenAll(
                imageView.TranslateTo(-imageView.Width, 0, AnimationSpeed, Easing.CubicIn),
                imageView.FadeTo(0, AnimationSpeed, Easing.CubicIn)
            );

            // Change the image
            onImageChanged();

            // Position image off-screen right
            imageView.TranslationX = imageView.Width;
            imageView.Opacity = 0;

            // Slide in from right + fade in
            await Task.WhenAll(
                imageView.TranslateTo(0, 0, AnimationSpeed, Easing.CubicOut),
                imageView.FadeTo(1, AnimationSpeed, Easing.CubicOut)
            );
        }

        public static async Task SlideToPreviousAsync(Image imageView, Action onImageChanged)
        {
            if (imageView == null || onImageChanged == null)
                return;

            // Slide out right + fade
            await Task.WhenAll(
                imageView.TranslateTo(imageView.Width, 0, AnimationSpeed, Easing.CubicIn),
                imageView.FadeTo(0, AnimationSpeed, Easing.CubicIn)
            );

            // Change the image
            onImageChanged();

            // Position image off-screen left
            imageView.TranslationX = -imageView.Width;
            imageView.Opacity = 0;

            // Slide in from left + fade in
            await Task.WhenAll(
                imageView.TranslateTo(0, 0, AnimationSpeed, Easing.CubicOut),
                imageView.FadeTo(1, AnimationSpeed, Easing.CubicOut)
            );
        }
    }
}
