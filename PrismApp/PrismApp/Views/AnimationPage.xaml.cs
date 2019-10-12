using Xamarin.Forms;

namespace PrismApp.Views
{
    public partial class AnimationPage : ContentPage
    {
        public AnimationPage()
        {
            InitializeComponent();
        }

        private void SpinButton_Clicked(object sender, System.EventArgs e)
        {
            var animation = new Animation(callback: d => SpinButton.Rotation = d,
                                  start: SpinButton.Rotation,
                                  end: SpinButton.Rotation + 360,
                                  easing: Easing.SpringOut);

            animation.Commit(SpinButton, "Loop", length: 800);
        }


        private void CompoundButton_Clicked(object sender, System.EventArgs e)
        {
            // Dirty hack you probably shouldn't use
            var width = Application.Current.MainPage.Width;

            var storyboard = new Animation();
            var rotation = new Animation(callback: d => CompoundButton.Rotation = d,
                                          start: CompoundButton.Rotation,
                                          end: CompoundButton.Rotation + 360,
                                          easing: Easing.SpringOut);


            var exitRight = new Animation(callback: d => CompoundButton.TranslationX = d,
                                           start: 0,
                                           end: width,
                                           easing: Easing.SpringIn);

            var enterLeft = new Animation(callback: d => CompoundButton.TranslationX = d,
                                           start: -width,
                                           end: 0,
                                           easing: Easing.BounceOut);

            storyboard.Add(0, 1, rotation);
            storyboard.Add(0, 0.5, exitRight);
            storyboard.Add(0.5, 1, enterLeft);

            storyboard.Commit(CompoundButton, "Compound", length: 1400);
        }


        private void ZoomyFadeButton_Clicked(object sender, System.EventArgs e)
        {
            var storyboard = new Animation();

            var fade_out = new Animation(callback: d => ZoomyFadeButton.Opacity = d,
                              start: ZoomyFadeButton.Opacity,
                              end: 0,
                              easing: Easing.SpringOut);

            var fade_int = new Animation(callback: d => ZoomyFadeButton.Opacity = d,
                              start: 0,
                              end: 1,
                              easing: Easing.SpringIn);

            storyboard.Add(0, 0.5, fade_out);
            storyboard.Add(0.5, 1, fade_int);

            storyboard.Commit(ZoomyFadeButton, "ZoomyFade", length: 1000);
        }

        private void BooshButton_Clicked(object sender, System.EventArgs e)
        {
            // Dirty hack you probably shouldn't use
            var width = Application.Current.MainPage.Width;

            var storyboard = new Animation();

            var fade_out = new Animation(callback: d => BooshButton.Opacity = d,
                              start: ZoomyFadeButton.Opacity,
                              end: 0,
                              easing: Easing.SpringOut);

            var fade_int = new Animation(callback: d => BooshButton.Opacity = d,
                              start: 0,
                              end: 1,
                              easing: Easing.SpringIn);

            var exitRight = new Animation(callback: d => LayoutRoot.TranslationX = d,
                               start: 0,
                               end: width,
                               easing: Easing.SpringIn);

            var enterLeft = new Animation(callback: d => LayoutRoot.TranslationX = d,
                                           start: -width,
                                           end: 0,
                                           easing: Easing.BounceOut);


            storyboard.Add(0, 1, fade_out);
            storyboard.Add(0.2, 0.5, exitRight);

            storyboard.Add(0.5, 1, fade_int);
            storyboard.Add(0.5, 1, enterLeft);

            storyboard.Commit(LayoutRoot, "ZoomyFade", length: 1000);
        }
    }
}
